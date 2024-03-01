namespace PixelArtEditor.Drawing_Tools
{
    public interface IDrawingTool
    {
        public void UseTool(Graphics imageGraphics, Brush colorBrush, int pixelSize, Point? beginPoint, Point? endPoint, Size? imageSize);
    }
}
