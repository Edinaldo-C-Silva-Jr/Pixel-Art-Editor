using PixelArtEditor.Image_Editing.Undo_Redo;
using System.Drawing.Imaging;

namespace PixelArtEditor.Image_Editing.Drawing_Tools.Tools.MirrorPenTool
{
    /// <summary>
    /// A tool that draws a pixel in the location of the mouse click, along with three more pixels, mirrored horizontally, vertically, and both.
    /// </summary>
    public class FourMirrorPenTool : BaseMirrorPenTool
    {
        /// <summary>
        /// Sets the initial boundaries of the mouse movement.
        /// In the Four Mirror Pen Tool, the click location will always be in the top left quarter of the image.
        /// </summary>
        /// <param name="clickX">X position of the mouse click.</param>
        /// <param name="clickY">Y position of the mouse click.</param>
        /// <param name="imageSize">The size of the Drawing Image.</param>
        protected override void ValidateInitialBoundaries(int clickX, int clickY, Size imageSize)
        {
            // The click location will always be on the top and left since the other 3 quadrants will be mirrored.
            if (clickX > (imageSize.Width - 1) / 2)
            {
                clickX = imageSize.Width - clickX - 1;
            }

            if (clickY > (imageSize.Height - 1) / 2)
            {
                clickY = imageSize.Height - clickY - 1;
            }

            base.ValidateInitialBoundaries(clickX, clickY, imageSize);
        }

        /// <summary>
        /// Validates the boundaries during each drawing action to account for the mirrored positions.
        /// In the Four Mirror Pen Tool, the click location will always be in the top left quarter of the image.
        /// </summary>
        /// <param name="clickX">X position of the mouse click.</param>
        /// <param name="clickY">Y position of the mouse click.</param>
        /// <param name="imageSize">The size of the Drawing Image.</param>

        protected override void ValidateMirrorBoundaries(int clickX, int clickY, Size imageSize)
        {
            // The click location will always be on the top and left since the other 3 quadrants will be mirrored.
            if (clickX > (imageSize.Width - 1) / 2)
            {
                clickX = imageSize.Width - clickX - 1;
            }

            if (clickY > (imageSize.Height - 1) / 2)
            {
                clickY = imageSize.Height - clickY - 1;
            }

            base.ValidateMirrorBoundaries(clickX, clickY, imageSize);
        }

        protected override void DrawMirrorPixel(Graphics drawGraphics, SolidBrush drawBrush, Point location, Size imageSize)
        {
            // Draws the first pixel, in the click location.
            Point pixelPoint = location;
            drawGraphics.FillRectangle(drawBrush, pixelPoint.X, pixelPoint.Y, 1, 1);

            // Inverts the click location horizontally and draws another pixel.
            pixelPoint = new(imageSize.Width - location.X - 1, location.Y);
            drawGraphics.FillRectangle(drawBrush, pixelPoint.X, pixelPoint.Y, 1, 1);

            // Inverts the click location vertically and draws another pixel.
            pixelPoint = new(location.X, imageSize.Height - location.Y - 1);
            drawGraphics.FillRectangle(drawBrush, pixelPoint.X, pixelPoint.Y, 1, 1);

            // Inverts the click location diagonally and draws another pixel.
            pixelPoint = new(imageSize.Width - location.X - 1, imageSize.Height - location.Y - 1);
            drawGraphics.FillRectangle(drawBrush, pixelPoint.X, pixelPoint.Y, 1, 1);
        }

        protected override void DrawMirrorPixel(Graphics drawGraphics, SolidBrush drawBrush, Point location, Size imageSize, int zoom)
        {
            // Draws the first pixel, in the click location.
            Point pixelPoint = new(location.X * zoom, location.Y * zoom);
            drawGraphics.FillRectangle(drawBrush, pixelPoint.X, pixelPoint.Y, zoom, zoom);

            // Inverts the click location horizontally and draws another pixel.
            pixelPoint = new((imageSize.Width - location.X - 1) * zoom, location.Y * zoom);
            drawGraphics.FillRectangle(drawBrush, pixelPoint.X, pixelPoint.Y, zoom, zoom);

            // Inverts the click location vertically and draws another pixel.
            pixelPoint = new(location.X * zoom, (imageSize.Height - location.Y - 1) * zoom);
            drawGraphics.FillRectangle(drawBrush, pixelPoint.X, pixelPoint.Y, zoom, zoom);

            // Inverts the click location diagonally and draws another pixel.
            pixelPoint = new((imageSize.Width - location.X - 1) * zoom, (imageSize.Height - location.Y - 1) * zoom);
            drawGraphics.FillRectangle(drawBrush, pixelPoint.X, pixelPoint.Y, zoom, zoom);
        }

        public override IUndoRedoCommand? CreateUndoStep(UndoParameters parameters)
        {
            if (parameters.DrawingImageLocation.HasValue && UneditedImage is not null && EditedImage is not null)
            {
                // Getting only the edited portion of the images.
                // For a four mirror pen, the Edited Image only considers the top left quarter of the image, since the other quarters are the same image mirrored.
                Rectangle editedArea = new(LeftBoundary, UpperBoundary, RightBoundary - LeftBoundary + 1, LowerBoundary - UpperBoundary + 1);
                using Bitmap topLeftUneditedImage = UneditedImage.Clone(editedArea, PixelFormat.Format32bppArgb);
                using Bitmap topLeftEditedImage = EditedImage.Clone(editedArea, PixelFormat.Format32bppArgb);

                // Mirrors the area horizontally, to get the unedited portion of the image on the top right quarter.
                editedArea = new(UneditedImage.Width - RightBoundary - 1, UpperBoundary, RightBoundary - LeftBoundary + 1, LowerBoundary - UpperBoundary + 1);
                using Bitmap topRightUneditedImage = UneditedImage.Clone(editedArea, PixelFormat.Format32bppArgb);
                using Bitmap topRightEditedImage = EditedImage.Clone(editedArea, PixelFormat.Format32bppArgb);

                // Mirrors the area vertically, to get the unedited portion of the image on the bottom left quarter.
                editedArea = new(LeftBoundary, UneditedImage.Height - LowerBoundary - 1, RightBoundary - LeftBoundary + 1, LowerBoundary - UpperBoundary + 1);
                using Bitmap lowerLeftUneditedImage = UneditedImage.Clone(editedArea, PixelFormat.Format32bppArgb);
                using Bitmap lowerLeftEditedImage = EditedImage.Clone(editedArea, PixelFormat.Format32bppArgb);

                // Mirrors the area horizontally and vertically, to get the unedited portion of the image on the bottom right quarter.
                editedArea = new(UneditedImage.Width - RightBoundary - 1, UneditedImage.Height - LowerBoundary - 1, RightBoundary - LeftBoundary + 1, LowerBoundary - UpperBoundary + 1);
                using Bitmap lowerRightUneditedImage = UneditedImage.Clone(editedArea, PixelFormat.Format32bppArgb);
                using Bitmap lowerRightEditedImage = EditedImage.Clone(editedArea, PixelFormat.Format32bppArgb);

                // Changes the edited image to stop it from referencing the original DrawingImage.
                EditedImage = null;

                // Getting the locations where the edits started.
                // These are the four locations that represent the top left pixel of all edited areas.
                Point topLeftEditLocation = new(parameters.DrawingImageLocation.Value.X + LeftBoundary, parameters.DrawingImageLocation.Value.Y + UpperBoundary);
                Point topRightEditLocation = new(parameters.DrawingImageLocation.Value.X + UneditedImage.Width - RightBoundary - 1, parameters.DrawingImageLocation.Value.Y + UpperBoundary);
                Point lowerLeftEditLocation = new(parameters.DrawingImageLocation.Value.X + LeftBoundary, parameters.DrawingImageLocation.Value.Y + UneditedImage.Height - LowerBoundary - 1);
                Point lowerRightEditLocation = new(parameters.DrawingImageLocation.Value.X + UneditedImage.Width - RightBoundary - 1, parameters.DrawingImageLocation.Value.Y + UneditedImage.Height - LowerBoundary - 1);

                FourMirrorPenCommand undoStep = new(new Bitmap(topLeftUneditedImage), new Bitmap(topRightUneditedImage), new Bitmap(lowerLeftUneditedImage), new Bitmap(lowerRightUneditedImage),
                    new(topLeftEditedImage), new(topRightEditedImage), new(lowerLeftEditedImage), new(lowerRightEditedImage),
                    topLeftEditLocation, topRightEditLocation, lowerLeftEditLocation, lowerRightEditLocation);
                ClearProperties();
                return undoStep;
            }
            else
            {
                return null;
            }

        }
    }
}
