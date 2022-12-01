namespace Library;

using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web;

public static class Geocode
{
    //private static string _token = DotNetEnv.Env.GetString("DISTANCE_KEY");
    
    private static string _token = "AIzaSyAsJ_WMO3_s45aNYNJ6jmj01UmVMa7mBVg";

    public static string Request(string address)
    {
        var encodedLoc = HttpUtility.UrlEncode(address);

        var client = new HttpClient();
        var request = new HttpRequestMessage{
            Method = HttpMethod.Get,
            RequestUri = new Uri($"https://maps.googleapis.com/maps/api/geocode/json?address={encodedLoc}&key={_token}")
        };
        var response = client.Send(request);
        response.EnsureSuccessStatusCode();
        var content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        return content;
    }

    public static Tuple<double, double> Process(string address)
    {
        Tuple<double, double> locTuple;
        var body = Request(address);
        var parsedJson = JObject.Parse(body);
        double lng = double.Parse(parsedJson["results"][0]["geometry"]["location"]["lng"].ToString());
        double lat = double.Parse(parsedJson["results"][0]["geometry"]["location"]["lat"].ToString());
        locTuple = new(lng, lat);
        return locTuple;
    }
}