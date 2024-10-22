namespace PixelArtEditor.Image_Editing.Undo_Redo
{
    /// <summary>
    /// A class that handles the storage and execution of Undo and Redo steps.
    /// </summary>
    public class UndoRedoHandler
    {
        #region Properties
        /// <summary>
        /// The stack of Undo commands to execute, ordered as they're created in the application.
        /// </summary>
        private Stack<IUndoRedoCommand> UndoCommands { get; set; }

        /// <summary>
        /// The stack of Redo commands to execute, ordered as they're created in the application.
        /// </summary>
        private Stack<IUndoRedoCommand> RedoCommands { get; set; }
        #endregion

        /// <summary>
        /// Default constructor.
        /// </summary>
        public UndoRedoHandler()
        {
            UndoCommands = new();
            RedoCommands = new();
        }

        /// <summary>
        /// Tracks a new change made in the editor, adding a new Undo command to the stack.
        /// It also clears the stack of Redo commands, since the new change is the latest.
        /// </summary>
        /// <param name="command">The command created for a new change made in the editor.</param>
        public void TrackChange(IUndoRedoCommand? command)
        {
            if (command is not null)
            {
                UndoCommands.Push(command);
                RedoCommands.Clear();
            }
        }

        /// <summary>
        /// Rolls back a change previously made, which is the latest change in the Undo command stack.
        /// This change is then added to the Redo stack.
        /// </summary>
        /// <param name="originalImage">The image where the change should be rolled back.</param>
        public void UndoChange(Bitmap originalImage)
        {
            if (UndoCommands.Count == 0)
            {
                return;
            }

            using Graphics imageGraphics = Graphics.FromImage(originalImage);

            IUndoRedoCommand command = UndoCommands.Pop();
            command.RollbackChange(imageGraphics);
            RedoCommands.Push(command);
        }

        /// <summary>
        /// Executes a change previously undone, which is the latest change in the Redo command stack.
        /// The change is then returned to the Undo stack.
        /// </summary>
        /// <param name="originalImage">The image where the change should be executed.</param>
        public void RedoChange(Bitmap originalImage)
        {
            if (RedoCommands.Count == 0)
            {
                return;
            }

            using Graphics imageGraphics = Graphics.FromImage(originalImage);

            IUndoRedoCommand command = RedoCommands.Pop();
            command.ExecuteChange(imageGraphics);
            UndoCommands.Push(command);
        }

        /// <summary>
        /// Returns whether there are Undo and/or Redo commands available.
        /// </summary>
        /// <returns>A tuple of bool values, which tell if there are any Undo or Redo commands available, respectively.</returns>
        public (bool undoAvailable, bool redoAvailable) UndoRedoAvailable()
        {
            return (UndoCommands.Count > 0, RedoCommands.Count > 0);
        }
    }
}
