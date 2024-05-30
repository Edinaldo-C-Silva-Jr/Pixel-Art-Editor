namespace PixelArtEditor.Files.File_Forms
{
    partial class SaveFileForm
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
            FullImagePictureBox = new PictureBox();
            SaveFullImageButton = new Button();
            CancelSaveButton = new Button();
            FullImageZoomNumberBox = new Controls.NumberBox();
            FullImageZoomNumberBar = new Controls.NumberBar();
            FullImageZoomLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)FullImagePictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)FullImageZoomNumberBox).BeginInit();
            SuspendLayout();
            // 
            // FullImagePictureBox
            // 
            FullImagePictureBox.Location = new Point(5, 60);
            FullImagePictureBox.Name = "FullImagePictureBox";
            FullImagePictureBox.Size = new Size(256, 256);
            FullImagePictureBox.TabIndex = 0;
            FullImagePictureBox.TabStop = false;
            // 
            // SaveFullImageButton
            // 
            SaveFullImageButton.Location = new Point(10, 10);
            SaveFullImageButton.Name = "SaveFullImageButton";
            SaveFullImageButton.Size = new Size(80, 40);
            SaveFullImageButton.TabIndex = 1;
            SaveFullImageButton.Text = "Save Image";
            SaveFullImageButton.UseVisualStyleBackColor = true;
            // 
            // CancelSaveButton
            // 
            CancelSaveButton.Location = new Point(100, 10);
            CancelSaveButton.Name = "CancelSaveButton";
            CancelSaveButton.Size = new Size(80, 40);
            CancelSaveButton.TabIndex = 2;
            CancelSaveButton.Text = "Cancel";
            CancelSaveButton.UseVisualStyleBackColor = true;
            // 
            // FullImageZoomNumberBox
            // 
            FullImageZoomNumberBox.Location = new Point(240, 20);
            FullImageZoomNumberBox.Maximum = new decimal(new int[] { 64, 0, 0, 0 });
            FullImageZoomNumberBox.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            FullImageZoomNumberBox.Name = "FullImageZoomNumberBox";
            FullImageZoomNumberBox.Size = new Size(30, 23);
            FullImageZoomNumberBox.TabIndex = 3;
            FullImageZoomNumberBox.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // FullImageZoomNumberBar
            // 
            FullImageZoomNumberBar.IncrementAmount = 1;
            FullImageZoomNumberBar.Location = new Point(275, 15);
            FullImageZoomNumberBar.MaximumValue = 64;
            FullImageZoomNumberBar.MinimumValue = 1;
            FullImageZoomNumberBar.Name = "FullImageZoomNumberBar";
            FullImageZoomNumberBar.Size = new Size(128, 30);
            FullImageZoomNumberBar.TabIndex = 4;
            FullImageZoomNumberBar.Value = 1;
            // 
            // FullImageZoomLabel
            // 
            FullImageZoomLabel.AutoSize = true;
            FullImageZoomLabel.Location = new Point(200, 25);
            FullImageZoomLabel.Name = "FullImageZoomLabel";
            FullImageZoomLabel.Size = new Size(39, 15);
            FullImageZoomLabel.TabIndex = 5;
            FullImageZoomLabel.Text = "Zoom";
            // 
            // SaveFileForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(584, 461);
            Controls.Add(FullImageZoomLabel);
            Controls.Add(FullImageZoomNumberBar);
            Controls.Add(FullImageZoomNumberBox);
            Controls.Add(CancelSaveButton);
            Controls.Add(SaveFullImageButton);
            Controls.Add(FullImagePictureBox);
            Name = "SaveFileForm";
            Text = "SaveFileForm";
            ((System.ComponentModel.ISupportInitialize)FullImagePictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)FullImageZoomNumberBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox FullImagePictureBox;
        private Button SaveFullImageButton;
        private Button CancelSaveButton;
        private Controls.NumberBox FullImageZoomNumberBox;
        private Controls.NumberBar FullImageZoomNumberBar;
        private Label FullImageZoomLabel;
    }
}