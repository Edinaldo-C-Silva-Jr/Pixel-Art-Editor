using PixelArtEditor.Image_Editing.Undo_Redo;
using System.Drawing.Imaging;

namespace PixelArtEditor.Image_Editing.Drawing_Tools.Tools.MirrorPenTool
{
    public class FullMirrorPenTool : BaseMirrorPenTool
    {
        protected override void DrawMirrorPixel(Graphics drawGraphics, SolidBrush drawBrush, Point location, Size imageSize)
        {
            // Draws the first pixel, in the click location.
            Point pixelPoint = location;
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

            // Inverts the click location diagonally and draws another pixel.
            pixelPoint = new((imageSize.Width - location.X - 1) * zoom, (imageSize.Height - location.Y - 1) * zoom);
            drawGraphics.FillRectangle(drawBrush, pixelPoint.X, pixelPoint.Y, zoom, zoom);
        }

        public override IUndoRedoCommand CreateUndoStep(Point drawingImageLocation)
        {
            // Getting only the edited portion of the images.
            // For a four mirror pen, the Edited Image only considers the top left quarter of the image, since the other quarters are the same image mirrored.
            Rectangle editedArea = new(LeftBoundary, UpperBoundary, RightBoundary - LeftBoundary + 1, LowerBoundary - UpperBoundary + 1);
            using Bitmap topLeftUneditedImage = UneditedImage!.Clone(editedArea, PixelFormat.Format32bppArgb);
            EditedImage = EditedImage!.Clone(editedArea, PixelFormat.Format32bppArgb);

            // Mirrors the area horizontally and vertically, to get the unedited portion of the image on the bottom right quarter.
            editedArea = new(UneditedImage.Width - RightBoundary - 1, UneditedImage.Height - LowerBoundary - 1, RightBoundary - LeftBoundary + 1, LowerBoundary - UpperBoundary + 1);
            using Bitmap lowerRightUneditedImage = UneditedImage!.Clone(editedArea, PixelFormat.Format32bppArgb);

            // Getting the locations where the edits started.
            // These are the four locations that represent the top left pixel of all edited areas.
            Point topLeftEditLocation = new(drawingImageLocation.X + LeftBoundary, drawingImageLocation.Y + UpperBoundary);
            Point lowerRightEditLocation = new(drawingImageLocation.X + UneditedImage!.Width - RightBoundary - 1, drawingImageLocation.Y + UneditedImage.Height - LowerBoundary - 1);

            FullMirrorPenCommand undoStep = new(new Bitmap(topLeftUneditedImage), new Bitmap(lowerRightUneditedImage), new(EditedImage), topLeftEditLocation, lowerRightEditLocation);
            ClearProperties();
            return undoStep;
        }
    }
}
