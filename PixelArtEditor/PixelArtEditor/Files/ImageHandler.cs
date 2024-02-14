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
        /// <summary>
        /// The image the editor should draw in.
        /// </summary>
        public Bitmap OriginalImage { get; private set; }

        /// <summary>
        /// The image that represents the clipboard, used to copy and paste the selected portion of the original image.
        /// </summary>
        public Bitmap ClipboardImage { get; private set; }

        /// <summary>
        /// The brush used to draw the selection box in the image when making a selection.
        /// </summary>
        private SolidBrush SelectionBrush { get; }

        /// <summary>
        /// A point that holds the position where the current selection started.
        /// </summary>
        public Point SelectionStart { get; private set; }

        /// <summary>
        /// A field that stores the rectangle currently used as the selection area in the image.
        /// </summary>
        private Rectangle SelectedArea;

        /// <summary>
        /// Defines the color of the selection brush and initializes both the original image and the clipboard image.
        /// </summary>
        public ImageHandler()
        {
            SelectionBrush = new(Color.FromArgb(128, 32, 196, 255));
            OriginalImage = new(1, 1);
            ClipboardImage = new(1, 1);
        }

        /// <summary>
        /// Creates a blank new image with the defined parameters.
        /// </summary>
        /// <param name="width">The width of the image, in pixels. This is the width used for the pixel art itself.</param>
        /// <param name="height">The height of the image, in pixels. This is the height used for the pixel art itself.</param>
        /// <param name="zoom">The zoom factor applied to the image. This is the size of each pixel.</param>
        /// <param name="backgroundColor">The color used for the image's background.</param>
        /// <param name="transparent">Defines whether the image will have a transparent background or not.</param>
        public void CreateNewImage(int width, int height, int zoom, Color backgroundColor, bool transparent)
        {
            // Creates the image and fills its background with the desired color
            OriginalImage = new(width * zoom, height * zoom);
            using Graphics imageFiller = Graphics.FromImage(OriginalImage);
            imageFiller.Clear(backgroundColor);

            // Changes transparency, if applicable.
            if (transparent)
            {
                OriginalImage.MakeTransparent(backgroundColor);
            }
        }

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

        /// <summary>
        /// Makes the image transparent based on a specific color, which is considered the background.
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
            Bitmap temporaryImage = new(OriginalImage.Width, OriginalImage.Height);
            Graphics temporaryGraphics = Graphics.FromImage(temporaryImage);
            temporaryGraphics.Clear(backgroundColor);
            temporaryGraphics.DrawImage(OriginalImage, 0, 0);
            OriginalImage = temporaryImage;
        }

        /// <summary>
        /// Changes the zoom of the image, based on the image's pixel width, pixel height and the zoom factor.
        /// </summary>
        /// <param name="width">The width of the image, in pixels. This is the width used for the pixel art itself.</param>
        /// <param name="height">The height of the image, in pixels. This is the height used for the pixel art itself.</param>
        /// <param name="zoom">The zoom factor applied to the image. This is the size of each pixel.</param>
        public void ChangeImageZoom(int width, int height, int zoom)
        {
            Bitmap zoomedImage = new(width * zoom, height * zoom);

            Graphics zoomGraphics = Graphics.FromImage(zoomedImage);
            zoomGraphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            zoomGraphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            zoomGraphics.DrawImage(OriginalImage, 0, 0, zoomedImage.Width, zoomedImage.Height);

            OriginalImage = new(zoomedImage);
        }

        public void DefineSelectionStart(Point location)
        {
            SelectionStart = location;
        }

        public void ChangeImageSelection(Point selectionEnd, int drawingBoxWidth, int drawingBoxHeight, int zoom)
        {
            // Makes sure the selection can't go outside the boundaries of the drawing box.
            if (selectionEnd.X >= drawingBoxWidth)
            {
                selectionEnd.X = drawingBoxWidth - 1;
            }
            if (selectionEnd.Y >= drawingBoxHeight)
            {
                selectionEnd.Y = drawingBoxHeight - 1;
            }
            if (selectionEnd.X < 0)
            {
                selectionEnd.X = 0;
            }
            if (selectionEnd.Y < 0)
            {
                selectionEnd.Y = 0;
            }

            // Defines the selection
            if (selectionEnd.X < SelectionStart.X)
            {
                SelectedArea.X = selectionEnd.X - selectionEnd.X % zoom;
                selectionEnd.X = SelectionStart.X - SelectionStart.X % zoom + zoom;
            }
            else
            {
                SelectedArea.X = SelectionStart.X - SelectionStart.X % zoom;
                selectionEnd.X = selectionEnd.X - selectionEnd.X % zoom + zoom;
            }

            SelectedArea.Y = SelectionStart.Y;
            if (selectionEnd.Y < SelectionStart.Y)
            {
                SelectedArea.Y = selectionEnd.Y - selectionEnd.Y % zoom;
                selectionEnd.Y = SelectionStart.Y - SelectionStart.Y % zoom + zoom;
            }
            else
            {
                SelectedArea.Y = SelectionStart.Y - SelectionStart.Y % zoom;
                selectionEnd.Y = selectionEnd.Y - selectionEnd.Y % zoom + zoom;
            }

            SelectedArea.Width = selectionEnd.X - SelectedArea.X;
            SelectedArea.Height = selectionEnd.Y - SelectedArea.Y;
        }

        public void DrawSelectionOntoDrawingBox(Graphics paintGraphics)
        {
            if (SelectedArea != Rectangle.Empty)
            {
                paintGraphics.FillRectangle(SelectionBrush, SelectedArea);
            }
        }

        public void ClearImageSelection()
        {
            SelectedArea = Rectangle.Empty;
        }

        public void CopySelectionFromImage()
        {
            if (SelectedArea != Rectangle.Empty)
            {
                ClipboardImage = OriginalImage.Clone(SelectedArea, PixelFormat.Format32bppArgb);
            }
        }

        public void PasteSelectionInImage()
        {
            if (SelectedArea != Rectangle.Empty)
            {
                using Graphics pasteGraphics = Graphics.FromImage(OriginalImage);
                pasteGraphics.DrawImage(ClipboardImage, new Point(SelectedArea.X, SelectedArea.Y));
            }
        }

        public void Dispose()
        {
            OriginalImage?.Dispose();
            ClipboardImage?.Dispose();
            SelectionBrush?.Dispose();
        }
    }
}
