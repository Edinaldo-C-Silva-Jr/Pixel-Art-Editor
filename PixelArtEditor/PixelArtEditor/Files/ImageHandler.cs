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
        public Size DrawingDimensions{ get; private set; }

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
        }

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
        /// Changes only the pixel size value of the Original Image. Also zooms the image to the new pixel size.
        /// </summary>
        /// <param name="PixelSize"></param>
        public void ChangeOriginalImageZoom(int PixelSize)
        {
            OriginalPixelSize = PixelSize;

            ZoomOriginalImage();
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

        /// <summary>
        /// Zooms the Original Image based on the newly defined pixel size value.
        /// </summary>
        private void ZoomOriginalImage()
        {
            ApplyZoom(OriginalImage, OriginalDimensions.Width * OriginalPixelSize, OriginalDimensions.Height * OriginalPixelSize);
        }







        /// <summary>
        /// Changes the size of the Drawing Image to the values passed as parameters.
        /// </summary>
        /// <param name="pixelWidth">The width of the image, in pixel sizes.</param>
        /// <param name="pixelHeight">The height of the image, in pixel sizes.</param>
        /// <param name="pixelSize">The size of each pixel in the image.</param>
        public void ChangeDrawingImageSize(int pixelWidth, int pixelHeight, int pixelSize)
        {
            DrawingDimensions = new Size(pixelWidth, pixelHeight);
            DrawingPixelSize = pixelSize;
        }

        /// <summary>
        /// Changes only the pixel size value of the Drawing Image.
        /// </summary>
        /// <param name="PixelSize"></param>
        public void ChangeDrawingImageZoom(int pixelSize)
        {
            DrawingPixelSize = pixelSize;
        }






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
            Bitmap temporaryImage = new(OriginalImage.Width, OriginalImage.Height);
            Graphics temporaryGraphics = Graphics.FromImage(temporaryImage);
            temporaryGraphics.Clear(backgroundColor);
            temporaryGraphics.DrawImage(OriginalImage, 0, 0);
            OriginalImage = temporaryImage;
        }

        private void ApplyZoom(Bitmap originalImage, int zoomWidth, int zoomHeight)
        {
            using Bitmap zoomedImage = new(zoomWidth, zoomHeight);
            using Graphics zoomGraphics = Graphics.FromImage(zoomedImage);
            zoomGraphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            zoomGraphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            zoomGraphics.DrawImage(originalImage, 0, 0, zoomWidth, zoomHeight);

            originalImage = new(zoomedImage);
        }








        public void CreateImageToDraw()
        {
            // Cuts a piece of the image.
            Rectangle pieceToCut = new(XPos, YPos, Width * OriginalPixelSize, Height * OriginalPixelSize);
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
            Bitmap drawingImageZoom = new(drawnImageNoZoom.Width * OriginalPixelSize, drawnImageNoZoom.Height * OriginalPixelSize);
            Graphics drawnZoomGraphics = Graphics.FromImage(drawingImageZoom);
            drawnZoomGraphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            drawnZoomGraphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            drawnZoomGraphics.DrawImage(drawnImageNoZoom, 0, 0, drawnImageNoZoom.Width * OriginalPixelSize, drawnImageNoZoom.Height * OriginalPixelSize);

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
