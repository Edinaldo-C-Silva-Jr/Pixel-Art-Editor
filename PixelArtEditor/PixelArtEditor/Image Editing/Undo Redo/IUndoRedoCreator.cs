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
        /// <param name="drawingImageLocation">The location of the original image where the drawing image is taken from.</param>
        /// <returns></returns>
        abstract public IUndoRedoCommand CreateUndoStep(Point drawingImageLocation);
    }
}
