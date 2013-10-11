using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Text;
using System.Windows.Forms;

using System.Runtime.InteropServices; // For "DllImport"

namespace TextMap
{
   public class MapBitmap 
   {
      public MapBitmap(Size size) { New( size ); }
      public MapBitmap(Size sizeGrid, Size sizeCell ) { New( sizeGrid, sizeCell ); }
      
      public Bitmap Bitmap     { get { return _bmpFore; } }
      public Bitmap Background { get { return getBackImage(); } 
                                 set { _bmpBack = value; } }

      public void New(Size size) {
         _bmpFore = new Bitmap(size.Width, size.Height); 
         CalcCellSize( size );
      
         /*T*E*S*T*/
         //string [] FixFonts = GetAvailableFontList();
         //MessageBox.Show( String.Join( ", ",  FixFonts )); 
         /*T*E*S*T*/


         //string s = "";
         //InstalledFontCollection installedFontCollection = new InstalledFontCollection();
         //int count = installedFontCollection.Families.Length;
         //for (int j = 0; j < count; ++j) {
         //   s = s + installedFontCollection.Families[j].Name + System.Environment.NewLine;
         //}
         //System.Diagnostics.Debug.WriteLine(s);
         
      }
      
      public void New(Size sizeGrid, Size sizeCell) { 
         _sizeGrid = sizeGrid;
         _sizeCell = sizeCell;
         _bmpFore = new Bitmap(sizeGrid.Width * sizeCell.Width, sizeGrid.Height * sizeCell.Height); 
         calcFontSize( _sizeCell );
      }

      public Size Size     { get { return _bmpFore.Size; } }
      public Size CellSize { get { return _sizeCell; } }
      public Size GridSize { get { return _sizeGrid; }
                             set { _sizeGrid = value;
                                   CalcCellSize( Size );
                                 } }   

      // Clears and draws the entire map from the data in the given MapTxt object.
      public void DrawMap( MapText mapText )
      {
         using (var g = Graphics.FromImage(_bmpFore)) {

            Color cr = windowColor();
            g.Clear(cr);

            // Draw grid lines
            Pen pen = new Pen(CR_GRID);

            Point pt1 = new Point(0, 0);
            Point pt2 = new Point(0, 0);

            pt1.Y = 0; pt2.Y = _sizeGrid.Height * _sizeCell.Height;
            int i; for (i = 0; i <= _sizeGrid.Width; i++) {
               pt1.X = pt2.X = i * _sizeCell.Width;
               g.DrawLine(pen, pt1, pt2);
            }

            pt1.X = 0; pt2.X = _sizeGrid.Width * _sizeCell.Width;
            for (i = 0; i <= _sizeGrid.Height; i++) {
               pt1.Y = pt2.Y = i * _sizeCell.Height;
               g.DrawLine(pen, pt1, pt2);
            }

            // Draw Chars
            for (int x = 0; x < _sizeGrid.Width; x++) {
               for (int y = 0; y < _sizeGrid.Height; y++) {
                  drawChar(g, mapText.GetCell(x, y), mapText.GetColor(x, y), new Point(x, y));
               }
            }
         }
      }
      
      // Clears and draws a letter in a cell in the given bitmap. 
      public Rectangle DrawCell(Point ptCell, MapText mapText)
      {
         Rectangle r = RectFromCell(ptCell);
         using (Graphics g = Graphics.FromImage(_bmpFore))
         {
            Rectangle r1 = new Rectangle(r.Location, r.Size + new Size(1, 1));
            g.FillRectangle(new SolidBrush(windowColor()), r1);
            drawChar(g, mapText.GetCell(ptCell),mapText.GetColor(ptCell), r);
         }
         return r;
      }

      public Rectangle RectFromCell(Point pt) { return RectFromSel( new Selection( pt, pt )); }

      // This rectangle lies strictly inside the gridlines.
      public Rectangle RectFromSel(Selection sel) {
         Point pt1 = sel.pt1;
         Point pt2 = sel.pt2;
         Util.NormalizeExtents(ref pt1, ref pt2);
         return new Rectangle
         (  new Point(_sizeCell.Width *  pt1.X                 , _sizeCell.Height *  pt1.Y                  )
         ,  new Size (_sizeCell.Width * (pt2.X - pt1.X + 1) - 1, _sizeCell.Height * (pt2.Y - pt1.Y + 1) - 1 )
         );
      }

      public void CalcCellSize(Size mapSize) {
         Size sizeNew = new Size(mapSize.Width / _sizeGrid.Width, mapSize.Height / _sizeGrid.Height);
         if (!_sizeCell.Equals( sizeNew )) {
            _sizeCell = sizeNew;
            calcFontSize(_sizeCell);
         }   
      }

      public Point CellFromPoint(Point pt)
      {
         return new Point(Math.Max(0, Math.Min(_sizeGrid.Width - 1, pt.X / _sizeCell.Width))
                         , Math.Max(0, Math.Min(_sizeGrid.Height - 1, pt.Y / _sizeCell.Height)));
      }
           
      // In case we wanted to offer use an option of which installed fixed-width
      // font to use for drawing characters.

/*
      [DllImport("_testdll.dll")]
      public static extern int IsFixedFont(IntPtr hFont);      
//      public static extern bool IsFixedFont( IntPtr hFont );
      
      public string[] GetAvailableFontList() {
         var ret = new List<string>();
         var IFC = new System.Drawing.Text.InstalledFontCollection();
         FontFamily [] FFA = IFC.Families;
         foreach (var ff in FFA) { 
            Font f;
            try { f = new Font( ff, 10 ); }
            catch (Exception xx) { continue;}   
            
            // from here we need an IsFixedWidth property
            //if (IsFixedFont( f.ToHfont())) {
            //   ret.Add(ff.Name);
            //}
            ret.Add( IsFixedFont( f.ToHfont()).ToString() );
         }
         return ret.ToArray();
      }
*/
      
   // -------------------------------------------------------------------------
   //    Private
   // -------------------------------------------------------------------------   

      readonly Color CR_BACK = Color.Black;
      readonly Color CR_GRID = Util.ColorFromRGB(32, 32, 32);

      // Note: These don't work: "Terminal", "System", "Monospace" 
      private const string _sFontName = "Lucida Console";
      //private const string _sFontName = "Consolas"      ; 
          
      private Bitmap _bmpFore, _bmpBack;
      private FontFamily _ffam = new FontFamily(_sFontName);
      private Font _font;

      private byte _byOpacity = 0xc0;
      
      Size _sizeGrid, _sizeCell; // , _sizeFont;

      private void calcFontSize(Size cellSize) {
         //_font = new Font(sFontName, (float)Math.Min(cellSize.Height*1.35, cellSize.Width/0.8) );
      
         var g = Graphics.FromImage(_bmpFore);
         Font good = _font;
         for (float em = 1; em<1000; em+=1) {
         
            // Create a font of the given em size.
            Font test = new Font(_ffam, em);                     
               
            // Measure the width of a wide character.   
            StringFormat fmt = new StringFormat(); // StringFormatFlags.FitBlackBox );
            fmt.Alignment = StringAlignment.Center; // .Near ;
            fmt.SetMeasurableCharacterRanges(new CharacterRange[] { new CharacterRange(0, 1), new CharacterRange(1,1) });
            Region[] rgns = g.MeasureCharacterRanges( "Wj", test, new Rectangle( new Point(0,0), cellSize + cellSize ), fmt );
            int wid = (int)Math.Round(rgns[0].GetBounds(g).Width); //(rgns[0].GetBounds(g).Size + new SizeF(0.5F,0.5F)).ToSize().Width;
            //hgt = (int)Math.Floor(rgns[1].GetBounds(g).Height); //(rgns[1].GetBounds(g).Size + new SizeF(0.5F, 0.5F)).ToSize().Height;            
            
            // Line height is fixed.
            int hgt = test.Height;
            
            // If this font fits in our cell size, it's good.  Save it and try the next size, otherwise
            // exit the loop and use the last good font. 
            if ((wid < cellSize.Width) && (hgt < cellSize.Height)) {
               good = test;
            } else {
               break;
            }                          
         }
         
         _font = good;
         //_sizeFont = new Size( wid, hgt );
         
         // debug
         string dbg = String.Format("Font: {0},{1},{2},{3}", _font.Name, _font.OriginalFontName, _font.FontFamily, _font.Size);
         System.Diagnostics.Debug.WriteLine( dbg ); 
      }
      private void drawChar(Graphics g, char c, Color cr, Rectangle r)
      {
         // This is a great way to draw the grid, but only if letters never 
         // hang over into an adjacent cell.
         //g.FillRectangle( new SolidBrush( windowColor()), r );
         //g.DrawRectangle( new Pen( CR_GRID ), r ); 

         StringFormat fmt = new StringFormat(); // StringFormatFlags.FitBlackBox);
         fmt.Alignment = StringAlignment.Center; // Near;
         //r.X -= (int)(_sizeFont.Width * .3 + 0.5) - 2;
         //r.Width += (int)(_sizeFont.Width * .3 + 0.5) + 1;
         r.Y += 1;
         r.X += 1;
         g.DrawString(c.ToString(), _font, new SolidBrush( cr ), r, fmt);
      }
      private void drawChar(Graphics g, char c, Rectangle r)  {
         drawChar(g, c, SystemColors.WindowText, r);
      }
      private void drawChar(Graphics g, char c, Color cr, Point cell) {
         drawChar(g, c, cr, RectFromCell(cell));      
      }      
      
      private Color windowColor() {
         bool bBack = (null != _bmpBack) ; 
         byte alpha = (bBack) ? _byOpacity : (byte)255;
         return CR_BACK; // Color.FromArgb(alpha, SystemColors.Window);
      }


      // Returns a bitmap that is the current background image stretched to
      // fit the size of the foreground bitmap. 
      private Bitmap getBackImage() {
         if (null == _bmpBack) return null;

         Bitmap img = new Bitmap(_bmpFore.Size.Width, _bmpFore.Size.Height);
         Graphics g = Graphics.FromImage(img);
         g.DrawImage(_bmpBack, getMapRect());
         return img;
      }

      private Rectangle getMapRect() {
         return new Rectangle(0, 0, _sizeCell.Width * _sizeGrid.Width, _sizeCell.Height * _sizeGrid.Height);
      }

      private Point pointFromCell(Point cel) {
         return new Point(cel.X * _sizeCell.Width, cel.Y * _sizeCell.Height);
      }  
   };
   
   
   // A scratchpad on which to draw lines and ellipses that can provide a list of 
   // coordinates that can be used to draw the same lines on the text grid. 
   public class DrawPad {
   
      readonly Color CR_BACK = Color.White;
      readonly Color CR_FORE = Color.Black;      

      public DrawPad(Size size) { New(size); }   
   
      public void New(Size size) {
         _bmp = new Bitmap(size.Width, size.Height);
      }

      public Point[] DrawLine(Selection sel) {
         return drawShape(sel, (g, s) => g.DrawLine(new Pen(CR_FORE), sel.pt1, sel.pt2));                  
      }

      public Point[] DrawRect(Selection sel) {
         return drawShape(sel, (g, s) => g.DrawRectangle(new Pen(CR_FORE), sel.GetTrimRect()));            
      }

      public Point[] DrawEllipse(Selection sel) {
         return drawShape(sel, (g, s) => g.DrawEllipse(new Pen(CR_FORE), sel.GetTrimRect()));      
      }

      public Point[] FillEllipse(Selection sel) {
         return drawShape(sel, (g, s) => { 
            g.FillEllipse(new SolidBrush(CR_FORE), sel.GetTrimRect());         
            g.DrawEllipse(new Pen       (CR_FORE), sel.GetTrimRect());
         });
      }

      //public Point[] DrawClosedCurve(Selection sel) {
      //   Rectangle r = sel.GetTrimRect();
      //   r.Inflate(-5,-5);
      //   Point[] pts = new Point[] { r.Location, new Point (r.Right,r.Top), new Point (r.Right,r.Bottom), new Point (r.Left, r.Bottom) } ;
      //   return drawShape( sel, (g,s) => g.DrawClosedCurve( new Pen(CR_FORE), pts, 0.2F, System.Drawing.Drawing2D.FillMode.Alternate  ));
      //}

      private Point[] drawShape(Selection sel, Action<Graphics, Selection> f )
      {
         using (Graphics g = Graphics.FromImage(_bmp)) {
            g.Clear(CR_BACK);
            f( g, sel );
         }
         return getPixelList(sel);
      }
             
      private Point[] getPixelList( Selection sel ) {
      
         var pts = new List<Point>();

         Point cel1 = sel.pt1;
         Point cel2 = sel.pt2;
         Util.NormalizeExtents(ref cel1, ref cel2);    
      
         int argbFore = CR_FORE.ToArgb(); 
         for (int x = cel1.X; x <= cel2.X; x++) {
            for (int y = cel1.Y; y <= cel2.Y; y++) {
               if (argbFore == _bmp.GetPixel(x, y).ToArgb()) {
                  pts.Add(new Point(x, y));
               }
            }
         }
         
         return pts.ToArray();
      }

      private Bitmap _bmp;   
   }
   
}   