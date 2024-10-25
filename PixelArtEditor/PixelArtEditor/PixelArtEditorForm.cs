using PixelArtEditor.Controls;
using PixelArtEditor.Files;
using PixelArtEditor.Grids;
using PixelArtEditor.Image_Editing;
using PixelArtEditor.Image_Editing.Drawing_Tools;
using PixelArtEditor.Image_Editing.Image_Tools;
using PixelArtEditor.Image_Editing.Image_Tools.Tools;
using PixelArtEditor.Image_Editing.Undo_Redo;

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

        private UndoRedoHandler UndoHandler { get; }

        private DrawingHandler DrawHandler { get; }

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
            UndoHandler = new();
            DrawHandler = new();

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
            SetViewingZoom();
            SetDrawingSizeValues();
            SetDrawingZoom();

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

            SetUndoRedoButtonAvailability();

            DrawingToolButtonPanel.ReorganizeButtons();
        }
        #endregion

        #region Viewing Box Size, Zoom and Clicking
        /// <summary>
        /// Saves the size values in the ImageHandler class.
        /// Then changes the size of the Viewing Box and generates a new grid for it based on the size values.
        /// </summary>
        private void ViewingBoxSizeButton_Click(object sender, EventArgs e)
        {
            SetViewingSizeValues();

            // Also sets the Drawing Box values to ensure they're still valid.
            // They'll be invalid if the new Viewing Box size is smaller than the current Drawing Box size.
            SetDrawingSizeValues();
        }

        /// <summary>
        /// Gets the size values from the Viewing Number Boxes and sets them in the Image Handler, then updates the Viewing Box size.
        /// Also checks if the Drawing Box size is still valid after resizing.
        /// </summary>
        private void SetViewingSizeValues()
        {
            int width = (int)ViewWidthNumberBox.Value;
            int height = (int)ViewHeightNumberBox.Value;

            Images.ChangeOriginalImageSize(width, height);

            SetViewingBoxSize();
        }

        /// <summary>
        /// Sets a new size to the Viewing Box based on the size of the Original Image, then updates the image and the grid.
        /// </summary>
        private void SetViewingBoxSize()
        {
            Selector.ClearSelection(ImageType.OriginalImage);

            ViewingBox.SetNewSize(Images.DisplayOriginalImage.Width, Images.DisplayOriginalImage.Height);
            ViewingBox.SetNewImage(Images.DisplayOriginalImage);
            ChangeViewingBoxGrid();
            ReorganizeControls();
        }

        /// <summary>
        /// Changes the Original Image Pixel Size in the ImageHandler class, then resizes the image and the Viewing Box.
        /// </summary>
        private void ViewPixelSizeNumberBox_ValueChanged(object sender, EventArgs e)
        {
            ViewPixelSizeNumberBar.Value = (int)ViewPixelSizeNumberBox.Value; // Syncs both NumberBox and NumberBar.
            SetViewingZoom();
        }

        /// <summary>
        /// Updates the Pixel Size value in the ViewPixelSizeNumberBox.
        /// </summary>
        private void ViewPixelSizeNumberBar_ValueChanged(object sender, EventArgs e)
        {
            ViewPixelSizeNumberBox.Value = ViewPixelSizeNumberBar.Value; // Syncs both NumberBox and NumberBar.
        }

        /// <summary>
        /// Changes the zoom value of the Original Image in the ImageHandler class and updates the ViewingBox size.
        /// </summary>
        private void SetViewingZoom()
        {
            int zoom = (int)ViewPixelSizeNumberBox.Value;
            Images.ChangeOriginalImageZoom(zoom);

            SetViewingBoxSize();
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

        #region Drawing Box Size and Zoom
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

            Images.ChangeDrawingImageSize(width, height);

            // Updates the number box values, since they have possibly been changed again by the validation of the Drawing Image Size.
            DrawWidthNumberBox.Value = Images.DrawingImageSize.Width;
            DrawHeightNumberBox.Value = Images.DrawingImageSize.Height;

            SetDrawingBoxSize();
        }

        /// <summary>
        /// Sets a new size to the Drawing Box based on the size of the Drawing Image, then updates the image and the grid.
        /// </summary>
        private void SetDrawingBoxSize()
        {
            Selector.ClearSelection(ImageType.DrawingImage);

            DrawingBox.SetNewSize(Images.DisplayDrawingImage.Width, Images.DisplayDrawingImage.Height);
            DrawingBox.SetNewImage(Images.DisplayDrawingImage);
            ChangeDrawingBoxGrid();
            ReorganizeControls();
        }

        /// <summary>
        /// Changes the Drawing Image Pixel Size in the ImageHandler class, then resizes the image and the Drawing Box.
        /// </summary>
        private void DrawPixelSizeNumberBox_ValueChanged(object sender, EventArgs e)
        {
            DrawPixelSizeNumberBar.Value = (int)DrawPixelSizeNumberBox.Value; // Syncs both NumberBox and NumberBar.
            SetDrawingZoom();
        }

        /// <summary>
        /// Updates the Pixel Size value in the DrawPixelSizeNumberBox.
        /// </summary>
        private void DrawPixelSizeNumberBar_ValueChanged(object sender, EventArgs e)
        {
            DrawPixelSizeNumberBox.Value = DrawPixelSizeNumberBar.Value; // Syncs both NumberBox and NumberBar.
        }

        /// <summary>
        /// Changes the zoom value of the Drawing Image in the ImageHandler class and updates the ViewingBox size.
        /// </summary>
        private void SetDrawingZoom()
        {
            int zoom = (int)DrawPixelSizeNumberBox.Value;
            Images.ChangeDrawingImageZoom(zoom);

            SetDrawingBoxSize();
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
            ViewingBox.SetBackgroundGrid(Images.DisplayOriginalImage.Width, Images.DisplayOriginalImage.Height, Images.OriginalImageZoom);
        }

        /// <summary>
        /// Defines which Grid implementation to use based on the currently selected Grid Type.
        /// Also sets the background grid in the Drawing Box.
        /// </summary>
        private void ChangeDrawingBoxGrid()
        {
            Color gridColor = GridColorTable.GetCurrentColor();
            GridType gridType = (GridType)GridTypeComboBox.SelectedItem;

            GridFactory.ChangeCurrentGrid(gridType, Images.DisplayDrawingImage.Width, Images.DisplayDrawingImage.Height, Images.DrawingImageZoom, gridColor);
            DrawingBox.SetBackgroundGrid(Images.DisplayDrawingImage.Width, Images.DisplayDrawingImage.Height, Images.DrawingImageZoom);
        }
        #endregion

        #region Creating a New Image
        /// <summary>
        /// Event for the "New Image" button. Creates a new image for both Viewing and Drawing Boxes.
        /// </summary>
        private void SetNewImageButton_Click(object sender, EventArgs e)
        {
            SetNewImageOnBoxes();
        }

        /// <summary>
        /// Creates a new image for both the Viewing Box and the Drawing Box.
        /// </summary>
        private void SetNewImageOnBoxes()
        {
            ClearOriginalImage();
            SetImageOnDrawingBox(new Point(0, 0));
        }

        /// <summary>
        /// Uses the NewImageTool to clear the Original Image.
        /// </summary>
        private void ClearOriginalImage()
        {
            NewImageTool tool = new();
            ImageToolParameters imageParameters = new()
            {
                BackgroundColor = BackgroundColorTable.GetCurrentColor()
            };

            UndoParameters undoParameters = new()
            {
                BackgroundColor = BackgroundColorTable.GetCurrentColor()
            };

            tool.UseTool(Images.EditOriginalImage, imageParameters);
            UndoHandler.TrackChange(tool.CreateUndoStep(undoParameters));
            SetUndoRedoButtonAvailability();

            Images.CreateNewDisplayOriginalImage();
            ViewingBox.SetNewImage(Images.DisplayOriginalImage);
        }

        /// <summary>
        /// Creates a new Drawing Image and sets it on the Drawing Box. Receives a point to be used as the location for the Drawing Image.
        /// </summary>
        /// <param name="drawingImageLocation">The location the Drawing Image will be copied from in the full image.</param>
        private void SetImageOnDrawingBox(Point drawingImageLocation)
        {
            drawingImageLocation = new(drawingImageLocation.X / Images.OriginalImageZoom, drawingImageLocation.Y / Images.OriginalImageZoom);
            Images.ChangeDrawingImageLocation(drawingImageLocation);
            DrawingBox.SetNewImage(Images.DisplayDrawingImage);
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
                DrawingToolParameters toolParameters = GetToolParameters(MouseOnDrawingBox.Value);

                DrawHandler.PreviewTool(ToolFactory.GetTool(), e.Graphics, PaletteColorTable.GetCurrentColor(), toolParameters);
            }

            // Draws the selection in the box, if the selection's image type matches the passed image type.
            Selector.DrawSelection(e.Graphics, ImageType.DrawingImage, Images.DrawingImageZoom);

            // Applies the grid on top of the Drawing Box.
            GridFactory.GetGrid().ApplyGrid(e.Graphics, Images.DisplayDrawingImage.Width, Images.DisplayDrawingImage.Height);
        }

        /// <summary>
        /// Draws the Drawing Box location on the Viewing Box.
        /// </summary>
        private void ViewingBox_Paint(object sender, PaintEventArgs e)
        {
            // Draws the selection in the box, if the selection's image type matches the passed image type.
            Selector.DrawSelection(e.Graphics, ImageType.OriginalImage, Images.OriginalImageZoom);

            // Draws the overlay indicating the Drawing Box position inside the Viewing Box.
            Point location = new(Images.DrawingLocation.X * Images.OriginalImageZoom, Images.DrawingLocation.Y * Images.OriginalImageZoom);
            ViewingBox.DrawDrawingBoxOverlay(e.Graphics, location, Images.DrawingImageSize * Images.OriginalImageZoom);
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
        private DrawingToolParameters GetToolParameters(Point mouseLocation)
        {
            DrawingToolParameters toolParameters = new();

            Dictionary<string, bool> properties = DrawingToolButtonPanel.CheckToolDrawProperties();

            if (properties["ClickLocation"])
            {
                if (mouseLocation.X < 0)
                {
                    mouseLocation.X -= Images.DrawingImageZoom;
                }

                if (mouseLocation.Y < 0)
                {
                    mouseLocation.Y -= Images.DrawingImageZoom;
                }

                Point drawLocation = new(mouseLocation.X / Images.DrawingImageZoom, mouseLocation.Y / Images.DrawingImageZoom);
                toolParameters.ClickLocation = drawLocation;
            }

            if (properties["ImageSize"])
            {
                toolParameters.ImageSize = Images.EditDrawingImage.Size;
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
                toolParameters.PixelSize = Images.DrawingImageZoom;
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
                Selector.ClearSelection(ImageType.DrawingImage);

                Color paletteColor = PaletteColorTable.GetCurrentColor();

                DrawingToolParameters toolParameters = GetToolParameters(e.Location);

                DrawHandler.DrawClick(ToolFactory.GetTool(), Images.EditDrawingImage, paletteColor, toolParameters);
                
                Images.CreateNewDisplayDrawingImage();
                DrawingBox.SetNewImage(Images.DisplayDrawingImage);
            }

            if (e.Button == MouseButtons.Right)
            {
                Point selectionStartNoZoom = new(e.X / Images.DrawingImageZoom, e.Y / Images.DrawingImageZoom);
                Selector.DefineStart(selectionStartNoZoom, ImageType.DrawingImage);
                ChangeSelectionOnDrawingImage(selectionStartNoZoom);
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
            if (e.X < 0 || e.Y < 0 || e.X >= Images.DisplayDrawingImage.Width || e.Y >= Images.DisplayDrawingImage.Height)
            {
                MouseOnDrawingBox = e.Location;
            }

            if (e.Button == MouseButtons.Left)
            {
                DrawingToolParameters toolParameters = GetToolParameters(e.Location);

                DrawHandler.DrawHold(ToolFactory.GetTool(), toolParameters); 
                
                Images.CreateNewDisplayDrawingImage();
                DrawingBox.SetNewImage(Images.DisplayDrawingImage);
            }

            if (e.Button == MouseButtons.Right)
            {
                Point selectionLocationNoZoom = new(e.X / Images.DrawingImageZoom, e.Y / Images.DrawingImageZoom);
                ChangeSelectionOnDrawingImage(selectionLocationNoZoom);
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
                DrawingToolParameters toolParameters = GetToolParameters(e.Location);
                UndoParameters undoParameters = new()
                {
                    DrawingImageLocation = Images.DrawingLocation
                };

                DrawHandler.DrawRelease(ToolFactory.GetTool(), toolParameters);

                UndoHandler.TrackChange(DrawHandler.CreateUndoStepFromTool((IUndoRedoCreator)ToolFactory.GetTool(), undoParameters));
                SetUndoRedoButtonAvailability();

                Images.CreateNewDisplayDrawingImage();
                DrawingBox.SetNewImage(Images.DisplayDrawingImage);

                Images.ApplyDrawnImage();
                Images.CreateNewDisplayOriginalImage();
                ViewingBox.SetNewImage(Images.DisplayOriginalImage);
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
                Selector.ClearSelection(ImageType.OriginalImage);
                ViewingBox.Invalidate();
            }

            if (e.Button == MouseButtons.Right)
            {
                Point selectionStartNoZoom = new(e.X / Images.OriginalImageZoom, e.Y / Images.OriginalImageZoom);
                Selector.DefineStart(selectionStartNoZoom, ImageType.OriginalImage);
                ChangeSelectionOnOriginalImage(selectionStartNoZoom);
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
                Point selectionLocationNoZoom = new(e.X / Images.OriginalImageZoom, e.Y / Images.OriginalImageZoom);
                ChangeSelectionOnOriginalImage(selectionLocationNoZoom);
            }
        }
        #endregion

        #region Image Selection and Copy/Paste
        /// <summary>
        /// Copies the current selection.
        /// </summary>
        private void CopyButton_Click(object sender, EventArgs e)
        {
            CopyImage();
        }

        /// <summary>
        /// Pastes the previously copied image to the current selection.
        /// </summary>
        private void PasteButton_Click(object sender, EventArgs e)
        {
            PasteImage();
        }

        /// <summary>
        /// Copies the selected portion of the image to the clipboard image of its corresponding image type.
        /// </summary>
        private void CopyImage()
        {
            Images.CopySelectionFromImage(Selector.SelectedArea, Selector.CurrentImageType);
        }

        /// <summary>
        /// Pastes the clipboard image into its corresponding image box.
        /// Then refreshes the images for both Drawing and Viewing Box to keep them in sync.
        /// </summary>
        private void PasteImage()
        {
            Images.PasteSelectionOnImage(Selector.SelectedArea, Selector.CurrentImageType);

            ViewingBox.SetNewImage(Images.DisplayOriginalImage);
            DrawingBox.SetNewImage(Images.DisplayDrawingImage);
        }

        /// <summary>
        /// Gets the dimensions to use for the selection in the ViewingBox.
        /// </summary>
        /// <returns>A tuple of int values, which represent the width and height to use for the Viewing Box selection.</returns>
        private (int, int) GetViewingSelectionDimensions()
        {
            if (SelectionSizeComboBox.SelectedIndex == 4)
            {
                return (Images.DrawingImageSize.Width, Images.DrawingImageSize.Height);
            }
            else
            {
                int value = int.Parse(SelectionSizeComboBox.SelectedItem.ToString() ?? "1");
                return (value, value);
            }
        }

        /// <summary>
        /// Changes the selection of the Original Image, and then calls a redraw for the boxes.
        /// Redraws the Viewing Box to show the selection, and redraws the Drawing Box to remove any selection, if there was one in it.
        /// </summary>
        /// <param name="location">The current location of the mouse cursor.</param>
        private void ChangeSelectionOnOriginalImage(Point location)
        {
            (int width, int height) = GetViewingSelectionDimensions();
            Selector.ChangeSelectionArea(location, ViewingBox.Size, width, height);
            ViewingBox.Invalidate();
        }

        /// <summary>
        /// Changes the selection of the Drawing Image, and then calls a redraw for the boxes.
        /// Redraws the Drawing Box to show the selection, and redraws the Viewing Box to remove any selection, if there was one in it.
        /// </summary>
        /// <param name="location">The current location of the mouse cursor.</param>
        private void ChangeSelectionOnDrawingImage(Point location)
        {
            Selector.ChangeSelectionArea(location, DrawingBox.Size);
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
                        SwapColorInImage(cell.BackColor, ColorPickerDialog.Color, cell.ChangeCellColor);
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
                        Images.CreateNewDisplayOriginalImage();
                        ViewingBox.SetNewImage(Images.DisplayOriginalImage);
                        Images.CreateImageToDraw();
                        DrawingBox.SetNewImage(Images.DisplayDrawingImage);
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
        private void SwapColorInImage(Color oldColor, Color newColor, Action<Color> changeCellColor)
        {
            Color backgroundColor = BackgroundColorTable.GetCurrentColor();

            // If the image has a transparent background, temportarily remove the transparency.
            if (TransparencyCheckBox.Checked)
            {
                Images.MakeImageNotTransparent(backgroundColor);
            }

            ReplaceColorTool tool = new();
            ImageToolParameters imageParameters = new()
            {
                OldColor = oldColor,
                NewColor = newColor
            };

            UndoParameters undoParameters = new()
            {
                ChangeCellColor = changeCellColor,
                OldColor = oldColor,
                NewColor = newColor
            };

            tool.UseTool(Images.EditOriginalImage, imageParameters);
            UndoHandler.TrackChange(tool.CreateUndoStep(undoParameters));
            SetUndoRedoButtonAvailability();

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

            ViewingBox.SetNewImage(Images.DisplayOriginalImage);
            Images.CreateImageToDraw();
            DrawingBox.SetNewImage(Images.DisplayDrawingImage);
        }

        #region Saving and Loading Files
        /// <summary>
        /// The method that saves the image to a file.
        /// </summary>
        private void SaveImageButton_Click(object sender, EventArgs e)
        {
            FileSaverLoader.SaveImage(Images.EditOriginalImage, Images.OriginalImageSize);
        }

        /// <summary>
        /// The methods that loads an image from a file into the program.
        /// </summary>
        private void LoadImageButton_Click(object sender, EventArgs e)
        {
            using Bitmap? imageLoaded = FileSaverLoader.LoadImage();
            if (imageLoaded is not null) // Null check in case no image is loaded.
            {
                int currentPixelSize = (int)ViewPixelSizeNumberBox.Value;
                ViewPixelSizeNumberBox.Value = 1;

                Images.ReplaceOriginalImage(imageLoaded);

                ViewPixelSizeNumberBox.Value = currentPixelSize;
            }

            ViewingBox.SetNewImage(Images.DisplayOriginalImage);
            Images.CreateImageToDraw();
            DrawingBox.SetNewImage(Images.DisplayDrawingImage);
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
                        Images.CopySelectionFromImage(Selector.SelectedArea, Selector.CurrentImageType);
                        break;
                    case Keys.V: // Control V: Paste.
                        PasteImage();
                        break;
                    case Keys.Z: // Control Z: Undo
                        UndoAction();
                        break;
                    case Keys.Y: // Control Y: Redo
                        RedoAction();
                        break;
                }
            }
        }

        private void UndoButton_Click(object sender, EventArgs e)
        {
            UndoAction();
        }

        private void UndoAction()
        {
            UndoHandler.UndoChange(Images.EditOriginalImage);

            SetUndoRedoButtonAvailability();

            Images.CreateImageToDraw();
            DrawingBox.SetNewImage(Images.DisplayDrawingImage);
            Images.CreateNewDisplayOriginalImage();
            ViewingBox.SetNewImage(Images.DisplayOriginalImage);
        }

        private void RedoButton_Click(object sender, EventArgs e)
        {
            RedoAction();
        }

        private void RedoAction()
        {
            UndoHandler.RedoChange(Images.EditOriginalImage);

            SetUndoRedoButtonAvailability();

            Images.CreateImageToDraw();
            DrawingBox.SetNewImage(Images.DisplayDrawingImage);
            Images.CreateNewDisplayOriginalImage();
            ViewingBox.SetNewImage(Images.DisplayOriginalImage);
        }

        private void SetUndoRedoButtonAvailability()
        {
            (bool undoAvailable, bool redoAvailable) = UndoHandler.UndoRedoAvailable();
            UndoButton.Enabled = undoAvailable;
            RedoButton.Enabled = redoAvailable;
        }
    }
}