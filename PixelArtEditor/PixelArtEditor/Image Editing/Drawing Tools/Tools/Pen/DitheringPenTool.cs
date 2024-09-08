
namespace PixelArtEditor.Image_Editing.Drawing_Tools.Tools.Pen
{
    public class DitheringPenTool : BasePenTool
    {
        protected override void DrawPenPixel(Graphics drawGraphics, SolidBrush drawBrush, Point location)
        {
            if (location.X % 2 + location.Y % 2 == 1)
            {
                drawGraphics.FillRectangle(drawBrush, location.X, location.Y, 1, 1);
            }
        }

        protected override void DrawPenPixel(Graphics drawGraphics, SolidBrush drawBrush, Point location, int zoom)
        {
            if (location.X % 2 + location.Y % 2 == 1)
            {
                location = new(location.X * zoom, location.Y * zoom);
                drawGraphics.FillRectangle(drawBrush, location.X, location.Y, zoom, zoom);
            }
        }
    }
}
