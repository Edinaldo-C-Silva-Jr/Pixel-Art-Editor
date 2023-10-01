using PixelEditor.Controls;
using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace PixelEditor
{
    public partial class frm_PixelEditor : Form
    {
        private Bitmap originalImage = new Bitmap(1, 1);

        public frm_PixelEditor()
        {
            InitializeComponent();
        }

        private void frm_PixelEditor_Load(object sender, EventArgs e)
        {
            cbb_Grid.SelectedIndex = 0;
            cbb_ColorAmount.SelectedIndex = 3;
            chk_Transparency.Checked = false;
            chk_ChangeColor.Checked = true;

            SetColorAmount();
            OrganizeColorsPanel();

            SetViewingAreaSize();

            ReorganizeControls();
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

        private void SetViewingAreaSize()
        {
            Color defaultColor = Color.FromArgb(254, 254, 254);

            int height = (int)nmb_PixelHeight.Value;
            int width = (int)nmb_PixelWidth.Value;
            int zoom = (int)nmb_ViewingZoom.Value;
            int gridType = cbb_Grid.SelectedIndex;
            Color gridColor = tbl_GridColor.GetCurrentColor();

            originalImage = new Bitmap(width * zoom, height * zoom);
            Graphics imageFiller = Graphics.FromImage(originalImage);
            imageFiller.Clear(defaultColor);

            if (chk_Transparency.Checked)
            {
                originalImage.MakeTransparent(defaultColor);
            }

            dbx_ViewingArea.SetNewImage(originalImage, zoom, gridType, gridColor, chk_Transparency.Checked);
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

            originalImage = dbx_ViewingArea.DrawPixelByClick(originalImage, mouseClick.X, mouseClick.Y, (int)nmb_ViewingZoom.Value, tbl_Colors.GetCurrentColor(), cbb_Grid.SelectedIndex);
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
                    if (!(cell.IsColorDefault()) && chk_ChangeColor.Checked)
                    {
                        SwapColorInImage(cell.BackColor, clr_ColorPicker.Color);
                    }
                    cell.BackColor = clr_ColorPicker.Color;
                    cell.SetIfDefaultColor(false);
                }
            }

            cellParent.ChangeCurrentCell(cell);
        }

        private void SwapColorInImage(Color oldColor, Color newColor)
        {
            int height = (int)nmb_PixelHeight.Value;
            int width = (int)nmb_PixelWidth.Value;
            int zoom = (int)nmb_ViewingZoom.Value;
            int gridType = cbb_Grid.SelectedIndex;
            Color pixelColor;

            Bitmap image = new Bitmap(dbx_ViewingArea.Image);
            
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    pixelColor = image.GetPixel(x*zoom, y*zoom);
                    if (oldColor.ToArgb() == pixelColor.ToArgb())
                    {
                        originalImage = dbx_ViewingArea.DrawPixelByPosition(originalImage, x, y, zoom, newColor, gridType);
                    }
                }
            }

            dbx_ViewingArea.Refresh();
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

        private void dbx_ViewingArea_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                originalImage = dbx_ViewingArea.DrawPixelByClick(originalImage, e.X, e.Y, (int)nmb_ViewingZoom.Value, tbl_Colors.GetCurrentColor(), cbb_Grid.SelectedIndex);
                dbx_ViewingArea.Refresh();
            }
        }

        private void btn_SaveImage_Click(object sender, EventArgs e)
        {
            string directory = "C:\\Users\\" + Environment.UserName + "\\Documents\\PixelEditor\\SavedImages\\";

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            string filename = directory + "PixelImage_" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-ff") + ".png";

            originalImage.Save(filename, ImageFormat.Png);
        }
    }
}