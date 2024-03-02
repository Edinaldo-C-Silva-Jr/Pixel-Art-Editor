namespace PixelArtEditor.Drawing_Tools.Tools
{
    internal class HorizontalMirrorPenTool : DrawingTool
    {
        public override void UseTool(Graphics imageGraphics, Brush colorBrush, int pixelSize, OptionalToolParameters toolParameters)
        {
            if (toolParameters.BeginPoint.HasValue && toolParameters.ImageSize.HasValue)
            {
                Point pixelPoint = SnapPixelTopLeft(toolParameters.BeginPoint.Value, pixelSize);
                DrawPixel(imageGraphics, colorBrush, pixelPoint.X, pixelPoint.Y, pixelSize);

                pixelPoint = new(toolParameters.ImageSize.Value.Width - toolParameters.BeginPoint.Value.X - 1, toolParameters.BeginPoint.Value.Y);
                pixelPoint = SnapPixelTopLeft(pixelPoint, pixelSize);
                DrawPixel(imageGraphics, colorBrush, pixelPoint.X, pixelPoint.Y, pixelSize);
            }
        }
    }
}
