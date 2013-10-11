// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
//
// TextMap
//
//   Map editor for console mode (text graphics) dungeon crawlers.
//
//   Jeff Birkel - August, 2011
//
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
// Version History:
//
// 0.9.7
// - Added drawing modes: line, rectangle, ellipse, filled ellipse.  (Selection
//   mode can be used for filled rectangles, as before.)
// - Added 15-color drawing (Black is not allowed as a foreground color.)
// - Copy/paste now embeds Valkyrie color escapes in color maps.  (But not in 
//   all-white maps.)
// - Changed to a black background.
// - Improved font auto-sizing (again!)
// - Eliminated some uglies in the selection drawing
// - Improved ease of use: Shift key "paints" with the last pressed key.
//   The space bar is never saved so once the key is selected, drawing can be 
//   done by toggling between just the shift and space keys.
//
// 0.9.6 
// - Added font auto-size to fit the current grid size.
// - Changed font to Lucida Console
// - Fixed error on minimize.
//
// 0.9.5 
// - Added Undo/Redo.
// - Increased max grid size to 160 x 100
// - Fixed crash after changing grid size.
//
// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
// To-Do's:
//
// - right-click in a selection enters move mode
//
// - Add special (non-printing) key handling
//   - with a selection active arrows move and size
//   - shift-arrow (up/dn) changes colors?
//   - arrow keys move the cursor, enter 1x1 select mode
//   - maybe pressing a key move one to the right, like typing?
//   - color painting: hold Ctrl, arrow changes colors only
//   - shift-key painting is not quite right:
//     - Can't do single cells with it
//     - keyUp of any key hile shift is held, turns it off
//       (Maybe query shift key rather than tracking up/downs?)
//   
// - Enter key does something weird: changes map size?
//
// - Background image stuff is messed up
//   - How did I used to refresh the background image whenchanging the contents 
//     of a cell?
//   - Background image might actually be useful for drawing ASCII art.
//
// - Undo (working but flaky?)
// - Font selection: Lucida Console, Consolas, Terminal, System?
// - Design is becoming unwieldy
// - Deselect key (ESC)
//   
// - Add Map, Opacity menu item.  Opens to a "live" dialog with slider in 
//   10% increments from 0 to 100.  No cancel button.  Disabled if no 
//   background image loaded.
//
// - Drag and drop of selection boxes.
//
// Bugs:
// - Fix partial erasure of text when moving cursor over map at max grid size.
//
// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

using System.Runtime.Serialization.Formatters.Binary;

using System.Linq;
      
namespace TextMap
{
   public partial class Form1 : Form
   {
      // ----------------------------------   
      // Constants
      // ----------------------------------      
      
      const string NO_SELECTION = "No Selection";
      const string NO_KEYCACHED = "No Key";

      const char CH_SPACE = ' ';      
      const char CH_WALL  = '#';
   
      readonly Point PT_NO_VALUE = new Point( -1, -1 );

      readonly Color CR_SELECTION = Color.FromArgb(0x80, SystemColors.Highlight);
      
      
      // ----------------------------------
      // Major Application State variables
      // ----------------------------------      

      Point _ptCelCurr, _ptCelLast, _ptCelDown;
      
      bool      _bSelection = false;
      Selection  _SelCurr;
      //Point[] _ptsSelCurr;
      //Rectangle _rSelLast;
      Region  _rgnSelLast;

      bool _bMouseDown = false;
      bool _bInitDone;

      string _sSaveFileName = null;
      string _sBkgrFileName = null;

      // Should be locked whenever painting the map control.   ???
      private Object _mapLock = new Object();

      // ----------------------------------            
      // Major application objects.
      // ----------------------------------            

      MapText   MapTxt;
      MapBitmap MapBmp;
      DrawPad   PadBmp;
     
      // -------
      // Logging
      // -------
      void DEBUG_OUT( string s ) {
         System.Diagnostics.Debug.WriteLine(String.Format("{0}: {1}", AppDomain.CurrentDomain.FriendlyName, s));
      }
      
      // ----------------------------------------------------------------------
      // Form Events
      // ----------------------------------------------------------------------                 
     
      public Form1()
      {
         _bInitDone = false;

         // Load saved settings.         
         Size sizeGrid = Properties.Settings.Default.GridSize;
         Size sizeCell = Properties.Settings.Default.CellSize;         
         
         // Init our drawing objects based on the saved cell and grid sizes.   
         MapTxt = new MapText( sizeGrid );
         PadBmp = new DrawPad( sizeGrid );
         MapBmp = new MapBitmap(sizeGrid, sizeCell);
         
         // Whack the beehive.
         InitializeComponent();

         _KSH = new KeyState( this, CH_WALL );

         InitDrawModesTable();
         InitColorToolbar();                      
                     

         // Resize the app to fit the client area to the current grid and cell sizes. 
         Size += new Size((MapBmp.Size.Width - picMap.Size.Width)
                        , (MapBmp.Size.Height - picMap.Size.Height));    

         picMap.Image = MapBmp.Bitmap;
         RefreshMap();

         UpdateCaption() ;
         
         sbarMsg.Text = "Hello Dungeon Maker!";
         //sbarKey.Text = _chKeyDown.ToString(); // NO_KEYCACHED;
         sbarSel.Text = NO_SELECTION;

         _bInitDone = true;
      }

      private void Form1_FormClosing(object sender, FormClosingEventArgs e)
      {
         if (_bInitDone)
         {
            Properties.Settings.Default.GridSize = MapBmp.GridSize;
            Properties.Settings.Default.CellSize = MapBmp.CellSize;
            Properties.Settings.Default.Save();
         }   
      }

      private void UpdateCaption()
      {
         string s = Properties.Settings.Default.AppTitle;
         if (null != _sSaveFileName)
         {
            s += " - " + _sSaveFileName;
         }
         Text = s;
      }
      
      // ----------------------------------------------------------------------
      // 
      // Keyboard Event Handling 
      //
      // ---------------------------------------------------------------------- 
      
      class KeyState {
      
         public KeyState( Form1 f, char chInit ) { 
            _f = f; 
            _curr = _cache = chInit ; 
            _e = new KeyEventArgs(Keys.None);             
         }
         
         public bool Down { get { return _down || _e.Shift ; }}
         public char Curr { get { return _curr; } }

         public void KeyDown(KeyEventArgs e) {
            _e = e;         
            if (Keys.ShiftKey == e.KeyCode) {
               _f.KeyPressHandler(_curr);            
            }
         }
         public void KeyUp(KeyEventArgs e) {
            _down = false;
            _curr = _cache;
            _e = e;
         }

         public void KeyPress(KeyPressEventArgs e) {
         
            // Ignore certain keys.
            char[] chIgnore = { '\r' };
            if (chIgnore.Contains( e.KeyChar )) {
               e.Handled = true ;
               return;
            } 
                     
            // The last-pressed key becomes the current key.
            _down = true;
            _curr = e.KeyChar ;
            
            // Cache the key if it is NOT the space key.   
            if (CH_SPACE != _curr) { _cache = _curr; }

            // Update the status bar.
            updateKeyStatusText();
            
            _f.KeyPressHandler( _curr );
         }
         
         // Private
         KeyEventArgs _e;
         bool _down;
         char _curr, _cache;
         Form1 _f;

         void updateKeyStatusText() {
            _f.sbarKey.Text = (CH_SPACE == _curr) ? "Eraser" : String.Format("Key: '{0}'", _curr) ;
         }
      }
      
      KeyState _KSH;
      
      void KeyPressHandler( char ch ) 
      {
         if (_bSelection) {
            FillSelection( ch );
            RefreshMap();
         }
         else {
            MapTxt.SetCell( ch, _ptCelCurr);
            RefreshCell(_ptCelCurr);
         }
      }      
      
      private void Form1_KeyPress(object sender, KeyPressEventArgs e) {
         _KSH.KeyPress( e );
         DEBUG_OUT(String.Format("KeyPress: '{0}'", e.KeyChar ));
      }
      private void Form1_KeyDown(object sender, KeyEventArgs e) {
         _KSH.KeyDown( e );
         DEBUG_OUT(String.Format("KeyDown: Code:{2},Data:{3},Val:{4},ALT:{0},CTL:{1},SH:{6},Mods:{5}",
              e.Alt.ToString()[0], e.Control.ToString()[0], e.KeyCode, e.KeyData, e.KeyValue, e.Modifiers, e.Shift.ToString()[0]
         ));                    
      }
      private void Form1_KeyUp  (object sender, KeyEventArgs e) {
         _KSH.KeyUp( e ); 
         DEBUG_OUT(String.Format("KeyUp  : Code:{2},Data:{3},Val:{4},ALT:{0},CTL:{1},SH:{6},Mods:{5}",
             e.Alt.ToString()[0], e.Control.ToString()[0], e.KeyCode, e.KeyData, e.KeyValue, e.Modifiers, e.Shift.ToString()[0]
         ));    
      }
      private void Form1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e) { }

      private void sbarKey_Click(object sender, EventArgs e) {
         //_chKeyDown = '\0';
         //sbarKey.Text = String.Format(NO_KEYCACHED);
      }

      private void sbarSel_Click(object sender, EventArgs e) {
         //ClearSelectionState();
         //RefreshMap();   
      }
      
      // ----------------------------------------------------------------------
      // 
      // Mouse Events (PictureBox) 
      //
      // ---------------------------------------------------------------------- 
      private void picMap_MouseDown(object sender, MouseEventArgs e)
      {
         // Save the coords of the cell in which the mouse down even occurred.
         _ptCelDown = MapBmp.CellFromPoint( e.Location );

         // If there is a selection, reset it. 
         if (_bSelection) {
            ClearSelectionState(); // EndSelectState();
            RefreshMap();
            PointerUpdate(e); 
         }
         
         _bMouseDown = true;
         UpdateMouseStatusText(e);
      }
      
      private void picMap_MouseUp(object sender, MouseEventArgs e)
      {
         _bMouseDown = false;
         UpdateMouseStatusText(e);
         
         // NOTE: Mouse-Up does not end the selection state. If a selection
         //       was made while the mouse was down it stays in effect now.
      }

      private void picMap_MouseMove(object sender, MouseEventArgs e)
      {
         if (PointerUpdate(e)) { 
         
            // If no selection and a key is pressed we are in text painting mode.
            if (!_bSelection && _KSH.Down) {
               MapTxt.SetCell(_KSH.Curr, MapBmp.CellFromPoint(e.Location));
            }         
         }   
         UpdateMouseStatusText(e);
      }

      private void UpdateMouseStatusText(MouseEventArgs e)
      {
         sbarCell.Text = String.Format("{0}, {1}", _ptCelCurr.X, _ptCelCurr.Y);
         if (_bSelection) {
            Rectangle r = _SelCurr.GetRect(); //  GetSelectRect();
            sbarSel.Text = String.Format("{0}, {1} - {2}, {3} ({4} x {5})"
                                   , _SelCurr.pt1.X, _SelCurr.pt1.Y
                                   , _SelCurr.pt2.X, _SelCurr.pt2.Y
                                   , r.Width, r.Height );
         }
      }

      // PictureBox is being resized.
      private void picMap_SizeChanged(object sender, EventArgs e)
      {
         if (!_bInitDone) return;
      
         // Ignore minimize operations, since the map is not viewable in that state.
         if (WindowState == FormWindowState.Minimized) return;

         MapBmp.New(picMap.Size);
         picMap.Image = MapBmp.Bitmap;
         sbarCell.Text = String.Format("Map: {0} x {1} (cell {2} x {3})"
                                      , picMap.Size.Width, picMap.Size.Height
                                      , MapBmp.CellSize.Width, MapBmp.CellSize.Height);
         PaintBackground();
         RefreshMap();

         //if (null != picMap.BackgroundImage) { DrawBackground(); }
      }

      //      private void map_Layout(object sender, LayoutEventArgs e) {
      //         if (WindowState == FormWindowState.Minimized) return;      
      //      }

      // ----------------------------------------------------------------------
      //
      // Selection Painting
      //
      // ----------------------------------------------------------------------      


      private void ClearSelectionState() {
         _bSelection = false;
         _SelCurr = new Selection( new Point(-1, -1), new Point(-1, -1) );
         _ptCelLast = PT_NO_VALUE;
         sbarSel.Text = NO_SELECTION;         
      }
            
      // ----------------------------------------------------------------------
      //
      // Painting the the MapBitmap to the PictureBox (Refresh operations)
      //
      // ----------------------------------------------------------------------      
      
      private void UpdateRegion( Region rgn ) {
         //picMap.Image = MapBmp.Bitmap;
         // PaintBackground();
         picMap.Invalidate(rgn);
         picMap.Update();    
         
         if (null != _rgnSelLast) {
            _rgnSelLast.Exclude( rgn );
         }
      }
      
      private void RefreshCell( Point ptCell ) 
      {
         if (ptCell.Equals( PT_NO_VALUE )) { return; }

         UpdateRegion( new Region( MapBmp.DrawCell( ptCell, MapTxt ) ));
      
         //if (_bSelection) {
         //   _rSelLast = new Rectangle();
         //   DrawSelection(MapBmp.RectFromSel(_SelCurr)); 
         //} 
      }
      
      private void RefreshCells( Point[] Cells, Point[] CellsExclude ) {
         var rgn = new Region();
         foreach (var pt in Cells       ) { rgn.Union  (MapBmp.RectFromCell(pt)); }                  
         foreach (var pt in CellsExclude) { rgn.Exclude(MapBmp.RectFromCell(pt)); }         
         UpdateRegion( rgn );
      }

      private void RefreshMap() 
      {
         _ptCelLast = PT_NO_VALUE;

         lock (_mapLock) {
            MapBmp.DrawMap(MapTxt);
            UpdateRegion( new Region( new Rectangle( new Point(0,0), MapBmp.Size )));
         }
   
         UpdateSelection();      // PaintSelection();
      }

      private bool PointerUpdate( MouseEventArgs e )
      {
         _ptCelCurr = MapBmp.CellFromPoint( e.Location );

         // If no change, there's nothing to do.
         if (_ptCelCurr == _ptCelLast) {
            return false;
         }

         // Update the current cell, and if necessary, the selection rectangle.
         Point ptCelStart = _ptCelCurr;

         if (_bMouseDown) {
         
            ptCelStart = _ptCelDown;
         
            if (!_bSelection) { 
               _bSelection = (ptCelStart != _ptCelCurr); 
            }
            
            if (_bSelection) { 
               _SelCurr = new Selection(ptCelStart, _ptCelCurr);
               UpdateSelection();            
            }
         }
         else { 
            if (!_bSelection) {
               RefreshCell(_ptCelLast);
               RefreshCell(_ptCelCurr);
               PaintCell(_ptCelCurr, CR_SELECTION);                           
            }
         }

         _ptCelLast = _ptCelCurr;
         return true;
      }
      
      // Paints the new selection, and also clears the old selection.
      void UpdateSelection() {
         
         if (!_bSelection) { return; }
      
         // Get the region corresponding to the current selection.
         Region rgnNew = GetSelectRegion();
         
         // If there is an existing selection, erase the portion of it not in the
         // new selection region.
         if (null != _rgnSelLast) {
            Region rgnOld = new Region( _rgnSelLast.GetRegionData() );
            rgnOld.Exclude( rgnNew );
            UpdateRegion( rgnOld );
         }   
         
         PaintSelection( rgnNew );
      }
      
      Region GetSelectRegion() 
      {
         if (!_bSelection) return null;
      
         if (_MapDrawMode == MapDrawMode_e.Select) {
            return new Region(MapBmp.RectFromSel(_SelCurr));
         } 
         else {
            Region rgn = new Region();
            rgn.MakeEmpty();
            foreach (var pt in GetDrawModeCells(_SelCurr)) {
               rgn.Union(MapBmp.RectFromCell(pt));
            }
            return rgn;
         }
      }

      void PaintSelection() {
         PaintSelection(GetSelectRegion());
      }
      
      // Paints the selection.
      void PaintSelection( Region rgn ) {
         if (null == rgn) return;
         
         Region rgnPaint = new Region( rgn.GetRegionData() );
         if (null != _rgnSelLast) {
            rgnPaint.Exclude(_rgnSelLast);
         }   
         
         lock(_mapLock) {
            using (var g = picMap.CreateGraphics()) {
               Brush br = new SolidBrush(CR_SELECTION);
               g.FillRegion(br, rgnPaint);               
            }
         }

         _rgnSelLast = new Region(rgn.GetRegionData()); 
      }

      private Point[] GetDrawModeCells( Selection sel ) {
         Point[] cells;
         switch (_MapDrawMode) {
            case MapDrawMode_e.Line   : cells = PadBmp.DrawLine   (sel); break;
            case MapDrawMode_e.Rect   : cells = PadBmp.DrawRect   (sel); break;
            case MapDrawMode_e.Ellipse: cells = PadBmp.DrawEllipse(sel); break;            
            case MapDrawMode_e.Filipse: cells = PadBmp.FillEllipse(sel); break;
            default: return null;
         }
         return cells;
      }

      private void PaintCell(Point cell, Color cr) {
         PaintSelection( new Region( MapBmp.RectFromCell(cell)));
         //using (Graphics g = picMap.CreateGraphics()) {
         //   g.FillRectangle(new SolidBrush(cr), MapBmp.RectFromCell(cell));
         //}   
      }

      // Copies an external bitmap directly into main bitmap control's background image property.
      private void PaintBackground() {
         //Bitmap img = new Bitmap(map.Size.Width, map.Size.Height);
         //Graphics g = Graphics.FromImage(img);
         //g.DrawImage(MapBmp.Background, MapBmp.GetMapRect());
         //map.BackgroundImage = img;         
         picMap.BackgroundImage = MapBmp.Background;
      }


      // Fills the current selection with the given character.
      void FillSelection(char ch) {
         try {
            switch (_MapDrawMode) {
               case MapDrawMode_e.Select: MapTxt.FillRect(ch, _SelCurr); return;

               case MapDrawMode_e.Line:
               case MapDrawMode_e.Rect:
               case MapDrawMode_e.Ellipse:
               case MapDrawMode_e.Filipse:                
                  MapTxt.SetCells(ch, GetDrawModeCells( _SelCurr )); break;
            }
         }
         catch (Exception xx) {
            System.Diagnostics.Debug.WriteLine("***ERROR: FillSelection: " + xx.Message);
         }
      }
      
      // ----------------------------------------------------------------------
      // 
      // .textmap file format  (File Open/Save)
      //
      // ----------------------------------------------------------------------   
      private void SaveMapFile(string sPath)
      {
         Application.UseWaitCursor = true;      
      
         using (StreamWriter sw = File.CreateText(sPath))
         {
            sw.WriteLine(".textmap");
            sw.WriteLine("Version = 1");
            sw.WriteLine("GridWidth = {0}", MapBmp.GridSize.Width);
            sw.WriteLine("GridHeight = {0}", MapBmp.GridSize.Height);
            if (null != _sBkgrFileName)
            {
               sw.WriteLine("BackgroundImagePath = {0}", _sBkgrFileName);
            }
            sw.WriteLine("CharData:");
            sw.WriteLine(MapTxt.Serialize());

            sw.Close();
         }

         _sSaveFileName = sPath;
         AddToRecentList(_sSaveFileName);
         UpdateCaption();
         sbarMsg.Text = "Saved " + _sSaveFileName;

         Application.UseWaitCursor = false;               
      }

      private bool OpenMapFile(string sPath) 
      {
         ClearSelectionState(); // EndSelectState();

         int fileVer = 0;
         int gridWid = -1;
         int gridHgt = -1;
         bool bCharData = false;
         string sCharData = null;
         string sBkImagePath = null;
                  
         bool bFailed = false;  // not yet, anyway
         try {
            Application.UseWaitCursor = true;         

            string s;
            using (StreamReader sr = new StreamReader( sPath, Encoding.ASCII ))
            {
               s = sr.ReadLine();
               if (0 != s.CompareTo(".textmap")) {
                  throw new ApplicationException( "The file is not valid TextMap file." );
               }
       
               while (!sr.EndOfStream) {
               
                  if (bCharData) { 
                     sCharData = sr.ReadToEnd();
                  }
                   
                  else { 
                     s = sr.ReadLine();
                     char[] delim = new char[] { ' ', '=', '\r', '\n' };
                     string[] fld = s.Split(delim, StringSplitOptions.RemoveEmptyEntries);

                     if (0 == fld[0].CompareTo("Version")) { int.TryParse(fld[1], out fileVer); } else
                     if (0 == fld[0].CompareTo("GridWidth")) { int.TryParse( fld[1], out gridWid ); }else
                     if (0 == fld[0].CompareTo("GridHeight")) { int.TryParse( fld[1], out gridHgt  ); } else               
                     if (0 == fld[0].CompareTo("BackgroundImagePath")) { sBkImagePath = fld[1] ; } else
                     if (0 == fld[0].CompareTo("CharData:")) { bCharData = true; } else
                     { /* end of if-else statement */ }
                  }   
               }
               
               sr.Close();
            }
         }   
         catch (Exception e) {
               MessageBox.Show( e.ToString() );
               sbarMsg.Text = e.ToString() ;
               bFailed = true;
         }
         
         Application.UseWaitCursor = false;  
         
         if (bFailed) return false;
                  
         // -------         
         // Success:
         // -------
         
         // Adjust grid size.
         if (gridWid == -1) gridWid = MapBmp.GridSize.Width;            
         if (gridHgt == -1) gridHgt = MapBmp.GridSize.Height;
         if ((gridHgt != MapBmp.GridSize.Height) || (gridWid != MapBmp.GridSize.Width)) { 
            MapBmp.GridSize = new Size( gridWid, gridHgt );
//            MapBmp.New();
         }   
         
         // New TextMap
         if (null != sCharData) {
            MapTxt.Deserialize( sCharData );
         }
         
         // Load background image
         if (null != sBkImagePath) {
            _sBkgrFileName = sBkImagePath;
            LoadBkgrImageFile( _sBkgrFileName );
         }
         
         RefreshMap();
            
         _sSaveFileName = sPath;
         AddToRecentList( _sSaveFileName );
         UpdateCaption();
         sbarMsg.Text = "Opened " + _sSaveFileName;     
         
         return true;    
      }

      //  MM      MM                                 
      //  MM      MM                                 
      //  MMM    MMM                                 
      //  MMM    MMM   eeee   nnnnn   uu  uu   ssss  
      //  MMMM  MMMM  ee  ee  nn  nn  uu  uu  ss  ss 
      //  MMMM  MMMM  eeeeee  nn  nn  uu  uu  ss     
      //  MM MMMM MM  ee      nn  nn  uu  uu   ssss  
      //  MM MMMM MM  ee      nn  nn  uu  uu      ss 
      //  MM  MM  MM  ee  ee  nn  nn  uu  uu  ss  ss 
      //  MM  MM  MM   eeee   nn  nn   uuuuu   ssss  
      
      // 
      // ---   Menu Helpers   -------------------------------------------------
      
      private void ShowRecentList() {
         
         // First, clear the current list.
         ToolStripItemCollection items = recentMenuItem.DropDownItems;
         items.Clear();

         string sRecent = Properties.Settings.Default.RecentFiles;
         string[] list = sRecent.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

         if (0 < list.Length) {
            recentMenuItem.Enabled = true;
            int i; for (i = 0; i < list.Length; i++) {
               items.Add(String.Format("{0}", list[i]), null, recentMenuItem_Click);
            }             
         }
         else {
            recentMenuItem.Enabled = false;
         }            
      }
      
      private void AddToRecentList( string sFile ) {
         string sNewList = sFile ;
         string sOldList = Properties.Settings.Default.RecentFiles;
         string[] list = sOldList.Split( new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries );
         int nItems = 1;  // The new file counts as 1.
         int i; for (i=0; (i<list.Length) && (nItems < 4); i++) {
            // If the new file is already in the list, skip it.
            if (0 == sFile.CompareTo( list[i] )) continue;   
            sNewList += String.Format( "|{0}", list[i] );   
         }
         Properties.Settings.Default.RecentFiles = sNewList ;
      }

      // ----------------------------------------------------------------------
      // 
      // File Menu
      //
      // ----------------------------------------------------------------------   

      private void newFileMenuItem_Click(object sender, EventArgs e)
      {
         _sSaveFileName = null;
         ClearSelectionState();
         MapTxt.Clear(); // New();
         RefreshMap();
      }

      private void openFileMenuItem_Click(object sender, EventArgs e)
      {
         if (DialogResult.OK == dlgOpen.ShowDialog()) {
            OpenMapFile(dlgOpen.FileName);         
         } 
      }
     
      private void saveFileMenuItem_Click(object sender, EventArgs e)
      {
         if (null == _sSaveFileName)
         {
            saveAsMenuItem_Click(sender, e);
         }
         else
         {
            Application.UseWaitCursor = true;
            SaveMapFile(_sSaveFileName);
            sbarMsg.Text = "Saved " + _sSaveFileName;
            Application.UseWaitCursor = false;
         }
      }

      private void saveAsMenuItem_Click(object sender, EventArgs e)
      {
         if (null != _sSaveFileName) { dlgSave.FileName = _sSaveFileName; }
         
         DialogResult res = dlgSave.ShowDialog();

         if (DialogResult.OK == res)
         {
            Application.UseWaitCursor = true;          
            
            _sSaveFileName = dlgSave.FileName;
            SaveMapFile(_sSaveFileName);
            UpdateCaption();
            sbarMsg.Text = "Saved " + _sSaveFileName;

            Application.UseWaitCursor = false;
         }
      }

      private void recentMenuItem_Click(object sender, EventArgs e)
      {
         // Get the text of the selected menu item. 
         string sPath = sender.ToString();
         
         // If it's not the "Recent Files" root item, it should be a file path.
         if (recentMenuItem.Text != sPath) {
            OpenMapFile( sPath );
         }   
      }
      
      private void exitMenuItem_Click(object sender, EventArgs e)
      {
         Close();
      }

      // This is our chance to modify the File menu just before the user sees it.
      private void fileMenuItem_DropDownOpening(object sender, EventArgs e)
      {
         ShowRecentList();              
      }
      
      // ----------------------------------------------------------------------
      // 
      // Edit Menu
      //
      // ----------------------------------------------------------------------   
      private void undoMenuItem_Click(object sender, EventArgs e) {
         if (MapTxt.Undo()) { RefreshMap(); } 
      }

      private void redoMenuItem_Click(object sender, EventArgs e) {
         if (MapTxt.Redo()) { RefreshMap(); }       
      }
          
      private void copyToClipboardMenuItem_Click(object sender, EventArgs e)
      {
         Clipboard.SetText(MapTxt.Serialize());
      }

      private void cutMenuItem_click(object sender, EventArgs e) {
         copyMenuItem_Click( sender, e );
          delMenuItem_Click( sender, e );
      }
      
      private void copyMenuItem_Click(object sender, EventArgs e) {
         if (_bSelection) { Clipboard.SetText(MapTxt.Serialize(_SelCurr)); }
         else             { Clipboard.SetText(MapTxt.Serialize(_ptCelCurr)); }   
      }
      
      private void pasteMenuItem_Click(object sender, EventArgs e) {
         if (_bSelection) { MapTxt.Deserialize(Clipboard.GetText(), _SelCurr.pt1, _SelCurr.pt2); } 
         else             { MapTxt.Deserialize(Clipboard.GetText(), _ptCelCurr                ); }
         RefreshMap();
      }      
      
      private void delMenuItem_Click(object sender, EventArgs e) {
         OnKeyPress( new KeyPressEventArgs( ' ' ));
         //if (_bSelection) { MapTxt.Clear(_SelCurr  ); }
         //else             { MapTxt.Clear(_ptCelCurr); }
         //RefreshMap();
      }

      private void flipHorizMenuItem_Click(object sender, EventArgs e) {
         if (_bSelection) { MapTxt.Flip(false, _SelCurr); RefreshMap(); }
      }

      private void flipVertMenuItem_Click(object sender, EventArgs e) {
         if (_bSelection) { MapTxt.Flip(true, _SelCurr); RefreshMap(); }
      }

      private void rotateMenuItem_Click(object sender, EventArgs e) {
         if (_bSelection) { MapTxt.Rotate(_SelCurr); RefreshMap(); }
      }
      
      private void selectAllMenuItem_Click(object sender, EventArgs e) {
         ClearSelectionState();
         SetDrawMode(MapDrawMode_e.Select);
         _bSelection = true;
         _SelCurr = new Selection(new Point(0, 0), new Point(MapBmp.GridSize.Width - 1, MapBmp.GridSize.Height - 1));
         PaintSelection();
      }
      
      private void propsMenuItem_Click(object sender, EventArgs e)
      {
         Props dlg = new Props();
         dlg.Owner = this;
         dlg.sizeGrid = MapBmp.GridSize;
         dlg.ShowDialog();

         // Process data entered by user if dialog box is accepted
         if (dlg.DialogResult == DialogResult.OK)
         {
            // Did grid dimensions change?
            if (!MapBmp.GridSize.Equals( dlg.sizeGrid )) {
            
               // Update the MapText object:
               // -- Export the map text to a string
               // -- recreate it with the new size
               // -- import the map text 
               string s = MapTxt.Serialize();
               MapTxt.New(dlg.sizeGrid);
               MapTxt.Deserialize(s);

               // Resize the drawing scratchpad
               PadBmp.New(dlg.sizeGrid);
               
               // Set the grid size in the MapBitmap object.
               MapBmp.GridSize = dlg.sizeGrid;
               
               // Redraw the display.
               RefreshMap();
            }
         }         
      }        


      // ----------------------------------------------------------------------
      // 
      // Image Menu
      //
      // ----------------------------------------------------------------------   
      private void clearImageMenuItem_Click(object sender, EventArgs e)
      {
         _sBkgrFileName = null;
         MapBmp.Background = null;
         //picMap.BackgroundImage = null;
         RefreshMap();
         sbarMsg.Text = "Cleared background image." ;
      }

      private void LoadBkgrImageFile( string sFile ) {
         if (null == sFile) {
            MapBmp.Background = null;
            return;
         }
         MapBmp.Background = new Bitmap(sFile);
         PaintBackground();      
      }

      private void loadImageMenuItem_Click(object sender, EventArgs e)
      {
         DialogResult res = dlgImgOpen.ShowDialog();
         string s = "Loaded Image: " + res;

         if (DialogResult.OK == res)
         {
            _sBkgrFileName = dlgImgOpen.FileName;
            LoadBkgrImageFile( _sBkgrFileName );

            RefreshMap();

            s += ": " + dlgImgOpen.FileName;
         }
         sbarMsg.Text = s;
      }

      // ----------------------------------------------------------------------
      // 
      // Help Menu
      //
      // ----------------------------------------------------------------------   
      private void aboutMenuItem_Click(object sender, EventArgs e)
      {
         AboutBox dlg = new AboutBox();
         dlg.Show();
      }

      private void editMenuItem_DropDownOpening(object sender, EventArgs e)
      {
         undoMenuItem.Enabled = MapTxt.CanUndo();
         redoMenuItem.Enabled = MapTxt.CanRedo(); 
      }


      // ----------------------------------------------------------------------
      // 
      // Toolbar
      //
      // ----------------------------------------------------------------------  

      enum MapDrawMode_e { Select, Line, Rect, Ellipse, Filipse };
      MapDrawMode_e _MapDrawMode = MapDrawMode_e.Select;
 
      //Dictionary<MapDrawMode_e, ToolStripButton> DrawModes = new Dictionary<MapDrawMode_e, ToolStripButton>();
      Dictionary<MapDrawMode_e, ModesInfo> DrawModes = new Dictionary<MapDrawMode_e, ModesInfo>();      

      struct ModesInfo { 
         public ToolStripButton tsb; 
         public Cursor cur; 
         
         public ModesInfo( ToolStripButton tsb, Cursor cur ) {         
            this.tsb = tsb ;         
            this.cur = cur ;
         }   
         public ModesInfo( ToolStripButton tsb, byte[] CursorBytes ) 
            : this( tsb, new Cursor( new MemoryStream( CursorBytes ))) 
         {}
      }
      
      void InitDrawModesTable() {
         DrawModes.Add(MapDrawMode_e.Select , new ModesInfo( tbarSelMode    , Cursors.Default ));
         DrawModes.Add(MapDrawMode_e.Line   , new ModesInfo( tbarLineMode   , TextMap.Properties.Resources.line_pro    ));      
         DrawModes.Add(MapDrawMode_e.Rect   , new ModesInfo( tbarRectMode   , TextMap.Properties.Resources.rect_pro    ));      
         DrawModes.Add(MapDrawMode_e.Ellipse, new ModesInfo( tbarEllipseMode, TextMap.Properties.Resources.ellipse_pro ));
         DrawModes.Add(MapDrawMode_e.Filipse, new ModesInfo( tbarFilipseMode, TextMap.Properties.Resources.filipse_pro ));                         
      }
            
      void SetDrawMode( MapDrawMode_e mode ) {

         // Set the new mode.
         _MapDrawMode = mode;
         UpdateSelection();
      
         // Set the mode and check the corresponding button
         DrawModes[ mode ].tsb.Checked = true;
         sbarMsg.Text = DrawModes[ mode ].tsb.ToolTipText;         
         //sbarMsg.Text = Enum.GetName(typeof(MapDrawMode_e), _MapDrawMode);
      
         // Uncheck all the other buttons.
         var tsbUnchecks = from d in DrawModes where d.Key != mode select d.Value.tsb ;
         foreach (var tsb in tsbUnchecks) { tsb.Checked = false; };
         
         // Set the PictureBox cursor to reflect the mode
         picMap.Cursor = DrawModes[ mode ].cur;

         // Enable or disable all the Selection Mode edit menu items per the current drawing mode.
         bool b = (MapDrawMode_e.Select == mode) ;
         
               cutMenuItem.Enabled = b;         
              copyMenuItem.Enabled = b;         
             pasteMenuItem.Enabled = b;         
         flipHorizMenuItem.Enabled = b;         
          flipVertMenuItem.Enabled = b;         
            rotateMenuItem.Enabled = b;
      }

      private void tbarModeButton_Click(object sender, EventArgs e)
      {
         var tsb = (ToolStripButton)sender;
         var mode = (from d in DrawModes where d.Value.tsb == tsb select d.Key).ElementAt(0);         
        
         if (_MapDrawMode != mode ) {
            SetDrawMode( mode );
         }
      }

      private void tbarColors_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e) {
         var tsddb = (ToolStripDropDownButton)sender ;
         tsddb.BackColor = e.ClickedItem.BackColor ;
         MapTxt.TextColor = (MapText.RL_Color)e.ClickedItem.Tag ;
      }
      
      ToolStripMenuItem MakeColorItem( MapText.RL_Color id ) {
         var tsmi = new ToolStripMenuItem();
         tsmi.BackColor = MapText.PaletteByID[id].cr;
         tsmi.Tag = id;
         tsmi.AutoSize = tbarColorMenuBase.AutoSize;
         tsmi.Padding = tbarColorMenuBase.Padding;         
         tsmi.Text = tbarColorMenuBase.Text;
         tsmi.Size = tbarColorMenuBase.Size; 
         return tsmi;
      }
      
      // This array defines the ordering of the colors in the toolbar dropdown.
      MapText.RL_Color[] TBarPal = {
         MapText.RL_Color.White    , 
         MapText.RL_Color.LtBlue   , 
         MapText.RL_Color.LtCyan   ,        
         MapText.RL_Color.LtGreen  , 
         MapText.RL_Color.Yellow   ,
         MapText.RL_Color.LtRed    , 
         MapText.RL_Color.LtMagenta, 
         MapText.RL_Color.LtGray   , 
         MapText.RL_Color.Blue     ,  
         MapText.RL_Color.Cyan     ,  
         MapText.RL_Color.Green    , 
         MapText.RL_Color.Brown    ,
         MapText.RL_Color.Red      , 
         MapText.RL_Color.Magenta  , 
         MapText.RL_Color.DkGray   , 
      };  
      
      private void InitColorToolbar() {
         var items = from p in TBarPal select MakeColorItem( p );
         tbarColors.DropDownItems.AddRange( items.ToArray() );
         tbarColors.DropDownItems.Remove( tbarColorMenuBase ); 
         tbarColors.BackColor = MapText.PaletteByID[ MapText.RL_Color.White ].cr;
      }
   }
}
