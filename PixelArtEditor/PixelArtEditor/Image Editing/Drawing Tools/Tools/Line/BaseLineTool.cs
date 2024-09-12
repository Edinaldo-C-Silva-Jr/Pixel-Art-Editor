using PixelArtEditor.Image_Editing.Undo_Redo;

namespace PixelArtEditor.Image_Editing.Drawing_Tools.Tools.Line
{
    public abstract class BaseLineTool : DrawingTool
    {
        /// <summary>
        /// The point where the line begins, which is where the mouse is first clicked.
        /// </summary>
        private Point? StartingPoint { get; set; }

        /// <summary>
        /// The point where the line ends, which is where the mouse is last clicked.
        /// </summary>
        private Point? EndPoint { get; set; }

        /// <summary>
        /// The Graphics object used for a drawing cycle.
        /// </summary>
        private Graphics? DrawingCycleGraphics { get; set; }

        /// <summary>
        /// The brush object used for a drawing cycle.
        /// </summary>
        private SolidBrush? DrawingBrush { get; set; }

        protected abstract void DrawLine(Graphics drawGraphics, SolidBrush drawBrush, Point location);

        protected abstract void DrawLine(Graphics drawGraphics, SolidBrush drawBrush, Point location, int pixelSize);

        public override void PreviewTool(Graphics paintGraphics, Color drawingColor, OptionalToolParameters toolParameters)
        {
            if (StartingPoint.HasValue && toolParameters.ClickLocation.HasValue && toolParameters.PixelSize.HasValue)
            {
                using SolidBrush previewBrush = MakePreviewBrush(drawingColor);
                DrawLine(paintGraphics, previewBrush, toolParameters.ClickLocation.Value, toolParameters.PixelSize.Value);
            }
        }

        public override void UseToolClick(Bitmap drawingImage, Color drawingColor, OptionalToolParameters toolParameters)
        {
            if (toolParameters.ClickLocation.HasValue)
            {
                // Preparing drawing properties.
                DrawingCycleGraphics = Graphics.FromImage(drawingImage);
                DrawingBrush = new(drawingColor);
                StartingPoint = toolParameters.ClickLocation.Value;
                EndPoint = toolParameters.ClickLocation.Value;
            }
        }

        public override void UseToolHold(OptionalToolParameters toolParameters)
        {
            if (StartingPoint.HasValue && toolParameters.ClickLocation.HasValue && toolParameters.PixelSize.HasValue)
            {
                // Preparing drawing properties.
                EndPoint = toolParameters.ClickLocation.Value;
            }
            return;
        }

        public override void UseToolRelease(OptionalToolParameters toolParameters)
        {
            if (StartingPoint.HasValue && toolParameters.ClickLocation.HasValue && toolParameters.PixelSize.HasValue)
            {
                // Preparing drawing properties.
                EndPoint = toolParameters.ClickLocation.Value;

                DrawLine(DrawingCycleGraphics!, DrawingBrush!, toolParameters.ClickLocation.Value, toolParameters.PixelSize.Value);
            }
        }
        public override IUndoRedoCommand CreateUndoStep(Point drawingImageLocation)
        {
            throw new NotImplementedException();
        }

        protected void ClearPropertie()
        {
            StartingPoint = EndPoint = null;

            DrawingCycleGraphics?.Dispose();
            DrawingCycleGraphics = null;
            DrawingBrush?.Dispose();
            DrawingBrush = null;
        }
    }
}
