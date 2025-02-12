﻿namespace PixelArtEditor.Image_Editing.Drawing_Tools
{
    /// <summary>
    /// An interface that defines the methods used by the Drawing Tools.
    /// These tools are used to draw onto the Drawing Image.
    /// </summary>
    public interface IDrawingTool
    {
        /// <summary>
        /// Implements the preview of the tool's usage on the Drawing Box.
        /// </summary>
        /// <param name="paintGraphics">The Drawing Box's paint event graphics.</param>
        /// <param name="pixelColor">The color to use to draw on the image.</param>
        /// <param name="toolParameters">The optional parameters used by the current tool.</param>
        public void PreviewTool(Graphics paintGraphics, Color pixelColor, DrawingToolParameters toolParameters);

        /// <summary>
        /// Implements the function of the tool when the mouse button is pressed.
        /// </summary>
        /// <param name="drawingImage">The image that will be drawn on.</param>
        /// <param name="drawingColor">The color to use to draw on the image.</param>
        /// <param name="toolParameters">The optional parameters used by the current tool.</param>
        public void UseToolClick(Bitmap drawingImage, Color drawingColor, DrawingToolParameters toolParameters);

        /// <summary>
        /// Implements the function of the tool when the mouse button is held down.
        /// </summary>
        /// <param name="toolParameters">The optional parameters used by the current tool.</param>
        public void UseToolHold(DrawingToolParameters toolParameters);

        /// <summary>
        /// Implements the function of the tool when the mouse button is released.
        /// </summary>
        /// <param name="toolParameters">The optional parameters used by the current tool.</param>
        public void UseToolRelease(DrawingToolParameters toolParameters);
    }
}
