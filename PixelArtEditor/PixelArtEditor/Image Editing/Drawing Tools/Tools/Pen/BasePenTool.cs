using PixelArtEditor.Drawing_Tools;

namespace PixelArtEditor.Image_Editing.Drawing_Tools.Tools.Pen
{
    public class BasePenTool : DrawingTool
    {
        private Bitmap? UneditedImage { get; set; }

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
                if (LeftBoundary > toolParameters.ClickLocation.Value.X)
                {
                    LeftBoundary = toolParameters.ClickLocation.Value.X;
                }

                if (RightBoundary < toolParameters.ClickLocation.Value.X)
                {
                    RightBoundary = toolParameters.ClickLocation.Value.X;
                }

                if (UpperBoundary > toolParameters.ClickLocation.Value.Y)
                {
                    UpperBoundary = toolParameters.ClickLocation.Value.X;
                }

                if (LowerBoundary < toolParameters.ClickLocation.Value.X)
                {
                    LowerBoundary = toolParameters.ClickLocation.Value.X;
                }

                DrawPenPixel(DrawingCycleGraphics!, DrawingBrush!, toolParameters.ClickLocation.Value, toolParameters.PixelSize.Value);
            }
        }

        public override void UseToolRelease(OptionalToolParameters toolParameters)
        {
            return;
        }

        public void CreateUndoStep()
        {
            ClearProperties();
        }

        protected void ClearProperties()
        {
            UneditedImage?.Dispose();
            UneditedImage = null;
            DrawingCycleGraphics?.Dispose();
            DrawingCycleGraphics = null;
            DrawingBrush?.Dispose();
            DrawingBrush = null;
        }
    }
}
