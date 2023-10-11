using PixelArtEditor.Controls;
using PixelArtEditor.Grid;
using System.Drawing.Imaging;

namespace PixelArtEditor
{
    public partial class PixelArtEditorForm : Form
    {
        private Bitmap originalImage = new Bitmap(1, 1);

        public PixelArtEditorForm()
        {
            InitializeComponent();
        }

        private void PixelArtEditorForm_Load(object sender, EventArgs e)
        {
            GridTypeComboBox.DataSource = Enum.GetValues(typeof(GridType));
            GridTypeComboBox.SelectedItem = GridType.None;
            ColorAmountComboBox.SelectedIndex = 3;
            TransparencyCheckBox.Checked = false;
            ColorChangeCheckBox.Checked = true;

            SetColorAmount();
            OrganizeColorsPanel();

            SetViewingAreaSize();

            ReorganizeControls();
        }

        private void SetColorAmount()
        {
            int colorAmount = int.Parse(ColorAmountComboBox.SelectedItem.ToString());
            PaletteColorTable.GenerateColorGrid(colorAmount, 16, new EventHandler(ColorCellClicked));
        }

        private void OrganizeColorsPanel()
        {
            ColorAmountLabel.Size = new Size(90, 20);
            ColorAmountLabel.Text = "Color Amount";
            ColorAmountComboBox.Location = new Point(ColorAmountLabel.Location.X + ColorAmountLabel.Width, ColorAmountComboBox.Location.Y);

            GridColorLabel.Size = new Size(65, 20);
            GridColorLabel.Text = "Grid Color";
            GridColorTable.GenerateColorGrid(1, 30, new EventHandler(ColorCellClicked), Color.Gray, false);
            GridColorTable.Location = new Point(GridColorLabel.Location.X + GridColorLabel.Width, GridColorTable.Location.Y);

            BackgroundColorLabel.Size = new Size(110, 20);
            BackgroundColorLabel.Text = "Background Color";
            BackgroundColorLabel.Location = new Point(GridColorTable.Location.X + GridColorTable.Width + 10, BackgroundColorLabel.Location.Y);
            BackgroundColorTable.GenerateColorGrid(1, 30, new EventHandler(ColorCellClicked), Color.FromArgb(254, 254, 254), false);
            BackgroundColorTable.Location = new Point(BackgroundColorLabel.Location.X + BackgroundColorLabel.Width, BackgroundColorTable.Location.Y);
        }

        private void SetViewingAreaSize()
        {
            Color defaultColor = BackgroundColorTable.GetCurrentColor();

            int height = (int)PixelHeightNumberBox.Value;
            int width = (int)PixelWidthNumberBox.Value;
            int zoom = (int)ViewingZoomNumberBox.Value;
            GridType gridType = (GridType)GridTypeComboBox.SelectedItem;
            Color gridColor = GridColorTable.GetCurrentColor();

            originalImage = new Bitmap(width * zoom, height * zoom);
            Graphics imageFiller = Graphics.FromImage(originalImage);
            imageFiller.Clear(defaultColor);

            if (TransparencyCheckBox.Checked)
            {
                originalImage.MakeTransparent(defaultColor);
            }

            ViewingAreaDrawingBox.SetNewImage(originalImage, zoom, gridType, gridColor);
        }

        private void ReorganizeControls()
        {
            this.SuspendLayout();
            ViewingAreaBackgroundPanel.DefineNewSize(514, 514);
            ColorAreaBackgroundPanel.DefineNewSize(300, 200);

            ColorAreaBackgroundPanel.Location = new Point(ColorAreaBackgroundPanel.Location.X, ViewingAreaBackgroundPanel.Location.Y + ViewingAreaBackgroundPanel.Height + 10);
            this.ResumeLayout();
        }

        private void ViewingAreaDrawingBox_Click(object sender, EventArgs e)
        {
            MouseEventArgs mouseClick = (MouseEventArgs)e;

            originalImage = ViewingAreaDrawingBox.DrawPixelByClick(originalImage, mouseClick.X, mouseClick.Y, (int)ViewingZoomNumberBox.Value, PaletteColorTable.GetCurrentColor(), (GridType)GridTypeComboBox.SelectedItem);
            ViewingAreaDrawingBox.Refresh();
        }

        private void ColorCellClicked(object sender, EventArgs e)
        {
            MouseEventArgs mouseClick = (MouseEventArgs)e;
            RectangleCell cell = sender as RectangleCell;
            ColorTable cellParent = cell.Parent as ColorTable;

            if (mouseClick.Button == MouseButtons.Right)
            {
                DialogResult colorpicked = ColorPickerDialog.ShowDialog();

                if (colorpicked == DialogResult.OK)
                {
                    if (!(cell.IsColorDefault()) && ColorChangeCheckBox.Checked)
                    {
                        SwapColorInImage(cell.BackColor, ColorPickerDialog.Color);
                    }
                    if (cellParent.Name == "tbl_BackgroundColor")
                    {
                        ChangeImageTransparency(TransparencyCheckBox.Checked, BackgroundColorTable.GetCurrentColor(), ColorPickerDialog.Color);
                    }

                    cell.BackColor = ColorPickerDialog.Color;
                    cell.SetIfDefaultColor(false);
                }
            }

            cellParent.ChangeCurrentCell(cell);
        }

        private void SwapColorInImage(Color oldColor, Color newColor)
        {
            int height = (int)PixelHeightNumberBox.Value;
            int width = (int)PixelWidthNumberBox.Value;
            int zoom = (int)ViewingZoomNumberBox.Value;
            GridType gridType = (GridType)GridTypeComboBox.SelectedItem;
            Color pixelColor;

            Bitmap image = new Bitmap(ViewingAreaDrawingBox.Image);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    pixelColor = image.GetPixel(x * zoom, y * zoom);
                    if (oldColor.ToArgb() == pixelColor.ToArgb())
                    {
                        originalImage = ViewingAreaDrawingBox.DrawPixelByPosition(originalImage, x, y, zoom, newColor, gridType);
                    }
                }
            }

            ViewingAreaDrawingBox.Refresh();
        }

        private void SetNewImageSizeButton_Click(object sender, EventArgs e)
        {
            SetViewingAreaSize();
            ReorganizeControls();
        }

        private void CalculateMaximumZoom(int size)
        {
            int zoom = 4096 / size;
            ViewingZoomNumberBox.Maximum = zoom;
        }

        private void SizeValuesChanged(object sender, EventArgs e)
        {
            if (PixelHeightNumberBox.Value > PixelWidthNumberBox.Value)
            {
                CalculateMaximumZoom((int)PixelHeightNumberBox.Value);
            }
            else
            {
                CalculateMaximumZoom((int)PixelWidthNumberBox.Value);
            }
        }

        private void ColorAmountComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetColorAmount();
            ReorganizeControls();
        }

        private void ViewingAreaDrawingBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                originalImage = ViewingAreaDrawingBox.DrawPixelByClick(originalImage, e.X, e.Y, (int)ViewingZoomNumberBox.Value, PaletteColorTable.GetCurrentColor(), (GridType)GridTypeComboBox.SelectedItem);
                ViewingAreaDrawingBox.Refresh();
            }
        }

        private void SaveImageButton_Click(object sender, EventArgs e)
        {
            string directory = "C:\\Users\\" + Environment.UserName + "\\Documents\\PixelEditor\\SavedImages\\";

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            string filename = directory + "PixelImage_" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-ff") + ".png";

            originalImage.Save(filename, ImageFormat.Png);
        }

        private void TransparencyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Color color = BackgroundColorTable.GetCurrentColor();
            ChangeImageTransparency(TransparencyCheckBox.Checked, color, color);
            if (TransparencyCheckBox.Checked)
            {
                BackgroundColorLabel.Text = "Transparency Color";
            }
            else
            {
                BackgroundColorLabel.Text = "Background Color";
            }
        }

        private void ChangeImageTransparency(bool transparent, Color oldColor, Color newColor)
        {
            originalImage.MakeTransparent(oldColor);

            if (!transparent)
            {
                int height = (int)PixelHeightNumberBox.Value;
                int width = (int)PixelWidthNumberBox.Value;
                int zoom = (int)ViewingZoomNumberBox.Value;
                GridType gridType = (GridType)GridTypeComboBox.SelectedItem;
                Bitmap temporaryImage = new Bitmap(width * zoom, height * zoom);
                Graphics temporaryGraphics = Graphics.FromImage(temporaryImage);

                temporaryGraphics.Clear(newColor);
                temporaryGraphics.DrawImage(originalImage, 0, 0);
                originalImage = temporaryImage;

                ViewingAreaDrawingBox.SetNewImage(originalImage, zoom, gridType, GridColorTable.GetCurrentColor());
            }
        }
    }
}