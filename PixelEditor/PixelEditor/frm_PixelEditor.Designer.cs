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
            ((System.ComponentModel.ISupportInitialize)(this.dbx_ViewingArea)).BeginInit();
            this.SuspendLayout();
            // 
            // dbx_ViewingArea
            // 
            this.dbx_ViewingArea.Location = new System.Drawing.Point(10, 10);
            this.dbx_ViewingArea.Name = "dbx_ViewingArea";
            this.dbx_ViewingArea.Size = new System.Drawing.Size(10, 10);
            this.dbx_ViewingArea.TabIndex = 0;
            this.dbx_ViewingArea.TabStop = false;
            // 
            // frm_PixelEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 661);
            this.Controls.Add(this.dbx_ViewingArea);
            this.Name = "frm_PixelEditor";
            this.Text = "Pixel Art Editor";
            this.Load += new System.EventHandler(this.frm_PixelEditor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dbx_ViewingArea)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.DrawingBox dbx_ViewingArea;
    }
}

