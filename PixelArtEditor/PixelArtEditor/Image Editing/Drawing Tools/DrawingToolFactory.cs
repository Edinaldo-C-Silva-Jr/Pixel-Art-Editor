using PixelArtEditor.Image_Editing.Drawing_Tools.Tools.Pen;
using PixelArtEditor.Image_Editing.Drawing_Tools.Tools.Line;

namespace PixelArtEditor.Image_Editing.Drawing_Tools
{
    /// <summary>
    /// A Factory that generates Drawing Tools.
    /// </summary>
    internal class DrawingToolFactory
    {
        /// <summary>
        /// The currently generated tool. It is stored to be reused throughout the application for as long as no new tool is selected.
        /// </summary>
        private IDrawingTool Tool { get; set; }

        /// <summary>
        /// Default constructor. The default tool chosen is a Pixel Pen.
        /// </summary>
        public DrawingToolFactory()
        {
            Tool = new PixelPenTool();
        }

        /// <summary>
        /// Returns the current tool available, which is the last one generated.
        /// </summary>
        /// <returns>The last generated Drawing Tool.</returns>
        public IDrawingTool GetTool()
        {
            return Tool;
        }

        /// <summary>
        /// Generates a new Drawing Tool based on the value passed.
        /// </summary>
        /// <param name="toolValue">The value of the tool to be used.</param>
        /// <returns>A new instance of the tool that corresponds to the tool value.</returns>
        public IDrawingTool ChangeCurrentTool(int toolValue)
        {
            Tool = toolValue switch
            {
                /*1 => new HorizontalMirrorPenTool(),
                2 => new VerticalMirrorPenTool(),
                3 => new FullMirrorPenTool(),
                4 => new FourMirrorPenTool(),
                5 => new EraserTool(),
                6 => new CardinalLineTool(),
                7 => new OrdinalLineTool(),
                8 => new FreeLineTool(),
                9 => new SolidRectangleTool(),
                10 => new OutlineRectangleTool(),*/
                3 => new OrdinalLineTool(),
                2 => new CardinalLineTool(),
                1 => new DitheringPenTool(),
                _ => new PixelPenTool()
            };

            return Tool;
        }
    }
}
