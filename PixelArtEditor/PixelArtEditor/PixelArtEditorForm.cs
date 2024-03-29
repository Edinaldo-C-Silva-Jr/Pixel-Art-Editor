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

        /// <summary>
        /// Runs all the initialization methods.
        /// Initializes the control values, default values, position and text values, builds the color tables and creates a default blank image.
        /// </summary>
        private void PixelArtEditorForm_Load(object sender, EventArgs e)
        {
            InitializeControlValues();
            InitializeColorsPanel();

            SetPaletteColorAmount();

            SetViewingBoxSize();
            SetDrawingBoxSize();
            SetNewImageOnBoxes();

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

        /// <summary>
        /// Creates a new blank image using the current image size, zoom values and transparency settings.
        /// Then sets it in the Viewing and Drawing Box with the appropriate grid.
        /// </summary>
        private void SetNewImageOnBoxes()
        {
            // Gets all relevant image parameters: size, background color and transparency.
            (int width, int height, int zoom) = GetViewingSizeValues();
            Color backgroundColor = BackgroundColorTable.GetCurrentColor();
            bool transparent = TransparencyCheckBox.Checked;

            Images.CreateNewImage(width, height, zoom, backgroundColor, transparent);

            // Remove later.
            DrawingBox.SetNewImage(Images.OriginalImage);
            // Remove later.

            ViewingBox.SetNewImage(Images.OriginalImage);
            ChangeImageBoxesGrids();
        }

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
                        // Remove later.
                        DrawingBox.SetNewImage(Images.OriginalImage);
                        // Remove later.

                        ViewingBox.SetNewImage(Images.OriginalImage);
                    }

                    cell.ChangeCellColor(ColorPickerDialog.Color);

                    // Reloads the grid if the grid color was changed.
                    if (cellParent.Name == "GridColorTable")
                    {
                        ChangeImageBoxesGrids();
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

        private void SetNewImageButton_Click(object sender, EventArgs e)
        {
            SetNewImageOnBoxes();
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
                Images.MakeImageTransparent(backgroundColor);
                BackgroundColorLabel.Text = "Transparency Color";
            }
            else
            {
                Images.MakeImageNotTransparent(backgroundColor);
                BackgroundColorLabel.Text = "Background Color";
            }

            // Remove later.
            DrawingBox.SetNewImage(Images.OriginalImage);
            // Remove later.

            ViewingBox.SetNewImage(Images.OriginalImage);
        }

        private void SizeNumberBoxes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SetNewImageOnBoxes();
                ReorganizeControls();
            }
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
            int zoom = (int)ViewingZoomNumberBox.Value;
            Images.ChangeImageSelection(location, width, height, zoom);
            DrawingBox.Invalidate();
        }

        private void ViewingZoomNumberBox_ValueChanged(object sender, EventArgs e)
        {
            (int width, int height, int zoom) = GetViewingSizeValues();

            Images.ChangeImageZoom(width, height, zoom);

            DrawingBox.SetNewSize(width * zoom, height * zoom);
            DrawingBox.SetNewImage(Images.OriginalImage);

            ChangeImageBoxesGrids();

            ReorganizeControls();
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
                        DrawingBox.SetNewSize(Images.OriginalImage.Width, Images.OriginalImage.Height);
                    }
                }

                DrawingBox.SetNewImage(Images.OriginalImage);

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
                toolParameters.ImageSize = Images.OriginalImage.Size;
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

                DrawingBox.DrawClick(ToolFactory.GetTool(), Images.OriginalImage, paletteColor, toolParameters);
                DrawingBox.Image = Images.OriginalImage;
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
            if (e.X < 0 || e.Y < 0 || e.X >= Images.OriginalImage.Width || e.Y >= Images.OriginalImage.Height)
            {
                MouseOnDrawingBox = e.Location;
            }

            if (e.Button == MouseButtons.Left)
            {
                OptionalToolParameters toolParameters = GetToolParameters(e.Location);

                DrawingBox.DrawHold(ToolFactory.GetTool(), toolParameters);
                DrawingBox.Image = Images.OriginalImage;
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
                DrawingBox.Image = Images.OriginalImage;
            }

            MouseOnDrawingBox = null;
        }

        private void ViewingAreaDrawingBox_MouseLeave(object sender, EventArgs e)
        {
            MouseOnDrawingBox = null;
            DrawingBox.Invalidate();
        }
        #endregion

        #region Grid Definition and Generation
        /// <summary>
        /// Method that defines which Grid implementation to use based on the currently selected Grid Type.
        /// Also initializes the background grid of the Drawing Box.
        /// </summary>
        /// <returns>A grid implementation of the currently defined grid type.</returns>
        private void ChangeImageBoxesGrids()
        {
            // Gets all necessary parameters.
            (int width, int height, int zoom) = GetViewingSizeValues();
            Color gridColor = GridColorTable.GetCurrentColor();
            GridType gridType = (GridType)GridTypeComboBox.SelectedItem;

            // Sets the grids.
            GridFactory.ChangeCurrentGrid(gridType, width * zoom, height * zoom, zoom, gridColor);
            DrawingBox.SetBackgroundGrid(width * zoom, height * zoom, zoom);
            ViewingBox.SetBackgroundGrid(width * zoom, height * zoom, zoom);
        }

        /// <summary>
        /// Method called when a change is made to the Gridtype ComboBox.
        /// It changes the implementation of grids used to the newly selected grid type, then applies the new grid to the Drawing Box.
        /// </summary>
        private void GridTypeComboBox_SelectedIndexChanged_ApplyGridToImage(object sender, EventArgs e)
        {
            ChangeImageBoxesGrids();
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
            gridGenerator.ApplyGrid(e.Graphics, Images.OriginalImage.Width, Images.OriginalImage.Height);
        }

        private (int width, int height, int zoom) GetDrawingSizeValues()
        {
            int width = (int)DrawingWidthNumberBox.Value;
            int height = (int)DrawingHeightNumberBox.Value;
            int zoom = (int)DrawingZoomNumberBox.Value;

            return (width, height, zoom);
        }

        private void SetDrawingBoxSize()
        {
            (int width, int height, int zoom) = GetDrawingSizeValues();

            DrawingBox.SetNewSize(width * zoom, height * zoom);
        }

        private void DrawingBoxSizeButton_Click(object sender, EventArgs e)
        {
            SetDrawingBoxSize();
            ReorganizeControls();
        }

        /// <summary>
        /// Gets the values for the image's width, height and zoom amount from the respective ComboBoxes and returns them as a tuple.
        /// </summary>
        /// <returns>A tuple of Width, Height and Zoom values, in this order.</returns>
        private (int width, int height, int zoom) GetViewingSizeValues()
        {
            int height = (int)PixelHeightNumberBox.Value;
            int width = (int)PixelWidthNumberBox.Value;
            int zoom = (int)ViewingZoomNumberBox.Value;

            return (width, height, zoom);
        }

        private void SetViewingBoxSize()
        {
            (int width, int height, int zoom) = GetViewingSizeValues();

            ViewingBox.SetNewSize(width * zoom, height * zoom);
        }

        private void ViewingBoxSizeButton_Click(object sender, EventArgs e)
        {
            SetViewingBoxSize();
            ReorganizeControls();
        }
    }
}