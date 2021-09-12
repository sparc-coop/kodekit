using System.Collections.Generic;
using System.Linq;

namespace Kodekit.Features.Elements
{
    public interface ISerializable
    {
        public Dictionary<string, string> Serialize();
    }

    public static class ISerializableExtensions
    {
        public static Dictionary<string, string> Concat(this ISerializable item, ISerializable item2)
        {
            return item.Concat(item2).ToDictionary(x => x.Key, x => x.Value);
        }

        public static Dictionary<string, string> Concat(this Dictionary<string, string> item, ISerializable item2)
        {
            return item.Concat(item2.Serialize())
                .ToDictionary(x => x.Key, x => x.Value);
        }
    }
}
