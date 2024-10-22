using PixelArtEditor.Image_Editing.Undo_Redo;

namespace PixelArtEditor.Image_Editing.Image_Tools
{
    public abstract class BaseImageTool : IOriginalImageTool, IUndoRedoCreator
    {
        public abstract void UseTool(Bitmap OriginalImage, OptionalImageParameters parameters);
        
        public abstract IUndoRedoCommand CreateUndoStep(Point drawingImageLocation);
    }
}
