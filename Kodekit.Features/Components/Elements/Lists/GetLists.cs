namespace Kodekit.Features.Components.Elements;

public class GetLists : PublicFeature<string, UpdateListsModel>
{
    public GetLists(KitRepository kits)
    {
        Kits = kits;
    }

    public KitRepository Kits { get; }

    public override async Task<UpdateListsModel> ExecuteAsync(string id)
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
}
