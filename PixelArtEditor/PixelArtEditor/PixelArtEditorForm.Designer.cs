namespace PixelArtEditor
{
    partial class PixelArtEditorForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            PixelWidthLabel = new Label();
            PixelWidthNumberBox = new Controls.NumberBox();
            PixelHeightNumberBox = new Controls.NumberBox();
            PixelHeightLabel = new Label();
            ViewingZoomNumberBox = new Controls.NumberBox();
            ViewingZoomLabel = new Label();
            GridTypeLabel = new Label();
            GridTypeComboBox = new ComboBox();
            SetNewImageButton = new Button();
            SaveImageButton = new Button();
            ColorPickerDialog = new ColorDialog();
            ViewingAreaBackgroundPanel = new Controls.BackgroundPanel();
            ViewingAreaDrawingBox = new Controls.DrawingBox();
            ColorAreaBackgroundPanel = new Controls.BackgroundPanel();
            ColorAmountComboBox = new ComboBox();
            PaletteColorTable = new Controls.ColorTable();
            BackgroundColorTable = new Controls.ColorTable();
            BackgroundColorLabel = new Label();
            GridColorTable = new Controls.ColorTable();
            GridColorLabel = new Label();
            ColorAmountLabel = new Label();
            TransparencyCheckBox = new CheckBox();
            ColorChangeCheckBox = new CheckBox();
            LoadImageButton = new Button();
            DialogForLoadingFiles = new OpenFileDialog();
            ResizeOnLoadCheckBox = new CheckBox();
            CopyButton = new Button();
            PasteButton = new Button();
            LoadPaletteButton = new Button();
            SavePaletteButton = new Button();
            DialogForSavingFiles = new SaveFileDialog();
            FullMirrorPenButton = new Controls.ToolButton();
            DrawingToolButtonPanel = new Controls.ToolButtonPanel();
            EraserButton = new Controls.ToolButton();
            FourMirrorPenButton = new Controls.ToolButton();
            VerticalMirrorPenButton = new Controls.ToolButton();
            HorizontalMirrorPenButton = new Controls.ToolButton();
            PixelPenButton = new Controls.ToolButton();
            ((System.ComponentModel.ISupportInitialize)PixelWidthNumberBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PixelHeightNumberBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ViewingZoomNumberBox).BeginInit();
            ViewingAreaBackgroundPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ViewingAreaDrawingBox).BeginInit();
            ColorAreaBackgroundPanel.SuspendLayout();
            DrawingToolButtonPanel.SuspendLayout();
            SuspendLayout();
            // 
            // PixelWidthLabel
            // 
            PixelWidthLabel.Location = new Point(10, 10);
            PixelWidthLabel.Name = "PixelWidthLabel";
            PixelWidthLabel.Size = new Size(50, 20);
            PixelWidthLabel.TabIndex = 0;
            PixelWidthLabel.Text = "Width";
            PixelWidthLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // PixelWidthNumberBox
            // 
            PixelWidthNumberBox.Location = new Point(55, 9);
            PixelWidthNumberBox.Maximum = new decimal(new int[] { 4096, 0, 0, 0 });
            PixelWidthNumberBox.Minimum = new decimal(new int[] { 5, 0, 0, 0 });
            PixelWidthNumberBox.Name = "PixelWidthNumberBox";
            PixelWidthNumberBox.Size = new Size(50, 23);
            PixelWidthNumberBox.TabIndex = 1;
            PixelWidthNumberBox.Value = new decimal(new int[] { 16, 0, 0, 0 });
            PixelWidthNumberBox.ValueChanged += SizeValuesChanged;
            PixelWidthNumberBox.KeyDown += SizeNumberBoxes_KeyDown;
            // 
            // PixelHeightNumberBox
            // 
            PixelHeightNumberBox.Location = new Point(55, 39);
            PixelHeightNumberBox.Maximum = new decimal(new int[] { 4096, 0, 0, 0 });
            PixelHeightNumberBox.Minimum = new decimal(new int[] { 5, 0, 0, 0 });
            PixelHeightNumberBox.Name = "PixelHeightNumberBox";
            PixelHeightNumberBox.Size = new Size(50, 23);
            PixelHeightNumberBox.TabIndex = 3;
            PixelHeightNumberBox.Value = new decimal(new int[] { 16, 0, 0, 0 });
            PixelHeightNumberBox.ValueChanged += SizeValuesChanged;
            PixelHeightNumberBox.KeyDown += SizeNumberBoxes_KeyDown;
            // 
            // PixelHeightLabel
            // 
            PixelHeightLabel.Location = new Point(10, 40);
            PixelHeightLabel.Name = "PixelHeightLabel";
            PixelHeightLabel.Size = new Size(50, 20);
            PixelHeightLabel.TabIndex = 2;
            PixelHeightLabel.Text = "Height";
            PixelHeightLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ViewingZoomNumberBox
            // 
            ViewingZoomNumberBox.Location = new Point(165, 9);
            ViewingZoomNumberBox.Maximum = new decimal(new int[] { 64, 0, 0, 0 });
            ViewingZoomNumberBox.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            ViewingZoomNumberBox.Name = "ViewingZoomNumberBox";
            ViewingZoomNumberBox.Size = new Size(50, 23);
            ViewingZoomNumberBox.TabIndex = 5;
            ViewingZoomNumberBox.Value = new decimal(new int[] { 16, 0, 0, 0 });
            ViewingZoomNumberBox.ValueChanged += ViewingZoomNumberBox_ValueChanged;
            // 
            // ViewingZoomLabel
            // 
            ViewingZoomLabel.Location = new Point(120, 10);
            ViewingZoomLabel.Name = "ViewingZoomLabel";
            ViewingZoomLabel.Size = new Size(50, 20);
            ViewingZoomLabel.TabIndex = 4;
            ViewingZoomLabel.Text = "Zoom";
            ViewingZoomLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // GridTypeLabel
            // 
            GridTypeLabel.Location = new Point(105, 40);
            GridTypeLabel.Name = "GridTypeLabel";
            GridTypeLabel.Size = new Size(60, 20);
            GridTypeLabel.TabIndex = 6;
            GridTypeLabel.Text = "Grid Type";
            GridTypeLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // GridTypeComboBox
            // 
            GridTypeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            GridTypeComboBox.FormattingEnabled = true;
            GridTypeComboBox.Location = new Point(165, 39);
            GridTypeComboBox.Name = "GridTypeComboBox";
            GridTypeComboBox.Size = new Size(70, 23);
            GridTypeComboBox.TabIndex = 7;
            GridTypeComboBox.SelectedIndexChanged += GridTypeComboBox_SelectedIndexChanged_ApplyGridToImage;
            // 
            // SetNewImageButton
            // 
            SetNewImageButton.Location = new Point(15, 70);
            SetNewImageButton.Name = "SetNewImageButton";
            SetNewImageButton.Size = new Size(80, 30);
            SetNewImageButton.TabIndex = 8;
            SetNewImageButton.Text = "New Image";
            SetNewImageButton.UseVisualStyleBackColor = true;
            SetNewImageButton.Click += SetNewImageButton_Click;
            // 
            // SaveImageButton
            // 
            SaveImageButton.Location = new Point(130, 70);
            SaveImageButton.Name = "SaveImageButton";
            SaveImageButton.Size = new Size(80, 30);
            SaveImageButton.TabIndex = 9;
            SaveImageButton.Text = "Save Image";
            SaveImageButton.UseVisualStyleBackColor = true;
            SaveImageButton.Click += SaveImageButton_Click;
            // 
            // ViewingAreaBackgroundPanel
            // 
            ViewingAreaBackgroundPanel.AutoScroll = true;
            ViewingAreaBackgroundPanel.AutoScrollMargin = new Size(1, 1);
            ViewingAreaBackgroundPanel.BackColor = Color.Black;
            ViewingAreaBackgroundPanel.Controls.Add(ViewingAreaDrawingBox);
            ViewingAreaBackgroundPanel.Location = new Point(10, 120);
            ViewingAreaBackgroundPanel.MaximumHeight = 514;
            ViewingAreaBackgroundPanel.MaximumWidth = 514;
            ViewingAreaBackgroundPanel.Name = "ViewingAreaBackgroundPanel";
            ViewingAreaBackgroundPanel.Size = new Size(40, 40);
            ViewingAreaBackgroundPanel.TabIndex = 10;
            // 
            // ViewingAreaDrawingBox
            // 
            ViewingAreaDrawingBox.BackColor = SystemColors.Control;
            ViewingAreaDrawingBox.Location = new Point(1, 1);
            ViewingAreaDrawingBox.Name = "ViewingAreaDrawingBox";
            ViewingAreaDrawingBox.Size = new Size(20, 20);
            ViewingAreaDrawingBox.TabIndex = 0;
            ViewingAreaDrawingBox.TabStop = false;
            ViewingAreaDrawingBox.Paint += ViewingAreaDrawingBox_Paint;
            ViewingAreaDrawingBox.MouseDown += ViewingAreaDrawingBox_MouseDown;
            ViewingAreaDrawingBox.MouseMove += ViewingAreaDrawingBox_MouseMove;
            ViewingAreaDrawingBox.MouseUp += ViewingAreaDrawingBox_MouseUp;
            // 
            // ColorAreaBackgroundPanel
            // 
            ColorAreaBackgroundPanel.BackColor = Color.White;
            ColorAreaBackgroundPanel.Controls.Add(ColorAmountComboBox);
            ColorAreaBackgroundPanel.Controls.Add(PaletteColorTable);
            ColorAreaBackgroundPanel.Controls.Add(BackgroundColorTable);
            ColorAreaBackgroundPanel.Controls.Add(BackgroundColorLabel);
            ColorAreaBackgroundPanel.Controls.Add(GridColorTable);
            ColorAreaBackgroundPanel.Controls.Add(GridColorLabel);
            ColorAreaBackgroundPanel.Controls.Add(ColorAmountLabel);
            ColorAreaBackgroundPanel.Location = new Point(10, 170);
            ColorAreaBackgroundPanel.MaximumHeight = 200;
            ColorAreaBackgroundPanel.MaximumWidth = 300;
            ColorAreaBackgroundPanel.Name = "ColorAreaBackgroundPanel";
            ColorAreaBackgroundPanel.Size = new Size(120, 120);
            ColorAreaBackgroundPanel.TabIndex = 11;
            // 
            // ColorAmountComboBox
            // 
            ColorAmountComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            ColorAmountComboBox.FormattingEnabled = true;
            ColorAmountComboBox.Items.AddRange(new object[] { "2", "4", "8", "16", "32", "64" });
            ColorAmountComboBox.Location = new Point(25, 3);
            ColorAmountComboBox.Name = "ColorAmountComboBox";
            ColorAmountComboBox.Size = new Size(50, 23);
            ColorAmountComboBox.TabIndex = 17;
            ColorAmountComboBox.SelectedIndexChanged += ColorAmountComboBox_SelectedIndexChanged;
            // 
            // PaletteColorTable
            // 
            PaletteColorTable.BackColor = SystemColors.Control;
            PaletteColorTable.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            PaletteColorTable.CellSize = 16;
            PaletteColorTable.CellVisibleSelection = true;
            PaletteColorTable.ColumnCount = 1;
            PaletteColorTable.ColumnStyles.Add(new ColumnStyle());
            PaletteColorTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            PaletteColorTable.Location = new Point(1, 80);
            PaletteColorTable.MaximumCellAmount = 64;
            PaletteColorTable.Name = "PaletteColorTable";
            PaletteColorTable.RowCount = 2;
            PaletteColorTable.RowStyles.Add(new RowStyle());
            PaletteColorTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            PaletteColorTable.Size = new Size(20, 20);
            PaletteColorTable.TabIndex = 16;
            // 
            // BackgroundColorTable
            // 
            BackgroundColorTable.BackColor = SystemColors.Control;
            BackgroundColorTable.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            BackgroundColorTable.CellSize = 30;
            BackgroundColorTable.CellVisibleSelection = false;
            BackgroundColorTable.ColumnCount = 1;
            BackgroundColorTable.ColumnStyles.Add(new ColumnStyle());
            BackgroundColorTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            BackgroundColorTable.Location = new Point(90, 40);
            BackgroundColorTable.MaximumCellAmount = 1;
            BackgroundColorTable.Name = "BackgroundColorTable";
            BackgroundColorTable.RowCount = 1;
            BackgroundColorTable.RowStyles.Add(new RowStyle());
            BackgroundColorTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            BackgroundColorTable.Size = new Size(20, 20);
            BackgroundColorTable.TabIndex = 14;
            // 
            // BackgroundColorLabel
            // 
            BackgroundColorLabel.Location = new Point(60, 45);
            BackgroundColorLabel.Name = "BackgroundColorLabel";
            BackgroundColorLabel.Size = new Size(30, 20);
            BackgroundColorLabel.TabIndex = 15;
            BackgroundColorLabel.Text = "BC";
            // 
            // GridColorTable
            // 
            GridColorTable.BackColor = SystemColors.Control;
            GridColorTable.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            GridColorTable.CellSize = 30;
            GridColorTable.CellVisibleSelection = false;
            GridColorTable.ColumnCount = 1;
            GridColorTable.ColumnStyles.Add(new ColumnStyle());
            GridColorTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            GridColorTable.Location = new Point(30, 40);
            GridColorTable.MaximumCellAmount = 1;
            GridColorTable.Name = "GridColorTable";
            GridColorTable.RowCount = 2;
            GridColorTable.RowStyles.Add(new RowStyle());
            GridColorTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            GridColorTable.Size = new Size(20, 20);
            GridColorTable.TabIndex = 12;
            // 
            // GridColorLabel
            // 
            GridColorLabel.Location = new Point(0, 45);
            GridColorLabel.Name = "GridColorLabel";
            GridColorLabel.Size = new Size(30, 20);
            GridColorLabel.TabIndex = 13;
            GridColorLabel.Text = "GC";
            // 
            // ColorAmountLabel
            // 
            ColorAmountLabel.Location = new Point(0, 5);
            ColorAmountLabel.Name = "ColorAmountLabel";
            ColorAmountLabel.Size = new Size(30, 20);
            ColorAmountLabel.TabIndex = 12;
            ColorAmountLabel.Text = "CA";
            // 
            // TransparencyCheckBox
            // 
            TransparencyCheckBox.Location = new Point(240, 10);
            TransparencyCheckBox.Name = "TransparencyCheckBox";
            TransparencyCheckBox.Size = new Size(120, 20);
            TransparencyCheckBox.TabIndex = 12;
            TransparencyCheckBox.Text = "Use Transparency";
            TransparencyCheckBox.UseVisualStyleBackColor = true;
            TransparencyCheckBox.CheckedChanged += TransparencyCheckBox_CheckedChanged;
            // 
            // ColorChangeCheckBox
            // 
            ColorChangeCheckBox.Location = new Point(240, 35);
            ColorChangeCheckBox.Name = "ColorChangeCheckBox";
            ColorChangeCheckBox.Size = new Size(150, 20);
            ColorChangeCheckBox.TabIndex = 13;
            ColorChangeCheckBox.Text = "Change Color in Image";
            ColorChangeCheckBox.UseVisualStyleBackColor = true;
            // 
            // LoadImageButton
            // 
            LoadImageButton.Location = new Point(220, 70);
            LoadImageButton.Name = "LoadImageButton";
            LoadImageButton.Size = new Size(80, 30);
            LoadImageButton.TabIndex = 14;
            LoadImageButton.Text = "Load Image";
            LoadImageButton.UseVisualStyleBackColor = true;
            LoadImageButton.Click += LoadImageButton_Click;
            // 
            // ResizeOnLoadCheckBox
            // 
            ResizeOnLoadCheckBox.AutoSize = true;
            ResizeOnLoadCheckBox.Location = new Point(380, 10);
            ResizeOnLoadCheckBox.Name = "ResizeOnLoadCheckBox";
            ResizeOnLoadCheckBox.Size = new Size(104, 19);
            ResizeOnLoadCheckBox.TabIndex = 15;
            ResizeOnLoadCheckBox.Text = "Resize on Load";
            ResizeOnLoadCheckBox.UseVisualStyleBackColor = true;
            // 
            // CopyButton
            // 
            CopyButton.Location = new Point(310, 70);
            CopyButton.Name = "CopyButton";
            CopyButton.Size = new Size(50, 30);
            CopyButton.TabIndex = 16;
            CopyButton.Text = "Copy";
            CopyButton.UseVisualStyleBackColor = true;
            CopyButton.Click += CopyButton_Click;
            // 
            // PasteButton
            // 
            PasteButton.Location = new Point(360, 70);
            PasteButton.Name = "PasteButton";
            PasteButton.Size = new Size(50, 30);
            PasteButton.TabIndex = 17;
            PasteButton.Text = "Paste";
            PasteButton.UseVisualStyleBackColor = true;
            PasteButton.Click += PasteButton_Click;
            // 
            // LoadPaletteButton
            // 
            LoadPaletteButton.Location = new Point(506, 70);
            LoadPaletteButton.Name = "LoadPaletteButton";
            LoadPaletteButton.Size = new Size(80, 30);
            LoadPaletteButton.TabIndex = 19;
            LoadPaletteButton.Text = "Load Palette";
            LoadPaletteButton.UseVisualStyleBackColor = true;
            LoadPaletteButton.Click += LoadPaletteButton_Click;
            // 
            // SavePaletteButton
            // 
            SavePaletteButton.Location = new Point(416, 70);
            SavePaletteButton.Name = "SavePaletteButton";
            SavePaletteButton.Size = new Size(80, 30);
            SavePaletteButton.TabIndex = 18;
            SavePaletteButton.Text = "Save Palette";
            SavePaletteButton.UseVisualStyleBackColor = true;
            SavePaletteButton.Click += SavePaletteButton_Click;
            // 
            // FullMirrorPenButton
            // 
            FullMirrorPenButton.Location = new Point(150, 0);
            FullMirrorPenButton.Name = "FullMirrorPenButton";
            FullMirrorPenButton.PreviewOnHold = false;
            FullMirrorPenButton.PreviewOnMove = true;
            FullMirrorPenButton.Size = new Size(40, 40);
            FullMirrorPenButton.TabIndex = 25;
            FullMirrorPenButton.Text = "Mir. Pen";
            FullMirrorPenButton.ToolValue = 3;
            FullMirrorPenButton.UseBackgroundColor = false;
            FullMirrorPenButton.UseBeginPoint = true;
            FullMirrorPenButton.UseEndPoint = false;
            FullMirrorPenButton.UseImageSize = true;
            FullMirrorPenButton.UsePixelSize = true;
            FullMirrorPenButton.UseTransparency = false;
            FullMirrorPenButton.UseVisualStyleBackColor = true;
            FullMirrorPenButton.Click += ChangeTool_ToolButtonsClick;
            // 
            // DrawingToolButtonPanel
            // 
            DrawingToolButtonPanel.BackColor = SystemColors.Control;
            DrawingToolButtonPanel.Controls.Add(EraserButton);
            DrawingToolButtonPanel.Controls.Add(FourMirrorPenButton);
            DrawingToolButtonPanel.Controls.Add(VerticalMirrorPenButton);
            DrawingToolButtonPanel.Controls.Add(HorizontalMirrorPenButton);
            DrawingToolButtonPanel.Controls.Add(PixelPenButton);
            DrawingToolButtonPanel.Controls.Add(FullMirrorPenButton);
            DrawingToolButtonPanel.Location = new Point(592, 12);
            DrawingToolButtonPanel.Name = "DrawingToolButtonPanel";
            DrawingToolButtonPanel.Size = new Size(240, 190);
            DrawingToolButtonPanel.TabIndex = 22;
            // 
            // EraserButton
            // 
            EraserButton.Location = new Point(0, 50);
            EraserButton.Name = "EraserButton";
            EraserButton.PreviewOnHold = false;
            EraserButton.PreviewOnMove = true;
            EraserButton.Size = new Size(40, 40);
            EraserButton.TabIndex = 27;
            EraserButton.Text = "Eraser";
            EraserButton.ToolValue = 5;
            EraserButton.UseBackgroundColor = true;
            EraserButton.UseBeginPoint = true;
            EraserButton.UseEndPoint = false;
            EraserButton.UseImageSize = false;
            EraserButton.UsePixelSize = true;
            EraserButton.UseTransparency = true;
            EraserButton.UseVisualStyleBackColor = true;
            EraserButton.Click += ChangeTool_ToolButtonsClick;
            // 
            // FourMirrorPenButton
            // 
            FourMirrorPenButton.Location = new Point(200, 0);
            FourMirrorPenButton.Name = "FourMirrorPenButton";
            FourMirrorPenButton.PreviewOnHold = false;
            FourMirrorPenButton.PreviewOnMove = true;
            FourMirrorPenButton.Size = new Size(40, 40);
            FourMirrorPenButton.TabIndex = 26;
            FourMirrorPenButton.Text = "4x Pen";
            FourMirrorPenButton.ToolValue = 4;
            FourMirrorPenButton.UseBackgroundColor = false;
            FourMirrorPenButton.UseBeginPoint = true;
            FourMirrorPenButton.UseEndPoint = false;
            FourMirrorPenButton.UseImageSize = true;
            FourMirrorPenButton.UsePixelSize = true;
            FourMirrorPenButton.UseTransparency = false;
            FourMirrorPenButton.UseVisualStyleBackColor = true;
            FourMirrorPenButton.Click += ChangeTool_ToolButtonsClick;
            // 
            // VerticalMirrorPenButton
            // 
            VerticalMirrorPenButton.Location = new Point(100, 0);
            VerticalMirrorPenButton.Name = "VerticalMirrorPenButton";
            VerticalMirrorPenButton.PreviewOnHold = false;
            VerticalMirrorPenButton.PreviewOnMove = true;
            VerticalMirrorPenButton.Size = new Size(40, 40);
            VerticalMirrorPenButton.TabIndex = 24;
            VerticalMirrorPenButton.Text = "U D Pen";
            VerticalMirrorPenButton.ToolValue = 2;
            VerticalMirrorPenButton.UseBackgroundColor = false;
            VerticalMirrorPenButton.UseBeginPoint = true;
            VerticalMirrorPenButton.UseEndPoint = false;
            VerticalMirrorPenButton.UseImageSize = true;
            VerticalMirrorPenButton.UsePixelSize = true;
            VerticalMirrorPenButton.UseTransparency = false;
            VerticalMirrorPenButton.UseVisualStyleBackColor = true;
            VerticalMirrorPenButton.Click += ChangeTool_ToolButtonsClick;
            // 
            // HorizontalMirrorPenButton
            // 
            HorizontalMirrorPenButton.Location = new Point(50, 0);
            HorizontalMirrorPenButton.Name = "HorizontalMirrorPenButton";
            HorizontalMirrorPenButton.PreviewOnHold = false;
            HorizontalMirrorPenButton.PreviewOnMove = true;
            HorizontalMirrorPenButton.Size = new Size(40, 40);
            HorizontalMirrorPenButton.TabIndex = 23;
            HorizontalMirrorPenButton.Text = "L R Pen";
            HorizontalMirrorPenButton.ToolValue = 1;
            HorizontalMirrorPenButton.UseBackgroundColor = false;
            HorizontalMirrorPenButton.UseBeginPoint = true;
            HorizontalMirrorPenButton.UseEndPoint = false;
            HorizontalMirrorPenButton.UseImageSize = true;
            HorizontalMirrorPenButton.UsePixelSize = true;
            HorizontalMirrorPenButton.UseTransparency = false;
            HorizontalMirrorPenButton.UseVisualStyleBackColor = true;
            HorizontalMirrorPenButton.Click += ChangeTool_ToolButtonsClick;
            // 
            // PixelPenButton
            // 
            PixelPenButton.Location = new Point(0, 0);
            PixelPenButton.Name = "PixelPenButton";
            PixelPenButton.PreviewOnHold = false;
            PixelPenButton.PreviewOnMove = true;
            PixelPenButton.Size = new Size(40, 40);
            PixelPenButton.TabIndex = 22;
            PixelPenButton.Text = "Pen";
            PixelPenButton.ToolValue = 0;
            PixelPenButton.UseBackgroundColor = false;
            PixelPenButton.UseBeginPoint = true;
            PixelPenButton.UseEndPoint = false;
            PixelPenButton.UseImageSize = false;
            PixelPenButton.UsePixelSize = true;
            PixelPenButton.UseTransparency = false;
            PixelPenButton.UseVisualStyleBackColor = true;
            PixelPenButton.Click += ChangeTool_ToolButtonsClick;
            // 
            // PixelArtEditorForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(884, 461);
            Controls.Add(DrawingToolButtonPanel);
            Controls.Add(LoadPaletteButton);
            Controls.Add(SavePaletteButton);
            Controls.Add(PasteButton);
            Controls.Add(CopyButton);
            Controls.Add(ResizeOnLoadCheckBox);
            Controls.Add(LoadImageButton);
            Controls.Add(ColorChangeCheckBox);
            Controls.Add(TransparencyCheckBox);
            Controls.Add(ColorAreaBackgroundPanel);
            Controls.Add(ViewingAreaBackgroundPanel);
            Controls.Add(SaveImageButton);
            Controls.Add(SetNewImageButton);
            Controls.Add(GridTypeComboBox);
            Controls.Add(GridTypeLabel);
            Controls.Add(ViewingZoomNumberBox);
            Controls.Add(ViewingZoomLabel);
            Controls.Add(PixelHeightNumberBox);
            Controls.Add(PixelHeightLabel);
            Controls.Add(PixelWidthNumberBox);
            Controls.Add(PixelWidthLabel);
            Name = "PixelArtEditorForm";
            Text = "Pixel Art Editor";
            Load += PixelArtEditorForm_Load;
            ((System.ComponentModel.ISupportInitialize)PixelWidthNumberBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)PixelHeightNumberBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)ViewingZoomNumberBox).EndInit();
            ViewingAreaBackgroundPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)ViewingAreaDrawingBox).EndInit();
            ColorAreaBackgroundPanel.ResumeLayout(false);
            DrawingToolButtonPanel.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label PixelWidthLabel;
        private Controls.NumberBox PixelWidthNumberBox;
        private Controls.NumberBox PixelHeightNumberBox;
        private Label PixelHeightLabel;
        private Controls.NumberBox ViewingZoomNumberBox;
        private Label ViewingZoomLabel;
        private Label GridTypeLabel;
        private ComboBox GridTypeComboBox;
        private Button SetNewImageButton;
        private Button SaveImageButton;
        private ColorDialog ColorPickerDialog;
        private Controls.BackgroundPanel ViewingAreaBackgroundPanel;
        private Controls.DrawingBox ViewingAreaDrawingBox;
        private Controls.BackgroundPanel ColorAreaBackgroundPanel;
        private Label GridColorLabel;
        private Label ColorAmountLabel;
        private Controls.ColorTable GridColorTable;
        private Controls.ColorTable BackgroundColorTable;
        private Label BackgroundColorLabel;
        private ComboBox ColorAmountComboBox;
        private Controls.ColorTable PaletteColorTable;
        private CheckBox TransparencyCheckBox;
        private CheckBox ColorChangeCheckBox;
        private Button LoadImageButton;
        private OpenFileDialog DialogForLoadingFiles;
        private CheckBox ResizeOnLoadCheckBox;
        private Button CopyButton;
        private Button PasteButton;
        private Button LoadPaletteButton;
        private Button SavePaletteButton;
        private SaveFileDialog DialogForSavingFiles;
        private Controls.ToolButton FullMirrorPenButton;
        private Controls.ToolButtonPanel DrawingToolButtonPanel;
        private Controls.ToolButton PixelPenButton;
        private Controls.ToolButton VerticalMirrorPenButton;
        private Controls.ToolButton HorizontalMirrorPenButton;
        private Controls.ToolButton FourMirrorPenButton;
        private Controls.ToolButton EraserButton;
    }
}