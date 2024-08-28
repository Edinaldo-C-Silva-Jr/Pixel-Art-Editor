using PixelArtEditor.Drawing_Tools;

namespace PixelArtEditor.Image_Editing
{
    public class DrawingHandler
    {
        /// <summary>
        /// Keeps track of whether the mouse has been clicked in the current drawing cycle or not.
        /// </summary>
        private bool MouseClicked { get; set; }

        /// <summary>
        /// Executes the drawing tool's Click method.
        /// Marks the beginning of a drawing cycle, and creates the Graphics and Brush objects to be used in it.
        /// The drawing cycle represents the string of actions between clicking, holding and releasing the mouse while drawing.
        /// </summary>
        /// <param name="tool">The drawing tool to be used.</param>
        /// <param name="image">The image that is being drawn on.</param>
        /// <param name="pixelColor">The color to be used by the tool.</param>
        /// <param name="toolParameters">The parameters to be used by the current tool.</param>
        public void DrawClick(IDrawingTool tool, Bitmap image, Color pixelColor, OptionalToolParameters toolParameters)
        {
            MouseClicked = true;

            tool.UseToolClick(image, pixelColor, toolParameters);
        }

        /// <summary>
        /// Executes the tool's Mouse Hold method.
        /// This event continues the drawing cycle started by DrawClick, utilizing the same Graphics and Brush already created by it.
        /// </summary>
        /// <param name="tool">The drawing tool to be used.</param>
        /// <param name="toolParameters">The parameters to be used by the current tool.</param>
        public void DrawHold(IDrawingTool tool, OptionalToolParameters toolParameters)
        {
            // Only executes the drawing tool if the mouse has been clicked in the control before, to ensure the graphics and brush objects are valid.
            if (!MouseClicked)
            {
                return;
            }

            tool.UseToolHold(toolParameters);
        }

        /// <summary>
        /// Executes the tool's Mouse Release method.
        /// This concludes the current drawing cycle, by disposing of the Graphics and Brush used.
        /// </summary>
        /// <param name="tool">The drawing tool to be used.</param>
        /// <param name="toolParameters">The parameters to be used by the current tool.</param>
        public void DrawRelease(IDrawingTool tool, OptionalToolParameters toolParameters)
        {
            // Only executes the drawing tool if the mouse has been clicked in the control before, to ensure the graphics and brush objects are valid.
            if (!MouseClicked)
            {
                return;
            }

            tool.UseToolRelease(toolParameters);

            MouseClicked = false;
        }

        /// <summary>
        /// Executes the tool's Preview method.
        /// </summary>
        /// <param name="tool">The drawing tool to be used.</param>
        /// <param name="paintGraphics">The graphics from the Paint event in the DrawingBox.</param>
        /// <param name="pixelColor">The color to be used by the tool.</param>
        /// <param name="toolParameters">The parameters to be used by the current tool.</param>
        public void PreviewTool(IDrawingTool tool, Graphics paintGraphics, Color pixelColor, OptionalToolParameters toolParameters)
        {
            // Changes the CompositingMode to SourceCopy to make the preview brush solid.
            paintGraphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
            
            tool.PreviewTool(paintGraphics, pixelColor, toolParameters);

            paintGraphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
        }
    }
}
