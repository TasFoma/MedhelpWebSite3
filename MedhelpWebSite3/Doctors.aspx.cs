using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// Обработка ошибок не сделана
/// </summary>

public partial class Doctors : Page
{
    private string doctorsListName = "doctorsList";

    private BaseEntity<Doctor> GetAllDoctors()
    {
        return ViewState[doctorsListName] as BaseEntity<Doctor>;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack) return;

        RegisterAsyncTask(new PageAsyncTask(PageLoadAsync));
    }

    public async Task PageLoadAsync()
    {
        if (Session[SessionParamName.UrlAddressSession] == null || Session[SessionParamName.BranchID] == null)
            Response.Redirect("SessionEnded.aspx", true);
        try
        {
            var responseAllDoctors = await ApiRequest.GetAllDoctors(Session[SessionParamName.UrlAddressSession] as string
                , new DoctorsParameters(UsersAccessData.ClientID, Session[SessionParamName.BranchID] as string, Session[SessionParamName.DB_name] as string));
            ViewState[doctorsListName] = responseAllDoctors;
            await responseAllDoctors.ConvertStringToImage();
            doctorsListView.DataSource = responseAllDoctors.Response;
            doctorsListView.DataBind();
            var responseSpecialty = await ApiRequest.GetSpecialties(Session[SessionParamName.UrlAddressSession] as string
                , new SpecialtiesParameters(UsersAccessData.ClientID, Session[SessionParamName.BranchID] as string, Session[SessionParamName.DB_name] as string));
            roleCheckDropDownList.DataSource = responseSpecialty.Response;
            roleCheckDropDownList.DataTextField = "Title";
            roleCheckDropDownList.DataValueField = "Id_Spec";
            roleCheckDropDownList.DataBind();
            roleCheckDropDownList.Items.Insert(0, new ListItem("Все", "-1"));
        }
        catch
        {
            Response.Redirect("SessionEnded.aspx", false);
        }
    }
    
    protected void doctorsSearchButton_ServerClick(object sender, EventArgs e)
    {
        var searchedText = doctorsSearchTextBox.Text;
        roleCheckDropDownList.SelectedIndex = 0;
        var doctors = GetAllDoctors();
        if (doctors == null) return;
        if (string.IsNullOrEmpty(searchedText) || string.IsNullOrWhiteSpace(searchedText))
        {
            doctorsListView.DataSource = doctors.Response;
            doctorsListView.DataBind();
            return;
        }
        var filteredDoctors = doctors.Response.Where(row => row.Full_Name.ToLower().Contains(searchedText.ToLower()));
        doctorsListView.DataSource = filteredDoctors;
        doctorsListView.DataBind();
    }

    protected void roleCheckDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        var selectedItem = roleCheckDropDownList.SelectedItem;
        var doctors = GetAllDoctors();
        if (doctors == null) return;

        if (selectedItem.Text.Equals("Все"))
        {
            doctorsListView.DataSource = doctors.Response;
            doctorsListView.DataBind();
        }
        else
        {
            var filteredDoctors = doctors.Response.Where(row => row.Specialty.Contains(selectedItem.Text));
            doctorsListView.DataSource = filteredDoctors;
            doctorsListView.DataBind();
        }
    }

    protected void doctorsSearchTextBox_TextChanged(object sender, EventArgs e)
    {
        var searchedText = doctorsSearchTextBox.Text;
        roleCheckDropDownList.SelectedIndex = 0;
        var doctors = GetAllDoctors();
        if (doctors == null) return;
        if (string.IsNullOrEmpty(searchedText) || string.IsNullOrWhiteSpace(searchedText))
        {
            doctorsListView.DataSource = doctors.Response;
            doctorsListView.DataBind();
            return;
        }
        var filteredDoctors = doctors.Response.Where(row => row.Full_Name.ToLower().Contains(searchedText.ToLower()));
        doctorsListView.DataSource = filteredDoctors;
        doctorsListView.DataBind();
    }
}