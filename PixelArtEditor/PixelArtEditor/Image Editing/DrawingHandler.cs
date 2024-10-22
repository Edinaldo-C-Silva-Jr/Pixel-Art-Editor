using PixelArtEditor.Image_Editing.Drawing_Tools;
using PixelArtEditor.Image_Editing.Undo_Redo;

namespace PixelArtEditor.Image_Editing
{
    /// <summary>
    /// A class that handles the call to drawing methods from drawing tools.
    /// </summary>
    public class DrawingHandler
    {
        /// <summary>
        /// Keeps track of whether the mouse has been clicked in the current drawing cycle or not.
        /// </summary>
        private bool MouseClicked { get; set; }

        /// <summary>
        /// Executes the drawing tool's Click method.
        /// Marks the beginning of a drawing cycle, which represents the string of actions between clicking, holding and releasing the mouse while drawing.
        /// </summary>
        /// <param name="tool">The drawing tool to be used.</param>
        /// <param name="image">The image that is being drawn on.</param>
        /// <param name="pixelColor">The color to be used by the tool.</param>
        /// <param name="toolParameters">The parameters to be used by the current tool.</param>
        public void DrawClick(IDrawingTool tool, Bitmap image, Color pixelColor, DrawingToolParameters toolParameters)
        {
            MouseClicked = true;

            tool.UseToolClick(image, pixelColor, toolParameters);
        }

        /// <summary>
        /// Executes the tool's Mouse Hold method.
        /// This event continues the drawing cycle started by DrawClick.
        /// </summary>
        /// <param name="tool">The drawing tool to be used.</param>
        /// <param name="toolParameters">The parameters to be used by the current tool.</param>
        public void DrawHold(IDrawingTool tool, DrawingToolParameters toolParameters)
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
        /// This concludes the current drawing cycle as far as drawing is concerned.
        /// </summary>
        /// <param name="tool">The drawing tool to be used.</param>
        /// <param name="toolParameters">The parameters to be used by the current tool.</param>
        public void DrawRelease(IDrawingTool tool, DrawingToolParameters toolParameters)
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
        public void PreviewTool(IDrawingTool tool, Graphics paintGraphics, Color pixelColor, DrawingToolParameters toolParameters)
        {
            // Changes the CompositingMode to SourceCopy to make the preview brush solid.
            paintGraphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
            
            tool.PreviewTool(paintGraphics, pixelColor, toolParameters);

            paintGraphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
        }

        /// <summary>
        /// Creates an undo step.
        /// Takes the data from the concluded drawing cycle and creates a command object that's capable of rolling back and executing the changes.
        /// </summary>
        /// <param name="tool">The Drawing Tool used in the concluded drawing cycle.</param>
        /// <param name="drawingImageLocation">The location from where the Drawing Image was taken, to keep track of where the undo should be done.</param>
        /// <returns></returns>
        public IUndoRedoCommand? CreateUndoStepFromTool(IUndoRedoCreator tool, UndoParameters parameters)
        {
            return tool.CreateUndoStep(parameters);
        }
    }
}
