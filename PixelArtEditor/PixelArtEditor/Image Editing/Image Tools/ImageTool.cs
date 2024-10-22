using PixelArtEditor.Image_Editing.Undo_Redo;

namespace PixelArtEditor.Image_Editing.Image_Tools
{
    public abstract class ImageTool : IOriginalImageTool, IUndoRedoCreator
    {
        public abstract IUndoRedoCommand CreateUndoStep(Point drawingImageLocation);

        public abstract void UseTool(Bitmap OriginalImage);
    }
}
