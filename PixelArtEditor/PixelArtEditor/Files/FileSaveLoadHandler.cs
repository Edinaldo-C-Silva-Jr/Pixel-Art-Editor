using PixelArtEditor.Grids;
using System.Drawing.Imaging;

namespace PixelArtEditor.Files
{
    internal class FileSaveLoadHandler
    {
        private SaveFileDialog _saveFileDialog { get; set; }
        private OpenFileDialog _openFileDialog { get; set; }

        public FileSaveLoadHandler()
        {
            _saveFileDialog = new SaveFileDialog();
            _openFileDialog = new OpenFileDialog();
        }

        private static string DefineFileDirectory(string directoryName)
        {
            string directory = "C:\\Users\\" + Environment.UserName + "\\Documents\\PixelEditor\\" + directoryName + "\\";
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            return directory;
        }

        public void SaveImage(Bitmap originalImage)
        {
            string directory = DefineFileDirectory("SavedImages");

            _saveFileDialog.InitialDirectory = directory;
            _saveFileDialog.FileName = "PixelImage_" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-ff") + ".png";
            _saveFileDialog.Filter = "PNG Image|*.png";
            _saveFileDialog.DefaultExt = "png";
            _saveFileDialog.AddExtension = true;
            _saveFileDialog.Title = "Save the current image";
            DialogResult result = _saveFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                string nameOfFile = _saveFileDialog.FileName;
                originalImage.Save(nameOfFile, ImageFormat.Png);
            }
        }

        public void SavePalette(string paletteValues)
        {
            string directory = DefineFileDirectory("SavedPalettes");

            _saveFileDialog.InitialDirectory = directory;
            _saveFileDialog.FileName = "Palette_" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-ff") + ".txt";
            _saveFileDialog.Filter = "Text file|*.txt";
            _saveFileDialog.DefaultExt = "txt";
            _saveFileDialog.AddExtension = true;
            _saveFileDialog.Title = "Save the current color palette";
            DialogResult result = _saveFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                string nameOfFile = _saveFileDialog.FileName;
                File.WriteAllText(nameOfFile, paletteValues);
            }
        }

        public Bitmap LoadImage()
        {
            {
                string directory = DefineFileDirectory("SavedImages");

                _openFileDialog.InitialDirectory = directory;
                _openFileDialog.Title = "Load an image into the editor";
                DialogResult result = _openFileDialog.ShowDialog();

                Bitmap imageToOpen = null;
                if (result == DialogResult.OK)
                {
                    imageToOpen = new(_openFileDialog.FileName);
                }
                return imageToOpen;
            }
        }

        public string LoadPalette()
        {
            string directory = DefineFileDirectory("SavedPalettes");
            string paletteValues = string.Empty;

            _openFileDialog.InitialDirectory = directory;
            _openFileDialog.Title = "Load a color palette";
            DialogResult result = _openFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                paletteValues = File.ReadAllText(_openFileDialog.FileName);
            }

            return paletteValues;
        }
    }
}
