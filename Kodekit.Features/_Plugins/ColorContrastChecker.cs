using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;
using Newtonsoft.Json;
using Sparc.Features;
using System.Reflection;

namespace Kodekit.Features
{
    public class ColorContrastChecker : PublicFeature<Kit, List<ContrastColorResponse>>
    {
        string baseurl = "https://webaim.org/resources/contrastchecker/?";
        string clrs = "";
        public ColorContrastChecker() 
        {
            //clrs = colors;
        }
        public override async Task<List<ContrastColorResponse>> ExecuteAsync(Kit kit)
        {
            HttpClient client = new HttpClient();
            //fcolor=0000FF&bcolor=FFFFFF&api


            //var apiurl = baseurl + colors + "&api";

            //var apiurl = baseurl + "fcolor=" + kit.BackgroundColor.Substring(1) + "&bcolor=" + kit.DarkColor.Substring(1) + "&api";

            //backgroud + primary, secondary, tertiary, darkest, lightest?
            //primary + darkest, lightest?

            ContrastColorResponse colorContrast = new ContrastColorResponse();
            List<ContrastColorResponse> resultsList = new List<ContrastColorResponse>();

            try
            {
                
                //Record record = new Record();
                string colorCompare = "";
                string testing = "";
                ContrastColorResponse contrastResults = new ContrastColorResponse();
                PropertyInfo[] properties = typeof(Kit).GetProperties();
                foreach (PropertyInfo property in properties.Skip(6).Take(5))
                {

                    colorCompare = property.GetValue(kit).ToString();
                    testing = property.Name;


                    var apiurl = baseurl + "fcolor=" + kit.PrimaryColor.Substring(1) + "&bcolor=" + colorCompare.Substring(1) + "&api";
                    var url = new Uri(apiurl);
                    var response = await client.GetAsync(url);
                    var responseBody = await response.Content.ReadAsStringAsync();

                    contrastResults = JsonConvert.DeserializeObject<ContrastColorResponse>(responseBody);
                    //contrastResults.Failed = new List<string>();
                    contrastResults.ColorA = "PrimaryColor";
                    contrastResults.ColorB = property.Name;

                    if (contrastResults.AA == "fail")
                    {
                        resultsList.Add(contrastResults);
                    }

                    
                }
                client.Dispose();
                return resultsList;
            }
            catch (Exception ex)
            {
                string checkResult = "Error " + ex.ToString();
                client.Dispose();
                return resultsList;
                //return checkResult;
            }

            

            //var answer = JsonConvert.DeserializeObject<string>(response);

            //return answer;
        }
        //public async Task<string> CheckColors(string fg, string bg)
        //{

        //    HttpClient client = new HttpClient();
        //    //fcolor=0000FF&bcolor=FFFFFF&api

        //    var apiurl = baseurl + "fcolor=" + fg + "&bcolor=" + bg + "&api";
        //    var response = await client.GetStringAsync($"apiurl");

        //    var answer = JsonConvert.DeserializeObject<string>(response);

        //    return answer;

        //}
    }

    public class ContrastColorResponse
    {
        public string Ratio { get; set; }
        public string AA { get; set; }
        public string AALarge { get; set; }
        public string AAALarge { get; set; }
        public string AAA { get; set; }
        public List<string> Failed {get; set;}
        public string ColorA { get; set; }
        public string ColorB { get; set; }
    }

}
