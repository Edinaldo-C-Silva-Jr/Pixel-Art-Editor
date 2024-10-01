namespace PixelArtEditor.Image_Editing.Drawing_Tools.Tools.MirrorPenTool
{
    public class FourMirrorPenTool : BaseMirrorPenTool
    {
        protected override void DrawMirrorPixel(Graphics drawGraphics, SolidBrush drawBrush, Point location, Size imageSize)
        {
            // Draws the first pixel, in the click location.
            Point pixelPoint = location;
            drawGraphics.FillRectangle(drawBrush, pixelPoint.X, pixelPoint.Y, 1, 1);

            // Inverts the click location horizontally and draws another pixel.
            pixelPoint = new(imageSize.Width - location.X - 1, location.Y);
            drawGraphics.FillRectangle(drawBrush, pixelPoint.X, pixelPoint.Y, 1, 1);

            // Inverts the click location vertically and draws another pixel.
            pixelPoint = new(location.X, imageSize.Height - location.Y - 1);
            drawGraphics.FillRectangle(drawBrush, pixelPoint.X, pixelPoint.Y, 1, 1);

            // Inverts the click location diagonally and draws another pixel.
            pixelPoint = new(imageSize.Width - location.X - 1, imageSize.Height - location.Y - 1);
            drawGraphics.FillRectangle(drawBrush, pixelPoint.X, pixelPoint.Y, 1, 1);
        }

        protected override void DrawMirrorPixel(Graphics drawGraphics, SolidBrush drawBrush, Point location, Size imageSize, int zoom)
        {
            // Draws the first pixel, in the click location.
            Point pixelPoint = new(location.X * zoom, location.Y * zoom);
            drawGraphics.FillRectangle(drawBrush, pixelPoint.X, pixelPoint.Y, zoom, zoom);

            // Inverts the click location horizontally and draws another pixel.
            pixelPoint = new((imageSize.Width - location.X - 1) * zoom, location.Y * zoom);
            drawGraphics.FillRectangle(drawBrush, pixelPoint.X, pixelPoint.Y, zoom, zoom);

            // Inverts the click location vertically and draws another pixel.
            pixelPoint = new(location.X * zoom, (imageSize.Height - location.Y - 1) * zoom);
            drawGraphics.FillRectangle(drawBrush, pixelPoint.X, pixelPoint.Y, zoom, zoom);

            // Inverts the click location diagonally and draws another pixel.
            pixelPoint = new((imageSize.Width - location.X - 1) * zoom, (imageSize.Height - location.Y - 1) * zoom);
            drawGraphics.FillRectangle(drawBrush, pixelPoint.X, pixelPoint.Y, zoom, zoom);
        }
    }
}
