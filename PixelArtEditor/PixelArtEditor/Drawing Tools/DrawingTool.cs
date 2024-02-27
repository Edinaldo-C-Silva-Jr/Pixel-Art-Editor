
namespace PixelArtEditor.Drawing_Tools
{
    internal class DrawingTool : IDrawingTool
    {
        protected static void DrawPixel(Graphics drawGraphics, Brush drawBrush, int xPosition, int yPosition, int pixelSize)
        {
            drawGraphics.FillRectangle(drawBrush, xPosition, yPosition, pixelSize, pixelSize);
        }

        protected static void DrawRectangle(Graphics drawGraphics, Brush drawBrush, int xPosition, int yPosition, int pixelSize, int xLength, int yLength)
        {
            drawGraphics.FillRectangle(drawBrush, xPosition, yPosition, pixelSize * xLength, pixelSize * yLength);
        }
        public void UseTool(Bitmap imageToDraw, Color colorToUse, int xPosition, int yPosition, int pixelSize)
        {
            return;
        }
    }
}
