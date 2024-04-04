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
        /// The size of each pixel in the current Original Image.
        /// </summary>
        private int ImagePixelSize { get; set; }


        private int Width { get; set; }
        private int Height { get; set; }
        private int XPos { get; set; }
        private int YPos { get; set; }
        private int DrawZoom { get; set; }


        private ImageSelection Selector { get; set; }
        #endregion

        /// <summary>
        /// Default constructor, initializes all properties.
        /// </summary>
        public ImageHandler()
        {
            Selector = new();
            OriginalImage = new(1, 1);
            DrawingImage = new(1, 1);
            ClipboardImage = new(1, 1);
            ImagePixelSize = 1;
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
            ImagePixelSize = zoom;

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

        public void ChangeImageToDrawPosition(int width, int height, int zoom, int startingHorizontal, int startingVertical)
        {
            Width = width;
            Height = height;
            XPos = startingHorizontal;
            YPos = startingVertical;
            DrawZoom = zoom;
        }

        public void CreateImageToDraw()
        {
            // Cuts a piece of the image.
            Rectangle pieceToCut = new(XPos, YPos, Width * ImagePixelSize, Height * ImagePixelSize);
            Bitmap imagePieceZoomed = OriginalImage.Clone(pieceToCut, PixelFormat.Format32bppArgb);

            // Removes zoom.
            Bitmap imagePieceNoZoom = new(Width, Height);
            Graphics pieceGraphics = Graphics.FromImage(imagePieceNoZoom);
            pieceGraphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            pieceGraphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            pieceGraphics.DrawImage(imagePieceZoomed ,0, 0, Width, Height);

            // Zooms to draw.
            Bitmap imagePieceNewZoom = new(Width * DrawZoom, Height * DrawZoom);
            Graphics fullGraphics = Graphics.FromImage(imagePieceNewZoom);
            fullGraphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            fullGraphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            fullGraphics.DrawImage(imagePieceNoZoom, 0, 0, Width * DrawZoom, Height * DrawZoom);

            DrawingImage = new(imagePieceNewZoom);
        }

        public void ApplyDrawnImage()
        {
            // Removes Zoom
            Bitmap drawnImageNoZoom = new(Width, Height);
            Graphics drawnGraphics = Graphics.FromImage(drawnImageNoZoom);
            drawnGraphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            drawnGraphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            drawnGraphics.DrawImage(DrawingImage, 0, 0, Width, Height);

            // Applies viewing zoom
            Bitmap drawingImageZoom = new(drawnImageNoZoom.Width * ImagePixelSize, drawnImageNoZoom.Height * ImagePixelSize);
            Graphics drawnZoomGraphics = Graphics.FromImage(drawingImageZoom);
            drawnZoomGraphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            drawnZoomGraphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            drawnZoomGraphics.DrawImage(drawnImageNoZoom, 0, 0, drawnImageNoZoom.Width * ImagePixelSize, drawnImageNoZoom.Height * ImagePixelSize);

            // Draws image onto original
            Graphics mergeGraphics = Graphics.FromImage(OriginalImage);
            mergeGraphics.DrawImage(drawingImageZoom, XPos, YPos);
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
            ImagePixelSize = zoom;

            Bitmap zoomedImage = new(width * zoom, height * zoom);

            Graphics zoomGraphics = Graphics.FromImage(zoomedImage);
            zoomGraphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            zoomGraphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            zoomGraphics.DrawImage(OriginalImage, 0, 0, zoomedImage.Width, zoomedImage.Height);

            OriginalImage = new(zoomedImage);
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
