using PixelArtEditor.Drawing_Tools.Tools;

namespace PixelArtEditor.Drawing_Tools
{
    public interface IDrawingTool
    {
        public void PreviewTool(Graphics paintGraphics, SolidBrush colorBrush, OptionalToolParameters toolParameters);

        public void UseToolClick(Graphics imageGraphics, SolidBrush colorBrush, OptionalToolParameters toolParameters);

        public void UseToolHold(Graphics imageGraphics, SolidBrush colorBrush, OptionalToolParameters toolParameters);

        public void UseToolRelease(Graphics imageGraphics, SolidBrush colorBrush, OptionalToolParameters toolParameters);
    }
}
