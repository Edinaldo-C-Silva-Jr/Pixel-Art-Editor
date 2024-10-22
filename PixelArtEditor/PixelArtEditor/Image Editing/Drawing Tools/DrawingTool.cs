using PixelArtEditor.Image_Editing.Undo_Redo;

namespace PixelArtEditor.Image_Editing.Drawing_Tools
{
    /// <summary>
    /// A base implementation of a Drawing Tool, that has some of the common methods used by most other tools.
    /// </summary>
    abstract public class DrawingTool : IDrawingTool, IUndoRedoCreator
    {
        /// <summary>
        /// Creates a brush to use in the preview tool methods. This brush will have a darker or lighter version of the color.
        /// </summary>
        /// <param name="brushColor">The color to use in the brush.</param>
        /// <returns>A new brush with a lighter or darker version of the original color.</returns>
        protected static SolidBrush MakePreviewBrush(Color brushColor)
        {
            // Formula to get the brightness of a color. Values range from 0 to 255.
            // The green component has the most influence in the color's brightness, and the blue component has the least influence.
            decimal brightness = 0.2126M * brushColor.R + 0.7152M * brushColor.G + 0.0722M * brushColor.B;

            // Make the color lighter if it's dark, and darker if it's light.
            Color previewColor;
            if (brightness < 64)
            {
                previewColor = ControlPaint.Light(brushColor, 1.0f);
            } 
            else if (brightness < 128)
            {
                previewColor = ControlPaint.Light(brushColor, 0.4f);
            } 
            else if(brightness < 196)
            {
                previewColor = ControlPaint.Dark(brushColor, 0.05f);
            }
            else
            {
                previewColor = ControlPaint.Dark(brushColor, 0.1f);
            }
            
            return new SolidBrush(previewColor);
        }

        abstract public void PreviewTool(Graphics paintGraphics, Color drawingColor, DrawingToolParameters parameters);

        abstract public void UseToolClick(Bitmap drawingImage, Color drawingColor, DrawingToolParameters parameters);

        abstract public void UseToolHold(DrawingToolParameters parameters);

        abstract public void UseToolRelease(DrawingToolParameters parameters);

        abstract public IUndoRedoCommand? CreateUndoStep(UndoParameters parameters);
    }
}
