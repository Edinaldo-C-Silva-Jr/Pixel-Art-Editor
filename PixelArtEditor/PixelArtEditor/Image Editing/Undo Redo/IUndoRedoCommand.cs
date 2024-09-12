namespace PixelArtEditor.Image_Editing.Undo_Redo
{
    /// <summary>
    /// An interface that defines the methods used for an Undo/Redo Command.
    /// The command is capable of rolling back and executing a specific change to the image made by a drawing tool.
    /// </summary>
    public interface IUndoRedoCommand
    {
        /// <summary>
        /// Executes the changes made by a specific tool on a specific drawing cycle.
        /// </summary>
        /// <param name="imageGraphics">The graphics of the image where the changes should be executed.</param>
        public void ExecuteChange(Graphics imageGraphics);

        /// <summary>
        /// Rolls back the changes made by a specific tool on a specific drawing cycle, reverting the image to the way it was before the cycle started.
        /// </summary>
        /// <param name="imageGraphics">The graphics of the image where the changes should be rolled back.</param>
        public void RollbackChange(Graphics imageGraphics);
    }
}
