namespace PixelArtEditor.Drawing_Tools.Tools
{
    internal class EraserTool : DrawingTool
    {
        private static void ErasePixel(Graphics graphics, OptionalToolParameters parameters)
        {
            Point pixelPoint = SnapPixelTopLeft(parameters.BeginPoint.Value, parameters.PixelSize.Value);
            Rectangle eraseArea = new(pixelPoint, new Size(parameters.PixelSize.Value, parameters.PixelSize.Value));
            graphics.SetClip(eraseArea);
            if (parameters.Transparency.Value)
            {
                graphics.Clear(Color.Transparent);
            }
            else
            {
                graphics.Clear(parameters.BackgroundColor.Value);
            }
        }

        public override void PreviewTool(Graphics paintGraphics, SolidBrush colorBrush, OptionalToolParameters toolParameters)
        {
            if (toolParameters.BeginPoint.HasValue && toolParameters.PixelSize.HasValue)
            {
                using SolidBrush outerBrush = new(Color.Black);
                using SolidBrush innerBrush = new(Color.White);

                Point pixelPoint = SnapPixelTopLeft(toolParameters.BeginPoint.Value, toolParameters.PixelSize.Value);
                DrawPixel(paintGraphics, outerBrush, pixelPoint.X, pixelPoint.Y, toolParameters.PixelSize.Value);
                DrawPixel(paintGraphics, innerBrush, pixelPoint.X + 1, pixelPoint.Y + 1, toolParameters.PixelSize.Value - 2);
            }
        }

        public override void UseToolClick(Graphics imageGraphics, SolidBrush colorBrush, OptionalToolParameters toolParameters)
        {
            if (toolParameters.BeginPoint.HasValue && toolParameters.BackgroundColor.HasValue && toolParameters.Transparency.HasValue && toolParameters.PixelSize.HasValue)
            {
                ErasePixel(imageGraphics, toolParameters);
            }
        }

        public override void UseToolHold(Graphics imageGraphics, SolidBrush colorBrush, OptionalToolParameters toolParameters)
        {
            if (toolParameters.BeginPoint.HasValue && toolParameters.BackgroundColor.HasValue && toolParameters.Transparency.HasValue && toolParameters.PixelSize.HasValue)
            {
                ErasePixel(imageGraphics, toolParameters);
            }
        }

        public override void UseToolRelease(Graphics imageGraphics, SolidBrush colorBrush, OptionalToolParameters toolParameters)
        {
            return;
        }
    }
}