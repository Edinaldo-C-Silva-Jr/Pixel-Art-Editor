namespace PixelArtEditor.Image_Editing.Undo_Redo
{
    public class UndoRedoHandler
    {
        private Stack<IUndoRedoCommand> UndoCommands { get; set; }
        private Stack<IUndoRedoCommand> RedoCommands { get; set; }

        public UndoRedoHandler()
        {
            UndoCommands = new();
            RedoCommands = new();
        }

        public void TrackChange(IUndoRedoCommand command)
        {
            UndoCommands.Push(command);
            RedoCommands.Clear();
        }

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
    }
}
