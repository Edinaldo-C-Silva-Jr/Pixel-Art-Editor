
namespace PixelArtEditor.Drawing_Tools
{
    abstract public class DrawingTool : IDrawingTool
    {
        protected static void DrawPixel(Graphics drawGraphics, Brush drawBrush, int xPosition, int yPosition, int pixelSize)
        {
            drawGraphics.FillRectangle(drawBrush, xPosition, yPosition, pixelSize, pixelSize);
        }

        protected static void DrawRectangle(Graphics drawGraphics, Brush drawBrush, int xPosition, int yPosition, int pixelSize, int xLength, int yLength)
        {
            drawGraphics.FillRectangle(drawBrush, xPosition, yPosition, pixelSize * xLength, pixelSize * yLength);
        }
        abstract public void UseTool(Graphics imageGraphics, Brush colorBrush, int xPosition, int yPosition, int pixelSize);
    }
}
