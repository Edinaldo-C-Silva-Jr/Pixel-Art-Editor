namespace PixelArtEditor.Drawing_Tools
{
    internal interface IDrawingTool
    {
        public void UseTool(Bitmap imageToDraw, Color colorToUse, int xPosition, int yPosition, int pixelSize);
    }
}
