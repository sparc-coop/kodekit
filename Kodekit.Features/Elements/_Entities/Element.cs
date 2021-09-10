using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kodekit.Features.Elements
{
    public abstract class Element
    {
        public Element(string name) => Name = name;
        
        public string Name { get; set; }

        //public List<Variation> Variations { get; set; }

        public StringBuilder ToCss(StringBuilder css)
        {
            css.Append(Name)
                .AppendLine(" {");

            var attributes = from p in GetType().GetProperties()
                             let attr = p.GetCustomAttributes(typeof(CssAttribute), true)
                             where attr.Length == 1
                             select new { Property = p, Attribute = attr.First() as CssAttribute };

            foreach (var attribute in attributes)
                css.AppendFormat("    {0}: {1}", attribute.Attribute.Name, attribute.Property.GetValue(this));

            css.AppendLine("}");

            //if (Variations != null)
            //    foreach (var variation in Variations)
            //        css = variation.ToCss(css);

            return css;
        }
    }
}
