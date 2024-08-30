using System.Drawing.Imaging;
using PixelArtEditor.Extension_Methods;

namespace PixelArtEditor.Files
{
    /// <summary>
    /// A class responsible for handling the main image used by the application.
    /// It has methods for handling the image creation, image-wide changes, as well as image selection.
    /// </summary>
    internal class ImageHandler : IDisposable
    {
        #region Properties
        public Bitmap EditOriginalImage { get; set; }

        public Bitmap DisplayOriginalImage { get; set; }

        /// <summary>
        /// The image that represents the clipboard, used to copy and paste the selected portion of the Original Image.
        /// </summary>
        public Bitmap ClipboardOriginalImage { get; private set; }

        public Size OriginalImageSize { get; set; }

        public int OriginalImageZoom { get; set; }



        /// <summary>
        /// The portion of the original image that is being used in the DrawingBox.
        /// </summary>
        public Bitmap DrawingImage { get; private set; }

        

        /// <summary>
        /// The image that represents the clipboard, used to copy and paste the selected portion of the Drawing Image.
        /// </summary>
        public Bitmap ClipboardDrawingImage { get; private set; }

        

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
            EditOriginalImage = new(1, 1);
            DisplayOriginalImage = new(1, 1);
            ClipboardOriginalImage = new(1, 1);
            OriginalImageSize = new(1, 1);
            OriginalImageZoom = 1;


            DrawingImage = new(1, 1);
            ClipboardDrawingImage = new(1, 1);

            DrawingPixelSize = 1;
        }

        #region Original Image Size, Creation and Changing
        /// <summary>
        /// Changes the size of the Original Image to the values passed as parameters.
        /// </summary>
        /// <param name="pixelWidth">The width of the image, in pixels.</param>
        /// <param name="pixelHeight">The height of the image, in pixels.</param>
        /// <param name="zoom">The amount of zoom to apply to the image.</param>
        public void ChangeOriginalImageSize(int pixelWidth, int pixelHeight, int zoom)
        {
            OriginalImageSize = new Size(pixelWidth, pixelHeight);
            OriginalImageZoom = zoom;

            ChangeSizeButPreserveOriginalImage();
        }

        /// <summary>
        /// Creates a new image to use as the current Original Image, with the currently defined size, while preserving the Original Image.
        /// </summary>
        private void ChangeSizeButPreserveOriginalImage()
        {
            // Creates a new image with the currently defined size.
            using Bitmap imageWithNewSize = new(OriginalImageSize.Width, OriginalImageSize.Height);
            using Graphics newSizeGraphics = Graphics.FromImage(imageWithNewSize);

            // Draws the Original Image on top of the new image, then assigns it to the Original Image.
            newSizeGraphics.DrawImage(EditOriginalImage, 0, 0);
            EditOriginalImage?.Dispose();
            EditOriginalImage = new(imageWithNewSize);
        }

        /// <summary>
        /// Creates a blank new image with the current size values and the defined color and transparency.
        /// </summary>s
        /// <param name="backgroundColor">The color used for the image's background.</param>
        /// <param name="transparent">Defines whether the image will have a transparent background or not.</param>
        public void CreateNewBlankImage(Color backgroundColor, bool transparent)
        {
            // Creates the image and fills its background with the desired color.
            EditOriginalImage?.Dispose();
            EditOriginalImage = new(OriginalImageSize.Width, OriginalImageSize.Height);
            using Graphics imageFiller = Graphics.FromImage(EditOriginalImage);
            imageFiller.Clear(backgroundColor);

            // Changes transparency, if applicable.
            if (transparent)
            {
                EditOriginalImage.MakeTransparent(backgroundColor);
            }
        }

        /// <summary>
        /// Replaces the Original Image with a new one, while maintaining the image's size.
        /// </summary>
        /// <param name="newOriginalImage">The new image that will be set in place of the current Original Image.</param>
        public void ReplaceOriginalImage(Bitmap newOriginalImage)
        {
            CreateNewBlankImage(Color.White, true);

            using Graphics originalImageGraphics = Graphics.FromImage(EditOriginalImage);
            originalImageGraphics.DrawImage(newOriginalImage, 0, 0);
        }
        #endregion

        // TODO: Will have to revise several of the calculations that involve the Original Image
        // Also need to change the Drawing Image eventually.
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
        /// Defines a new location for the Drawing Image to be taken from the Original Image, by receiving the location value as a parameter.
        /// </summary>
        /// <param name="location">The new location from which the Drawing Image will be copied in the Original Image.</param>
        public void ChangeDrawingImageLocation(Point location)
        {
            location = RemoveZoomFromLocation(location);
            location = ValidadeDrawingLocation(location);

            DrawingLocation = location;

            CreateImageToDraw();
        }

        /// <summary>
        /// Defines a new location for the Drawing Image to be taken from the Original Image, uses the current Drawing Location.
        /// </summary>
        private void ChangeDrawingImageLocation()
        {
            DrawingLocation = ValidadeDrawingLocation(DrawingLocation);

            CreateImageToDraw();
        }

        /// <summary>
        /// Removes the current Pixel Size from the drawing location, to get the raw location value for it.
        /// </summary>
        /// <param name="location">The location value received, which has the zoom applied.</param>
        /// <returns>The location value without the zoom, which represents the raw pixel location.</returns>
        private Point RemoveZoomFromLocation(Point location)
        {
            location.X -= location.X % OriginalPixelSize;
            location.Y -= location.Y % OriginalPixelSize;

            location.X /= OriginalPixelSize;
            location.Y /= OriginalPixelSize;

            return location;
        }

        /// <summary>
        /// Validates the Drawing Location to make sure the Drawing Box stays within the Original Image's boundaries.
        /// It also sets an interval for moving the Drawing Box to make the clicking more lenient.
        /// </summary>
        /// <param name="location">The drawing location value to validate.</param>
        /// <returns>A location value that keeps the Drawing Box inside the Original Image, and snaps the box depending on its size.</returns>
        private Point ValidadeDrawingLocation(Point location)
        {
            if (location.X < OriginalDimensions.Width - DrawingDimensions.Width)
            {
                // Snaps the location to a position interval depending on the Drawing Box's width.
                location.X -= location.X % GetInterval(DrawingDimensions.Width);
            }
            else
            {
                // Changes the location to the last pixel to keep the Drawing Box within the image's boundaries.
                location.X = OriginalDimensions.Width - DrawingDimensions.Width;
            }

            if (location.Y < OriginalDimensions.Height - DrawingDimensions.Height)
            {
                // Snaps the location to a position interval depending on the Drawing Box's height.
                location.Y -= location.Y % GetInterval(DrawingDimensions.Height);
            }
            else
            {
                // Changes the location to the last pixel to keep the Drawing Box within the image's boundaries.
                location.Y = OriginalDimensions.Height - DrawingDimensions.Height;
            }

            return location;
        }

        /// <summary>
        /// Calculates a pixel interval for the Drawing Box. This interval defines how many pixels the Drawing Box will move.
        /// The interval is calculated based on the Drawing Box size.
        /// It is used to make choosing the Drawing Box position more lenient, rather than having it be pixel accurate.
        /// </summary>
        /// <param name="dimension">The dimension of the Drawing Box, can be either width or height.</param>
        /// <returns>The pixel interval to use for that dimension.</returns>
        private static int GetInterval(int dimension)
        {
            // These lists contain the Drawing Box sizes, with each list being tied with a specific pixel interval.
            // List of values with a 3 pixel interval { 6, 9, 11, 15, 17, 18, 21, 23, 27, 29, 33, 39, 51, 54, 57 }
            // List of values with a 4 pixel interval { 8, 12, 16, 20, 22, 24, 28, 32, 34, 36, 40, 44, 48, 52, 56, 60, 64 }
            // List of values with a 5 pixel interval { 5, 10, 13, 19, 25, 30, 35, 37, 38, 43, 45, 50, 55, 58, 59 }
            // List of values with a 7 pixel interval { 7, 14, 21, 26, 31, 41, 42, 46, 47, 49, 53, 61, 62, 63 }
            List<List<int>> listOfSizes = new()
            {
                new() { 6, 9, 11, 15, 17, 18, 21, 23, 27, 29, 33, 39, 51, 54, 57 },
                new() { 5, 10, 13, 19, 25, 30, 35, 37, 38, 43, 45, 50, 55, 58, 59 },
                new() { 7, 14, 21, 26, 31, 41, 42, 46, 47, 49, 53, 61, 62, 63 }
            };

            int interval = 4; // Defaults the interval to 4.
            for (int i = 0; i < 3; i++)
            {
                if (listOfSizes[i].Contains(dimension)) // Checks if the dimension value exists in any of the lists.
                {
                    interval = 2 * i + 3; // If it exists, set the interval to that value (3, 5 or 7).
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
            // Applies the Original Image zoom, to ensure the Drawing Image has the same size it had when it was copied.
            int drawingImageWidth = DrawingDimensions.Width * OriginalPixelSize;
            int drawingImageHeight = DrawingDimensions.Height * OriginalPixelSize;
            using Bitmap drawingImageToApply = ApplyZoom(DrawingImage, drawingImageWidth, drawingImageHeight);

            // Draws the Drawing Image onto the Original Image, in the currently defined location.
            Point location = new(DrawingLocation.X * OriginalPixelSize, DrawingLocation.Y * OriginalPixelSize);
            using Graphics mergeGraphics = Graphics.FromImage(OriginalImage);
            mergeGraphics.DrawImage(drawingImageToApply, location);
        }
        #endregion

        #region Changing and Applying Zoom
        /// <summary>
        /// Changes only the pixel size value of the Original Image. Also zooms the image to the new pixel size.
        /// </summary>
        /// <param name="pixelSize">The new pixel size to use for the Original Image</param>
        public void ChangeOriginalImageZoom(int pixelSize)
        {
            OriginalImageZoom = pixelSize;
            CreateNewDisplayOriginalImage();
        }

        /// <summary>
        /// Creates a new Display Image by zooming the Original Image with the defined zoom value.
        /// </summary>
        private void CreateNewDisplayOriginalImage()
        {
            DisplayOriginalImage = EditOriginalImage.ApplyZoomNearestNeighbor(OriginalImageSize.Width * OriginalImageZoom, OriginalImageSize.Height * OriginalImageZoom);
        }

        /// <summary>
        /// Changes only the pixel size value of the Drawing Image.
        /// </summary>
        /// <param name="pixelSize">The new pixel size to use for the Drawing Image.</param>
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
            EditOriginalImage.MakeTransparent(transparencyColor);
        }

        /// <summary>
        /// Removes the transparency of the image, applying a solid color to its background.
        /// </summary>
        /// <param name="backgroundColor">The color to be applied as the image's background.</param>
        public void MakeImageNotTransparent(Color backgroundColor)
        {
            // Creates a temporary image and applies the background color to it.
            Bitmap temporaryImage = new(OriginalImageSize.Width, OriginalImageSize.Height);
            using Graphics temporaryGraphics = Graphics.FromImage(temporaryImage);
            temporaryGraphics.Clear(backgroundColor);

            // Then draws the Original Image on top of the solid color image to remove the transparency.
            temporaryGraphics.DrawImage(EditOriginalImage, 0, 0);
            EditOriginalImage.Dispose();
            EditOriginalImage = temporaryImage;
        }
        #endregion

        #region Copy and Paste
        /// <summary>
        /// Copies the portion of the image that corresponds to the selection area.
        /// Which image gets copied depends on the ImageType parameter.
        /// </summary>
        /// <param name="selectedArea">The rectangle area selected on the image.</param>
        /// <param name="currentImage">Which of the images is currently selected.</param>
        public void CopySelectionFromImage(Rectangle selectedArea, ImageType currentImage)
        {
            if (selectedArea != Rectangle.Empty)
            {
                if (currentImage == ImageType.OriginalImage) // Copies the Original Image.
                {
                    ClipboardOriginalImage = EditOriginalImage.Clone(selectedArea, PixelFormat.Format32bppArgb);
                }
                else // Copies the Drawing Image.
                {
                    ClipboardDrawingImage = DrawingImage.Clone(selectedArea, PixelFormat.Format32bppArgb);
                }
            }
        }

        /// <summary>
        /// Pastes the previously copied portion of the image into the current selection position.
        /// Which image gets pasted depends on the ImageType parameter.
        /// </summary>
        /// <param name="selectedArea">he rectangle area selected on the image.</param>
        /// <param name="currentImage">Which of the images is currently selected.</param>
        public void PasteSelectionOnImage(Rectangle selectedArea, ImageType currentImage)
        {
            if (selectedArea != Rectangle.Empty)
            {
                if (currentImage == ImageType.OriginalImage) // Pastes the Clipboard Original Image into the Original Image.
                {
                    using Graphics pasteGraphics = Graphics.FromImage(EditOriginalImage);
                    pasteGraphics.DrawImage(ClipboardOriginalImage, new Point(selectedArea.X, selectedArea.Y));
                }
                else // Pastes the Clipboard Drawing Image into the Drawing Image.
                {
                    using Graphics pasteGraphics = Graphics.FromImage(DrawingImage);
                    pasteGraphics.DrawImage(ClipboardDrawingImage, new Point(selectedArea.X, selectedArea.Y));
                }
            }
        }
        #endregion

        public void Dispose()
        {
            EditOriginalImage?.Dispose();
            DisplayOriginalImage?.Dispose();
            ClipboardOriginalImage?.Dispose();

            ClipboardDrawingImage?.Dispose();
            DrawingImage?.Dispose();
        }
    }
}
