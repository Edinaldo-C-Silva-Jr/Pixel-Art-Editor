namespace PixelArtEditor.Image_Editing.Undo_Redo
{
    public interface IUndoRedoCommand
    {
        public void ExecuteChange(Graphics imageGraphics);
        public void RollbackChange(Graphics imageGraphics);
    }
}
