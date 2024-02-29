namespace PixelArtEditor.Drawing_Tools.Tools
{
    internal class MirrorPenTool : DrawingTool
    {
        public override void UseTool(Graphics imageGraphics, Brush colorBrush, int pixelSize, Point? startingPosition = null, Point? endPosition = null, Size? pictureSize = null)
        {
            if (startingPosition.HasValue && pictureSize.HasValue)
            {
                DrawPixel(imageGraphics, colorBrush, startingPosition.Value.X, startingPosition.Value.Y, pixelSize);
                DrawPixel(imageGraphics, colorBrush, pictureSize.Value.Width - startingPosition.Value.X, pictureSize.Value.Height - startingPosition.Value.Y, pixelSize);
            }
        }
    }
}
