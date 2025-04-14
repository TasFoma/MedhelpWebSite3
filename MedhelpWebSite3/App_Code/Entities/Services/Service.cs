using System;

/// <summary>
/// Сводное описание для Service
/// </summary>
[Serializable]
public class Service
{
    public Service() { }

    public int Id_Service { get; set; }
    public int Id_Spec { get; set; }
    public int Admission { get; set; }
    //public int Value { get; set; }
    public string Value { get; set; }
    public string Title { get; set; }
    public string Komment { get; set; }
    //public string DB_name { get; set; }
    public int Zapis { get; set; }
    public int Izbrannoe { get; set; }
    public int Max_Zapis { get; set; }
    public int Poryadok { get; set; }
}