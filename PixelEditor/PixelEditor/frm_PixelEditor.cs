using PixelEditor.Controls;
using System;
using System.Media;
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

            tbl_Colors.GenerateColorGrid(2, 8, new EventHandler(ColorCellClicked));
        }

        private void dbx_ViewingArea_Click(object sender, EventArgs e)
        {
            MouseEventArgs mouseClick = (MouseEventArgs)e;

            dbx_ViewingArea.DrawPixel(mouseClick.X, mouseClick.Y, (int)nmb_ViewingZoom.Value, tbl_Colors.GetCurrentColor());
            dbx_ViewingArea.Refresh();
        }

        private void ColorCellClicked(object sender, EventArgs e)
        {
            MouseEventArgs mouseClick = (MouseEventArgs)e;
            RectangleCell cell = sender as RectangleCell;

            if (mouseClick.Button == MouseButtons.Right)
            {
                DialogResult colorpicked = clr_ColorPicker.ShowDialog();

                if (colorpicked == DialogResult.OK)
                {
                    cell.BackColor = clr_ColorPicker.Color;
                }
            }

            tbl_Colors.ChangeCurrentCell(cell);
        }

        private void btn_PixelSize_Click(object sender, EventArgs e)
        {
            int height = (int)nmb_PixelHeight.Value;
            int width = (int)nmb_PixelWidth.Value;
            int zoom = (int)nmb_ViewingZoom.Value;

            dbx_ViewingArea.NewImage(width*zoom, height*zoom);
            dbx_ViewingArea.GenerateGrid(zoom);
        }
    }
}
