namespace TextMap
{
   partial class Form1
   {
      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose(bool disposing)
      {
         if (disposing && (components != null))
         {
            components.Dispose();
         }
         base.Dispose(disposing);
      }

      #region Windows Form Designer generated code

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         this.components = new System.ComponentModel.Container();
         this.sbar = new System.Windows.Forms.StatusStrip();
         this.sbarMsg = new System.Windows.Forms.ToolStripStatusLabel();
         this.sbarKey = new System.Windows.Forms.ToolStripStatusLabel();
         this.sbarCell = new System.Windows.Forms.ToolStripStatusLabel();
         this.sbarSel = new System.Windows.Forms.ToolStripStatusLabel();
         this.mnu = new System.Windows.Forms.MenuStrip();
         this.fileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.openFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
         this.recentMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.emptyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
         this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.editMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.undoMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.redoMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
         this.cutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.copyMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.pasteMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
         this.selectAllMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
         this.flipHorizMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.flipVertMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.rotateMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.imageMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.mapSizeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.mapBkgdMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.loadImageFromFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.aboutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.recentMenuSep = new System.Windows.Forms.ToolStripSeparator();
         this.dlgSave = new System.Windows.Forms.SaveFileDialog();
         this.dlgOpen = new System.Windows.Forms.OpenFileDialog();
         this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
         this.dlgImgOpen = new System.Windows.Forms.OpenFileDialog();
         this.tbar = new System.Windows.Forms.ToolStrip();
         this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
         this.picMap = new System.Windows.Forms.PictureBox();
         this.tbarSelMode = new System.Windows.Forms.ToolStripButton();
         this.tbarLineMode = new System.Windows.Forms.ToolStripButton();
         this.tbarRectMode = new System.Windows.Forms.ToolStripButton();
         this.tbarEllipseMode = new System.Windows.Forms.ToolStripButton();
         this.tbarFilipseMode = new System.Windows.Forms.ToolStripButton();
         this.tbarColors = new System.Windows.Forms.ToolStripDropDownButton();
         this.tbarColorMenuBase = new System.Windows.Forms.ToolStripMenuItem();
         this.sbar.SuspendLayout();
         this.mnu.SuspendLayout();
         this.tbar.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.picMap)).BeginInit();
         this.SuspendLayout();
         // 
         // sbar
         // 
         this.sbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sbarMsg,
            this.sbarKey,
            this.sbarCell,
            this.sbarSel});
         this.sbar.Location = new System.Drawing.Point(0, 348);
         this.sbar.Name = "sbar";
         this.sbar.Size = new System.Drawing.Size(732, 24);
         this.sbar.TabIndex = 0;
         this.sbar.Text = "All your base are belong to us.";
         // 
         // sbarMsg
         // 
         this.sbarMsg.Name = "sbarMsg";
         this.sbarMsg.Size = new System.Drawing.Size(621, 19);
         this.sbarMsg.Spring = true;
         this.sbarMsg.Text = ":::";
         this.sbarMsg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
         // 
         // sbarKey
         // 
         this.sbarKey.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
         this.sbarKey.Name = "sbarKey";
         this.sbarKey.Size = new System.Drawing.Size(33, 19);
         this.sbarKey.Text = "Key:";
         this.sbarKey.Click += new System.EventHandler(this.sbarKey_Click);
         // 
         // sbarCell
         // 
         this.sbarCell.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
         this.sbarCell.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
         this.sbarCell.Name = "sbarCell";
         this.sbarCell.RightToLeft = System.Windows.Forms.RightToLeft.No;
         this.sbarCell.Size = new System.Drawing.Size(34, 19);
         this.sbarCell.Text = "Cell:";
         // 
         // sbarSel
         // 
         this.sbarSel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
         this.sbarSel.Name = "sbarSel";
         this.sbarSel.Size = new System.Drawing.Size(29, 19);
         this.sbarSel.Text = "Sel:";
         this.sbarSel.Click += new System.EventHandler(this.sbarSel_Click);
         // 
         // mnu
         // 
         this.mnu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenuItem,
            this.editMenuItem,
            this.imageMenuItem,
            this.helpToolStripMenuItem});
         this.mnu.Location = new System.Drawing.Point(0, 0);
         this.mnu.Name = "mnu";
         this.mnu.Size = new System.Drawing.Size(732, 24);
         this.mnu.TabIndex = 1;
         // 
         // fileMenuItem
         // 
         this.fileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openFileMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.toolStripSeparator5,
            this.recentMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
         this.fileMenuItem.Name = "fileMenuItem";
         this.fileMenuItem.Size = new System.Drawing.Size(37, 20);
         this.fileMenuItem.Text = "File";
         this.fileMenuItem.DropDownOpening += new System.EventHandler(this.fileMenuItem_DropDownOpening);
         // 
         // newToolStripMenuItem
         // 
         this.newToolStripMenuItem.Name = "newToolStripMenuItem";
         this.newToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
         this.newToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
         this.newToolStripMenuItem.Text = "&New";
         this.newToolStripMenuItem.Click += new System.EventHandler(this.newFileMenuItem_Click);
         // 
         // openFileMenuItem
         // 
         this.openFileMenuItem.Name = "openFileMenuItem";
         this.openFileMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
         this.openFileMenuItem.Size = new System.Drawing.Size(155, 22);
         this.openFileMenuItem.Text = "&Open...";
         this.openFileMenuItem.Click += new System.EventHandler(this.openFileMenuItem_Click);
         // 
         // saveToolStripMenuItem
         // 
         this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
         this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
         this.saveToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
         this.saveToolStripMenuItem.Text = "&Save";
         this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveFileMenuItem_Click);
         // 
         // saveAsToolStripMenuItem
         // 
         this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
         this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
         this.saveAsToolStripMenuItem.Text = "Save &As...";
         this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsMenuItem_Click);
         // 
         // toolStripSeparator5
         // 
         this.toolStripSeparator5.Name = "toolStripSeparator5";
         this.toolStripSeparator5.Size = new System.Drawing.Size(152, 6);
         // 
         // recentMenuItem
         // 
         this.recentMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.emptyToolStripMenuItem});
         this.recentMenuItem.Name = "recentMenuItem";
         this.recentMenuItem.Size = new System.Drawing.Size(155, 22);
         this.recentMenuItem.Text = "Recent Files";
         this.recentMenuItem.Click += new System.EventHandler(this.recentMenuItem_Click);
         // 
         // emptyToolStripMenuItem
         // 
         this.emptyToolStripMenuItem.Name = "emptyToolStripMenuItem";
         this.emptyToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
         this.emptyToolStripMenuItem.Text = "<None>";
         // 
         // toolStripSeparator1
         // 
         this.toolStripSeparator1.Name = "toolStripSeparator1";
         this.toolStripSeparator1.Size = new System.Drawing.Size(152, 6);
         // 
         // exitToolStripMenuItem
         // 
         this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
         this.exitToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
         this.exitToolStripMenuItem.Text = "E&xit";
         this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitMenuItem_Click);
         // 
         // editMenuItem
         // 
         this.editMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoMenuItem,
            this.redoMenuItem,
            this.toolStripSeparator2,
            this.cutMenuItem,
            this.copyMenuItem,
            this.pasteMenuItem,
            this.deleteToolStripMenuItem,
            this.toolStripSeparator4,
            this.selectAllMenuItem,
            this.toolStripSeparator3,
            this.flipHorizMenuItem,
            this.flipVertMenuItem,
            this.rotateMenuItem});
         this.editMenuItem.Name = "editMenuItem";
         this.editMenuItem.Size = new System.Drawing.Size(39, 20);
         this.editMenuItem.Text = "Edit";
         this.editMenuItem.DropDownOpening += new System.EventHandler(this.editMenuItem_DropDownOpening);
         // 
         // undoMenuItem
         // 
         this.undoMenuItem.Name = "undoMenuItem";
         this.undoMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
         this.undoMenuItem.Size = new System.Drawing.Size(194, 22);
         this.undoMenuItem.Text = "Undo";
         this.undoMenuItem.Click += new System.EventHandler(this.undoMenuItem_Click);
         // 
         // redoMenuItem
         // 
         this.redoMenuItem.Name = "redoMenuItem";
         this.redoMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
         this.redoMenuItem.Size = new System.Drawing.Size(194, 22);
         this.redoMenuItem.Text = "Redo";
         this.redoMenuItem.Click += new System.EventHandler(this.redoMenuItem_Click);
         // 
         // toolStripSeparator2
         // 
         this.toolStripSeparator2.Name = "toolStripSeparator2";
         this.toolStripSeparator2.Size = new System.Drawing.Size(191, 6);
         // 
         // cutMenuItem
         // 
         this.cutMenuItem.Name = "cutMenuItem";
         this.cutMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
         this.cutMenuItem.Size = new System.Drawing.Size(194, 22);
         this.cutMenuItem.Text = "Cut";
         this.cutMenuItem.Click += new System.EventHandler(this.cutMenuItem_click);
         // 
         // copyMenuItem
         // 
         this.copyMenuItem.Name = "copyMenuItem";
         this.copyMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
         this.copyMenuItem.Size = new System.Drawing.Size(194, 22);
         this.copyMenuItem.Text = "Copy";
         this.copyMenuItem.Click += new System.EventHandler(this.copyMenuItem_Click);
         // 
         // pasteMenuItem
         // 
         this.pasteMenuItem.Name = "pasteMenuItem";
         this.pasteMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
         this.pasteMenuItem.Size = new System.Drawing.Size(194, 22);
         this.pasteMenuItem.Text = "Paste";
         this.pasteMenuItem.Click += new System.EventHandler(this.pasteMenuItem_Click);
         // 
         // deleteToolStripMenuItem
         // 
         this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
         this.deleteToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
         this.deleteToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
         this.deleteToolStripMenuItem.Text = "Delete";
         this.deleteToolStripMenuItem.Click += new System.EventHandler(this.delMenuItem_Click);
         // 
         // toolStripSeparator4
         // 
         this.toolStripSeparator4.Name = "toolStripSeparator4";
         this.toolStripSeparator4.Size = new System.Drawing.Size(191, 6);
         // 
         // selectAllMenuItem
         // 
         this.selectAllMenuItem.Name = "selectAllMenuItem";
         this.selectAllMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
         this.selectAllMenuItem.Size = new System.Drawing.Size(194, 22);
         this.selectAllMenuItem.Text = "Select All";
         this.selectAllMenuItem.Click += new System.EventHandler(this.selectAllMenuItem_Click);
         // 
         // toolStripSeparator3
         // 
         this.toolStripSeparator3.Name = "toolStripSeparator3";
         this.toolStripSeparator3.Size = new System.Drawing.Size(191, 6);
         // 
         // flipHorizMenuItem
         // 
         this.flipHorizMenuItem.Name = "flipHorizMenuItem";
         this.flipHorizMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H)));
         this.flipHorizMenuItem.Size = new System.Drawing.Size(194, 22);
         this.flipHorizMenuItem.Text = "Flip Horizontal";
         this.flipHorizMenuItem.Click += new System.EventHandler(this.flipHorizMenuItem_Click);
         // 
         // flipVertMenuItem
         // 
         this.flipVertMenuItem.Name = "flipVertMenuItem";
         this.flipVertMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
         this.flipVertMenuItem.Size = new System.Drawing.Size(194, 22);
         this.flipVertMenuItem.Text = "Flip Vertical";
         this.flipVertMenuItem.Click += new System.EventHandler(this.flipVertMenuItem_Click);
         // 
         // rotateMenuItem
         // 
         this.rotateMenuItem.Name = "rotateMenuItem";
         this.rotateMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
         this.rotateMenuItem.Size = new System.Drawing.Size(194, 22);
         this.rotateMenuItem.Text = "Rotate";
         this.rotateMenuItem.Click += new System.EventHandler(this.rotateMenuItem_Click);
         // 
         // imageMenuItem
         // 
         this.imageMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mapSizeMenuItem,
            this.mapBkgdMenuItem});
         this.imageMenuItem.Name = "imageMenuItem";
         this.imageMenuItem.Size = new System.Drawing.Size(43, 20);
         this.imageMenuItem.Text = "Map";
         // 
         // mapSizeMenuItem
         // 
         this.mapSizeMenuItem.Name = "mapSizeMenuItem";
         this.mapSizeMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
         this.mapSizeMenuItem.Size = new System.Drawing.Size(180, 22);
         this.mapSizeMenuItem.Text = "Properties ...";
         this.mapSizeMenuItem.Click += new System.EventHandler(this.propsMenuItem_Click);
         // 
         // mapBkgdMenuItem
         // 
         this.mapBkgdMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadImageFromFileToolStripMenuItem,
            this.clearToolStripMenuItem});
         this.mapBkgdMenuItem.Enabled = false;
         this.mapBkgdMenuItem.Name = "mapBkgdMenuItem";
         this.mapBkgdMenuItem.Size = new System.Drawing.Size(180, 22);
         this.mapBkgdMenuItem.Text = "Background";
         // 
         // loadImageFromFileToolStripMenuItem
         // 
         this.loadImageFromFileToolStripMenuItem.Name = "loadImageFromFileToolStripMenuItem";
         this.loadImageFromFileToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
         this.loadImageFromFileToolStripMenuItem.Text = "Load Image...";
         this.loadImageFromFileToolStripMenuItem.Click += new System.EventHandler(this.loadImageMenuItem_Click);
         // 
         // clearToolStripMenuItem
         // 
         this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
         this.clearToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
         this.clearToolStripMenuItem.Text = "Clear";
         this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearImageMenuItem_Click);
         // 
         // helpToolStripMenuItem
         // 
         this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutMenuItem});
         this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
         this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
         this.helpToolStripMenuItem.Text = "Help";
         // 
         // aboutMenuItem
         // 
         this.aboutMenuItem.Name = "aboutMenuItem";
         this.aboutMenuItem.Size = new System.Drawing.Size(107, 22);
         this.aboutMenuItem.Text = "About";
         this.aboutMenuItem.Click += new System.EventHandler(this.aboutMenuItem_Click);
         // 
         // recentMenuSep
         // 
         this.recentMenuSep.Name = "recentMenuSep";
         this.recentMenuSep.Size = new System.Drawing.Size(152, 6);
         // 
         // dlgSave
         // 
         this.dlgSave.DefaultExt = "textmap";
         this.dlgSave.Filter = "TextMap saves (*.textmap)|*.textmap|All files (*.*)|*.*";
         // 
         // dlgOpen
         // 
         this.dlgOpen.DefaultExt = "textmap";
         this.dlgOpen.FileName = "*.textmap";
         this.dlgOpen.Filter = "TextMap saves (*.textmap)|*.textmap|All files (*.*)|*.*";
         // 
         // notifyIcon1
         // 
         this.notifyIcon1.Text = "notifyIcon1";
         this.notifyIcon1.Visible = true;
         // 
         // dlgImgOpen
         // 
         this.dlgImgOpen.Filter = "Image Files(*.bmp;*.jpg;*.png)|*.BMP;*.JPG;*.PNG|All files (*.*)|*.*";
         // 
         // tbar
         // 
         this.tbar.AutoSize = false;
         this.tbar.Dock = System.Windows.Forms.DockStyle.Left;
         this.tbar.GripMargin = new System.Windows.Forms.Padding(0);
         this.tbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbarSelMode,
            this.tbarLineMode,
            this.tbarRectMode,
            this.tbarEllipseMode,
            this.tbarFilipseMode,
            this.toolStripSeparator6,
            this.tbarColors});
         this.tbar.Location = new System.Drawing.Point(0, 24);
         this.tbar.Name = "tbar";
         this.tbar.Size = new System.Drawing.Size(24, 324);
         this.tbar.TabIndex = 4;
         this.tbar.Text = "toolStrip1";
         // 
         // toolStripSeparator6
         // 
         this.toolStripSeparator6.Name = "toolStripSeparator6";
         this.toolStripSeparator6.Size = new System.Drawing.Size(22, 6);
         // 
         // picMap
         // 
         this.picMap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.picMap.Dock = System.Windows.Forms.DockStyle.Fill;
         this.picMap.Location = new System.Drawing.Point(24, 24);
         this.picMap.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
         this.picMap.Name = "picMap";
         this.picMap.Size = new System.Drawing.Size(708, 324);
         this.picMap.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
         this.picMap.TabIndex = 3;
         this.picMap.TabStop = false;
         this.picMap.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picMap_MouseMove);
         this.picMap.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picMap_MouseDown);
         this.picMap.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picMap_MouseUp);
         this.picMap.SizeChanged += new System.EventHandler(this.picMap_SizeChanged);
         // 
         // tbarSelMode
         // 
         this.tbarSelMode.Checked = true;
         this.tbarSelMode.CheckState = System.Windows.Forms.CheckState.Checked;
         this.tbarSelMode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
         this.tbarSelMode.Image = global::TextMap.Properties.Resources.SelToolBmp;
         this.tbarSelMode.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
         this.tbarSelMode.ImageTransparentColor = System.Drawing.Color.White;
         this.tbarSelMode.Margin = new System.Windows.Forms.Padding(2, 1, 0, 1);
         this.tbarSelMode.Name = "tbarSelMode";
         this.tbarSelMode.Size = new System.Drawing.Size(20, 18);
         this.tbarSelMode.ToolTipText = "Selection Mode";
         this.tbarSelMode.Click += new System.EventHandler(this.tbarModeButton_Click);
         // 
         // tbarLineMode
         // 
         this.tbarLineMode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
         this.tbarLineMode.Image = global::TextMap.Properties.Resources.LineToolBmp;
         this.tbarLineMode.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
         this.tbarLineMode.ImageTransparentColor = System.Drawing.Color.White;
         this.tbarLineMode.Margin = new System.Windows.Forms.Padding(2, 1, 0, 1);
         this.tbarLineMode.Name = "tbarLineMode";
         this.tbarLineMode.Size = new System.Drawing.Size(20, 17);
         this.tbarLineMode.ToolTipText = "Line Drawing Mode";
         this.tbarLineMode.Click += new System.EventHandler(this.tbarModeButton_Click);
         // 
         // tbarRectMode
         // 
         this.tbarRectMode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
         this.tbarRectMode.Image = global::TextMap.Properties.Resources.RectToolBmp;
         this.tbarRectMode.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
         this.tbarRectMode.ImageTransparentColor = System.Drawing.Color.White;
         this.tbarRectMode.Margin = new System.Windows.Forms.Padding(2, 1, 0, 1);
         this.tbarRectMode.Name = "tbarRectMode";
         this.tbarRectMode.Size = new System.Drawing.Size(20, 17);
         this.tbarRectMode.ToolTipText = "Rectangle Mode";
         this.tbarRectMode.Click += new System.EventHandler(this.tbarModeButton_Click);
         // 
         // tbarEllipseMode
         // 
         this.tbarEllipseMode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
         this.tbarEllipseMode.Image = global::TextMap.Properties.Resources.ElliToolBmp;
         this.tbarEllipseMode.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
         this.tbarEllipseMode.ImageTransparentColor = System.Drawing.Color.White;
         this.tbarEllipseMode.Margin = new System.Windows.Forms.Padding(2, 1, 0, 1);
         this.tbarEllipseMode.Name = "tbarEllipseMode";
         this.tbarEllipseMode.Size = new System.Drawing.Size(20, 17);
         this.tbarEllipseMode.ToolTipText = "Ellipse Mode";
         this.tbarEllipseMode.Click += new System.EventHandler(this.tbarModeButton_Click);
         // 
         // tbarFilipseMode
         // 
         this.tbarFilipseMode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
         this.tbarFilipseMode.Image = global::TextMap.Properties.Resources.FEliToolBmp;
         this.tbarFilipseMode.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
         this.tbarFilipseMode.ImageTransparentColor = System.Drawing.Color.White;
         this.tbarFilipseMode.Margin = new System.Windows.Forms.Padding(2, 1, 0, 1);
         this.tbarFilipseMode.Name = "tbarFilipseMode";
         this.tbarFilipseMode.Size = new System.Drawing.Size(20, 17);
         this.tbarFilipseMode.ToolTipText = "Filled Ellipse Mode";
         this.tbarFilipseMode.Click += new System.EventHandler(this.tbarModeButton_Click);
         // 
         // tbarColors
         // 
         this.tbarColors.BackColor = System.Drawing.Color.Black;
         this.tbarColors.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
         this.tbarColors.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbarColorMenuBase});
         this.tbarColors.Image = global::TextMap.Properties.Resources.ColorToolBmp;
         this.tbarColors.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
         this.tbarColors.ImageTransparentColor = System.Drawing.Color.White;
         this.tbarColors.Margin = new System.Windows.Forms.Padding(2, 1, 0, 0);
         this.tbarColors.Name = "tbarColors";
         this.tbarColors.Padding = new System.Windows.Forms.Padding(1, 0, 0, 0);
         this.tbarColors.ShowDropDownArrow = false;
         this.tbarColors.Size = new System.Drawing.Size(20, 19);
         this.tbarColors.ToolTipText = "Text Color";
         this.tbarColors.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.tbarColors_DropDownItemClicked);
         // 
         // tbarColorMenuBase
         // 
         this.tbarColorMenuBase.AutoSize = false;
         this.tbarColorMenuBase.BackColor = System.Drawing.Color.Black;
         this.tbarColorMenuBase.Name = "tbarColorMenuBase";
         this.tbarColorMenuBase.Padding = new System.Windows.Forms.Padding(0, 0, 0, 1);
         this.tbarColorMenuBase.Size = new System.Drawing.Size(80, 14);
         // 
         // Form1
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(732, 372);
         this.Controls.Add(this.picMap);
         this.Controls.Add(this.tbar);
         this.Controls.Add(this.sbar);
         this.Controls.Add(this.mnu);
         this.KeyPreview = true;
         this.MainMenuStrip = this.mnu;
         this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
         this.Name = "Form1";
         this.Text = "Form1";
         this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Form1_KeyPress);
         this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
         this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
         this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.Form1_PreviewKeyDown);
         this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
         this.sbar.ResumeLayout(false);
         this.sbar.PerformLayout();
         this.mnu.ResumeLayout(false);
         this.mnu.PerformLayout();
         this.tbar.ResumeLayout(false);
         this.tbar.PerformLayout();
         ((System.ComponentModel.ISupportInitialize)(this.picMap)).EndInit();
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.StatusStrip sbar;
      private System.Windows.Forms.MenuStrip mnu;
      private System.Windows.Forms.PictureBox picMap;
      private System.Windows.Forms.ToolStripMenuItem fileMenuItem;
      private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem openFileMenuItem;
      private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
      private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
      private System.Windows.Forms.ToolStripStatusLabel sbarMsg;
      private System.Windows.Forms.ToolStripStatusLabel sbarCell;
      private System.Windows.Forms.ToolStripStatusLabel sbarKey;
      private System.Windows.Forms.SaveFileDialog dlgSave;
      private System.Windows.Forms.ToolStripMenuItem imageMenuItem;
      private System.Windows.Forms.OpenFileDialog dlgOpen;
      private System.Windows.Forms.NotifyIcon notifyIcon1;
      private System.Windows.Forms.ToolStripMenuItem editMenuItem;
      private System.Windows.Forms.ToolStripMenuItem cutMenuItem;
      private System.Windows.Forms.ToolStripMenuItem copyMenuItem;
      private System.Windows.Forms.ToolStripMenuItem pasteMenuItem;
      private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
      private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
      private System.Windows.Forms.ToolStripMenuItem flipHorizMenuItem;
      private System.Windows.Forms.ToolStripMenuItem flipVertMenuItem;
      private System.Windows.Forms.ToolStripMenuItem rotateMenuItem;
      private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
      private System.Windows.Forms.ToolStripMenuItem selectAllMenuItem;
      private System.Windows.Forms.ToolStripMenuItem recentMenuItem;
      private System.Windows.Forms.ToolStripMenuItem emptyToolStripMenuItem;
      private System.Windows.Forms.ToolStripSeparator recentMenuSep;
      private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem aboutMenuItem;
      private System.Windows.Forms.ToolStripStatusLabel sbarSel;
      private System.Windows.Forms.OpenFileDialog dlgImgOpen;
      private System.Windows.Forms.ToolStripMenuItem mapSizeMenuItem;
      private System.Windows.Forms.ToolStripMenuItem mapBkgdMenuItem;
      private System.Windows.Forms.ToolStripMenuItem loadImageFromFileToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem undoMenuItem;
      private System.Windows.Forms.ToolStripMenuItem redoMenuItem;
      private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
      private System.Windows.Forms.ToolStrip tbar;
      private System.Windows.Forms.ToolStripButton tbarSelMode;
      private System.Windows.Forms.ToolStripButton tbarLineMode;
      private System.Windows.Forms.ToolStripButton tbarRectMode;
      private System.Windows.Forms.ToolStripButton tbarEllipseMode;
      private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
      private System.Windows.Forms.ToolStripButton tbarFilipseMode;
      private System.Windows.Forms.ToolStripDropDownButton tbarColors;
      private System.Windows.Forms.ToolStripMenuItem tbarColorMenuBase;
      private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;


   }
}

