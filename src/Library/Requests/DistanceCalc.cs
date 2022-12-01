namespace Library.DistanceMatrix;

using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web;

public class Distance
{
    private static Distance? _instance = null;
    private static string _token = "AIzaSyAsJ_WMO3_s45aNYNJ6jmj01UmVMa7mBVg";

    public async Task<int> Calculate(string from, string to)
    {   
        string origin = UrlFormat(from);
        string destination = UrlFormat(to);
        var client = new HttpClient();
        var request = new HttpRequestMessage
        {
        Method = HttpMethod.Get,
        RequestUri = new Uri($"https://maps.googleapis.com/maps/api/distancematrix/json?origins={origin}&destinations={destination}&language=en-EN&key={_token}"),   //Key deshabilitada
        };
        var response = client.SendAsync(request).GetAwaiter().GetResult();          //Funcionaría? Probar
        try{
            response.EnsureSuccessStatusCode();
        } catch(Exception e){
            Debug.WriteLine("El código HTTP de estado no es 200, revisar key, la validez del origen, destino y URL");
            throw new Exception("Ocurrió un error al recibir la respuesta del servidor, revisar que los parámetros sean válidos.");
        }
        var body = await response.Content.ReadAsStringAsync();
        var parsedJson = JObject.Parse(body);
        int distanceValue = Convert.ToUInt16(parsedJson["rows"][0]["elements"][0]["distance"]["text"].ToString().Split()[0]);
        return distanceValue;
    }


    //Se espera una dirección del formato "{Ciudad} {País}"
    //Falta retocar este método
    private string UrlFormat(string address){
        if(address.Trim().Count(x => x == ' ') == 1)
        {
            string[] trimmedArr = address.Trim().Split(" ");
            string origin = trimmedArr[0];
            string destination = trimmedArr[1];
            string result = HttpUtility.UrlEncode($"{origin} {destination}");
            return result;
        }
        throw new ArgumentException("Se esperaba una dirección del siguiente formato: <Ciudad> <País>");
    }

    //Por Singleton
    public static Distance GetInstance()
    {
        // Honestamente no recuerdo por qué lo hice Singleton XD, no la uso para extender de nada, después veo
        if (_instance == null)
        {
            _token = DotNetEnv.Env.GetString("DISTANCE_KEY");
            _instance = new Distance();
        }
        return _instance;
    }

    private Distance(){}
}