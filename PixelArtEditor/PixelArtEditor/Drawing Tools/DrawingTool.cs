

using PixelArtEditor.Drawing_Tools.Tools;

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

        protected static Point SnapPixelTopLeft(Point absoluteLocation, int pixelSize)
        {
            int xPos = absoluteLocation.X - absoluteLocation.X % pixelSize;
            int yPos = absoluteLocation.Y - absoluteLocation.Y % pixelSize;
            return new(xPos, yPos);
        }

        abstract public void UseTool(Graphics imageGraphics, Brush colorBrush, int pixelSize, OptionalToolParameters toolParameters);
    }
}
