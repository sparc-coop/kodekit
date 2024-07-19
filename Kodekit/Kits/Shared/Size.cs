namespace Kodekit;

public class Size
{
    public double Value { get; set; }
    public string Unit { get; set; }
    public string Placeholder { get; set; }

    internal Size()
    {
        Value = 0;
        Unit = "px";
        Placeholder = ValidUnits["px"];
    }

    internal Size(double size) : this()
    {
        Value = size;
    }

    internal static Dictionary<string, string> ValidUnits = new()
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

    internal Size Scale(double typeScale, int power)
    {
        var result = power > 0
            ? new Size(Value * Math.Pow(typeScale, power))
            : new Size(Value / Math.Pow(typeScale, power * -1));

        result.Value = Math.Round(result.Value, 2);

        return result;
    }
}
