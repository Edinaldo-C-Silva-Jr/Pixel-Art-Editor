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
            cbb_ColorAmount.SelectedIndex = 3;

            SetColorAmount();
            OrganizeColorsPanel();
            SetViewingAreaSize();

            ReorganizeControls();
        }

        private void SetViewingAreaSize()
        {
            int height = (int)nmb_PixelHeight.Value;
            int width = (int)nmb_PixelWidth.Value;
            int zoom = (int)nmb_ViewingZoom.Value;
            int gridType = cbb_Grid.SelectedIndex;
            Color gridColor = tbl_GridColor.GetCurrentColor();
            dbx_ViewingArea.GenerateNewImage(width * zoom, height * zoom, zoom, gridType, gridColor);
        }

        private void SetColorAmount()
        {
            int colorAmount = int.Parse(cbb_ColorAmount.SelectedItem.ToString());
            tbl_Colors.GenerateColorGrid(colorAmount, 16, new EventHandler(ColorCellClicked));
        }

        private void OrganizeColorsPanel()
        {
            lbl_ColorAmount.Size = new Size(80, 20);
            lbl_ColorAmount.Text = "Color Amount";

            lbl_gridColor.Size = new Size(65, 20);
            lbl_gridColor.Text = "Grid Color:";

            cbb_ColorAmount.Location = new Point(lbl_ColorAmount.Location.X + lbl_ColorAmount.Width, cbb_ColorAmount.Location.Y);
            
            tbl_GridColor.GenerateColorGrid(1, 30, new EventHandler(ColorCellClicked), Color.Gray, false);
            tbl_GridColor.Location = new Point(lbl_gridColor.Location.X + lbl_gridColor.Width, tbl_GridColor.Location.Y);
        }

        private void ReorganizeControls()
        {
            this.SuspendLayout();
            pnl_ViewingArea.DefineNewSize(514, 514);
            pnl_Colors.DefineNewSize(300, 200);

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
            ColorTable cellParent = cell.Parent as ColorTable;

            if (mouseClick.Button == MouseButtons.Right)
            {
                DialogResult colorpicked = clr_ColorPicker.ShowDialog();

                if (colorpicked == DialogResult.OK)
                {
                    cell.BackColor = clr_ColorPicker.Color;
                }
            }

            cellParent.ChangeCurrentCell(cell);
        }

        private void btn_PixelSize_Click(object sender, EventArgs e)
        {
            SetViewingAreaSize();
            ReorganizeControls();
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

        private void cbb_ColorAmount_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetColorAmount();
            ReorganizeControls();
        }
    }
}
