using PixelArtEditor.Image_Editing.Undo_Redo;
using System.Drawing.Imaging;

namespace PixelArtEditor.Image_Editing.Drawing_Tools.Tools.MirrorPenTool
{
    /// <summary>
    /// A tool that draws a pixel in the location of the mouse click, along with another pixel, mirrored horizontally.
    /// </summary>
    public class HorizontalMirrorPenTool : BaseMirrorPenTool
    {
        /// <summary>
        /// Sets the initial boundaries of the mouse movement.
        /// In the Horizontal Mirror Pen Tool, the click location will always be in the left half of the image.
        /// </summary>
        /// <param name="clickX">X position of the mouse click.</param>
        /// <param name="clickY">Y position of the mouse click.</param>
        /// <param name="imageSize">The size of the Drawing Image.</param>
        protected override void ValidateInitialBoundaries(int clickX, int clickY, Size imageSize)
        {
            // Guarantees the click location represents the left portion of the image (since the right portion will be the same, but mirrored).
            if (clickX > (imageSize.Width - 1) / 2)
            {
                clickX = imageSize.Width - clickX - 1;
            }
            base.ValidateInitialBoundaries(clickX, clickY, imageSize);
        }

        /// <summary>
        /// Validates the boundaries during each drawing action to account for the mirrored positions.
        /// In the Horizontal Mirror Pen Tool, the click location will always be in the left half of the image.
        /// </summary>
        /// <param name="clickX">X position of the mouse click.</param>
        /// <param name="clickY">Y position of the mouse click.</param>
        /// <param name="imageSize">The size of the Drawing Image.</param>

        protected override void ValidateMirrorBoundaries(int clickX, int clickY, Size imageSize)
        {
            // Guarantees the click location represents the left portion of the image (since the right portion will be the same, but mirrored).
            if (clickX > (imageSize.Width - 1) / 2)
            {
                clickX = imageSize.Width - clickX - 1;
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
        }

        protected override void DrawMirrorPixel(Graphics drawGraphics, SolidBrush drawBrush, Point location, Size imageSize, int zoom)
        {
            // Draws the first pixel, in the click location.
            Point pixelPoint = new(location.X * zoom, location.Y * zoom);
            drawGraphics.FillRectangle(drawBrush, pixelPoint.X, pixelPoint.Y, zoom, zoom);

            // Inverts the click location horizontally and draws another pixel.
            pixelPoint = new((imageSize.Width - location.X - 1) * zoom, location.Y * zoom);
            drawGraphics.FillRectangle(drawBrush, pixelPoint.X, pixelPoint.Y, zoom, zoom);
        }

        public override IUndoRedoCommand? CreateUndoStep(UndoParameters parameters)
        {
            if (parameters.DrawingImageLocation.HasValue && UneditedImage is not null && EditedImage is not null)
            {
                // Getting only the edited portion of the images.
                // For a horizontal mirror pen, the Edited Image only considers the left half of the image, since the right half is the same image mirrored.
                Rectangle editedArea = new(LeftBoundary, UpperBoundary, RightBoundary - LeftBoundary + 1, LowerBoundary - UpperBoundary + 1);
                using Bitmap leftUneditedImage = UneditedImage.Clone(editedArea, PixelFormat.Format32bppArgb);
                using Bitmap leftEditedImage = EditedImage.Clone(editedArea, PixelFormat.Format32bppArgb);

                // Mirrors the area horizontally, to get the unedited portion of the image on the right half.
                editedArea = new(UneditedImage.Width - RightBoundary - 1, UpperBoundary, RightBoundary - LeftBoundary + 1, LowerBoundary - UpperBoundary + 1);
                using Bitmap rightUneditedImage = UneditedImage.Clone(editedArea, PixelFormat.Format32bppArgb);
                using Bitmap rightEditedArea = EditedImage.Clone(editedArea, PixelFormat.Format32bppArgb);

                // Changes the edited image to stop it from referencing the original DrawingImage.
                EditedImage = null;

                // Getting the locations where the edits started.
                // These are the two locations that represent the top left pixel of both edited areas.
                Point leftEditLocation = new(parameters.DrawingImageLocation.Value.X + LeftBoundary, parameters.DrawingImageLocation.Value.Y + UpperBoundary);
                Point rightEditLocation = new(parameters.DrawingImageLocation.Value.X + UneditedImage.Width - RightBoundary - 1, parameters.DrawingImageLocation.Value.Y + UpperBoundary);

                HorizontalMirrorPenCommand undoStep = new(new Bitmap(leftUneditedImage), new Bitmap(rightUneditedImage), new(leftEditedImage), new(rightEditedArea), leftEditLocation, rightEditLocation);
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
