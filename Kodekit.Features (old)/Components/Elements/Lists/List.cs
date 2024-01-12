namespace Kodekit.Features.Components.Elements;

public class List : ISerializable
{
    public List() : base()
    {
        Font = new();
        ListPadding = new();
        ItemPadding = new();
    }

    public List(double? fontSize, string? fontWeight, string? olStyleType, string? ulStyleType, double? listHorizontalPadding, double? listVerticalPadding,
        double? itemHorizontalPadding, double? itemVerticalPadding) : this()
    {
        Font = new(fontSize, fontWeight);
        OrderedListStyleType = olStyleType;
        UnorderedListStyleType = ulStyleType;
        ListPadding = new(listHorizontalPadding, listVerticalPadding);
        ItemPadding = new(itemHorizontalPadding, itemVerticalPadding);
    }

    public Font Font { get; set; }
    public string? OrderedListStyleType { get; set; }
    public string? UnorderedListStyleType { get; set; }
    public Padding ListPadding { get; set; }
    public Padding ItemPadding { get; set; }


    public Dictionary<string, string> Serialize()
    {
        var dict = Font.Concat(ListPadding).Concat(ItemPadding.Serialize("li"))
            .ToDictionary(x => x.Key, x => x.Value);

        if (OrderedListStyleType != null)
            dict.Add("ol-style-type", OrderedListStyleType);

        if (UnorderedListStyleType != null)
            dict.Add("ul-style-type", UnorderedListStyleType);

        return dict;
    }
}
