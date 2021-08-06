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
    public class GoogleFonts : PublicFeature<string, FontListResponse>
    {
        string baseurl = "https://www.googleapis.com/webfonts/v1/webfonts?key=";
        public GoogleFonts()
        {
            
        }
        public override async Task<FontListResponse> ExecuteAsync(string colors)
        {
            HttpClient client = new HttpClient();          
            string key = "AIzaSyCVQkjhKXtzJlXxMCEWQN5Yi52gInslEZE";
            FontListResponse fonts = new FontListResponse();

            try
            {
                var apiurl = baseurl + key;
                var url = new Uri(apiurl);
                var response = await client.GetAsync(url);
                var responseBody = await response.Content.ReadAsStringAsync();

                fonts = JsonConvert.DeserializeObject<FontListResponse>(responseBody);

                client.Dispose();
                return fonts;
            }
            catch (Exception ex)
            {
                string checkResult = "Error " + ex.ToString();
                client.Dispose();
                return fonts;
            }

        }

    }
    public class FontListResponse
    {
        public List<Font> Items { get; set; }
    }
    public class Font
    {
        public string Family { get; set; }
    }

}
