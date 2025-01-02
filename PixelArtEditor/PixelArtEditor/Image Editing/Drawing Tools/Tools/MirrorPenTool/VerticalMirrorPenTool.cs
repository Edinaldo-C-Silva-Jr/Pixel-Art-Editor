using PixelArtEditor.Image_Editing.Undo_Redo;
using System.Drawing.Imaging;

namespace PixelArtEditor.Image_Editing.Drawing_Tools.Tools.MirrorPenTool
{
    /// <summary>
    /// A tool that draws a pixel in the location of the mouse click, along with another pixel, mirrored vertically.
    /// </summary>
    public class VerticalMirrorPenTool : BaseMirrorPenTool
    {
        /// <summary>
        /// Sets the initial boundaries of the mouse movement.
        /// In the Vertical Mirror Pen Tool, the click location will always be in the upper half of the image.
        /// </summary>
        /// <param name="clickX">X position of the mouse click.</param>
        /// <param name="clickY">Y position of the mouse click.</param>
        /// <param name="imageSize">The size of the Drawing Image.</param>
        protected override void ValidateInitialBoundaries(int clickX, int clickY, Size imageSize)
        {
            // Guarantees the click location represents the upper portion of the image (since the lower portion will be the same, but mirrored).
            if (clickY > (imageSize.Height - 1) / 2)
            {
                clickY = imageSize.Height - clickY - 1;
            }
            base.ValidateInitialBoundaries(clickX, clickY, imageSize);
        }

        /// <summary>
        /// Validates the boundaries during each drawing action to account for the mirrored positions.
        /// In the Vertical Mirror Pen Tool, the click location will always be in the upper half of the image.
        /// </summary>
        /// <param name="clickX">X position of the mouse click.</param>
        /// <param name="clickY">Y position of the mouse click.</param>
        /// <param name="imageSize">The size of the Drawing Image.</param>
        protected override void ValidateMirrorBoundaries(int clickX, int clickY, Size imageSize)
        {
            // Guarantees the click location represents the upper portion of the image (since the lower portion will be the same, but mirrored).
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

            // Inverts the click location vertically and draws another pixel.
            pixelPoint = new(location.X, imageSize.Height - location.Y - 1);
            drawGraphics.FillRectangle(drawBrush, pixelPoint.X, pixelPoint.Y, 1, 1);
        }

        protected override void DrawMirrorPixel(Graphics drawGraphics, SolidBrush drawBrush, Point location, Size imageSize, int zoom)
        {
            // Draws the first pixel, in the click location.
            Point pixelPoint = new(location.X * zoom, location.Y * zoom);
            drawGraphics.FillRectangle(drawBrush, pixelPoint.X, pixelPoint.Y, zoom, zoom);

            // Inverts the click location vertically and draws another pixel.
            pixelPoint = new(location.X * zoom, (imageSize.Height - location.Y - 1) * zoom);
            drawGraphics.FillRectangle(drawBrush, pixelPoint.X, pixelPoint.Y, zoom, zoom);
        }

        public override IUndoRedoCommand? CreateUndoStep(UndoParameters parameters)
        {
            if (parameters.DrawingImageLocation.HasValue && UneditedImage is not null && EditedImage is not null)
            {
                // Getting only the edited portion of the images.
                // For a vertical mirror pen, the Edited Image only considers the upper half of the image, since the lower half is the same image mirrored.
                Rectangle editedArea = new(LeftBoundary, UpperBoundary, RightBoundary - LeftBoundary + 1, LowerBoundary - UpperBoundary + 1);
                using Bitmap upperUneditedImage = UneditedImage.Clone(editedArea, PixelFormat.Format32bppArgb);
                using Bitmap upperEditedImage = EditedImage.Clone(editedArea, PixelFormat.Format32bppArgb);

                // Mirrors the area vertically, to get the unedited portion of the image on the lower half.
                editedArea = new(LeftBoundary, UneditedImage.Height - LowerBoundary - 1, RightBoundary - LeftBoundary + 1, LowerBoundary - UpperBoundary + 1);
                using Bitmap lowerUneditedImage = UneditedImage.Clone(editedArea, PixelFormat.Format32bppArgb);
                using Bitmap lowerEditedImage = EditedImage.Clone(editedArea, PixelFormat.Format32bppArgb);

                // Changes the edited image to stop it from referencing the original DrawingImage.
                EditedImage = null;

                // Getting the locations where the edits started.
                // These are the two locations that represent the top left pixel of both edited areas.
                Point upperEditLocation = new(parameters.DrawingImageLocation.Value.X + LeftBoundary, parameters.DrawingImageLocation.Value.Y + UpperBoundary);
                Point lowerEditLocation = new(parameters.DrawingImageLocation.Value.X + LeftBoundary, parameters.DrawingImageLocation.Value.Y + UneditedImage.Height - LowerBoundary - 1);

                VerticalMirrorPenCommand undoStep = new(new Bitmap(upperUneditedImage), new Bitmap(lowerUneditedImage), new(upperEditedImage), new(lowerEditedImage), upperEditLocation, lowerEditLocation);
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
