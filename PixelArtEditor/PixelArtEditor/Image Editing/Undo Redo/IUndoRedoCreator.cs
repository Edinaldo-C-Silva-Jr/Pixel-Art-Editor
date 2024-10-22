namespace PixelArtEditor.Image_Editing.Undo_Redo
{
    /// <summary>
    /// An interface that defines the methods used for a class that creates Undo/Redo Commands.
    /// </summary>
    public interface IUndoRedoCreator
    {
        /// <summary>
        /// Creates an Undo step with the data from a concluded drawing cycle.
        /// Gives the command all the data necessary to undo and redo the action by itself.
        /// </summary>
        /// <param name="parameters">The optional parameters that can be used by the implementation classes.</param>
        /// <returns></returns>
        abstract public IUndoRedoCommand? CreateUndoStep(UndoParameters parameters);
    }
}
