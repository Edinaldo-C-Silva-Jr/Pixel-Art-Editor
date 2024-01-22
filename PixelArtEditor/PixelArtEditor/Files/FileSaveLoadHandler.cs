using System.Drawing.Imaging;

namespace PixelArtEditor.Files
{
    internal class FileSaveLoadHandler
    {
        private SaveFileDialog _saveFileDialog { get; set; }
        private OpenFileDialog _loadFileDialog { get; set; }

        public FileSaveLoadHandler()
        {
            _saveFileDialog = new SaveFileDialog();
            _loadFileDialog = new OpenFileDialog();
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
    }
}
