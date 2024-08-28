using PixelArtEditor.Drawing_Tools;
using PixelArtEditor.Image_Editing.Undo_Redo;
using System.Drawing.Imaging;

namespace PixelArtEditor.Image_Editing.Drawing_Tools.Tools.Pen
{
    public class BasePenTool : DrawingTool
    {
        private Bitmap? UneditedImage { get; set; }
        private Bitmap? EditedImage { get; set; }

        private int LeftBoundary { get; set; }
        private int RightBoundary { get; set; }
        private int UpperBoundary { get; set; }
        private int LowerBoundary { get; set; }

        private Graphics? DrawingCycleGraphics { get; set; }

        private SolidBrush? DrawingBrush { get; set; }

        /// <summary>
        /// Draws a single pixel with the current pixel size, filling the pixel where the mouse clicked.
        /// </summary>
        /// <param name="drawGraphics">The graphics for the image being drawn.</param>
        /// <param name="drawBrush">The brush with the currently selected color.</param>
        /// <param name="location">The location of the mouse click inside the Drawing Box.</param>
        /// <param name="pixelSize">The size of each pixel in the image.</param>
        protected static void DrawPenPixel(Graphics drawGraphics, SolidBrush drawBrush, Point location, int pixelSize)
        {
            Point pixelLocation = DrawingCalculations.SnapPixelTopLeft(location, pixelSize);
            drawGraphics.FillRectangle(drawBrush, pixelLocation.X, pixelLocation.Y, pixelSize, pixelSize);
        }

        public override void PreviewTool(Graphics paintGraphics, Color pixelColor, OptionalToolParameters toolParameters)
        {
            if (toolParameters.ClickLocation.HasValue && toolParameters.PixelSize.HasValue)
            {
                using SolidBrush previewBrush = MakePreviewBrush(pixelColor);
                DrawPenPixel(paintGraphics, previewBrush, toolParameters.ClickLocation.Value, toolParameters.PixelSize.Value);
            }
        }

        public override void UseToolClick(Bitmap drawingImage, Color drawingColor, OptionalToolParameters toolParameters)
        {
            if (toolParameters.ClickLocation.HasValue && toolParameters.PixelSize.HasValue)
            {
                UneditedImage = new(drawingImage);
                EditedImage = drawingImage;
                LeftBoundary = RightBoundary = toolParameters.ClickLocation.Value.X;
                UpperBoundary = LowerBoundary = toolParameters.ClickLocation.Value.Y;

                DrawingCycleGraphics = Graphics.FromImage(drawingImage);
                DrawingBrush = new(drawingColor);
                DrawPenPixel(DrawingCycleGraphics, DrawingBrush, toolParameters.ClickLocation.Value, toolParameters.PixelSize.Value);
            }
        }

        public override void UseToolHold(OptionalToolParameters toolParameters)
        {
            if (toolParameters.ClickLocation.HasValue && toolParameters.PixelSize.HasValue)
            {
                int pixelClickedX = toolParameters.ClickLocation.Value.X / toolParameters.PixelSize.Value;
                int pixelClickedY = toolParameters.ClickLocation.Value.Y / toolParameters.PixelSize.Value;

                if (LeftBoundary > pixelClickedX)
                {
                    LeftBoundary = pixelClickedX;
                }

                if (RightBoundary < pixelClickedX)
                {
                    RightBoundary = pixelClickedX;
                }

                if (UpperBoundary > pixelClickedY)
                {
                    UpperBoundary = pixelClickedY;
                }

                if (LowerBoundary < pixelClickedY)
                {
                    LowerBoundary = pixelClickedY;
                }

                DrawPenPixel(DrawingCycleGraphics!, DrawingBrush!, toolParameters.ClickLocation.Value, toolParameters.PixelSize.Value);
            }
        }

        public override void UseToolRelease(OptionalToolParameters toolParameters)
        {
            return;
        }

        public override IUndoRedoCommand CreateUndoStep(Point drawingImageLocation)
        {
            Rectangle editedArea = new(LeftBoundary, UpperBoundary, RightBoundary - LeftBoundary, LowerBoundary - UpperBoundary);
            UneditedImage = UneditedImage!.Clone(editedArea, PixelFormat.Format32bppArgb);

            EditedImage = EditedImage!.Clone(editedArea, PixelFormat.Format32bppArgb);

            Point editLocation = new(drawingImageLocation.X + LeftBoundary, drawingImageLocation.Y + UpperBoundary);

            PenCommand UndoStep = new(new Bitmap(UneditedImage), new Bitmap(EditedImage), editLocation);

            ClearProperties();

            return UndoStep;
        }

        protected void ClearProperties()
        {
            UneditedImage?.Dispose();
            UneditedImage = null;
            EditedImage?.Dispose();
            EditedImage = null;
            DrawingCycleGraphics?.Dispose();
            DrawingCycleGraphics = null;
            DrawingBrush?.Dispose();
            DrawingBrush = null;
        }
    }
}
