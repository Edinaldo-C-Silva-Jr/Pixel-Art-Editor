﻿namespace PixelArtEditor.Files.File_Forms
{
    partial class LoadImageForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            LoadImageWidthLabel = new Label();
            LoadImageBackgroundPanel = new Controls.BackgroundPanel();
            LoadImagePictureBox = new PictureBox();
            LoadImageHeightLabel = new Label();
            ConfirmLoadButton = new Button();
            CancelLoadButton = new Button();
            LoadImageZoomedLabel = new Label();
            LoadImageZoomNumberBar = new Controls.NumberBar();
            LoadImageZoomNumberBox = new Controls.NumberBox();
            LoadPixelHeightLabel = new Label();
            LoadPixelWidthLabel = new Label();
            OpenImageButton = new Button();
            LoadImageZoomPanel = new Panel();
            LoadImageCancelZoomButton = new Button();
            LoadImageAcceptZoomButton = new Button();
            LoadImageShrinkImageButton = new Button();
            LoadImageEnlargeImageButton = new Button();
            LoadImageZoomedBackgroundPanel = new Controls.BackgroundPanel();
            LoadImageZoomedPictureBox = new PictureBox();
            ImageLoadedOriginalSizeLabel = new Label();
            LoadedImageZoomedSizeLabel = new Label();
            ResizeAfterLoadCheckBox = new CheckBox();
            LoadImageBackgroundPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)LoadImagePictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)LoadImageZoomNumberBox).BeginInit();
            LoadImageZoomPanel.SuspendLayout();
            LoadImageZoomedBackgroundPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)LoadImageZoomedPictureBox).BeginInit();
            SuspendLayout();
            // 
            // LoadImageWidthLabel
            // 
            LoadImageWidthLabel.AutoSize = true;
            LoadImageWidthLabel.Location = new Point(70, 20);
            LoadImageWidthLabel.Name = "LoadImageWidthLabel";
            LoadImageWidthLabel.Size = new Size(45, 15);
            LoadImageWidthLabel.TabIndex = 0;
            LoadImageWidthLabel.Text = "Width: ";
            // 
            // LoadImageBackgroundPanel
            // 
            LoadImageBackgroundPanel.AutoScroll = true;
            LoadImageBackgroundPanel.AutoScrollMargin = new Size(1, 1);
            LoadImageBackgroundPanel.BackColor = Color.Black;
            LoadImageBackgroundPanel.Controls.Add(LoadImagePictureBox);
            LoadImageBackgroundPanel.Location = new Point(4, 109);
            LoadImageBackgroundPanel.MaximumHeight = 514;
            LoadImageBackgroundPanel.MaximumWidth = 514;
            LoadImageBackgroundPanel.Name = "LoadImageBackgroundPanel";
            LoadImageBackgroundPanel.Size = new Size(40, 40);
            LoadImageBackgroundPanel.TabIndex = 1;
            // 
            // LoadImagePictureBox
            // 
            LoadImagePictureBox.BackColor = SystemColors.Control;
            LoadImagePictureBox.Location = new Point(1, 1);
            LoadImagePictureBox.Name = "LoadImagePictureBox";
            LoadImagePictureBox.Size = new Size(20, 20);
            LoadImagePictureBox.TabIndex = 0;
            LoadImagePictureBox.TabStop = false;
            LoadImagePictureBox.Paint += LoadImagePictureBox_Paint;
            // 
            // LoadImageHeightLabel
            // 
            LoadImageHeightLabel.AutoSize = true;
            LoadImageHeightLabel.Location = new Point(70, 40);
            LoadImageHeightLabel.Name = "LoadImageHeightLabel";
            LoadImageHeightLabel.Size = new Size(49, 15);
            LoadImageHeightLabel.TabIndex = 2;
            LoadImageHeightLabel.Text = "Height: ";
            // 
            // ConfirmLoadButton
            // 
            ConfirmLoadButton.Location = new Point(540, 5);
            ConfirmLoadButton.Name = "ConfirmLoadButton";
            ConfirmLoadButton.Size = new Size(80, 30);
            ConfirmLoadButton.TabIndex = 3;
            ConfirmLoadButton.Text = "Confirm";
            ConfirmLoadButton.UseVisualStyleBackColor = true;
            ConfirmLoadButton.Click += ConfirmLoadButton_Click;
            // 
            // CancelLoadButton
            // 
            CancelLoadButton.Location = new Point(540, 40);
            CancelLoadButton.Name = "CancelLoadButton";
            CancelLoadButton.Size = new Size(80, 30);
            CancelLoadButton.TabIndex = 4;
            CancelLoadButton.Text = "Cancel";
            CancelLoadButton.UseVisualStyleBackColor = true;
            CancelLoadButton.Click += CancelLoadButton_Click;
            // 
            // LoadImageZoomedLabel
            // 
            LoadImageZoomedLabel.AutoSize = true;
            LoadImageZoomedLabel.Location = new Point(5, 5);
            LoadImageZoomedLabel.Name = "LoadImageZoomedLabel";
            LoadImageZoomedLabel.Size = new Size(71, 15);
            LoadImageZoomedLabel.TabIndex = 5;
            LoadImageZoomedLabel.Text = "Zoom Panel";
            // 
            // LoadImageZoomNumberBar
            // 
            LoadImageZoomNumberBar.DefaultWidth = 128;
            LoadImageZoomNumberBar.IncrementAmount = 1;
            LoadImageZoomNumberBar.Location = new Point(40, 30);
            LoadImageZoomNumberBar.MaximumValue = 64;
            LoadImageZoomNumberBar.MinimumValue = 1;
            LoadImageZoomNumberBar.Name = "LoadImageZoomNumberBar";
            LoadImageZoomNumberBar.Size = new Size(128, 30);
            LoadImageZoomNumberBar.TabIndex = 7;
            LoadImageZoomNumberBar.Value = 1;
            LoadImageZoomNumberBar.ValueChanged += LoadImageZoomNumberBar_ValueChanged;
            // 
            // LoadImageZoomNumberBox
            // 
            LoadImageZoomNumberBox.Location = new Point(5, 35);
            LoadImageZoomNumberBox.Maximum = new decimal(new int[] { 64, 0, 0, 0 });
            LoadImageZoomNumberBox.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            LoadImageZoomNumberBox.Name = "LoadImageZoomNumberBox";
            LoadImageZoomNumberBox.Size = new Size(30, 23);
            LoadImageZoomNumberBox.TabIndex = 6;
            LoadImageZoomNumberBox.Value = new decimal(new int[] { 1, 0, 0, 0 });
            LoadImageZoomNumberBox.ValueChanged += LoadImageZoomNumberBox_ValueChanged;
            // 
            // LoadPixelHeightLabel
            // 
            LoadPixelHeightLabel.AutoSize = true;
            LoadPixelHeightLabel.Location = new Point(430, 40);
            LoadPixelHeightLabel.Name = "LoadPixelHeightLabel";
            LoadPixelHeightLabel.Size = new Size(77, 15);
            LoadPixelHeightLabel.TabIndex = 9;
            LoadPixelHeightLabel.Text = "Pixel Height: ";
            // 
            // LoadPixelWidthLabel
            // 
            LoadPixelWidthLabel.AutoSize = true;
            LoadPixelWidthLabel.Location = new Point(430, 20);
            LoadPixelWidthLabel.Name = "LoadPixelWidthLabel";
            LoadPixelWidthLabel.Size = new Size(73, 15);
            LoadPixelWidthLabel.TabIndex = 8;
            LoadPixelWidthLabel.Text = "Pixel Width: ";
            // 
            // OpenImageButton
            // 
            OpenImageButton.Location = new Point(5, 10);
            OpenImageButton.Name = "OpenImageButton";
            OpenImageButton.Size = new Size(60, 60);
            OpenImageButton.TabIndex = 10;
            OpenImageButton.Text = "Open Image";
            OpenImageButton.UseVisualStyleBackColor = true;
            OpenImageButton.Click += OpenImageButton_Click;
            // 
            // LoadImageZoomPanel
            // 
            LoadImageZoomPanel.Controls.Add(LoadImageCancelZoomButton);
            LoadImageZoomPanel.Controls.Add(LoadImageAcceptZoomButton);
            LoadImageZoomPanel.Controls.Add(LoadImageZoomNumberBar);
            LoadImageZoomPanel.Controls.Add(LoadImageZoomedLabel);
            LoadImageZoomPanel.Controls.Add(LoadImageZoomNumberBox);
            LoadImageZoomPanel.Location = new Point(170, 100);
            LoadImageZoomPanel.Name = "LoadImageZoomPanel";
            LoadImageZoomPanel.Size = new Size(250, 70);
            LoadImageZoomPanel.TabIndex = 11;
            // 
            // LoadImageCancelZoomButton
            // 
            LoadImageCancelZoomButton.Location = new Point(180, 40);
            LoadImageCancelZoomButton.Name = "LoadImageCancelZoomButton";
            LoadImageCancelZoomButton.Size = new Size(60, 25);
            LoadImageCancelZoomButton.TabIndex = 9;
            LoadImageCancelZoomButton.Text = "Cancel";
            LoadImageCancelZoomButton.UseVisualStyleBackColor = true;
            LoadImageCancelZoomButton.Click += LoadImageCancelZoomButton_Click;
            // 
            // LoadImageAcceptZoomButton
            // 
            LoadImageAcceptZoomButton.Location = new Point(180, 5);
            LoadImageAcceptZoomButton.Name = "LoadImageAcceptZoomButton";
            LoadImageAcceptZoomButton.Size = new Size(60, 25);
            LoadImageAcceptZoomButton.TabIndex = 8;
            LoadImageAcceptZoomButton.Text = "Accept";
            LoadImageAcceptZoomButton.UseVisualStyleBackColor = true;
            LoadImageAcceptZoomButton.Click += LoadImageAcceptZoomButton_Click;
            // 
            // LoadImageShrinkImageButton
            // 
            LoadImageShrinkImageButton.Location = new Point(180, 20);
            LoadImageShrinkImageButton.Name = "LoadImageShrinkImageButton";
            LoadImageShrinkImageButton.Size = new Size(60, 40);
            LoadImageShrinkImageButton.TabIndex = 12;
            LoadImageShrinkImageButton.Text = "Shrink Image";
            LoadImageShrinkImageButton.UseVisualStyleBackColor = true;
            LoadImageShrinkImageButton.Click += LoadImageShrinkImageButton_Click;
            // 
            // LoadImageEnlargeImageButton
            // 
            LoadImageEnlargeImageButton.Location = new Point(270, 20);
            LoadImageEnlargeImageButton.Name = "LoadImageEnlargeImageButton";
            LoadImageEnlargeImageButton.Size = new Size(60, 40);
            LoadImageEnlargeImageButton.TabIndex = 13;
            LoadImageEnlargeImageButton.Text = "Enlarge Image";
            LoadImageEnlargeImageButton.UseVisualStyleBackColor = true;
            LoadImageEnlargeImageButton.Click += LoadImageEnlargeImageButton_Click;
            // 
            // LoadImageZoomedBackgroundPanel
            // 
            LoadImageZoomedBackgroundPanel.AutoScroll = true;
            LoadImageZoomedBackgroundPanel.AutoScrollMargin = new Size(1, 1);
            LoadImageZoomedBackgroundPanel.BackColor = Color.Black;
            LoadImageZoomedBackgroundPanel.Controls.Add(LoadImageZoomedPictureBox);
            LoadImageZoomedBackgroundPanel.Location = new Point(524, 109);
            LoadImageZoomedBackgroundPanel.MaximumHeight = 514;
            LoadImageZoomedBackgroundPanel.MaximumWidth = 514;
            LoadImageZoomedBackgroundPanel.Name = "LoadImageZoomedBackgroundPanel";
            LoadImageZoomedBackgroundPanel.Size = new Size(40, 40);
            LoadImageZoomedBackgroundPanel.TabIndex = 2;
            // 
            // LoadImageZoomedPictureBox
            // 
            LoadImageZoomedPictureBox.BackColor = SystemColors.Control;
            LoadImageZoomedPictureBox.Location = new Point(1, 1);
            LoadImageZoomedPictureBox.Name = "LoadImageZoomedPictureBox";
            LoadImageZoomedPictureBox.Size = new Size(20, 20);
            LoadImageZoomedPictureBox.TabIndex = 0;
            LoadImageZoomedPictureBox.TabStop = false;
            // 
            // ImageLoadedOriginalSizeLabel
            // 
            ImageLoadedOriginalSizeLabel.AutoSize = true;
            ImageLoadedOriginalSizeLabel.Location = new Point(4, 80);
            ImageLoadedOriginalSizeLabel.Name = "ImageLoadedOriginalSizeLabel";
            ImageLoadedOriginalSizeLabel.Size = new Size(85, 15);
            ImageLoadedOriginalSizeLabel.TabIndex = 14;
            ImageLoadedOriginalSizeLabel.Text = "Original Image";
            // 
            // LoadedImageZoomedSizeLabel
            // 
            LoadedImageZoomedSizeLabel.AutoSize = true;
            LoadedImageZoomedSizeLabel.Location = new Point(524, 80);
            LoadedImageZoomedSizeLabel.Name = "LoadedImageZoomedSizeLabel";
            LoadedImageZoomedSizeLabel.Size = new Size(112, 15);
            LoadedImageZoomedSizeLabel.TabIndex = 15;
            LoadedImageZoomedSizeLabel.Text = "Image to be Loaded";
            // 
            // ResizeAfterLoadCheckBox
            // 
            ResizeAfterLoadCheckBox.Location = new Point(410, 60);
            ResizeAfterLoadCheckBox.Name = "ResizeAfterLoadCheckBox";
            ResizeAfterLoadCheckBox.Size = new Size(110, 40);
            ResizeAfterLoadCheckBox.TabIndex = 16;
            ResizeAfterLoadCheckBox.Text = "Resize ViewBox to fit image";
            ResizeAfterLoadCheckBox.UseVisualStyleBackColor = true;
            ResizeAfterLoadCheckBox.CheckedChanged += ResizeAfterLoadCheckBox_CheckedChanged;
            // 
            // LoadImageForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(634, 461);
            Controls.Add(ResizeAfterLoadCheckBox);
            Controls.Add(LoadedImageZoomedSizeLabel);
            Controls.Add(ImageLoadedOriginalSizeLabel);
            Controls.Add(LoadImageZoomedBackgroundPanel);
            Controls.Add(LoadImageEnlargeImageButton);
            Controls.Add(LoadImageShrinkImageButton);
            Controls.Add(LoadImageZoomPanel);
            Controls.Add(OpenImageButton);
            Controls.Add(LoadPixelHeightLabel);
            Controls.Add(LoadPixelWidthLabel);
            Controls.Add(CancelLoadButton);
            Controls.Add(ConfirmLoadButton);
            Controls.Add(LoadImageHeightLabel);
            Controls.Add(LoadImageBackgroundPanel);
            Controls.Add(LoadImageWidthLabel);
            Name = "LoadImageForm";
            Text = "Load Image";
            LoadImageBackgroundPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)LoadImagePictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)LoadImageZoomNumberBox).EndInit();
            LoadImageZoomPanel.ResumeLayout(false);
            LoadImageZoomPanel.PerformLayout();
            LoadImageZoomedBackgroundPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)LoadImageZoomedPictureBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label LoadImageWidthLabel;
        private Controls.BackgroundPanel LoadImageBackgroundPanel;
        private PictureBox LoadImagePictureBox;
        private Label LoadImageHeightLabel;
        private Button ConfirmLoadButton;
        private Button CancelLoadButton;
        private Label LoadImageZoomedLabel;
        private Controls.NumberBar LoadImageZoomNumberBar;
        private Controls.NumberBox LoadImageZoomNumberBox;
        private Label LoadPixelHeightLabel;
        private Label LoadPixelWidthLabel;
        private Button OpenImageButton;
        private Panel LoadImageZoomPanel;
        private Button LoadImageCancelZoomButton;
        private Button LoadImageAcceptZoomButton;
        private Button LoadImageShrinkImageButton;
        private Button LoadImageEnlargeImageButton;
        private Controls.BackgroundPanel LoadImageZoomedBackgroundPanel;
        private PictureBox LoadImageZoomedPictureBox;
        private Label ImageLoadedOriginalSizeLabel;
        private Label LoadedImageZoomedSizeLabel;
        private CheckBox ResizeAfterLoadCheckBox;
    }
}