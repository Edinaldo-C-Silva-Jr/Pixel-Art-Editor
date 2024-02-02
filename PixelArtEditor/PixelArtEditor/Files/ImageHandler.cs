namespace PixelArtEditor.Files
{
    internal class ImageHandler : IDisposable
    {
        /// <summary>
        /// The image the editor should draw in.
        /// </summary>
        public Bitmap OriginalImage { get; set; }

        /// <summary>
        /// The image that represents the clipboard, used to copy and paste the selected portion of the original image.
        /// </summary>
        public Bitmap ClipboardImage { get; set; }

        public ImageHandler()
        {
            OriginalImage = new(1, 1);
            ClipboardImage = new(1, 1);
        }

        public void Dispose()
        {
            OriginalImage?.Dispose();
            ClipboardImage?.Dispose();
        }
    }
}
