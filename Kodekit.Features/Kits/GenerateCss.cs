using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kodekit.Features.Elements;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using SharpScss;
using Sparc.Features;

namespace Kodekit.Features
{
    public class GenerateCss : Controller
    {
        public KitRepository Kits { get; }
        public string RootPath { get; }

        public GenerateCss(KitRepository kits, IWebHostEnvironment env)
        {
            Kits = kits;
            RootPath = env.ContentRootPath;
        }

        [HttpGet("/{kitId}.css")]
        public async Task<IActionResult> HandleAsync(string kitId, string? v, string? scope)
        {
            var kit = v == null
                ? await Kits.GetPublishedAsync(kitId)
                : await Kits.GetKitAndRevisionAsync(kitId, v);

            if (kit.Revision == null)
                throw new NotFoundException($"Revision {v} does not exist!");

            if (!string.IsNullOrWhiteSpace(scope) && !scope.StartsWith("."))
                scope = $".{scope}";

            var css = new StringBuilder();

            foreach (var url in kit.Revision.Imports())
                css.AppendLine($"@import url('{url}');");

            css.AppendLine(CompileVariables(kit.Revision, scope));
            css.AppendLine(GetLocalFile("Elements/_Shared/destyle-reset.css"));

            if (kit.Revision.Settings.UseTypography)
                css.AppendLine(GetLocalFile("Elements/Typography/typography.css"));

            if (kit.Revision.Settings.UseButtons) 
                css.AppendLine(GetLocalFile("Elements/Buttons/buttons.css"));

            if (kit.Revision.Settings.UseInputs)
                css.AppendLine(GetLocalFile("Elements/Inputs/inputs.css"));

            if (kit.Revision.Settings.UseAnchors)
                css.AppendLine(GetLocalFile("Elements/Anchors/anchors.css"));

            if (kit.Revision.Settings.UseLists)
                css.AppendLine(GetLocalFile("Elements/Lists/lists.css"));

            if (kit.Revision.Settings.UseShadows)
                css.AppendLine(GetLocalFile("Elements/Effects/shadows.css"));

            var result = css.ToString();
            if (!string.IsNullOrWhiteSpace(scope))
            {
                result = Scss.ConvertToCss(scope + " { " + result + " }").Css;
            }

            return new ContentResult
            {
                ContentType = "text/css",
                Content = result,
                StatusCode = 200
            };
        }

        private string CompileVariables(KitRevision kit, string? scope)
        {
            if (scope == null)
                scope = ":root";
            else
                scope = "*";

            var variables = new Dictionary<string, Dictionary<string, string>>();

            Compile(variables, scope, kit.Colors.Where(x => x.Name != "lightest" && x.Name != "darkest").ToList());
            Compile(variables, scope, kit.GetGreyscaleColors());
            Compile(variables, scope, kit.GetShadows());

            if (kit.Settings.UseTypography)
            {
                Compile(variables, scope, kit.Paragraphs);
                Compile(variables, "h1, h2, h3, h4, h5, h6, .subtitle", kit.Headings);
            }

            if (kit.Settings.UseButtons)
                Compile(variables, "button", kit.Buttons);

            if (kit.Settings.UseInputs)
            {
                Compile(variables, "input, label, textarea" + (!kit.Dropdowns.OverwriteInherited ? ", select" : ""), kit.Inputs);
                Compile(variables, "input[type=checkbox], input[type=radio], label.switch, label.switch .slider", kit.Selectors);
                if (kit.Dropdowns.OverwriteInherited)
                    Compile(variables, "select", kit.Dropdowns);
            }

            if (kit.Settings.UseAnchors)
                Compile(variables, "a", kit.Anchors);

            if (kit.Settings.UseLists)
                Compile(variables, "ol, ul, li", kit.Lists);

            var css = Write(variables);
            return css;
        }

        private static void Compile(Dictionary<string, Dictionary<string, string>> variables, string scope, ISerializable element)
        {
            Compile(variables, scope, element.Serialize());
        }

        private static void Compile<T>(Dictionary<string, Dictionary<string, string>> variables, string scope, List<Variable<T>> values) where T : ISerializable, new()
        {
            foreach (var value in values)
            {
                Compile(variables, scope, value.Serialize());
            }
        }

        private static void Compile(Dictionary<string, Dictionary<string, string>> variables, string scope, Dictionary<string, string>? values)
        {
            if (values == null)
                return;
            
            if (variables.ContainsKey(scope))
            {
                variables[scope] = variables[scope].Concat(values.Where(x => !variables[scope].ContainsKey(x.Key))).ToDictionary(x => x.Key, x => x.Value);
            }
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
                    css.AppendLine($"--{item.Key.ToLower()}: {item.Value};");
                }

                css.AppendLine("}");
                css.AppendLine();

                //if (Variations != null)
                //    foreach (var variation in Variations)
                //        css = variation.ToCss(css);
            }

            return css.ToString();
        }

        private string GetLocalFile(string filename) => System.IO.File.ReadAllText(System.IO.Path.Combine(RootPath, filename));
    }
}
