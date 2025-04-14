using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ServicesPrice : Page
{
    private string servicesList = "servicesList";
    private string specialtiesList = "specialtiesList";

    private string businessID = "BusinessID";
    private string specID = "SpecID";
    private string clientID = "ClientID";
    private string filialID = "FilialID";
    private string tokenID = "TokenID";

    private async Task<List<Specialty>> GetSpecialtiesFromCache()
    {
        List<Specialty> specialties = Cache[specialtiesList + businessIDLabel.Text] != null ? Cache[specialtiesList + businessIDLabel.Text] as List<Specialty> : null;
        if (specialties == null)
        {
            BaseEntity<Specialty> baseSpecialties = new BaseEntity<Specialty>();
            try
            {
                baseSpecialties = await ApiRequest.GetSpecialties(Session[SessionParamName.UrlAddressSession] as string
                    , new SpecialtiesParameters(UsersAccessData.ClientID, Session[SessionParamName.BranchID] as string, Session[SessionParamName.DB_name] as string));
                specialties = baseSpecialties?.Response;
                if (specialties != null)
                {
                    Cache.Insert(specialtiesList + businessIDLabel.Text, specialties, null, DateTime.Now.AddMinutes(5), TimeSpan.Zero);
                }
            }
            catch
            {
                errorLabelContainer.Visible = true;
            }
        }
        return specialties;
    }

    private async Task<List<Service>> GetServicesFromCache()
    {
        List<Service> services = Cache[servicesList + businessIDLabel.Text] != null ? Cache[servicesList + businessIDLabel.Text] as List<Service> : null;
        if (services == null)
        {
            var baseServices = new BaseEntity<Service>();
            try
            {
                baseServices = await ApiRequest.GetServices(Session[SessionParamName.UrlAddressSession] as string
                    , new ServicesParameters(UsersAccessData.ClientID, Session[SessionParamName.BranchID] as string, Session[SessionParamName.DB_name] as string));
                services = new List<Service>(baseServices?.Response.Where(ser3 => ser3.Zapis != 0).OrderBy(ser1 => ser1.Poryadok).OrderByDescending(ser2 => ser2.Poryadok != 0));
                if (services != null)
                {
                    Cache.Insert(servicesList + businessIDLabel.Text, services, null, DateTime.Now.AddMinutes(5), TimeSpan.Zero);
                }
            }
            catch
            {
                errorLabelContainer.Visible = true;
            }
        }
        return services;
    }

    protected async void Page_Load(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(1000);

        var businessId = Request.QueryString[businessID];
        var specId = Request.QueryString[specID];
        var clientId = Request.QueryString[clientID];
        var filialId = Request.QueryString[filialID];
        var tokenId = Request.QueryString[tokenID];

        if (businessId == null)
            throw new MissingBusinessIDException();

        var centerInfo = await ApiRequest.GetCenterInfoByBusinessID(new CenterInfoParameters(businessId));
        var api = centerInfo.Response[0].Ip_Address;
        var branchID = centerInfo.Response[0].Id_Filial;
        var maxApp = centerInfo.Response[0].Max_Zapis;
        var centerID = centerInfo.Response[0].Id_Center.ToString();
        var title = centerInfo.Response[0].Title;
        var clientIP = GetIPAddress();//await GetIPRequest.GetMyIP();
        var db_name = centerInfo.Response[0].DB_name;

        var refererAddress = GetRefererAddress();
        if (api == null || branchID == null || maxApp == null)
            throw new SpecialException(Errors.NoCenterInfo);

        if (!Page.IsPostBack) ApiRequest.SetTimeout();
        if (Page.IsPostBack && Session[SessionParamName.UrlAddressSession] != null && Session[SessionParamName.BranchID] != null)
        {
            return;
        }
        errorLabelContainer.Visible = false;

        try
        {
            Session.Timeout = 10;//выкинет после 10 минут бездействия
            Session[SessionParamName.UrlAddressSession] = api;
            Session[SessionParamName.BranchID] = branchID;
            Session[SessionParamName.CenterID] = centerID;
            Session[SessionParamName.BusinessID] = businessId;
            Session[SessionParamName.SpecID] = specId;
            Session[SessionParamName.ClientID] = clientId;
            Session[SessionParamName.FilialID] = filialId;
            Session[SessionParamName.TokenID] = tokenId;
            Session[SessionParamName.ClientIPAddr] = clientIP;
            Session[SessionParamName.MedTitle] = title;
            Session[SessionParamName.DB_name] = db_name;
            var headerLabel = Master.FindControl("headerLabel") as Label;
            if (!headerLabel.Text.EndsWith($"\"{title}\""))
                headerLabel.Text += $"\"{title}\"";
            if (refererAddress != null)
                Session[SessionParamName.ClientSiteAddr] = refererAddress;
            hiddenLabel.Text = maxApp.ToString();
            await ApiRequest.InsertLogData(new InsertLogParameters(centerID, branchID, null, clientIP, LogTypes.Route), LogMessage.HasEnteredInTheSite);
        }
        catch (MissingBusinessIDException)
        {
            RedirectToLoginFailedPage();
            //errorLabelContainer.Visible = true;
            return;
        }
        catch (SpecialException)
        {
            errorLabelContainer.Visible = true;
            return;
        }
        catch (Exception)
        {
            errorLabelContainer.Visible = true;
            return;
        }
        businessIDLabel.Text = Request.QueryString[businessID];
        await PageLoadAsync();

        if (specId != null)
        {
            SpecCheckDropDownList.ClearSelection();
            SpecCheckDropDownList.Items.FindByText(specId).Selected = true;

            try
            {
                var selectedValue = SpecCheckDropDownList.SelectedValue;
                var services = await GetServicesFromCache();
                if (services == null)
                {
                    errorLabelContainer.Visible = true;
                    return;
                }
                SpecSearchTextBox.TextChanged -= RoleSearchTextBox_TextChanged;
                SpecSearchTextBox.Text = string.Empty;
                SpecSearchTextBox.TextChanged += RoleSearchTextBox_TextChanged;

                switch (selectedValue)
                {
                    case "0":
                        servicesContainer.Visible = false;
                        priceListView.DataSource = null;
                        priceListView.DataBind();
                        break;
                    default:
                        var deletedItem = SpecCheckDropDownList.Items.FindByValue("0");
                        if (deletedItem != null)
                            SpecCheckDropDownList.Items.Remove(deletedItem);
                        var filteredServices = services.Where(row => row.Id_Spec == int.Parse(selectedValue));
                        priceListView.DataSource = filteredServices;
                        priceListView.DataBind();

                        servicesContainer.Visible = filteredServices != null && filteredServices.Count() > 0;
                        haventServicesDiv.Visible = !servicesContainer.Visible;
                        break;
                }
            }
            catch
            {

            }
        }
    }

    protected string GetRefererAddress()
    {
        return Request.ServerVariables["HTTP_REFERER"];
    }

    protected string GetIPAddress()
    {
        System.Web.HttpContext context = System.Web.HttpContext.Current;
        string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

        if (!string.IsNullOrEmpty(ipAddress))
        {
            string[] addresses = ipAddress.Split(',');
            if (addresses.Length != 0)
            {
                return addresses[0];
            }
        }

        return context.Request.ServerVariables["REMOTE_ADDR"];
    }

    public void ClearDataLists()
    {
        priceListView.Items.Clear();
        priceListView.DataSource = null;
        priceListView.DataBind();
        SpecCheckDropDownList.Items.Clear();
        SpecCheckDropDownList.DataSource = null;
        SpecCheckDropDownList.DataBind();
    }

    public async Task PageLoadAsync()
    {
        try
        {
            if (Page.IsPostBack)
                ClearDataLists();
            priceListView.DataSource = await GetServicesFromCache();
            priceListView.DataBind();
            var responseSpecialty = await GetSpecialtiesFromCache();
            SpecCheckDropDownList.DataSource = responseSpecialty;
            SpecCheckDropDownList.DataTextField = "Title";
            SpecCheckDropDownList.DataValueField = "Id_Spec";
            SpecCheckDropDownList.DataBind();
        }
        catch (Exception)
        {
            errorLabelContainer.Visible = true;
        }
    }

    //Выбор специальности
    protected async void RoleCheckDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {           
            var selectedValue = SpecCheckDropDownList.SelectedValue;
            var services = await GetServicesFromCache();
            if (services == null)
            {
                errorLabelContainer.Visible = true;
                return;
            }
            SpecSearchTextBox.TextChanged -= RoleSearchTextBox_TextChanged;
            SpecSearchTextBox.Text = string.Empty;
            SpecSearchTextBox.TextChanged += RoleSearchTextBox_TextChanged;
                        
            switch (selectedValue)
            {
                case "0":
                    servicesContainer.Visible = false;
                    priceListView.DataSource = null;
                    priceListView.DataBind();
                    break;
                default:
                    var deletedItem = SpecCheckDropDownList.Items.FindByValue("0");
                    if (deletedItem != null)
                        SpecCheckDropDownList.Items.Remove(deletedItem);
                    var filteredServices = services.Where(row => row.Id_Spec == int.Parse(selectedValue));
                    priceListView.DataSource = filteredServices;
                    priceListView.DataBind();

                    servicesContainer.Visible = filteredServices != null && filteredServices.Count() > 0;
                    haventServicesDiv.Visible = !servicesContainer.Visible;
                    break;
            }
        }
        catch 
        {
           
        }
    }

    protected async void RoleSearchButton_ServerClick(object sender, EventArgs e)
    {
        var searchedText = SpecSearchTextBox.Text;
        await SearchByInputedText(searchedText);
    } 

    protected async void RoleSearchTextBox_TextChanged(object sender, EventArgs e)
    {
        var searchedText = SpecSearchTextBox.Text;
        await SearchByInputedText(searchedText);
    }

    protected async Task SearchByInputedText(string searchedText)
    {
        SpecCheckDropDownList.SelectedIndexChanged -= RoleCheckDropDownList_SelectedIndexChanged;
        AddHintItemToDDL();
        SpecCheckDropDownList.SelectedIndex = 0;
        SpecCheckDropDownList.SelectedIndexChanged += RoleCheckDropDownList_SelectedIndexChanged;
        if (string.IsNullOrEmpty(searchedText) || string.IsNullOrWhiteSpace(searchedText) || searchedText.Length < 3)
        {
            servicesContainer.Visible = false;
            haventServicesLabel.Visible = true;
            priceListView.DataSource = null;
            priceListView.DataBind();
            return;
        }
        var services = await GetServicesFromCache();
        if (services == null)
        {
            errorLabelContainer.Visible = true;
            return;
        }
        var filteredServices = services.Where(row => row.Title.ToLower().Contains(searchedText.ToLower()));
        priceListView.DataSource = filteredServices;
        priceListView.DataBind();
        servicesContainer.Visible = filteredServices != null && filteredServices.Count() > 0;
        haventServicesDiv.Visible = !servicesContainer.Visible;
    }

    protected void AddHintItemToDDL()
    {
        if (SpecCheckDropDownList.Items.FindByValue("0") != null) return;
        SpecCheckDropDownList.Items.Insert(0, new ListItem("Выбор специальности", "0"));
    }

    //protected void priceListView_ItemDataBound(object sender, ListViewItemEventArgs e)
    //{
    //    if ((e.Item.DataItem as Service).Zapis == 1) return;
    //    e.Item.Controls.RemoveAt(3);
    //    e.Item.Controls.RemoveAt(1);
    //}

    protected async void AdditSignUpLinkButton_Click(object sender, EventArgs e)
    {
        var services = await GetServicesFromCache();

        var serviceId = Convert.ToInt32((sender as LinkButton).CommandArgument);
        try
        {
            var filteredServices = services?.First(row => row.Id_Service == serviceId);
            Session[SessionParamName.AppData] = $"{serviceId};{filteredServices?.Title};{filteredServices?.Admission};{hiddenLabel.Text};{filteredServices?.Max_Zapis};{filteredServices?.Value}";
            Response.Redirect("AppointmentDatePicking.aspx", false);
        }
        catch { }
    }

    private void RedirectToLoginFailedPage()
    {
        Response.Redirect($"LoginFailed.aspx", false);
    }
}