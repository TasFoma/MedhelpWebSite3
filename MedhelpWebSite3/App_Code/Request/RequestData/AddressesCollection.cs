/// <summary>
/// Сводное описание для AddressesCollection
/// </summary>
public class AddressesCollection
{
    public AddressesCollection() { }

    private static readonly string insertAppointmentAddress = "ZapisInsertSite";

    public static string InsertAppointmentInfoAddress { get; } = $"http://oneclick.tmweb.ru/v1/" + $"{insertAppointmentAddress}";

    private static readonly string insertLogRequestAddress = "LogDataInsertSite";
    private static readonly string phoneConfirmationRequestAddress = "GeneratePWDVerif";
    private static readonly string codeConfirmationSendingAddress = "CheckPWDVerif";
    private static readonly string centerInfoByBusinessIdAddress = "centres_by_business_id";
    private static readonly string getCurrentDateAddress = "date";

    public static string InsertLogRequestAddress { get; } = $"http://oneclick.tmweb.ru/v1/" + $"{insertLogRequestAddress}";
    public static string PhoneConfirmationRequestAddress { get; } = $"http://oneclick.tmweb.ru/v1/" + $"{phoneConfirmationRequestAddress}";
    public static string CodeConfirmationSendingAddress { get; } = $"http://oneclick.tmweb.ru/v1/" + $"{codeConfirmationSendingAddress}";
    public static string CenterInfoByBusinessIdUrlAddress { get; } = $"http://oneclick.tmweb.ru/v1/" + $"{centerInfoByBusinessIdAddress}";    

    private static readonly string servicesAddress = "services";
    private static readonly string specialtyAddress = "specialty";
    private static readonly string analisysPriceAddress = "getAnalizPrice";
    private static readonly string allDoctorsAddress = "doctors";
    private static readonly string sheduleBySerivceAddress = "schedule/service";
    private static readonly string recordAddress = "recordOnline";
    private static readonly string uniqCena = "FindUniqCena";
    private static readonly string servicesByDoctorsAddress = "services/doctor";
    private static readonly string branchByServiceIDAddress = "FilialByIdYsl";
    private static readonly string checkSpamAppointmentAddress = "CheckSpamZapisOnline";
    private static readonly string getDoctorInformation = "sotr_info";
    private static readonly string insertInWaitingList = "wait_list";

    public static string ServicesUrlAddress { get; } = servicesAddress;
    public static string SpecialtyUrlAddress { get; } = specialtyAddress;
    public static string AnalisysPriceUrlAddress { get; } = analisysPriceAddress;
    public static string AllDoctorsUrlAddress { get; } = allDoctorsAddress;
    public static string SheduleByServiceUrlAddress { get; } = sheduleBySerivceAddress;
    public static string RecordUrlAddress { get; } = recordAddress;
    public static string UniqCenaUrl { get; } = uniqCena;
    public static string ServicesByDoctorsUrlAddress { get; } = servicesByDoctorsAddress;
    public static string BranchByServiceIDAddress { get; } = branchByServiceIDAddress;
    public static string CheckSpamAppointmentAddress { get; } = checkSpamAppointmentAddress;
    public static string GetCurrentDateAddress { get; } = getCurrentDateAddress;
    public static string GetDoctorInformationAddress { get; } = getDoctorInformation;
    public static string InsertInWaitingList { get; } = insertInWaitingList;

    public static string BuildUrlString(string urlStart, string resourceUrl)
    {
        return urlStart != null ? string.Concat(urlStart, resourceUrl) : null;
    }
}