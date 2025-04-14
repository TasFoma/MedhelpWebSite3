/// <summary>
/// Сводное описание для CenterInfo
/// </summary>
public class CenterInfo
{
    public int? Id { get; set; }
    public int? Id_Center { get; set; }
    public string Id_Filial { get; set; }
    public string City { get; set; }
    public int? Time_Zone { get; set; }
    public string Ip_Address { get; set; }
    public string Ip_Address_Local { get; set; }
    public string Title { get; set; }
    public string Info { get; set; }
    public string Logo { get; set; }
    public string Site { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public int? Time_Otkaz { get; set; }
    public int? Time_Podtvergd { get; set; }
    public string Komment_Zapis { get; set; }
    public int? Max_Zapis { get; set; }
    public string DB_name { get; set; }

    public CenterInfo()
    {
        //
        // TODO: добавьте логику конструктора
        //
    }
}