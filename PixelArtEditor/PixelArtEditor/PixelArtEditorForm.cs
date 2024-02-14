using PixelArtEditor.Controls;
using PixelArtEditor.Files;
using PixelArtEditor.Grids;

namespace PixelArtEditor
{
    public partial class PixelArtEditorForm : Form
    {
        /// <summary>
        /// The class responsible for storing and handling the image used in the program.
        /// </summary>
        private ImageHandler ImageManager { get; set; }

        /// <summary>
        /// The class responsible for handling the saving and loading of files in the program.
        /// </summary>
        private FileSaveLoadHandler FileSaverLoader { get; } = new();

        public PixelArtEditorForm()
        {
            ImageManager = new();
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
            GridColorTable.GenerateColorGrid(1, new EventHandler(ColorCellClicked), Color.Gray);
            BackgroundColorTable.GenerateColorGrid(1, new EventHandler(ColorCellClicked), Color.FromArgb(254, 254, 254));

            // Defines the values for the GridType ComboBox based on the GridType Enum values.
            GridTypeComboBox.DataSource = Enum.GetValues(typeof(GridType));

            // Sets default values for the ComboBoxes and CheckBoxes
            GridTypeComboBox.SelectedItem = GridType.None;
            ColorAmountComboBox.SelectedIndex = 3;
            TransparencyCheckBox.Checked = false;
            ColorChangeCheckBox.Checked = true;
            ResizeOnLoadCheckBox.Checked = true;
        }

        /// <summary>
        /// Sets the amount of colors shown in the Palette ColorTable based on the amount of colors selected in the ComboBox. 
        /// </summary>
        private void SetPaletteColorAmount()
        {
            int colorAmount = int.Parse(ColorAmountComboBox.SelectedItem.ToString() ?? "0");
            PaletteColorTable.GenerateColorGrid(colorAmount, new EventHandler(ColorCellClicked));
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

        // TODO: Don't generate Grid everytime the Grid type has to be called, only when the grid type changes
        /// <summary>
        /// Method that defines which Grid implementation to use based on the currently selected Grid Type.
        /// It returns a grid implementation set to the current image size.
        /// </summary>
        /// <returns>A grid implementation of the currently defined grid type.</returns>
        private IGridGenerator DefineGridType()
        {
            int zoom = (int)ViewingZoomNumberBox.Value;
            Color gridColor = GridColorTable.GetCurrentColor();

            GridType gridType = (GridType)GridTypeComboBox.SelectedItem;
            IGridGenerator gridGenerator;

            gridGenerator = gridType switch
            {
                GridType.Line => new LineGrid(),
                GridType.Checker => new CheckerGrid(),
                _ => new NoGrid()
            };

            gridGenerator.GenerateGrid(ImageManager.OriginalImage, zoom, gridColor);
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
            IGridGenerator gridGenerator = DefineGridType();

            bool transparent = TransparencyCheckBox.Checked;
            ImageManager.CreateNewImage(width, height, zoom, backgroundColor, transparent);

            ViewingAreaDrawingBox.SetNewSize(width * zoom, height * zoom);
            ViewingAreaDrawingBox.SetNewImage(gridGenerator, ImageManager.OriginalImage, backgroundColor);
        }

        private void ReorganizeControls()
        {
            this.SuspendLayout();
            ViewingAreaBackgroundPanel.ResizePanelToFitControls();
            ColorAreaBackgroundPanel.ResizePanelToFitControls();

            ColorAreaBackgroundPanel.Location = new Point(ColorAreaBackgroundPanel.Location.X, ViewingAreaBackgroundPanel.Location.Y + ViewingAreaBackgroundPanel.Height + 10);
            this.ResumeLayout();
        }

        private void ViewingAreaDrawingBox_Click(object sender, EventArgs e)
        {
            MouseEventArgs mouseClick = (MouseEventArgs)e;

            if (mouseClick.Button == MouseButtons.Left)
            {
                ImageManager.ClearImageSelection();
                int zoom = (int)ViewingZoomNumberBox.Value;
                Color paletteColor = PaletteColorTable.GetCurrentColor();
                ViewingAreaDrawingBox.DrawPixelByClick(DefineGridType(), ImageManager.OriginalImage, mouseClick.X, mouseClick.Y, zoom, paletteColor);
                ViewingAreaDrawingBox.Refresh();
            }
        }

        private void ColorCellClicked(object? sender, EventArgs e)
        {
            MouseEventArgs mouseClick = (MouseEventArgs)e;

            if (sender is not RectangleCell cell)
            {
                return;
            }

            if (cell.Parent is not ColorTable cellParent)
            {
                return;
            }

            if (mouseClick.Button == MouseButtons.Right)
            {
                DialogResult colorpicked = ColorPickerDialog.ShowDialog();

                if (colorpicked == DialogResult.OK)
                {
                    bool colorWasChanged = false;

                    // The color will always be swaped for the image's background, otherwise only if the color is not in its default color.
                    if (cellParent.Name == "BackgroundColorTable" || (!cell.DefaultColor && ColorChangeCheckBox.Checked))
                    {
                        SwapColorInImage(cell.BackColor, ColorPickerDialog.Color);
                        colorWasChanged = true;
                    }

                    // If the change was done to the background, check if the background should be shown as transparent
                    if (cellParent.Name == "BackgroundColorTable" && TransparencyCheckBox.Checked)
                    {
                        ImageManager.MakeImageTransparent(ColorPickerDialog.Color);
                        colorWasChanged = true;
                    }

                    // Only reload the image if there was a change.
                    if (colorWasChanged)
                    {
                        Color background = BackgroundColorTable.GetCurrentColor();
                        ViewingAreaDrawingBox.SetNewImage(DefineGridType(), ImageManager.OriginalImage, background);
                    }

                    cell.ChangeCellColor(ColorPickerDialog.Color);
                }
            }

            cellParent.ChangeCurrentCell(cell);
        }

        private void SwapColorInImage(Color oldColor, Color newColor)
        {
            if (TransparencyCheckBox.Checked)
            {
                Color backgroundColor = BackgroundColorTable.GetCurrentColor();
                (int width, int height, int zoom) = GetImageSizeValues();
                ImageManager.MakeImageNotTransparent(backgroundColor, width, height, zoom);
            }

            ImageManager.MakeImageTransparent(oldColor);

            using Bitmap auxiliaryImage = new(ImageManager.OriginalImage);
            using Graphics auxiliaryGraphics = Graphics.FromImage(auxiliaryImage);
            auxiliaryGraphics.Clear(newColor);
            auxiliaryGraphics.DrawImage(ImageManager.OriginalImage, 0, 0);
            
            ImageManager.DefineNewImage(auxiliaryImage, true, 0, 0);
        }

        private void SetNewImageButton_Click(object sender, EventArgs e)
        {
            SetNewImageAndViewingAreaSize();
            ReorganizeControls();
        }

        private void CalculateMaximumZoom(int size)
        {
            int zoom = 4096 / size;
            if (zoom > 64)
            {
                zoom = 64;
            }
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
                ImageManager.ClearImageSelection();
                int zoom = (int)ViewingZoomNumberBox.Value;
                Color paletteColor = PaletteColorTable.GetCurrentColor();
                ViewingAreaDrawingBox.DrawPixelByClick(DefineGridType(), ImageManager.OriginalImage, e.X, e.Y, zoom, paletteColor);
                ViewingAreaDrawingBox.Refresh();
            }

            if (e.Button == MouseButtons.Right)
            {
                ChangeSelectionOnImage(e.Location);
            }
        }

        private void SaveImageButton_Click(object sender, EventArgs e)
        {
            FileSaverLoader.SaveImage(ImageManager.OriginalImage);
        }

        private void TransparencyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Color backgroundColor = BackgroundColorTable.GetCurrentColor();

            if (TransparencyCheckBox.Checked)
            {
                ImageManager.MakeImageTransparent(backgroundColor);
                BackgroundColorLabel.Text = "Transparency Color";
            }
            else
            {
                (int width, int height, int zoom) = GetImageSizeValues();
                ImageManager.MakeImageNotTransparent(backgroundColor, width, height, zoom);
                BackgroundColorLabel.Text = "Background Color";
            }

            IGridGenerator generator = DefineGridType();
            ViewingAreaDrawingBox.SetNewImage(generator, ImageManager.OriginalImage, backgroundColor);
        }

        /// <summary>
        /// Method called when a change is made to the Gridtype ComboBox.
        /// It changes the implementation of grids used to the newly selected grid type, then applies the new grid type to the image in the drawing area.
        /// </summary>
        private void GridTypeComboBox_SelectedIndexChanged_ApplyGridToImage(object sender, EventArgs e)
        {
            IGridGenerator gridApply = DefineGridType();
            Color backgroundColor = BackgroundColorTable.GetCurrentColor();
            ViewingAreaDrawingBox.ApplyNewGrid(gridApply, ImageManager.OriginalImage, backgroundColor);
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
            {
                using Bitmap imageLoaded = FileSaverLoader.LoadImage();
                if (imageLoaded != null)
                {
                    bool resizeOnLoad = ResizeOnLoadCheckBox.Checked;
                    int width = ViewingAreaDrawingBox.Width;
                    int height = ViewingAreaDrawingBox.Height;
                    ImageManager.DefineNewImage(imageLoaded, resizeOnLoad, width, height);

                    if (resizeOnLoad)
                    {
                        ViewingAreaDrawingBox.SetNewSize(ImageManager.OriginalImage.Width, ImageManager.OriginalImage.Height);
                    }
                }

                IGridGenerator generator = DefineGridType();
                Color backgroundColor = BackgroundColorTable.GetCurrentColor();
                ViewingAreaDrawingBox.SetNewImage(generator, ImageManager.OriginalImage, backgroundColor);

                ReorganizeControls();
            }
        }

        private void ViewingAreaDrawingBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ImageManager.DefineSelectionStart(e.Location);
                ChangeSelectionOnImage(e.Location);
            }
        }

        private void CopyButton_Click(object sender, EventArgs e)
        {
            ImageManager.CopySelectionFromImage();
        }

        private void PasteButton_Click(object sender, EventArgs e)
        {
            ImageManager.PasteSelectionInImage();

            IGridGenerator generator = DefineGridType();
            Color backgroundColor = BackgroundColorTable.GetCurrentColor();
            ViewingAreaDrawingBox.SetNewImage(generator, ImageManager.OriginalImage, backgroundColor);
        }

        private void ViewingAreaDrawingBox_Paint(object sender, PaintEventArgs e)
        {
            ImageManager.DrawSelectionOntoDrawingBox(e.Graphics);
        }

        private void ChangeSelectionOnImage(Point location)
        {
            int width = ViewingAreaDrawingBox.Width;
            int height = ViewingAreaDrawingBox.Height;
            int zoom = (int)ViewingZoomNumberBox.Value;
            ImageManager.ChangeImageSelection(location, width, height, zoom);
            ViewingAreaDrawingBox.Invalidate();
        }

        private void ViewingZoomNumberBox_ValueChanged(object sender, EventArgs e)
        {
            (int width, int height, int zoom) = GetImageSizeValues();

            ImageManager.ChangeImageZoom(width, height, zoom);

            IGridGenerator generator = DefineGridType();
            Color backgroundColor = BackgroundColorTable.GetCurrentColor();

            ViewingAreaDrawingBox.SetNewSize(width * zoom, height * zoom);
            ViewingAreaDrawingBox.SetNewImage(generator, ImageManager.OriginalImage, backgroundColor);

            ReorganizeControls();
        }

        private void SavePaletteButton_Click(object sender, EventArgs e)
        {
            string paletteValues = PaletteColorTable.GetAllColorValues();
            FileSaverLoader.SavePalette(paletteValues);
        }

        private void LoadPaletteButton_Click(object sender, EventArgs e)
        {
            string paletteValues = FileSaverLoader.LoadPalette();
            PaletteColorTable.SetAllColorValues(paletteValues);
        }
    }
}