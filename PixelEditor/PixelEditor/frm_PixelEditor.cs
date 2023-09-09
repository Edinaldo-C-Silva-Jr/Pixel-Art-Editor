using System;
using System.Drawing;
using System.Windows.Forms;

namespace PixelEditor
{
    public partial class frm_PixelEditor : Form
    {
        public frm_PixelEditor()
        {
            InitializeComponent();
        }

        private void frm_PixelEditor_Load(object sender, EventArgs e)
        {
            dbx_ViewingArea.NewImage(256, 256);
            dbx_ViewingArea.GenerateGrid(16);

            tbl_Colors.GenerateColorGrid(2, 8);
        }

        private void dbx_ViewingArea_Click(object sender, EventArgs e)
        {
            MouseEventArgs mouseClick = (MouseEventArgs)e;

            dbx_ViewingArea.DrawPixel(mouseClick.X, mouseClick.Y, 16, Color.Black);
            dbx_ViewingArea.Refresh();
        }
    }
}
