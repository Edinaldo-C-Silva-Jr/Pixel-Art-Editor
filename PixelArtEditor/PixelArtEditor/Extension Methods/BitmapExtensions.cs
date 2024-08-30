using System.Drawing.Drawing2D;

namespace PixelArtEditor.Extension_Methods
{
    /// <summary>
    /// A class that implements extension methods for the type Bitmap.
    /// </summary>
    public static class BitmapExtensions
    {
        /// <summary>
        /// Applies zoom to the image using the specified size values.
        /// Uses NearestNeighbor to preserve the original image's resolution.
        /// </summary>
        /// <param name="originalImage">The image to apply the zoom to.</param>
        /// <param name="zoomWidth">The new width for the image after the zoom.</param>
        /// <param name="zoomHeight">The new height for the image after the zoom.</param>
        /// <returns>The image stretched to its.</returns>
        public static Bitmap ApplyZoomNearestNeighbor(this Bitmap originalImage, int zoomWidth, int zoomHeight)
        {
            // Creates an image with the newly desired size.
            Bitmap zoomedImage = new(zoomWidth, zoomHeight);
            using Graphics zoomGraphics = Graphics.FromImage(zoomedImage);

            // Sets the Interpolation mode and Pixel Offset to use for zooming. Uses Nearest Neighbor to preserve the pixels.
            zoomGraphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            zoomGraphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

            // Draws the original image onto the zoomed image, using the new size and interpolation mode defined.
            zoomGraphics.DrawImage(originalImage, 0, 0, zoomWidth, zoomHeight);
            return zoomedImage;
        }
    }
}
