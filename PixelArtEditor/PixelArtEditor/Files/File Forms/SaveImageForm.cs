using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace PixelArtEditor.Files.File_Forms
{
    /// <summary>
    /// A form used to save images to a file.
    /// It allows changing the size of the image and picking the directory to save it to.
    /// </summary>
    public partial class SaveImageForm : Form
    {
        #region Properties
        /// <summary>
        /// The width of the original image that's being saved.
        /// </summary>
        private int OriginalImageWidth { get; set; }

        /// <summary>
        /// The height of the original image that's being saved.
        /// </summary>
        private int OriginalImageHeight { get; set; }

        /// <summary>
        /// The Save File Dialog used to save the image.
        /// </summary>
        private SaveFileDialog DialogForSavingImages { get; set; }
        #endregion

        /// <summary>
        /// Defalt constructor. It receives the image to be saved and a savefiledialog.
        /// It also receives the original size of the image, to show it without zoom.
        /// </summary>
        /// <param name="originalImage">The original image that will be saved.</param>
        /// <param name="originalSize">The size of the original image, without the zoom applied.</param>
        /// <param name="dialogForSavingImages">The dialog that will be used for saving the image, which already has its properties set.</param>
        public SaveImageForm(Bitmap originalImage, Size originalSize, SaveFileDialog dialogForSavingImages)
        {
            InitializeComponent();

            DialogForSavingImages = dialogForSavingImages;
            SaveImagePictureBox.Image = new Bitmap(originalImage);
            OriginalImageWidth = originalSize.Width;
            OriginalImageHeight = originalSize.Height;

            ValidateZoomAllowed();
            ZoomImage();
        }

        #region Zoom Related Methods
        /// <summary>
        /// Validates the amount of zoom that can be allowed depending on the original image size.
        /// This prevents the zoomed image from going above the maximum storage space allowed for a Bitmap object.
        /// </summary>
        private void ValidateZoomAllowed()
        {
            const int maximumBitmapPixelAmount = 536870912; // Maximum pixel amount a Bitmap object can hold.
            int totalImagePixelAmount = OriginalImageHeight * OriginalImageWidth; // The amount of pixels in the original image.

            // Formula to find the maximum allowed zoom.
            int maximumAllowedZoom = (int)Math.Sqrt(maximumBitmapPixelAmount / totalImagePixelAmount);

            if (maximumAllowedZoom > 64)
            {
                maximumAllowedZoom = 64;
            }

            SaveImageZoomNumberBox.Maximum = SaveImageZoomNumberBar.MaximumValue = maximumAllowedZoom;
        }

        /// <summary>
        /// Takes the size values, applies zoom to the image then displays the image and the zoomed size values.
        /// </summary>
        private void ZoomImage()
        {
            // Gets the zoomed size values to apply.
            int zoom = (int)SaveImageZoomNumberBox.Value;
            int zoomWidth = OriginalImageWidth * zoom;
            int zoomHeight = OriginalImageHeight * zoom;

            // Appied zoom to the image.
            SaveImagePictureBox.Image = ApplyZoom((Bitmap)SaveImagePictureBox.Image, zoomWidth, zoomHeight);

            // Displays image in picturebox and size values in labels.
            SaveImagePictureBox.Size = SaveImagePictureBox.Image.Size;
            SaveImageBackgroundPanel.ResizePanelToFitControls();
            SaveImageWidthLabel.Text = $"Width: {zoomWidth} ({OriginalImageWidth})";
            SaveImageHeightLabel.Text = $"Height: {zoomHeight} ({OriginalImageHeight})";
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

        #region NumberBox / NumberBar events
        /// <summary>
        /// Syncs the value of the NumberBox with the NumberBar, then zooms the image.
        /// </summary>
        private void FullImageZoomNumberBox_ValueChanged(object sender, EventArgs e)
        {
            SaveImageZoomNumberBar.Value = (int)SaveImageZoomNumberBox.Value;
            ZoomImage();
        }

        /// <summary>
        /// Syncs the value of the NumberBox with the NumberBar.
        /// </summary>
        private void FullImageZoomNumberBar_ValueChanged(object sender, EventArgs e)
        {
            SaveImageZoomNumberBox.Value = SaveImageZoomNumberBar.Value;
        }
        #endregion

        #region Save / Close Buttons
        /// <summary>
        /// Calls the method to save the image.
        /// </summary>
        private void SaveFullImageButton_Click(object sender, EventArgs e)
        {
            SaveFullImage();
        }

        /// <summary>
        /// Sets the filename for the image and opens the SaveFileDialog to save it.
        /// </summary>
        private void SaveFullImage()
        {
            DialogForSavingImages.FileName = "PixelImage_" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-ff") + ".png";
            DialogResult result = DialogForSavingImages.ShowDialog();

            if (result == DialogResult.OK && DialogForSavingImages.FileName != string.Empty)
            {
                using FileStream imageStream = (FileStream)DialogForSavingImages.OpenFile();
                SaveImagePictureBox.Image.Save(imageStream, ImageFormat.Png);
            }
        }

        /// <summary>
        /// Simply closes the form.
        /// </summary>
        private void CancelSaveButton_Click(object sender, EventArgs e)
        {
            Close();
        }


        #endregion
    }
}
