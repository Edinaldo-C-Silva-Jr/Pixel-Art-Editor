
namespace PixelArtEditor.Drawing_Tools.Tools
{
    /// <summary>
    /// A Pen tool that simply draws a pixel in the location clicked on the image.
    /// </summary>
    internal class PixelPenTool : DrawingTool
    {
        /// <summary>
        /// Method that actually draws the pixel.
        /// </summary>
        /// <param name="graphics">The graphics to draw the pixel on.</param>
        /// <param name="brush">The brush to use when drawing the pixel.</param>
        /// <param name="location">The location clicked on the image.</param>
        /// <param name="pixelSize">The size of each pixel in the image.</param>
        private static void DrawPenPixel(Graphics graphics, SolidBrush brush, Point location, int pixelSize)
        {
            Point pixelPoint = SnapPixelTopLeft(location, pixelSize);
            DrawPixel(graphics, brush, pixelPoint, pixelSize);
        }

        public override void PreviewTool(Graphics paintGraphics, SolidBrush colorBrush, OptionalToolParameters toolParameters)
        {
            if (toolParameters.ClickLocation.HasValue && toolParameters.PixelSize.HasValue)
            {
                colorBrush = MakePreviewBrush(colorBrush);
                DrawPenPixel(paintGraphics, colorBrush, toolParameters.ClickLocation.Value, toolParameters.PixelSize.Value);
            }
        }

        public override void UseToolClick(Graphics imageGraphics, SolidBrush colorBrush, OptionalToolParameters toolParameters)
        {
            if (toolParameters.ClickLocation.HasValue && toolParameters.PixelSize.HasValue)
            {
                DrawPenPixel(imageGraphics, colorBrush, toolParameters.ClickLocation.Value, toolParameters.PixelSize.Value);
            }
        }

        public override void UseToolHold(Graphics imageGraphics, SolidBrush colorBrush, OptionalToolParameters toolParameters)
        {
            if (toolParameters.ClickLocation.HasValue && toolParameters.PixelSize.HasValue)
            {
                DrawPenPixel(imageGraphics, colorBrush, toolParameters.ClickLocation.Value, toolParameters.PixelSize.Value);
            }
        }

        public override void UseToolRelease(Graphics imageGraphics, SolidBrush colorBrush, OptionalToolParameters toolParameters)
        {
            return; 
        }
    }
}
