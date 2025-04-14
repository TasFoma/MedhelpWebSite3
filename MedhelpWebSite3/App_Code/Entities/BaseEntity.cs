using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

/// <summary>
/// Сводное описание для BaseEntity
/// </summary>
[Serializable]
public class BaseEntity<T>
{
    public bool Error { get; set; }
    public string Message { get; set; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public List<T> Response { get; set; }

    public async Task ConvertStringToImage()
    {
        foreach (T item in Response)
        {
            if (item is Doctor)
            {
                (item as Doctor).StringDoctorPhoto = PhotoFromTheServer.PhotoFromServerOrDefault(await ApiRequest.BaseImageRequest((item as Doctor).Image_Url));
            }
            else if (item is ScheduleByService)
            {
                (item as ScheduleByService).StringDoctorPhoto = PhotoFromTheServer.PhotoFromServerOrDefault(await ApiRequest.BaseImageRequest((item as ScheduleByService).StringDoctorPhoto));
            }
        }
    }
}