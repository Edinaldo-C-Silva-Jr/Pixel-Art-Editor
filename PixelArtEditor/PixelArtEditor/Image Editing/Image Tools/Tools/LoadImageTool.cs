using PixelArtEditor.Files.File_Forms;
using PixelArtEditor.Image_Editing.Undo_Redo;

namespace PixelArtEditor.Image_Editing.Image_Tools.Tools
{
    public class LoadImageTool : IImageTool, IUndoRedoCreator
    {
        public void UseTool(Bitmap originalImage, ImageToolParameters parameters)
        {
            string directory = "C:\\Users\\" + Environment.UserName + "\\Documents\\PixelEditor\\SavedImages\\";
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            using OpenFileDialog openDialog = new()
            {
                InitialDirectory = directory,
                Title = "Load an image into the editor"
            };

            using LoadImageForm loadImageForm = new(openDialog);
            DialogResult result = loadImageForm.ShowDialog();

            if (result == DialogResult.OK)
            {
                using Bitmap imageLoaded = new(loadImageForm.ImageLoaded!);
                using Graphics originalImageGraphics = Graphics.FromImage(originalImage);
                originalImageGraphics.DrawImage(imageLoaded, 0, 0);
            }
        }

        public IUndoRedoCommand? CreateUndoStep(UndoParameters parameters)
        {
            throw new NotImplementedException();
        }
    }
}
