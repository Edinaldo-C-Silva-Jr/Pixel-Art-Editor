namespace PixelArtEditor.Drawing_Tools.Tools
{
    internal class VerticalMirrorPenTool : DrawingTool
    {
        public override void UseTool(Graphics imageGraphics, Brush colorBrush, int pixelSize, OptionalToolParameters toolParameters)
        {
            if (toolParameters.BeginPoint.HasValue && toolParameters.ImageSize.HasValue)
            {
                Point pixelPoint = SnapPixelTopLeft(toolParameters.BeginPoint.Value, pixelSize);
                DrawPixel(imageGraphics, colorBrush, pixelPoint.X, pixelPoint.Y, pixelSize);

                toolParameters.BeginPoint = new(toolParameters.BeginPoint.Value.X, toolParameters.ImageSize.Value.Height - toolParameters.BeginPoint.Value.Y - 1);
                pixelPoint = SnapPixelTopLeft(toolParameters.BeginPoint.Value, pixelSize);
                DrawPixel(imageGraphics, colorBrush, pixelPoint.X, pixelPoint.Y, pixelSize);
            }
        }
    }
}
