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
            LoadImageBackgroundPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)LoadImagePictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)LoadImageZoomNumberBox).BeginInit();
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
            LoadImageBackgroundPanel.AutoSize = true;
            LoadImageBackgroundPanel.BackColor = Color.Black;
            LoadImageBackgroundPanel.Controls.Add(LoadImagePictureBox);
            LoadImageBackgroundPanel.Location = new Point(4, 79);
            LoadImageBackgroundPanel.MaximumHeight = 514;
            LoadImageBackgroundPanel.MaximumWidth = 514;
            LoadImageBackgroundPanel.Name = "LoadImageBackgroundPanel";
            LoadImageBackgroundPanel.Size = new Size(40, 40);
            LoadImageBackgroundPanel.TabIndex = 1;
            // 
            // LoadImagePictureBox
            // 
            LoadImagePictureBox.Location = new Point(1, 1);
            LoadImagePictureBox.Name = "LoadImagePictureBox";
            LoadImagePictureBox.Size = new Size(20, 20);
            LoadImagePictureBox.TabIndex = 0;
            LoadImagePictureBox.TabStop = false;
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
            ConfirmLoadButton.Location = new Point(490, 5);
            ConfirmLoadButton.Name = "ConfirmLoadButton";
            ConfirmLoadButton.Size = new Size(80, 30);
            ConfirmLoadButton.TabIndex = 3;
            ConfirmLoadButton.Text = "Confirm";
            ConfirmLoadButton.UseVisualStyleBackColor = true;
            // 
            // CancelLoadButton
            // 
            CancelLoadButton.Location = new Point(490, 40);
            CancelLoadButton.Name = "CancelLoadButton";
            CancelLoadButton.Size = new Size(80, 30);
            CancelLoadButton.TabIndex = 4;
            CancelLoadButton.Text = "Cancel";
            CancelLoadButton.UseVisualStyleBackColor = true;
            // 
            // LoadImageZoomedLabel
            // 
            LoadImageZoomedLabel.AutoSize = true;
            LoadImageZoomedLabel.Location = new Point(180, 10);
            LoadImageZoomedLabel.Name = "LoadImageZoomedLabel";
            LoadImageZoomedLabel.Size = new Size(155, 15);
            LoadImageZoomedLabel.TabIndex = 5;
            LoadImageZoomedLabel.Text = "Does the image have zoom?";
            // 
            // LoadImageZoomNumberBar
            // 
            LoadImageZoomNumberBar.IncrementAmount = 1;
            LoadImageZoomNumberBar.Location = new Point(210, 35);
            LoadImageZoomNumberBar.MaximumValue = 64;
            LoadImageZoomNumberBar.MinimumValue = 1;
            LoadImageZoomNumberBar.Name = "LoadImageZoomNumberBar";
            LoadImageZoomNumberBar.Size = new Size(128, 30);
            LoadImageZoomNumberBar.TabIndex = 7;
            LoadImageZoomNumberBar.Value = 1;
            // 
            // LoadImageZoomNumberBox
            // 
            LoadImageZoomNumberBox.Location = new Point(175, 40);
            LoadImageZoomNumberBox.Maximum = new decimal(new int[] { 64, 0, 0, 0 });
            LoadImageZoomNumberBox.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            LoadImageZoomNumberBox.Name = "LoadImageZoomNumberBox";
            LoadImageZoomNumberBox.Size = new Size(30, 23);
            LoadImageZoomNumberBox.TabIndex = 6;
            LoadImageZoomNumberBox.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // LoadPixelHeightLabel
            // 
            LoadPixelHeightLabel.AutoSize = true;
            LoadPixelHeightLabel.Location = new Point(350, 40);
            LoadPixelHeightLabel.Name = "LoadPixelHeightLabel";
            LoadPixelHeightLabel.Size = new Size(77, 15);
            LoadPixelHeightLabel.TabIndex = 9;
            LoadPixelHeightLabel.Text = "Pixel Height: ";
            // 
            // LoadPixelWidthLabel
            // 
            LoadPixelWidthLabel.AutoSize = true;
            LoadPixelWidthLabel.Location = new Point(350, 20);
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
            // 
            // LoadImageForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(584, 461);
            Controls.Add(OpenImageButton);
            Controls.Add(LoadPixelHeightLabel);
            Controls.Add(LoadPixelWidthLabel);
            Controls.Add(LoadImageZoomNumberBar);
            Controls.Add(LoadImageZoomNumberBox);
            Controls.Add(LoadImageZoomedLabel);
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
    }
}