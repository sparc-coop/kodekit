namespace Kodekit;

public class KitRevision : BlossomEntity<string>
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


    internal KitRevision(Kit kit) : this()
    {
        KitId = kit.Id;
        Id = "1";
    }

    internal KitRevision(KitRevision revision) : this()
    {
        // Auto-incrementing integer version IDs
        Id = (int.Parse(revision.Id) + 1).ToString();

        KitId = revision.KitId;
        ParentRevisionId = revision.Id;

        Colors = revision.Colors;
        Shadows = revision.Shadows;
        Headings = revision.Headings;
        Paragraphs = revision.Paragraphs;
        Buttons = revision.Buttons;
        Inputs = revision.Inputs;
        Selectors = revision.Selectors;
        Settings = revision.Settings;
        Dropdowns = revision.Dropdowns;
        Anchors = revision.Anchors;
        Lists = revision.Lists;
        Icons = revision.Icons;
    }

    internal KitRevision(bool isDefault, string kitId, string? revisionId)
    {
        Id = revisionId != null ? (int.Parse(revisionId) + 1).ToString() : "1";
        KitId = kitId;
        DateCreated = DateTime.UtcNow;

        Colors =
        [
            new Variable<Color>{ Name = "Primary", Value = new Color{ HexValue = "#0B5FFF" } },
            new Variable<Color>{ Name = "Secondary", Value = new Color{ HexValue = "#72E5DD" } },
            new Variable<Color>{ Name = "Tertiary", Value = new Color{ HexValue = "#FFB46E" } },
            new Variable<Color>{ Name = "Darkest", Value = new Color{ HexValue = "#000000" } },
            new Variable<Color>{ Name = "Lightest", Value = new Color{ HexValue = "#FFFFFF" } },
            new Variable<Color>{ Name = "Error", Value = new Color{ HexValue = "#F96464" } },
            new Variable<Color>{ Name = "Warning", Value = new Color{ HexValue = "#FF8F39" } },
            new Variable<Color>{ Name = "Success", Value = new Color{ HexValue = "#5ACA75" } }
        ];
        Shadows =
        [
            new Variable<Shadow> {Name = "", Value = new Shadow{ Blur = 5, HexColor = "", Spread = 5}}
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

    internal string KitId { get; private set; }
    public DateTime DateCreated { get; private set; }
    internal string? ParentRevisionId { get; private set; }//For child elements/later versions
    public string? Name { get; private set; }

    // Kit settings
    public List<Variable<Color>> Colors { get; private set; }
    public List<Variable<Shadow>> Shadows { get; private set; }
    public Typography Headings { get; private set; }
    public Typography Paragraphs { get; private set; }
    public Button Buttons { get; private set; }
    public Input Inputs { get; private set; }
    public Selector Selectors { get; private set; }
    public KitSettings Settings { get; private set; }
    public Dropdown Dropdowns { get; private set; }
    public Anchor Anchors { get; private set; }
    public List Lists { get; private set; }
    public IconLibrary Icons { get; private set; }
    public bool IsDeleted { get; private set; }

    internal void UpdateSelectors(Selector selectors)
    {
        Selectors = selectors;
    }

    internal void UpdateTypography(Typography headings, Typography paragraphs)
    {
        Headings = headings;
        Paragraphs = paragraphs;
    }

    internal void UpdateSettings(KitSettings settings)
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

    internal Color? GetColor(ColorTypes colorType)
    {
        return Colors.FirstOrDefault(x => x.Name == $"{colorType.ToString().ToLower()}")?.Value;
    }

    internal Dictionary<string, string>? GetGreyscaleColors()
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

    internal Shadow? GetShadow(string shadowSize)
    {
        return Shadows.FirstOrDefault(x => x.Name == shadowSize.ToLower())?.Value;
    }

    internal Dictionary<string, string>? GetShadows()
    {
        var lightest = GetShadow("small");
        var darkest = GetShadow("xlarge");
        if (lightest != null && darkest != null)
            return lightest.Expand(darkest);

        return null;
    }

    internal void UpdateShadows(Shadow smallShadow, Shadow xLargeShadow)
    {
        Shadows.Clear();
        Shadows.Add(new Variable<Shadow>("small", smallShadow));
        Shadows.Add(new Variable<Shadow>("xlarge", xLargeShadow));
    }

    internal void UpdateColor(ColorTypes colorType, string? hexValue)
    {
        Colors.RemoveAll(x => colorType.ToString().ToLower() == x.Name.ToLower());

        if (hexValue != null)
            Colors.Add(new Variable<Color>(colorType.ToString(), new Color(hexValue)));
    }

    internal List<string?> Imports()
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
