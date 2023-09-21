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
            cbb_Grid.SelectedIndex = 0;

            SetViewingAreaSize();
            SetColorAmount();

            RelocateControls();
        }

        private void SetViewingAreaSize()
        {
            int height = (int)nmb_PixelHeight.Value;
            int width = (int)nmb_PixelWidth.Value;
            int zoom = (int)nmb_ViewingZoom.Value;
            dbx_ViewingArea.GenerateNewImage(width * zoom, height * zoom, zoom, cbb_Grid.SelectedIndex);
        }

        private void SetColorAmount()
        {
            int rows = 2;
            int columns = 8;
            tbl_Colors.GenerateColorGrid(rows, columns, new EventHandler(ColorCellClicked));
            pnl_Colors.DefineNewSize(272, 69);
        }

        private void RelocateControls()
        {
            this.SuspendLayout();
            pnl_ViewingArea.DefineNewSize(514, 514);

            pnl_Colors.Location = new Point(pnl_Colors.Location.X, pnl_ViewingArea.Location.Y + pnl_ViewingArea.Height + 10);
            this.ResumeLayout();
        }

        private void dbx_ViewingArea_Click(object sender, EventArgs e)
        {
            MouseEventArgs mouseClick = (MouseEventArgs)e;

            dbx_ViewingArea.DrawPixel(mouseClick.X, mouseClick.Y, (int)nmb_ViewingZoom.Value, tbl_Colors.GetCurrentColor(), cbb_Grid.SelectedIndex);
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
            SetViewingAreaSize();
            RelocateControls();
        }

        private void CalculateMaximumZoom(int size)
        {
            int zoom = 4096 / size;
            nmb_ViewingZoom.Maximum = zoom;
        }

        private void SizeValuesChanged(object sender, EventArgs e)
        {
            if (nmb_PixelHeight.Value > nmb_PixelWidth .Value)
            {
                CalculateMaximumZoom((int)nmb_PixelHeight.Value);
            }
            else
            {
                CalculateMaximumZoom((int)nmb_PixelWidth.Value);
            }
        }
    }
}
