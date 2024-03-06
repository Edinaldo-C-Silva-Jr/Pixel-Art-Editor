namespace PixelArtEditor.Drawing_Tools.Tools
{
    internal class HorizontalMirrorPenTool : DrawingTool
    {
        private static void DrawMirrorPixel(Graphics graphics, SolidBrush brush, OptionalToolParameters parameters)
        {
            Point pixelPoint = SnapPixelTopLeft(parameters.ClickLocation.Value, parameters.PixelSize.Value);
            DrawPixel(graphics, brush, pixelPoint.X, pixelPoint.Y, parameters.PixelSize.Value);

            pixelPoint = new(parameters.ImageSize.Value.Width - parameters.ClickLocation.Value.X - 1, parameters.ClickLocation.Value.Y);
            pixelPoint = SnapPixelTopLeft(pixelPoint, parameters.PixelSize.Value);
            DrawPixel(graphics, brush, pixelPoint.X, pixelPoint.Y, parameters.PixelSize.Value);
        }

        public override void PreviewTool(Graphics paintGraphics, SolidBrush colorBrush, OptionalToolParameters toolParameters)
        {
            if (toolParameters.ClickLocation.HasValue && toolParameters.ImageSize.HasValue && toolParameters.PixelSize.HasValue)
            {
                colorBrush = MakePreviewBrush(colorBrush);
                DrawMirrorPixel(paintGraphics, colorBrush, toolParameters);
            }
        }

        public override void UseToolClick(Graphics imageGraphics, SolidBrush colorBrush, OptionalToolParameters toolParameters)
        {
            if (toolParameters.ClickLocation.HasValue && toolParameters.ImageSize.HasValue && toolParameters.PixelSize.HasValue)
            {
                DrawMirrorPixel(imageGraphics, colorBrush, toolParameters);
            }
        }

        public override void UseToolHold(Graphics imageGraphics, SolidBrush colorBrush, OptionalToolParameters toolParameters)
        {
            if (toolParameters.ClickLocation.HasValue && toolParameters.ImageSize.HasValue && toolParameters.PixelSize.HasValue)
            {
                DrawMirrorPixel(imageGraphics, colorBrush, toolParameters);
            }
        }

        public override void UseToolRelease(Graphics imageGraphics, SolidBrush colorBrush, OptionalToolParameters toolParameters)
        {
            return;
        }
    }
}
