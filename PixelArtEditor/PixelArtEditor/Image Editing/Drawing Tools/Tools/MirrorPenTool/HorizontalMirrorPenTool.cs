using PixelArtEditor.Extension_Methods;
using PixelArtEditor.Image_Editing.Undo_Redo;
using System.Drawing.Imaging;

namespace PixelArtEditor.Image_Editing.Drawing_Tools.Tools.MirrorPenTool
{
    public class HorizontalMirrorPenTool : BaseMirrorPenTool
    {
        protected override void ValidateInitialLocation(int clickX, int clickY, Size imageSize)
        {
            if (clickX > (imageSize.Width - 1) / 2)
            {
                clickX = imageSize.Width - clickX - 1;
            }
            base.ValidateInitialLocation(clickX, clickY, imageSize);
        }

        protected override void ValidateMirrorLocation(int clickX, int clickY, Size imageSize)
        {
            if (clickX > (imageSize.Width - 1) / 2)
            {
                clickX = imageSize.Width - clickX - 1;
            }
            base.ValidateMirrorLocation(clickX, clickY, imageSize);
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

        public override IUndoRedoCommand CreateUndoStep(Point drawingImageLocation)
        {
            // Getting only the edited portion of the images.
            Rectangle editedArea = new(LeftBoundary, UpperBoundary, RightBoundary - LeftBoundary + 1, LowerBoundary - UpperBoundary + 1);
            using Bitmap leftUneditedImage = UneditedImage!.Clone(editedArea, PixelFormat.Format32bppArgb);
            EditedImage = EditedImage!.Clone(editedArea, PixelFormat.Format32bppArgb);

            editedArea = new(UneditedImage.Width - RightBoundary - 1, UpperBoundary, RightBoundary - LeftBoundary + 1, LowerBoundary - UpperBoundary + 1);
            using Bitmap rightUneditedImage = UneditedImage!.Clone(editedArea, PixelFormat.Format32bppArgb);

            // Getting the location where the edits started.
            Point leftEditLocation = new(drawingImageLocation.X + LeftBoundary, drawingImageLocation.Y + UpperBoundary);
            Point rightEditLocation = new(drawingImageLocation.X + UneditedImage!.Width - RightBoundary - 1, drawingImageLocation.Y + UpperBoundary);

            HorizontalMirrorPenCommand undoStep = new(new Bitmap(leftUneditedImage), new Bitmap(rightUneditedImage), new(EditedImage), leftEditLocation, rightEditLocation);
            ClearProperties();
            return undoStep;
        }
    }
}
