using PixelArtEditor.Controls;
using PixelArtEditor.Files;
using PixelArtEditor.Grids;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace PixelArtEditor
{
    public partial class PixelArtEditorForm : Form
    {
        // The original image that is being drawn on in the editor
        private Bitmap originalImage = new(1, 1);

        private FileSaveLoadHandler FileSaverLoader = new();

        private Bitmap clipboardImage = new(1, 1);
        private Point selectionStart = new();
        private Rectangle selectedArea = new();
        private readonly SolidBrush selectionBrush = new(Color.FromArgb(128, 32, 196, 255));

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
            GridColorTable.GenerateColorGrid(1, new EventHandler(ColorCellClicked!), Color.Gray);
            BackgroundColorTable.GenerateColorGrid(1, new EventHandler(ColorCellClicked!), Color.FromArgb(254, 254, 254));

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
            int colorAmount = int.Parse(ColorAmountComboBox.SelectedItem.ToString()!);
            PaletteColorTable.GenerateColorGrid(colorAmount, new EventHandler(ColorCellClicked!));
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
            using Graphics imageFiller = Graphics.FromImage(originalImage);
            imageFiller.Clear(backgroundColor);

            // Changes transparency
            if (TransparencyCheckBox.Checked)
            {
                originalImage.MakeTransparent(backgroundColor);
            }

            ViewingAreaDrawingBox.SetNewSize(width * zoom, height * zoom);
            ViewingAreaDrawingBox.SetNewImage(gridGenerator, originalImage, zoom, gridColor, backgroundColor);
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
                selectedArea = Rectangle.Empty;
                ViewingAreaDrawingBox.DrawPixelByClick(DefineGridType(), originalImage, mouseClick.X, mouseClick.Y, (int)ViewingZoomNumberBox.Value, PaletteColorTable.GetCurrentColor(), (GridType)GridTypeComboBox.SelectedItem);
                ViewingAreaDrawingBox.Refresh();
            }
        }

        private void ColorCellClicked(object sender, EventArgs e)
        {
            MouseEventArgs mouseClick = (MouseEventArgs)e;
            RectangleCell? cell = sender as RectangleCell;
            ColorTable? cellParent = cell!.Parent as ColorTable;

            if (mouseClick.Button == MouseButtons.Right)
            {
                DialogResult colorpicked = ColorPickerDialog.ShowDialog();

                if (colorpicked == DialogResult.OK)
                {
                    if (!cell.DefaultColor && ColorChangeCheckBox.Checked)
                    {
                        SwapColorInImage(cell.BackColor, ColorPickerDialog.Color);
                    }
                    if (cellParent!.Name == "tbl_BackgroundColor")
                    {
                        ChangeImageTransparency(TransparencyCheckBox.Checked, BackgroundColorTable.GetCurrentColor(), ColorPickerDialog.Color);
                    }

                    cell.ChangeCellColor(ColorPickerDialog.Color);
                }
            }

            cellParent!.ChangeCurrentCell(cell);
        }

        private void SwapColorInImage(Color oldColor, Color newColor)
        {
            int height = (int)PixelHeightNumberBox.Value;
            int width = (int)PixelWidthNumberBox.Value;
            int zoom = (int)ViewingZoomNumberBox.Value;
            GridType gridType = (GridType)GridTypeComboBox.SelectedItem;
            Color pixelColor;

            Bitmap image = new Bitmap(originalImage);

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
                selectedArea = Rectangle.Empty;
                ViewingAreaDrawingBox.DrawPixelByClick(DefineGridType(), originalImage, e.X, e.Y, (int)ViewingZoomNumberBox.Value, PaletteColorTable.GetCurrentColor(), (GridType)GridTypeComboBox.SelectedItem);
                ViewingAreaDrawingBox.Refresh();
            }

            if (e.Button == MouseButtons.Right)
            {
                ChangeRectangleSelection(e.Location);
            }
        }

        private void SaveImageButton_Click(object sender, EventArgs e)
        {
            FileSaverLoader.SaveImage(originalImage);
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
                Color gridColor = GridColorTable.GetCurrentColor();
                Color backgroundColor = BackgroundColorTable.GetCurrentColor();
                Bitmap temporaryImage = new Bitmap(width * zoom, height * zoom);
                IGridGenerator generator = DefineGridType();

                Graphics temporaryGraphics = Graphics.FromImage(temporaryImage);

                temporaryGraphics.Clear(newColor);
                temporaryGraphics.DrawImage(originalImage, 0, 0);
                originalImage = temporaryImage;

                ViewingAreaDrawingBox.SetNewImage(generator, originalImage, zoom, gridColor, backgroundColor);
            }
        }

        /// <summary>
        /// Method called when a change is made to the Gridtype ComboBox.
        /// It changes the implementation of grids used to the newly selected grid type, then applies the new grid type to the image in the drawing area.
        /// </summary>
        private void GridTypeComboBox_SelectedIndexChanged_ApplyGridToImage(object sender, EventArgs e)
        {
            IGridGenerator gridApply = DefineGridType();
            Color backgroundColor = BackgroundColorTable.GetCurrentColor();
            ViewingAreaDrawingBox.ApplyNewGrid(gridApply, originalImage, backgroundColor);
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
                FileSaverLoader.LoadImage(ref originalImage);

                (_, _, int zoom) = GetImageSizeValues();

                if (ResizeOnLoadCheckBox.Checked)
                {
                    ViewingAreaDrawingBox.SetNewSize(originalImage.Width, originalImage.Height);
                }
                else
                {
                    using Bitmap newOriginalImage = new(ViewingAreaDrawingBox.Width, ViewingAreaDrawingBox.Height);
                    using Graphics newImageGraphics = Graphics.FromImage(newOriginalImage);
                    newImageGraphics.DrawImage(originalImage, 0, 0);

                    originalImage = new(newOriginalImage);
                }

                IGridGenerator generator = DefineGridType();
                Color gridColor = GridColorTable.GetCurrentColor();
                Color backgroundColor = BackgroundColorTable.GetCurrentColor();
                ViewingAreaDrawingBox.SetNewImage(generator, originalImage, zoom, gridColor, backgroundColor);

                ReorganizeControls();
            }
        }

        private void ViewingAreaDrawingBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                selectionStart.X = e.Location.X;
                selectionStart.Y = e.Location.Y;

                ChangeRectangleSelection(e.Location);
            }
        }

        private void CopyButton_Click(object sender, EventArgs e)
        {
            if (selectedArea != Rectangle.Empty)
            {
                clipboardImage = originalImage.Clone(selectedArea, PixelFormat.Format32bppArgb);
            }
        }

        private void PasteButton_Click(object sender, EventArgs e)
        {
            if (selectedArea != Rectangle.Empty)
            {
                using Graphics pasteGraphics = Graphics.FromImage(originalImage);
                pasteGraphics.DrawImage(clipboardImage, new Point(selectedArea.X, selectedArea.Y));

                IGridGenerator generator = DefineGridType();
                Color gridColor = GridColorTable.GetCurrentColor();
                Color backgroundColor = BackgroundColorTable.GetCurrentColor();
                ViewingAreaDrawingBox.SetNewImage(generator, originalImage, (int)ViewingZoomNumberBox.Value, gridColor, backgroundColor);
            }
        }

        private void ViewingAreaDrawingBox_Paint(object sender, PaintEventArgs e)
        {
            if (selectedArea != Rectangle.Empty)
            {
                e.Graphics.FillRectangle(selectionBrush, selectedArea);
            }
        }

        private void ChangeRectangleSelection(Point location)
        {
            int zoom = (int)ViewingZoomNumberBox.Value;
            Point selectionEnd = new()
            {
                X = location.X,
                Y = location.Y
            };

            if (selectionEnd.X >= ViewingAreaDrawingBox.Width)
            {
                selectionEnd.X = ViewingAreaDrawingBox.Width - 1;
            }
            if (selectionEnd.Y >= ViewingAreaDrawingBox.Height)
            {
                selectionEnd.Y = ViewingAreaDrawingBox.Height - 1;
            }

            if (selectionEnd.X < 0)
            {
                selectionEnd.X = 0;
            }
            if (selectionEnd.Y < 0)
            {
                selectionEnd.Y = 0;
            }

            if (selectionEnd.X < selectionStart.X)
            {
                selectedArea.X = selectionEnd.X - selectionEnd.X % zoom;
                selectionEnd.X = selectionStart.X - selectionStart.X % zoom + zoom;
            }
            else
            {
                selectedArea.X = selectionStart.X - selectionStart.X % zoom;
                selectionEnd.X = selectionEnd.X - selectionEnd.X % zoom + zoom;
            }

            selectedArea.Y = selectionStart.Y;
            if (selectionEnd.Y < selectionStart.Y)
            {
                selectedArea.Y = selectionEnd.Y - selectionEnd.Y % zoom;
                selectionEnd.Y = selectionStart.Y - selectionStart.Y % zoom + zoom;
            }
            else
            {
                selectedArea.Y = selectionStart.Y - selectionStart.Y % zoom;
                selectionEnd.Y = selectionEnd.Y - selectionEnd.Y % zoom + zoom;
            }

            selectedArea.Width = selectionEnd.X - selectedArea.X;
            selectedArea.Height = selectionEnd.Y - selectedArea.Y;

            ViewingAreaDrawingBox.Invalidate();
        }

        private void ViewingZoomNumberBox_ValueChanged(object sender, EventArgs e)
        {
            (int width, int height, int zoom) = GetImageSizeValues();

            Bitmap zoomedImage = new Bitmap(width * zoom, height * zoom);
            Graphics zoomGraphics = Graphics.FromImage(zoomedImage);
            zoomGraphics.SmoothingMode = SmoothingMode.HighQuality;
            zoomGraphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            zoomGraphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            zoomGraphics.DrawImage(originalImage, 0, 0, zoomedImage.Width, zoomedImage.Height);

            originalImage = new(zoomedImage);

            IGridGenerator generator = DefineGridType();
            Color gridColor = GridColorTable.GetCurrentColor();
            Color backgroundColor = BackgroundColorTable.GetCurrentColor();

            ViewingAreaDrawingBox.SetNewSize(zoomedImage.Width, zoomedImage.Height);
            ViewingAreaDrawingBox.SetNewImage(generator, zoomedImage, zoom, gridColor, backgroundColor);

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