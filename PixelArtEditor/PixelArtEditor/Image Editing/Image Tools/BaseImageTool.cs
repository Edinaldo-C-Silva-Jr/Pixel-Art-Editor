using PixelArtEditor.Image_Editing.Undo_Redo;

namespace PixelArtEditor.Image_Editing.Image_Tools
{
    /// <summary>
    /// A base implementation of an Image Tool, which defines all base methods used by these tools.
    /// </summary>
    public abstract class BaseImageTool : IImageTool, IUndoRedoCreator
    {
        public abstract void UseTool(Bitmap OriginalImage, ImageToolParameters parameters);
        
        public abstract IUndoRedoCommand? CreateUndoStep(UndoParameters parameters);
    }
}
