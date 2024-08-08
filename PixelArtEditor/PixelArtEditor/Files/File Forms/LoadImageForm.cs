﻿using PixelArtEditor.Grids.Implementations;
using System.Drawing.Drawing2D;

namespace PixelArtEditor.Files.File_Forms
{
    public partial class LoadImageForm : Form
    {
        private OpenFileDialog DialogForOpeningImages { get; set; }

        private ZoomType TypeOfZoom { get; set; } = ZoomType.None;

        private bool ShowZoom { get; set; }

        private bool ImageIsValid { get; set; } = false;

        private Size InitialImageSize { get; set; }

        private Size ImageLoadedSize { get; set; }

        private int AppliedZoom { get; set; }

        private ZoomType AppliedZoomType { get; set; }

        private LineGrid ZoomedGrid { get; set; }

        public Bitmap? ImageLoaded { get; set; }

        public LoadImageForm(OpenFileDialog dialogForOpeningImages)
        {
            InitializeComponent();

            DialogForOpeningImages = dialogForOpeningImages;
            ZoomedGrid = new();

            // Single time control setup for when the form opens.
            LoadImagePictureBox.Size = new(0, 0);
            LoadImageBackgroundPanel.Size = new(0, 0);
            LoadImageZoomedPictureBox.Size = new(0, 0);
            LoadImageZoomedBackgroundPanel.Size = new(0, 0);
            LoadImageZoomPanel.Location = new(170, 0);

            ImageLoadedOriginalSizeLabel.Visible = false;
            LoadImageZoomPanel.Visible = false;
            LoadImageRemoveZoomButton.Visible = false;
            LoadImageAddZoomButton.Visible = false;
            LoadedImageZoomedSizeLabel.Visible = false;
            ConfirmLoadButton.Enabled = false;
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
                ImageLoadedOriginalSizeLabel.Visible = true;

                AppliedZoom = 1;
                ImageLoadedSize = InitialImageSize = LoadImagePictureBox.Size;
                LoadImageWidthLabel.Text = $"Width: {InitialImageSize.Width}";
                LoadImageHeightLabel.Text = $"Height: {InitialImageSize.Height}";

                AcceptPixelDimensions();
                ShowImageIfValid();
            }
        }

        private void LoadImageRemoveZoomButton_Click(object sender, EventArgs e)
        {
            EnableZoomPanel("How much zoom to remove?");
            LoadImageZoomNumberBox.Maximum = LoadImageZoomNumberBar.MaximumValue = CalculateMaximumShrink();
            TypeOfZoom = ZoomType.Shrink;
        }

        private int CalculateMaximumShrink()
        {
            int imageWidth = InitialImageSize.Width;
            int imageHeight = InitialImageSize.Height;

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

        private void LoadImageAddZoomButton_Click(object sender, EventArgs e)
        {
            EnableZoomPanel("How much zoom to add?");
            LoadImageZoomNumberBox.Maximum = LoadImageZoomNumberBar.MaximumValue = CalculateMaximumEnlarge();
            TypeOfZoom = ZoomType.Enlarge;
        }

        private int CalculateMaximumEnlarge()
        {
            int imageWidth = InitialImageSize.Width;
            int imageHeight = InitialImageSize.Height;

            int biggerDimension = imageWidth > imageHeight ? imageWidth : imageHeight;

            if (biggerDimension > 1024)
            {
                return 1;
            }
            else
            {
                return 1024 / biggerDimension;
            }
        }

        private void EnableZoomPanel(string labelText)
        {
            LoadImageRemoveZoomButton.Visible = false;
            LoadImageAddZoomButton.Visible = false;
            LoadImageZoomPanel.Visible = true;
            LoadImageZoomedLabel.Text = labelText;
            LoadImageZoomNumberBox.Value = 1;
            ShowZoom = true;
        }

        private void DisableZoomPanel()
        {
            LoadImageRemoveZoomButton.Visible = true;
            LoadImageAddZoomButton.Visible = true;
            LoadImageZoomPanel.Visible = false;
            ShowZoom = false;
            AcceptPixelDimensions();
        }

        private void LoadImageZoomNumberBox_ValueChanged(object sender, EventArgs e)
        {
            LoadImageZoomNumberBar.Value = (int)LoadImageZoomNumberBox.Value;
            if (ShowZoom)
            {
                ShowPixelDimensions();
            }
        }

        private void LoadImageZoomNumberBar_ValueChanged(object sender, EventArgs e)
        {
            LoadImageZoomNumberBox.Value = LoadImageZoomNumberBar.Value;
        }

        private void LoadImageAcceptZoomButton_Click(object sender, EventArgs e)
        {
            AppliedZoom = (int)LoadImageZoomNumberBox.Value;
            AppliedZoomType = TypeOfZoom;
            DisableZoomPanel();
            ShowImageIfValid();
            
            if (AppliedZoomType == ZoomType.Shrink)
            {
                ZoomedGrid.GenerateGrid(InitialImageSize.Width, InitialImageSize.Height, AppliedZoom, Color.Gray);
                LoadImagePictureBox.Invalidate();
            }
        }

        private void LoadImageCancelZoomButton_Click(object sender, EventArgs e)
        {
            DisableZoomPanel();
        }

        private Size CalculatePixelDimensions(int zoom, ZoomType type)
        {
            int pixelWidth = type switch
            {
                ZoomType.Shrink => InitialImageSize.Width / zoom,
                ZoomType.Enlarge => InitialImageSize.Width * zoom,
                _ => InitialImageSize.Width
            };

            int pixelHeight = type switch
            {
                ZoomType.Shrink => InitialImageSize.Height / zoom,
                ZoomType.Enlarge => InitialImageSize.Height * zoom,
                _ => InitialImageSize.Height
            };

            return new Size(pixelWidth, pixelHeight);
        }

        private void ShowPixelDimensions()
        {
            Size pixelDimensions = CalculatePixelDimensions((int)LoadImageZoomNumberBox.Value, TypeOfZoom);

            LoadPixelWidthLabel.ForeColor = Color.Black;
            LoadPixelHeightLabel.ForeColor = Color.Black;
            LoadPixelWidthLabel.Text = $"Pixel Width: {pixelDimensions.Width}";
            LoadPixelHeightLabel.Text = $"Pixel Height: {pixelDimensions.Height}";
        }

        private void AcceptPixelDimensions()
        {
            Size pixelDimensions = CalculatePixelDimensions(AppliedZoom, AppliedZoomType);
            ImageLoadedSize = pixelDimensions;

            ValidatePixelDimensions(pixelDimensions);
            LoadPixelWidthLabel.Text = $"Pixel Width: {pixelDimensions.Width}";
            LoadPixelHeightLabel.Text = $"Pixel Height: {pixelDimensions.Height}";
        }

        private void ValidatePixelDimensions(Size pixelDimensions)
        {
            bool valid = true;
            if (pixelDimensions.Width > 1024)
            {
                LoadPixelWidthLabel.ForeColor = Color.Red;
                valid = false;
            }
            else
            {
                LoadPixelWidthLabel.ForeColor = Color.Green;
            }

            if (pixelDimensions.Height > 1024)
            {
                LoadPixelHeightLabel.ForeColor = Color.Red;
                valid = false;
            }
            else
            {
                LoadPixelHeightLabel.ForeColor = Color.Green;
            }

            ImageIsValid = valid;
            ConfirmLoadButton.Enabled = valid;
        }

        private void ShowImageIfValid()
        {
            LoadImageZoomedBackgroundPanel.Visible = ImageIsValid;
            LoadedImageZoomedSizeLabel.Visible = ImageIsValid;

            if (ImageIsValid)
            {
                LoadImageZoomedPictureBox.Image = ApplyZoom((Bitmap)LoadImagePictureBox.Image, ImageLoadedSize.Width, ImageLoadedSize.Height);
            }
            else
            {
                LoadImageZoomedPictureBox.Image = new Bitmap(1, 1);
            }

            LoadImageZoomedPictureBox.Size = LoadImageZoomedPictureBox.Image.Size;
            LoadImageZoomedBackgroundPanel.ResizePanelToFitControls();
        }

        private void ConfirmLoadButton_Click(object sender, EventArgs e)
        {
            if (ImageIsValid)
            {
                ImageLoaded = ApplyZoom((Bitmap)LoadImagePictureBox.Image, ImageLoadedSize.Width, ImageLoadedSize.Height);
            }
            else
            {
                ImageLoaded = null;
            }

            DialogResult = DialogResult.OK;
        }

        private static Bitmap ApplyZoom(Bitmap originalImage, int zoomWidth, int zoomHeight)
        {
            // Creates an image with the newly desired size.
            Bitmap zoomedImage = new(zoomWidth, zoomHeight);
            using Graphics zoomGraphics = Graphics.FromImage(zoomedImage);

            // Sets the Interpolation mode and Pixel Offset to use for the editor. Uses Nearest Neighbor to preserve the pixels.
            zoomGraphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            zoomGraphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

            // Draws the original image onto the zoomed image, using the new size and interpolation mode defined.
            zoomGraphics.DrawImage(originalImage, 0, 0, zoomWidth, zoomHeight);
            return zoomedImage;
        }

        private void CancelLoadButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void LoadImagePictureBox_Paint(object sender, PaintEventArgs e)
        {
            ZoomedGrid.ApplyGrid(e.Graphics, InitialImageSize.Width, InitialImageSize.Height);
        }
    }
}