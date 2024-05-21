namespace PixelArtEditor.Controls
{
    public partial class NumberBar : TrackBar
    {
        public NumberBar()
        {
            InitializeComponent();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (e.X > 0 && e.X < Width)
                {
                    ChangeValueOnClick(e.X);
                }
            }
            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (e.X > 0 && e.X < Width)
                {
                    ChangeValueOnClick(e.X);
                }
            }
            base.OnMouseMove(e);
        }

        private void ChangeValueOnClick(int xLocation)
        {
            int amountOfTicks = 1 + ((Maximum - Minimum) / TickFrequency);
            int tickLocationInterval = Width / amountOfTicks;
            ValidateSize(amountOfTicks, tickLocationInterval);
            Value = Minimum + xLocation / tickLocationInterval * TickFrequency;
        }

        private void ValidateSize(int amountOfTicks, int tickLocationInterval)
        {
            if (Width % tickLocationInterval != 0)
            {
                Width = tickLocationInterval * amountOfTicks;
            }
        }

        protected override void OnValueChanged(EventArgs e)
        {
            Invalidate();
            base.OnValueChanged(e);
        }
    }
}
