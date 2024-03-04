
namespace PixelArtEditor.Drawing_Tools.Tools
{
    internal class PixelPenTool : DrawingTool
    {
        public override void PreviewTool(Graphics paintGraphics, SolidBrush colorBrush, OptionalToolParameters toolParameters)
        {
            if (toolParameters.BeginPoint.HasValue && toolParameters.PixelSize.HasValue)
            {
                colorBrush = MakePreviewBrush(colorBrush);
                Point pixelPoint = SnapPixelTopLeft(toolParameters.BeginPoint.Value, toolParameters.PixelSize.Value);
                DrawPixel(paintGraphics, colorBrush, pixelPoint.X, pixelPoint.Y, toolParameters.PixelSize.Value);
            }
        }

        public override void UseToolClick(Graphics imageGraphics, SolidBrush colorBrush, OptionalToolParameters toolParameters)
        {
            if (toolParameters.BeginPoint.HasValue && toolParameters.PixelSize.HasValue)
            {
                Point pixelPoint = SnapPixelTopLeft(toolParameters.BeginPoint.Value, toolParameters.PixelSize.Value);
                DrawPixel(imageGraphics, colorBrush, pixelPoint.X, pixelPoint.Y, toolParameters.PixelSize.Value);
            }
        }

        public override void UseToolHold(Graphics imageGraphics, SolidBrush colorBrush, OptionalToolParameters toolParameters)
        {
            if (toolParameters.BeginPoint.HasValue && toolParameters.PixelSize.HasValue)
            {
                Point pixelPoint = SnapPixelTopLeft(toolParameters.BeginPoint.Value, toolParameters.PixelSize.Value);
                DrawPixel(imageGraphics, colorBrush, pixelPoint.X, pixelPoint.Y, toolParameters.PixelSize.Value);
            }
        }

        public override void UseToolRelease(Graphics imageGraphics, SolidBrush colorBrush, OptionalToolParameters toolParameters)
        {
            return;
        }
    }
}
