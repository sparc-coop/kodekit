using Kodekit.Features.Components.Elements;

namespace Kodekit.Features;

public class KitRevision : Root<string>
{
    private KitRevision()
    {
        Id = string.Empty;
        KitId = string.Empty;
        DateCreated = DateTime.UtcNow;

        Colors = new();
        Shadows = new();

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
    }

    public KitRevision(KitRevision revision) : this()
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

    public KitRevision(bool isDefault, string kitId, string? revisionId)
    {
        Id = revisionId != null ? (int.Parse(revisionId) + 1).ToString() : "1";
        KitId = kitId;
        DateCreated = DateTime.UtcNow;

        Colors = new List<Variable<Kodekit.Features.Components.Elements.Color>>
        {
            new Variable<Kodekit.Features.Components.Elements.Color>{ Name = "Primary", Value = new Kodekit.Features.Components.Elements.Color{ HexValue = "#0B5FFF" } },
            new Variable<Kodekit.Features.Components.Elements.Color>{ Name = "Secondary", Value = new Kodekit.Features.Components.Elements.Color{ HexValue = "#72E5DD" } },
            new Variable<Kodekit.Features.Components.Elements.Color>{ Name = "Tertiary", Value = new Kodekit.Features.Components.Elements.Color{ HexValue = "#FFB46E" } },
            new Variable<Kodekit.Features.Components.Elements.Color>{ Name = "Darkest", Value = new Kodekit.Features.Components.Elements.Color{ HexValue = "#000000" } },
            new Variable<Kodekit.Features.Components.Elements.Color>{ Name = "Lightest", Value = new Kodekit.Features.Components.Elements.Color{ HexValue = "#FFFFFF" } },
            new Variable<Kodekit.Features.Components.Elements.Color>{ Name = "Error", Value = new Kodekit.Features.Components.Elements.Color{ HexValue = "#F96464" } },
            new Variable<Kodekit.Features.Components.Elements.Color>{ Name = "Warning", Value = new Kodekit.Features.Components.Elements.Color{ HexValue = "#FF8F39" } },
            new Variable<Kodekit.Features.Components.Elements.Color>{ Name = "Success", Value = new Kodekit.Features.Components.Elements.Color{ HexValue = "#5ACA75" } }
        };
        Shadows = new List<Variable<Kodekit.Features.Components.Elements.Shadow>>
        {
            new Variable<Kodekit.Features.Components.Elements.Shadow> {Name = "", Value = new Kodekit.Features.Components.Elements.Shadow{ Blur = 5, HexColor = "", Spread = 5}}
        };
        Headings = new Kodekit.Features.Components.Elements.Typography("Roboto", "400", 16, 1.250, 120, null);
        Paragraphs = new Kodekit.Features.Components.Elements.Typography("Roboto", "500", 17, 1.200, 160, null);

        Buttons = new Kodekit.Features.Components.Elements.Button(16, "500", 8, 8, 4, 0, 16, 16, false);
        Inputs = new Kodekit.Features.Components.Elements.Input(16, "400", 10, 16, 4, 1);
        Selectors = new Kodekit.Features.Components.Elements.Selector(16, "400", "#0B5FFF");
        Settings = new();
        Dropdowns = new Kodekit.Features.Components.Elements.Dropdown(16, "400", 10, 16, 4, 1, true);
        Anchors = new Kodekit.Features.Components.Elements.Anchor(16, "400", "#17171A", "#0B5FFF", "#7FABFF", "#00BFB2");
        Lists = new Kodekit.Features.Components.Elements.List (16, "400", "upper-roman", "None", 0, 0, 0, 8);
        Icons = new Kodekit.Features.Components.Elements.IconLibrary("Material", "https://fonts.googleapis.com/icon?family=Material+Icons", "<span class='material-icons'>%</span>");
    }

    public string KitId { get; set; }
    public DateTime DateCreated { get; set; }
    public string? ParentRevisionId { get; set; }//For child elements/later versions
    public string? Name { get; set; }

    // Kit settings
    public List<Variable<Kodekit.Features.Components.Elements.Color>> Colors { get; set; }
    public List<Variable<Kodekit.Features.Components.Elements.Shadow>> Shadows { get; set; }
    public Kodekit.Features.Components.Elements.Typography Headings { get; set; }
    public Kodekit.Features.Components.Elements.Typography Paragraphs { get; set; }
    public Kodekit.Features.Components.Elements.Button Buttons { get; set; }
    public Kodekit.Features.Components.Elements.Input Inputs { get; set; }
    public Kodekit.Features.Components.Elements.Selector Selectors { get; set; }
    public KitSettings Settings { get; set; }
    public Kodekit.Features.Components.Elements.Dropdown Dropdowns { get; set; }
    public Kodekit.Features.Components.Elements.Anchor Anchors { get; set; }
    public Kodekit.Features.Components.Elements.List Lists { get; set; }
    public Kodekit.Features.Components.Elements.IconLibrary Icons { get; set; }
    public bool IsDeleted { get; set; }

    public void UpdateSelectors(Kodekit.Features.Components.Elements.Selector selectors)
    {
        Selectors = selectors;
    }

    public void UpdateTypography(Kodekit.Features.Components.Elements.Typography headings, Kodekit.Features.Components.Elements.Typography paragraphs)
    {
        Headings = headings;
        Paragraphs = paragraphs;
    }

    public void UpdateSettings(KitSettings settings)
    {
        Settings = settings;
    }

    internal void UpdateInputs(Kodekit.Features.Components.Elements.Input input)
    {
        Inputs = input;
    }

    internal void UpdateDropdowns(Kodekit.Features.Components.Elements.Dropdown dropdowns)
    {
        Dropdowns = dropdowns;
    }

    internal void UpdateAnchors(Kodekit.Features.Components.Elements.Anchor anchor)
    {
        Anchors = anchor;
    }

    internal void UpdateLists(Kodekit.Features.Components.Elements.List list)
    {
        Lists = list;
    }

    internal void UpdateIcons(Kodekit.Features.Components.Elements.IconLibrary icon)
    {
        Icons = icon;
    }

    public Kodekit.Features.Components.Elements.Color? GetColor(Kodekit.Features.Components.Elements.ColorTypes colorType)
    {
        return Colors.FirstOrDefault(x => x.Name == $"{colorType.ToString().ToLower()}")?.Value;
    }

    public Dictionary<string, string>? GetGreyscaleColors()
    {
        var lightest = GetColor(Kodekit.Features.Components.Elements.ColorTypes.Lightest);
        var darkest = GetColor(Kodekit.Features.Components.Elements.ColorTypes.Darkest);
        if (lightest != null && darkest != null)
            return lightest.Expand(darkest);

        return null;
    }

    internal void UpdateButtons(Kodekit.Features.Components.Elements.Button buttons)
    {
        Buttons = buttons;
    }

    public Kodekit.Features.Components.Elements.Shadow? GetShadow(string shadowSize)
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

    public void UpdateShadows(Kodekit.Features.Components.Elements.Shadow smallShadow, Kodekit.Features.Components.Elements.Shadow xLargeShadow)
    {
        Shadows.Clear();
        Shadows.Add(new Variable<Kodekit.Features.Components.Elements.Shadow>("small", smallShadow));
        Shadows.Add(new Variable<Kodekit.Features.Components.Elements.Shadow>("xlarge", xLargeShadow));
    }

    public void UpdateColor(Kodekit.Features.Components.Elements.ColorTypes colorType, string? hexValue)
    {
        Colors.RemoveAll(x => colorType.ToString().ToLower() == x.Name.ToLower());

        if (hexValue != null)
            Colors.Add(new Variable<Kodekit.Features.Components.Elements.Color>(colorType.ToString(), new Kodekit.Features.Components.Elements.Color(hexValue)));
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
