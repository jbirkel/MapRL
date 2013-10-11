using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TextMap
{
   public partial class Props : Form
   {
      public Size sizeGrid;
      
      public Props()
      {
         InitializeComponent();
      }

      private void Props_Load(object sender, EventArgs e)
      {
         numGridWid.Value = sizeGrid.Width;
         numGridHgt.Value = sizeGrid.Height;
      }

      private void btnOK_Click(object sender, EventArgs e)
      {
         sizeGrid.Width  = (int) numGridWid.Value;
         sizeGrid.Height = (int) numGridHgt.Value;
         DialogResult = DialogResult.OK;
      }

      private void button1_Click(object sender, EventArgs e)
      {
         DialogResult = DialogResult.Cancel;      
      }

      private void numGridWid_Enter(object sender, EventArgs e) { numGridWid.Select(0, 9); }
      private void numGridHgt_Enter(object sender, EventArgs e) { numGridHgt.Select(0, 9); }

      private void numGridWid_ValueChanged(object sender, EventArgs e)
      {

      }

      private void numGridHgt_ValueChanged(object sender, EventArgs e)
      {

      }
   }
}
