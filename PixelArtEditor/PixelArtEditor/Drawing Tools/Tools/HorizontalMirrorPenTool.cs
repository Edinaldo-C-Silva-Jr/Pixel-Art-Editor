namespace PixelArtEditor.Drawing_Tools.Tools
{
    internal class HorizontalMirrorPenTool : DrawingTool
    {
        public override void UseTool(Graphics imageGraphics, Brush colorBrush, int pixelSize, Point? beginPoint, Point? endPoint, Size? pictureSize)
        {
            if (beginPoint.HasValue && pictureSize.HasValue)
            {
                Point pixelPoint = SnapPixelTopLeft(beginPoint.Value, pixelSize);
                DrawPixel(imageGraphics, colorBrush, pixelPoint.X, pixelPoint.Y, pixelSize);

                beginPoint = new(pictureSize.Value.Width - beginPoint.Value.X - 1, beginPoint.Value.Y);
                pixelPoint = SnapPixelTopLeft(beginPoint.Value, pixelSize);
                DrawPixel(imageGraphics, colorBrush, pixelPoint.X, pixelPoint.Y, pixelSize);
            }
        }
    }
}
