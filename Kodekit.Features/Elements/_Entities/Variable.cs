namespace Kodekit.Features.Elements
{
    public class Variable<T>
    {
        public Variable(string friendlyName, string internalName, T value)
        {
            FriendlyName = friendlyName;
            InternalName = internalName;
            Value = value;
        }

        public string FriendlyName { get; set; }
        public string InternalName { get; set; }
        public T Value { get; set; }
    }
}
