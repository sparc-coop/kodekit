﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kodekit.Features.Elements;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Sparc.Core;

namespace Kodekit.Features
{
    public class GenerateKit : Controller
    {
        public IRepository<Kit> Kits { get; }
        public string RootPath { get; }

        public GenerateKit(IRepository<Kit> kits, IWebHostEnvironment env)
        {
            Kits = kits;
            RootPath = env.ContentRootPath;
        }

        [HttpGet("/{kitId}.css")]
        public async Task<IActionResult> HandleAsync(string kitId, bool? preview)
        {
            var kit = await Kits.FindAsync(kitId);

            if (kit == null)
                throw new Exception("Kit not found!");

            //if (kit.IsPublished != true)
            //{
                // Todo: lookup the kit that is actually published
            //}

            var css = new StringBuilder();

            foreach (var url in kit.Imports())
                css.AppendLine($"@import url('{url}');");

            css.AppendLine(CompileVariables(kit));
            css.AppendLine(GetLocalFile("Elements/_Shared/destyle-reset.css"));
            css.AppendLine(GetLocalFile("Elements/Typography/typography.css"));
            css.AppendLine(GetLocalFile("Elements/Buttons/buttons.css"));
            css.AppendLine(GetLocalFile("Elements/Inputs/inputs.css"));

            return new ContentResult
            {
                ContentType = "text/css",
                Content = css.ToString(),
                StatusCode = 200
            };
        }

        private string CompileVariables(Kit kit)
        {
            var variables = new Dictionary<string, Dictionary<string, string>>();

            Compile(variables, ":root", kit.Colors.Where(x => x.Name != "lightest" && x.Name != "darkest").ToList());

            // Hack for greyscale calculation, I hate it
            var lightest = kit.GetColor(ColorTypes.Lightest);
            var darkest = kit.GetColor(ColorTypes.Darkest);
            if (lightest != null && darkest != null)
                Compile(variables, ":root", lightest.Expand(darkest));

            Compile(variables, "body", kit.Paragraphs);
            Compile(variables, "h1, h2, h3, h4, h5, h6", kit.Headings);
            Compile(variables, "button", kit.Buttons);
            Compile(variables, "input[type=checkbox]", kit.Selectors);

            Compile(variables, "input, textarea" + (!kit.Dropdowns.OverwriteInherited ? ", select" : ""), kit.Inputs);
            if (kit.Dropdowns.OverwriteInherited)
                Compile(variables, "select", kit.Dropdowns);

            var css = Write(variables);
            return css;
        }

        private void Compile(Dictionary<string, Dictionary<string, string>> variables, string scope, ISerializable element)
        {
            Compile(variables, scope, element.Serialize());
        }

        private void Compile<T>(Dictionary<string, Dictionary<string, string>> variables, string scope, List<Variable<T>> values) where T : ISerializable, new()
        {
            foreach (var value in values)
            {
                Compile(variables, scope, value.Serialize());
            }
        }

        private void Compile(Dictionary<string, Dictionary<string, string>> variables, string scope, Dictionary<string, string> values)
        {
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
