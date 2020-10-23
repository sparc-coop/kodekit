using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UI_Kit.Model;

namespace UI_Kit.Data
{
    public class CreateCss
    {
        public string Execute(Kit kit)
        {
            string cssString = "h1 { \n" +
            "   font-family: Roboto Mono;\n" +
            "   font-size: 48px;\n" +
            "   line-height: " + kit.HeaderLineHeight + ";\n" +
            "}\n" +
            "h2 { \n" +
            "   font-family: Roboto Mono;\n" +
            "   font-size: 48px;\n" +
            "   line-height: " + kit.HeaderLineHeight + ";\n" +
            "}\n" +
            "h3 { \n" +
            "   font-family: Roboto Mono;\n" +
            "   font-size: 48px;\n" +
            "   line-height: " + kit.HeaderLineHeight + ";\n" +
            "}\n" +
            "h4 { \n" +
            "   font-family: Roboto Mono;\n" +
            "   font-size: 48px;\n" +
            "   line-height: " + kit.HeaderLineHeight + ";\n" +
            "}\n" +
            "h5 { \n" +
            "   font-family: Roboto Mono;\n" +
            "   font-size: 48px;\n" +
            "   line-height: " + kit.HeaderLineHeight + ";\n" +
            "}\n" +
            "h6 { \n" +
            "   font-family: Roboto Mono;\n" +
            "   font-size: 48px;\n" +
            "   line-height: " + kit.HeaderLineHeight + ";\n" +
            "}\n" +
            "button { \n" +
            "   color: #FFFFFF;\n" +
            "   background-color: "+ kit.PrimaryColor +";\n" +
            "   font-size: 16px;\n" +
            "   line-height: " + kit.HeaderLineHeight + ";\n" +
            "}\n";

            return cssString;
        }
    }
}
