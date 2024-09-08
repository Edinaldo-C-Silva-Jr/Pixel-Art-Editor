namespace PixelArtEditor.Image_Editing.Drawing_Tools.Tools.Pen
{
    /// <summary>
    /// A Pen tool that draws a pixel in the location clicked on the image.
    /// </summary>
    public class PixelPenTool : BasePenTool
    {
        protected override void DrawPenPixel(Graphics drawGraphics, SolidBrush drawBrush, Point location)
        {
            drawGraphics.FillRectangle(drawBrush, location.X, location.Y, 1, 1);
        }

        protected override void DrawPenPixel(Graphics drawGraphics, SolidBrush drawBrush, Point location, int zoom)
        {
            location = new(location.X * zoom, location.Y * zoom);
            drawGraphics.FillRectangle(drawBrush, location.X, location.Y, zoom, zoom);
        }
    }
}
