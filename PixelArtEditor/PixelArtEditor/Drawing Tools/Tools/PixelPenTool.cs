
namespace PixelArtEditor.Drawing_Tools.Tools
{
    internal class PixelPenTool : DrawingTool
    {
        public override void UseTool(Graphics imageGraphics, Brush colorBrush, int xPosition, int yPosition, int pixelSize)
        {
            DrawPixel(imageGraphics, colorBrush, xPosition, yPosition, pixelSize);
        }
    }
}
