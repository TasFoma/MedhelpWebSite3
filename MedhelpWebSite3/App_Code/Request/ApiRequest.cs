using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

/// <summary>
/// Сводное описание для ApiRequest
/// </summary>
public class ApiRequest
{
    private static HttpClient Client = new HttpClient();

    public static void SetTimeout()
    {
        try
        {
            Client.Timeout = TimeSpan.FromSeconds(10);
        }
        catch { }
    }

    public ApiRequest() { }

    public async static Task<SimpleResponse> AddInWaitingList(string urlStart, WaitingListAdditionalParameters waitingListAdditionalParameters)
    {
        var parameters = waitingListAdditionalParameters.GetParameters();
        var headers = waitingListAdditionalParameters.GetHeaders();
        try
        {
            return await BaseRequestGet<SimpleResponse>(
                AddressesCollection.BuildUrlString(urlStart, AddressesCollection.InsertInWaitingList), headers, parameters);
            // AddressesCollection.BuildUrlString(urlStart, AddressesCollection.InsertInWaitingList), new Dictionary<string, string>(), parameters);
        }
        catch (Exception exc)
        {
            throw new Exception("Отправка кода подтверждения. " + exc.Message);
        }
    }

    public async static Task<BaseEntity<AppointmentsRestriction>> GetAppAmountRestrictions(string urlStart, CheckSpamAppParameters checkSpamAppParameters)
    {
        var parameters = checkSpamAppParameters.GetParameters();
        var headers = checkSpamAppParameters.GetHeaders();
        try
        {
            return await BaseRequestGet<BaseEntity<AppointmentsRestriction>>(
                AddressesCollection.BuildUrlString(urlStart, AddressesCollection.CheckSpamAppointmentAddress), headers, parameters);
        }
        catch (Exception exc)
        {
            throw new Exception("Проверка записей на спам. " + exc.Message);
        }
    }

    public async static Task<BaseEntity<Branch>> GetBranchesByServiceID(string urlStart, BranchesByServiceIDParameters branchesByServiceIDParameters)
    {
        var parameters = branchesByServiceIDParameters.GetParameters();
        var headers = branchesByServiceIDParameters.GetHeaders();
        try
        {
            return await BaseRequestGet<BaseEntity<Branch>>(AddressesCollection.BuildUrlString(urlStart, AddressesCollection.BranchByServiceIDAddress), headers, parameters);
        }
        catch (Exception exc)
        {
            throw new Exception("Получение списка филиалов. " + exc.Message);
        }
    }

    public async static Task<SimpleResponse> SendConfirmationCode(ConfirmationCodeParameters confirmationCodeParameters)
    {
        var parameters = confirmationCodeParameters.GetParameters();
        try
        {
            return await BaseRequestGet<SimpleResponse>(AddressesCollection.CodeConfirmationSendingAddress, new Dictionary<string, string>(), parameters);
        }
        catch (Exception exc)
        {
            throw new Exception("Отправка кода подтверждения. " + exc.Message);
        }
    }

    public async static Task<SimpleResponse> ConfirmPhoneNumber(PhoneConfirmationParameters phoneConfirmationParameters)
    {
        var parameters = phoneConfirmationParameters.GetParameters();
        try
        {
            return await BaseRequestGet<SimpleResponse>(AddressesCollection.PhoneConfirmationRequestAddress, new Dictionary<string, string>(), parameters);
        }
        catch (Exception exc)
        {
            throw new Exception("Запрос кода подтверждения. " + exc.Message);
        }
    }

    public async static Task<BaseEntity<CenterInfo>> GetCenterInfoByBusinessID(CenterInfoParameters centerInfoParameters)
    {
        var parameters = centerInfoParameters.GetParameters();
        try
        {
            return await BaseRequestGet<BaseEntity<CenterInfo>>(AddressesCollection.CenterInfoByBusinessIdUrlAddress, new Dictionary<string, string>(), parameters);
        }
        catch (Exception exc)
        {
            throw new Exception("Получение информации о центре по BusinessID. " + exc.Message);
        }
    }

    public async static Task<BaseCurrentDate> GetCurrentServerDate(string urlStart, GetCurrentServerDateParameters getCurrentServerDateParameters)
    {
        var headers = getCurrentServerDateParameters.GetHeaders();
        try
        {
            return await BaseRequestGet<BaseCurrentDate>(AddressesCollection.BuildUrlString(urlStart, AddressesCollection.GetCurrentDateAddress), headers);
        }
        catch (Exception exc)
        {
            throw new Exception("Получение текущей даты сервера. " + exc.Message);
        }
    }

    public async static Task<SimpleResponse> InsertAppointmentData(InsertAppointmentInfo insertAppointmentInfo)
    {
        var parameters = insertAppointmentInfo.GetParameters();
        try
        {
            return await BaseRequestPost<SimpleResponse>(AddressesCollection.InsertAppointmentInfoAddress, parameters);
        }
        catch (Exception exc)
        {
            throw new Exception("Вставка данных о записи на приём. " + exc.Message);
        }
    }

    public async static Task<SimpleResponse> InsertLogData(InsertLogParameters insertLogParameters, string message)
    {
        var parameters = insertLogParameters.GetParameters();
        try
        {
            return await BaseRequestPost<SimpleResponse>(AddressesCollection.InsertLogRequestAddress, parameters, message);
        }
        catch (Exception exc)
        {
            throw new Exception("Вставка данных в журнал. " + exc.Message);
        }
    }

    public async static Task<SimpleResponse> AppointmentConfirmationRequest(string urlStart, AppointmentConfirmationParameters appointmentConfirmationParameters)
    {
        var parameters = appointmentConfirmationParameters.GetParameters();
        var headers = appointmentConfirmationParameters.GetHeaders();
        try
        {
            return await BaseRequestGet<SimpleResponse>(AddressesCollection.BuildUrlString(urlStart, AddressesCollection.RecordUrlAddress), headers, parameters);
        }
        catch (Exception exc)
        {
            throw new Exception("Запрос записи на приём. " + exc.Message);
        }
    }
    public async static Task<BaseEntity<UniqCena>> UniqCena(string urlStart, UniqPriceParameters uniqpriceParameters)
    {
        var parameters = uniqpriceParameters.GetParameters();
        var headers = uniqpriceParameters.GetHeaders();
        try
        {
            return await BaseRequestGet<BaseEntity<UniqCena>>(AddressesCollection.BuildUrlString(urlStart, AddressesCollection.UniqCenaUrl)
                , headers, parameters);
        }
        catch (Exception exc)
        {
            throw new Exception("Запрос записи на приём. " + exc.Message);
        }
    }

    public async static Task<BaseEntity<ScheduleByService>> GetScheduleByService(string urlStart, ScheduleByServiceParameters scheduleByServiceParameters)
    {
        var parameters = scheduleByServiceParameters.GetParameters();
        var headers = scheduleByServiceParameters.GetHeaders();
        try
        {
            var result = await BaseRequestGet<BaseEntity<ScheduleByService>>(AddressesCollection.BuildUrlString(urlStart, AddressesCollection.SheduleByServiceUrlAddress), headers, parameters);
            await result.ConvertStringToImage();
            return result;
        }
        catch (Exception exc)
        {
            throw new Exception("Получение расписания для услуги. " + exc.Message);
        }
    }

    /*public async static Task<BaseEntity<Analisys>> GetAnalisys(string urlStart, AnalisysParameters analisysParameters)
    {
        var headers = analisysParameters.GetHeaders();
        try
        {
            return await BaseRequestGet<BaseEntity<Analisys>>(AddressesCollection.BuildUrlString(urlStart, AddressesCollection.AnalisysPriceUrlAddress), headers);
        }
        catch (Exception exc)
        {
            throw new Exception("Запрос прейскуранта на анализы. " + exc.Message);
        }
    }*/

    public async static Task<BaseEntity<Service>> GetServices(string urlStart, ServicesParameters servicesParameters)
    {
        var headers = servicesParameters.GetHeaders();
        var parameters = servicesParameters.GetParameters();
        try
        {
            return await BaseRequestGet<BaseEntity<Service>>(AddressesCollection.BuildUrlString(urlStart, AddressesCollection.ServicesUrlAddress)
                , headers, parameters);
        }
        catch (Exception exc)
        {
            throw new Exception("Запрос списка услуг. " + exc.Message);
        }
    }

    public async static Task<BaseEntity<Specialty>> GetSpecialties(string urlStart, SpecialtiesParameters specialtiesParameters)
    {
        var headers = specialtiesParameters.GetHeaders();
        try
        {
            return await BaseRequestGet<BaseEntity<Specialty>>(AddressesCollection.BuildUrlString(urlStart, AddressesCollection.SpecialtyUrlAddress), headers);
        }
        catch (Exception exc)
        {
            throw new Exception("Запрос списка специальностей. " + exc.Message);
        }
    }

    public async static Task<BaseEntity<Doctor>> GetDoctorInformation(string urlStart, DoctorInformationParameters doctorsParameters)
    {
        var headers = doctorsParameters.GetHeaders();
        var parameters = doctorsParameters.GetParameters();
        try
        {
            return await BaseRequestGet<BaseEntity<Doctor>>(AddressesCollection.BuildUrlString(urlStart, AddressesCollection.GetDoctorInformationAddress), headers, parameters);
        }
        catch (Exception exc)
        {
            throw new Exception("Запрос списка специалистов. " + exc.Message);
        }
    }

    public async static Task<BaseEntity<Doctor>> GetAllDoctors(string urlStart, DoctorsParameters doctorsParameters)
    {
        var headers = doctorsParameters.GetHeaders();
        try
        {
            return await BaseRequestGet<BaseEntity<Doctor>>(AddressesCollection.BuildUrlString(urlStart, AddressesCollection.AllDoctorsUrlAddress), headers);
        }
        catch (Exception exc)
        {
            throw new Exception("Запрос списка специалистов. " + exc.Message);
        }
    }

    private static Dictionary<string, string> FillHeaders(List<string> parameters, List<string> values)
    {
        var headers = new Dictionary<string, string>();
        for (int i = 0; i < parameters.Count; i++)
            headers.Add(parameters.ElementAt(i), values.ElementAt(i));
        return headers;
    }

    private async static Task<T> BaseRequestGet<T>(string url, Dictionary<string, string> headers, List<string> values = null)
    {
        if (url == null)
            throw new Exception("Отсутствует URL-адрес для запроса");
        string result = "";
        
        Client.DefaultRequestHeaders.Clear();
        Client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", AdmittanceToken.Token);
        foreach (var header in headers)
            Client.DefaultRequestHeaders.TryAddWithoutValidation(header.Key, header.Value);
        using (HttpResponseMessage response = await Client.GetAsync(url + (values != null ? RequestParameters.StringParametersBuilder(values) : "")))
        using (HttpContent content = response.Content)
        {
            if (response.StatusCode == HttpStatusCode.OK)
                result = await content.ReadAsStringAsync();
            else
                throw new Exception($"Ошибка при выполнении запроса.\nКод ответа {response.StatusCode}.\n");
        }

        try
        {
            return Serializer.Deserialize<T>(result);
        }
        catch (Exception exc1)
        {
            try
            {
                //сейчас сведения об ошибке исключаются, но после можно будет разделить на 2 группы и добавить вторую, например, в лог
                var excMessage = Serializer.Deserialize<SimpleResponse>(Regex.Match(result, @"(^{[\s\S]*})(<br[\s\S]*)").Groups[1].Value);
                throw new Exception(excMessage.Error ? excMessage.Message : $"Внешнее исключение: {exc1.Message}. Неизвестная ошибка");
            }
            catch (Exception exc2)
            {
                throw new Exception($"Внешнее: {exc1.Message}. Внутреннее: {exc2.Message}");
            }
        }
    }

    private async static Task<T> BaseRequestPost<T>(string url, List<string> values, string message = null)
    {
        if (url == null)
            throw new Exception("Отсутствует URL-адрес для запроса");
        string result = "";

        Client.DefaultRequestHeaders.Clear();
        Client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", AdmittanceToken.Token);
        using (HttpResponseMessage response = await Client.PostAsync(url + RequestParameters.StringParametersBuilder(values), message != null ? new FormUrlEncodedContent(new Dictionary<string, string>() { ["log"] = message }) : null))
        using (HttpContent content = response.Content)
        {
            if (response.StatusCode == HttpStatusCode.OK)
                result = await content.ReadAsStringAsync();
            else
                throw new Exception($"Ошибка при выполнении запроса.\nКод ответа {response.StatusCode}.\n");
        }
        try
        {
            return Serializer.Deserialize<T>(result);
        }
        catch (Exception exc1)
        {
            try
            {
                //сейчас сведения об ошибке исключаются, но после можно будет разделить на 2 группы и добавить вторую, например, в лог
                var excMessage = Serializer.Deserialize<SimpleResponse>(Regex.Match(result, @"(^{[\s\S]*})(<br[\s\S]*)").Groups[1].Value);
                throw new Exception(excMessage.Error ? excMessage.Message : $"Внешнее исключение: {exc1.Message}. Неизвестная ошибка");
            }
            catch (Exception exc2)
            {
                throw new Exception($"Внешнее: {exc1.Message}. Внутреннее: {exc2.Message}");
            }
        }
    }

    public async static Task<string> BaseImageRequest(string url)
    {
        string result = "";
        if (string.IsNullOrEmpty(url)) return result;
        try
        {
            Client.DefaultRequestHeaders.Clear();
            using (HttpResponseMessage response = await Client.GetAsync($"{url}&token={AdmittanceToken.ImageToken}"))
            using (HttpContent content = response.Content)
            {
                if (response.StatusCode == HttpStatusCode.OK)
                    result = await content.ReadAsStringAsync();
            }
        }
        catch (Exception exc)
        {
            throw new Exception("Запрос BaseImageRequest. " + exc.Message);
        }
        return result;
    }
}