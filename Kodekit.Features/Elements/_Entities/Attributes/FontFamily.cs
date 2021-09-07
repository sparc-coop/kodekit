using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodekit.Features.Elements
{
    public record FontFamily
    {
        public string FriendlyName { get; set; }
        public string InternalName { get; set; }
        public string SourceUrl { get; set; }

        public override string ToString() => $"'{InternalName}'";
    }
}
