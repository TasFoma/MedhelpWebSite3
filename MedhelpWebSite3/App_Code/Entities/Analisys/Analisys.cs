using Newtonsoft.Json;
using System;

/// <summary>
/// Сводное описание для Analisys
/// </summary>
[Serializable]
public class Analisys
{
    [JsonProperty(PropertyName = "Gryppa")]
    public string Group { get; set; }

    [JsonProperty(PropertyName = "Analiz")]
    public string Analiz { get; set; }

    [JsonProperty(PropertyName = "Cena")]
    public int Price { get; set; }

    [JsonProperty(PropertyName = "Srok")]
    public int Time { get; set; }
}