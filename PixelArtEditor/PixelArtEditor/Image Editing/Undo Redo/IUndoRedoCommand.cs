namespace PixelArtEditor.ImageEditing
{
    public interface IUndoRedoCommand
    {
        public void ExecuteChange();
        public void RollbackChange();
    }
}
