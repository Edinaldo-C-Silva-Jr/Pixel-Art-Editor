namespace PixelArtEditor.Controls
{
    /// <summary>
    /// A Panel control used to hold the ToolButton controls, essentially becoming a ToolBar.
    /// It handles the properties of the ToolButtons and which tool is currently selected.
    /// </summary>
    public partial class ToolButtonPanel : Panel
    {
        /// <summary>
        /// Which ToolButton is currently selected to be used.
        /// </summary>
        public int CurrentButton { get; private set; }

        private List<ToolButton> ButtonList { get; set; }

        /// <summary>
        /// Default constructor, always starts with the ToolButton with value 0 selected.
        /// </summary>
        public ToolButtonPanel()
        {
            CurrentButton = 0;
            ButtonList = new();
            InitializeComponent();
        }

        /// <summary>
        /// Reorganizes the Controls collection of the panel so that the ToolButtons are in the same order as their ToolValue.
        /// </summary>
        public void ReorganizeButtons()
        {
            foreach (Control control in Controls)
            {
                if (control is ToolButton button)
                {
                    ButtonList.Add(button);
                }
            }

            ButtonList = ButtonList.OrderBy(button => button.ToolValue).ToList();

            ButtonList[0].Enabled = false; // Since the tool with value 0 is selected by default, it starts as disabled.
        }

        /// <summary>
        /// Changes the currently selected button to the new button received.
        /// </summary>
        /// <param name="button">The new button that will be selected.</param>
        public void ChangeCurrentButton(ToolButton button)
        {
            ButtonList[CurrentButton].Enabled = true; // Enables the previously selected button.
            CurrentButton = button.ToolValue;
            button.Enabled = false; // Disables the newly selected button.
        }

        /// <summary>
        /// Checks all the properties of the currently selected button to define when to show the tool's preview.
        /// </summary>
        /// <returns>A dictionary containing all current button preview properties, with a string key that identifies the property.</returns>
        public Dictionary<string, bool> CheckToolPreviewProperties()
        {
            Dictionary<string, bool> properties = new();

            if (ButtonList[CurrentButton] is ToolButton button)
            {
                properties.Add("PreviewMove", button.PreviewOnMove);
                properties.Add("PreviewHold", button.PreviewOnHold);
            }

            return properties;
        }

        /// <summary>
        /// Checks all the properties of the currently selected button to define which parameters the button's tool needs.
        /// </summary>
        /// <returns>A dictionary containing all current button properties, with a string key that identifies the property.</returns>
        public Dictionary<string, bool> CheckToolDrawProperties()
        {
            Dictionary<string, bool> properties = new();

            if (ButtonList[CurrentButton] is ToolButton button)
            {
                properties.Add("ClickLocation", button.UseClickLocation);
                properties.Add("ImageSize", button.UseImageSize);
                properties.Add("Transparency", button.UseTransparency);
                properties.Add("BackgroundColor", button.UseBackgroundColor);
                properties.Add("PixelSize", button.UsePixelSize);
            }

            return properties;
        }
    }
}
