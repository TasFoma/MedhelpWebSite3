using Newtonsoft.Json.Linq;
using System.Linq;

/// <summary>
/// Сводное описание для JsonSearchEngine
/// </summary>
public class JsonSearchEngine
{
    public static string FindIP(string json)
    {
        var jObj = JObject.Parse(json);
        string ip = (string)jObj.Descendants()
            .OfType<JProperty>()
            .Where(p => p.Name == "ip")
            .First()
            .Value;
        return ip;
    }

    public JsonSearchEngine()
    {
        //
        // TODO: добавьте логику конструктора
        //
    }
}