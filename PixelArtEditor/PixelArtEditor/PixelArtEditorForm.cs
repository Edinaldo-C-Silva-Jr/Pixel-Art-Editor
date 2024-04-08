using PixelArtEditor.Controls;
using PixelArtEditor.Drawing_Tools;
using PixelArtEditor.Files;
using PixelArtEditor.Grids;

namespace PixelArtEditor
{
    public partial class PixelArtEditorForm : Form
    {
        #region Properties
        /// <summary>
        /// The class responsible for storing and handling the image used in the program.
        /// </summary>
        private ImageHandler Images { get; set; }

        /// <summary>
        /// The class responsible for handling the saving and loading of files in the program.
        /// </summary>
        private FileSaveLoadHandler FileSaverLoader { get; }

        /// <summary>
        /// A factory class that handles the creation and recovery of DrawingTools.
        /// </summary>
        private DrawingToolFactory ToolFactory { get; set; }

        /// <summary>
        /// A factory class that handles creation and recovery of Grids.
        /// </summary>
        private GridGeneratorFactory GridFactory { get; set; }

        /// <summary>
        /// A point that indicates the mouse position inside the Drawing Box.
        /// Used in the Paint event to define the tool preview location.
        /// </summary>
        private Point? MouseOnDrawingBox { get; set; }
        #endregion

        /// <summary>
        /// Default constructor. Initializes the requirede properties.
        /// </summary>
        public PixelArtEditorForm()
        {
            Images = new();
            FileSaverLoader = new();
            ToolFactory = new();
            GridFactory = new();
            InitializeComponent();
        }

        #region Form and Control Initialization
        /// <summary>
        /// Runs all the initialization methods.
        /// Initializes the control values, default values, position and text values, builds the color tables and creates a default blank image.
        /// </summary>
        private void PixelArtEditorForm_Load(object sender, EventArgs e)
        {
            // Initializes the panels and the controls.
            InitializeControlValues();
            InitializeColorsPanel();
            SetPaletteColorAmount();

            // Initializes the Drawing and Viewing Boxes.
            SetViewingSizeValues();
            SetDrawingSizeValues();
            CreateNewImageForBoxes();

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
            GridColorTable.GenerateColorGrid(1, new EventHandler(ColorCellClicked), Color.LightGray);
            BackgroundColorTable.GenerateColorGrid(1, new EventHandler(ColorCellClicked), Color.White);

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

        private void ColorAmountComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetPaletteColorAmount();
            ReorganizeControls();
        }

        #region Viewing Box Size
        /// <summary>
        /// Saves the size values in the ImageHandler class.
        /// Then changes the size of the Viewing Box and generates a new grid for it based on the size values.
        /// </summary>
        private void ViewingBoxSizeButton_Click(object sender, EventArgs e)
        {
            SetViewingSizeValues();
        }

        /// <summary>
        /// Changes the Original Image Pixel Size in the ImageHandler class, then resizes the image and the Viewing Box.
        /// </summary>
        private void ViewingZoomNumberBox_ValueChanged(object sender, EventArgs e)
        {
            int zoom = (int)ViewingZoomNumberBox.Value;
            Images.ChangeOriginalImageZoom(zoom);

            SetViewingBoxSize();
        }

        /// <summary>
        /// Gets the size values from the Viewing Number Boxes and sets them in the Image Handler, then updates the Viewing Box size.
        /// Also checks if the Drawing Box size is still valid after resizing.
        /// </summary>
        private void SetViewingSizeValues()
        {
            int width = (int)PixelWidthNumberBox.Value;
            int height = (int)PixelHeightNumberBox.Value;
            int zoom = (int)ViewingZoomNumberBox.Value;

            Images.ChangeOriginalImageSize(width, height, zoom);

            SetViewingBoxSize();

            // Sets the Drawing Box values as well to ensure they're still valid.
            // They'll be invalid if the new Viewing Box size is smaller than the current Drawing Box size.
            SetDrawingSizeValues();
        }

        /// <summary>
        /// Sets a new size to the Viewing Box based on the size of the Original Image, then updates the image and the grid.
        /// </summary>
        private void SetViewingBoxSize()
        {
            ViewingBox.SetNewSize(Images.OriginalImage.Width, Images.OriginalImage.Height);
            ViewingBox.SetNewImage(Images.OriginalImage);
            ChangeViewingBoxGrid();
            ReorganizeControls();
        }
        #endregion

        #region Drawing Box Size
        /// <summary>
        /// Saves the size values in the ImageHandler class.
        /// Then changes the size of the Drawing Box and generates a new grid for it based on the size values.
        /// </summary>
        private void DrawingBoxSizeButton_Click(object sender, EventArgs e)
        {
            SetDrawingSizeValues();
        }

        /// <summary>
        /// Changes the Drawing Image Pixel Size in the ImageHandler class, then resizes the image and the Drawing Box.
        /// </summary>
        private void DrawingZoomNumberBox_ValueChanged(object sender, EventArgs e)
        {
            int zoom = (int)DrawingZoomNumberBox.Value;
            Images.ChangeDrawingImageZoom(zoom);

            SetDrawingBoxSize();
        }

        /// <summary>
        /// Gets the size values from the Drawing Number Boxes and sets them in the Image Handler, then updates the Drawing Box size.
        /// </summary>
        private void SetDrawingSizeValues()
        {
            int width = (int)DrawingWidthNumberBox.Value;
            int height = (int)DrawingHeightNumberBox.Value;
            int zoom = (int)DrawingZoomNumberBox.Value;

            Images.ChangeDrawingImageSize(width, height, zoom);

            // Changes the Number Box values to match the new Drawing Dimensions.
            // This updates the Number Boxes in case the values passed are invalid.
            DrawingWidthNumberBox.Value = Images.DrawingDimensions.Width;
            DrawingHeightNumberBox.Value = Images.DrawingDimensions.Height;

            SetDrawingBoxSize();
        }

        /// <summary>
        /// Sets a new size to the Drawing Box based on the size of the Drawing Image, then updates the image and the grid.
        /// </summary>
        private void SetDrawingBoxSize()
        {
            DrawingBox.SetNewSize(Images.DrawingImage.Width, Images.DrawingImage.Height);
            DrawingBox.SetNewImage(Images.DrawingImage);
            ChangeDrawingBoxGrid();
            ReorganizeControls();
        }
        #endregion

        #region Grid Definition and Generation
        /// <summary>
        /// Method called when a change is made to the Gridtype ComboBox.
        /// It changes the implementation of grids used to the newly selected grid type, then applies the new grid to the Drawing Box.
        /// </summary>
        private void ChangeGrid_GridTypeComboBoxIndexChanged(object sender, EventArgs e)
        {
            ChangeDrawingBoxGrid();
            DrawingBox.Invalidate();
        }

        /// <summary>
        /// Sets the background grid for the Viewing box.
        /// </summary>
        private void ChangeViewingBoxGrid()
        {
            ViewingBox.SetBackgroundGrid(Images.OriginalImage.Width, Images.OriginalImage.Height, Images.OriginalPixelSize);
        }

        /// <summary>
        /// Defines which Grid implementation to use based on the currently selected Grid Type.
        /// Also sets the background grid in the Drawing Box.
        /// </summary>
        private void ChangeDrawingBoxGrid()
        {
            Color gridColor = GridColorTable.GetCurrentColor();
            GridType gridType = (GridType)GridTypeComboBox.SelectedItem;

            GridFactory.ChangeCurrentGrid(gridType, Images.DrawingImage.Width, Images.DrawingImage.Height, Images.DrawingPixelSize, gridColor);
            DrawingBox.SetBackgroundGrid(Images.DrawingImage.Width, Images.DrawingImage.Height, Images.DrawingPixelSize);
        }
        #endregion

        #region Creating a New Image
        /// <summary>
        /// Event for the "New Image" button. Creates a new image for both Viewing and Drawing Boxes.
        /// </summary>
        private void SetNewImageButton_Click(object sender, EventArgs e)
        {
            CreateNewImageForBoxes();
        }

        /// <summary>
        /// Creates a new image for both the Viewing Box and the Drawing Box.
        /// </summary>
        private void CreateNewImageForBoxes()
        {
            CreateImageForViewingBox();
            SetImageOnDrawingBox(new Point(0, 0));
        }

        /// <summary>
        /// Creates a new blank image for the Viewing Box, using the current background color and transparency value.
        /// </summary>
        private void CreateImageForViewingBox()
        {
            // Gets the color and transparency values.
            Color backgroundColor = BackgroundColorTable.GetCurrentColor();
            bool transparent = TransparencyCheckBox.Checked;

            Images.CreateNewBlankImage(backgroundColor, transparent);
            ViewingBox.SetNewImage(Images.OriginalImage);
        }

        /// <summary>
        /// Creates a new Drawing Image and sets it on the Drawing Box. Receives a point to be used as the location for the Drawing Image.
        /// </summary>
        /// <param name="drawingImageLocation">The location the Drawing Image will be copied from in the full image.</param>
        private void SetImageOnDrawingBox(Point drawingImageLocation)
        {
            Images.ChangeDrawingImageLocation(drawingImageLocation);
            Images.CreateImageToDraw();
            DrawingBox.SetNewImage(Images.DrawingImage);
        }
        #endregion




        /// <summary>
        /// Resizes all Background Panels to fit the current size of their controls and updates their location accordingly to the size of the ones around them.
        /// </summary>
        private void ReorganizeControls()
        {
            SuspendLayout();
            DrawingBackgroundPanel.ResizePanelToFitControls();
            ViewingBackgroundPanel.ResizePanelToFitControls();
            ColorsBackgroundPanel.ResizePanelToFitControls();

            // Sets a new location to the panels after resizing them to make sure they don't overlap.
            ViewingBackgroundPanel.Location = new Point(DrawingBackgroundPanel.Left + DrawingBackgroundPanel.Width + 10, DrawingBackgroundPanel.Top);

            if (DrawingBackgroundPanel.Height > ViewingBackgroundPanel.Height)
            {
                ColorsBackgroundPanel.Location = new Point(ColorsBackgroundPanel.Location.X, DrawingBackgroundPanel.Top + DrawingBackgroundPanel.Height + 10);
            }
            else
            {
                ColorsBackgroundPanel.Location = new Point(ColorsBackgroundPanel.Location.X, ViewingBackgroundPanel.Top + ViewingBackgroundPanel.Height + 10);
            }

            ResumeLayout();
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

                    // The color will always be swaped for the image's background, and never be swapped when changing the grid color.
                    // Otherwise only if: The Change Color option is enabled, the cell is not in its default color and the color isn't the background color.
                    if (cellParent.Name == "BackgroundColorTable" ||
                        (cellParent.Name != "GridColorTable" && !cell.DefaultColor && ColorChangeCheckBox.Checked && cell.BackColor != BackgroundColorTable.GetCurrentColor()))
                    {
                        SwapColorInImage(cell.BackColor, ColorPickerDialog.Color);
                        colorWasSwaped = true;
                    }

                    // If the swap was done to the background, check if the background should be shown as transparent
                    if (cellParent.Name == "BackgroundColorTable" && TransparencyCheckBox.Checked)
                    {
                        Images.MakeImageTransparent(ColorPickerDialog.Color);
                        colorWasSwaped = true;
                    }

                    // Only reload the image if there was a color swap.
                    if (colorWasSwaped)
                    {
                        ViewingBox.SetNewImage(Images.OriginalImage);
                        Images.CreateImageToDraw();
                        DrawingBox.SetNewImage(Images.DrawingImage);
                    }

                    cell.ChangeCellColor(ColorPickerDialog.Color);

                    // Reloads the grid if the grid color was changed.
                    if (cellParent.Name == "GridColorTable")
                    {
                        ChangeDrawingBoxGrid();
                    }
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
                Images.MakeImageNotTransparent(backgroundColor);
            }

            // Makes all pixels of the color to be replaced transparent...
            Images.MakeImageTransparent(oldColor);

            // Then applies the new color to all transparent pixels.
            using Bitmap auxiliaryImage = new(Images.OriginalImage);
            using Graphics auxiliaryGraphics = Graphics.FromImage(auxiliaryImage);
            auxiliaryGraphics.Clear(newColor);
            auxiliaryGraphics.DrawImage(Images.OriginalImage, 0, 0);
            Images.DefineNewImage(auxiliaryImage, true, 0, 0);

            // Restored transparency to image if needed.
            if (TransparencyCheckBox.Checked)
            {
                Images.MakeImageTransparent(backgroundColor);
            }
        }

        private void TransparencyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Color backgroundColor = BackgroundColorTable.GetCurrentColor();

            if (TransparencyCheckBox.Checked)
            {
                Images.MakeImageTransparent(backgroundColor);
                BackgroundColorLabel.Text = "Transparency Color";
            }
            else
            {
                Images.MakeImageNotTransparent(backgroundColor);
                BackgroundColorLabel.Text = "Background Color";
            }

            ViewingBox.SetNewImage(Images.OriginalImage);
            Images.CreateImageToDraw();
            DrawingBox.SetNewImage(Images.DrawingImage);
        }

        private void CopyButton_Click(object sender, EventArgs e)
        {
            Images.CopySelectionFromImage();
        }

        private void PasteButton_Click(object sender, EventArgs e)
        {
            Images.PasteSelectionInImage();

            DrawingBox.SetNewImage(Images.OriginalImage);
        }

        private void ChangeSelectionOnImage(Point location)
        {
            int width = DrawingBox.Width;
            int height = DrawingBox.Height;
            int zoom = Images.DrawingPixelSize;
            Images.ChangeImageSelection(location, width, height, zoom);
            DrawingBox.Invalidate();
        }

        #region Saving and Loading Files
        /// <summary>
        /// The method that saves the image to a file.
        /// </summary>
        private void SaveImageButton_Click(object sender, EventArgs e)
        {
            FileSaverLoader.SaveImage(Images.OriginalImage);
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
                    int width = DrawingBox.Width;
                    int height = DrawingBox.Height;
                    Images.DefineNewImage(imageLoaded, resizeOnLoad, width, height);

                    if (resizeOnLoad)
                    {
                        ViewingBox.SetNewSize(Images.OriginalImage.Width, Images.OriginalImage.Height);
                    }
                }

                ViewingBox.SetNewImage(Images.OriginalImage);

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

        #region Drawing Tools and Drawing Events
        /// <summary>
        /// Changes the currently selected ToolButton to the button clicked, and changes the current tool to the newly selected one.
        /// </summary>
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
            ToolFactory.ChangeCurrentTool(toolButton.ToolValue);

            MouseOnDrawingBox = null;
        }

        /// <summary>
        /// Gets the tool parameters from the current tool button and returns them as an OptionalToolParameters object.
        /// </summary>
        /// <param name="mouseLocation">The mouse's current location.</param>
        /// <returns>An OptionalToolParameters object containing the parameters relevant for the current tool.</returns>
        private OptionalToolParameters GetToolParameters(Point mouseLocation)
        {
            OptionalToolParameters toolParameters = new();

            Dictionary<string, bool> properties = DrawingToolButtonPanel.CheckToolDrawProperties();

            if (properties["ClickLocation"])
            {
                toolParameters.ClickLocation = mouseLocation;
            }

            if (properties["ImageSize"])
            {
                toolParameters.ImageSize = Images.DrawingImage.Size;
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
                toolParameters.PixelSize = Images.DrawingPixelSize;
            }

            return toolParameters;
        }

        /// <summary>
        /// Uses the current tool's Mouse Click method.
        /// Sets the mouse location and fires the Paint event for the tool's preview.
        /// Defines the start of the image selection on right click.
        /// </summary>
        private void ViewingAreaDrawingBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Images.ClearImageSelection();

                Color paletteColor = PaletteColorTable.GetCurrentColor();

                OptionalToolParameters toolParameters = GetToolParameters(e.Location);

                DrawingBox.DrawClick(ToolFactory.GetTool(), Images.DrawingImage, paletteColor, toolParameters);
                DrawingBox.Image = Images.DrawingImage;
            }

            if (e.Button == MouseButtons.Right)
            {
                Images.DefineSelectionStart(e.Location);
                ChangeSelectionOnImage(e.Location);
            }

            Dictionary<string, bool> previewProperties = DrawingToolButtonPanel.CheckToolPreviewProperties();

            if (previewProperties["PreviewHold"] && e.Button == MouseButtons.Left)
            {
                MouseOnDrawingBox = e.Location;
                DrawingBox.Invalidate();
            }
        }

        /// <summary>
        /// Uses the current tool's Mouse Hold method.
        /// Sets the mouse location and fires the Paint event for the tool's preview.
        /// Changes the image selection.
        /// </summary>
        private void ViewingAreaDrawingBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.X < 0 || e.Y < 0 || e.X >= Images.DrawingImage.Width || e.Y >= Images.DrawingImage.Height)
            {
                MouseOnDrawingBox = e.Location;
            }

            if (e.Button == MouseButtons.Left)
            {
                OptionalToolParameters toolParameters = GetToolParameters(e.Location);

                DrawingBox.DrawHold(ToolFactory.GetTool(), toolParameters);
                DrawingBox.Image = Images.DrawingImage;
            }

            if (e.Button == MouseButtons.Right)
            {
                ChangeSelectionOnImage(e.Location);
            }

            Dictionary<string, bool> previewProperties = DrawingToolButtonPanel.CheckToolPreviewProperties();

            if (previewProperties["PreviewMove"] || (previewProperties["PreviewHold"] && e.Button == MouseButtons.Left))
            {
                MouseOnDrawingBox = e.Location;
                DrawingBox.Invalidate();
            }
        }

        /// <summary>
        /// Uses the current tool's Mouse Release method.
        /// </summary>
        private void ViewingAreaDrawingBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                OptionalToolParameters toolParameters = GetToolParameters(e.Location);

                DrawingBox.DrawRelease(ToolFactory.GetTool(), toolParameters);
                DrawingBox.Image = Images.DrawingImage;

                Images.ApplyDrawnImage();
                ViewingBox.Image = Images.OriginalImage;
            }

            MouseOnDrawingBox = null;
        }

        private void ViewingAreaDrawingBox_MouseLeave(object sender, EventArgs e)
        {
            MouseOnDrawingBox = null;
            DrawingBox.Invalidate();
        }
        #endregion

        /// <summary>
        /// Uses the current tool's Preview method.
        /// Draws the selection onto the image.
        /// </summary>
        private void ViewingAreaDrawingBox_Paint(object sender, PaintEventArgs e)
        {
            if (MouseOnDrawingBox.HasValue)
            {
                OptionalToolParameters toolParameters = GetToolParameters(MouseOnDrawingBox.Value);

                DrawingBox.PreviewTool(ToolFactory.GetTool(), e.Graphics, PaletteColorTable.GetCurrentColor(), toolParameters);
            }

            Images.DrawSelectionOntoDrawingBox(e.Graphics);

            IGridGenerator gridGenerator = GridFactory.GetGrid();
            gridGenerator.ApplyGrid(e.Graphics, Images.DrawingImage.Width, Images.DrawingImage.Height);
        }

        private void ViewingBox_Click(object sender, EventArgs e)
        {
            MouseEventArgs mouseArgs = (MouseEventArgs)e;

            int xPosition = mouseArgs.X;
            int yPosition = mouseArgs.Y;

            xPosition -= xPosition % (Images.OriginalPixelSize * 5);
            yPosition -= yPosition % (Images.OriginalPixelSize * 5);

            if (xPosition > Images.OriginalImage.Width - Images.DrawingDimensions.Width * Images.OriginalPixelSize)
            {
                xPosition = Images.OriginalImage.Width - Images.DrawingDimensions.Width * Images.OriginalPixelSize;
            }

            if (yPosition > Images.OriginalImage.Height - Images.DrawingDimensions.Height * Images.OriginalPixelSize)
            {
                yPosition = Images.OriginalImage.Height - Images.DrawingDimensions.Height * Images.OriginalPixelSize;
            }

            SetImageOnDrawingBox(new Point(xPosition, yPosition));
        }
    }
}