namespace PixelArtEditor.Drawing_Tools.Tools
{
    internal class VerticalMirrorPenTool : DrawingTool
    {
        public override void UseTool(Graphics imageGraphics, Brush colorBrush, int pixelSize, Point? beginPoint, Point? endPoint, Size? pictureSize)
        {
            if (beginPoint.HasValue && pictureSize.HasValue)
            {
                Point pixelPoint = SnapPixelTopLeft(beginPoint.Value, pixelSize);
                DrawPixel(imageGraphics, colorBrush, pixelPoint.X, pixelPoint.Y, pixelSize);

                beginPoint = new(beginPoint.Value.X, pictureSize.Value.Height - beginPoint.Value.Y - 1);
                pixelPoint = SnapPixelTopLeft(beginPoint.Value, pixelSize);
                DrawPixel(imageGraphics, colorBrush, pixelPoint.X, pixelPoint.Y, pixelSize);
            }
        }
    }
}
