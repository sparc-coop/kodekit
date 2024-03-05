using System.Net.Http;
using Newtonsoft.Json;

namespace Kodekit;

public class GetGoogleFonts : PublicFeature<string, FontListResponse>
{
    string baseurl = "https://www.googleapis.com/webfonts/v1/webfonts?key=";
    public GetGoogleFonts()
    {

    }
    public override async Task<FontListResponse> ExecuteAsync(string colors)
    {
        HttpClient client = new();
        string key = "AIzaSyCVQkjhKXtzJlXxMCEWQN5Yi52gInslEZE";
        FontListResponse fonts = new();

        try
        {
            var apiurl = baseurl + key;
            var url = new Uri(apiurl);
            var response = await client.GetAsync(url);
            var responseBody = await response.Content.ReadAsStringAsync();

            fonts = JsonConvert.DeserializeObject<FontListResponse>(responseBody);
            fonts.Items = fonts.Items?.Where(x => x.Category == "serif" || x.Category == "sans-serif").ToList();
            //fonts.Items.RemoveAll(x => x.Category == "handwriting");
            // .Where(x => x.Category != "handwriting").ToList();
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
    public List<GoogleFont>? Items { get; set; }
}

public class GoogleFont
{
    public string? Family { get; set; }
    public string? Category { get; set; }
    public FontFile? Files { get; set; }

}
public class FontFile
{
    public string? Regular { get; set; }
}
