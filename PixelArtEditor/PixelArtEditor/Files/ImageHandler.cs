using System.Drawing.Imaging;
using PixelArtEditor.Extension_Methods;

namespace PixelArtEditor.Files
{
    /// <summary>
    /// A class responsible for handling the main image used by the application.
    /// It has methods for handling the image creation, image-wide changes, as well as image selection.
    /// </summary>
    public class ImageHandler : IDisposable
    {
        #region Original Image Properties
        /// <summary>
        /// The full image that is being made in the editor. 
        /// This version is used for editting. It has no zoom.
        /// </summary>
        public Bitmap EditOriginalImage { get; private set; }

        /// <summary>
        /// The original image used to be displayed in the editor. It is zoomed.
        /// </summary>
        public Bitmap DisplayOriginalImage { get; private set; }

        /// <summary>
        /// The image that represents the clipboard, used to copy and paste the selected portion of the Original Image.
        /// </summary>
        public Bitmap ClipboardOriginalImage { get; private set; }

        /// <summary>
        /// The size values of the original image.
        /// </summary>
        public Size OriginalImageSize { get; private set; }

        /// <summary>
        /// The zoom to be applied to the original image when making the display version.
        /// </summary>
        public int OriginalImageZoom { get; private set; }
        #endregion

        #region Drawing Image Properties
        /// <summary>
        /// A portion of the full image that is currently being drawn in the editor.
        /// This version is used for editting. It has no zoom.
        /// </summary>
        public Bitmap EditDrawingImage { get; private set; }

        /// <summary>
        /// The drawing image used to be displayed in the editor. It is zoomed.
        /// </summary>
        public Bitmap DisplayDrawingImage { get; private set; }

        /// <summary>
        /// The image that represents the clipboard, used to copy and paste the selected portion of the Drawing Image.
        /// </summary>
        public Bitmap ClipboardDrawingImage { get; private set; }

        /// <summary>
        /// The size values of the drawing image.
        /// </summary>
        public Size DrawingImageSize { get; private set; }

        /// <summary>
        /// The zoom to be applied to the drawing image when making the display version.
        /// </summary>
        public int DrawingImageZoom { get; private set; }

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

            EditDrawingImage = new(1, 1);
            DisplayDrawingImage = new(1, 1);
            ClipboardDrawingImage = new(1, 1);
            DrawingImageSize = new(1, 1);
            DrawingImageZoom = 1;
        }

        #region Original Image Size, Creation and Changing
        /// <summary>
        /// Changes the size of the Original Image to the values passed as parameters.
        /// </summary>
        /// <param name="pixelWidth">The width of the image, in pixels.</param>
        /// <param name="pixelHeight">The height of the image, in pixels.</param>
        public void ChangeOriginalImageSize(int pixelWidth, int pixelHeight)
        {
            OriginalImageSize = new Size(pixelWidth, pixelHeight);

            ChangeSizeButPreserveOriginalImage();
            CreateNewDisplayOriginalImage();
        }

        /// <summary>
        /// Changes the zoom value of the Original Image. Also creates a new display image with the new value.
        /// </summary>
        /// <param name="zoom">The new zoom value to use for the Original Image</param>
        public void ChangeOriginalImageZoom(int zoom)
        {
            OriginalImageZoom = zoom;
            CreateNewDisplayOriginalImage();
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
            newSizeGraphics.Clear(Color.White);
            newSizeGraphics.DrawImage(EditOriginalImage, 0, 0);
            EditOriginalImage?.Dispose();
            EditOriginalImage = new(imageWithNewSize);
        }

        /// <summary>
        /// Creates a new Display Image by zooming the Original Image with the defined zoom value.
        /// </summary>
        public void CreateNewDisplayOriginalImage()
        {
            DisplayOriginalImage?.Dispose();
            DisplayOriginalImage = EditOriginalImage.ApplyZoomNearestNeighbor(OriginalImageSize.Width * OriginalImageZoom, OriginalImageSize.Height * OriginalImageZoom);
        }

        /// <summary>
        /// Replaces the Original Image with a new one, while maintaining the image's size.
        /// </summary>
        /// <param name="newOriginalImage">The new image that will be set in place of the current Original Image.</param>
        public void ReplaceOriginalImage(Bitmap newOriginalImage)
        {
            using Graphics originalImageGraphics = Graphics.FromImage(EditOriginalImage);
            originalImageGraphics.DrawImage(newOriginalImage, 0, 0);

            CreateNewDisplayOriginalImage();
        }
        #endregion

        #region Drawing Image Size, Creation and Application
        /// <summary>
        /// Changes the size of the Drawing Image to the values passed as parameters, then creates a new Drawing Image.
        /// </summary>
        /// <param name="pixelWidth">The width of the image, in pixels.</param>
        /// <param name="pixelHeight">The height of the image, in pixels.</param>
        public void ChangeDrawingImageSize(int pixelWidth, int pixelHeight)
        {
            (pixelWidth, pixelHeight) = ValidateDrawingSize(pixelWidth, pixelHeight);

            DrawingImageSize = new Size(pixelWidth, pixelHeight);

            ChangeDrawingImageLocation();
        }

        /// <summary>
        /// Changes only the zoom value of the Drawing Image. Also creates a new display image with the new value.
        /// </summary>
        /// <param name="zoom">The new zoom value to use for the Drawing Image.</param>
        public void ChangeDrawingImageZoom(int zoom)
        {
            DrawingImageZoom = zoom;
            CreateNewDisplayDrawingImage();
        }

        /// <summary>
        /// Checks if the Drawing Image Dimensions are bigger than the Original Image Dimensions.
        /// If they are bigger, reduces them to match the Original Image, since the Drawing Image will be a copy of the Original Image.
        /// </summary>
        /// <param name="pixelWidth">The pixel width of the Drawing Image.</param>
        /// <param name="pixelHeight">The pixel height of the Drawing Image.</param>
        /// <returns>A tuple of width and height values.</returns>
        private (int, int) ValidateDrawingSize(int pixelWidth, int pixelHeight)
        {
            pixelWidth = pixelWidth.ValidateMaximum(OriginalImageSize.Width);
            pixelHeight = pixelHeight.ValidateMaximum(OriginalImageSize.Height);
            return (pixelWidth, pixelHeight);
        }

        /// <summary>
        /// Defines a new location for the Drawing Image to be taken from the Original Image.
        /// Uses the current Drawing Location value.
        /// </summary>
        private void ChangeDrawingImageLocation()
        {
            DrawingLocation = ValidadeDrawingLocation(DrawingLocation);
            CreateImageToDraw();
        }

        /// <summary>
        /// Defines a new location for the Drawing Image to be taken from the Original Image.
        /// Receives the location value as a parameter.
        /// </summary>
        /// <param name="location">The new location from which the Drawing Image will be copied in the Original Image.</param>
        public void ChangeDrawingImageLocation(Point location)
        {
            location = ValidadeDrawingLocation(location);
            DrawingLocation = location;
            CreateImageToDraw();
        }

        /// <summary>
        /// Validates the Drawing Location to make sure the Drawing Box stays within the Original Image's boundaries.
        /// It also sets an interval for moving the Drawing Box to make the clicking more lenient.
        /// </summary>
        /// <param name="location">The drawing location value to validate.</param>
        /// <returns>A location value that keeps the Drawing Box inside the Original Image, and snaps the box depending on its size.</returns>
        private Point ValidadeDrawingLocation(Point location)
        {
            // If the location allows the Drawing Image to stay within the Original Image's boundaries...
            if (location.X < OriginalImageSize.Width - DrawingImageSize.Width)
            {
                // Snaps the location to a position interval depending on the Drawing Image's width.
                location.X -= location.X % GetInterval(DrawingImageSize.Width);
            }
            else
            {
                // Changes the location to the last pixel to keep the Drawing Image within the image's boundaries.
                location.X = OriginalImageSize.Width - DrawingImageSize.Width;
            }

            // If the location allows the Drawing Image to stay within the Original Image's boundaries...
            if (location.Y < OriginalImageSize.Height - DrawingImageSize.Height)
            {
                // Snaps the location to a position interval depending on the Drawing Image's height.
                location.Y -= location.Y % GetInterval(DrawingImageSize.Height);
            }
            else
            {
                // Changes the location to the last pixel to keep the Drawing Image within the image's boundaries.
                location.Y = OriginalImageSize.Height - DrawingImageSize.Height;
            }

            return location;
        }

        // TODO: Probably make this simpler.
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
        /// </summary>
        public void CreateImageToDraw()
        {
            // Defines a rectangle to clone the Drawing Image from the Original Image.
            Rectangle areaToCopyFromOriginalImage = new(DrawingLocation, DrawingImageSize);
            EditDrawingImage?.Dispose();
            EditDrawingImage = EditOriginalImage.Clone(areaToCopyFromOriginalImage, PixelFormat.Format32bppArgb);

            CreateNewDisplayDrawingImage();
        }

        /// <summary>
        /// Applies the Drawing Image to the Original Image, by drawing it onto the Original Image in the same location it was copied from.
        /// </summary>
        public void ApplyDrawnImage()
        {
            using Graphics mergeGraphics = Graphics.FromImage(EditOriginalImage);
            mergeGraphics.DrawImage(EditDrawingImage, DrawingLocation);
        }

        /// <summary>
        /// Creates a new Display Image by zooming the Drawing Image with the defined zoom value.
        /// </summary>
        public void CreateNewDisplayDrawingImage()
        {
            DisplayDrawingImage?.Dispose();
            DisplayDrawingImage = EditDrawingImage.ApplyZoomNearestNeighbor(DrawingImageSize.Width * DrawingImageZoom, DrawingImageSize.Height * DrawingImageZoom);
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

            CreateNewDisplayOriginalImage();
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

            CreateNewDisplayOriginalImage();
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
                    ClipboardOriginalImage?.Dispose();
                    ClipboardOriginalImage = EditOriginalImage.Clone(selectedArea, PixelFormat.Format32bppArgb);
                }
                else // Copies the Drawing Image.
                {
                    ClipboardDrawingImage?.Dispose();
                    ClipboardDrawingImage = EditDrawingImage.Clone(selectedArea, PixelFormat.Format32bppArgb);
                }
            }
        }

        /// <summary>
        /// Pastes the previously copied portion of the image into the current selection position.
        /// Which image gets pasted depends on the ImageType parameter.
        /// </summary>
        /// <param name="selectedArea">The rectangle area selected on the image.</param>
        /// <param name="currentImage">Which of the images is currently selected.</param>
        public void PasteSelectionOnImage(Rectangle selectedArea, ImageType currentImage)
        {
            if (selectedArea != Rectangle.Empty)
            {
                if (currentImage == ImageType.OriginalImage) // Pastes the Clipboard Original Image into the Original Image.
                {
                    using Graphics pasteGraphics = Graphics.FromImage(EditOriginalImage);
                    pasteGraphics.DrawImage(ClipboardOriginalImage, new Point(selectedArea.X, selectedArea.Y));

                    CreateImageToDraw();
                    CreateNewDisplayOriginalImage();
                }
                
                if (currentImage == ImageType.DrawingImage) // Pastes the Clipboard Drawing Image into the Drawing Image.
                {
                    using Graphics pasteGraphics = Graphics.FromImage(EditDrawingImage);
                    pasteGraphics.DrawImage(ClipboardDrawingImage, new Point(selectedArea.X, selectedArea.Y));

                    ApplyDrawnImage();
                    CreateNewDisplayDrawingImage();
                }
            }
        }
        #endregion

        /// <summary>
        /// Disposes of all the images used in the class.
        /// </summary>
        public void Dispose()
        {
            EditOriginalImage?.Dispose();
            DisplayOriginalImage?.Dispose();
            ClipboardOriginalImage?.Dispose();

            EditDrawingImage?.Dispose();
            DisplayDrawingImage?.Dispose();
            ClipboardDrawingImage?.Dispose();
        }
    }
}
