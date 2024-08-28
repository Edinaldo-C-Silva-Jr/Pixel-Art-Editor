namespace PixelArtEditor.Image_Editing.Undo_Redo
{
    public interface IUndoRedoCreator
    {
        abstract public IUndoRedoCommand CreateUndoStep(Point drawingImageLocation);
    }
}
