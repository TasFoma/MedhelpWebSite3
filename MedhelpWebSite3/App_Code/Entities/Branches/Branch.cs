using Newtonsoft.Json;

/// <summary>
/// Сводное описание для Branch
/// </summary>
[System.Serializable]
public class Branch
{
    [JsonProperty(PropertyName = "id_filial")]
    public int? BranchID { get; set; }

    [JsonProperty(PropertyName = "naim_filial")]
    public string BranchName { get; set; }

    [JsonProperty(PropertyName = "address")]
    public string BranchAddress { get; set; }

    public Branch()
    {
        //
        // TODO: добавьте логику конструктора
        //
    }
}