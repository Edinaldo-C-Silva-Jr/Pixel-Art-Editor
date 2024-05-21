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

        public void SetSizeDivisions()
        {
            int amountOfTicks = 1 + ((Maximum - Minimum) / TickFrequency);
            int tickLocationInterval = Width / amountOfTicks;
            Width = tickLocationInterval * amountOfTicks;
        }

        private void ChangeValueOnClick(int xLocation)
        {
            int amountOfTicks = 1 + ((Maximum - Minimum) / TickFrequency);
            int tickLocationInterval = Width / amountOfTicks;
            Value = Minimum + xLocation / tickLocationInterval * TickFrequency;
            Invalidate();
        }

    }
}
