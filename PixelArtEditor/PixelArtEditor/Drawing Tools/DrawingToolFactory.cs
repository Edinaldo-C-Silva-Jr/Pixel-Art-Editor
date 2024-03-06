using PixelArtEditor.Drawing_Tools.Tools;

namespace PixelArtEditor.Drawing_Tools
{
    internal class DrawingToolFactory
    {
        private IDrawingTool Tool { get; set; }

        public DrawingToolFactory()
        {
            Tool = new PixelPenTool();
        }

        public IDrawingTool GetTool()
        {
            return Tool;
        }

        public IDrawingTool ChangeCurrentTool(int currentButton)
        {
            Tool = currentButton switch
            {
                1 => new HorizontalMirrorPenTool(),
                2 => new VerticalMirrorPenTool(),
                3 => new FullMirrorPenTool(),
                4 => new FourMirrorPenTool(),
                5 => new EraserTool(),
                6 => new CardinalLineTool(),
                _ => new PixelPenTool()
            };

            return Tool;
        }
    }
}
