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

namespace Kodekit.Features
{
    public class ColorContrastChecker : PublicFeature<Kit, ContrastColorResponse>
    {
        string baseurl = "https://webaim.org/resources/contrastchecker/?";
        string clrs = "";
        public ColorContrastChecker() 
        {
            //clrs = colors;
        }
        public override async Task<ContrastColorResponse> ExecuteAsync(Kit kit)
        {
            HttpClient client = new HttpClient();
            //fcolor=0000FF&bcolor=FFFFFF&api

            var apiurl = baseurl + "fcolor=" + kit.PrimaryColor.Substring(1) + "&bcolor=" + kit.SecondaryColor.Substring(1) + "&api";
            var url = new Uri(apiurl);
            //var apiurl = baseurl + colors + "&api";

            ContrastColorResponse colorContrast = new ContrastColorResponse();

            try
            {
                var response = await client.GetAsync(url);
                //string checkResult = response.ToString();
                var responseBody = await response.Content.ReadAsStringAsync();
                //var answer = JsonConvert.DeserializeObject<string>(checkResult);

                var myParsedObject = JsonConvert.DeserializeObject<ContrastColorResponse>(responseBody); 
                
                //(ContrastColorResponse)(new JsonConvert).Deserialize(contentString, typeof(MyObject));
                client.Dispose();
                return myParsedObject;
            }
            catch (Exception ex)
            {
                string checkResult = "Error " + ex.ToString();
                client.Dispose();
                return colorContrast;
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
    }

}
