using System.Collections.Generic;

/// <summary>
/// Сводное описание для RequestParameters
/// </summary>
public class RequestParameters
{
    public RequestParameters() { }

    //База
    public static string ClientId { get; } = "id_kl";
    public static string CenterId { get; } = "id_centr";
    public static string BranchId { get; } = "id_filial";
    public static string DB_name { get; } = "db_name";
    

    public static string StringParametersBuilder(List<string> parameters)
    {
        string result = "";
        foreach (var parameter in parameters)
            result += $"/{parameter}";
        return result;
    }
}

public class Parameters
{
    private string phoneNumber;

    public string CenterID { get; set; }
    public string BusinessID { get; set; }
    public string ServiceID { get; set; }
    public string DB_name { get; set; }    
    public string Date { get; set; }
    public string Admission { get; set; }
    public string DoctorID { get; set; }
    public string Time { get; set; }
    public string SpecialtyID { get; set; }
    public string ConfirmationCode { get; set; }
    public string ClientID { get; set; }
    public string ClientIPAddress { get; set; }
    public string BranchID { get; set; }
    public string Type { get; set; }
    public string ServiceName { get; set; }

    public string PhoneNumber
    {
        get
        {
            if (phoneNumber == null)
                return "0000000000";
            return phoneNumber;
        }
        set
        {            
            phoneNumber = value ?? "0000000000";
        }
    }
}

public class Headers
{
    public string ClientID { get; set; } = UsersAccessData.ClientID;
    public string BranchID { get; set; }
    public string DB_name { get; set; }    
}

public interface IBaseRequestData : IBaseRequestHeadersData, IBaseRequestParamsData
{
}

public interface IBaseRequestHeadersData
{
    Headers Headers { get; set; }

    Dictionary<string, string> GetHeaders();
}

public interface IBaseRequestParamsData
{
    Parameters Parameters { get; set; }

    List<string> GetParameters();
}

public class BaseRequestHeaders
{
    public Headers Headers { get; set; }

    public Dictionary<string, string> GetHeaders()
    {
        return new Dictionary<string, string>()
        {
            [RequestParameters.ClientId] = Headers.ClientID,
            [RequestParameters.BranchId] = Headers.BranchID,
            [RequestParameters.DB_name] = Headers.DB_name
        };
    }

    public BaseRequestHeaders() { }

    public BaseRequestHeaders(string clientID, string branchID, string db_name)
    {
        Headers = new Headers()
        {
            ClientID = clientID,
            BranchID = branchID,
            DB_name = db_name
        };
    }
}

public class GetCurrentServerDateParameters : BaseRequestHeaders
{
    public GetCurrentServerDateParameters(string clientID, string branchID, string db_name)
        : base(clientID, branchID, db_name) { }
}

public class ServicesParameters : BaseRequestHeaders, IBaseRequestParamsData
{
    public Parameters Parameters { get; set; }

    public List<string> GetParameters()
    {
        return new List<string>()
        {
            Parameters.ClientID,
            Parameters.CenterID
        };
    }

    public ServicesParameters(string clientID, string branchID, string db_name) 
        :base(clientID, branchID, db_name)
    {
        Parameters = new Parameters()
        {
            ClientID = clientID,
            CenterID = branchID,
            DB_name = db_name
        };
    }
}

public class DoctorsParameters : BaseRequestHeaders
{
    public DoctorsParameters(string clientID, string branchID, string db_name)
        : base(clientID, branchID, db_name) { }
}

public class AnalisysParameters : BaseRequestHeaders
{
    public AnalisysParameters(string clientID, string branchID, string db_name)
        : base(clientID, branchID, db_name) { }
}

public class SpecialtiesParameters : BaseRequestHeaders
{
    public SpecialtiesParameters(string clientID, string branchID, string db_name)
        : base(clientID, branchID, db_name) { }
}

public class BranchesByServiceIDParameters : BaseRequestHeaders, IBaseRequestParamsData
{
    public Parameters Parameters { get; set; }    

    public List<string> GetParameters()
    {
        return new List<string>()
        {
            Parameters.ServiceID
        };
    }

    public BranchesByServiceIDParameters() { }
    public BranchesByServiceIDParameters(string clientID, string branchID, string db_name, string serviceID)
       : base(clientID, branchID, db_name) 
    {
        Parameters = new Parameters()
        {
            ServiceID = serviceID
        };
    }
}

public class PhoneConfirmationParameters : IBaseRequestParamsData
{
    public Parameters Parameters { get; set; }

    public List<string> GetParameters()
    {
        return new List<string>()
        {
            Parameters.CenterID,
            Parameters.PhoneNumber,
            Parameters.ClientIPAddress
        };
    }

    public PhoneConfirmationParameters() { }
    public PhoneConfirmationParameters(string phoneNumber, string centerID, string clientIPAddress)
    {
        Parameters = new Parameters()
        {
            PhoneNumber = phoneNumber,
            CenterID = centerID,
            ClientIPAddress = clientIPAddress
        };
    }
}

public class ConfirmationCodeParameters : IBaseRequestParamsData
{
    public Parameters Parameters { get; set; }

    public List<string> GetParameters()
    {
        return new List<string>()
        {
            Parameters.CenterID,
            Parameters.PhoneNumber,
            Parameters.ConfirmationCode
        };
    }

    public ConfirmationCodeParameters() { }
    public ConfirmationCodeParameters(string code, string phoneNumber, string centerID)
    {
        Parameters = new Parameters()
        {
            CenterID = centerID,
            PhoneNumber = phoneNumber,
            ConfirmationCode = code
        };
    }
}

public class CheckSpamAppParameters : BaseRequestHeaders, IBaseRequestParamsData
{
    public Parameters Parameters { get; set; }

    public List<string> GetParameters()
    {
        return new List<string>()
        {
            Parameters.PhoneNumber,
            Parameters.ServiceID
        };
    }

    public CheckSpamAppParameters() { }
    public CheckSpamAppParameters(string phoneNumber, string serviceID, string clientID, string branchID, string db_name)
        : base(clientID, branchID, db_name)
    {
        Parameters = new Parameters()
        {
            PhoneNumber = phoneNumber,
            ServiceID = serviceID
        };
    }
}
public class UniqPriceParameters : BaseRequestHeaders, IBaseRequestParamsData
{
    public Parameters Parameters { get; set; }

    public List<string> GetParameters()
    {
        return new List<string>()
        {
            Parameters.DoctorID,
            Parameters.ServiceName
        };
    }

    public UniqPriceParameters() { }
    public UniqPriceParameters(string doctorid, string serviceNAME, string clientID, string branchID, string db_name)
        : base(clientID, branchID, db_name)
    {
        Parameters = new Parameters()
        {
            DoctorID = doctorid,
            ServiceName = serviceNAME
        };
    }
}

public class CenterInfoParameters : IBaseRequestParamsData
{
    public Parameters Parameters { get; set; }

    public List<string> GetParameters()
    {
        return new List<string>()
        {
            Parameters.BusinessID
        };
    }

    public CenterInfoParameters() { }
    public CenterInfoParameters(string businessID)
    {
        Parameters = new Parameters()
        {
            BusinessID = businessID
        };
    }
}

public class InsertAppointmentInfo : IBaseRequestParamsData
{
    public Parameters Parameters { get; set; }

    public List<string> GetParameters()
    {
        return new List<string>()
        {
            Parameters.CenterID,
            Parameters.BranchID,
            Parameters.ServiceName,
            Parameters.Date,
            Parameters.Time,
            Parameters.PhoneNumber
        };
    }

    public InsertAppointmentInfo() { }
    public InsertAppointmentInfo(string centerID, string branchID, string serviceName, string date, string time, string phoneNumber)
    {
        Parameters = new Parameters()
        {
            CenterID = centerID,
            BranchID = branchID,
            ServiceName = serviceName,
            Date = date,
            Time = time,
            PhoneNumber = phoneNumber
        };
    }
}

//Вставка в лог журанала действий
public class InsertLogParameters : IBaseRequestParamsData
{
    public Parameters Parameters { get; set; }

    public List<string> GetParameters()
    {
        return new List<string>()
        {
            Parameters.CenterID,
            Parameters.BranchID,
            Parameters.PhoneNumber,
            Parameters.ClientIPAddress,
            Parameters.Type
        };
    }

    public InsertLogParameters() { }
    public InsertLogParameters(string centerID, string branchID, string phoneNumber, string clientIPAddress, string type)
    {
        Parameters = new Parameters()
        {
            CenterID = centerID,
            BranchID = branchID,
            PhoneNumber = phoneNumber,
            ClientIPAddress = clientIPAddress,
            Type = type
        };
    }
}

public class DoctorInformationParameters : BaseRequestHeaders, IBaseRequestParamsData
{
    public Parameters Parameters { get; set; }

    public List<string> GetParameters()
    {
        return new List<string>()
        {
            Parameters.DoctorID
        };
    }

    public DoctorInformationParameters() { }
    public DoctorInformationParameters(string doctorID, string clientID, string branchID, string db_name)
        : base(clientID, branchID, db_name)
    {
        Parameters = new Parameters()
        {
            DoctorID = doctorID
        };
    }
}

public class ScheduleByServiceParameters : BaseRequestHeaders, IBaseRequestParamsData
{
    public Parameters Parameters { get; set; }    

    public List<string> GetParameters()
    {
        return new List<string>()
        {
            Parameters.ServiceID,
            Parameters.Date,
            Parameters.Admission,
            Parameters.CenterID
        };
    }

    public ScheduleByServiceParameters() { }
    public ScheduleByServiceParameters(string serviceID, string date, string admission, string clientID, string branchID, string db_name)
        : base(clientID, branchID, db_name)
    {
        Parameters = new Parameters()
        {
            ServiceID = serviceID,
            Date = date,
            Admission = admission,
            CenterID = branchID
        };
    }
}

public class AppointmentConfirmationParameters : BaseRequestHeaders, IBaseRequestParamsData
{
    public Parameters Parameters { get; set; }

    public List<string> GetParameters()
    {
        return new List<string>()
        {
            Parameters.DoctorID,
            Parameters.Date,
            Parameters.Time,
            Parameters.PhoneNumber,
            Parameters.SpecialtyID,
            Parameters.ServiceID,
            Parameters.Admission,
            Parameters.CenterID
        };
    }

    public AppointmentConfirmationParameters(string employeeID, string date, string appointmentTime,
        string phoneNumber, string specialtyID, string serviceID, string admission, string clientID, string branchID, string db_name)
        : base(clientID, branchID, db_name)
    {
        Parameters = new Parameters()
        {
            DoctorID = employeeID,
            Date = date,
            Time = appointmentTime,
            SpecialtyID = specialtyID,
            ServiceID = serviceID,
            Admission = admission,
            PhoneNumber = phoneNumber,
            CenterID = branchID
        };
    }
}

public class WaitingListAdditionalParameters : BaseRequestHeaders, IBaseRequestParamsData
{
    public Parameters Parameters { get; set; }

    public List<string> GetParameters()
    {
        return new List<string>()
        {
            Parameters.PhoneNumber,
            Parameters.ServiceID,
            Parameters.BranchID
        };
    }

    public WaitingListAdditionalParameters() { }
    public WaitingListAdditionalParameters(string phoneNumber, string serviceID, string clientID, string branchID, string db_name)
        : base(clientID, branchID, db_name)
    {
        Parameters = new Parameters()
        {
            PhoneNumber = phoneNumber,
            ServiceID = serviceID,
            BranchID = branchID
        };
    }
}
