namespace PixelArtEditor.Drawing_Tools
{
    abstract public class DrawingTool : IDrawingTool
    {
        protected static void DrawPixel(Graphics drawGraphics, SolidBrush drawBrush, int xPosition, int yPosition, int pixelSize)
        {
            drawGraphics.FillRectangle(drawBrush, xPosition, yPosition, pixelSize, pixelSize);
        }

        protected static void DrawRectangle(Graphics drawGraphics, SolidBrush drawBrush, int xPosition, int yPosition, int pixelSize, int xLength, int yLength)
        {
            drawGraphics.FillRectangle(drawBrush, xPosition, yPosition, pixelSize * xLength, pixelSize * yLength);
        }

        protected static Point SnapPixelTopLeft(Point absoluteLocation, int pixelSize)
        {
            int xPos = absoluteLocation.X - absoluteLocation.X % pixelSize;
            int yPos = absoluteLocation.Y - absoluteLocation.Y % pixelSize;
            return new(xPos, yPos);
        }

        protected static SolidBrush MakePreviewBrush(SolidBrush colorBrush)
        {
            Color transluscentColor = Color.FromArgb(128, colorBrush.Color);
            colorBrush = new(transluscentColor);
            return colorBrush;
        }

        abstract public void UseToolClick(Graphics imageGraphics, SolidBrush colorBrush, OptionalToolParameters toolParameters);

        abstract public void UseToolHold(Graphics imageGraphics, SolidBrush colorBrush, OptionalToolParameters toolParameters);

        abstract public void PreviewTool(Graphics paintGraphics, SolidBrush colorBrush, OptionalToolParameters toolParameters);

        abstract public void UseToolRelease(Graphics imageGraphics, SolidBrush colorBrush, OptionalToolParameters toolParameters);
    }
}
