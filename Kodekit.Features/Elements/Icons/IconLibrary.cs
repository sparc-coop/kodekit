using System.Net.Http;
using System.Text.RegularExpressions;

namespace Kodekit.Features.Elements;

public class IconLibrary
{
    public IconLibrary()
    { }

    public IconLibrary(string? name)
    {
        Name = name;
        Url = GetValidIcons().FirstOrDefault(x => x.Name == name)?.Url;
    }

    public IconLibrary(string? name, string? url, string? markup)
    {
        Name = name;
        Url = url;
        Markup = markup;
    }

    public string? Name { get; set; }
    public string? Url { get; set; }
    public string? Markup { get; set; }

    public static List<IconLibrary> GetValidIcons()
    {
        return new()
        {
            new("Material", "https://fonts.googleapis.com/icon?family=Material+Icons", "<span class='material-icons'>%</span>"),
            new("Remix", "https://cdn.jsdelivr.net/npm/remixicon@2.5.0/fonts/remixicon.css", "<i class='ri-%'></i>"),
            new("CSS", "https://css.gg/css", "<i class='gg-%'></i>"),
            new("FontAwesome", "https://use.fontawesome.com/releases/v5.15.4/css/all.css", "<i class='fas fa-%'></i>")
        };
    }

    public async Task<List<string>> GetRandomIconsAsync()
    {
        if (string.IsNullOrWhiteSpace(Url)) return new();

        var css = await new HttpClient().GetStringAsync(Url);

        var search = Name switch
        {
            "Material" => "",
            "Remix" => "ri-",
            "CSS" => "gg-",
            "FontAwesome" => "fa-",
            _ => ""
        };

        if (search == string.Empty) return new();

        return Regex.Matches(css, @"\.(" + search + @"[a-z\-]+)")
            .Select(x => x.Groups[1]?.Value)
            .Where(x => x != null)
            .Distinct()
            .ToList();
    }
}
