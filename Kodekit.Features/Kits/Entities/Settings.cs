namespace Kodekit.Features
{
    public class Settings
    {
        public Settings()
        {
            UseTypography = true;
            UseColors = true;
            UseIcons = true;
            UseButtons = true;
            UseInputs = true;
            UseCheckboxes = true;
            UseDropdowns = true;
            UseAnchors = true;
            UseLists = true;
            UseShadows = true;
        }
        
        public bool UseTypography { get; set; }
        public bool UseColors { get; set; }
        public bool UseIcons { get; set; }
        public bool UseButtons { get; set; }
        public bool UseInputs { get; set; }
        public bool UseCheckboxes { get; set; }
        public bool UseDropdowns { get; set; }
        public bool UseAnchors { get; set; }
        public bool UseLists { get; set; }
        public bool UseShadows { get; set; }
    }
}
