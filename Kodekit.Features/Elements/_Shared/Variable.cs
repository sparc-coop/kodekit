using System.Collections.Generic;
using System.Linq;

namespace Kodekit.Features.Elements
{
    public class Variable<T> : ISerializable where T : ISerializable, new()
    {
        public Variable()
        {
            Name = string.Empty;
            Value = new();
        }
        
        public Variable(string name, T value)
        {
            Name = name.ToLower();
            Value = value;
        }

        public string Name { get; set; }
        public T Value { get; set; }

        public Dictionary<string, string> Serialize()
        {
            return Value.Serialize().ToDictionary(x => $"{Name}-{x.Key}", x => x.Value);
        }
    }
}
