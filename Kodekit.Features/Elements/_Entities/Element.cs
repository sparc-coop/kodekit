using System.Text;

namespace Kodekit.Features.Elements
{
    public abstract class Element
    {
        public string ToCss(string name, params object[] elements)
        {
            var css = new StringBuilder();
            css.Append(name)
                .AppendLine(" {");

            var indent = "    ";
            foreach (var element in elements)
                css.Append(element.ToString()!.Replace("\n", $"\n{indent}"));

            css.AppendLine("}");
            css.AppendLine();

            //if (Variations != null)
            //    foreach (var variation in Variations)
            //        css = variation.ToCss(css);

            return css.ToString();
        }
    }
}
