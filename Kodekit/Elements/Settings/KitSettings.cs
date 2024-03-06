namespace Kodekit;

public class KitSettings
{
    public KitSettings()
    {
        UpdateAll(true);
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

    public void UpdateAll(bool value)
    {
        UseTypography = value;
        UseColors = value;
        UseIcons = value;
        UseButtons = value;
        UseInputs = value;
        UseCheckboxes = value;
        UseDropdowns = value;
        UseAnchors = value;
        UseLists = value;
        UseShadows = value;
    }
}
