namespace PixelEditor
{
    partial class frm_PixelEditor
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.clr_ColorPicker = new System.Windows.Forms.ColorDialog();
            this.btn_PixelSize = new System.Windows.Forms.Button();
            this.lbl_PixelHeight = new System.Windows.Forms.Label();
            this.lbl_PixelWidth = new System.Windows.Forms.Label();
            this.lbl_ViewingZoom = new System.Windows.Forms.Label();
            this.cbb_Grid = new System.Windows.Forms.ComboBox();
            this.lbl_Grid = new System.Windows.Forms.Label();
            this.nmb_ViewingZoom = new PixelEditor.Controls.NumberBox();
            this.nmb_PixelWidth = new PixelEditor.Controls.NumberBox();
            this.tbl_Colors = new PixelEditor.Controls.ColorTable();
            this.dbx_ViewingArea = new PixelEditor.Controls.DrawingBox();
            this.nmb_PixelHeight = new PixelEditor.Controls.NumberBox();
            ((System.ComponentModel.ISupportInitialize)(this.nmb_ViewingZoom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmb_PixelWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbx_ViewingArea)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmb_PixelHeight)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_PixelSize
            // 
            this.btn_PixelSize.Location = new System.Drawing.Point(10, 70);
            this.btn_PixelSize.Name = "btn_PixelSize";
            this.btn_PixelSize.Size = new System.Drawing.Size(90, 30);
            this.btn_PixelSize.TabIndex = 5;
            this.btn_PixelSize.Text = "Set New Size";
            this.btn_PixelSize.UseVisualStyleBackColor = true;
            this.btn_PixelSize.Click += new System.EventHandler(this.btn_PixelSize_Click);
            // 
            // lbl_PixelHeight
            // 
            this.lbl_PixelHeight.Location = new System.Drawing.Point(10, 20);
            this.lbl_PixelHeight.Name = "lbl_PixelHeight";
            this.lbl_PixelHeight.Size = new System.Drawing.Size(40, 20);
            this.lbl_PixelHeight.TabIndex = 4;
            this.lbl_PixelHeight.Text = "Height";
            this.lbl_PixelHeight.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_PixelWidth
            // 
            this.lbl_PixelWidth.Location = new System.Drawing.Point(10, 45);
            this.lbl_PixelWidth.Name = "lbl_PixelWidth";
            this.lbl_PixelWidth.Size = new System.Drawing.Size(40, 20);
            this.lbl_PixelWidth.TabIndex = 6;
            this.lbl_PixelWidth.Text = "Width";
            this.lbl_PixelWidth.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_ViewingZoom
            // 
            this.lbl_ViewingZoom.Location = new System.Drawing.Point(105, 20);
            this.lbl_ViewingZoom.Name = "lbl_ViewingZoom";
            this.lbl_ViewingZoom.Size = new System.Drawing.Size(40, 20);
            this.lbl_ViewingZoom.TabIndex = 10;
            this.lbl_ViewingZoom.Text = "Zoom";
            this.lbl_ViewingZoom.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbb_Grid
            // 
            this.cbb_Grid.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbb_Grid.FormattingEnabled = true;
            this.cbb_Grid.Items.AddRange(new object[] {
            "None",
            "Line",
            "Checkered"});
            this.cbb_Grid.Location = new System.Drawing.Point(145, 45);
            this.cbb_Grid.Name = "cbb_Grid";
            this.cbb_Grid.Size = new System.Drawing.Size(80, 21);
            this.cbb_Grid.TabIndex = 4;
            // 
            // lbl_Grid
            // 
            this.lbl_Grid.Location = new System.Drawing.Point(105, 45);
            this.lbl_Grid.Name = "lbl_Grid";
            this.lbl_Grid.Size = new System.Drawing.Size(40, 20);
            this.lbl_Grid.TabIndex = 15;
            this.lbl_Grid.Text = "Grid";
            this.lbl_Grid.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nmb_ViewingZoom
            // 
            this.nmb_ViewingZoom.Location = new System.Drawing.Point(145, 20);
            this.nmb_ViewingZoom.Maximum = new decimal(new int[] {
            256,
            0,
            0,
            0});
            this.nmb_ViewingZoom.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmb_ViewingZoom.Name = "nmb_ViewingZoom";
            this.nmb_ViewingZoom.Size = new System.Drawing.Size(50, 20);
            this.nmb_ViewingZoom.TabIndex = 3;
            this.nmb_ViewingZoom.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            // 
            // nmb_PixelWidth
            // 
            this.nmb_PixelWidth.Location = new System.Drawing.Point(50, 45);
            this.nmb_PixelWidth.Maximum = new decimal(new int[] {
            4096,
            0,
            0,
            0});
            this.nmb_PixelWidth.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nmb_PixelWidth.Name = "nmb_PixelWidth";
            this.nmb_PixelWidth.Size = new System.Drawing.Size(50, 20);
            this.nmb_PixelWidth.TabIndex = 2;
            this.nmb_PixelWidth.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.nmb_PixelWidth.ValueChanged += new System.EventHandler(this.SizeValuesChanged);
            // 
            // tbl_Colors
            // 
            this.tbl_Colors.ColumnCount = 1;
            this.tbl_Colors.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tbl_Colors.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tbl_Colors.Location = new System.Drawing.Point(10, 620);
            this.tbl_Colors.Name = "tbl_Colors";
            this.tbl_Colors.RowCount = 1;
            this.tbl_Colors.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tbl_Colors.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tbl_Colors.Size = new System.Drawing.Size(20, 20);
            this.tbl_Colors.TabIndex = 1;
            // 
            // dbx_ViewingArea
            // 
            this.dbx_ViewingArea.BackColor = System.Drawing.Color.White;
            this.dbx_ViewingArea.Location = new System.Drawing.Point(10, 110);
            this.dbx_ViewingArea.Name = "dbx_ViewingArea";
            this.dbx_ViewingArea.Size = new System.Drawing.Size(20, 20);
            this.dbx_ViewingArea.TabIndex = 0;
            this.dbx_ViewingArea.TabStop = false;
            this.dbx_ViewingArea.Click += new System.EventHandler(this.dbx_ViewingArea_Click);
            // 
            // nmb_PixelHeight
            // 
            this.nmb_PixelHeight.Location = new System.Drawing.Point(50, 20);
            this.nmb_PixelHeight.Maximum = new decimal(new int[] {
            4096,
            0,
            0,
            0});
            this.nmb_PixelHeight.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nmb_PixelHeight.Name = "nmb_PixelHeight";
            this.nmb_PixelHeight.Size = new System.Drawing.Size(50, 20);
            this.nmb_PixelHeight.TabIndex = 1;
            this.nmb_PixelHeight.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.nmb_PixelHeight.ValueChanged += new System.EventHandler(this.SizeValuesChanged);
            // 
            // frm_PixelEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1184, 761);
            this.Controls.Add(this.nmb_PixelHeight);
            this.Controls.Add(this.lbl_Grid);
            this.Controls.Add(this.cbb_Grid);
            this.Controls.Add(this.nmb_ViewingZoom);
            this.Controls.Add(this.lbl_ViewingZoom);
            this.Controls.Add(this.nmb_PixelWidth);
            this.Controls.Add(this.lbl_PixelWidth);
            this.Controls.Add(this.lbl_PixelHeight);
            this.Controls.Add(this.btn_PixelSize);
            this.Controls.Add(this.tbl_Colors);
            this.Controls.Add(this.dbx_ViewingArea);
            this.Name = "frm_PixelEditor";
            this.Text = "Pixel Art Editor";
            this.Load += new System.EventHandler(this.frm_PixelEditor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nmb_ViewingZoom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmb_PixelWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dbx_ViewingArea)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmb_PixelHeight)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.DrawingBox dbx_ViewingArea;
        private Controls.ColorTable tbl_Colors;
        private System.Windows.Forms.ColorDialog clr_ColorPicker;
        private System.Windows.Forms.Button btn_PixelSize;
        private System.Windows.Forms.Label lbl_PixelHeight;
        private System.Windows.Forms.Label lbl_PixelWidth;
        private Controls.NumberBox nmb_PixelWidth;
        private Controls.NumberBox nmb_ViewingZoom;
        private System.Windows.Forms.Label lbl_ViewingZoom;
        private System.Windows.Forms.ComboBox cbb_Grid;
        private System.Windows.Forms.Label lbl_Grid;
        private Controls.NumberBox nmb_PixelHeight;
    }
}

