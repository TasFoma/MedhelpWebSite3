/// <summary>
/// Сводное описание для Doctor
/// </summary>
[System.Serializable]
public class Doctor
{
    public string StringDoctorPhoto { get; set; }

    public int Id_Doctor { get; set; }
    public string Full_Name { get; set; }
    public string Id_Spec { get; set; }
    public string Stag { get; set; }
    public string Specialty { get; set; }
    public string Dop_Info { get; set; }
    public string Image_Url { get; set; }
}