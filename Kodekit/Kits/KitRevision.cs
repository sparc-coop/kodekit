using Kodekit.Models.Elements;

namespace Kodekit;

public class KitRevision : Entity<string>
{
    private KitRevision()
    {
        Id = string.Empty;
        KitId = string.Empty;
        DateCreated = DateTime.UtcNow;

        Colors = [];
        Shadows = [];

        Headings = new();
        Paragraphs = new();
        Buttons = new();
        Inputs = new();
        Selectors = new();
        Settings = new();
        Dropdowns = new();
        Anchors = new();
        Lists = new();
        Icons = new();
    }


    public KitRevision(Kit kit) : this()
    {
        KitId = kit.Id;
        Id = "1";

        if (kit.Current == null)
            return;

        Id = (int.Parse(kit.Current.Id) + 1).ToString();

        KitId = kit.Current.KitId;
        ParentRevisionId = kit.Current.Id;

        Colors = kit.Current.Colors;
        Shadows = kit.Current.Shadows;
        Headings = kit.Current.Headings;
        Paragraphs = kit.Current.Paragraphs;
        Buttons = kit.Current.Buttons;
        Inputs = kit.Current.Inputs;
        Selectors = kit.Current.Selectors;
        Settings = kit.Current.Settings;
        Dropdowns = kit.Current.Dropdowns;
        Anchors = kit.Current.Anchors;
        Lists = kit.Current.Lists;
        Icons = kit.Current.Icons;
    }

    public KitRevision(string kitId, string? revisionId)
    {
        Id = revisionId != null ? (int.Parse(revisionId) + 1).ToString() : "1";
        KitId = kitId;
        DateCreated = DateTime.UtcNow;

        Colors =
        [
            new() { Name = "Primary", Value = new Color{ HexValue = "#0B5FFF" } },
            new() { Name = "Secondary", Value = new Color{ HexValue = "#72E5DD" } },
            new() { Name = "Tertiary", Value = new Color{ HexValue = "#FFB46E" } },
            new() { Name = "Darkest", Value = new Color{ HexValue = "#000000" } },
            new() { Name = "Lightest", Value = new Color{ HexValue = "#FFFFFF" } },
            new() { Name = "Error", Value = new Color{ HexValue = "#F96464" } },
            new() { Name = "Warning", Value = new Color{ HexValue = "#FF8F39" } },
            new() { Name = "Success", Value = new Color{ HexValue = "#5ACA75" } }
        ];

        Shadows =
        [
            new() {Name = "", Value = new Shadow{ Blur = 5, HexColor = "", Spread = 5}}
        ];

        Headings = new Typography("Roboto", "400", 16, 1.250, 120, null);
        Paragraphs = new Typography("Roboto", "500", 17, 1.200, 160, null);

        Buttons = new Button(16, "500", 8, 8, 4, 0, 16, 16, false);
        Inputs = new Input(16, "400", 10, 16, 4, 1);
        Selectors = new Selector(16, "400", "#0B5FFF");
        Settings = new();
        Dropdowns = new Dropdown(16, "400", 10, 16, 4, 1, true);
        Anchors = new Anchor(16, "400", "#17171A", "#0B5FFF", "#7FABFF", "#00BFB2");
        Lists = new List (16, "400", "upper-roman", "None", 0, 0, 0, 8);
        Icons = new IconLibrary("Material", "https://fonts.googleapis.com/icon?family=Material+Icons", "<span class='material-icons'>%</span>");
    }

    public string KitId { get; set; }
    public DateTime DateCreated { get; set; }
    public string? ParentRevisionId { get; set; }//For child elements/later versions
    public string? Name { get; set; }

    // Kit settings
    public List<Variable<Color>> Colors { get; set; }
    public List<Variable<Shadow>> Shadows { get; set; }
    public Typography Headings { get; set; }
    public Typography Paragraphs { get; set; }
    public Button Buttons { get; set; }
    public Input Inputs { get; set; }
    public Selector Selectors { get; set; }
    public KitSettings Settings { get; set; }
    public Dropdown Dropdowns { get; set; }
    public Anchor Anchors { get; set; }
    public List Lists { get; set; }
    public IconLibrary Icons { get; set; }
    public bool IsDeleted { get; set; }

    public void UpdateSelectors(Selector selectors)
    {
        Selectors = selectors;
    }

    public void UpdateTypography(Typography headings, Typography paragraphs)
    {
        Headings = headings;
        Paragraphs = paragraphs;
    }

    public void UpdateSettings(KitSettings settings)
    {
        Settings = settings;
    }

    internal void UpdateInputs(Input input)
    {
        Inputs = input;
    }

    internal void UpdateDropdowns(Dropdown dropdowns)
    {
        Dropdowns = dropdowns;
    }

    internal void UpdateAnchors(Anchor anchor)
    {
        Anchors = anchor;
    }

    internal void UpdateLists(List list)
    {
        Lists = list;
    }

    internal void UpdateIcons(IconLibrary icon)
    {
        Icons = icon;
    }

    public Color? GetColor(ColorTypes colorType)
    {
        return Colors.FirstOrDefault(x => x.Name == $"{colorType.ToString().ToLower()}")?.Value;
    }

    public Dictionary<string, string>? GetGreyscaleColors()
    {
        var lightest = GetColor(ColorTypes.Lightest);
        var darkest = GetColor(ColorTypes.Darkest);
        if (lightest != null && darkest != null)
            return lightest.Expand(darkest);

        return null;
    }

    internal void UpdateButtons(Button buttons)
    {
        Buttons = buttons;
    }

    public Shadow? GetShadow(string shadowSize)
    {
        return Shadows.FirstOrDefault(x => x.Name == shadowSize.ToLower())?.Value;
    }

    public Dictionary<string, string>? GetShadows()
    {
        var lightest = GetShadow("small");
        var darkest = GetShadow("xlarge");
        if (lightest != null && darkest != null)
            return lightest.Expand(darkest);

        return null;
    }

    public void UpdateShadows(Shadow smallShadow, Shadow xLargeShadow)
    {
        Shadows.Clear();
        Shadows.Add(new Variable<Shadow>("small", smallShadow));
        Shadows.Add(new Variable<Shadow>("xlarge", xLargeShadow));
    }

    public void UpdateColor(ColorTypes colorType, string? hexValue)
    {
        Colors.RemoveAll(x => colorType.ToString().ToLower() == x.Name.ToLower());

        if (hexValue != null)
            Colors.Add(new Variable<Color>(colorType.ToString(), new Color(hexValue)));
    }

    public List<string?> Imports()
    {
        return new List<string?>
            {
                Headings.Font.FamilyUrl,
                Paragraphs.Font.FamilyUrl,
                Buttons.Font.FamilyUrl,
                Inputs.Font.FamilyUrl,
                Selectors.Font.FamilyUrl,
                Icons.Url
            }
        .Distinct()
        .Where(x => !string.IsNullOrWhiteSpace(x))
        .ToList();
    }
}
