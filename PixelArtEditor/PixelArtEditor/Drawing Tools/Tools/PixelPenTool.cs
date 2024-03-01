
namespace PixelArtEditor.Drawing_Tools.Tools
{
    internal class PixelPenTool : DrawingTool
    {
        public override void UseTool(Graphics imageGraphics, Brush colorBrush, int pixelSize, Point? beginPoint, Point? endPoint, Size? imageSize)
        {
            if (beginPoint.HasValue)
            {
                beginPoint = SnapPixelTopLeft(beginPoint.Value, pixelSize);
                DrawPixel(imageGraphics, colorBrush, beginPoint.Value.X, beginPoint.Value.Y, pixelSize);
            }
        }
    }
}
