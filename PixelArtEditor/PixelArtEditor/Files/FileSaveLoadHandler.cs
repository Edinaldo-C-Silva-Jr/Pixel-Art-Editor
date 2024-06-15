using PixelArtEditor.Files.File_Forms;
using System.Drawing.Imaging;

namespace PixelArtEditor.Files
{
    /// <summary>
    /// A class that handles the saving and loading of files in the application, by using a SaveFileDialog and an OpenFileDialog.
    /// </summary>
    internal class FileSaveLoadHandler
    {
        /// <summary>
        /// The dialog used for saving the files, which can have its parameters set for the location, filename and extension of files that will be saved.
        /// </summary>
        private SaveFileDialog DialogForSavingFiles { get; set; }

        /// <summary>
        /// The dialog used for loading files, which can have its parameters set for the location and filename of the files that will be loaded.
        /// </summary>
        private OpenFileDialog DialogForOpeningFiles { get; set; }

        /// <summary>
        /// A constructor that instances the dialogs used in this class.
        /// </summary>
        public FileSaveLoadHandler()
        {
            DialogForSavingFiles = new SaveFileDialog
            {
                AddExtension = true
            };

            DialogForOpeningFiles = new OpenFileDialog();
        }

        /// <summary>
        /// A method to define the directory that will be used as the default location for each file type.
        /// It receives a directory name, and either creates or selects it to be used as the default directory for this operation.
        /// </summary>
        /// <param name="directoryName">The name of the directory that will be created or used.</param>
        /// <returns>The full name of the path to the directory chosen.</returns>
        private static string DefineFileDirectory(string directoryName)
        {
            string directory = "C:\\Users\\" + Environment.UserName + "\\Documents\\PixelEditor\\" + directoryName + "\\";
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            return directory;
        }

        /// <summary>
        /// A method to save an image file.
        /// It sets the default directory for images saved by the program and the default filename and extension for the image.
        /// </summary>
        /// <param name="originalImage">The image to be saved as a file.</param>
        public void SaveImage(Bitmap originalImage, Size originalDimensions)
        {
            string directory = DefineFileDirectory("SavedImages");

            DialogForSavingFiles.InitialDirectory = directory;
            DialogForSavingFiles.Filter = "PNG Image|*.png";
            DialogForSavingFiles.DefaultExt = "png";
            DialogForSavingFiles.Title = "Save the current image";

            SaveImageForm saveImageForm = new(originalImage, originalDimensions,DialogForSavingFiles);
            saveImageForm.ShowDialog();
        }

        /// <summary>
        /// A method to save a palette file, which is a file containing all the color values for a color palette on the application.
        /// It sets the default directory for palettes saved by the program and the default filename and extension for the file.
        /// </summary>
        /// <param name="paletteValues">The string containing all color values for the palette to be saved.</param>
        public void SavePalette(string paletteValues)
        {
            string directory = DefineFileDirectory("SavedPalettes");

            DialogForSavingFiles.InitialDirectory = directory;
            DialogForSavingFiles.FileName = "Palette_" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss-ff") + ".txt";
            DialogForSavingFiles.Filter = "Text file|*.txt";
            DialogForSavingFiles.DefaultExt = "txt";
            DialogForSavingFiles.Title = "Save the current color palette";
            DialogResult result = DialogForSavingFiles.ShowDialog();

            if (result == DialogResult.OK)
            {
                string nameOfFile = DialogForSavingFiles.FileName;
                File.WriteAllText(nameOfFile, paletteValues);
            }
        }

        /// <summary>
        /// A method to load an image file into the application.
        /// It sets the default directory for images saved by the program in the OpenFileDialog.
        /// </summary>
        /// <returns>The Bitmap loaded from the file, or null, in case no image was loaded.</returns>
        public Bitmap LoadImage()
        {
            string directory = DefineFileDirectory("SavedImages");

            DialogForOpeningFiles.InitialDirectory = directory;
            DialogForOpeningFiles.Title = "Load an image into the editor";

            LoadImageForm loadImageForm = new(DialogForOpeningFiles);
            DialogResult result = loadImageForm.ShowDialog();

            Bitmap? imageLoaded = null;
            if (result == DialogResult.OK)
            {
                imageLoaded = loadImageForm.ImageLoaded;
            }
            return imageLoaded!;
        }

        /// <summary>
        /// A method to load a palette file into the application.
        /// It sets the default directory for images saved by the program in the OpenFileDialog. 
        /// </summary>
        /// <returns>The string containing all colors values for the palette loaded, or an empty string, in case nothing was loaded.</returns>
        public string LoadPalette()
        {
            string directory = DefineFileDirectory("SavedPalettes");

            DialogForOpeningFiles.InitialDirectory = directory;
            DialogForOpeningFiles.Title = "Load a color palette";
            DialogResult result = DialogForOpeningFiles.ShowDialog();

            if (result == DialogResult.OK)
            {
                return File.ReadAllText(DialogForOpeningFiles.FileName);
            }
            return String.Empty;
        }
    }
}
