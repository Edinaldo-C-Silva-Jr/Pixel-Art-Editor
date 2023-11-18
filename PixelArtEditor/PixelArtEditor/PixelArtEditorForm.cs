using PixelArtEditor.Controls;
using PixelArtEditor.Grids;
using System.Drawing.Imaging;

namespace PixelArtEditor
{
    public partial class PixelArtEditorForm : Form
    {
        // The original image that is being drawn on in the editor
        private Bitmap originalImage = new(1, 1);

        public PixelArtEditorForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Runs all the initialization methods.
        /// Initializes the control values, default values, position and text values, builds the color tables and creates a default blank image.
        /// </summary>
        private void PixelArtEditorForm_Load(object sender, EventArgs e)
        {
            InitializeControlValues();
            InitializeColorsPanel();
            SetPaletteColorAmount();
            SetNewImageAndViewingAreaSize();
            ReorganizeControls();
        }

        /// <summary>
        /// Sets all the location and text values to the controls in the ColorAreaBackgroundPanel.
        /// Builds the panel programatically as to prevent Designer clutter.
        /// </summary>
        private void InitializeColorsPanel()
        {
            // Sets the text and size of the Color Amount Label, and moves the ComboBox accordingly
            ColorAmountLabel.Size = new Size(90, 20);
            ColorAmountLabel.Text = "Color Amount";
            ColorAmountComboBox.Location = new Point(ColorAmountLabel.Location.X + ColorAmountLabel.Width, ColorAmountComboBox.Location.Y);

            // Sets the text and size of the Grid Color Label, and moves the ColorTable accordingly
            GridColorLabel.Size = new Size(65, 20);
            GridColorLabel.Text = "Grid Color";
            GridColorTable.Location = new Point(GridColorLabel.Location.X + GridColorLabel.Width, GridColorTable.Location.Y);
            
            // Sets the text, size and location of the Background Color Label, and moves the ColorTable accordingly
            BackgroundColorLabel.Size = new Size(110, 20);
            BackgroundColorLabel.Text = "Background Color";
            BackgroundColorLabel.Location = new Point(GridColorTable.Location.X + GridColorTable.Width + 10, BackgroundColorLabel.Location.Y);
            BackgroundColorTable.Location = new Point(BackgroundColorLabel.Location.X + BackgroundColorLabel.Width, BackgroundColorTable.Location.Y);
        }

        /// <summary>
        /// Sets all the default values for controls that need a default value.
        /// Also initializes the ColorTables and ComboBoxes
        /// </summary>
        private void InitializeControlValues()
        {
            // Generates the ColorTables for Grid Color and Background Color.
            GridColorTable.GenerateColorGrid(1, 30, new EventHandler(ColorCellClicked), Color.Gray, false);
            BackgroundColorTable.GenerateColorGrid(1, 30, new EventHandler(ColorCellClicked), Color.FromArgb(254, 254, 254), false);

            // Defines the values for the GridType ComboBox based on the GridType Enum values.
            GridTypeComboBox.DataSource = Enum.GetValues(typeof(GridType));

            // Sets default values for the ComboBoxes and CheckBoxes
            GridTypeComboBox.SelectedItem = GridType.None;
            ColorAmountComboBox.SelectedIndex = 3;
            TransparencyCheckBox.Checked = false;
            ColorChangeCheckBox.Checked = true;
        }

        /// <summary>
        /// Sets the amount of colors shown in the Palette ColorTable based on the amount of colors selected in the ComboBox. 
        /// </summary>
        private void SetPaletteColorAmount()
        {
            int colorAmount = int.Parse(ColorAmountComboBox.SelectedItem.ToString()!);
            PaletteColorTable.GenerateColorGrid(colorAmount, 16, new EventHandler(ColorCellClicked!));
        }

        /// <summary>
        /// Gets the values for the image's width, height and zoom amount from the respective ComboBoxes and returns them as a tuple.
        /// </summary>
        /// <returns>A tuple of Width, Height and Zoom values, in this order.</returns>
        private (int width, int height, int zoom) GetImageSizeValues()
        {
            int height = (int)PixelHeightNumberBox.Value;
            int width = (int)PixelWidthNumberBox.Value;
            int zoom = (int)ViewingZoomNumberBox.Value;

            return (width, height, zoom);
        }

        /// <summary>
        /// Method that defines which Grid implementation to use based on the currently selected Grid Type.
        /// It returns a grid implementation set to the current image size.
        /// </summary>
        /// <returns>A grid implementation of the currently defined grid type.</returns>
        private IGridGenerator DefineGridType()
        {
            (_, _, int zoom) = GetImageSizeValues();
            Color gridColor = GridColorTable.GetCurrentColor();

            GridType gridType = (GridType)GridTypeComboBox.SelectedItem;
            IGridGenerator gridGenerator;

            gridGenerator = gridType switch
            {
                GridType.Line => new LineGrid(),
                GridType.Checker => new CheckerGrid(),
                _ => new NoGrid()
            };

            gridGenerator.GenerateGrid(originalImage, zoom, gridColor);
            return gridGenerator;
        }

        /// <summary>
        /// Creates a new blank image using the current image size, zoom values and transparency settings.
        /// Then sets it in the ViewingArea DrawingBox with the appropriate grid.
        /// </summary>
        private void SetNewImageAndViewingAreaSize()
        {
            // Gets current image and color values
            (int width, int height, int zoom) = GetImageSizeValues();
            Color backgroundColor = BackgroundColorTable.GetCurrentColor();
            Color gridColor = GridColorTable.GetCurrentColor();
            IGridGenerator gridGenerator = DefineGridType();

            // Creates the image
            originalImage = new Bitmap(width * zoom, height * zoom);
            Graphics imageFiller = Graphics.FromImage(originalImage);
            imageFiller.Clear(backgroundColor);

            // Changes transparency
            if (TransparencyCheckBox.Checked)
            {
                originalImage.MakeTransparent(backgroundColor);
            }

            ViewingAreaDrawingBox.SetNewImage(gridGenerator, originalImage, zoom, gridColor);
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

            ViewingAreaDrawingBox.DrawPixelByClick(DefineGridType(), originalImage, mouseClick.X, mouseClick.Y, (int)ViewingZoomNumberBox.Value, PaletteColorTable.GetCurrentColor(), (GridType)GridTypeComboBox.SelectedItem);
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
                    if (!(cell.DefaultColor) && ColorChangeCheckBox.Checked)
                    {
                        SwapColorInImage(cell.BackColor, ColorPickerDialog.Color);
                    }
                    if (cellParent.Name == "tbl_BackgroundColor")
                    {
                        ChangeImageTransparency(TransparencyCheckBox.Checked, BackgroundColorTable.GetCurrentColor(), ColorPickerDialog.Color);
                    }

                    cell.ChangeCellColor(ColorPickerDialog.Color);
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
                        ViewingAreaDrawingBox.DrawPixelByPosition(DefineGridType(), originalImage, x, y, zoom, newColor, gridType);
                    }
                }
            }

            ViewingAreaDrawingBox.Refresh();
        }

        private void SetNewImageSizeButton_Click(object sender, EventArgs e)
        {
            SetNewImageAndViewingAreaSize();
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
            SetPaletteColorAmount();
            ReorganizeControls();
        }

        private void ViewingAreaDrawingBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ViewingAreaDrawingBox.DrawPixelByClick(DefineGridType(), originalImage, e.X, e.Y, (int)ViewingZoomNumberBox.Value, PaletteColorTable.GetCurrentColor(), (GridType)GridTypeComboBox.SelectedItem);
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
                Bitmap temporaryImage = new Bitmap(width * zoom, height * zoom);
                IGridGenerator generator = DefineGridType();

                Graphics temporaryGraphics = Graphics.FromImage(temporaryImage);

                temporaryGraphics.Clear(newColor);
                temporaryGraphics.DrawImage(originalImage, 0, 0);
                originalImage = temporaryImage;

                ViewingAreaDrawingBox.SetNewImage(generator, originalImage, zoom, GridColorTable.GetCurrentColor());
            }
        }

        /// <summary>
        /// Method called when a change is made to the Gridtype ComboBox.
        /// It changes the implementation of grids used to the newly selected grid type, then applies the new grid type to the image in the drawing area.
        /// </summary>
        private void GridTypeComboBox_SelectedIndexChanged_ApplyGridToImage(object sender, EventArgs e)
        {
            IGridGenerator gridApply = DefineGridType();
            ViewingAreaDrawingBox.ApplyNewGrid(gridApply, originalImage);
        }

        private void SizeNumberBoxes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SetNewImageAndViewingAreaSize();
                ReorganizeControls();
            }
        }

        private void LoadImageButton_Click(object sender, EventArgs e)
        {
            string directory = "C:\\Users\\" + Environment.UserName + "\\Documents\\PixelEditor\\SavedImages\\";

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            FileLoadDialog.InitialDirectory = directory;

            DialogResult result = FileLoadDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                originalImage = new(FileLoadDialog.FileName);
                int zoom = (int)ViewingZoomNumberBox.Value;
                PixelWidthNumberBox.Value = originalImage.Width / zoom;
                PixelHeightNumberBox.Value = originalImage.Height / zoom;

                IGridGenerator generator = DefineGridType();
                Color gridColor = GridColorTable.GetCurrentColor();
                ViewingAreaDrawingBox.SetNewImage(generator, originalImage, zoom, gridColor);

                ReorganizeControls();
            }
        }
    }
}