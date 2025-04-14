using Newtonsoft.Json;

/// <summary>
/// Сводное описание для AppointmentsLimitation
/// </summary>
public class AppointmentsRestriction
{
    [JsonProperty(PropertyName = "count_all")]
    public int CommonAmount { get; set; }

    [JsonProperty(PropertyName = "count_ysl")]
    public int ServiceAmount { get; set; }

    public AppointmentsRestriction()
    {
        //
        // TODO: добавьте логику конструктора
        //
    }
}