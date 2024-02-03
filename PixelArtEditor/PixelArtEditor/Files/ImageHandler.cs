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

        public Rectangle SelectedArea { get; set; }

        public Point SelectionStart { get; set; }

        private SolidBrush SelectionBrush { get; set; }

        public ImageHandler()
        {
            SelectionBrush = new(Color.FromArgb(128, 32, 196, 255));
            OriginalImage = new(1, 1);
            ClipboardImage = new(1, 1);
        }

        public void CreateNewImage(int width, int height, int zoom, Color backgroundColor, bool transparent)
        {
            // Creates the image
            OriginalImage = new(width * zoom, height * zoom);
            using Graphics imageFiller = Graphics.FromImage(OriginalImage);
            imageFiller.Clear(backgroundColor);

            // Changes transparency
            if (transparent)
            {
                OriginalImage.MakeTransparent(backgroundColor);
            }
        }

        // TODO: maybe try to make this more efficient (and have less parameters)
        public void ChangeImageTransparency(Color oldColor, Color newColor, bool transparent, int width, int height, int zoom, Color gridColor, Color backgroundColor)
        {
            OriginalImage.MakeTransparent(oldColor);

            if (!transparent)
            {
                Bitmap temporaryImage = new(width * zoom, height * zoom);
                Graphics temporaryGraphics = Graphics.FromImage(temporaryImage);
                temporaryGraphics.Clear(newColor);
                temporaryGraphics.DrawImage(OriginalImage, 0, 0);
                OriginalImage = temporaryImage;
            }
        }

        public void ClearSelection()
        {
            SelectedArea = Rectangle.Empty;
        }

        public void Dispose()
        {
            OriginalImage?.Dispose();
            ClipboardImage?.Dispose();
            SelectionBrush?.Dispose();
        }
    }
}
