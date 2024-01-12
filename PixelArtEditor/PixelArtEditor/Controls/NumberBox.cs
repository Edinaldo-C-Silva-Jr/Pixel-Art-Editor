namespace PixelArtEditor.Controls
{
    /// <summary>
    /// A NumericUpDown control used to enter numbers. It has no arrows to increase or decrease the number inside it (but still works with the arrow keys).
    /// </summary>
    public partial class NumberBox : NumericUpDown
    {
        public NumberBox()
        {
            InitializeComponent();
            // Removes the arrows from the control.
            Controls[0].Enabled = false;
            Controls[0].Visible = false;
        }

        protected override void OnTextBoxResize(object source, EventArgs e)
        {
            Controls[1].Width = Width - 4; // Resizes the Textbox to fit the whole control, including the space the arrows were originally.
        }
    }
}
