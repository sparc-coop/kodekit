using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kodekit.Features.Elements;
using Sparc.Core;
using Sparc.Features;

namespace Kodekit.Features
{
    public class GenerateKit : PublicFeature<string, CreateKitResponse>
    {
        public IRepository<Kit> Kits { get; }
        public GenerateKit(IRepository<Kit> kit) => Kits = kit;

        public override async Task<CreateKitResponse> ExecuteAsync(string kitId)
        {
            var kit = await Kits.FindAsync(kitId);
            if (kit == null)
                throw new Exception("Kit not found!");

            var variables = new Dictionary<string, Dictionary<string, string>>();

            Compile(variables, ":root", kit.Colors);
            Compile(variables, "body", kit.Paragraphs);
            Compile(variables, "h1, h2, h3, h4, h5, h6", kit.Headings);
            Compile(variables, "button", kit.Buttons);
            Compile(variables, "input, textarea", kit.Inputs);
            Compile(variables, "input[type=checkbox]", kit.Selectors);
            Compile(variables, "select", kit.Dropdowns);

            var css = Write(variables);

            return null;
        }


        private void Compile(Dictionary<string, Dictionary<string, string>> variables, string scope, ISerializable element)
        {
            Compile(variables, scope, element.Serialize());
        }

        private void Compile<T>(Dictionary<string, Dictionary<string, string>> variables, string scope, List<Variable<T>> values) where T : ISerializable, new()
        {
            foreach (var value in values)
                Compile(variables, scope, value.Serialize());
        }

        private void Compile(Dictionary<string, Dictionary<string, string>> variables, string scope, Dictionary<string, string> values)
        {
            if (variables.ContainsKey(scope))
                variables[scope] = variables[scope].Concat(values).ToDictionary(x => x.Key, x => x.Value);
            else
                variables.Add(scope, values);
        }

        private static string Write(Dictionary<string, Dictionary<string, string>> variables)
        {
            var css = new StringBuilder();

            foreach (var key in variables.Keys)
            {
                css.Append(key).AppendLine(" {");

                var indent = "    ";

                foreach (var item in variables[key])
                {
                    css.Append(indent);
                    css.AppendLine($"--{item.Key.ToLower()}: {item.Value}");
                }

                css.AppendLine("}");
                css.AppendLine();

                //if (Variations != null)
                //    foreach (var variation in Variations)
                //        css = variation.ToCss(css);
            }

            return css.ToString();
        }
    }
}
