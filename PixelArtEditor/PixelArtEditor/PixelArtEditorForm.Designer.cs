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
            DrawingBackgroundPanel = new Controls.BackgroundPanel();
            DrawingBox = new Controls.DrawBox();
            ColorsBackgroundPanel = new Controls.BackgroundPanel();
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
            OutlineRectangleButton = new Controls.ToolButton();
            SolidRectangleButton = new Controls.ToolButton();
            FreeLineButton = new Controls.ToolButton();
            OrdinalLineButton = new Controls.ToolButton();
            CardinalLineButton = new Controls.ToolButton();
            EraserButton = new Controls.ToolButton();
            FourMirrorPenButton = new Controls.ToolButton();
            VerticalMirrorPenButton = new Controls.ToolButton();
            HorizontalMirrorPenButton = new Controls.ToolButton();
            PixelPenButton = new Controls.ToolButton();
            ViewingBackgroundPanel = new Controls.BackgroundPanel();
            ViewingBox = new Controls.ViewBox();
            DrawingZoomNumberBox = new Controls.NumberBox();
            label1 = new Label();
            DrawingHeightNumberBox = new Controls.NumberBox();
            label2 = new Label();
            DrawingWidthNumberBox = new Controls.NumberBox();
            label3 = new Label();
            DrawingBoxSizeButton = new Button();
            ViewingBoxSizeButton = new Button();
            ViewAreaGroupBox = new GroupBox();
            DrawAreaGroupBox = new GroupBox();
            ((System.ComponentModel.ISupportInitialize)PixelWidthNumberBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)PixelHeightNumberBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ViewingZoomNumberBox).BeginInit();
            DrawingBackgroundPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DrawingBox).BeginInit();
            ColorsBackgroundPanel.SuspendLayout();
            DrawingToolButtonPanel.SuspendLayout();
            ViewingBackgroundPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ViewingBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)DrawingZoomNumberBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)DrawingHeightNumberBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)DrawingWidthNumberBox).BeginInit();
            ViewAreaGroupBox.SuspendLayout();
            DrawAreaGroupBox.SuspendLayout();
            SuspendLayout();
            // 
            // PixelWidthLabel
            // 
            PixelWidthLabel.Location = new Point(5, 20);
            PixelWidthLabel.Name = "PixelWidthLabel";
            PixelWidthLabel.Size = new Size(50, 20);
            PixelWidthLabel.TabIndex = 0;
            PixelWidthLabel.Text = "Width";
            PixelWidthLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // PixelWidthNumberBox
            // 
            PixelWidthNumberBox.Location = new Point(50, 20);
            PixelWidthNumberBox.Maximum = new decimal(new int[] { 1024, 0, 0, 0 });
            PixelWidthNumberBox.Minimum = new decimal(new int[] { 5, 0, 0, 0 });
            PixelWidthNumberBox.Name = "PixelWidthNumberBox";
            PixelWidthNumberBox.Size = new Size(35, 23);
            PixelWidthNumberBox.TabIndex = 1;
            PixelWidthNumberBox.Value = new decimal(new int[] { 64, 0, 0, 0 });
            // 
            // PixelHeightNumberBox
            // 
            PixelHeightNumberBox.Location = new Point(50, 45);
            PixelHeightNumberBox.Maximum = new decimal(new int[] { 1024, 0, 0, 0 });
            PixelHeightNumberBox.Minimum = new decimal(new int[] { 5, 0, 0, 0 });
            PixelHeightNumberBox.Name = "PixelHeightNumberBox";
            PixelHeightNumberBox.Size = new Size(35, 23);
            PixelHeightNumberBox.TabIndex = 3;
            PixelHeightNumberBox.Value = new decimal(new int[] { 64, 0, 0, 0 });
            // 
            // PixelHeightLabel
            // 
            PixelHeightLabel.Location = new Point(5, 45);
            PixelHeightLabel.Name = "PixelHeightLabel";
            PixelHeightLabel.Size = new Size(50, 20);
            PixelHeightLabel.TabIndex = 2;
            PixelHeightLabel.Text = "Height";
            PixelHeightLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // ViewingZoomNumberBox
            // 
            ViewingZoomNumberBox.Location = new Point(100, 40);
            ViewingZoomNumberBox.Maximum = new decimal(new int[] { 4, 0, 0, 0 });
            ViewingZoomNumberBox.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            ViewingZoomNumberBox.Name = "ViewingZoomNumberBox";
            ViewingZoomNumberBox.Size = new Size(20, 23);
            ViewingZoomNumberBox.TabIndex = 5;
            ViewingZoomNumberBox.Value = new decimal(new int[] { 4, 0, 0, 0 });
            ViewingZoomNumberBox.ValueChanged += ViewingZoomNumberBox_ValueChanged;
            // 
            // ViewingZoomLabel
            // 
            ViewingZoomLabel.Location = new Point(90, 20);
            ViewingZoomLabel.Name = "ViewingZoomLabel";
            ViewingZoomLabel.Size = new Size(40, 20);
            ViewingZoomLabel.TabIndex = 4;
            ViewingZoomLabel.Text = "Zoom";
            ViewingZoomLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // GridTypeLabel
            // 
            GridTypeLabel.Location = new Point(100, 45);
            GridTypeLabel.Name = "GridTypeLabel";
            GridTypeLabel.Size = new Size(40, 20);
            GridTypeLabel.TabIndex = 6;
            GridTypeLabel.Text = "Grid";
            GridTypeLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // GridTypeComboBox
            // 
            GridTypeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            GridTypeComboBox.FormattingEnabled = true;
            GridTypeComboBox.Location = new Point(90, 65);
            GridTypeComboBox.Name = "GridTypeComboBox";
            GridTypeComboBox.Size = new Size(60, 23);
            GridTypeComboBox.TabIndex = 7;
            GridTypeComboBox.SelectedIndexChanged += ChangeGrid_GridTypeComboBoxIndexChanged;
            // 
            // SetNewImageButton
            // 
            SetNewImageButton.Location = new Point(311, 10);
            SetNewImageButton.Name = "SetNewImageButton";
            SetNewImageButton.Size = new Size(80, 25);
            SetNewImageButton.TabIndex = 8;
            SetNewImageButton.Text = "New Image";
            SetNewImageButton.UseVisualStyleBackColor = true;
            SetNewImageButton.Click += SetNewImageButton_Click;
            // 
            // SaveImageButton
            // 
            SaveImageButton.Location = new Point(311, 40);
            SaveImageButton.Name = "SaveImageButton";
            SaveImageButton.Size = new Size(80, 25);
            SaveImageButton.TabIndex = 9;
            SaveImageButton.Text = "Save Image";
            SaveImageButton.UseVisualStyleBackColor = true;
            SaveImageButton.Click += SaveImageButton_Click;
            // 
            // DrawingBackgroundPanel
            // 
            DrawingBackgroundPanel.AutoScroll = true;
            DrawingBackgroundPanel.AutoScrollMargin = new Size(1, 1);
            DrawingBackgroundPanel.BackColor = Color.Black;
            DrawingBackgroundPanel.Controls.Add(DrawingBox);
            DrawingBackgroundPanel.Location = new Point(10, 120);
            DrawingBackgroundPanel.MaximumHeight = 514;
            DrawingBackgroundPanel.MaximumWidth = 514;
            DrawingBackgroundPanel.Name = "DrawingBackgroundPanel";
            DrawingBackgroundPanel.Size = new Size(40, 40);
            DrawingBackgroundPanel.TabIndex = 10;
            // 
            // DrawingBox
            // 
            DrawingBox.BackColor = SystemColors.Control;
            DrawingBox.Location = new Point(1, 1);
            DrawingBox.Name = "DrawingBox";
            DrawingBox.Size = new Size(20, 20);
            DrawingBox.TabIndex = 0;
            DrawingBox.TabStop = false;
            DrawingBox.Paint += ViewingAreaDrawingBox_Paint;
            DrawingBox.MouseDown += ViewingAreaDrawingBox_MouseDown;
            DrawingBox.MouseLeave += ViewingAreaDrawingBox_MouseLeave;
            DrawingBox.MouseMove += ViewingAreaDrawingBox_MouseMove;
            DrawingBox.MouseUp += ViewingAreaDrawingBox_MouseUp;
            // 
            // ColorsBackgroundPanel
            // 
            ColorsBackgroundPanel.BackColor = Color.White;
            ColorsBackgroundPanel.Controls.Add(ColorAmountComboBox);
            ColorsBackgroundPanel.Controls.Add(PaletteColorTable);
            ColorsBackgroundPanel.Controls.Add(BackgroundColorTable);
            ColorsBackgroundPanel.Controls.Add(BackgroundColorLabel);
            ColorsBackgroundPanel.Controls.Add(GridColorTable);
            ColorsBackgroundPanel.Controls.Add(GridColorLabel);
            ColorsBackgroundPanel.Controls.Add(ColorAmountLabel);
            ColorsBackgroundPanel.Location = new Point(10, 170);
            ColorsBackgroundPanel.MaximumHeight = 200;
            ColorsBackgroundPanel.MaximumWidth = 300;
            ColorsBackgroundPanel.Name = "ColorsBackgroundPanel";
            ColorsBackgroundPanel.Size = new Size(120, 120);
            ColorsBackgroundPanel.TabIndex = 11;
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
            TransparencyCheckBox.Location = new Point(515, 60);
            TransparencyCheckBox.Name = "TransparencyCheckBox";
            TransparencyCheckBox.Size = new Size(90, 20);
            TransparencyCheckBox.TabIndex = 12;
            TransparencyCheckBox.Text = "Transparent";
            TransparencyCheckBox.UseVisualStyleBackColor = true;
            TransparencyCheckBox.CheckedChanged += TransparencyCheckBox_CheckedChanged;
            // 
            // ColorChangeCheckBox
            // 
            ColorChangeCheckBox.Location = new Point(490, 10);
            ColorChangeCheckBox.Name = "ColorChangeCheckBox";
            ColorChangeCheckBox.Size = new Size(150, 20);
            ColorChangeCheckBox.TabIndex = 13;
            ColorChangeCheckBox.Text = "Change Color in Image";
            ColorChangeCheckBox.UseVisualStyleBackColor = true;
            // 
            // LoadImageButton
            // 
            LoadImageButton.Location = new Point(311, 70);
            LoadImageButton.Name = "LoadImageButton";
            LoadImageButton.Size = new Size(80, 25);
            LoadImageButton.TabIndex = 14;
            LoadImageButton.Text = "Load Image";
            LoadImageButton.UseVisualStyleBackColor = true;
            LoadImageButton.Click += LoadImageButton_Click;
            // 
            // ResizeOnLoadCheckBox
            // 
            ResizeOnLoadCheckBox.AutoSize = true;
            ResizeOnLoadCheckBox.Location = new Point(510, 35);
            ResizeOnLoadCheckBox.Name = "ResizeOnLoadCheckBox";
            ResizeOnLoadCheckBox.Size = new Size(104, 19);
            ResizeOnLoadCheckBox.TabIndex = 15;
            ResizeOnLoadCheckBox.Text = "Resize on Load";
            ResizeOnLoadCheckBox.UseVisualStyleBackColor = true;
            // 
            // CopyButton
            // 
            CopyButton.Location = new Point(396, 70);
            CopyButton.Name = "CopyButton";
            CopyButton.Size = new Size(50, 25);
            CopyButton.TabIndex = 16;
            CopyButton.Text = "Copy";
            CopyButton.UseVisualStyleBackColor = true;
            CopyButton.Click += CopyButton_Click;
            // 
            // PasteButton
            // 
            PasteButton.Location = new Point(451, 70);
            PasteButton.Name = "PasteButton";
            PasteButton.Size = new Size(50, 25);
            PasteButton.TabIndex = 17;
            PasteButton.Text = "Paste";
            PasteButton.UseVisualStyleBackColor = true;
            PasteButton.Click += PasteButton_Click;
            // 
            // LoadPaletteButton
            // 
            LoadPaletteButton.Location = new Point(396, 40);
            LoadPaletteButton.Name = "LoadPaletteButton";
            LoadPaletteButton.Size = new Size(80, 25);
            LoadPaletteButton.TabIndex = 19;
            LoadPaletteButton.Text = "Load Palette";
            LoadPaletteButton.UseVisualStyleBackColor = true;
            LoadPaletteButton.Click += LoadPaletteButton_Click;
            // 
            // SavePaletteButton
            // 
            SavePaletteButton.Location = new Point(396, 10);
            SavePaletteButton.Name = "SavePaletteButton";
            SavePaletteButton.Size = new Size(80, 25);
            SavePaletteButton.TabIndex = 18;
            SavePaletteButton.Text = "Save Palette";
            SavePaletteButton.UseVisualStyleBackColor = true;
            SavePaletteButton.Click += SavePaletteButton_Click;
            // 
            // FullMirrorPenButton
            // 
            FullMirrorPenButton.Location = new Point(120, 0);
            FullMirrorPenButton.Name = "FullMirrorPenButton";
            FullMirrorPenButton.PreviewOnHold = false;
            FullMirrorPenButton.PreviewOnMove = true;
            FullMirrorPenButton.Size = new Size(40, 40);
            FullMirrorPenButton.TabIndex = 25;
            FullMirrorPenButton.Text = "Mir. Pen";
            FullMirrorPenButton.ToolValue = 3;
            FullMirrorPenButton.UseBackgroundColor = false;
            FullMirrorPenButton.UseClickLocation = true;
            FullMirrorPenButton.UseImageSize = true;
            FullMirrorPenButton.UsePixelSize = true;
            FullMirrorPenButton.UseTransparency = false;
            FullMirrorPenButton.UseVisualStyleBackColor = true;
            FullMirrorPenButton.Click += ChangeTool_ToolButtonsClick;
            // 
            // DrawingToolButtonPanel
            // 
            DrawingToolButtonPanel.BackColor = SystemColors.Control;
            DrawingToolButtonPanel.Controls.Add(OutlineRectangleButton);
            DrawingToolButtonPanel.Controls.Add(SolidRectangleButton);
            DrawingToolButtonPanel.Controls.Add(FreeLineButton);
            DrawingToolButtonPanel.Controls.Add(OrdinalLineButton);
            DrawingToolButtonPanel.Controls.Add(CardinalLineButton);
            DrawingToolButtonPanel.Controls.Add(EraserButton);
            DrawingToolButtonPanel.Controls.Add(FourMirrorPenButton);
            DrawingToolButtonPanel.Controls.Add(VerticalMirrorPenButton);
            DrawingToolButtonPanel.Controls.Add(HorizontalMirrorPenButton);
            DrawingToolButtonPanel.Controls.Add(PixelPenButton);
            DrawingToolButtonPanel.Controls.Add(FullMirrorPenButton);
            DrawingToolButtonPanel.Location = new Point(650, 20);
            DrawingToolButtonPanel.Name = "DrawingToolButtonPanel";
            DrawingToolButtonPanel.Size = new Size(240, 80);
            DrawingToolButtonPanel.TabIndex = 22;
            // 
            // OutlineRectangleButton
            // 
            OutlineRectangleButton.Location = new Point(200, 40);
            OutlineRectangleButton.Name = "OutlineRectangleButton";
            OutlineRectangleButton.PreviewOnHold = true;
            OutlineRectangleButton.PreviewOnMove = false;
            OutlineRectangleButton.Size = new Size(40, 40);
            OutlineRectangleButton.TabIndex = 32;
            OutlineRectangleButton.Text = "Out. Rect.";
            OutlineRectangleButton.ToolValue = 10;
            OutlineRectangleButton.UseBackgroundColor = false;
            OutlineRectangleButton.UseClickLocation = true;
            OutlineRectangleButton.UseImageSize = false;
            OutlineRectangleButton.UsePixelSize = true;
            OutlineRectangleButton.UseTransparency = false;
            OutlineRectangleButton.UseVisualStyleBackColor = true;
            OutlineRectangleButton.Click += ChangeTool_ToolButtonsClick;
            // 
            // SolidRectangleButton
            // 
            SolidRectangleButton.Location = new Point(160, 40);
            SolidRectangleButton.Name = "SolidRectangleButton";
            SolidRectangleButton.PreviewOnHold = true;
            SolidRectangleButton.PreviewOnMove = false;
            SolidRectangleButton.Size = new Size(40, 40);
            SolidRectangleButton.TabIndex = 31;
            SolidRectangleButton.Text = "Rect";
            SolidRectangleButton.ToolValue = 9;
            SolidRectangleButton.UseBackgroundColor = false;
            SolidRectangleButton.UseClickLocation = true;
            SolidRectangleButton.UseImageSize = false;
            SolidRectangleButton.UsePixelSize = true;
            SolidRectangleButton.UseTransparency = false;
            SolidRectangleButton.UseVisualStyleBackColor = true;
            SolidRectangleButton.Click += ChangeTool_ToolButtonsClick;
            // 
            // FreeLineButton
            // 
            FreeLineButton.Location = new Point(120, 40);
            FreeLineButton.Name = "FreeLineButton";
            FreeLineButton.PreviewOnHold = true;
            FreeLineButton.PreviewOnMove = false;
            FreeLineButton.Size = new Size(40, 40);
            FreeLineButton.TabIndex = 30;
            FreeLineButton.Text = "Free Line";
            FreeLineButton.ToolValue = 8;
            FreeLineButton.UseBackgroundColor = false;
            FreeLineButton.UseClickLocation = true;
            FreeLineButton.UseImageSize = false;
            FreeLineButton.UsePixelSize = true;
            FreeLineButton.UseTransparency = false;
            FreeLineButton.UseVisualStyleBackColor = true;
            FreeLineButton.Click += ChangeTool_ToolButtonsClick;
            // 
            // OrdinalLineButton
            // 
            OrdinalLineButton.Location = new Point(80, 40);
            OrdinalLineButton.Name = "OrdinalLineButton";
            OrdinalLineButton.PreviewOnHold = true;
            OrdinalLineButton.PreviewOnMove = false;
            OrdinalLineButton.Size = new Size(40, 40);
            OrdinalLineButton.TabIndex = 29;
            OrdinalLineButton.Text = "8 Dir Line";
            OrdinalLineButton.ToolValue = 7;
            OrdinalLineButton.UseBackgroundColor = false;
            OrdinalLineButton.UseClickLocation = true;
            OrdinalLineButton.UseImageSize = false;
            OrdinalLineButton.UsePixelSize = true;
            OrdinalLineButton.UseTransparency = false;
            OrdinalLineButton.UseVisualStyleBackColor = true;
            OrdinalLineButton.Click += ChangeTool_ToolButtonsClick;
            // 
            // CardinalLineButton
            // 
            CardinalLineButton.Location = new Point(40, 40);
            CardinalLineButton.Name = "CardinalLineButton";
            CardinalLineButton.PreviewOnHold = true;
            CardinalLineButton.PreviewOnMove = false;
            CardinalLineButton.Size = new Size(40, 40);
            CardinalLineButton.TabIndex = 28;
            CardinalLineButton.Text = "L R Line";
            CardinalLineButton.ToolValue = 6;
            CardinalLineButton.UseBackgroundColor = false;
            CardinalLineButton.UseClickLocation = true;
            CardinalLineButton.UseImageSize = false;
            CardinalLineButton.UsePixelSize = true;
            CardinalLineButton.UseTransparency = false;
            CardinalLineButton.UseVisualStyleBackColor = true;
            CardinalLineButton.Click += ChangeTool_ToolButtonsClick;
            // 
            // EraserButton
            // 
            EraserButton.Location = new Point(0, 40);
            EraserButton.Name = "EraserButton";
            EraserButton.PreviewOnHold = false;
            EraserButton.PreviewOnMove = true;
            EraserButton.Size = new Size(40, 40);
            EraserButton.TabIndex = 27;
            EraserButton.Text = "Eraser";
            EraserButton.ToolValue = 5;
            EraserButton.UseBackgroundColor = true;
            EraserButton.UseClickLocation = true;
            EraserButton.UseImageSize = false;
            EraserButton.UsePixelSize = true;
            EraserButton.UseTransparency = true;
            EraserButton.UseVisualStyleBackColor = true;
            EraserButton.Click += ChangeTool_ToolButtonsClick;
            // 
            // FourMirrorPenButton
            // 
            FourMirrorPenButton.Location = new Point(160, 0);
            FourMirrorPenButton.Name = "FourMirrorPenButton";
            FourMirrorPenButton.PreviewOnHold = false;
            FourMirrorPenButton.PreviewOnMove = true;
            FourMirrorPenButton.Size = new Size(40, 40);
            FourMirrorPenButton.TabIndex = 26;
            FourMirrorPenButton.Text = "4x Pen";
            FourMirrorPenButton.ToolValue = 4;
            FourMirrorPenButton.UseBackgroundColor = false;
            FourMirrorPenButton.UseClickLocation = true;
            FourMirrorPenButton.UseImageSize = true;
            FourMirrorPenButton.UsePixelSize = true;
            FourMirrorPenButton.UseTransparency = false;
            FourMirrorPenButton.UseVisualStyleBackColor = true;
            FourMirrorPenButton.Click += ChangeTool_ToolButtonsClick;
            // 
            // VerticalMirrorPenButton
            // 
            VerticalMirrorPenButton.Location = new Point(80, 0);
            VerticalMirrorPenButton.Name = "VerticalMirrorPenButton";
            VerticalMirrorPenButton.PreviewOnHold = false;
            VerticalMirrorPenButton.PreviewOnMove = true;
            VerticalMirrorPenButton.Size = new Size(40, 40);
            VerticalMirrorPenButton.TabIndex = 24;
            VerticalMirrorPenButton.Text = "U D Pen";
            VerticalMirrorPenButton.ToolValue = 2;
            VerticalMirrorPenButton.UseBackgroundColor = false;
            VerticalMirrorPenButton.UseClickLocation = true;
            VerticalMirrorPenButton.UseImageSize = true;
            VerticalMirrorPenButton.UsePixelSize = true;
            VerticalMirrorPenButton.UseTransparency = false;
            VerticalMirrorPenButton.UseVisualStyleBackColor = true;
            VerticalMirrorPenButton.Click += ChangeTool_ToolButtonsClick;
            // 
            // HorizontalMirrorPenButton
            // 
            HorizontalMirrorPenButton.Location = new Point(40, 0);
            HorizontalMirrorPenButton.Name = "HorizontalMirrorPenButton";
            HorizontalMirrorPenButton.PreviewOnHold = false;
            HorizontalMirrorPenButton.PreviewOnMove = true;
            HorizontalMirrorPenButton.Size = new Size(40, 40);
            HorizontalMirrorPenButton.TabIndex = 23;
            HorizontalMirrorPenButton.Text = "L R Pen";
            HorizontalMirrorPenButton.ToolValue = 1;
            HorizontalMirrorPenButton.UseBackgroundColor = false;
            HorizontalMirrorPenButton.UseClickLocation = true;
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
            PixelPenButton.UseClickLocation = true;
            PixelPenButton.UseImageSize = false;
            PixelPenButton.UsePixelSize = true;
            PixelPenButton.UseTransparency = false;
            PixelPenButton.UseVisualStyleBackColor = true;
            PixelPenButton.Click += ChangeTool_ToolButtonsClick;
            // 
            // ViewingBackgroundPanel
            // 
            ViewingBackgroundPanel.AutoScroll = true;
            ViewingBackgroundPanel.AutoScrollMargin = new Size(1, 1);
            ViewingBackgroundPanel.BackColor = Color.Black;
            ViewingBackgroundPanel.Controls.Add(ViewingBox);
            ViewingBackgroundPanel.Location = new Point(60, 120);
            ViewingBackgroundPanel.MaximumHeight = 514;
            ViewingBackgroundPanel.MaximumWidth = 514;
            ViewingBackgroundPanel.Name = "ViewingBackgroundPanel";
            ViewingBackgroundPanel.Size = new Size(40, 40);
            ViewingBackgroundPanel.TabIndex = 23;
            // 
            // ViewingBox
            // 
            ViewingBox.BackColor = SystemColors.Control;
            ViewingBox.Location = new Point(1, 1);
            ViewingBox.Name = "ViewingBox";
            ViewingBox.Size = new Size(20, 20);
            ViewingBox.TabIndex = 0;
            ViewingBox.TabStop = false;
            ViewingBox.Click += ViewingBox_Click;
            ViewingBox.Paint += ViewingBox_Paint;
            // 
            // DrawingZoomNumberBox
            // 
            DrawingZoomNumberBox.Location = new Point(120, 15);
            DrawingZoomNumberBox.Maximum = new decimal(new int[] { 32, 0, 0, 0 });
            DrawingZoomNumberBox.Minimum = new decimal(new int[] { 8, 0, 0, 0 });
            DrawingZoomNumberBox.Name = "DrawingZoomNumberBox";
            DrawingZoomNumberBox.Size = new Size(25, 23);
            DrawingZoomNumberBox.TabIndex = 29;
            DrawingZoomNumberBox.Value = new decimal(new int[] { 16, 0, 0, 0 });
            DrawingZoomNumberBox.ValueChanged += DrawingZoomNumberBox_ValueChanged;
            // 
            // label1
            // 
            label1.Location = new Point(80, 15);
            label1.Name = "label1";
            label1.Size = new Size(40, 20);
            label1.TabIndex = 28;
            label1.Text = "Zoom";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // DrawingHeightNumberBox
            // 
            DrawingHeightNumberBox.Location = new Point(50, 45);
            DrawingHeightNumberBox.Maximum = new decimal(new int[] { 64, 0, 0, 0 });
            DrawingHeightNumberBox.Minimum = new decimal(new int[] { 5, 0, 0, 0 });
            DrawingHeightNumberBox.Name = "DrawingHeightNumberBox";
            DrawingHeightNumberBox.Size = new Size(25, 23);
            DrawingHeightNumberBox.TabIndex = 27;
            DrawingHeightNumberBox.Value = new decimal(new int[] { 16, 0, 0, 0 });
            // 
            // label2
            // 
            label2.Location = new Point(5, 45);
            label2.Name = "label2";
            label2.Size = new Size(50, 20);
            label2.TabIndex = 26;
            label2.Text = "Height";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // DrawingWidthNumberBox
            // 
            DrawingWidthNumberBox.Location = new Point(50, 20);
            DrawingWidthNumberBox.Maximum = new decimal(new int[] { 64, 0, 0, 0 });
            DrawingWidthNumberBox.Minimum = new decimal(new int[] { 5, 0, 0, 0 });
            DrawingWidthNumberBox.Name = "DrawingWidthNumberBox";
            DrawingWidthNumberBox.Size = new Size(25, 23);
            DrawingWidthNumberBox.TabIndex = 25;
            DrawingWidthNumberBox.Value = new decimal(new int[] { 16, 0, 0, 0 });
            // 
            // label3
            // 
            label3.Location = new Point(5, 20);
            label3.Name = "label3";
            label3.Size = new Size(50, 20);
            label3.TabIndex = 24;
            label3.Text = "Width";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // DrawingBoxSizeButton
            // 
            DrawingBoxSizeButton.Location = new Point(5, 75);
            DrawingBoxSizeButton.Name = "DrawingBoxSizeButton";
            DrawingBoxSizeButton.Size = new Size(70, 25);
            DrawingBoxSizeButton.TabIndex = 30;
            DrawingBoxSizeButton.Text = "Set Size";
            DrawingBoxSizeButton.UseVisualStyleBackColor = true;
            DrawingBoxSizeButton.Click += DrawingBoxSizeButton_Click;
            // 
            // ViewingBoxSizeButton
            // 
            ViewingBoxSizeButton.Location = new Point(30, 75);
            ViewingBoxSizeButton.Name = "ViewingBoxSizeButton";
            ViewingBoxSizeButton.Size = new Size(80, 25);
            ViewingBoxSizeButton.TabIndex = 31;
            ViewingBoxSizeButton.Text = "Set Size";
            ViewingBoxSizeButton.UseVisualStyleBackColor = true;
            ViewingBoxSizeButton.Click += ViewingBoxSizeButton_Click;
            // 
            // ViewAreaGroupBox
            // 
            ViewAreaGroupBox.Controls.Add(PixelWidthNumberBox);
            ViewAreaGroupBox.Controls.Add(ViewingBoxSizeButton);
            ViewAreaGroupBox.Controls.Add(PixelWidthLabel);
            ViewAreaGroupBox.Controls.Add(PixelHeightNumberBox);
            ViewAreaGroupBox.Controls.Add(PixelHeightLabel);
            ViewAreaGroupBox.Controls.Add(ViewingZoomNumberBox);
            ViewAreaGroupBox.Controls.Add(ViewingZoomLabel);
            ViewAreaGroupBox.Location = new Point(170, 5);
            ViewAreaGroupBox.Name = "ViewAreaGroupBox";
            ViewAreaGroupBox.Size = new Size(135, 105);
            ViewAreaGroupBox.TabIndex = 32;
            ViewAreaGroupBox.TabStop = false;
            ViewAreaGroupBox.Text = "View Area";
            // 
            // DrawAreaGroupBox
            // 
            DrawAreaGroupBox.Controls.Add(DrawingWidthNumberBox);
            DrawAreaGroupBox.Controls.Add(label3);
            DrawAreaGroupBox.Controls.Add(DrawingZoomNumberBox);
            DrawAreaGroupBox.Controls.Add(DrawingBoxSizeButton);
            DrawAreaGroupBox.Controls.Add(label1);
            DrawAreaGroupBox.Controls.Add(DrawingHeightNumberBox);
            DrawAreaGroupBox.Controls.Add(label2);
            DrawAreaGroupBox.Controls.Add(GridTypeLabel);
            DrawAreaGroupBox.Controls.Add(GridTypeComboBox);
            DrawAreaGroupBox.Location = new Point(5, 5);
            DrawAreaGroupBox.Name = "DrawAreaGroupBox";
            DrawAreaGroupBox.Size = new Size(160, 105);
            DrawAreaGroupBox.TabIndex = 33;
            DrawAreaGroupBox.TabStop = false;
            DrawAreaGroupBox.Text = "Draw Area";
            // 
            // PixelArtEditorForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(984, 461);
            Controls.Add(DrawAreaGroupBox);
            Controls.Add(ViewAreaGroupBox);
            Controls.Add(ViewingBackgroundPanel);
            Controls.Add(DrawingToolButtonPanel);
            Controls.Add(LoadPaletteButton);
            Controls.Add(SavePaletteButton);
            Controls.Add(PasteButton);
            Controls.Add(CopyButton);
            Controls.Add(ResizeOnLoadCheckBox);
            Controls.Add(LoadImageButton);
            Controls.Add(ColorChangeCheckBox);
            Controls.Add(TransparencyCheckBox);
            Controls.Add(ColorsBackgroundPanel);
            Controls.Add(DrawingBackgroundPanel);
            Controls.Add(SaveImageButton);
            Controls.Add(SetNewImageButton);
            Name = "PixelArtEditorForm";
            Text = "Pixel Art Editor";
            Load += PixelArtEditorForm_Load;
            ((System.ComponentModel.ISupportInitialize)PixelWidthNumberBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)PixelHeightNumberBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)ViewingZoomNumberBox).EndInit();
            DrawingBackgroundPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)DrawingBox).EndInit();
            ColorsBackgroundPanel.ResumeLayout(false);
            DrawingToolButtonPanel.ResumeLayout(false);
            ViewingBackgroundPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)ViewingBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)DrawingZoomNumberBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)DrawingHeightNumberBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)DrawingWidthNumberBox).EndInit();
            ViewAreaGroupBox.ResumeLayout(false);
            DrawAreaGroupBox.ResumeLayout(false);
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
        private Controls.BackgroundPanel DrawingBackgroundPanel;
        private Controls.DrawBox DrawingBox;
        private Controls.BackgroundPanel ColorsBackgroundPanel;
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
        private Controls.ToolButton CardinalLineButton;
        private Controls.ToolButton OrdinalLineButton;
        private Controls.ToolButton FreeLineButton;
        private Controls.ToolButton SolidRectangleButton;
        private Controls.ToolButton OutlineRectangleButton;
        private Controls.BackgroundPanel ViewingBackgroundPanel;
        private Controls.ViewBox ViewingBox;
        private Controls.NumberBox DrawingZoomNumberBox;
        private Label label1;
        private Controls.NumberBox DrawingHeightNumberBox;
        private Label label2;
        private Controls.NumberBox DrawingWidthNumberBox;
        private Label label3;
        private Button DrawingBoxSizeButton;
        private Button ViewingBoxSizeButton;
        private GroupBox ViewAreaGroupBox;
        private GroupBox DrawAreaGroupBox;
    }
}