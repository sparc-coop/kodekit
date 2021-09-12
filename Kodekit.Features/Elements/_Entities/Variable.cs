using System.Linq;

namespace Kodekit.Features.Elements
{
    public class Variable<T>
    {
        public Variable()
        { }
        
        public Variable(string name, T value)
        {
            Name = name.ToLower();
            Value = value;
        }

        public string Name { get; set; }
        public T Value { get; set; }

        public override string ToString()
        {
            var cleanValue = Value!.ToString();
            
            if (cleanValue.Contains(":"))
                cleanValue = cleanValue.Split(":").Last().Trim();

            return $"{Name}: ${cleanValue};";
        }
    }

    public class Variables
    {
        public const string Serif = "$serif";
    }
}
