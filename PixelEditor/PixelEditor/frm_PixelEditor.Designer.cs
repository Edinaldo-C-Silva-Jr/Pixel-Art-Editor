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
            this.dbx_ViewingArea = new PixelEditor.Controls.DrawingBox();
            this.tbl_Colors = new PixelEditor.Controls.ColorTable();
            ((System.ComponentModel.ISupportInitialize)(this.dbx_ViewingArea)).BeginInit();
            this.SuspendLayout();
            // 
            // dbx_ViewingArea
            // 
            this.dbx_ViewingArea.Location = new System.Drawing.Point(10, 10);
            this.dbx_ViewingArea.Name = "dbx_ViewingArea";
            this.dbx_ViewingArea.Size = new System.Drawing.Size(20, 20);
            this.dbx_ViewingArea.TabIndex = 0;
            this.dbx_ViewingArea.TabStop = false;
            this.dbx_ViewingArea.Click += new System.EventHandler(this.dbx_ViewingArea_Click);
            // 
            // tbl_Colors
            // 
            this.tbl_Colors.ColumnCount = 1;
            this.tbl_Colors.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tbl_Colors.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tbl_Colors.Location = new System.Drawing.Point(10, 400);
            this.tbl_Colors.Name = "tbl_Colors";
            this.tbl_Colors.RowCount = 1;
            this.tbl_Colors.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tbl_Colors.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tbl_Colors.Size = new System.Drawing.Size(20, 20);
            this.tbl_Colors.TabIndex = 1;
            // 
            // frm_PixelEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 661);
            this.Controls.Add(this.tbl_Colors);
            this.Controls.Add(this.dbx_ViewingArea);
            this.Name = "frm_PixelEditor";
            this.Text = "Pixel Art Editor";
            this.Load += new System.EventHandler(this.frm_PixelEditor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dbx_ViewingArea)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.DrawingBox dbx_ViewingArea;
        private Controls.ColorTable tbl_Colors;
    }
}

