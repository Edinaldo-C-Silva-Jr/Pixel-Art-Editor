using PixelArtEditor.Drawing_Tools.Tools;

namespace PixelArtEditor.Drawing_Tools
{
    public interface IDrawingTool
    {
        public void UseTool(Graphics imageGraphics, Brush colorBrush, int pixelSize, OptionalToolParameters toolParameters);
    }
}
