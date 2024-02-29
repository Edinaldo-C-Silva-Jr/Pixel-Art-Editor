﻿
namespace PixelArtEditor.Drawing_Tools.Tools
{
    internal class PixelPenTool : DrawingTool
    {
        public override void UseTool(Graphics imageGraphics, Brush colorBrush, int pixelSize, Point? startingPosition = null, Point? endPosition = null, Size? pictureSize = null)
        {
            if (startingPosition.HasValue)
            {
                DrawPixel(imageGraphics, colorBrush, startingPosition.Value.X, startingPosition.Value.Y, pixelSize);
            }
        }
    }
}
