using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodekit.Features.Elements
{
    public class Variable<T>
    {
        public Variable(string friendlyName, string internalName)
        {
            FriendlyName = friendlyName;
            InternalName = internalName;
        }

        public string FriendlyName { get; set; }
        public string InternalName { get; set; }
        public T Value { get; set; }
    }
}
