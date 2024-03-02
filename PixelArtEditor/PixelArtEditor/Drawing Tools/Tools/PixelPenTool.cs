
namespace PixelArtEditor.Drawing_Tools.Tools
{
    internal class PixelPenTool : DrawingTool
    {
        public override void UseTool(Graphics imageGraphics, Brush colorBrush, int pixelSize, OptionalToolParameters toolParameters)
        {
            if (toolParameters.BeginPoint.HasValue)
            {
                Point pixelPoint = SnapPixelTopLeft(toolParameters.BeginPoint.Value, pixelSize);
                DrawPixel(imageGraphics, colorBrush, pixelPoint.X, pixelPoint.Y, pixelSize);
            }
        }
    }
}
