using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace PixelArtEditor.Files.File_Forms
{
    public partial class SaveImageForm : Form
    {
        private int OriginalImageWidth { get; set; }
        private int OriginalImageHeight { get; set; }

        private SaveFileDialog DialogForSavingImages { get; set; }

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

        private void ValidateZoomAllowed()
        {
            const int maximumBitmapPixelAmount = 536870912; // Maximum pixel amount a Bitmap object can hold.
            int totalImagePixelAmount = OriginalImageHeight * OriginalImageWidth;

            int maximumAllowedZoom = (int)Math.Sqrt(maximumBitmapPixelAmount / totalImagePixelAmount);

            if (maximumAllowedZoom > 64)
            {
                maximumAllowedZoom = 64;
            }

            SaveImageZoomNumberBox.Maximum = maximumAllowedZoom;
            SaveImageZoomNumberBar.MaximumValue = maximumAllowedZoom;
        }

        private void FullImageZoomNumberBox_ValueChanged(object sender, EventArgs e)
        {
            SaveImageZoomNumberBar.Value = (int)SaveImageZoomNumberBox.Value; // Syncs both NumberBox and NumberBar.

            ZoomImage();
        }

        private void FullImageZoomNumberBar_ValueChanged(object sender, EventArgs e)
        {
            SaveImageZoomNumberBox.Value = SaveImageZoomNumberBar.Value;
        }

        private void ZoomImage()
        {
            int zoom = (int)SaveImageZoomNumberBox.Value;
            int zoomWidth = OriginalImageWidth * zoom;
            int zoomHeight = OriginalImageHeight * zoom;
            SaveImagePictureBox.Image = ApplyZoom((Bitmap)SaveImagePictureBox.Image, zoomWidth, zoomHeight);

            SaveImagePictureBox.Size = SaveImagePictureBox.Image.Size;
            SaveImageBackgroundPanel.ResizePanelToFitControls();

            SaveImageWidthLabel.Text = $"Width: {zoomWidth} ({OriginalImageWidth})";
            SaveImageHeightLabel.Text = $"Height: {zoomHeight} ({OriginalImageHeight})";
        }

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

        private void SaveFullImage()
        {
            DialogForSavingImages.FileName = "PixelImage_" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-ff") + ".png";
            DialogResult result = DialogForSavingImages.ShowDialog();

            if (result == DialogResult.OK && DialogForSavingImages.FileName != string.Empty)
            {
                FileStream imageStream = (FileStream)DialogForSavingImages.OpenFile();
                SaveImagePictureBox.Image.Save(imageStream, ImageFormat.Png);
                imageStream.Close();
            }
        }

        private void SaveFullImageButton_Click(object sender, EventArgs e)
        {
            SaveFullImage();
        }

        private void CancelSaveButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
