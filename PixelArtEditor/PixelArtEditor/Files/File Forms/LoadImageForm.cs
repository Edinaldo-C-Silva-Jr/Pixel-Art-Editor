namespace PixelArtEditor.Files.File_Forms
{
    public partial class LoadImageForm : Form
    {
        private OpenFileDialog DialogForOpeningImages { get; set; }

        public LoadImageForm(OpenFileDialog dialogForOpeningImages)
        {
            InitializeComponent();

            DialogForOpeningImages = dialogForOpeningImages;

            // Single time control setup for when the form opens.
            LoadImagePictureBox.Size = new(0, 0);
            LoadImageBackgroundPanel.Size = new(0, 0);
            LoadImageZoomPanel.Location = new(170, 0);
            LoadImageZoomPanel.Visible = false;
            LoadImageRemoveZoomButton.Visible = false;
            LoadImageAddZoomButton.Visible = false;
        }

        private void LoadImageRemoveZoomButton_Click(object sender, EventArgs e)
        {
            EnableZoomPanel("How much zoom to remove?");
        }

        private void LoadImageAddZoomButton_Click(object sender, EventArgs e)
        {
            EnableZoomPanel("How much zoom to add?");
        }

        private void EnableZoomPanel(string labelText)
        {
            LoadImageRemoveZoomButton.Visible = false;
            LoadImageAddZoomButton.Visible = false;
            LoadImageZoomPanel.Visible = true;
            LoadImageZoomedLabel.Text = labelText;
        }

        private void DisableZoomPanel()
        {
            LoadImageRemoveZoomButton.Visible = true;
            LoadImageAddZoomButton.Visible = true;
            LoadImageZoomPanel.Visible = false;
            LoadImageZoomNumberBox.Value = 1;
        }

        private void LoadImageAcceptZoomButton_Click(object sender, EventArgs e)
        {
            DisableZoomPanel();
        }

        private void LoadImageCancelZoomButton_Click(object sender, EventArgs e)
        {
            DisableZoomPanel();
        }

        private void OpenImageButton_Click(object sender, EventArgs e)
        {
            DialogResult result = DialogForOpeningImages.ShowDialog();
            if (result == DialogResult.OK)
            {
                DisableZoomPanel();

                LoadImagePictureBox.Image = new Bitmap(DialogForOpeningImages.FileName);
                LoadImagePictureBox.Size = LoadImagePictureBox.Image.Size;
                LoadImageBackgroundPanel.ResizePanelToFitControls();

                LoadImageWidthLabel.Text = $"Width: {LoadImagePictureBox.Image.Width}";
                LoadImageHeightLabel.Text = $"Height: {LoadImagePictureBox.Image.Height}";

                LoadImageZoomNumberBox.Maximum = LoadImageZoomNumberBar.MaximumValue = CalculateMaximumZoom();

                SetPixelDimensions();
            }
        }

        private int CalculateMaximumZoom()
        {
            int imageWidth = LoadImagePictureBox.Image.Width;
            int imageHeight = LoadImagePictureBox.Image.Height;

            int smallerDimension = imageWidth < imageHeight ? imageWidth : imageHeight;

            if (smallerDimension >= 320)
            {
                return 64;
            }
            else
            {
                return smallerDimension / 5;
            }
        }

        private void LoadImageZoomNumberBox_ValueChanged(object sender, EventArgs e)
        {
            LoadImageZoomNumberBar.Value = (int)LoadImageZoomNumberBox.Value;
            SetPixelDimensions();
        }

        private void LoadImageZoomNumberBar_ValueChanged(object sender, EventArgs e)
        {
            LoadImageZoomNumberBox.Value = LoadImageZoomNumberBar.Value;
        }

        private void SetPixelDimensions()
        {
            if (LoadImagePictureBox.Image != null)
            {
                int pixelWidth = LoadImagePictureBox.Image.Width / (int)LoadImageZoomNumberBox.Value;
                int pixelHeight = LoadImagePictureBox.Image.Height / (int)LoadImageZoomNumberBox.Value;

                LoadPixelWidthLabel.Text = $"Pixel Width: {pixelWidth}";
                LoadPixelHeightLabel.Text = $"Pixel Height: {pixelHeight}";

                if (pixelWidth > 1024)
                {
                    LoadPixelWidthLabel.ForeColor = Color.Red;
                }
                else
                {
                    LoadPixelWidthLabel.ForeColor = Color.Black;
                }

                if (pixelHeight > 1024)
                {
                    LoadPixelHeightLabel.ForeColor = Color.Red;
                }
                else
                {
                    LoadPixelHeightLabel.ForeColor = Color.Black;
                }
            }
        }
    }
}
