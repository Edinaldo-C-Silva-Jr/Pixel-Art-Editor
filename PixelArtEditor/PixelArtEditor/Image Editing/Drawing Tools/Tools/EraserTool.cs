/*namespace PixelArtEditor.Drawing_Tools.Tools
{
    /// <summary>
    /// An eraser tool that erases a single pixel in the image. It will either turn that pixel transparent, or restore it to the background color.
    /// </summary>
    internal class EraserTool : DrawingTool
    {
        /// <summary>
        /// Erases a pixel in the image, either repainting it to the background color, or to transparent.
        /// </summary>
        /// <param name="graphics">The graphics to draw the pixel on.</param>
        /// <param name="location">The location clicked on the image.</param>
        /// <param name="pixelSize">The size of each pixel in the image.</param>
        /// <param name="transparency">Whether the image is set to have a transparent or colored background.</param>
        /// <param name="backgroundColor">The color of the image's background.</param>
        private static void ErasePixel(Graphics graphics, Point location, int pixelSize, bool transparency, Color backgroundColor)
        {
            Point pixelPoint = SnapPixelTopLeft(location, pixelSize);
            Rectangle eraseArea = new(pixelPoint, new Size(pixelSize, pixelSize)); // Defines an area the size of the pixel to be erased.
            graphics.SetClip(eraseArea); // And sets the clip area of the graphics to that area.
            if (transparency)
            {
                graphics.Clear(Color.Transparent);
            }
            else
            {
                graphics.Clear(backgroundColor);
            }
        }

        public override void PreviewTool(Graphics paintGraphics, SolidBrush colorBrush, OptionalToolParameters toolParameters)
        {
            if (toolParameters.ClickLocation.HasValue && toolParameters.PixelSize.HasValue)
            {
                // The two brushes to use for the preview, white and black.
                using SolidBrush outerBrush = new(Color.Black);
                using SolidBrush innerBrush = new(Color.White);

                // Draws a white square with a black border as the preview for the eraser.
                Point pixelPoint = SnapPixelTopLeft(toolParameters.ClickLocation.Value, toolParameters.PixelSize.Value);
                DrawPixelAbsolute(paintGraphics, outerBrush, pixelPoint, toolParameters.PixelSize.Value);
                DrawPixelAbsolute(paintGraphics, innerBrush, new Point (pixelPoint.X + 1, pixelPoint.Y + 1), toolParameters.PixelSize.Value - 2);
            }
        }

        public override void UseToolClick(Graphics imageGraphics, SolidBrush colorBrush, OptionalToolParameters toolParameters)
        {
            if (toolParameters.ClickLocation.HasValue && toolParameters.PixelSize.HasValue && toolParameters.Transparency.HasValue && toolParameters.BackgroundColor.HasValue)
            {
                ErasePixel(imageGraphics, toolParameters.ClickLocation.Value, toolParameters.PixelSize.Value, toolParameters.Transparency.Value, toolParameters.BackgroundColor.Value);
            }
        }

        public override void UseToolHold(Graphics imageGraphics, SolidBrush colorBrush, OptionalToolParameters toolParameters)
        {
            if (toolParameters.ClickLocation.HasValue && toolParameters.PixelSize.HasValue && toolParameters.Transparency.HasValue && toolParameters.BackgroundColor.HasValue)
            {
                ErasePixel(imageGraphics, toolParameters.ClickLocation.Value, toolParameters.PixelSize.Value, toolParameters.Transparency.Value, toolParameters.BackgroundColor.Value);
            }
        }

        public override void UseToolRelease(Graphics imageGraphics, SolidBrush colorBrush, OptionalToolParameters toolParameters)
        {
            return;
        }
    }
}*/