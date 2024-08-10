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
        /// The class that handles the selection on the images.
        /// </summary>
        private ImageSelection Selector { get; set; }

        /// <summary>
        /// A point that indicates the mouse position inside the Drawing Box.
        /// Used in the Paint event to define the tool preview location.
        /// </summary>
        private Point? MouseOnDrawingBox { get; set; }
        #endregion

        #region Form Initialization and Closing
        /// <summary>
        /// Default constructor. Initializes the requirede properties.
        /// </summary>
        public PixelArtEditorForm()
        {
            Images = new();
            FileSaverLoader = new();
            ToolFactory = new();
            GridFactory = new();
            Selector = new();
            InitializeComponent();
        }

        /// <summary>
        /// Runs all the initialization methods.
        /// Initializes the control values, default values, position and text values, builds the color tables and creates a default blank image.
        /// </summary>
        private void PixelArtEditorForm_Load(object sender, EventArgs e)
        {
            // Initializes the controls.
            InitializeControlValues();

            // Initializes the Drawing and Viewing Boxes.
            SetViewingSizeValues();
            SetDrawingSizeValues();
            CreateNewImageForBoxes();

            ReorganizeControls();
        }

        /// <summary>
        /// Disposes of the necessary resources.
        /// </summary>
        private void PixelArtEditorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Images?.Dispose();
            GridFactory?.Dispose();
            Selector?.Dispose();
        }
        #endregion

        #region Control Initialization
        /// <summary>
        /// Sets all the default values for controls that need a default value.
        /// Also initializes the ColorTables and ComboBoxes
        /// </summary>
        private void InitializeControlValues()
        {
            // Generates the ColorTables.
            GridColorTable.GenerateColorGrid(1, new EventHandler(ColorCellClicked), Color.LightGray);
            BackgroundColorTable.GenerateColorGrid(1, new EventHandler(ColorCellClicked), Color.White);
            PaletteColorTable.GenerateColorGrid(64, new EventHandler(ColorCellClicked));

            // Defines the values for the GridType ComboBox based on the GridType Enum values.
            GridTypeComboBox.DataSource = Enum.GetValues(typeof(GridType));

            // Sets default values for the ComboBoxes and CheckBoxes
            GridTypeComboBox.SelectedItem = GridType.None;
            SelectionSizeComboBox.SelectedIndex = 0;
            TransparencyCheckBox.Checked = false;
            ColorChangeCheckBox.Checked = true;
            ViewPixelSizeNumberBar.Value = 4;
            DrawPixelSizeNumberBar.Value = 16;

            DrawingToolButtonPanel.ReorganizeButtons();
        }
        #endregion

        #region Viewing Box Size and Clicking
        /// <summary>
        /// Saves the size values in the ImageHandler class.
        /// Then changes the size of the Viewing Box and generates a new grid for it based on the size values.
        /// </summary>
        private void ViewingBoxSizeButton_Click(object sender, EventArgs e)
        {
            SetViewingSizeValues();
        }

        /// <summary>
        /// Gets the size values from the Viewing Number Boxes and sets them in the Image Handler, then updates the Viewing Box size.
        /// Also checks if the Drawing Box size is still valid after resizing.
        /// </summary>
        private void SetViewingSizeValues()
        {
            int width = (int)ViewWidthNumberBox.Value;
            int height = (int)ViewHeightNumberBox.Value;
            int zoom = (int)ViewPixelSizeNumberBox.Value;

            Images.ChangeOriginalImageSize(width, height, zoom);

            SetViewingBoxSize();

            // Sets the Drawing Box values as well to ensure they're still valid.
            // They'll be invalid if the new Viewing Box size is smaller than the current Drawing Box size.
            SetDrawingSizeValues();
        }

        /// <summary>
        /// Changes the Original Image Pixel Size in the ImageHandler class, then resizes the image and the Viewing Box.
        /// </summary>
        private void ViewPixelSizeNumberBox_ValueChanged(object sender, EventArgs e)
        {
            ViewPixelSizeNumberBar.Value = (int)ViewPixelSizeNumberBox.Value; // Syncs both NumberBox and NumberBar.

            int zoom = (int)ViewPixelSizeNumberBox.Value;
            Images.ChangeOriginalImageZoom(zoom);

            SetViewingBoxSize();
        }

        /// <summary>
        /// Updates the Pixel Size value in the ViewPixelSizeNumberBox.
        /// </summary>
        private void ViewPixelSizeNumberBar_ValueChanged(object sender, EventArgs e)
        {
            ViewPixelSizeNumberBox.Value = ViewPixelSizeNumberBar.Value;
        }

        /// <summary>
        /// Sets a new size to the Viewing Box based on the size of the Original Image, then updates the image and the grid.
        /// </summary>
        private void SetViewingBoxSize()
        {
            if (Selector.CurrentImage == ImageType.OriginalImage)
            {
                Selector.ClearSelection();
            }

            ViewingBox.SetNewSize(Images.OriginalImage.Width, Images.OriginalImage.Height);
            ViewingBox.SetNewImage(Images.OriginalImage);
            ChangeViewingBoxGrid();
            ReorganizeControls();
        }

        /// <summary>
        /// Defines a new position for the Drawing Box inside the Viewing Box, depending on the click location.
        /// </summary>
        private void ViewingBox_Click(object sender, EventArgs e)
        {
            MouseEventArgs mouseArgs = (MouseEventArgs)e;

            if (mouseArgs.Button == MouseButtons.Left)
            {
                SetImageOnDrawingBox(mouseArgs.Location);
            }

            ViewingBox.Invalidate();
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
            ViewingBox.Invalidate(); // Invalidates The Viewing Box in order to redraw the Drawing Box overlay in the Viewing Box.
        }

        /// <summary>
        /// Gets the size values from the Drawing Number Boxes and sets them in the Image Handler, then updates the Drawing Box size.
        /// </summary>
        private void SetDrawingSizeValues()
        {
            int width = (int)DrawWidthNumberBox.Value;
            int height = (int)DrawHeightNumberBox.Value;
            int zoom = (int)DrawPixelSizeNumberBox.Value;

            Images.ChangeDrawingImageSize(width, height, zoom);

            // Changes the Number Box values to match the new Drawing Dimensions.
            // This updates the Number Boxes in case the values passed are invalid.
            DrawWidthNumberBox.Value = Images.DrawingDimensions.Width;
            DrawHeightNumberBox.Value = Images.DrawingDimensions.Height;

            SetDrawingBoxSize();
        }

        /// <summary>
        /// Changes the Drawing Image Pixel Size in the ImageHandler class, then resizes the image and the Drawing Box.
        /// </summary>
        private void DrawPixelSizeNumberBox_ValueChanged(object sender, EventArgs e)
        {
            DrawPixelSizeNumberBar.Value = (int)DrawPixelSizeNumberBox.Value; // Syncs both NumberBox and NumberBar.

            int zoom = (int)DrawPixelSizeNumberBox.Value;
            Images.ChangeDrawingImageZoom(zoom);

            SetDrawingBoxSize();
        }

        /// <summary>
        /// Updates the Pixel Size value in the DrawPixelSizeNumberBox.
        /// </summary>
        private void DrawPixelSizeNumberBar_ValueChanged(object sender, EventArgs e)
        {
            DrawPixelSizeNumberBox.Value = DrawPixelSizeNumberBar.Value;
        }

        /// <summary>
        /// Sets a new size to the Drawing Box based on the size of the Drawing Image, then updates the image and the grid.
        /// </summary>
        private void SetDrawingBoxSize()
        {
            if (Selector.CurrentImage == ImageType.DrawingImage)
            {
                Selector.ClearSelection();
            }

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
            DrawingBox.SetNewImage(Images.DrawingImage);
        }
        #endregion





        #region Viewing and Drawing Box Paint
        /// <summary>
        /// Uses the current tool's Preview method.
        /// Draws the selection onto the image.
        /// </summary>
        private void DrawingBox_Paint(object sender, PaintEventArgs e)
        {
            // Uses the Drawing Tool in the Drawing Box.
            if (MouseOnDrawingBox.HasValue)
            {
                OptionalToolParameters toolParameters = GetToolParameters(MouseOnDrawingBox.Value);

                DrawingBox.PreviewTool(ToolFactory.GetTool(), e.Graphics, PaletteColorTable.GetCurrentColor(), toolParameters);
            }

            if (Selector.CurrentImage == ImageType.DrawingImage)
            {
                Selector.DrawSelection(e.Graphics);
            }

            // Applies the grid on top of the Drawing Box.
            GridFactory.GetGrid().ApplyGrid(e.Graphics, Images.DrawingImage.Width, Images.DrawingImage.Height);
        }

        /// <summary>
        /// Draws the Drawing Box location on the Viewing Box.
        /// </summary>
        private void ViewingBox_Paint(object sender, PaintEventArgs e)
        {
            if (Selector.CurrentImage == ImageType.OriginalImage)
            {
                Selector.DrawSelection(e.Graphics);
            }

            // Draws the overlay indicating the Drawing Box position inside the Viewing Box.
            Point location = new(Images.DrawingLocation.X * Images.OriginalPixelSize, Images.DrawingLocation.Y * Images.OriginalPixelSize);
            ViewingBox.DrawDrawingBoxOverlay(e.Graphics, location, Images.DrawingDimensions * Images.OriginalPixelSize);
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
        private void DrawingBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Selector.ClearSelection();

                Color paletteColor = PaletteColorTable.GetCurrentColor();

                OptionalToolParameters toolParameters = GetToolParameters(e.Location);

                DrawingBox.DrawClick(ToolFactory.GetTool(), Images.DrawingImage, paletteColor, toolParameters);
                DrawingBox.Image = Images.DrawingImage;
            }

            if (e.Button == MouseButtons.Right)
            {
                Selector.DefineStart(e.Location);
                Selector.CurrentImage = ImageType.DrawingImage;
                ChangeSelectionOnDrawingImage(e.Location);
                ViewingBox.Invalidate();
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
        private void DrawingBox_MouseMove(object sender, MouseEventArgs e)
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
                ChangeSelectionOnDrawingImage(e.Location);
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
        private void DrawingBox_MouseUp(object sender, MouseEventArgs e)
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

        private void DrawingBox_MouseLeave(object sender, EventArgs e)
        {
            MouseOnDrawingBox = null;
            DrawingBox.Invalidate();
        }
        #endregion

        #region Viewing Box Events
        /// <summary>
        /// Defines the place where the selection starts based on the mouse's right click position.
        /// </summary>
        private void ViewingBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Selector.ClearSelection();
                ViewingBox.Invalidate();
            }

            if (e.Button == MouseButtons.Right)
            {
                Selector.DefineStart(e.Location);
                Selector.CurrentImage = ImageType.OriginalImage;
                ChangeSelectionOnOriginalImage(e.Location);
                DrawingBox.Invalidate();
            }
        }

        /// <summary>
        /// Changes the selection on the image while the right mouse button is still held.
        /// </summary>
        private void ViewingBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ChangeSelectionOnOriginalImage(e.Location);
            }
        }
        #endregion

        #region Image Selection and Copy/Paste
        /// <summary>
        /// Copies the current selection.
        /// </summary>
        private void CopyButton_Click(object sender, EventArgs e)
        {
            Images.CopySelectionFromImage(Selector.SelectedArea, Selector.CurrentImage);
        }

        /// <summary>
        /// Pastes the previously copied image to the current selection.
        /// </summary>
        private void PasteButton_Click(object sender, EventArgs e)
        {
            PasteImage();
        }

        /// <summary>
        /// Pastes the clipboard image into its corresponding image box.
        /// Then refreshes the images for both Drawing and Viewing Box to keep them in sync.
        /// </summary>
        private void PasteImage()
        {
            Images.PasteSelectionOnImage(Selector.SelectedArea, Selector.CurrentImage);

            switch (Selector.CurrentImage)
            {
                case ImageType.OriginalImage:
                    Images.CreateImageToDraw();
                    break;
                case ImageType.DrawingImage:
                    Images.ApplyDrawnImage();
                    break;
            }

            ViewingBox.SetNewImage(Images.OriginalImage);
            DrawingBox.SetNewImage(Images.DrawingImage);
        }

        /// <summary>
        /// Gets the size to use for the selection in the ViewingBox.
        /// </summary>
        /// <returns>The size to use for the Viewing Box selection.</returns>
        private Size GetViewingSelectionSize()
        {
            if (SelectionSizeComboBox.SelectedIndex == 4)
            {
                return new Size(Images.DrawingDimensions.Width, Images.DrawingDimensions.Height);
            }
            else
            {
                int value = int.Parse(SelectionSizeComboBox.SelectedItem.ToString() ?? "1");
                return new Size(value, value);
            }
        }

        /// <summary>
        /// Changes the selection of the Original Image, and then calls a redraw for the boxes.
        /// Redraws the Viewing Box to show the selection, and redraws the Drawing Box to remove any selection, if there was one in it.
        /// </summary>
        /// <param name="location">The current location of the mouse cursor.</param>
        private void ChangeSelectionOnOriginalImage(Point location)
        {
            Selector.ChangeSelectionArea(location, ViewingBox.Size, Images.OriginalPixelSize, GetViewingSelectionSize());
            ViewingBox.Invalidate();
        }

        /// <summary>
        /// Changes the selection of the Drawing Image, and then calls a redraw for the boxes.
        /// Redraws the Drawing Box to show the selection, and redraws the Viewing Box to remove any selection, if there was one in it.
        /// </summary>
        /// <param name="location">The current location of the mouse cursor.</param>
        private void ChangeSelectionOnDrawingImage(Point location)
        {
            Selector.ChangeSelectionArea(location, DrawingBox.Size, Images.DrawingPixelSize);
            DrawingBox.Invalidate();
        }
        #endregion



        /// <summary>
        /// Resizes all Background Panels to fit the current size of their controls.
        /// </summary>
        private void ReorganizeControls()
        {
            SuspendLayout();
            DrawingBackgroundPanel.ResizePanelToFitControls();
            ViewingBackgroundPanel.ResizePanelToFitControls();
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
            ColorValueLabel.Text = $"R:{cell.BackColor.R} G:{cell.BackColor.G} B:{cell.BackColor.B}";
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
            Images.ReplaceOriginalImage(auxiliaryImage);

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

        #region Saving and Loading Files
        /// <summary>
        /// The method that saves the image to a file.
        /// </summary>
        private void SaveImageButton_Click(object sender, EventArgs e)
        {
            FileSaverLoader.SaveImage(Images.OriginalImage, Images.OriginalDimensions);
        }

        /// <summary>
        /// The methods that loads an image from a file into the program.
        /// </summary>
        private void LoadImageButton_Click(object sender, EventArgs e)
        {
            using Bitmap imageLoaded = FileSaverLoader.LoadImage();
            if (imageLoaded != null) // Null check in case no image is loaded.
            {
                int currentPixelSize = (int)ViewPixelSizeNumberBox.Value;
                ViewPixelSizeNumberBox.Value = 1;

                Images.ReplaceOriginalImage(imageLoaded);

                ViewPixelSizeNumberBox.Value = currentPixelSize;
            }

            ViewingBox.SetNewImage(Images.OriginalImage);
            Images.CreateImageToDraw();
            DrawingBox.SetNewImage(Images.DrawingImage);
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

        /// <summary>
        /// Handles the keyboard shortcuts.
        /// </summary>
        private void PixelArtEditorForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control) // Control Key.
            {
                switch (e.KeyCode)
                {
                    case Keys.C: // Control C: Copy.
                        Images.CopySelectionFromImage(Selector.SelectedArea, Selector.CurrentImage);
                        break;
                    case Keys.V: // Control V: Paste.
                        PasteImage();
                        break;
                }
            }
        }
    }
}