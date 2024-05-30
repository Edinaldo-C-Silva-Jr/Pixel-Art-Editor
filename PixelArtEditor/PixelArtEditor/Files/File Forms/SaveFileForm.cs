using System.Drawing.Drawing2D;

namespace PixelArtEditor.Files.File_Forms
{
    public partial class SaveFileForm : Form
    {
        private int OriginalImageWidth { get; set; }
        private int OriginalImageHeight { get; set; }

        public SaveFileForm(Bitmap originalImage, Size originalSize)
        {
            InitializeComponent();

            FullImagePictureBox.Image = new Bitmap(originalImage);
            OriginalImageWidth = originalSize.Width;
            OriginalImageHeight = originalSize.Height;

            ZoomImage();
        }

        private void FullImageZoomNumberBox_ValueChanged(object sender, EventArgs e)
        {
            FullImageZoomNumberBar.Value = (int)FullImageZoomNumberBox.Value; // Syncs both NumberBox and NumberBar.

            ZoomImage();
        }

        private void FullImageZoomNumberBar_ValueChanged(object sender, EventArgs e)
        {
            FullImageZoomNumberBox.Value = FullImageZoomNumberBar.Value;
        }

        private void ZoomImage()
        {
            int zoom = (int)FullImageZoomNumberBox.Value;
            int zoomWidth = OriginalImageWidth * zoom;
            int zoomHeight = OriginalImageHeight * zoom;
            FullImagePictureBox.Image = ApplyZoom((Bitmap)FullImagePictureBox.Image, zoomWidth, zoomHeight);

            FullImagePictureBox.Size = FullImagePictureBox.Image.Size;
            FullImageBackgroundPanel.ResizePanelToFitControls();
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
    }
}
