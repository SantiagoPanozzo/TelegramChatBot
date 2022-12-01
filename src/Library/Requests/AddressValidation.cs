namespace Library;

using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web;

public static class AddressValidation
{
    //private static string _token = DotNetEnv.Env.GetString("GEOAPIFY_KEY");
    private static string _token = "2d8e0a8afae747eeb760c88caa4a19e1";
    public static string Request(string address)
    {
        var encodedLoc = HttpUtility.UrlEncode(address);

        var client = new HttpClient();
        var request = new HttpRequestMessage{
            Method = HttpMethod.Get,
            RequestUri = new Uri($"https://api.geoapify.com/v1/geocode/search?text={encodedLoc}&format=json&apiKey={_token}")
        };
        var response = client.Send(request).EnsureSuccessStatusCode();
        var content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        return content;
    }

    public static bool Process(string address)
    {
        var body = Request(address);
        var deserialized = JObject.Parse(body);
        //Console.WriteLine(deserialized);
        var cofidenceLevel = double.Parse(deserialized["results"][0]["rank"]["confidence"].ToString());
        //Console.WriteLine(cofidenceLevel);
        return (cofidenceLevel >= 0.95) ? true : false;
    }
}