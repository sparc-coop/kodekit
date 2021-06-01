using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodekit.Core.Kits
{

    public class Variables
    {
        public string Primary { get; set; }
        public string Secondary { get; set; }
        public string Tertiary { get; set; }
        public string LightGrey { get; set; }
        public string DarkGrey { get; set; }
    }
    public class Body
    {
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
        public string TypeScale { get; set; }
        public string LineHeight { get; set; }
    }
    public class Header
    {
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
        public string TypeScale { get; set; }
        public string LineHeight { get; set; }

        public H1 H1 { get; set; }
    }
    
    public class Nav
    {
        public string Margin { get; set; }
        public string Padding { get; set; }
    }

    public class Section
    {
        public string Margin { get; set; }
        public string Padding { get; set; }
    }

    public class Article
    {
        public string Margin { get; set; }
        public string Padding { get; set; }
    }

    public class Aside
    {
        public string Margin { get; set; }
        public string Padding { get; set; }
    }

    public class Footer
    {
        public string Margin { get; set; }
        public string Padding { get; set; }
    }

    public class H1
    {
        public string FontFamily { get; set; }
        public string FontSize { get; set; }
        public string TypeScale { get; set; }
        public string LineHeight { get; set; }

    }
}
