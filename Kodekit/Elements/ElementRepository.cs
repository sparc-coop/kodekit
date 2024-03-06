using Kodekit.Models.Elements;
using Newtonsoft.Json;

namespace Kodekit;

public class ElementRepository
{
    public KitRepository Kits { get; }
   
    // SETTINGS
    public async Task<UpdateSettingsModel> GetSettingsAsync(string id)
    {
        var kit = await Kits.GetCurrentRevisionAsync(id);
        return new(id, kit.Settings);
    }

    public async Task<Kit> UpdateSettingsAsync(UpdateSettingsModel request)
    {
        var kit = await Kits.GetCurrentAsync(request.KitId);

        kit.Revision.UpdateSettings(request.Settings);
        await Kits.UpdateAsync(kit);

        return kit.Kit;
    }

    // SHADOWS
    public async Task<ShadowsModel> GetShadowsAsync(string id)
    {
        var kit = await Kits.GetCurrentRevisionAsync(id);
        if (kit == null)
            throw new NotFoundException("Kit not found!");

        var smallShadow = kit.GetShadow("small");
        var xlargeShadow = kit.GetShadow("xlarge");

        return new(id, smallShadow ?? new Shadow(), xlargeShadow ?? new Shadow());
    }
    public async Task<Kit> UpdateShadowsAsync(ShadowsModel request)
    {
        var kit = await Kits.GetCurrentAsync(request.KitId);

        if (request.Small != null && request.XLarge != null)
            kit.Revision.UpdateShadows(request.Small, request.XLarge);

        await Kits.UpdateAsync(kit);
        return kit.Kit;
    }

    // TYPOGRAPHY
    public async Task<GetTypographyResponse> GetTypographyAsync(string id)
    {
        var kit = await Kits.GetCurrentRevisionAsync(id);
        if (kit == null)
            throw new NotFoundException("Kit not found!");


        var heading = new TypographyModel(
            kit.Headings.Font.Family,
            kit.Headings.Font.Weight,
            kit.Headings.Font.Size?.Value,
            kit.Headings.TypeScale,
            kit.Headings.Font.LineHeight?.Value,
            kit.Headings.FontSizeOverrides,
            kit.Headings.Serialize() // to show on preview example
        );

        var paragraph = new TypographyModel(
            kit.Paragraphs.Font.Family,
            kit.Paragraphs.Font.Weight,
            kit.Paragraphs.Font.Size?.Value,
            kit.Paragraphs.TypeScale,
            kit.Paragraphs.Font.LineHeight?.Value,
            kit.Paragraphs.FontSizeOverrides,
            kit.Paragraphs.Serialize() // to show on preview example
        );

        return new(heading, paragraph, Typography.TypeScales);
    }

    public Task<Dictionary<string, string>> GetWeightsAsync()
    {
        return Task.FromResult(new Dictionary<string, string>(Font.ValidWeights));
    }

    public async Task<FontListResponse> GetGoogleFontsAsync(string colors)
    {
        string baseurl = "https://www.googleapis.com/webfonts/v1/webfonts?key=";

        HttpClient client = new();
        string key = "AIzaSyCVQkjhKXtzJlXxMCEWQN5Yi52gInslEZE";
        FontListResponse fonts = new();

        try
        {
            var apiurl = baseurl + key;
            var url = new Uri(apiurl);
            var response = await client.GetAsync(url);
            var responseBody = await response.Content.ReadAsStringAsync();

            fonts = JsonConvert.DeserializeObject<FontListResponse>(responseBody);
            fonts.Items = fonts.Items?.Where(x => x.Category == "serif" || x.Category == "sans-serif").ToList();
            //fonts.Items.RemoveAll(x => x.Category == "handwriting");
            // .Where(x => x.Category != "handwriting").ToList();
            client.Dispose();
            return fonts;
        }
        catch (Exception ex)
        {
            string checkResult = "Error " + ex.ToString();
            client.Dispose();
            return fonts;
        }

    }
    public async Task<Kit> UpdateTypographyAsync(UpdateTypographyModel request)
    {
        var kit = await Kits.GetCurrentAsync(request.KitId);

        var headings = new Typography(
            request.Headings.FontFamily,
            request.Headings.FontWeight,
            request.Headings.FontSize,
            request.Headings.TypeScale,
            request.Headings.LineHeight,
            request.Headings.FontSizeOverrides);

        var paragraphs = new Typography(
            request.Paragraphs.FontFamily,
            request.Paragraphs.FontWeight,
            request.Paragraphs.FontSize,
            request.Paragraphs.TypeScale,
            request.Paragraphs.LineHeight,
            request.Paragraphs.FontSizeOverrides);

        kit.Revision.UpdateTypography(headings, paragraphs);
        await Kits.UpdateAsync(kit);

        return kit.Kit;
    }
}