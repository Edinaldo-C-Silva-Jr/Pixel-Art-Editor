using PixelArtEditor.Extension_Methods;
using PixelArtEditor.Grids;
using System.Drawing.Drawing2D;

namespace PixelArtEditor.Files.File_Forms
{
    /// <summary>
    /// A form to load images from a file into the editor.
    /// It allows picking an image from a file, as well as shrinking or enlarging it to fit into the pixel grid.
    /// </summary>
    public partial class LoadImageForm : Form
    {
        #region Loading Window Properties
        /// <summary>
        /// The type of zoom currently selected to zoom the image.
        /// </summary>
        private ZoomType CurrentZoomType { get; set; } = ZoomType.None;

        /// <summary>
        /// The type of zoom applied last time the image was zoomed.
        /// </summary>
        private ZoomType AppliedZoomType { get; set; }

        /// <summary>
        /// The amount of zoom that has been applied to the image.
        /// </summary>
        private int AppliedZoom { get; set; }

        /// <summary>
        /// Whether the editor will do a zoom preview or not.
        /// The zoom preview is used while changing the zoom, but before clicking the Accept button.
        /// </summary>
        private bool PreviewZoom { get; set; }

        /// <summary>
        /// The size of the image as it is when loading from a file.
        /// </summary>
        private Size InitialImageSize { get; set; }

        /// <summary>
        /// The size of the image after applying zoom. This is the size it will have in the editor.
        /// </summary>
        private Size ImageLoadedSize { get; set; }

        /// <summary>
        /// Defines if the image has a valid size to be loaded into the program.
        /// The maximum allowed size is 1024 x 1024.
        /// </summary>
        private bool ImageIsValid { get; set; } = false;
        #endregion

        #region Load Properties
        /// <summary>
        /// The dialog used to load the images from files.
        /// </summary>
        private OpenFileDialog OpenDialog { get; set; }

        /// <summary>
        /// The factory that generates the grid applied to the original image..
        /// This grid shows how much of the image will correspond to a single pixel in the editor.
        /// </summary>
        private GridGeneratorFactory ZoomedGrid { get; set; }

        /// <summary>
        /// Defines if the editor will resize the ViewingBox after loading, in order to completely fit the loaded image.
        /// </summary>
        public bool ResizeAfterLoad { get; set; } = false;

        /// <summary>
        /// The image to load, after applying the appropriate zoom, which will be returned to the editor.
        /// </summary>
        public Bitmap? ImageLoaded { get; set; }
        #endregion

        #region Form Load and Initial Setup / Disposing
        /// <summary>
        /// Default constructor. Does the initial setup for all the controls necessary.
        /// </summary>
        /// <param name="dialogForOpeningImages">The dialog for loading images from files, which already has all its properties set.</param>
        public LoadImageForm(OpenFileDialog dialogForOpeningImages)
        {
            InitializeComponent();

            OpenDialog = dialogForOpeningImages;
            ZoomedGrid = new();
            Disposed += OnDispose;

            InitialControlSetup();
        }

        /// <summary>
        /// Does the single time control setup for when the form opens.
        /// </summary>
        private void InitialControlSetup()
        {
            // Sets the size for all picture boxes and panels so that they're initially invisible.
            LoadImagePictureBox.Size = new(0, 0);
            LoadImageBackgroundPanel.Size = new(0, 0);
            LoadImageZoomedPictureBox.Size = new(0, 0);
            LoadImageZoomedBackgroundPanel.Size = new(0, 0);
            LoadImageZoomPanel.Location = new(170, 0);

            // Hides and disables all controls that shouldn't be visible at first.
            ImageLoadedOriginalSizeLabel.Visible = false;
            LoadImageZoomPanel.Visible = false;
            LoadImageShrinkImageButton.Visible = false;
            LoadImageEnlargeImageButton.Visible = false;
            LoadedImageZoomedSizeLabel.Visible = false;
            ConfirmLoadButton.Enabled = false;
            ResizeAfterLoadCheckBox.Checked = false;
        }

        /// <summary>
        /// Releases unmanaged resources.
        /// </summary>
        private void OnDispose(object? sender, EventArgs e)
        {
            ZoomedGrid?.Dispose();
            ImageLoaded?.Dispose();
        }
        #endregion

        #region Open Image / Confirm and Cancel Load / Resize CheckBox.
        /// <summary>
        /// Utilizes the OpenFileDialog to load an image from a file into the PictureBox.
        /// Then checks if the image size is valid and loads the size values into the labels.
        /// </summary>
        private void OpenImageButton_Click(object sender, EventArgs e)
        {
            DialogResult result = OpenDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                DisableZoomPanel();

                // Loads the image.
                LoadImagePictureBox.Image?.Dispose();
                LoadImagePictureBox.Image = new Bitmap(OpenDialog.FileName);
                LoadImagePictureBox.Size = LoadImagePictureBox.Image.Size;
                LoadImageBackgroundPanel.ResizePanelToFitControls();
                ImageLoadedOriginalSizeLabel.Visible = true;

                AppliedZoom = 1;
                AppliedZoomType = ZoomType.None;
                ImageLoadedSize = InitialImageSize = LoadImagePictureBox.Image.Size;
                LoadImageWidthLabel.Text = $"Width: {InitialImageSize.Width}";
                LoadImageHeightLabel.Text = $"Height: {InitialImageSize.Height}";

                ValidateImageDimensions();
                ShowImageIfValid();
            }
        }

        /// <summary>
        /// Confirms loading the image, setting the DialogResilt to OK and applying the image into the ImageLoaded property.
        /// </summary>
        private void ConfirmLoadButton_Click(object sender, EventArgs e)
        {
            if (ImageIsValid)
            {
                ImageLoaded = ApplyZoom((Bitmap)LoadImagePictureBox.Image, ImageLoadedSize.Width, ImageLoadedSize.Height);
            }
            else
            {
                ImageLoaded = null;
            }

            DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// Cancels loading the image, setting the DialogResult to Cancel.
        /// </summary>
        private void CancelLoadButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        /// <summary>
        /// Defines whether the ViewingBox will be resized to fit the image after loading.
        /// </summary>
        private void ResizeAfterLoadCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ResizeAfterLoad = ResizeAfterLoadCheckBox.Checked;
        }
        #endregion

        #region Enlarge / Shrink Buttons and Setup
        /// <summary>
        /// Enables the zoom panel and sets it to shrink the image.
        /// </summary>
        private void LoadImageShrinkImageButton_Click(object sender, EventArgs e)
        {
            EnableZoomPanel("How much to shrink the image?");
            LoadImageZoomNumberBox.Maximum = LoadImageZoomNumberBar.MaximumValue = CalculateMaximumShrink();
            CurrentZoomType = ZoomType.Shrink;
        }

        /// <summary>
        /// Enables the zoom panel and sets it to enlarge the image.
        /// </summary>
        private void LoadImageEnlargeImageButton_Click(object sender, EventArgs e)
        {
            EnableZoomPanel("How much to enlarge the image?");
            LoadImageZoomNumberBox.Maximum = LoadImageZoomNumberBar.MaximumValue = CalculateMaximumEnlarge();
            CurrentZoomType = ZoomType.Enlarge;
        }

        /// <summary>
        /// Calculates how much the image can be reduced based on its size.
        /// The minimum image size allowed by the editor is 5 pixels wide or tall.
        /// </summary>
        /// <returns>The amount of times the image can be reduced.</returns>
        private int CalculateMaximumShrink()
        {
            int imageWidth = InitialImageSize.Width;
            int imageHeight = InitialImageSize.Height;

            // Gets the smaller dimension of the image, since the calculation needs to be done on the smaller dimension only.
            int smallerDimension = imageWidth < imageHeight ? imageWidth : imageHeight;

            if (smallerDimension >= 320) // If both image dimensions are equal to or bigger than 320...
            {
                return 64; // Then the reduce zoom can be set to 64x.
            } 
            else
            {
                // Set the reduce zoom so that the smaller dimension can't be less than 5.
                // Also make sure it isn't lower than 1.
                return (smallerDimension / 5).ValidateMinimum(1);
            }
        }

        /// <summary>
        /// Calculates how much the image can be enlarged based on its size.
        /// The maximum image size allowed by the editor is 1024 pixels wide or tall.
        /// </summary>
        /// <returns>The amount of times the image can be enlarged.</returns>
        private int CalculateMaximumEnlarge()
        {
            int imageWidth = InitialImageSize.Width;
            int imageHeight = InitialImageSize.Height;

            // Gets the bigger dimension of the image, since the calculations only needs to be done on the bigger dimension.
            int biggerDimension = imageWidth > imageHeight ? imageWidth : imageHeight;

            if (biggerDimension > 512) // If the image has any dimension bigger than 512...
            {
                return 1; // Then it cannot be enlarged without going over 1024.
            }
            else // Otherwise...
            {
                // Set the maximum zoom so that the bigger dimension won't be larger than 1024.
                // Also make sure it isn't higher than 99, so it doesn't break the NumberBar.
                return (1024 / biggerDimension).ValidateMaximum(99);
            }
        }

        /// <summary>
        /// Shows the zoom panel, sets up the zoom and enables Zoom Preview.
        /// </summary>
        /// <param name="labelText">The text to show in the zoom panel, which differs for shrink and enlarge.</param>
        private void EnableZoomPanel(string labelText)
        {
            LoadImageShrinkImageButton.Visible = false;
            LoadImageEnlargeImageButton.Visible = false;
            LoadImageZoomPanel.Visible = true;
            LoadImageZoomedLabel.Text = labelText;
            LoadImageZoomNumberBox.Value = 1;
            ConfirmLoadButton.Enabled = false;
            PreviewZoom = true;
        }
        #endregion

        #region Zoom Panel Buttons / Zoom Calculations, Validation and Application
        /// <summary>
        /// Accepts the current zoom, disables the zoom panel and shows the image.
        /// If the image was reduced, also generates a grid to show how much of the image corresponds to a pixel in the editor.
        /// </summary>
        private void LoadImageAcceptZoomButton_Click(object sender, EventArgs e)
        {
            DisableZoomPanel();

            AcceptZoomAndImageDimensions();
            ShowImageIfValid();

            if (AppliedZoomType == ZoomType.Shrink) // If the image was reduced, generates a grid.
            {
                ZoomedGrid.ChangeCurrentGrid(GridType.Line, InitialImageSize.Width, InitialImageSize.Height, AppliedZoom, Color.Gray);
            }
            else // Otherwise, sets the grid to None.
            {
                ZoomedGrid.ChangeCurrentGrid(GridType.None, 0, 0, 0, Color.Gray);
            }
            LoadImagePictureBox.Invalidate();
        }

        /// <summary>
        /// Simply disables the zoom panel and re-validates the previously applied zoom.
        /// </summary>
        private void LoadImageCancelZoomButton_Click(object sender, EventArgs e)
        {
            DisableZoomPanel();
            ValidateImageDimensions();
        }

        /// <summary>
        /// Hides the zoom panels and disables the Zoom Preview.
        /// </summary>
        private void DisableZoomPanel()
        {
            LoadImageZoomPanel.Visible = false;
            LoadImageShrinkImageButton.Visible = true;
            LoadImageEnlargeImageButton.Visible = true;
            PreviewZoom = false;
        }

        /// <summary>
        /// Accepts the current zoom value and type, saving them as applied. 
        /// Then calculates and validates the pixel dimensions to see if the image has a valid size (between 5 and 1024 pixels in each side).
        /// </summary>
        private void AcceptZoomAndImageDimensions()
        {
            AppliedZoom = (int)LoadImageZoomNumberBox.Value;
            AppliedZoomType = CurrentZoomType;
            ImageLoadedSize = CalculateImageDimensions(AppliedZoom, AppliedZoomType);

            ValidateImageDimensions();
        }

        /// <summary>
        /// Checks if the current dimensions of the loaded image are valid for the editor.
        /// If they are, set the label colors to green and enabled the confirm button. Otherwise, set the label color to red and disables the button.
        /// </summary>
        private void ValidateImageDimensions()
        {
            bool valid = true;
            if (ImageLoadedSize.Width > 1024 || ImageLoadedSize.Width < 5)
            {
                LoadPixelWidthLabel.ForeColor = Color.Red;
                valid = false;
            }
            else
            {
                LoadPixelWidthLabel.ForeColor = Color.Green;
            }

            if (ImageLoadedSize.Height > 1024 || ImageLoadedSize.Width < 5)
            {
                LoadPixelHeightLabel.ForeColor = Color.Red;
                valid = false;
            }
            else
            {
                LoadPixelHeightLabel.ForeColor = Color.Green;
            }

            LoadPixelWidthLabel.Text = $"Pixel Width: {ImageLoadedSize.Width}";
            LoadPixelHeightLabel.Text = $"Pixel Height: {ImageLoadedSize.Height}";
            ImageIsValid = valid;
            ConfirmLoadButton.Enabled = valid;
        }

        /// <summary>
        /// Calculates the image dimensions with the passed zoom value and zoom type.
        /// </summary>
        /// <param name="currentZoom">The zoom value to use for the calculations.</param>
        /// <param name="currentZoomType">The zoom type to use for the calculations.</param>
        /// <returns>The size values of the image.</returns>
        private Size CalculateImageDimensions(int currentZoom, ZoomType currentZoomType)
        {
            int pixelWidth = currentZoomType switch
            {
                ZoomType.Shrink => InitialImageSize.Width / currentZoom,
                ZoomType.Enlarge => InitialImageSize.Width * currentZoom,
                _ => InitialImageSize.Width
            };

            int pixelHeight = currentZoomType switch
            {
                ZoomType.Shrink => InitialImageSize.Height / currentZoom,
                ZoomType.Enlarge => InitialImageSize.Height * currentZoom,
                _ => InitialImageSize.Height
            };

            return new Size(pixelWidth, pixelHeight);
        }

        /// <summary>
        /// Applies the grid to the PictureBox.
        /// </summary>
        private void LoadImagePictureBox_Paint(object sender, PaintEventArgs e)
        {
            ZoomedGrid.GetGrid().ApplyGrid(e.Graphics, InitialImageSize.Width, InitialImageSize.Height);
        }

        /// <summary>
        /// Shows the image in the Zoomed Image picture box, but only if the image size is valid.
        /// </summary>
        private void ShowImageIfValid()
        {
            LoadImageZoomedBackgroundPanel.Visible = ImageIsValid;
            LoadedImageZoomedSizeLabel.Visible = ImageIsValid;

            if (ImageIsValid)
            {
                LoadImageZoomedPictureBox.Image = ApplyZoom((Bitmap)LoadImagePictureBox.Image, ImageLoadedSize.Width, ImageLoadedSize.Height);
            }
            else
            {
                LoadImageZoomedPictureBox.Image = new Bitmap(1, 1);
            }

            SuspendLayout();

            LoadImageZoomedPictureBox.Size = LoadImageZoomedPictureBox.Image.Size;
            LoadImageZoomedBackgroundPanel.ResizePanelToFitControls();

            ResumeLayout();
        }

        /// <summary>
        /// Applies the zoom to an image.
        /// </summary>
        /// <param name="originalImage">The image to apply the zoom to.</param>
        /// <param name="zoomWidth">The width of the newly zoomed image.</param>
        /// <param name="zoomHeight">The height of the newly zoomed image.</param>
        /// <returns>A new image with the zoom applied.</returns>
        private static Bitmap ApplyZoom(Bitmap originalImage, int zoomWidth, int zoomHeight)
        {
            // Creates an image with the newly desired size.
            Bitmap zoomedImage = new(zoomWidth, zoomHeight);
            using Graphics zoomGraphics = Graphics.FromImage(zoomedImage);

            // Sets the Interpolation mode and Pixel Offset to use for the editor. Uses Nearest Neighbor to preserve the pixels.
            zoomGraphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            zoomGraphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

            // Draws the original image onto the zoomed image, using the new size and interpolation mode defined.
            zoomGraphics.DrawImage(originalImage, 0, 0, zoomWidth, zoomHeight);
            return zoomedImage;
        }
        #endregion

        #region NumberBox and NumberBar events / Zoom Preview
        /// <summary>
        /// Syncs the value of the NumberBox with the NumberBar. Also previews the zoom if the Zoom Preview is enabled.
        /// </summary>
        private void LoadImageZoomNumberBox_ValueChanged(object sender, EventArgs e)
        {
            LoadImageZoomNumberBar.Value = (int)LoadImageZoomNumberBox.Value;
            PreviewZoomedImageDimensions();
        }

        /// <summary>
        /// Syncs the value of the NumberBox with the NumberBar.
        /// </summary>
        private void LoadImageZoomNumberBar_ValueChanged(object sender, EventArgs e)
        {
            LoadImageZoomNumberBox.Value = LoadImageZoomNumberBar.Value;
        }

        /// <summary>
        /// Previews the image dimensions with the currently selected zoom and displays it in the dimension labels, with a black text color.
        /// </summary>
        private void PreviewZoomedImageDimensions()
        {
            if (PreviewZoom)
            {
                Size pixelDimensions = CalculateImageDimensions((int)LoadImageZoomNumberBox.Value, CurrentZoomType);

                LoadPixelWidthLabel.ForeColor = Color.Black;
                LoadPixelHeightLabel.ForeColor = Color.Black;
                LoadPixelWidthLabel.Text = $"Pixel Width: {pixelDimensions.Width}";
                LoadPixelHeightLabel.Text = $"Pixel Height: {pixelDimensions.Height}";
            }
        }
        #endregion
    }
}
