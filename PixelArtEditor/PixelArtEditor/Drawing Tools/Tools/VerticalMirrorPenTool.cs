namespace PixelArtEditor.Drawing_Tools.Tools
{
    /// <summary>
    /// a Pen tool that draws two pixels, one in the location clicked, and another on the opposite side vertically.
    /// </summary>
    internal class VerticalMirrorPenTool : DrawingTool
    {
        /// <summary>
        /// Draws two pixels, mirrored vertically in the image.
        /// </summary>
        /// <param name="graphics">The graphics to draw the pixel on.</param>
        /// <param name="brush">The brush to use when drawing the pixel.</param>
        /// <param name="location">The location clicked on the image.</param>
        /// <param name="imageSize">The size of the image that is being drawn on.</param>
        /// <param name="pixelSize">The size of each pixel in the image.</param>
        private static void DrawMirrorPixel(Graphics graphics, SolidBrush brush, Point location, Size imageSize, int pixelSize)
        {
            // Draws the first pixel, in the click location.
            Point pixelPoint = SnapPixelTopLeft(location, pixelSize);
            DrawPixel(graphics, brush, pixelPoint, pixelSize);

            // Inverts the click location vertically and draws another pixel.
            pixelPoint = new(location.X, imageSize.Height - location.Y - 1);
            pixelPoint = SnapPixelTopLeft(pixelPoint, pixelSize);
            DrawPixel(graphics, brush, pixelPoint, pixelSize);
        }

        public override void PreviewTool(Graphics paintGraphics, SolidBrush colorBrush, OptionalToolParameters toolParameters)
        {
            if (toolParameters.ClickLocation.HasValue && toolParameters.ImageSize.HasValue && toolParameters.PixelSize.HasValue)
            {
                colorBrush = MakePreviewBrush(colorBrush);
                DrawMirrorPixel(paintGraphics, colorBrush, toolParameters.ClickLocation.Value, toolParameters.ImageSize.Value, toolParameters.PixelSize.Value);
            }
        }

        public override void UseToolClick(Graphics imageGraphics, SolidBrush colorBrush, OptionalToolParameters toolParameters)
        {
            if (toolParameters.ClickLocation.HasValue && toolParameters.ImageSize.HasValue && toolParameters.PixelSize.HasValue)
            {
                DrawMirrorPixel(imageGraphics, colorBrush, toolParameters.ClickLocation.Value, toolParameters.ImageSize.Value, toolParameters.PixelSize.Value);
            }
        }

        public override void UseToolHold(Graphics imageGraphics, SolidBrush colorBrush, OptionalToolParameters toolParameters)
        {
            if (toolParameters.ClickLocation.HasValue && toolParameters.ImageSize.HasValue && toolParameters.PixelSize.HasValue)
            {
                DrawMirrorPixel(imageGraphics, colorBrush, toolParameters.ClickLocation.Value, toolParameters.ImageSize.Value, toolParameters.PixelSize.Value);
            }
        }

        public override void UseToolRelease(Graphics imageGraphics, SolidBrush colorBrush, OptionalToolParameters toolParameters)
        {
            return;
        }
    }
}
