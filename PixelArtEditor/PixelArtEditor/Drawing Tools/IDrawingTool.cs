namespace PixelArtEditor.Drawing_Tools
{
    public interface IDrawingTool
    {
        public void UseTool(Graphics imageGraphics, Brush colorBrush, int xPosition, int yPosition, int pixelSize);
    }
}
