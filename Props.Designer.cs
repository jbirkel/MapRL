namespace TextMap
{
   partial class Props
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
         this.btnOK = new System.Windows.Forms.Button();
         this.btnCancel = new System.Windows.Forms.Button();
         this.groupBox1 = new System.Windows.Forms.GroupBox();
         this.label3 = new System.Windows.Forms.Label();
         this.label2 = new System.Windows.Forms.Label();
         this.label1 = new System.Windows.Forms.Label();
         this.numGridHgt = new System.Windows.Forms.NumericUpDown();
         this.numGridWid = new System.Windows.Forms.NumericUpDown();
         this.groupBox1.SuspendLayout();
         ((System.ComponentModel.ISupportInitialize)(this.numGridHgt)).BeginInit();
         ((System.ComponentModel.ISupportInitialize)(this.numGridWid)).BeginInit();
         this.SuspendLayout();
         // 
         // btnOK
         // 
         this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
         this.btnOK.Location = new System.Drawing.Point(122, 132);
         this.btnOK.Name = "btnOK";
         this.btnOK.Size = new System.Drawing.Size(75, 23);
         this.btnOK.TabIndex = 1;
         this.btnOK.Text = "&OK";
         this.btnOK.UseVisualStyleBackColor = true;
         this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
         // 
         // btnCancel
         // 
         this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
         this.btnCancel.Location = new System.Drawing.Point(203, 132);
         this.btnCancel.Name = "btnCancel";
         this.btnCancel.Size = new System.Drawing.Size(75, 23);
         this.btnCancel.TabIndex = 2;
         this.btnCancel.Text = "&Cancel";
         this.btnCancel.UseVisualStyleBackColor = true;
         this.btnCancel.Click += new System.EventHandler(this.button1_Click);
         // 
         // groupBox1
         // 
         this.groupBox1.Controls.Add(this.label3);
         this.groupBox1.Controls.Add(this.label2);
         this.groupBox1.Controls.Add(this.label1);
         this.groupBox1.Controls.Add(this.numGridHgt);
         this.groupBox1.Controls.Add(this.numGridWid);
         this.groupBox1.Location = new System.Drawing.Point(12, 12);
         this.groupBox1.Name = "groupBox1";
         this.groupBox1.Size = new System.Drawing.Size(266, 88);
         this.groupBox1.TabIndex = 0;
         this.groupBox1.TabStop = false;
         this.groupBox1.Text = "Map";
         // 
         // label3
         // 
         this.label3.AutoSize = true;
         this.label3.Location = new System.Drawing.Point(129, 46);
         this.label3.Name = "label3";
         this.label3.Size = new System.Drawing.Size(38, 13);
         this.label3.TabIndex = 4;
         this.label3.Text = "Height";
         // 
         // label2
         // 
         this.label2.AutoSize = true;
         this.label2.Location = new System.Drawing.Point(129, 20);
         this.label2.Name = "label2";
         this.label2.Size = new System.Drawing.Size(35, 13);
         this.label2.TabIndex = 3;
         this.label2.Text = "Width";
         // 
         // label1
         // 
         this.label1.AutoSize = true;
         this.label1.Location = new System.Drawing.Point(7, 20);
         this.label1.Name = "label1";
         this.label1.Size = new System.Drawing.Size(52, 13);
         this.label1.TabIndex = 2;
         this.label1.Text = "Grid Size:";
         // 
         // numGridHgt
         // 
         this.numGridHgt.Location = new System.Drawing.Point(65, 44);
         this.numGridHgt.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
         this.numGridHgt.Name = "numGridHgt";
         this.numGridHgt.Size = new System.Drawing.Size(58, 20);
         this.numGridHgt.TabIndex = 1;
         this.numGridHgt.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
         this.numGridHgt.ValueChanged += new System.EventHandler(this.numGridHgt_ValueChanged);
         this.numGridHgt.Enter += new System.EventHandler(this.numGridHgt_Enter);
         // 
         // numGridWid
         // 
         this.numGridWid.Location = new System.Drawing.Point(65, 18);
         this.numGridWid.Maximum = new decimal(new int[] {
            160,
            0,
            0,
            0});
         this.numGridWid.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
         this.numGridWid.Name = "numGridWid";
         this.numGridWid.Size = new System.Drawing.Size(58, 20);
         this.numGridWid.TabIndex = 0;
         this.numGridWid.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
         this.numGridWid.ValueChanged += new System.EventHandler(this.numGridWid_ValueChanged);
         this.numGridWid.Enter += new System.EventHandler(this.numGridWid_Enter);
         // 
         // Props
         // 
         this.AcceptButton = this.btnOK;
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.CancelButton = this.btnCancel;
         this.ClientSize = new System.Drawing.Size(290, 167);
         this.Controls.Add(this.groupBox1);
         this.Controls.Add(this.btnCancel);
         this.Controls.Add(this.btnOK);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
         this.Name = "Props";
         this.Load += new System.EventHandler(this.Props_Load);
         this.groupBox1.ResumeLayout(false);
         this.groupBox1.PerformLayout();
         ((System.ComponentModel.ISupportInitialize)(this.numGridHgt)).EndInit();
         ((System.ComponentModel.ISupportInitialize)(this.numGridWid)).EndInit();
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.Button btnOK;
      private System.Windows.Forms.Button btnCancel;
      private System.Windows.Forms.GroupBox groupBox1;
      private System.Windows.Forms.Label label1;
      private System.Windows.Forms.NumericUpDown numGridHgt;
      private System.Windows.Forms.NumericUpDown numGridWid;
      private System.Windows.Forms.Label label3;
      private System.Windows.Forms.Label label2;
   }
}