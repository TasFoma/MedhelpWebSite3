using Newtonsoft.Json;

/// <summary>
/// Сводное описание для CurrentDate
/// </summary>
public class CurrentDate
{
    public string Today { get; set; }

    [JsonProperty(PropertyName = "week_day")]
    public int WeekDay { get; set; }

    [JsonProperty(PropertyName = "last_monday")]
    public string LastMonday { get; set; }

    public CurrentDate()
    {
        //
        // TODO: добавьте логику конструктора
        //
    }
}