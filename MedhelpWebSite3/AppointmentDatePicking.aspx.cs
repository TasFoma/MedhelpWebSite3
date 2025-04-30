using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class AppointmentDatePicking : Page
{
    private string cookieSet = "Phones Set";
    private string phoneCookie = "PhoneCookie";

    private string branchViewState = "branchViewState";
    private string scheduleListViewState = "scheduleList";

    private string baseClassDayButton = "day-of-week-button";

    private string passedClass = "passed-day";
    //private string enabledClass = "enabled-day";
    private string loBusinessClass = "lo-business-day";
    //private string mediumBusinessClass = "medium-business-day";
    //private string highBusinessClass = "high-business-day";

    private List<Branch> GetBranches()
    {
        return ViewState[branchViewState] as List<Branch>;
    }

    private BaseEntity<ScheduleByService> GetSchedule()
    {
        return ViewState[scheduleListViewState] as BaseEntity<ScheduleByService>;
    }

    public async Task DoTheAsyncTask()
    {
        try
        {
            if (Session[SessionParamName.UrlAddressSession] == null || Session[SessionParamName.BranchID] == null)
                throw new SpecialException(Errors.SessionEndedError);
            BaseEntity<ScheduleByService> responseSchedule = null;
            try
            {
                responseSchedule = await ApiRequest.GetScheduleByService(Session[SessionParamName.UrlAddressSession] as string
                    , new ScheduleByServiceParameters(serviceIdLbl.Text, scheduleCurrentMonthLabel.Text, admissionLbl.Text, UsersAccessData.ClientID
                    , checkedBranchIDLabel.Text == "" ? Session[SessionParamName.BranchID] as string : checkedBranchIDLabel.Text, Session[SessionParamName.DB_name] as string));
            }
            catch (Exception exc)
            {
                InsertLogData(null, exc.Message, LogTypes.Error);
                throw new SpecialException(Errors.UnknownError);
            }
            responseSchedule.Response = GetDoctorsWithFreeAppTime(responseSchedule);
            ViewState[scheduleListViewState] = responseSchedule;
        }
        catch (SpecialException)
        {
            RedirectToSessionEndedPage();
            //ShowErrorContainer();
        }
        catch (Exception)
        {
            ShowErrorContainer();
        }
    }

    protected List<ScheduleByService> GetDoctorsWithFreeAppTime(BaseEntity<ScheduleByService> schedule)
    {
        return new List<ScheduleByService>(schedule.Response.Where(doc => doc.Adm_Time != null && doc.Adm_Time.Count > 0));
    }

    protected void FormatRptBranchButtons()
    {
        if (checkedBranchIDLabel.Text != string.Empty)
        {
            foreach (Control control in rptBranchButton.Controls)
            {
                var item = control.FindControl("branchButton") as Button;
                if (item != null && item.CommandArgument.Equals(checkedBranchIDLabel.Text))
                {
                    var script = string.Format("ChangeFormatRepeaterButtons('{0}');", item.ClientID);
                    ScriptManager.RegisterStartupScript(item, GetType(), "SaveOldBranchButtonFormat", script, true);
                    return;
                }
            }
        }
    }

    protected void Page_Prerender(object sender, EventArgs e)
    {
        if (Page.IsPostBack) return;

        var mainBrachId = Session[SessionParamName.BranchID] as string;

        try
        {
            if (Session[SessionParamName.UrlAddressSession] == null || mainBrachId == null)
                throw new SpecialException();
        }
        catch (SpecialException)
        {
            RedirectToSessionEndedPage();
            return;
        }

        if (rptBranchButton.Controls.Count == 0)
        {
            ShowErrorContainer(Errors.NoBranches);
            return;
        }

        SelectedBranchSetting(rptBranchButton.Controls[0].FindControl("branchButton") as Button);

        foreach (Control rptBrachContol in rptBranchButton.Controls)
        {
            var button = rptBrachContol.FindControl("branchButton") as Button;
            if (button != null && button.CommandArgument == mainBrachId)
            {
                SelectedBranchSetting(button);
                break;
            }
        }
    }

    protected void ShowErrorContainer(string error = null)
    {
        errorOnPageLabel.Text = error ?? Errors.SomethingWentWrong;
        errorLabelContainer.Visible = true;
    }

    protected async void Page_Load(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(1000);//Чтобы не мелькала гифка загрузки при быстрых запросах
        ScriptManager.ScriptResourceMapping.AddDefinition("jquery", new ScriptResourceDefinition
        {
            Path = "~/js/jquery-3.3.1.min.js",

        });
        errorLabelContainer.Visible = false;
        var script = "InitModalDialogElements();";
        ScriptManager.RegisterStartupScript(sender as Control, GetType(), "InitElements", script, true);

        script = "BindInformationButtonClickEvent();";
        ScriptManager.RegisterStartupScript(sender as Control, GetType(), "BindInformationButtonClickEvent", script, true);

        script = "AddMaskToCallMeTextBox();";
        ScriptManager.RegisterStartupScript(sender as Control, GetType(), "AddMaskToCallMeTextBox", script, true);

        script = "RemoveBackButtonForMobiles();";
        ScriptManager.RegisterStartupScript(sender as Control, GetType(), "RemoveBackButtonForMobiles", script, true);

        string[] pageParametres;
        if (Session[SessionParamName.AppData] == null)
            CheckedServiceTitle.InnerHtml = "<span style=\"color: red\">Вы не выбрали услугу для записи</span>";
        else
        {
            pageParametres = (Session[SessionParamName.AppData] as string).Split(';');
            //if (!string.IsNullOrEmpty(serviceIdLbl.Text) && !string.IsNullOrEmpty(serviceNameLbl.Text) && !string.IsNullOrEmpty(admissionLbl.Text) && !string.IsNullOrEmpty(commonMaxAppLabel.Text) && !string.IsNullOrEmpty(serviceMaxAppLabel.Text) && !string.IsNullOrEmpty(servicePriceLbl.Text)
            //    ||
            //    pageParametres != null && pageParametres.Length == 6 && !pageParametres.Any(par => string.IsNullOrEmpty(par)))
            if ((string.IsNullOrEmpty(serviceIdLbl.Text) || string.IsNullOrEmpty(serviceNameLbl.Text) || string.IsNullOrEmpty(admissionLbl.Text) || string.IsNullOrEmpty(commonMaxAppLabel.Text) || string.IsNullOrEmpty(serviceMaxAppLabel.Text) || string.IsNullOrEmpty(servicePriceLbl.Text))
                &&
                pageParametres != null && pageParametres.Length == 6 && !pageParametres.Any(par => string.IsNullOrEmpty(par)))
            {
                serviceIdLbl.Text = pageParametres[0];
                serviceNameLbl.Text = CheckedServiceTitle.InnerHtml = pageParametres[1];
                admissionLbl.Text = pageParametres[2];
                commonMaxAppLabel.Text = pageParametres[3];
                serviceMaxAppLabel.Text = pageParametres[4];
                servicePriceLbl.Text = pageParametres[5];
            }
            else
            {
                string BranchID = checkedBranchIDLabel.Text;
                string FilialID = Session[SessionParamName.FilialID] as string;
                string CenterID = Session[SessionParamName.CenterID] as string;

                /*if (pageParametres[5].Contains("Гео"))
                {
                    if (Session[SessionParamName.NameClickFilial] != null)
                        if (Session[SessionParamName.NameClickFilial].ToString() == "Геодезическая")
                        {
                            servicePriceLbl.Text = pageParametres[5].Substring(pageParametres[5].IndexOf('(')+7).Replace(")", "");
                        }
                        else
                        {
                            servicePriceLbl.Text = pageParametres[5].Substring(0, pageParametres[5].IndexOf('('));
                        }
                }*/
                CheckedServiceTitle.InnerHtml = "<span style=\"color: red\">Данные устарели. Попробуйте обновить предыдущую страницу и выбрать услугу снова.</span>";
            }
        }

        var today = DateTime.Now;
        var mondayDate = DateTime.MinValue;
        if (!Page.IsPostBack)
        {
            modalHeaderLabel.Text = "Онлайн-запись в медицинский центр \"" + Session[SessionParamName.MedTitle] as string + "\"";
            var headerLabel = Master.FindControl("headerLabel") as Label;
            if (!headerLabel.Text.EndsWith($"\"{Session[SessionParamName.MedTitle] as string}\""))
                headerLabel.Text += $"\"{Session[SessionParamName.MedTitle] as string}\"";
            InsertLogData(null, LogMessage.HasEnteredInTheAppDatePicking + ". Услуга: " + serviceNameLbl.Text, LogTypes.Route);
            mondayDate = today.AddDays(-ScheduleFormStaticValues.DaysOfWeek[today.DayOfWeek.ToString()] + 1);
            scheduleCurrentMonthLabel.Text = mondayDate.ToString("dd.MM.yyyy");
            monthLabel.Text = ScheduleFormStaticValues.Months[mondayDate.Month];
            await BindDataToRptBranch();
        }
        else
            mondayDate = DateTime.Parse(scheduleCurrentMonthLabel.Text);
        if (serviceIdLbl.Text == string.Empty)
        {
            FillWeek(mondayDate, true);
            return;
        }
        try
        {
            await UpdateSchedule();
        }
        catch (Exception exc)
        {
            InsertLogData(null, exc.Message, LogTypes.Error);
            ShowErrorContainer();
        }
    }

    private void InitScheduleCells()
    {
        var date = DateTime.Parse(scheduleCurrentMonthLabel.Text);
        FillWeek(date);
    }

    private void FormatDayButton(Button button)
    {
        MonButton.CssClass = MonButton.CssClass.Replace(" checked-day", "");
        TueButton.CssClass = TueButton.CssClass.Replace(" checked-day", "");
        WedButton.CssClass = WedButton.CssClass.Replace(" checked-day", "");
        ThuButton.CssClass = ThuButton.CssClass.Replace(" checked-day", "");
        FriButton.CssClass = FriButton.CssClass.Replace(" checked-day", "");
        SatButton.CssClass = SatButton.CssClass.Replace(" checked-day", "");
        SunButton.CssClass = SunButton.CssClass.Replace(" checked-day", "");
        button.CssClass += " checked-day";
    }

    private void FormatDayButton(DateTime date, DateTime today, Button button, bool valueIsNull, int buttonNumber)
    {
        Label label = null;
        switch (buttonNumber)
        {
            case 0: label = MonLabel; break;
            case 1: label = TueLabel; break;
            case 2: label = WedLabel; break;
            case 3: label = ThuLabel; break;
            case 4: label = FriLabel; break;
            case 5: label = SatLabel; break;
            case 6: label = SunLabel; break;
        }

        BaseEntity<ScheduleByService> data = null;
        if (!valueIsNull)
            data = GetSchedule();

        if (label != null)
        {
            label.CssClass = "day-of-week-label";

            if (data == null || data.Error || date < today || data.Response.Count == 0
                || !data.Response.Any(row => DateTime.Parse(row.Adm_Day).Date == date))
            {
                button.CssClass = $"{baseClassDayButton} {passedClass}";
                button.Enabled = false;
                label.CssClass += " disabled-day-of-week";
            }
            else
            {
                var amount = data.Response.Where(row => DateTime.Parse(row.Adm_Day).Date == date)
                    .Sum(time => time.Adm_Time != null ? time.Adm_Time.Count : 0);

                switch (amount)
                {
                    case 0:
                        button.CssClass = $"{baseClassDayButton} {passedClass}";
                        button.Enabled = false;
                        label.CssClass += " disabled-day-of-week";
                        break;
                    default:
                        button.CssClass = $"{baseClassDayButton} {loBusinessClass}";
                        DateTime dt;
                        if (!DateTime.TryParse(checkedDayLabel.Text, out dt))
                        {
                            var buttonDate = GetButtonDate(buttonNumber);
                            DayOfWeekButtonClick(buttonDate, button);
                        }
                        button.Enabled = true;
                        break;
                }
            }
        }
    }

    private DateTime GetButtonDate(int addDays)
    {
        DateTime buttonDate;
        try
        {
            if (!DateTime.TryParse(scheduleCurrentMonthLabel.Text, out buttonDate))
                throw new SpecialException(Errors.UnknownError);
            buttonDate = buttonDate.AddDays(addDays);
            return buttonDate;
        }
        catch (SpecialException)
        {
            ShowErrorContainer();
            return DateTime.MaxValue;
        }
    }

    private void FillWeek(DateTime start, bool valueIsNull = false)
    {
        var todayDate = DateTime.Now.Date;

        if (start.Date <= todayDate)
        {
            prevButton.Attributes["onclick"] = "javascript:PrevWeek(false);";
            prevButton.Attributes["class"] = "prev-week-button disabled-prev-week-button";
        }
        else
        {
            prevButton.Attributes["onclick"] = "javascript:PrevWeek(true); return true;";
            prevButton.Attributes["class"] = "prev-week-button";
        }

        if (start.Date >= todayDate.AddDays(-ScheduleFormStaticValues.DaysOfWeek[todayDate.DayOfWeek.ToString()] + 1).AddDays(28))
        {
            nextButton.Attributes["onclick"] = "javascript:NextWeek(false);";
            nextButton.Attributes["class"] = "next-week-button disabled-next-week-button";
        }

        else
        {
            nextButton.Attributes["onclick"] = "javascript:NextWeek(true); return true;";
            nextButton.Attributes["class"] = "next-week-button";
        }

        var schedule = GetSchedule();
        if (schedule == null || schedule.Error || schedule.Response.Count == 0)
        {
            checkboxRequired.Enabled = phoneRfv.Enabled = phoneRev.Enabled = codeRfv.Enabled = false;
            TermsCustomValidator.Enabled = true;
            CallMeContainer.Attributes["class"] = "call-me-container show";
        }
        else
        {
            checkboxRequired.Enabled = phoneRfv.Enabled = phoneRev.Enabled = true;
            TermsCustomValidator.Enabled = codeRfv.Enabled = false;
            CallMeContainer.Attributes["class"] = "call-me-container";
        }

        var curDate = start.Date;
        FormatDayButton(curDate, todayDate, MonButton, valueIsNull, 0);
        MonButton.Text = curDate.Day.ToString();

        curDate = start.AddDays(1).Date;
        FormatDayButton(curDate, todayDate, TueButton, valueIsNull, 1);
        TueButton.Text = curDate.Day.ToString();

        curDate = start.AddDays(2).Date;
        FormatDayButton(curDate, todayDate, WedButton, valueIsNull, 2);
        WedButton.Text = curDate.Day.ToString();

        curDate = start.AddDays(3).Date;
        FormatDayButton(curDate, todayDate, ThuButton, valueIsNull, 3);
        ThuButton.Text = curDate.Day.ToString();

        curDate = start.AddDays(4).Date;
        FormatDayButton(curDate, todayDate, FriButton, valueIsNull, 4);
        FriButton.Text = curDate.Day.ToString();

        curDate = start.AddDays(5).Date;
        FormatDayButton(curDate, todayDate, SatButton, valueIsNull, 5);
        SatButton.Text = curDate.Day.ToString();

        curDate = start.AddDays(6).Date;
        FormatDayButton(curDate, todayDate, SunButton, valueIsNull, 6);
        SunButton.Text = curDate.Day.ToString();
    }

    protected async void NextWeekButton_Click(object sender, EventArgs e)
    {
        ClearDoctorListView();
        checkedDayLabel.Text = string.Empty;
        DateTime date;
        try
        {
            if (!DateTime.TryParse(scheduleCurrentMonthLabel.Text, out date))
                throw new SpecialException(Errors.UnknownError);
            if (date >= DateTime.Today.AddDays(-ScheduleFormStaticValues.DaysOfWeek[DateTime.Today.DayOfWeek.ToString()] + 1).AddDays(28))
                return;

        }
        catch
        {
            //RedirectToErrorPage(specExc.Message);
            ShowErrorContainer();
            return;
        }

        var mondayDate = date.AddDays(7);
        monthLabel.Text = ScheduleFormStaticValues.Months[mondayDate.Month];
        scheduleCurrentMonthLabel.Text = mondayDate.ToString("dd.MM.yyyy");
        if (serviceIdLbl.Text == string.Empty)
        {
            FillWeek(mondayDate, true);
            return;
        }
        try
        {
            await UpdateSchedule();
            FormatRptBranchButtons();
        }
        catch (Exception exc)
        {
            InsertLogData(null, exc.Message, LogTypes.Error);
            ShowErrorContainer();
        }
    }

    protected async void PrevWeekButton_Click(object sender, EventArgs e)
    {
        ClearDoctorListView();
        checkedDayLabel.Text = string.Empty;
        DateTime date;
        try
        {
            if (!DateTime.TryParse(scheduleCurrentMonthLabel.Text, out date))
                throw new SpecialException(Errors.UnknownError);
            TimeSpan tmpDate = DateTime.Today - date;
            if (tmpDate <= TimeSpan.FromDays(6) && tmpDate >= TimeSpan.Zero)
                return;
        }
        catch
        {
            //RedirectToErrorPage(specExc.Message);
            ShowErrorContainer();
            return;
        }
        DateTime mondayDate = date.AddDays(-7);
        monthLabel.Text = ScheduleFormStaticValues.Months[mondayDate.Month];
        scheduleCurrentMonthLabel.Text = mondayDate.ToString("dd.MM.yyyy");
        if (serviceIdLbl.Text == string.Empty)
        {
            FillWeek(mondayDate, true);
            return;
        }
        try
        {
            await UpdateSchedule();
            FormatRptBranchButtons();
        }
        catch (Exception exc)
        {
            InsertLogData(null, exc.Message, LogTypes.Error);
            //RedirectToErrorPage(Errors.UnknownError);
            ShowErrorContainer();
        }
    }

    private void BindData(DateTime date)
    {
        var data = GetSchedule();
        if (data == null) return;
        DateTime dt;
        var bindingData = data.Response.Where(doctor => DateTime.TryParse(doctor.Adm_Day, out dt) && dt == date);
        doctorsListView.DataSource = bindingData;
        doctorsListView.DataBind();
    }

    private void ClearDoctorListView()
    {
        doctorsListView.DataSource = null;
        doctorsListView.DataBind();
    }

    private void SaveCheckedDay(DateTime date)
    {
        checkedDayLabel.Text = date.ToString("dd.MM.yyyy");
    }

    protected async void DayOfWeek_Click(object sender, EventArgs e)
    {
        int dayIndex = Int32.Parse((sender as Button).Attributes["DayOfWeekIndex"]);

        DateTime buttonDate = GetButtonDate(dayIndex);
        try
        {
            if (Session[SessionParamName.UrlAddressSession] == null)
                throw new SessionEndedException();
            var currentDate = await ApiRequest.GetCurrentServerDate(Session[SessionParamName.UrlAddressSession] as string
                , new GetCurrentServerDateParameters(UsersAccessData.ClientID
                , checkedBranchIDLabel.Text == "" ? Session[SessionParamName.BranchID] as string : checkedBranchIDLabel.Text, Session[SessionParamName.DB_name] as string));
            if (buttonDate == DateTime.MaxValue || DateTime.Parse(currentDate.Response.Today) > buttonDate) return;
        }
        catch (SessionEndedException)
        {
            RedirectToSessionEndedPage();
            return;
        }
        catch
        {
            ShowErrorContainer();
        }
        DayOfWeekButtonClick(buttonDate, sender as Button);
    }

    private void DayOfWeekButtonClick(DateTime date, Button button)
    {
        SaveCheckedDay(date);
        BindData(date);
        FormatDayButton(button);
        FormatRptBranchButtons();
    }

    protected async Task BindDataToRptBranch()
    {
        try
        {
            if (Session[SessionParamName.UrlAddressSession] == null)
                throw new SessionEndedException();
            var response = await ApiRequest.GetBranchesByServiceID(Session[SessionParamName.UrlAddressSession] as string
                , new BranchesByServiceIDParameters(UsersAccessData.ClientID, UsersAccessData.BranchID, Session[SessionParamName.DB_name] as string, serviceIdLbl.Text));
            if (response?.Response.Count == 0 || response?.Response[0].BranchID == null)
                response = null;

            if (response != null)
            {

                var selectedBranch = response.Response.Find(branch => branch.BranchID?.ToString() == Session[SessionParamName.BranchID] as string);
                var selectedBranchId = selectedBranch?.BranchID != null ? selectedBranch.BranchID.ToString()
                    : response.Response[0].BranchID?.ToString();

                checkedBranchIDLabel.Text = selectedBranchId ?? string.Empty;
            }

            rptBranchButton.DataSource = response?.Response;
            ViewState[branchViewState] = response?.Response;
            rptBranchButton.DataBind();
        }
        catch (SessionEndedException)
        {
            RedirectToSessionEndedPage();
            return;
        }
        catch (Exception exc)
        {
            InsertLogData(null, exc.Message, LogTypes.Error);
        }
    }

    protected async void DoctorsListView_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        ListViewDataItem item = (ListViewDataItem)e.Item;
        var rptr = e.Item.FindControl("rptRecordingButton") as Repeater;
        ScheduleByService dataItem = (ScheduleByService)item.DataItem;
        rptr.DataSource = dataItem.Adm_Time;
        rptr.DataBind();
        var doctor = await ApiRequest.GetDoctorInformation(Session[SessionParamName.UrlAddressSession] as string
            , new DoctorInformationParameters((
            e.Item.FindControl("doctorIDLabel") as Label).Text
            , UsersAccessData.ClientID
            , checkedBranchIDLabel.Text == "" ? Session[SessionParamName.BranchID] as string : checkedBranchIDLabel.Text
            , Session[SessionParamName.DB_name] as string));

        (e.Item.FindControl("specialtySpan") as HtmlGenericControl).InnerText = doctor.Response[0].Specialty;

        if (!string.IsNullOrEmpty(doctor.Response[0].Stag))
            (e.Item.FindControl("experienceSpan") as HtmlGenericControl).InnerText = doctor.Response[0].Stag;
        else
            e.Item.FindControl("experienceDiv").Visible = false;

        if (!string.IsNullOrEmpty(doctor.Response[0].Dop_Info))
            (e.Item.FindControl("additionalSpan") as HtmlGenericControl).InnerText = doctor.Response[0].Dop_Info;
        else
            e.Item.FindControl("additionalDiv").Visible = false;

        (e.Item.FindControl("informationButton") as Button).Visible = false;

        /*if (string.IsNullOrEmpty(doctor.Response[0].Dop_Info))
        {
            (e.Item.FindControl("informationButton") as Button).Visible = false;
            return;
        }
        if (string.IsNullOrEmpty(doctor.Response[0].Stag) && string.IsNullOrEmpty(doctor.Response[0].Dop_Info)) //было раньше
        {
            (e.Item.FindControl("informationButton") as Button).Visible = false;
            return;
        }

        if (!string.IsNullOrEmpty(doctor.Response[0].Dop_Info))
            (e.Item.FindControl("additionalSpan") as HtmlGenericControl).InnerText = doctor.Response[0].Dop_Info;
        else
            e.Item.FindControl("additionalDiv").Visible = false;*/
    }

    protected async void RptRecordingButton_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        Appointment app = new Appointment()
        {
            DoctorFullName = (e.Item.Parent.Parent.Parent.FindControl("doctorFullName") as Label).Text,
            DoctorId = (e.Item.Parent.Parent.Parent.FindControl("doctorIDLabel") as Label).Text,
            AppTime = e.CommandArgument.ToString()
        };
        ViewState["appData"] = app;
        codeWasRequestedLabel.Text = "0";
        errorLabel.Text = phoneTextBox.Value = codeTextBox.Text = "";
        phoneRfv.Enabled = phoneRev.Enabled = true;
        codeRfv.Enabled = false;
        int id = 0;

        var checkedBranch = ViewState[branchViewState] == null ? null : (ViewState[branchViewState] as List<Branch>)
            .Find(branch => int.TryParse(checkedBranchIDLabel.Text, out id) ? branch.BranchID == id : branch.BranchID == Convert.ToInt32(Session[SessionParamName.BranchID]));

        //Найти уникальную цену 
        BaseEntity<UniqCena> response = null;
        try
        {
            response = await ApiRequest.UniqCena(Session[SessionParamName.UrlAddressSession] as string
                , new UniqPriceParameters(app.DoctorId, serviceNameLbl.Text, UsersAccessData.ClientID
                , checkedBranchIDLabel.Text == "" ? Session[SessionParamName.BranchID] as string : checkedBranchIDLabel.Text, Session[SessionParamName.DB_name] as string));
        }
        catch
        { }

        string itog_cena = servicePriceLbl.Text;
        if (response.Response[0].cena.ToString() != "0")
            itog_cena = "!!ВНИМАНИЕ!! У данного врача отличается цена от общей и составляет: " + response.Response[0].cena.ToString();
        ////////////////////
        var script = string.Format("ShowModalDialogFirstStep('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', {7}, '{8}');"
        , checkedBranch.BranchName, checkedBranch.BranchAddress, itog_cena, serviceNameLbl.Text, checkedDayLabel.Text, app.AppTime
        , app.DoctorFullName, 0, null);
        ScriptManager.RegisterStartupScript(source as Control, GetType(), "ShowModalDialogFirst", script, true);
        FormatRptBranchButtons();
    }

    protected async void RptBranchButton_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        ClearDoctorListView();
        //выбор филиала для показа расписания на неделю и записи на приём
        Session[SessionParamName.NameClickFilial] = (e.Item.Controls[1] as Button).Text;//Геодезическая

        SelectedBranchSetting(e.Item.Controls[1] as Button);
        checkedDayLabel.Text = string.Empty;
        try
        {
            await UpdateSchedule();
        }
        catch (Exception exc)
        {
            InsertLogData(null, exc.Message, LogTypes.Error);
            //RedirectToErrorPage(Errors.UnknownError);
            ShowErrorContainer();
            return;
        }
    }

    protected void SelectedBranchSetting(Button button)
    {
        checkedBranchIDLabel.Text = button.CommandArgument.ToString();

        var script = string.Format("ChangeFormatRepeaterButtons('{0}');", button.ClientID);
        ScriptManager.RegisterStartupScript(button, GetType(), "ChangeFormatRepeaterButtons", script, true);
    }

    protected async void ConfirmButton_Click(object sender, EventArgs e)
    {
        await ConfirmAppointment(sender);
    }

    private async Task ConfirmAppointment(object sender)
    {
        BaseEntity<ScheduleByService> data = new BaseEntity<ScheduleByService>();
        Appointment appData = new Appointment();
        try
        {
            data = GetSchedule();
            appData = GetAppointment();
            if (data == null || appData == null)
                throw new SpecialException(Errors.AppointmentAreNullError);

        }
        catch (SpecialException)
        {
            RedirectToSessionEndedPage();
            return;
        }
        var curData = data.Response.Find(doctor => doctor.Id_Doctor.ToString() == appData.DoctorId);
        SimpleResponse response;
        try
        {
            if (Session[SessionParamName.UrlAddressSession] == null)
                throw new SpecialException();
            response = await ApiRequest.AppointmentConfirmationRequest(Session[SessionParamName.UrlAddressSession] as string
                , new AppointmentConfirmationParameters(appData.DoctorId, checkedDayLabel.Text, appData.AppTime, GetPhoneNumber(phoneTextBox.Value)
                , curData.Id_Spec.ToString(), serviceIdLbl.Text, admissionLbl.Text, UsersAccessData.ClientID
                , checkedBranchIDLabel.Text == "" ? Session[SessionParamName.BranchID] as string : checkedBranchIDLabel.Text, Session[SessionParamName.DB_name] as string));
        }
        catch (SpecialException)
        {
            RedirectToSessionEndedPage();
            ShowErrorContainer();
            return;
        }
        catch (Exception exc)
        {
            InsertLogData(GetPhoneNumber(phoneTextBox.Value), exc.Message, LogTypes.Error);
            //RedirectToErrorPage(Errors.UnknownError);
            ShowErrorContainer();
            return;
        }
        var itsOk = 1;
        var message = "";
        if (response.Error)
        {
            message = response.Message;
            itsOk = 0;
        }
        else
        {
            if (response.Response.ToLower().Equals("true"))
            {
                message = "Запись прошла успешно! Обратите внимание, что медицинский центр может Вам перезвонить для уточнения деталей по сделанной записи!";
                InsertLogData(GetPhoneNumber(phoneTextBox.Value), LogMessage.GetSuccessAppointmentLogText(checkedDayLabel.Text, appData.AppTime, serviceNameLbl.Text), LogTypes.Action);
                try
                {
                    if (Session[SessionParamName.CenterID] == null || Session[SessionParamName.BranchID] == null)
                        throw new SpecialException();
                    await ApiRequest.InsertAppointmentData(new InsertAppointmentInfo(Session[SessionParamName.CenterID] as string
                        , checkedBranchIDLabel.Text == "" ? Session[SessionParamName.BranchID] as string : checkedBranchIDLabel.Text, serviceNameLbl.Text
                        , checkedDayLabel.Text, appData.AppTime, GetPhoneNumber(phoneTextBox.Value)));
                }
                catch (SpecialException)
                {
                    RedirectToSessionEndedPage();
                    return;
                }
                catch (Exception)
                {
                    ShowErrorContainer();
                    return;
                }
            }
            else
            {
                message = response.Response;
                itsOk = 0;
                InsertLogData(GetPhoneNumber(phoneTextBox.Value), message, LogTypes.Warning);
            }
        }
        var script = string.Format("ShowConfirmAppointmentModalDialog({0}, '{1}');", itsOk, message);
        ScriptManager.RegisterStartupScript(sender as Control, GetType(), "ShowResultDialog", script, true);
    }

    protected async Task UpdateSchedule()
    {
        await DoTheAsyncTask();
        InitScheduleCells();
    }

    protected void UpdateCheckedDayFormat()
    {
        var dt1 = DateTime.MinValue;
        var dt2 = DateTime.MinValue;
        if (!DateTime.TryParse(checkedDayLabel.Text, out dt1) || !DateTime.TryParse(scheduleCurrentMonthLabel.Text, out dt2)) return;
        var res = dt1.Subtract(dt2).Days;
        var button = Form.FindControl("MainContentPlaceHolder").FindControl(ScheduleFormStaticValues.Buttons[res + 1]);
        FormatDayButton(button as Button);
        BindData(dt1);
    }

    protected void UpdateButton_Click(object sender, EventArgs e)
    {
        UpdateAllButtonsFormat();
    }

    private void UpdateAllButtonsFormat()
    {
        UpdateCheckedDayFormat();
        FormatRptBranchButtons();
    }

    protected Appointment GetAppointment()
    {
        return ViewState["appData"] as Appointment;
    }

    protected async void ConfirmPhoneButton_Click(object sender, EventArgs e)
    {
        try
        {
            var app = GetAppointment();
            if (app == null)
                throw new SpecialException();
            if (Session[SessionParamName.UrlAddressSession] == null)
                throw new SessionEndedException();

            var itsOk = false;
            var errorMessage = "";
            var script = "";
            var id = 0;
            var checkedBranch = ViewState[branchViewState] == null ? null
                : (ViewState[branchViewState] as List<Branch>).Find(branch => int.TryParse(checkedBranchIDLabel.Text, out id) ? branch.BranchID == id
                : branch.BranchID == Convert.ToInt32(Session[SessionParamName.BranchID]));

            //Найти уникальную цену 
            BaseEntity<UniqCena> response2 = null;
            try
            {
                response2 = await ApiRequest.UniqCena(Session[SessionParamName.UrlAddressSession] as string
                    , new UniqPriceParameters(app.DoctorId, serviceNameLbl.Text, UsersAccessData.ClientID
                    , checkedBranchIDLabel.Text == "" ? Session[SessionParamName.BranchID] as string : checkedBranchIDLabel.Text, Session[SessionParamName.DB_name] as string));
            }
            catch
            { }

            string itog_cena = servicePriceLbl.Text;
            if (response2.Response[0].cena.ToString() != "0")
                itog_cena = "!!ВНИМАНИЕ!! У данного врача отличается цена от общей и составляет: " + response2.Response[0].cena.ToString();

            if (!Page.IsValid)
            {
                script = string.Format("ShowModalDialogFirstStep('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', {7}, '{8}');", checkedBranch.BranchName
                    , checkedBranch.BranchAddress, itog_cena, serviceNameLbl.Text, checkedDayLabel.Text, app.AppTime, app.DoctorFullName, 0, null);
                ScriptManager.RegisterStartupScript(sender as Control, GetType(), "ShowModalDialogFirst", script, true);
                UpdateAllButtonsFormat();
                return;
            }

            if (Session[SessionParamName.UrlAddressSession] == null || Session[SessionParamName.BranchID] == null)
                throw new SessionEndedException();

            var appAmountResponse = await ApiRequest.GetAppAmountRestrictions(Session[SessionParamName.UrlAddressSession] as string
                , new CheckSpamAppParameters(GetPhoneNumber(phoneTextBox.Value), serviceIdLbl.Text, UsersAccessData.ClientID
                , checkedBranchIDLabel.Text == "" ? Session[SessionParamName.BranchID] as string : checkedBranchIDLabel.Text
                , Session[SessionParamName.DB_name] as string));
            if (!CanChangePhone())
            {
                errorMessage = Errors.OverShangingPhoneNumberAmountError;
                InsertLogData(GetPhoneNumber(phoneTextBox.Value), LogMessage.LimitOfAttemptsToChangePhoneNumberExceeded, LogTypes.Warning);
            }
            else if (appAmountResponse == null || !CheckAppointmentsAmount(appAmountResponse.Response[0].CommonAmount, appAmountResponse.Response[0].ServiceAmount))
            {
                errorMessage = appAmountResponse == null ? "Произошла ошибка во время выполнения запроса. Повторите попытку позже или обратитесь к администатору медицинского центра для записи на приём."
                : !CheckCommonAppointmentsAmount(appAmountResponse.Response[0].CommonAmount) ? "Превышен лимит записей без подтверждения для центра."
                : !CheckServiceAppointmentsAmount(appAmountResponse.Response[0].ServiceAmount) ? "Превышен лимит записей без подтверждения для данной услуги."
                : null;
                if (errorMessage != null && appAmountResponse != null)
                    InsertLogData(GetPhoneNumber(phoneTextBox.Value), errorMessage, LogTypes.Warning);
            }
            else
            {
                if (Session[SessionParamName.BranchID] == null || Session[SessionParamName.ClientIPAddr] == null)
                    throw new SessionEndedException();
                var response = await ApiRequest.ConfirmPhoneNumber(new PhoneConfirmationParameters(GetPhoneNumber(phoneTextBox.Value), Session[SessionParamName.CenterID] as string
                    , Session[SessionParamName.ClientIPAddr] as string));
                errorMessage = response == null ? "Произошла ошибка во время выполнения запроса. Повторите попытку позже или обратитесь к администатору медицинского центра для записи на приём."
                    : response.Error ? response.Message
                    : !response.Response.Equals("true") ? response.Response
                    : null;
                if (errorMessage == null && response.Response.Equals("true"))
                {
                    tryAmountLabel.Text = "0";
                    codeWasRequestedLabel.Text = "1";
                    itsOk = true;
                    InsertLogData(GetPhoneNumber(phoneTextBox.Value), LogMessage.SuccessConfirmationCodeRequest, LogTypes.Action);
                }
                else
                {
                    codeWasRequestedLabel.Text = "0";
                    InsertLogData(GetPhoneNumber(phoneTextBox.Value), $"{LogMessage.FaildConfirmationCodeRequest}. {errorMessage}", LogTypes.Warning);
                }
            }
           
            script = errorMessage == null ? string.Format("ShowModalDialogSecondStep('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', {7}, '{8}');", checkedBranch.BranchName, checkedBranch.BranchAddress, servicePriceLbl.Text, serviceNameLbl.Text, checkedDayLabel.Text, app.AppTime, app.DoctorFullName, 0, null)
                : string.Format("ShowModalDialogFirstStep('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', {7}, '{8}');", checkedBranch.BranchName, checkedBranch.BranchAddress, itog_cena, serviceNameLbl.Text, checkedDayLabel.Text, app.AppTime, app.DoctorFullName, 1, errorMessage);
            ScriptManager.RegisterStartupScript(sender as Control, GetType(), errorMessage == null ? "ShowModalDialogSecond" : "ShowModalDialogFirst", script, true);
            /* это затычка для теста
              script = string.Format("ShowModalDialogSecondStep('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', {7}, '{8}');",
    checkedBranch.BranchName, checkedBranch.BranchAddress, servicePriceLbl.Text, serviceNameLbl.Text,
    checkedDayLabel.Text, app.AppTime, app.DoctorFullName, 0, null);

ScriptManager.RegisterStartupScript(sender as Control, GetType(), "ShowModalDialogSecond", script, true);*/


            if (itsOk)
            {
                phoneRfv.Enabled = phoneRev.Enabled = false;
                codeRfv.Enabled = true;
                phoneInfoLabel.Text = GetPhoneTooltip(phoneTextBox.Value);
                errorLabel.Text = string.Empty;
            }

            UpdateAllButtonsFormat();
        }
        catch (SessionEndedException)
        {
            RedirectToSessionEndedPage();
            return;
        }
        catch (SpecialException)
        {
            ShowErrorContainer();
            return;
        }
        catch (Exception exc)
        {
            InsertLogData(GetPhoneNumber(phoneTextBox.Value), exc.Message, LogTypes.Error);
            //RedirectToErrorPage(Errors.UnknownError);
            ShowErrorContainer();
            return;
        }
    }

    protected bool CheckAppointmentsAmount(int commonAmount, int serviceAmount)
    {
        return CheckCommonAppointmentsAmount(commonAmount) && CheckServiceAppointmentsAmount(serviceAmount);
    }

    protected bool CheckCommonAppointmentsAmount(int commonAmount)
    {
        return int.Parse(commonMaxAppLabel.Text) > commonAmount;
    }

    protected bool CheckServiceAppointmentsAmount(int serviceAmount)
    {
        return int.Parse(serviceMaxAppLabel.Text) > serviceAmount;
    }

    protected bool CheckCode(string code)
    {
        return Regex.IsMatch(code, "^\\d{6}$");
    }

    protected bool CanChangePhone()
    {
        try
        {
            HttpCookie cookie = Request.Cookies[cookieSet] ?? new HttpCookie(cookieSet);
            cookie.Expires = DateTime.UtcNow.Add(DateTime.UtcNow.Date.AddDays(1) - DateTime.UtcNow);
            if (cookie[phoneCookie] == null)
            {
                cookie.Values[phoneCookie] = GetPhoneNumber(phoneTextBox.Value);
                Response.Cookies.Add(cookie);
                return true;
            }
            var phones = cookie[phoneCookie].Split('|').ToList();
            if (phones.Count > 2) return false;
            if (phones.Contains(GetPhoneNumber(phoneTextBox.Value))) return true;
            phones.Add(GetPhoneNumber(phoneTextBox.Value));
            cookie.Values[phoneCookie] = string.Join("|", phones);
            Response.Cookies.Set(cookie);
            return true;
        }
        catch (Exception exc)
        {
            InsertLogData(GetPhoneNumber(phoneTextBox.Value), exc.Message, LogTypes.Error);
            //RedirectToErrorPage(Errors.WeNeedCookie);
        }
        return true;
    }

    protected async void CodeConfirmButton_Click(object sender, EventArgs e)
    {
        BaseEntity<AppointmentsRestriction> appAmountResponse;
        Appointment app;
        try
        {
            app = GetAppointment();
            if (app == null)
                throw new SpecialException(Errors.AppointmentAreNullError);
            if (Session[SessionParamName.UrlAddressSession] == null || Session[SessionParamName.BranchID] == null)
                throw new SessionEndedException();
            appAmountResponse = await ApiRequest.GetAppAmountRestrictions(Session[SessionParamName.UrlAddressSession] as string, new CheckSpamAppParameters(GetPhoneNumber(phoneTextBox.Value)
                , serviceIdLbl.Text, UsersAccessData.ClientID, checkedBranchIDLabel.Text == "" ? Session[SessionParamName.BranchID] as string : checkedBranchIDLabel.Text, Session[SessionParamName.DB_name] as string));
        }
        catch (SessionEndedException)
        {
            RedirectToSessionEndedPage();
            return;
        }
        catch (SpecialException)
        {
            ShowErrorContainer();
            return;
        }
        catch (Exception exc)
        {
            InsertLogData(GetPhoneNumber(phoneTextBox.Value), exc.Message, LogTypes.Error);
            //RedirectToErrorPage(Errors.UnknownError);
            ShowErrorContainer();
            return;
        }
        var errorMessage = "";
        var script = "";

        var id = 0;
        var checkedBranch = ViewState[branchViewState] == null ? null : (ViewState[branchViewState] as List<Branch>).Find(branch => int.TryParse(checkedBranchIDLabel.Text, out id) ? branch.BranchID == id : branch.BranchID == Convert.ToInt32(Session[SessionParamName.BranchID]));
        if (codeWasRequestedLabel.Text.Equals("0"))
        {
            errorMessage = "Код подтверждения не был запрошен";
            script = string.Format("ShowModalDialogSecondStep('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', {7}, '{8}');", checkedBranch.BranchName, checkedBranch.BranchAddress, servicePriceLbl.Text, serviceNameLbl.Text, checkedDayLabel.Text, app.AppTime, app.DoctorFullName, 1, errorMessage);
            ScriptManager.RegisterStartupScript(sender as Control, GetType(), "ShowModalDialogSecond", script, true);
        }
        else if (appAmountResponse == null || !CheckAppointmentsAmount(appAmountResponse.Response[0].CommonAmount, appAmountResponse.Response[0].ServiceAmount))
        {
            errorMessage = appAmountResponse == null ? "Произошла ошибка во время выполнения запроса. Повторите попытку позже или обратитесь к администатору медицинского центра для записи на приём."
            : !CheckCommonAppointmentsAmount(appAmountResponse.Response[0].CommonAmount) ? "Превышен лимит записей без подтверждения для центра."
            : !CheckServiceAppointmentsAmount(appAmountResponse.Response[0].ServiceAmount) ? "Превышен лимит записей без подтверждения для данной услуги."
            : null;
            if (errorMessage != null && appAmountResponse != null)
                InsertLogData(GetPhoneNumber(phoneTextBox.Value), errorMessage, LogTypes.Warning);
            script = string.Format("ShowModalDialogSecondStep('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', {7}, '{8}');", checkedBranch.BranchName, checkedBranch.BranchAddress, servicePriceLbl.Text, serviceNameLbl.Text, checkedDayLabel.Text, app.AppTime, app.DoctorFullName, errorMessage == null ? 0 : 1, errorMessage);
            ScriptManager.RegisterStartupScript(sender as Control, GetType(), "ShowModalDialogSecond", script, true);
        }
        else if (!CheckCode(codeTextBox.Text))
        {
            script = string.Format("ShowModalDialogSecondStep('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', {7}, '{8}');", checkedBranch.BranchName, checkedBranch.BranchAddress, servicePriceLbl.Text, serviceNameLbl.Text, checkedDayLabel.Text, app.AppTime, app.DoctorFullName, 1, "Вы ввели неправильный код");
            ScriptManager.RegisterStartupScript(sender as Control, GetType(), "ShowModalDialog", script, true);
        }
        else
            try
            {
                if (int.Parse(tryAmountLabel.Text) > 2)//если вводили уже 3 раза (для тех, кто может сделать видимой форму ввода кода подтверждения)
                {
                    InsertLogData(GetPhoneNumber(phoneTextBox.Value), LogMessage.LimitOfAttemptsToConfirmCodeExceeded, LogTypes.Warning);
                    ShowCodeInputAmountErrorModalDialog(sender as Control, app);
                }
                else
                {
                    if (Session[SessionParamName.BranchID] == null)
                        throw new SessionEndedException();
                    var response = await ApiRequest.SendConfirmationCode(new ConfirmationCodeParameters(codeTextBox.Text, GetPhoneNumber(phoneTextBox.Value), checkedBranchIDLabel.Text == "" ? Session[SessionParamName.BranchID] as string : checkedBranchIDLabel.Text));
                    errorMessage = response == null ? "Произошла ошибка запроса. Повторите попытку позже или обратитесь к администатору медицинского центра для записи на приём."
                        : response.Error ? response.Message
                        : !response.Response.Equals("true") ? response.Response
                        : null;
                    script = errorMessage == null ? string.Format("ShowModalDialogSecondStep('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', {7}, '{8}');", checkedBranch.BranchName, checkedBranch.BranchAddress, servicePriceLbl.Text, serviceNameLbl.Text, checkedDayLabel.Text, app.AppTime, app.DoctorFullName, 0, null)
                        : string.Format("ShowModalDialogSecondStep('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', {7}, '{8}');", checkedBranch.BranchName, checkedBranch.BranchAddress, servicePriceLbl.Text, serviceNameLbl.Text, checkedDayLabel.Text, app.AppTime, app.DoctorFullName, 1, errorMessage);
                    ScriptManager.RegisterStartupScript(sender as Control, GetType(), "ShowModalDialog", script, true);
                    if (errorMessage == null)
                    {
                        InsertLogData(GetPhoneNumber(phoneTextBox.Value), LogMessage.PhoneConfirmed, LogTypes.Action);
                        await ConfirmAppointment(sender);
                    }
                    else if (!response.Error)
                    {

                        if (!response.Response.Equals("true"))
                        {
                            tryAmountLabel.Text = (int.Parse(tryAmountLabel.Text) + 1).ToString();
                            if (int.Parse(tryAmountLabel.Text) > 2)
                            {
                                InsertLogData(GetPhoneNumber(phoneTextBox.Value), LogMessage.LimitOfAttemptsToConfirmCodeExceeded, LogTypes.Warning);
                                ShowCodeInputAmountErrorModalDialog(sender as Control, app);
                            }
                            else
                                InsertLogData(GetPhoneNumber(phoneTextBox.Value), LogMessage.WrongConfirmationCode, LogTypes.Warning);
                        }
                        else
                            codeWasRequestedLabel.Text = "0";
                    }
                }
            }
            catch (SessionEndedException)
            {
                RedirectToSessionEndedPage();
                return;
            }
            catch (Exception exc)
            {
                InsertLogData(GetPhoneNumber(phoneTextBox.Value), exc.Message, LogTypes.Error);
                //RedirectToErrorPage(Errors.UnknownError);
                ShowErrorContainer();
                return;
            }
        UpdateAllButtonsFormat();
    }

    protected void ShowCodeInputAmountErrorModalDialog(Control sender, Appointment app)
    {
        var id = 0;
        var checkedBranch = ViewState[branchViewState] == null ? null : (ViewState[branchViewState] as List<Branch>).Find(branch => int.TryParse(checkedBranchIDLabel.Text, out id) ? branch.BranchID == id : branch.BranchID == Convert.ToInt32(Session[SessionParamName.BranchID]));
        var errorMessage = "Слишком много неудачных попыток ввести код подтверждения.";
        var script = string.Format("ShowModalDialogErrorCodeAmountSecondStep('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}');", checkedBranch.BranchName, checkedBranch.BranchAddress, servicePriceLbl.Text, serviceNameLbl.Text, checkedDayLabel.Text, app.AppTime, app.DoctorFullName, errorMessage);
        ScriptManager.RegisterStartupScript(sender, GetType(), "ShowErrorModalDialog", script, true);
    }

    protected string GetPhoneNumber(string maskedNumber)
    {
        return Regex.Replace(maskedNumber, @"\D", "").Substring(1);
    }

    protected string GetPhoneTooltip(string phone)
    {
        return $"На номер {phone} выслан код подтверждения.";
    }

    protected async void SendCodeAgainButton_Click(object sender, EventArgs e)
    {
        Appointment app;
        BaseEntity<AppointmentsRestriction> appAmountResponse;
        try
        {
            app = GetAppointment();
            if (app == null)
                throw new SpecialException(Errors.AppointmentAreNullError);
            if (Session[SessionParamName.UrlAddressSession] == null || Session[SessionParamName.BranchID] == null)
                throw new SessionEndedException();
            appAmountResponse = await ApiRequest.GetAppAmountRestrictions(Session[SessionParamName.UrlAddressSession] as string, new CheckSpamAppParameters(GetPhoneNumber(phoneTextBox.Value)
                , serviceIdLbl.Text, UsersAccessData.ClientID, checkedBranchIDLabel.Text == "" ? Session[SessionParamName.BranchID] as string : checkedBranchIDLabel.Text
                , Session[SessionParamName.DB_name] as string));
        }
        catch (SessionEndedException)
        {
            RedirectToSessionEndedPage();
            return;
        }
        catch (SpecialException)
        {
            //RedirectToErrorPage(specExc.Message);
            ShowErrorContainer();
            return;
        }
        catch (Exception exc)
        {
            InsertLogData(GetPhoneNumber(phoneTextBox.Value), exc.Message, LogTypes.Error);
            //RedirectToErrorPage(Errors.UnknownError);
            ShowErrorContainer();
            return;
        }
        errorLabel.Text = codeTextBox.Text = "";
        var errorMessage = "";
        var script = "";

        if (appAmountResponse == null || !CheckAppointmentsAmount(appAmountResponse.Response[0].CommonAmount, appAmountResponse.Response[0].ServiceAmount))
        {
            errorMessage = appAmountResponse == null ? "Произошла ошибка во время выполнения запроса. Повторите попытку позже или обратитесь к администатору медицинского центра для записи на приём."
            : !CheckCommonAppointmentsAmount(appAmountResponse.Response[0].CommonAmount) ? "Превышен лимит записей без подтверждения для центра."
            : !CheckServiceAppointmentsAmount(appAmountResponse.Response[0].ServiceAmount) ? "Превышен лимит записей без подтверждения для данной услуги."
            : null;
            if (errorMessage != null && appAmountResponse != null)
                InsertLogData(GetPhoneNumber(phoneTextBox.Value), errorMessage, LogTypes.Warning);
        }
        else
        {
            try
            {
                if (Session[SessionParamName.BranchID] == null || Session[SessionParamName.ClientIPAddr] == null)
                    throw new SessionEndedException();
                var response = await ApiRequest.ConfirmPhoneNumber(new PhoneConfirmationParameters(GetPhoneNumber(phoneTextBox.Value), checkedBranchIDLabel.Text == "" ? Session[SessionParamName.BranchID] as string : checkedBranchIDLabel.Text, Session[SessionParamName.ClientIPAddr] as string));
                errorMessage = response == null ? "Произошла ошибка во время выполнения запроса. Повторите попытку позже или обратитесь к администатору медицинского центра для записи на приём."
                    : response.Error ? response.Message
                    : !response.Response.Equals("true") ? response.Response
                    : null;
                if (errorMessage == null)
                {
                    tryAmountLabel.Text = "0";
                    InsertLogData(GetPhoneNumber(phoneTextBox.Value), LogMessage.ReSuccessConfirmationCodeRequest, LogTypes.Action);
                }
                else if (response != null)
                    InsertLogData(GetPhoneNumber(phoneTextBox.Value), errorMessage, LogTypes.Warning);
            }
            catch (SessionEndedException)
            {
                RedirectToSessionEndedPage();
                return;
            }
            catch (Exception exc)
            {
                InsertLogData(GetPhoneNumber(phoneTextBox.Value), exc.Message, LogTypes.Error);
                //RedirectToErrorPage(Errors.UnknownError);
                ShowErrorContainer();
                return;
            }
        }
        var id = 0;
        var checkedBranch = ViewState[branchViewState] == null ? null : (ViewState[branchViewState] as List<Branch>).Find(branch => int.TryParse(checkedBranchIDLabel.Text, out id) ? branch.BranchID == id : branch.BranchID == Convert.ToInt32(Session[SessionParamName.BranchID]));
        script = errorMessage == null ? string.Format("ShowModalDialogSecondStep('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', {7}, '{8}');", checkedBranch.BranchName, checkedBranch.BranchAddress, servicePriceLbl.Text, serviceNameLbl.Text, checkedDayLabel.Text, app.AppTime, app.DoctorFullName, 0, errorMessage) : string.Format("ShowModalDialogFirstStep('{0}', '{1}', '{2}', '{3}', {4}, '{5}');", serviceNameLbl.Text, checkedDayLabel.Text, app.AppTime, app.DoctorFullName, 1, errorMessage);
        ScriptManager.RegisterStartupScript(sender as Control, GetType(), errorMessage == null ? "ShowModalDialogSecond" : "ShowModalDialogFirst", script, true);
        if (errorMessage == null)
        {
            codeRfv.Enabled = true;
            phoneInfoLabel.Text = GetPhoneTooltip(phoneTextBox.Value);
            errorLabel.Text = string.Empty;
        }

        UpdateAllButtonsFormat();
    }

    protected void RedirectToSessionEndedPage()
    {
        Response.Redirect("SessionEnded.aspx", false);
    }

    protected async void InsertLogData(string phone, string log, string type)
    {
        try
        {
            if (Session[SessionParamName.CenterID] == null || Session[SessionParamName.BranchID] == null || Session[SessionParamName.ClientIPAddr] == null)
                throw new SessionEndedException();
            await ApiRequest.InsertLogData(new InsertLogParameters(Session[SessionParamName.CenterID] as string, checkedBranchIDLabel.Text == "" ? Session[SessionParamName.BranchID] as string : checkedBranchIDLabel.Text, phone, Session[SessionParamName.ClientIPAddr] as string, type), log);
        }
        catch (SessionEndedException)
        {
            RedirectToSessionEndedPage();
            return;
        }
        catch (Exception)
        {
        }
    }

    protected void BackToClientSiteButton_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session[SessionParamName.ClientSiteAddr] == null)
                throw new SessionEndedException();
            Response.Redirect(Session[SessionParamName.ClientSiteAddr] as string, false);
        }
        catch (SessionEndedException)
        {
            RedirectToSessionEndedPage();
            return;
        }
        catch (Exception) { }
    }

    protected void CheckboxRequired_ServerValidate(object source, ServerValidateEventArgs e)
    {
        e.IsValid = personalDataHandlingCheckBox.Checked;
    }

    protected void CallMeCheckboxRequired_ServerValidate(object source, ServerValidateEventArgs e)
    {
        e.IsValid = CallMePersonalDataHandlingCheckBox.Checked;
    }

    protected async void CallMeButton_Click(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsValid)
            {
                ClearDoctorListView();
                return;
            }
            var response = await ApiRequest.AddInWaitingList(Session[SessionParamName.UrlAddressSession] as string, new WaitingListAdditionalParameters(GetPhoneNumber(callMePhoneTextBox.Value), serviceIdLbl.Text, UsersAccessData.ClientID, checkedBranchIDLabel.Text == "" ? Session[SessionParamName.BranchID] as string : checkedBranchIDLabel.Text, Session[SessionParamName.DB_name] as string));
            if (response == null || response.Error)
                throw new Exception();
            InsertLogData(GetPhoneNumber(callMePhoneTextBox.Value), LogMessage.GetCallMeRequestLogText(serviceNameLbl.Text), LogTypes.Action);
            CallMe.Attributes["class"] = "hide";
            SuccessCallMe.Attributes["class"] = "success-call call-me-title show";
            ClearDoctorListView();
        }
        catch
        {
            ClearDoctorListView();
            ShowErrorContainer();
            return;
        }
    }
}