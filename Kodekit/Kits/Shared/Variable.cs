namespace Kodekit;

public class Variable<T> : ISerializable where T : ISerializable
{
    internal Variable()
    {
        Name = string.Empty;
        Value = default!;
    }

    internal Variable(string name, T value)
    {
        Name = name.ToLower();
        Value = value;
    }

    internal string Name { get; set; }
    internal T Value { get; set; }

    public Dictionary<string, string> Serialize()
    {
        return Value.Serialize().ToDictionary(x => $"{Name}-{x.Key}", x => x.Value);
    }
}
