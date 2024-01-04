using System.Net.Http;
using Newtonsoft.Json;

namespace Kodekit.Features;

public class ColorContrastChecker : PublicFeature<string, ContrastColorResponse>
{
    string baseurl = "https://webaim.org/resources/contrastchecker/?";

    public ColorContrastChecker()
    {
        //clrs = colors;
    }
    public override async Task<ContrastColorResponse> ExecuteAsync(string colors)
    {
        HttpClient client = new HttpClient();
        //fcolor=0000FF&bcolor=FFFFFF&api


        //var apiurl = baseurl + colors + "&api";

        //var apiurl = baseurl + "fcolor=" + kit.BackgroundColor.Substring(1) + "&bcolor=" + kit.DarkColor.Substring(1) + "&api";

        //backgroud + primary, secondary, tertiary, darkest, lightest?
        //primary + darkest, lightest?

        ContrastColorResponse colorContrast = new ContrastColorResponse();
        //List<ContrastColorResponse> resultsList = new List<ContrastColorResponse>();

        try
        {

            //Record record = new Record();
            //string colorCompare = "";
            //string testing = "";
            //ContrastColorResponse contrastResults = new ContrastColorResponse();

            string c1 = colors.Split("+").ElementAt(0);
            string c2 = colors.Split("+").ElementAt(1);

            var apiurl = baseurl + "fcolor=" + c1 + "&bcolor=" + c2 + "&api";
            var url = new Uri(apiurl);
            var response = await client.GetAsync(url);
            var responseBody = await response.Content.ReadAsStringAsync();

            colorContrast = JsonConvert.DeserializeObject<ContrastColorResponse>(responseBody);

            //PropertyInfo[] properties = typeof(Kit).GetProperties();
            //foreach (PropertyInfo property in properties.Skip(6).Take(5))
            //{

            //    colorCompare = property.GetValue(kit).ToString();
            //    testing = property.Name;


            //    var apiurl = baseurl + "fcolor=" + kit.PrimaryColor.Substring(1) + "&bcolor=" + colorCompare.Substring(1) + "&api";
            //    var url = new Uri(apiurl);
            //    var response = await client.GetAsync(url);
            //    var responseBody = await response.Content.ReadAsStringAsync();

            //    contrastResults = JsonConvert.DeserializeObject<ContrastColorResponse>(responseBody);
            //    //contrastResults.Failed = new List<string>();
            //    contrastResults.ColorA = "PrimaryColor";
            //    contrastResults.ColorB = property.Name;

            //    if (contrastResults.AA == "fail")
            //    {
            //        resultsList.Add(contrastResults);
            //    }


            //}
            client.Dispose();
            return colorContrast;
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

}

public class ContrastColorResponse
{
    public string? Ratio { get; set; }
    public string? AA { get; set; }
    public string? AALarge { get; set; }
    public string? AAALarge { get; set; }
    public string? AAA { get; set; }
}
