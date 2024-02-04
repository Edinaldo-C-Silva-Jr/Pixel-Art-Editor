using PixelArtEditor.Grids;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace PixelArtEditor.Files
{
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

        private SolidBrush SelectionBrush { get; }

        public Point SelectionStart { get; private set; }



        private Rectangle SelectedArea;

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

        public void ChangeImageZoom(int width, int height, int zoom)
        {
            Bitmap zoomedImage = new(width * zoom, height * zoom);

            Graphics zoomGraphics = Graphics.FromImage(zoomedImage);
            zoomGraphics.SmoothingMode = SmoothingMode.HighQuality;
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
