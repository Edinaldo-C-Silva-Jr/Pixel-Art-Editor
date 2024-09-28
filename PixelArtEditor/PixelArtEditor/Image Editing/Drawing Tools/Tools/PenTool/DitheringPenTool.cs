namespace PixelArtEditor.Image_Editing.Drawing_Tools.Tools.PenTool
{
    /// <summary>
    /// A pen tool that draws a pixel in the location clicked in the image.
    /// It then draws in every other pixel as the mouse is dragged around the picture, to implement a simple dithering effect.
    /// </summary>
    public class DitheringPenTool : BasePenTool
    {
        private byte? Parity { get; set; } = null;

        protected override void DrawPenPixel(Graphics drawGraphics, SolidBrush drawBrush, Point location)
        {
            // Calculates the pixels to draw based on a parity value, which can be 0 or 1.
            // Draws on every other pixel in the image.
            if (Parity is not null && (location.X % 2 + location.Y % 2) % 2 == Parity.Value)
            {
                drawGraphics.FillRectangle(drawBrush, location.X, location.Y, 1, 1);
            }
        }

        protected override void DrawPenPixel(Graphics drawGraphics, SolidBrush drawBrush, Point location, int zoom)
        {
            if (Parity is null || (location.X % 2 + location.Y % 2) % 2 == Parity)
            {
                location = new(location.X * zoom, location.Y * zoom);
                drawGraphics.FillRectangle(drawBrush, location.X, location.Y, zoom, zoom);
            }
        }

        public override void UseToolClick(Bitmap drawingImage, Color drawingColor, OptionalToolParameters toolParameters)
        {
            if (toolParameters.ClickLocation.HasValue)
            {
                // Gets the parity value based on the first click in this drawing cycle.
                Parity = (byte)((toolParameters.ClickLocation.Value.X % 2 + toolParameters.ClickLocation.Value.Y % 2) % 2);
                base.UseToolClick(drawingImage, drawingColor, toolParameters);
            }
        }

        public override void UseToolRelease(OptionalToolParameters toolParameters)
        {
            Parity = null;
            base.UseToolRelease(toolParameters);
        }
    }
}
