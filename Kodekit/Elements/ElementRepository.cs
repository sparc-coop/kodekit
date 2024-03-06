using Kodekit.Models.Elements;
using Newtonsoft.Json;

namespace Kodekit;

public class ElementRepository
{
    public KitRepository Kits { get; }
    
    // DROPDOWNS
    public async Task<UpdateDropdownsModel> GetDropdownsAsync(string id)
    {
        var kit = await Kits.GetCurrentRevisionAsync(id);

        return new(
            id,
            kit.Dropdowns.Font.Size?.Value,
            kit.Dropdowns.Font.Weight,
            kit.Dropdowns.Padding.Vertical?.Value,
            kit.Dropdowns.Padding.Horizontal?.Value,
            kit.Dropdowns.Border.Radius?.Value,
            kit.Dropdowns.Border.Width?.Value,
            kit.Dropdowns.OverwriteInherited
        );
    }

    public async Task<Kit> UpdateDropdownsAsync(UpdateDropdownsModel request)
    {
        var kit = await Kits.GetCurrentAsync(request.KitId);

        var Dropdowns = new Dropdown(request.FontSize, request.FontWeight, request.VerticalPadding, request.HorizontalPadding, request.CornerRadius, request.BorderWidth, request.OverwriteInherited);

        kit.Revision.UpdateDropdowns(Dropdowns);
        await Kits.UpdateAsync(kit);

        return kit.Kit;
    }

    // ICONS
    public async Task<UpdateIconsModel> GetIconsAsync(string id)
    {
        var kit = await Kits.GetCurrentRevisionAsync(id);

        return new(
            id,
            kit.Icons.Name,
            IconLibrary.GetValidIcons(),
            await kit.Icons.GetRandomIconsAsync()
        );
    }

    public async Task<Kit> UpdateIconsAsync(UpdateIconsModel request)
    {
        var kit = await Kits.GetCurrentAsync(request.KitId);

        var icon = new IconLibrary(request.Name);
        kit.Revision.UpdateIcons(icon);
        await Kits.UpdateAsync(kit);

        return kit.Kit;
    }

    // INPUTS
    public async Task<UpdateInputsModel> GetInputsAsync(string id)
    {
        var kit = await Kits.GetCurrentRevisionAsync(id);

        return new(
            id,
            kit.Inputs.Font.Size?.Value,
            kit.Inputs.Font.Weight,
            kit.Inputs.Padding.Vertical?.Value,
            kit.Inputs.Padding.Horizontal?.Value,
            kit.Inputs.Border.Radius?.Value,
            kit.Inputs.Border.Width?.Value
        );
    }

    public async Task<Kit> UpdateInputsAsync(UpdateInputsModel request)
    {
        var kit = await Kits.GetCurrentAsync(request.KitId);

        var input = new Input(request.FontSize, request.FontWeight, request.VerticalPadding,
            request.HorizontalPadding, request.CornerRadius, request.BorderWidth);

        kit.Revision.UpdateInputs(input);
        await Kits.UpdateAsync(kit);

        return kit.Kit;
    }

    // LISTS
    public async Task<UpdateListsModel> GetListsAsync(string id)
    {
        var kit = await Kits.GetCurrentRevisionAsync(id);

        return new(
            id,
            kit.Lists.Font.Size?.Value,
            kit.Lists.Font.Weight,
            kit.Lists.OrderedListStyleType,
            kit.Lists.UnorderedListStyleType,
            kit.Lists.ListPadding?.Horizontal?.Value,
            kit.Lists.ItemPadding?.Vertical?.Value,
            kit.Lists.ListPadding?.Horizontal?.Value,
            kit.Lists.ItemPadding?.Vertical?.Value
        );
    }
    public async Task<Kit> UpdateListsAsync(UpdateListsModel request)
    {
        var kit = await Kits.GetCurrentAsync(request.KitId);

        var List = new List(request.FontSize, request.FontWeight, request.OlStyleType, request.UlStyleType,
            request.ListHorizontalPadding, request.ListVerticalPadding,
            request.ItemHorizontalPadding, request.ItemVerticalPadding);

        kit.Revision.UpdateLists(List);
        await Kits.UpdateAsync(kit);

        return kit.Kit;
    }

    // SELECTORS
    public async Task<UpdateSelectorsModel> GetSelectorsAsync(string id)
    {
        var kit = await Kits.GetCurrentRevisionAsync(id);

        return new(id, kit.Selectors.Font.Size?.Value, kit.Selectors.Font.Weight, kit.Selectors.ActiveColor?.HexValue);
    }
    public async Task<Kit> UpdateSelectorsAsync(UpdateSelectorsModel request)
    {
        var kit = await Kits.GetCurrentAsync(request.KitId);

        kit.Revision.UpdateSelectors(new Selector(request.FontSize, request.FontWeight, request.ActiveColor));

        await Kits.UpdateAsync(kit);

        return kit.Kit;
    }

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