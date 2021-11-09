namespace Kodekit.Features.Elements;

public record Size
{
    public double Value { get; set; }
    public string Unit { get; set; }
    public string Placeholder { get; set; }

    public Size()
    {
        Value = 0;
        Unit = "px";
        Placeholder = ValidUnits["px"];
    }

    public Size(double size) : this()
    {
        Value = size;
    }

    public static Dictionary<string, string> ValidUnits = new()
    {
        { "px", "pixels" },
        { "pt", "points" },
        { "em", "relative to current font-size" },
        { "rem", "relative to root font-size" },
        { "vw", "relative to viewport width" },
        { "vh", "relative to viewport height" },
        { "%", "percentage" }
    };

    public override string ToString() => $"{Value}{Unit}";

    public Size Scale(double typeScale, int power)
    {
        var result = power > 0
            ? this with { Value = Value * Math.Pow(typeScale, power) }
            : this with { Value = Value / Math.Pow(typeScale, power * -1) };

        result.Value = Math.Round(result.Value, 2);

        return result;
    }
}
