using PixelArtEditor.Controls;
using PixelArtEditor.Drawing_Tools;
using PixelArtEditor.Drawing_Tools.Tools;
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

        Point? MouseOnControl;

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

        #region Control Initialization
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

            DrawingToolButtonPanel.ReorganizeButtons();
        }
        #endregion

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
            // Gets all relevant image parameters: size, background color, grid type and transparency.
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

        /// <summary>
        /// The event of when any cell in the Color Tables is clicked.
        /// For a right click, it opens a Color Pick Dialog to allow picking the color, and changes the color in the image if needed.
        /// For a left click, it simply selects the color to be used for drawing.
        /// </summary>
        private void ColorCellClicked(object? sender, EventArgs e)
        {
            MouseEventArgs mouseClick = (MouseEventArgs)e;

            // Only continues if the event was called from a cell within a Color Table.
            if (sender is not RectangleCell cell)
            {
                return;
            }
            if (cell.Parent is not ColorTable cellParent)
            {
                return;
            }

            // For right click, allow picking a color for the cell.
            if (mouseClick.Button == MouseButtons.Right)
            {
                DialogResult colorpicked = ColorPickerDialog.ShowDialog();

                if (colorpicked == DialogResult.OK)
                {
                    bool colorWasSwaped = false;

                    // The color will always be swaped for the image's background.
                    // Otherwise only if the Change Color is enabled and the cell is not in its default color.
                    if (cellParent.Name == "BackgroundColorTable" || (!cell.DefaultColor && ColorChangeCheckBox.Checked))
                    {
                        SwapColorInImage(cell.BackColor, ColorPickerDialog.Color);
                        colorWasSwaped = true;
                    }

                    // If the swap was done to the background, check if the background should be shown as transparent
                    if (cellParent.Name == "BackgroundColorTable" && TransparencyCheckBox.Checked)
                    {
                        ImageManager.MakeImageTransparent(ColorPickerDialog.Color);
                        colorWasSwaped = true;
                    }

                    // Only reload the image if there was a color swap.
                    if (colorWasSwaped)
                    {
                        Color background = BackgroundColorTable.GetCurrentColor();
                        ViewingAreaDrawingBox.SetNewImage(DefineGridType(), ImageManager.OriginalImage, background);
                    }

                    cell.ChangeCellColor(ColorPickerDialog.Color);
                }
            }

            cellParent.ChangeCurrentCell(cell);
        }

        /// <summary>
        /// Changes all pixels in the current image that are from a certain color to a new desired color.
        /// </summary>
        /// <param name="oldColor">The color to be replaced in the image.</param>
        /// <param name="newColor">The new color to apply to the image in place of the old one.</param>
        private void SwapColorInImage(Color oldColor, Color newColor)
        {
            Color backgroundColor = BackgroundColorTable.GetCurrentColor();

            // If the image has a transparent background, temportarily remove the transparency.
            if (TransparencyCheckBox.Checked)
            {
                ImageManager.MakeImageNotTransparent(backgroundColor);
            }

            // Makes all pixels of the color to be replaced transparent...
            ImageManager.MakeImageTransparent(oldColor);

            // Then applies the new color to all transparent pixels.
            using Bitmap auxiliaryImage = new(ImageManager.OriginalImage);
            using Graphics auxiliaryGraphics = Graphics.FromImage(auxiliaryImage);
            auxiliaryGraphics.Clear(newColor);
            auxiliaryGraphics.DrawImage(ImageManager.OriginalImage, 0, 0);

            // Restored transparency to image if needed.
            if (TransparencyCheckBox.Checked)
            {
                ImageManager.MakeImageTransparent(backgroundColor);
            }

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
                ImageManager.MakeImageNotTransparent(backgroundColor);
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

        #region Saving and Loading Files
        /// <summary>
        /// The method that saves the image to a file.
        /// </summary>
        private void SaveImageButton_Click(object sender, EventArgs e)
        {
            FileSaverLoader.SaveImage(ImageManager.OriginalImage);
        }

        /// <summary>
        /// The methods that loads an image from a file into the program.
        /// </summary>
        private void LoadImageButton_Click(object sender, EventArgs e)
        {
            {
                using Bitmap imageLoaded = FileSaverLoader.LoadImage();
                if (imageLoaded != null) // Null check in case no image is loaded.
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

        /// <summary>
        /// The method that saves the current palette into a file.
        /// </summary>
        private void SavePaletteButton_Click(object sender, EventArgs e)
        {
            string paletteValues = PaletteColorTable.GetAllColorValues();
            FileSaverLoader.SavePalette(paletteValues);
        }

        /// <summary>
        /// The method that loads the palette from a file into the program.
        /// </summary>
        private void LoadPaletteButton_Click(object sender, EventArgs e)
        {
            string paletteValues = FileSaverLoader.LoadPalette();
            PaletteColorTable.SetAllColorValues(paletteValues);
        }
        #endregion

        private void ChangeTool_ToolButtonsClick(object sender, EventArgs e)
        {
            if (sender is not ToolButton toolButton)
            {
                return;
            }

            if (toolButton.Parent is not ToolButtonPanel buttonPanel)
            {
                return;
            }

            buttonPanel.ChangeCurrentButton(toolButton);
        }

        private IDrawingTool DefineTool()
        {
            return DrawingToolButtonPanel.CurrentButton switch
            {
                0 => new PixelPenTool(),
                1 => new HorizontalMirrorPenTool(),
                2 => new VerticalMirrorPenTool(),
                3 => new FullMirrorPenTool(),
                4 => new FourMirrorPenTool(),
                5 => new EraserTool()
            };
        }

        private OptionalToolParameters GetToolParameters(Point mouseLocation)
        {
            OptionalToolParameters toolParameters = new();

            Dictionary<string, bool> properties = DrawingToolButtonPanel.CheckToolDrawProperties();

            if (properties["BeginPoint"])
            {
                toolParameters.BeginPoint = mouseLocation;
            }

            if (properties["ImageSize"])
            {
                toolParameters.ImageSize = ImageManager.OriginalImage.Size;
            }

            if (properties["Transparency"])
            {
                toolParameters.Transparency = TransparencyCheckBox.Checked;
            }

            if (properties["BackgroundColor"])
            {
                toolParameters.BackgroundColor = BackgroundColorTable.GetCurrentColor();
            }

            if (properties["PixelSize"])
            {
                toolParameters.PixelSize = (int)ViewingZoomNumberBox.Value;
            }

            return toolParameters;
        }

        private void ViewingAreaDrawingBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ImageManager.ClearImageSelection();

                Color paletteColor = PaletteColorTable.GetCurrentColor();

                OptionalToolParameters toolParameters = GetToolParameters(e.Location);

                ViewingAreaDrawingBox.DrawClick(DefineTool(), DefineGridType(), ImageManager.OriginalImage, paletteColor, toolParameters);
                ViewingAreaDrawingBox.Refresh();
            }

            if (e.Button == MouseButtons.Right)
            {
                ImageManager.DefineSelectionStart(e.Location);
                ChangeSelectionOnImage(e.Location);
            }
        }

        private void ViewingAreaDrawingBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.X < 0 || e.Y < 0 || e.X >= ImageManager.OriginalImage.Width || e.Y >= ImageManager.OriginalImage.Height)
            {
                return;
            }

            if (e.Button == MouseButtons.Left)
            {
                ImageManager.ClearImageSelection();

                OptionalToolParameters toolParameters = GetToolParameters(e.Location);

                ViewingAreaDrawingBox.DrawHold(DefineTool(), DefineGridType(), toolParameters);
                ViewingAreaDrawingBox.Refresh();
            }

            if (e.Button == MouseButtons.Right)
            {
                ChangeSelectionOnImage(e.Location);
            }

            Dictionary<string, bool> previewProperties = DrawingToolButtonPanel.CheckToolPreviewProperties();

            if (previewProperties["PreviewMove"] || (previewProperties["PreviewHold"] && e.Button == MouseButtons.Left))
            {
                MouseOnControl = e.Location;
                ViewingAreaDrawingBox.Invalidate();
            }
        }

        private void ViewingAreaDrawingBox_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void ViewingAreaDrawingBox_Paint(object sender, PaintEventArgs e)
        {
            ImageManager.DrawSelectionOntoDrawingBox(e.Graphics);

            if (MouseOnControl.HasValue)
            {
                OptionalToolParameters toolParameters = GetToolParameters(MouseOnControl.Value);

                ViewingAreaDrawingBox.PreviewTool(DefineTool(), e.Graphics, PaletteColorTable.GetCurrentColor(), toolParameters);
            }
        }
    }
}