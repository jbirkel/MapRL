using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

//using System.Runtime.InteropServices;

using System.Linq;
using System.Diagnostics;


namespace TextMap
{
   public class MapText {

      public MapText(Size size) { New(size); }

      const char CH_SPACE = ' ' ;
      const char CH_NULL  = '\0';
      const char CH_ESC   = '@' ;
      
      //readonly RL_Color ID_CR_NEW = RL_Color.White; // Black;
      
      public void Clear()              { Clear(new Selection( new Point(0, 0), new Point(-1,-1) + TB.Size)); }      
      public void Clear(Point cel)     { Clear(new Selection(cel, cel)); }
      public void Clear(Selection sel) { TextColor = RL_CR_MONO; FillRect(CH_SPACE, sel); }
      
      public RL_Color TextColor { set; get; }

      public void New(Size siz) {
         TB = new DataBlock<char>(siz, CH_SPACE);
         CB = new DataBlock<RL_Color>(siz, RL_CR_MONO);
         TextColor = RL_CR_MONO;
         //_fill_NoUndo(CH_FILL, new Selection(new Point(0, 0), new Point(TB.Size) - new Size(1, 1)));
      }
         
      public void FillRect(char c, Selection sel) {
         using (var U = new Undoable(this, sel)) {
            _fill_NoUndo(c, sel);
         }
      }      
      private void _fill_NoUndo(char c, Selection sel) {
         Rectangle r = sel.GetRect();
         var cc = new char    [r.Width];
         var cr = new RL_Color[r.Width];         
         for (int x = 0; x < r.Width; x++) { cc[x] = c; cr[x] = TextColor; }
         for (int y = r.Top; y < r.Bottom; y++) { 
            TB.SetElems(cc, 0, r.Left, y, r.Width);
            CB.SetElems(cr, 0, r.Left, y, r.Width);             
         }
      }
      
      public Color GetColor(int x, int y) { return GetColor(new Point(x, y)); }
      public Color GetColor(Point pt) {
         return PaletteByID[IsValidCell(pt) ? CB.GetElem(pt.X, pt.Y) : RL_CR_MONO].cr;
      }      

      public char GetCell(int x, int y) { return GetCell(new Point(x, y)); }
      public char GetCell(Point pt) {
         return IsValidCell(pt) ? TB.GetElem( pt.X, pt.Y) : CH_NULL;
      }

      public void SetCell(char c, Point pt) {
         if (IsValidCell(pt) && (CH_NULL != c)) {      
            using (var U = new Undoable(this, new Selection( pt, pt ))) {
               TB.SetElem(c    , pt.X, pt.Y);
               CB.SetElem(TextColor, pt.X, pt.Y);            
            }   
         }
      } 
      
      public void SetCells( char c, Point[] pts  ) {
         using (var U = new Undoable(this, new Selection(new Point(0,0), TB.Size))) {      
           foreach (var pt in pts) {
              TB.SetElem(c, pt.X, pt.Y);           
              CB.SetElem( TextColor, pt.X, pt.Y );
            }      
         }   
      }   
      
      public bool IsValidCell(Point ptCel) {
         Rectangle r = new Rectangle(new Point(0, 0), TB.Size);
         return r.Contains(ptCel);
      }

      // --------------------------------------------------------------------------------
      // Exports some or all of the MapText contents as string data.
      // -- Stringized TextMap data is one line of quoted text per row separated 
      //    by newlines.      
      // --------------------------------------------------------------------------------      
      private static readonly string NEWLINE = System.Environment.NewLine;

      //public string Serialize(Selection sel, bool bColor) {
      //   return bColor ? serialize_in_color ( sel ) 
      //                 : serialize_char_only( sel ) ;
      //}

      //private string serialize_in_color(Selection sel)      
      public string Serialize(Selection sel) 
      {
         bool bMonochrome = CB.IsAllElem( RL_CR_MONO ); 
      
         Rectangle r = sel.GetRect(); 
         char[] cc = new char[(r.Width * 4 + NEWLINE.Length) * r.Height];
         int i = 0;
         RL_Color rlCurr = RL_Color.None;
         for (int y = r.Top; y < r.Bottom; y++) {
            for (int x = r.Left; x < r.Right; x++) {
            
               char     ch = TB.GetElem(x, y);
               RL_Color rl = CB.GetElem(x, y);
               
               if (!bMonochrome && (rlCurr != rl && ch != ' ')) {
                  cc[i++] = CH_ESC;
                  cc[i++] = PaletteByID[rl].code ;  
                  rlCurr = rl;
               };

               cc[i++] = ch;
               if (CH_ESC == ch) cc[i++] = ch;  // Double the escape character. 
            }
            for (int j = 0; j < NEWLINE.Length; j++) { cc[i++] = NEWLINE[j]; }
         }
      
         return new string(cc).TrimEnd( null );
      }

      /*
      private string serialize_char_only (Selection sel)
      {
         Rectangle r = sel.GetRect();
         char[] cc = new char[(r.Width + NEWLINE.Length) * r.Height];
         int i = 0;
         for (int y = r.Top; y < r.Bottom; y++)
         {
            i += TB.GetElems(r.Left, y, cc, i, r.Width);
            for (int j = 0; j < NEWLINE.Length; j++)
            {
               cc[i++] = NEWLINE[j];
            }
         }
         return new string(cc);
      }
      */
      
      // -- convenience aliases
      public string Serialize()                       { return Serialize(new Point(0, 0), new Point(-1, -1) + TB.Size); }
      public string Serialize(Point cel1)             { return Serialize(cel1, cel1); }
      public string Serialize(Point cel1, Point cel2) { return Serialize(new Selection(cel1, cel2)); }

      // --------------------------------------------------------------------------------
      // Imports as much of the given string data as will fit in the given selection.
      // -- Inverse of Serialize operation.
      // --------------------------------------------------------------------------------            
      public void Deserialize(string s, Selection sel) {
         using (var U = new Undoable(this, sel.Clip(this.TB.Size))) {
            _deserialize( s,sel );                     
         }   
      }

      private void _deserialize(string s, Selection sel)
      {
         //char[] delim = NEWLINE.ToCharArray(); // .Take(1).ToArray();
         string[] delim = new string[] { NEWLINE };
         string[] Rows = s.Split(delim, StringSplitOptions.None ); // RemoveEmptyEntries);

         Rectangle r = sel.GetRect();
         RL_Color rlCurr = RL_CR_MONO; 
         
         for (int j = 0; j < Math.Min(Rows.Length, r.Height); j++)
         {
            // Get the next line;
            char[] row = Rows[j].ToCharArray();
            
            int x = r.Left;
            for (int i = 0; (i < row.Length) && (x < r.Right); ) {
            
               // Consume a character from the input stream.
               char ch = row[i++];
               
               // If it is the escape character we need to consume another character to
               // know how to proceed. 
               if (CH_ESC == ch) {
               
                  // Get the next character.  (It is either a second escape or a color code.)
                  ch = row[i++];               
               
                  // If not a double escape then set the current color ID by looking up
                  // the color code in the color table.
                  if (CH_ESC != ch) {               
                     try   { rlCurr = PaletteByCode[ ch ].idCR; }
                     catch { rlCurr = RL_CR_MONO; } // Handle invalid color code.
                     continue;                  
                  }
               }   
               
               // Set the current cell to the current character and color ID.
               TB.SetElem(ch    , x, r.Top + j);
               CB.SetElem(rlCurr, x, r.Top + j);
               x++;
            }            
         }
      }      
      
/*
      private void _deserialize(string s, Selection sel)            
      {
         char[] delim = NEWLINE.ToCharArray();
         string[] Rows = s.Split(delim, StringSplitOptions.None ); // .RemoveEmptyEntries);

         Rectangle r = sel.GetRect(); 

         int y; for (y = 0; y < Math.Min(Rows.Length, r.Height); y++) {
            string row = Rows[y];
            TB.SetElems(row.ToCharArray(), 0, r.Left, r.Top + y, Math.Min(row.Length, r.Width));            
         }
      }
*/
      // -- convenience aliases      
      public void Deserialize(string s)                         { Deserialize(s, new Point(0, 0), new Point(-1, -1) + TB.Size); }
      public void Deserialize(string s, Point cel)              { Deserialize(s, cel            , new Point(-1, -1) + TB.Size); }
      public void Deserialize(string s, Point cel1, Point cel2) { Deserialize(s, new Selection(cel1, cel2)); }

      // Flips the cells in the current selection either vertically or horizontally.
      public void Flip(bool bVertical, Selection sel)
      {
         using (var U = new Undoable(this, sel)) 
         {
            var chTmp = TB.Clone();
            var idTmp = CB.Clone();  
                      
            Rectangle r = sel.GetRect();
            
            for (int x = r.Left; x < r.Right; x++) {
               for (int y = r.Top; y < r.Bottom; y++) {
                  if (bVertical) { TB.SetElem( chTmp.GetElem(x, (r.Bottom + r.Top - 1) - y), x, y ); 
                                   CB.SetElem( idTmp.GetElem(x, (r.Bottom + r.Top - 1) - y), x, y ); }
                  else           { TB.SetElem( chTmp.GetElem((r.Right + r.Left - 1) - x, y), x, y );
                                   CB.SetElem( idTmp.GetElem((r.Right + r.Left - 1) - x, y), x, y ); }
               }
            }         
         }
      }

      // Rotates the cells in the current selection 90 degrees in the clockwise direction.
      // Note: We can only do rotates in a square region, so if the selection
      //       rectangle is not square, we truncate either the width or the height
      //       (which ever is greater) to make it so. 
      public void Rotate(Selection sel)
      {
         using (var U = new Undoable(this, sel)) 
         {
            var chTmp = TB.Clone();
            var idTmp = CB.Clone();          
            
            Rectangle r = sel.GetRect();
            
            r.Width = Math.Min(r.Width, r.Height);     
            r.Height = r.Width;
                  
            for (int x = 0; x < r.Width; x++) {
               for (int y = 0; y < r.Height; y++) {
                  TB.SetElem( chTmp.GetElem(r.Left + y, (r.Bottom - 1) - x), r.Left + x, r.Top + y);                          
                  CB.SetElem( idTmp.GetElem(r.Left + y, (r.Bottom - 1) - x), r.Left + x, r.Top + y);           
               }
            }
         }
         // Note: other transforms: 
         // - rotate left: SetCell(TempMap[r.Left - y, (r.Bottom - 1) + x], new Point(r.Left + x, r.Top + y));
         // - flip diag  : SetCell(TempMap[r.Left - y, (r.Bottom - 1) - x], new Point(r.Left + x, r.Top + y));
      }

      // ----------------------------------------------------------------------
      // DoomRL uses colors that are like the DOS character mode 16-color palette.
      // ----------------------------------------------------------------------

      public enum RL_Color { None, 
         Red  , Blue  , Green  , Magenta  , Cyan  , LtGray, DkGray, Brown,
         LtRed, LtBlue, LtGreen, LtMagenta, LtCyan, White , Black , Yellow
      }

      public struct ColorTable {
         readonly public RL_Color idCR;
         readonly public Color    cr;
         readonly public char     code;
         
         public ColorTable( RL_Color crRL, int RGB, char code ) {
            this.idCR = crRL;
            this.cr = ColorTranslator.FromWin32( RGB );  
            this.code = code;
         }
      }
      
      // Color ID we use for all cells when in monochrome mode.
      const RL_Color RL_CR_MONO = RL_Color.White;
      const RL_Color RL_CR_BACK = RL_Color.Black;      
      
      static public readonly ColorTable[] Palette = {
         new ColorTable( RL_Color.Red      , Util.RGB(0x80,0x00,0x00), 'r' ),  
         new ColorTable( RL_Color.Blue     , Util.RGB(0x00,0x00,0x80), 'b' ),  
         new ColorTable( RL_Color.Green    , Util.RGB(0x00,0x80,0x00), 'g' ),  
         new ColorTable( RL_Color.Magenta  , Util.RGB(0x80,0x00,0x80), 'v' ),  
         new ColorTable( RL_Color.Cyan     , Util.RGB(0x00,0x80,0x80), 'c' ),    
         new ColorTable( RL_Color.LtGray   , Util.RGB(0xC0,0xC0,0xC0), 'l' ),  
         new ColorTable( RL_Color.DkGray   , Util.RGB(0x80,0x80,0x80), 'd' ),            
         new ColorTable( RL_Color.Brown    , Util.RGB(0x80,0x80,0x00), 'n' ),           
         new ColorTable( RL_Color.LtRed    , Util.RGB(0xFF,0x00,0x00), 'R' ),  
         new ColorTable( RL_Color.LtBlue   , Util.RGB(0x00,0x00,0xFF), 'B' ),   
         new ColorTable( RL_Color.LtGreen  , Util.RGB(0x00,0xFF,0x00), 'G' ),   
         new ColorTable( RL_Color.LtMagenta, Util.RGB(0xFF,0x00,0xFF), 'V' ),             
         new ColorTable( RL_Color.LtCyan   , Util.RGB(0x00,0xFF,0xFF), 'C' ),    
         new ColorTable( RL_Color.White    , Util.RGB(0xFF,0xFF,0xFF), 'L' ),              
         new ColorTable( RL_Color.Black    , Util.RGB(0x00,0x00,0x00), 'D' ),     
         new ColorTable( RL_Color.Yellow   , Util.RGB(0xFF,0xFF,0x00), 'y' ),    
      };

      static public Dictionary<RL_Color, ColorTable> PaletteByID   = Palette.ToDictionary<ColorTable, RL_Color>(a => a.idCR);
      static public Dictionary<char    , ColorTable> PaletteByCode = Palette.ToDictionary<ColorTable, char    >(a => a.code);


      // ----------------------------------------------------------------------
      // 
      // Undo/Redo 
      //
      // Maintain a list of discrete edit operations as a pair of MapText
      // serialized strings, a before and an after.
      // - On Undo, apply the 'Before' MapText object and decrement list ptr
      // - On Redo, increment list ptr and apply the 'After' MapText object
      //
      // ----------------------------------------------------------------------   
      public bool CanUndo() { return Hist.IsPrev(); }
      public bool CanRedo() { return Hist.IsNext(); }

      public bool Undo() {
         if (!CanUndo()) { return false; }

         MapSegment ms = Hist.Prev();
         _deserialize(ms.Text, ms.Sel);
         return true;
      }

      public bool Redo() {
         if (!CanRedo()) return false;
         
         MapSegment ms = Hist.Next();
         _deserialize(ms.Text, ms.Sel);
         return true;
      }  
      
   // ---   Private   ---------------------------------------------------------

      private DataBlock<char>     TB;
      private DataBlock<RL_Color> CB;

      private EditHistory Hist  = new EditHistory();

      private class Undoable : UsingBlock 
      {
         private MapText mt;
         private Selection sel;
         private string sBefore;
      
         public Undoable(MapText mt, Selection sel) {
            this.mt  = mt;
            this.sel = sel;
            sBefore = mt.Serialize(sel);
         }

         public override void Finally()  {
            mt.Hist.Add(sel, sBefore, mt.Serialize(sel));         
         }
      }
   }

   // -----------------------------------------------------------------------------------
   // Abstraction of a 2-dimensional array of simple data type.
   // 
   // NOTES: 
   // - Internally, reverses the X and Y coords so that consecutive elements in a row 
   //   are stored consecutively in the data buffer.
   // - Stores data in a 1-dim array to allow the use of the Array.Copy function.
   // -----------------------------------------------------------------------------------   
   struct DataBlock<T> where T: struct
   {
      private T[] _db;   // 1-dim array buffer modelled as a 2-dim array
      
      public readonly Size Size;

      public DataBlock(Size size, T tInit) { 
         Size = size;
         _db = new T[size.Height * size.Width];
         for (int i=0; i<_db.Count(); i++) { _db[i] = tInit; }
      }
      public DataBlock( DataBlock<T> db) { 
         Size = db.Size;
         _db = (T[])db._db.Clone(); 
      }

      public DataBlock<T> Clone() { return new DataBlock<T>( this ); }   
      
      // This is what defines our 2-dim abstraction. 
      private int index( int x, int y ) { return x + y * Size.Width; }
      
      public T    GetElem(     int x, int y) { return _db[index(x,y)]    ; }
      public void SetElem(T t, int x, int y) {        _db[index(x,y)] = t; }

      //
      // GetElems/SetElems allow copying of multiple data elements between 
      // the DataBlock and a caller's one-dim array.  
      // - the starting location in the DataBlock is given by an x,y pair
      // - the caller supplies a single-dimension array of T and an offset
      //
      public int GetElems(int x, int y, T[] tt, int ofs, int len) { 
         Array.Copy(_db, index(x, y), tt, ofs, len);         
         return len;
      }
      public int SetElems(T[] tt, int ofs, int x, int y, int len) {
         Array.Copy(tt, ofs, _db, index(x, y), len);
         return len;
      }

      public bool IsAnyElem(T t) { return 0 != (from elem in _db where  elem.Equals(t) select elem).Count(); }
      public bool IsAllElem(T t) { return 0 == (from elem in _db where !elem.Equals(t) select elem).Count(); }      
   }
       
/*
   Char-only datablock with SetChars/GetChars members that used Buffer.BlockCopy
   for speed.  It turns out that BlockCopy does not work well with generic data
   types, because sizeof(T) is not supported and Marshalled data sizes do not
   seem to match the data size used in an array of T.  (Leaving it here only as
   an example of Buffer.BlockCopy usage.  Incidentally, Array.Copy is not noticably
   slower in this application.)
   
   struct TextBlock 
   {
      const int CHSIZ = sizeof(char);
      
      private char[,] _tb;
      
      public TextBlock( Size size ) { _tb = new char[ size.Height, size.Width ]; }
      public TextBlock(char[,] txt) { _tb = (char[,])txt.Clone();                }

      public TextBlock Clone() { return new TextBlock(_tb); }   
      
      public char GetChar(        int x, int y) { return _tb[y, x]    ; }
      public void SetChar(char c, int x, int y) {        _tb[y, x] = c; }

      //
      // GetChars/SetChars are optimized routines that allow direct copying
      // of multiple chars between the TextBlock and a caller's one-dim array 
      // of characters.  
      // - the starting location in the TextBlock is given by an x,y pair
      // - the caller supplies a char array and an offset
      //
      // All the passed-in argument are in units of characters.  Internally everything
      // is converted to bytes for the call to BlockCopy.
      //
      public int GetChars(int x, int y, char[] cc, int ofs, int len) { 
         int srcOfs = CHSIZ * (y * Width + x);
         int dstOfs = CHSIZ * ofs ;        
         int blkLen = CHSIZ * len ; 
         Buffer.BlockCopy( _tb, srcOfs, cc, dstOfs, blkLen );
         return len;
      }
      public int SetChars(char[] cc, int ofs, int x, int y, int len) {
         int srcOfs = CHSIZ * ofs;
         int dstOfs = CHSIZ * (y * Width + x);
         int blkLen = CHSIZ * len;
         Buffer.BlockCopy(cc, srcOfs, _tb, dstOfs, blkLen);
         return len;
      }
      
      //public char[] GetBlock( Selection sel )            { return new char[0]; }
      //public void   SetBlock( Selection sel, char[] cc ) {}
      
      public int  Width  { get { return _tb.GetUpperBound(1) + 1; } }
      public int  Height { get { return _tb.GetUpperBound(0) + 1; } }
      public Size Size   { get { return new Size(Width, Height) ; } }
   }
*/

   // Defines the items in the Undo/Redo list.
   struct EditHistItem
   {
      private static int _cnt = 0;
   
      public EditHistItem(Selection sel, string Before, string After) {
         this.seq = _cnt++;      
         this.sel = sel;
         this.Before = Before;
         this.After = After;
      }

      public int       seq;
      public Selection sel;
      public string Before;
      public string After;
   }

   // A MapSegment is intended to represent a rectangular-sub-region of a map.
   // In practice this means it is a MapText object with a selection area.
   // (It should obvously also have a size less than or equal to the current map, 
   // but this is not enforced.)
   struct MapSegment
   {
      public MapSegment(Selection sel, string text) {
         this.sel = sel;
         this.text = text;
      }

      private Selection sel;   // selection rectangle of the map segment
      private String    text;  // contents (as serialized by a MapText object)

      public Selection Sel  { get { return sel; } }
      public string    Text { get { return text; } }

      public bool IsEmpty() { return null == text; }
   }

   class EditHistory
   {
      const int MAX_LEN = 1000;

      private int _end; // Last usable index in list      
      private int _pos; // Current index in the list
      private List<EditHistItem> _list = new List<EditHistItem>();
    
      public EditHistory() { _end = _pos = -1; }
  
      public bool IsPrev() { return 0 <= _pos; }
      public bool IsNext() { return _pos < _end; }

      public MapSegment Prev() {
         if (!IsPrev()) { return new MapSegment(); };
         MapSegment ms = new MapSegment(_list[_pos].sel, _list[_pos].Before);
         _pos--;
         return ms;
      }

      public MapSegment Next() {
         if (!IsNext()) { return new MapSegment(); }
         _pos++;
         MapSegment ms = new MapSegment(_list[_pos].sel, _list[_pos].After);
         return ms;
      }

      // Note: After an 'Add' the current pointer is always the end of the list.
      public void Add(Selection sel, string sBefore, string sAfter)
      {
         //Debug.WriteLine(String.Format("EditHistory.Add( {0}, {1}, '{2}', '{3}'", _pos, sel.GetRect().Location, sBefore, sAfter));
         
         // Ignore no-change changes.
         if (0 == sBefore.CompareTo(sAfter)) {
            //Debug.WriteLine(String.Format("--- skipping, before & after identical" ));         
            return;
         }
      
         // Increment the list position for the new history element.
         _pos++;
         
         // If we have available space, insert the item in the current position, else add to end.
         if (_pos < _list.Count) { _list[_pos] = new EditHistItem(sel, sBefore, sAfter) ; }
         else                    { _list.Add   ( new EditHistItem(sel, sBefore, sAfter)); }

         // Keep the history list from growing without bound.         
         // NOTE: While we're at the limit we always drop the oldest (first) element.
         if(_pos > MAX_LEN) {
            _pos = MAX_LEN;
            _list.RemoveAt(0);
         }

         // Whenever we add an item we are at the end of the history list.
         _end = _pos;
      }
   }
}
