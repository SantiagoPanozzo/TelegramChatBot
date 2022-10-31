namespace Library.API;
using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
public static class Distance
{
    public static async Task<int> Calculate(string from, string to)
    {   
        string origin = UrlConvert(from);
        string destination = UrlConvert(to);
        var client = new HttpClient();
        var request = new HttpRequestMessage
        {
        Method = HttpMethod.Get,
        //RequestUri = new Uri("https://maps.googleapis.com/maps/api/distancematrix/json?origins=Salto%20Uruguay&destinations=Montevideo%20Uruguay&language=en-EN&key=AIzaSyC7qyLvE9YbQ1wUdEaS5VgztWC9Qcvsofw"),
        RequestUri = new Uri($"https://maps.googleapis.com/maps/api/distancematrix/json?origins={origin}&destinations={destination}&language=en-EN&key=AIzaSyC7qyLvE9YbQ1wUdEaS5VgztWC9Qcvsofw"),
        };
        var response = await client.SendAsync(request);
        response.EnsureSuccessStatusCode();
        var body = await response.Content.ReadAsStringAsync();
        var parsedJson = JObject.Parse(body);
        int distanceValue = Convert.ToUInt16(parsedJson["rows"][0]["elements"][0]["distance"]["text"].ToString().Split()[0]);
        return distanceValue;
    }

    public static string UrlConvert(string address){
        string result = address.Trim();
        result = result.Replace(" ", "%20");
        result = result.Replace(",", "%2C");
        return result;
    }
}