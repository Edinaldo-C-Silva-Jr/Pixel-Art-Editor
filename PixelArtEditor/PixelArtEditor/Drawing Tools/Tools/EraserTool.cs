namespace PixelArtEditor.Drawing_Tools.Tools
{
    internal class EraserTool : DrawingTool
    {
        public override void UseTool(Graphics imageGraphics, Brush colorBrush, int pixelSize, OptionalToolParameters toolParameters)
        {
            if (toolParameters.BeginPoint.HasValue && toolParameters.BackgroundColor.HasValue && toolParameters.Transparency.HasValue)
            {
                Point pixelPoint = SnapPixelTopLeft(toolParameters.BeginPoint.Value, pixelSize);
                Rectangle eraseArea = new(pixelPoint, new(pixelSize, pixelSize));
                imageGraphics.SetClip(eraseArea);
                if (toolParameters.Transparency.Value)
                {
                    imageGraphics.Clear(Color.Transparent);
                }
                else
                {
                    imageGraphics.Clear(toolParameters.BackgroundColor.Value);
                }
            }
        }
    }
}