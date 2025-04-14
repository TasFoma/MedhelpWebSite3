using Newtonsoft.Json;
using System;
using System.Collections.Generic;

/// <summary>
/// Сводное описание для ScheduleByService
/// </summary>
[Serializable]
public class ScheduleByService
{
    public int Id_Doctor { get; set; }
    public string Full_Name { get; set; }
    public bool Is_Work { get; set; }
    public string Adm_Day { get; set; }
    public List<string> Adm_Time { get; set; }
    public int Id_Spec { get; set; }

    [JsonProperty(PropertyName = "Foto")]
    public string StringDoctorPhoto { get; set; }
}