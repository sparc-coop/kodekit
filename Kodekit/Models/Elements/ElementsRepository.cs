﻿using Kodekit.Models.Elements;
using Newtonsoft.Json;

namespace Kodekit;

public class ElementsRepository
{
    public KitRepository? Kits { get; }

    // ANCHORS
    public record UpdateAnchorsModel(string KitId, double? FontSize, string? FontWeight, string? DefaultColor, string? HoverColor, string? VisitedColor, string? ActiveColor);

    public async Task<UpdateAnchorsModel> GetAnchorsAsync(string id)
    {
        var kit = await Kits.GetCurrentRevisionAsync(id);

        return new(
            id,
            kit.Anchors.Font.Size?.Value,
            kit.Anchors.Font.Weight,
            kit.Anchors.DefaultColor?.HexValue,
            kit.Anchors.HoverColor?.HexValue,
            kit.Anchors.VisitedColor?.HexValue,
            kit.Anchors.ActiveColor?.HexValue
        );
    }

    public async Task<Kit> UpdateAnchorsAsync(UpdateAnchorsModel request)
    {
        var kit = await Kits.GetCurrentAsync(request.KitId);

        var anchor = new Anchor(request.FontSize, request.FontWeight, request.DefaultColor, request.HoverColor, request.VisitedColor, request.ActiveColor);

        kit.Revision.UpdateAnchors(anchor);
        await Kits.UpdateAsync(kit);

        return kit.Kit;
    }

    // BUTTONS
    public record UpdateButtonsModel(string KitId, double? FontSize, string? FontWeight, double? VerticalPadding, double? HorizontalPadding, double? CornerRadius, double? BorderWidth, double? IconWidth, double? IconHeight, bool RemoveSecondaryBorder);

    public async Task<UpdateButtonsModel> GetButtonsAsync(string id)
    {
        var kit = await Kits.GetCurrentRevisionAsync(id);

        return new(
            id,
            kit.Buttons.Font.Size?.Value,
            kit.Buttons.Font.Weight,
            kit.Buttons.Padding.Vertical?.Value,
            kit.Buttons.Padding.Horizontal?.Value,
            kit.Buttons.Border.Radius?.Value,
            kit.Buttons.Border.Width?.Value,
            kit.Buttons.IconWidth?.Value,
            kit.Buttons.IconHeight?.Value,
            kit.Buttons.RemoveSecondaryBorder
        );
    }

    public async Task<Kit> UpdateButtonsAsync(UpdateButtonsModel request)
    {
        var kit = await Kits.GetCurrentAsync(request.KitId);

        var buttons = new Button(request.FontSize, request.FontWeight, request.VerticalPadding, request.HorizontalPadding, request.CornerRadius, request.BorderWidth, request.IconWidth, request.IconHeight, request.RemoveSecondaryBorder);

        kit.Revision.UpdateButtons(buttons);
        await Kits.UpdateAsync(kit);

        return kit.Kit;
    }

    // COLORS
    public record ColorsModel(string KitId, string? Primary, string? Secondary, string? Tertiary, string? Darkest, string? Lightest, string? Error, string? Warning, string? Success);

    public async Task<ColorsModel> GetColorAsync(string id)
    {
        var kit = await Kits.GetCurrentRevisionAsync(id);

        return new(id,
            kit.GetColor(ColorTypes.Primary)?.HexValue,
            kit.GetColor(ColorTypes.Secondary)?.HexValue,
            kit.GetColor(ColorTypes.Tertiary)?.HexValue,
            kit.GetColor(ColorTypes.Darkest)?.HexValue,
            kit.GetColor(ColorTypes.Lightest)?.HexValue,
            kit.GetColor(ColorTypes.Error)?.HexValue,
            kit.GetColor(ColorTypes.Warning)?.HexValue,
            kit.GetColor(ColorTypes.Success)?.HexValue);
    }

    public async Task<Kit> UpdateColorsAsync(ColorsModel request)
    {
        var kit = await Kits.GetCurrentAsync(request.KitId);
        var revision = kit.Revision;

        revision.UpdateColor(ColorTypes.Primary, request.Primary);
        revision.UpdateColor(ColorTypes.Secondary, request.Secondary);
        revision.UpdateColor(ColorTypes.Tertiary, request.Tertiary);
        revision.UpdateColor(ColorTypes.Error, request.Error);
        revision.UpdateColor(ColorTypes.Warning, request.Warning);
        revision.UpdateColor(ColorTypes.Success, request.Success);
        revision.UpdateColor(ColorTypes.Lightest, request.Lightest);
        revision.UpdateColor(ColorTypes.Darkest, request.Darkest);

        await Kits.UpdateAsync(kit);
        return kit.Kit;
    }

    // DROPDOWNS
    public record UpdateDropdownsModel(string KitId, double? FontSize, string? FontWeight, double? VerticalPadding, double? HorizontalPadding, double? CornerRadius, double? BorderWidth, bool OverwriteInherited);

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
    public record UpdateIconsModel(string KitId, string? Name, List<IconLibrary> ValidIcons, List<string> IconsList);

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

    public async Task<Kit> UpdatIconsAsync(UpdateIconsModel request)
    {
        var kit = await Kits.GetCurrentAsync(request.KitId);

        var icon = new IconLibrary(request.Name);
        kit.Revision.UpdateIcons(icon);
        await Kits.UpdateAsync(kit);

        return kit.Kit;
    }

    // INPUTS
    public record UpdateInputsModel(string KitId, double? FontSize, string? FontWeight, double? VerticalPadding, double? HorizontalPadding, double? CornerRadius, double? BorderWidth);

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
    public record UpdateListsModel(string KitId, double? FontSize, string? FontWeight, string? OlStyleType, string? UlStyleType, double? ListHorizontalPadding, double? ListVerticalPadding, double? ItemHorizontalPadding, double? ItemVerticalPadding);

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
    public record UpdateSelectorsModel(string KitId, double? FontSize, string? FontWeight, string? ActiveColor);

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
    public record UpdateSettingsModel(string KitId, KitSettings Settings);

    public async Task<UpdateSettingsModel> GetKitSettingsAsync(string id)
    {
        var kit = await Kits.GetCurrentRevisionAsync(id);
        return new(id, kit.Settings);
    }

    public async Task<Kit> UpdateKitSettingsAsync(UpdateSettingsModel request)
    {
        var kit = await Kits.GetCurrentAsync(request.KitId);

        kit.Revision.UpdateSettings(request.Settings);
        await Kits.UpdateAsync(kit);

        return kit.Kit;
    }

    // SHADOWS
    public record ShadowsModel(string KitId, Shadow? Small, Shadow? XLarge);

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
    public record GetTypographyResponse(TypographyModel Heading, TypographyModel Paragraph, Dictionary<double, string> TypeScales);
    public record TypographyModel(string? FontFamily, string? FontWeight, double? FontSize, double? TypeScale, double? LineHeight, Dictionary<string, string>? FontSizeOverrides, Dictionary<string, string> TypeScaleValues);
    public record GetWeightsModel(Dictionary<string, string> Weights);
    public record UpdateTypographyModel(string KitId, TypographyModel Headings, TypographyModel Paragraphs);

    public Task<GetWeightsModel> GetWeightsAsync()
    {
        return Task.FromResult(new GetWeightsModel(Font.ValidWeights));
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