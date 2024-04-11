using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace PixelArtEditor.Files
{
    /// <summary>
    /// A class responsible for handling the main image used by the application.
    /// It has methods for handling the image creation, image-wide changes, as well as image selection.
    /// </summary>
    internal class ImageHandler : IDisposable
    {
        #region Properties
        private ImageSelection Selector { get; set; }

        /// <summary>
        /// The full image being used in the pixel art editor.
        /// </summary>
        public Bitmap OriginalImage { get; private set; }

        /// <summary>
        /// The portion of the original image that is being used in the DrawingBox.
        /// </summary>
        public Bitmap DrawingImage { get; private set; }

        /// <summary>
        /// The image that represents the clipboard, used to copy and paste the selected portion of the original image.
        /// </summary>
        public Bitmap ClipboardImage { get; private set; }

        /// <summary>
        /// The pixel dimensions of the Original Image, without the zoom.
        /// </summary>
        public Size OriginalDimensions { get; private set; }

        /// <summary>
        /// The size of each pixel in the Original Image.
        /// </summary>
        public int OriginalPixelSize { get; private set; }

        /// <summary>
        /// The pixel dimensions of the Drawing Image, without the zoom.
        /// </summary>
        public Size DrawingDimensions { get; private set; }

        /// <summary>
        /// The size of each pixel in the Drawing image.
        /// </summary>
        public int DrawingPixelSize { get; private set; }

        /// <summary>
        /// The location from which the Drawing Image was taken in the Original Image.
        /// </summary>
        public Point DrawingLocation { get; private set; }
        #endregion

        /// <summary>
        /// Default constructor, instances all images and the image selector.
        /// </summary>
        public ImageHandler()
        {
            Selector = new();
            OriginalImage = new(1, 1);
            DrawingImage = new(1, 1);
            ClipboardImage = new(1, 1);

            OriginalPixelSize = 1;
            DrawingPixelSize = 1;
        }

        #region Original Image Size, Creation and Changing
        /// <summary>
        /// Changes the size of the Original Image to the values passed as parameters.
        /// </summary>
        /// <param name="pixelWidth">The width of the image, in pixel sizes.</param>
        /// <param name="pixelHeight">The height of the image, in pixel sizes.</param>
        /// <param name="pixelSize">The size of each pixel in the image.</param>
        public void ChangeOriginalImageSize(int pixelWidth, int pixelHeight, int pixelSize)
        {
            OriginalDimensions = new Size(pixelWidth, pixelHeight);
            OriginalPixelSize = pixelSize;

            ChangeSizeButPreserveOriginalImage();
        }

        /// <summary>
        /// Creates a blank new image with the current size values and the defined color and transparency.
        /// </summary>s
        /// <param name="backgroundColor">The color used for the image's background.</param>
        /// <param name="transparent">Defines whether the image will have a transparent background or not.</param>
        public void CreateNewBlankImage(Color backgroundColor, bool transparent)
        {
            // Creates the image and fills its background with the desired color
            OriginalImage = new(OriginalDimensions.Width * OriginalPixelSize, OriginalDimensions.Height * OriginalPixelSize);
            using Graphics imageFiller = Graphics.FromImage(OriginalImage);
            imageFiller.Clear(backgroundColor);

            // Changes transparency, if applicable.
            if (transparent)
            {
                OriginalImage.MakeTransparent(backgroundColor);
            }
        }

        /// <summary>
        /// Creates a new image to use as the current Original Image, with the currently defined size, while preserving the Original Image.
        /// </summary>
        private void ChangeSizeButPreserveOriginalImage()
        {
            // Creates a new image with the currently defined sizes.
            using Bitmap imageWithNewSize = new(OriginalDimensions.Width * OriginalPixelSize, OriginalDimensions.Height * OriginalPixelSize);
            using Graphics newSizeGraphics = Graphics.FromImage(imageWithNewSize);

            // Draws the Original Image on top of the new image.
            newSizeGraphics.DrawImage(OriginalImage, 0, 0);
            OriginalImage = new(imageWithNewSize);
        }
        #endregion

        #region Drawing Image Size, Creation and Application
        /// <summary>
        /// Changes the size of the Drawing Image to the values passed as parameters.
        /// </summary>
        /// <param name="pixelWidth">The width of the image, in pixel sizes.</param>
        /// <param name="pixelHeight">The height of the image, in pixel sizes.</param>
        /// <param name="pixelSize">The size of each pixel in the image.</param>
        public void ChangeDrawingImageSize(int pixelWidth, int pixelHeight, int pixelSize)
        {
            (pixelWidth, pixelHeight) = ValidateDrawingSize(pixelWidth, pixelHeight);

            DrawingDimensions = new Size(pixelWidth, pixelHeight);
            DrawingPixelSize = pixelSize;

            ChangeDrawingImageLocation();

            CreateImageToDraw();
        }

        /// <summary>
        /// Checks if the Drawing Image Dimensions are bigger than the Original Image Dimensions.
        /// If they are bigger, reduces them to match the Original Image.
        /// </summary>
        /// <param name="pixelWidth">The pixel width of the Drawing Image.</param>
        /// <param name="pixelHeight">The pixel height of the Drawing Image.</param>
        /// <returns>A tuple of width and height values.</returns>
        private (int, int) ValidateDrawingSize(int pixelWidth, int pixelHeight)
        {
            // Makes sure the Drawing Image isn't bigger than the Original Image, since it is supposed to be a copy of a piece the Original Image.
            if (pixelWidth > OriginalDimensions.Width)
            {
                pixelWidth = OriginalDimensions.Width;
            }
            if (pixelHeight > OriginalDimensions.Height)
            {
                pixelHeight = OriginalDimensions.Height;
            }

            return (pixelWidth, pixelHeight);
        }

        /// <summary>
        /// Defines a new location for the Drawing Image to be taken from the Original Image. Also creates the Drawing Image.
        /// </summary>
        /// <param name="location">The new location from which the Drawing Image will be copied in the Original Image.</param>
        public void ChangeDrawingImageLocation(Point location)
        {
            location = RemoveZoomFromLocation(location);
            location = ValidadeDrawingLocation(location);

            DrawingLocation = location;

            CreateImageToDraw();
        }

        private void ChangeDrawingImageLocation()
        {
            DrawingLocation = ValidadeDrawingLocation(DrawingLocation);

            CreateImageToDraw();
        }

        private Point RemoveZoomFromLocation(Point location)
        {
            location.X -= location.X % OriginalPixelSize;
            location.Y -= location.Y % OriginalPixelSize;

            location.X /= OriginalPixelSize;
            location.Y /= OriginalPixelSize;

            return location;
        }

        private Point ValidadeDrawingLocation(Point location)
        {
            if (location.X < OriginalDimensions.Width - DrawingDimensions.Width)
            {
                location.X -= location.X % GetInterval(DrawingDimensions.Width);
            }
            else
            {
                location.X = OriginalDimensions.Width - DrawingDimensions.Width;
            }

            if (location.Y < OriginalDimensions.Height - DrawingDimensions.Height)
            {
                location.Y -= location.Y % GetInterval(DrawingDimensions.Height);
            }
            else
            {
                location.Y = OriginalDimensions.Height - DrawingDimensions.Height;
            }

            return location;
        }

        private static int GetInterval(int dimension)
        {
            // List of 3 { 6, 9, 11, 15, 17, 18, 21, 23, 27, 29, 33, 39, 51, 54, 57 }
            // List of 4 { 8, 12, 16, 20, 22, 24, 28, 32, 34, 36, 40, 44, 48, 52, 56, 60, 64 }
            // List of 5 { 5, 10, 13, 19, 25, 30, 35, 37, 38, 43, 45, 50, 55, 58, 59 }
            // List of 7 { 7, 14, 21, 26, 31, 41, 42, 46, 47, 49, 53, 61, 62, 63 }

            List<List<int>> listOfSizes = new()
            {
                new() { 6, 9, 11, 15, 17, 18, 21, 23, 27, 29, 33, 39, 51, 54, 57 },
                new() { 5, 10, 13, 19, 25, 30, 35, 37, 38, 43, 45, 50, 55, 58, 59 },
                new() { 7, 14, 21, 26, 31, 41, 42, 46, 47, 49, 53, 61, 62, 63 }
            };

            int interval = 4;
            for (int i = 0; i < 3; i++)
            {
                if (listOfSizes[i].Contains(dimension))
                {
                    interval = 2 * i + 3;
                }
            }
            return interval;
        }

        /// <summary>
        /// Creates a new Drawing Image, by copying a piece of the Original Image.
        /// Utilizes previous defined values for the Drawing Image size and the location it will copy from the Original Image.
        /// </summary>
        public void CreateImageToDraw()
        {
            // Defines a rectangle to clone the Drawing Image from the Original Image.
            // The rectangle will be cloned from the Original Image, so it has to use the Original Image's pixel size.
            Size drawingImageSize = new(DrawingDimensions.Width * OriginalPixelSize, DrawingDimensions.Height * OriginalPixelSize);
            Point location = new(DrawingLocation.X * OriginalPixelSize, DrawingLocation.Y * OriginalPixelSize);
            Rectangle areaToCopyFromOriginalImage = new(location, drawingImageSize);
            using Bitmap copiedImagePiece = OriginalImage.Clone(areaToCopyFromOriginalImage, PixelFormat.Format32bppArgb);

            // Applies the Drawing Image pixel size to the image.
            DrawingImage = ApplyZoom(copiedImagePiece, DrawingDimensions.Width * DrawingPixelSize, DrawingDimensions.Height * DrawingPixelSize);
        }

        /// <summary>
        /// Applies the Drawing Image to the Original Image, by drawing it onto the Original Image in the same location it was copied from.
        /// </summary>
        public void ApplyDrawnImage()
        {
            Bitmap drawingImageToApply = new(DrawingDimensions.Width * OriginalPixelSize, DrawingDimensions.Height * OriginalPixelSize);
            Point location = new(DrawingLocation.X * OriginalPixelSize, DrawingLocation.Y * OriginalPixelSize);

            // Applies the Original Image zoom, to ensure the Drawing Image has the same size it had when it was copied.
            drawingImageToApply = ApplyZoom(DrawingImage, drawingImageToApply.Width, drawingImageToApply.Height);

            // Draws the Drawing Image onto the Original Image, in the currently defined location.
            using Graphics mergeGraphics = Graphics.FromImage(OriginalImage);
            mergeGraphics.DrawImage(drawingImageToApply, location);

            drawingImageToApply.Dispose();
        }
        #endregion

        #region Changing and Applying Zoom
        /// <summary>
        /// Receives an image and zooms it to the sizes passed as parameters.
        /// </summary>
        /// <param name="originalImage">The image to apply the zoom to.</param>
        /// <param name="zoomWidth">The new width to be applied to the image.</param>
        /// <param name="zoomHeight">The new height to be applied to the image.</param>
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

        /// <summary>
        /// Changes only the pixel size value of the Original Image. Also zooms the image to the new pixel size.
        /// </summary>
        /// <param name="PixelSize"></param>
        public void ChangeOriginalImageZoom(int PixelSize)
        {
            OriginalPixelSize = PixelSize;
            ZoomOriginalImage();
        }

        /// <summary>
        /// Zooms the Original Image based on the newly defined pixel size value.
        /// </summary>
        private void ZoomOriginalImage()
        {
            OriginalImage = ApplyZoom(OriginalImage, OriginalDimensions.Width * OriginalPixelSize, OriginalDimensions.Height * OriginalPixelSize);
        }

        /// <summary>
        /// Changes only the pixel size value of the Drawing Image.
        /// </summary>
        /// <param name="PixelSize"></param>
        public void ChangeDrawingImageZoom(int pixelSize)
        {
            DrawingPixelSize = pixelSize;
            ZoomDrawingImage();
        }

        /// <summary>
        /// Zooms the Drawing Image based on the newly defined pixel size value.
        /// </summary>
        private void ZoomDrawingImage()
        {
            DrawingImage = ApplyZoom(DrawingImage, DrawingDimensions.Width * DrawingPixelSize, DrawingDimensions.Height * DrawingPixelSize);
        }
        #endregion

        #region Adding and Removing Transparency
        /// <summary>
        /// Makes the image transparent based on the background color.
        /// </summary>
        /// <param name="transparencyColor">The color to be made transparent in the image.</param>
        public void MakeImageTransparent(Color transparencyColor)
        {
            OriginalImage.MakeTransparent(transparencyColor);
        }

        /// <summary>
        /// Removes the transparency of the image, applying a solid color to its background.
        /// </summary>
        /// <param name="backgroundColor">The color to be applied as the image's background.</param>
        public void MakeImageNotTransparent(Color backgroundColor)
        {
            // Creates a temporary image and applies the background color to it.
            using Bitmap temporaryImage = new(OriginalImage.Width, OriginalImage.Height);
            using Graphics temporaryGraphics = Graphics.FromImage(temporaryImage);
            temporaryGraphics.Clear(backgroundColor);

            // Then draws the Original Image on top of the solid color image to remove the transparency.
            temporaryGraphics.DrawImage(OriginalImage, 0, 0);
            OriginalImage = new(temporaryImage);
        }
        #endregion

        public void DefineNewImage(Bitmap newOriginalImage, bool resizeOnLoad, int drawingBoxWidth, int drawingBoxHeight)
        {
            if (resizeOnLoad)
            {
                OriginalImage = new(newOriginalImage);
            }
            else
            {
                using Bitmap nonResizedImage = new(drawingBoxWidth, drawingBoxHeight);
                using Graphics newImageGraphics = Graphics.FromImage(nonResizedImage);
                newImageGraphics.DrawImage(newOriginalImage, 0, 0);

                OriginalImage = new(nonResizedImage);
            }
        }

        public void DefineSelectionStart(Point location)
        {
            Selector.DefineStart(location);
        }

        public void ClearImageSelection()
        {
            Selector.ClearSelection();
        }

        public void ChangeImageSelection(Point selectionEnd, int drawingBoxWidth, int drawingBoxHeight, int zoom)
        {
            Selector.ChangeSelectionArea(selectionEnd, drawingBoxWidth, drawingBoxHeight, zoom);
        }

        public void DrawSelectionOntoDrawingBox(Graphics paintGraphics)
        {
            Selector.DrawSelection(paintGraphics);
        }

        public void CopySelectionFromImage()
        {
            if (Selector.SelectedArea != Rectangle.Empty)
            {
                ClipboardImage = OriginalImage.Clone(Selector.SelectedArea, PixelFormat.Format32bppArgb);
            }
        }

        public void PasteSelectionInImage()
        {
            if (Selector.SelectedArea != Rectangle.Empty)
            {
                using Graphics pasteGraphics = Graphics.FromImage(OriginalImage);
                pasteGraphics.DrawImage(ClipboardImage, new Point(Selector.SelectedArea.X, Selector.SelectedArea.Y));
            }
        }

        public void Dispose()
        {
            OriginalImage?.Dispose();
            ClipboardImage?.Dispose();
            DrawingImage?.Dispose();
            Selector.Dispose();
        }
    }
}
