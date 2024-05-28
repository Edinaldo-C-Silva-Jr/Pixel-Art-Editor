namespace PixelArtEditor.Controls
{
    /// <summary>
    /// A NumericUpDown control used to enter numbers. It has no arrows to increase or decrease the number inside it (but still works with the arrow keys).
    /// </summary>
    public partial class NumberBox : NumericUpDown
    {
        /// <summary>
        /// Default constructor. Removes the arrow control from the NumericUpDown.
        /// </summary>
        public NumberBox()
        {
            InitializeComponent();
            Controls[0].Enabled = false;
            Controls[0].Visible = false;
        }

        /// <summary>
        /// Resizes the Textbox  to fit the entire NumericUpDown control.
        /// </summary>
        protected override void OnTextBoxResize(object source, EventArgs e)
        {
            Controls[1].Width = Width - 4;
        }

        /// <summary>
        /// Guarantees the value will be within the available increments in the NumberBox.
        /// </summary>
        protected override void OnValueChanged(EventArgs e)
        {
            Value = Math.Truncate(Value);

            if (Value % Increment > Increment / 2)
            {
                Value += Increment - Value % Increment;
            }
            else
            {
                Value -= Value % Increment;
            }

            base.OnValueChanged(e);
        }

        /// <summary>
        /// Selects the contents of the Textbox when the control is entered.
        /// </summary>
        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);
            if (Controls[1] is TextBox box)
            {
                box.BeginInvoke(new Action(box.SelectAll));
            }
        }
    }
}
