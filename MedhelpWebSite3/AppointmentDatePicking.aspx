﻿<%@ Page Title="Онлайн-запись на приём" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="AppointmentDatePicking.aspx.cs" Inherits="AppointmentDatePicking" Async="true" EnableEventValidation="false"
    ErrorPage="~/ErrorPage.aspx" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="js/Schedule.js"></script>
    <link href="css/Schedule.css" rel="stylesheet" />
    <link href="css/ModalDialog.css" rel="stylesheet" />
    <script src="js/ModalDialog.js"></script>
    <meta name="viewport" content="initial-scale=1, minimum-scale=1">
</asp:Content>
<asp:Content ID="BackButton" ContentPlaceHolderID="BackButton" runat="server">
    <div id="AreaForBackButton" class="area-for-back-button"></div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <asp:ScriptManager ID="PageScriptManager" runat="server"></asp:ScriptManager>
    <asp:Label ID="serviceIdLbl" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="serviceNameLbl" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="servicePriceLbl" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="admissionLbl" runat="server" Text="" Visible="false"></asp:Label>
    <asp:Label ID="commonMaxAppLabel" runat="server" Text="0" Visible="false"></asp:Label>
    <asp:Label ID="serviceMaxAppLabel" runat="server" Text="0" Visible="false"></asp:Label>
    <div class="page-title-oform-zapis" style="">
        <h1>ОФОРМЛЕНИЕ 
            <br />
            ЗАПИСИ НА ПРИЁМ</h1>
    </div>
    <div class="checked-service-container" style="background-color: white;">
        <asp:Label CssClass="post-title" ID="checkedServiceLabel" runat="server" Text="Выбранная услуга:"></asp:Label>
        <span style="font-size: 17px; font-weight: normal; color: #258cd1;" id="CheckedServiceTitle" runat="server"></span>
    </div>
    <div class="schedule-container" style="background-color: rgba(246, 246, 246, 1); flex-grow: 1;">
        <asp:UpdatePanel ID="UpdatePanelScheduleContainer" runat="server">
            <%--<Triggers>
                <asp:PostBackTrigger ControlID="TueButton" /> 
            </Triggers>--%>
            <ContentTemplate>
                <div class="branch-container">
                    <asp:Label CssClass="post-title" ID="availabledBranchesLabel" runat="server" Text="Доступные филиалы:"></asp:Label>
                    <asp:Repeater ID="rptBranchButton" runat="server" OnItemCommand="RptBranchButton_ItemCommand">
                        <ItemTemplate>
                            <asp:Button CausesValidation="False" CssClass="branch-button" ID="branchButton" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "BranchID") %>' runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "BranchName") %>' />
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:Label ID="checkedBranchIDLabel" runat="server" Visible="false"></asp:Label>
                </div>
                <div class="appointment-picking-title" style="font-weight: bold; text-decoration: none; margin-top: 20px;">
                    <asp:Label ID="appointmentPickingLabel" runat="server" Text="ВЫБЕРИТЕ ДАТУ ДЛЯ ЗАПИСИ"></asp:Label>
                </div>
                <asp:UpdatePanel ID="UpdatePanelSchedule" runat="server">
                    <ContentTemplate>
                        <div class="month-container">
                            <asp:Label CssClass="month-label" ID="monthLabel" runat="server"></asp:Label>
                        </div>
                        <div class="schedule">
                            <div class="change-week">
                                <div id="prevButton" runat="server" class="prev-week-button" onclick="javascript:PrevWeek(true); return true;">&#8249</div>
                            </div>
                            <div class="day-of-week">
                                <asp:Label CssClass="day-of-week-label" ID="MonLabel" runat="server" Text="ПН"></asp:Label>
                                <asp:Button DayOfWeekIndex="0" CausesValidation="False" CssClass="day-of-week-button disabled-day" ID="MonButton" runat="server" OnClick="DayOfWeek_Click" />
                            </div>
                            <div class="day-of-week">
                                <asp:Label CssClass="day-of-week-label" ID="TueLabel" runat="server" Text="ВТ"></asp:Label>
                                <asp:Button DayOfWeekIndex="1" CausesValidation="False" CssClass="day-of-week-button disabled-day" ID="TueButton" runat="server" OnClick="DayOfWeek_Click" />
                            </div>
                            <div class="day-of-week">
                                <asp:Label CssClass="day-of-week-label" ID="WedLabel" runat="server" Text="СР"></asp:Label>
                                <asp:Button DayOfWeekIndex="2" CausesValidation="False" CssClass="day-of-week-button disabled-day" ID="WedButton" runat="server" OnClick="DayOfWeek_Click" />
                            </div>
                            <div class="day-of-week">
                                <asp:Label CssClass="day-of-week-label" ID="ThuLabel" runat="server" Text="ЧТ"></asp:Label>
                                <asp:Button DayOfWeekIndex="3" CausesValidation="False" CssClass="day-of-week-button disabled-day" ID="ThuButton" runat="server" OnClick="DayOfWeek_Click" />
                            </div>
                            <div class="day-of-week">
                                <asp:Label CssClass="day-of-week-label" ID="FriLabel" runat="server" Text="ПТ"></asp:Label>
                                <asp:Button DayOfWeekIndex="4" CausesValidation="False" CssClass="day-of-week-button disabled-day" ID="FriButton" runat="server" OnClick="DayOfWeek_Click" />
                            </div>
                            <div class="day-of-week">
                                <asp:Label CssClass="day-of-week-label" ID="SatLabel" runat="server" Text="СБ"></asp:Label>
                                <asp:Button DayOfWeekIndex="5" CausesValidation="False" CssClass="day-of-week-button disabled-day" ID="SatButton" runat="server" OnClick="DayOfWeek_Click" />
                            </div>
                            <div class="day-of-week">
                                <asp:Label CssClass="day-of-week-label" ID="SunLabel" runat="server" Text="ВС"></asp:Label>
                                <asp:Button DayOfWeekIndex="6" CausesValidation="False" CssClass="day-of-week-button disabled-day" ID="SunButton" runat="server" OnClick="DayOfWeek_Click" />
                            </div>
                            <div class="change-week">
                                <div id="nextButton" runat="server" class="next-week-button" onclick="javascript:NextWeek(true); return true;">&#8250</div>
                            </div>

                            <asp:Button CausesValidation="False" ID="nextWeekButton" CssClass="hidden" runat="server" Text="Next" OnClick="NextWeekButton_Click" />
                            <asp:Button CausesValidation="False" ID="prevWeekButton" CssClass="hidden" runat="server" Text="Previous" OnClick="PrevWeekButton_Click" />
                            <asp:Label ID="scheduleCurrentMonthLabel" runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="checkedDayLabel" runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="doctorIDAppTimeLabel" runat="server" Visible="false"></asp:Label>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdatePanel ID="UpdatePanelCallMe" runat="server">
                    <ContentTemplate>
                        <div id="CallMeContainer" class="call-me-container" runat="server">
                            <div id="CallMe" runat="server">
                                <div class="call-me-title">
                                    <span>Не нашли свободное время для записи?</span>
                                </div>
                                <div class="call-me-add-title">
                                    <span>Оставьте свой номер телефона, и мы Вам перезвоним.</span>
                                </div>
                                <div class="dialog-element center">
                                    <div>
                                        <asp:CheckBox ID="CallMePersonalDataHandlingCheckBox" runat="server" Checked="true" CssClass="checkBox-button" />
                                        <span>Я принимаю
                                            <asp:LinkButton CausesValidation="False" ID="CallMeTermsLinkButton" runat="server" OnClientClick="ShowTermsForInfoTransfer(); return false;">условия передачи информации</asp:LinkButton></span>
                                        <asp:CustomValidator ID="TermsCustomValidator" runat="server" Font-Size="0" ErrorMessage="Необходимо согласие на передачу информации"
                                            OnServerValidate="CallMeCheckboxRequired_ServerValidate" ValidationGroup="CallMeValidationGroup"></asp:CustomValidator>
                                    </div>
                                </div>
                                <div class="call-me-phone-place">
                                    <%--<input type="tel" id="Tel1" placeholder="Номер телефона" runat="server" autopostback="false" />--%>
                                    <input id="callMePhoneTextBox" placeholder="Номер телефона" runat="server" autopostback="false" />
                                    <%--  <asp:RequiredFieldValidator ID="CallMePhoneRfv" runat="server"
                                        ErrorMessage="Необходимо ввести номер телефона"
                                        ControlToValidate="callMePhoneTextBox"
                                        Display="Dynamic"
                                        SetFocusOnError="True"
                                        ToolTip="Обязательное поле"
                                        ValidationGroup="CallMeValidationGroup">
                                      <span style="color: red; font-weight: 600;">*</span>
                                    </asp:RequiredFieldValidator>--%>
                                    <asp:RegularExpressionValidator ID="CallMePhoneRev" runat="server"
                                        ErrorMessage="Введите действительный номер телефона"
                                        ControlToValidate="callMePhoneTextBox"
                                        ValidationExpression="\+7\(9\d{2}\)-\d{3}-\d{4}"
                                        ToolTip="Некорректный номер телефона"
                                        Display="Dynamic"
                                        Enabled="true"
                                        ValidationGroup="CallMeValidationGroup">
                                    <span style="color: red; font-weight: 600;">*</span>
                                    </asp:RegularExpressionValidator>
                                </div>
                                <div class="call-me-button">
                                    <asp:Button ID="callMeButton" runat="server" Text="Перезвоните мне" CssClass="simple-button" OnClick="CallMeButton_Click" ValidationGroup="CallMeValidationGroup" />
                                </div>
                                <div style="color: red; font-size: 18px; font-weight: 600;">
                                    <asp:ValidationSummary ID="callMeValidationSummary" runat="server" ValidationGroup="CallMeValidationGroup" />
                                </div>
                            </div>
                            <div id="SuccessCallMe" class="success-call call-me-title" runat="server">
                                <span>Ваша заявка принята.</span>
                                <br />
                                <span>Мы перезвоним Вам в ближайшее время.</span>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div id="errorLabelContainer" runat="server" style="padding: 20px; margin: 0 auto; text-align: center; font-size: 24px; font-weight: 600; color: #f00;" visible="false">
                    <asp:Label ID="errorOnPageLabel" runat="server" Text="Что-то пошло не так..."></asp:Label>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanelDoctorsContainer" runat="server">
            <%--<Triggers>
                <asp:PostBackTrigger ControlID="divRecordButtons" /> 
            </Triggers>--%>
            <ContentTemplate>
                <div class="doctors-container" style="display: flex;flex-wrap: wrap; justify-content: center;" runat="server">
                    <asp:ListView ID="doctorsListView" runat="server" OnItemDataBound="DoctorsListView_ItemDataBound">
                        <ItemTemplate>
                            <div class="list-view" style="display: flex; width: 100%; margin-left: 20px; justify-content: center;" id="dataList">
                                <table  class="table-list-view" style="width: 100%; margin-bottom: 23px; table-layout: fixed;">
                                    <tr>
                                        <td class="row-for-photo" style="height: 100%;">
                                            <div class="photo-container" style="height: 100%; margin-bottom: 20px;">
                                                <img class="photo" style="height: 100%; padding: 0; border-radius: 10px;" src="data:image/png;base64,<%#Eval("StringDoctorPhoto") %>" />
                                            </div>
                                        </td>
                                        <td>
                                            <div class="main-table-info-container" style="padding: 20px 20px 0 0;">
                                                <div id="divDoctorFullName" class="main-table-info" style="display: flex;" runat="server">
                                                    <div style="width: auto; display: flex; align-items: center; margin-right: 10px;">
                                                        <asp:Label CssClass="main-additional-info main-span no-margin" ID="doctorFullName" runat="server" Text='<%#Eval("Full_Name") %>'></asp:Label>
                                                    </div>
                                                    <div>
                                                        <asp:Button ID="informationButton" runat="server" Text="i" CssClass="simple-button information-button" />
                                                    </div>
                                                    <asp:Label ID="doctorIDLabel" runat="server" Text='<%#Eval("Id_Doctor") %>' Visible="false"></asp:Label>
                                                </div>
                                                <div class="doctor-info" id="specialtyDiv" runat="server">
                                                    <p class="doctor-additional-info">Специальность: <span class="additional-span" id="specialtySpan" runat="server"></span></p>
                                                </div>
                                                <div class="doctor-info" id="experienceDiv" runat="server">
                                                    <p class="doctor-additional-info">Стаж: <span class="additional-span" id="experienceSpan" runat="server"></span></p>
                                                </div>
                                                <div class="doctor-info" id="additionalDiv" runat="server">
                                                    <p class="doctor-additional-info mh">Дополнительно: <span class="additional-span-dop-info taj" id="additionalSpan" runat="server"></span></p>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <div class="schedule-time" style="margin-left: 15px;">
                                                <div class="record-buttons" id="divRecordButtons" runat="server">
                                                    <asp:Repeater ID="rptRecordingButton" runat="server" OnItemCommand="RptRecordingButton_ItemCommand">
                                                        <ItemTemplate>
                                                            <asp:Button
                                                                CausesValidation="False"
                                                                CssClass="time-button"
                                                                CommandArgument='<%# Container.DataItem %>'
                                                                Style="min-width: 70px;"
                                                                runat="server"
                                                                Text='<%# Container.DataItem %>' />
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </ItemTemplate>
                    </asp:ListView>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanelModalDialog" runat="server">
            <ContentTemplate>
                <div id="modalDialog" class="modal-dialog" style="border-radius: 50px;" tabindex="0" >
                    <%--Онлайн-запись в центр--%>
                    <div class="d" style="display: flex; justify-content: space-between; align-items: center; min-height: 100px; height: auto; padding: 0 25px;">
                        <h1 class="note-zapis" style="color: white; font-family: 'Manrope'; font-size: 35px; flex-grow: 1; text-align: center; margin: 0;">
                            <asp:Label ID="modalHeaderLabel" runat="server" Text=""></asp:Label>
                        </h1>
                        <button  type="submit"  onclick="javascript:CloseModalDialog(); return false;" id="MainContentPlaceHolder_cancelButton" style="background-color: transparent; font-size: 30px; border: none; color: white; cursor: pointer;">
                            ✕
                        </button>
                    </div>

                    <div id="window" class="window">
                        <div class="dialog-title center" style="background-color: rgba(255, 255, 255, 1); color: black; display: flex; justify-content: center;">
                            <h1 class="dialog-title-note" style="font-weight:600;">ПОДТВЕРЖДЕНИЕ ЗАПИСИ НА ПРИЁМ</h1>
                        </div>
                        <div id="window-style" class="window-style" style="background-color: rgba(255, 255, 255, 1); height: auto;">
                            <div class="dialog-element left" style="margin-left: 10%;display: flex;align-items: flex-start;gap: 5px;">
                                <asp:Label ID="branchLabel" runat="server" CssClass="serviceName" Text="Филиал: "></asp:Label>
                                <asp:Label ID="branchInfoLabel" CssClass="checked-value" runat="server" Text=""></asp:Label>
                            </div>
                            <hr class="dialog-hr" />
                            <div class="dialog-element left" style="margin-left: 10%;display: flex;align-items: flex-start;gap: 5px;">
                                <asp:Label ID="branchAddressLabel" runat="server" CssClass="serviceName" Text="Адрес: "></asp:Label>
                                <asp:Label ID="branchAddressInfoLabel" CssClass="checked-value" runat="server" Text=""></asp:Label>
                            </div>
                            <hr class="dialog-hr" />
                            <div class="dialog-element left" style="margin-left: 10%;display: flex;align-items: flex-start;gap: 5px;">
                                <asp:Label ID="serviceNameLabel" runat="server" CssClass="serviceName" Text="Услуга: "></asp:Label>
                                <asp:Label ID="serviceNameInfoLabel" CssClass="checked-value" runat="server" Text=""></asp:Label>
                            </div>
                            <hr class="dialog-hr" />
                            <div class="dialog-element left" style="margin-left: 10%;display: flex;align-items: flex-start;gap: 5px;">
                                <asp:Label ID="priceLabel" runat="server" CssClass="serviceName" Text="Цена: "></asp:Label>
                                <asp:Label ID="priceInfoLabel" CssClass="checked-value" runat="server" Text=""></asp:Label>
                            </div>
                            <hr class="dialog-hr" />
                            <div class="dialog-element left" style="margin-left: 10%;display: flex;align-items: flex-start;gap: 5px;">
                                <asp:Label ID="dateLabel" runat="server" CssClass="serviceName" Text="Дата приёма: "></asp:Label>
                                <asp:Label CssClass="checked-value" ID="dateInfoLabel" runat="server" Text=""></asp:Label>
                            </div>
                            <hr class="dialog-hr" />
                            <div class="dialog-element left" style="margin-left: 10%;display: flex;align-items: flex-start;gap: 5px;">
                                <asp:Label ID="timeLabel" runat="server" CssClass="serviceName" Text="Время приёма: "></asp:Label>
                                <asp:Label CssClass="checked-value" ID="timeInfoLabel" runat="server" Text=""></asp:Label>
                            </div>
                            <hr class="dialog-hr" />
                            <div class="dialog-element left" style="margin-left: 10%;display: flex;align-items: flex-start;gap: 5px;">
                                <asp:Label ID="doctorNameLabel" runat="server" CssClass="serviceName" Text="Принимающий доктор: "></asp:Label>
                                <asp:Label CssClass="checked-value" ID="doctorNameInfoLabel" runat="server" Text=""></asp:Label>
                            </div>
                            <hr class="dialog-hr" />
                            <%-- Приглашение подтвердить номер телефона --%>
                            <div id="phoneConfirmOffer" class="dialog-element left phoneConfirmOffer" =>
                                <div class="dialog-element center left-offset" >
                                    <div class="checkbox-row">
                                        <asp:CheckBox ID="personalDataHandlingCheckBox" CssClass="checkBox-button" runat="server" Checked="true" />
                                        <span class="checkBox-text">Я принимаю 
            <asp:LinkButton CausesValidation="False" CssClass="termsLinkButton" ID="termsLinkButton" runat="server" OnClientClick="ShowTermsForInfoTransfer(); return false;">условия передачи информации</asp:LinkButton>
                                        </span>
                                        <asp:CustomValidator ID="checkboxRequired" runat="server" Font-Size="0" ErrorMessage="Необходимо согласие на передачу информации"
                                            OnServerValidate="CheckboxRequired_ServerValidate" ValidationGroup="ModalDialogValidationGroup"></asp:CustomValidator>
                                    </div>
                                </div>

                                <div class="num-phone-but" style="">
                                    <%--<input type="tel" id="phoneTextBox" placeholder="Номер телефона" runat="server" autopostback="false" />--%>
                                    <input id="phoneTextBox" placeholder="Номер телефона" class="js-phoneMask" runat="server" autopostback="false" />

                                    <asp:RequiredFieldValidator ID="phoneRfv" runat="server"
                                        ErrorMessage="Необходимо ввести номер телефона"
                                        ControlToValidate="phoneTextBox"
                                        Display="Dynamic"
                                        Enabled="false"
                                        SetFocusOnError="True"
                                        ToolTip="Обязательное поле"
                                        ValidationGroup="ModalDialogValidationGroup">
                                        <%--<span style="color: red; font-weight: 600;">*</span>--%>
                                    </asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="phoneRev" runat="server"
                                        ErrorMessage="Введите действительный номер телефона"
                                        ControlToValidate="phoneTextBox"
                                        ValidationExpression="\+7\(9\d{2}\)-\d{3}-\d{4}"
                                        ToolTip="Некорректный номер телефона"
                                        Display="Dynamic"
                                        Enabled="false"
                                        ValidationGroup="ModalDialogValidationGroup">
                                 <%--   <span style="color: red; font-weight: 600;">*</span>--%>
                                    </asp:RegularExpressionValidator>
                                    <asp:Button ID="confirmPhoneButton" CssClass="simple-button" runat="server" Text="Получить код" OnClick="ConfirmPhoneButton_Click" ValidationGroup="ModalDialogValidationGroup" />

                                </div>
                                <div class="dialog-element left">
                                    <asp:Label ID="confirmPhoneLabel" CssClass="tooltip" runat="server" Text="На номер телефона будет выслан код через мессенджер WHATSAPP(если есть) или через SMS, который нужно ввести для подтверждения"></asp:Label>
                                </div>
                            </div>
                        </div>

                        <%-- Форма подтверждения номера телефона --%>
                        <div id="phoneConfirmForm" class="phoneConfirmForm dialog-element center">
                            <asp:Label ID="codeWasRequestedLabel" runat="server" Text="0" Visible="false"></asp:Label>
                            <asp:Label ID="tryAmountLabel" runat="server" Text="0" Visible="false"></asp:Label>
                            <div class="dialog-element center" style="width: 100%;display: flex;    margin-bottom: 5px;">
                                <asp:TextBox CssClass="codeTextBox" ID="codeTextBox" runat="server" placeHolder="Введите код" AutoCompleteType="Disabled"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="codeRfv" runat="server" ErrorMessage="Необходимо ввести код подтверждения" ControlToValidate="codeTextBox" ToolTip="Обязательное поле" Enabled="false" Display="Static" ValidationGroup="ModalDialogValidationGroup"><span style="color: red; font-weight: 600;">*</span></asp:RequiredFieldValidator>
                                <asp:Button CssClass="simple-button" ID="codeConfirmButton" runat="server" Text="Записаться" OnClick="CodeConfirmButton_Click" ValidationGroup="ModalDialogValidationGroup" />
                            </div>
                            <div class="dialog-element center">
                                <asp:Label ID="phoneInfoLabel" CssClass="tooltip" runat="server"></asp:Label>
                            </div>
                        </div>

                        <div class="dialog-element center error-container" id="errorContainer">
                            <asp:Label ID="errorLabel" CssClass="errorMessage" runat="server"></asp:Label>
                            <div class="dialog-element">
                                <asp:Button CausesValidation="False" ID="sendCodeAgainButton" CssClass="simple-button small-button hide" runat="server" Text="Получить новый код" OnClick="SendCodeAgainButton_Click" />
                            </div>
                        </div>


                        <div class="dialog-element center">
                            <asp:ValidationSummary ID="fieldsValidationSummary" runat="server" ValidationGroup="ModalDialogValidationGroup" />
                        </div>
                    </div>
                    <div class="modal-footer">
                        <span>Сервис онлайн-записи МИС "Медицинский Помощник"</span>
                    </div>

                </div>
                <asp:Label ID="appInfoLabel" runat="server" Visible="false"></asp:Label>
                <div id="resultModalDialog" class="modal-dialog" tabindex="0">
                    <div class="window">
                        <div class="dialog-element center">
                            <div style="width: 64px; height: 64px; margin: auto; padding: 10px;">
                                <img src="Image/itsOK.png" />
                            </div>
                            <asp:Label ID="resultLabel" runat="server" Text="Запись прошла успешно! Обратите внимание, что медицинский центр может Вам перезвонить для уточнения деталей по сделанной записи!" Font-Size="18px"></asp:Label>
                        </div>
                        <div class="dialog-element">
                            <asp:Button ID="backToServicesButton" CssClass="simple-button dialog-button full-width" runat="server" Text="Вернуться к списку услуг" OnClientClick="HideConfirmAppointmentModalDialog();" />
                        </div>
                        <div class="dialog-element">
                            <asp:Button ID="backToClientSiteButton" CssClass="simple-button dialog-button full-width" runat="server" Text="Вернуться на сайт мед.центра" OnClick="BackToClientSiteButton_Click" />
                        </div>
                    </div>
                </div> 
                <!-- Модальное окно -->
                <div id="TermsModalDialog" style="display: none; position: fixed; top: 0; left: 0; right: 0; bottom: 0; background-color: rgba(0,0,0,0.5); justify-content: center; align-items: center; z-index: 1000;">
                    <div id="TermsWindow" style="background-color: white; width: 50%; max-width: 600px; height: 50%; max-height: 400px; border-radius: 15px; box-shadow: 0 4px 15px rgba(0,0,0,0.3); display: flex; flex-direction: column; overflow: hidden; position: relative;">
                        <!-- Заголовок -->
                        <div style="background-color: #258cd1; color: white; padding: 15px 40px 15px 20px; font-size: 1.25rem; font-weight: 600; border-top-left-radius: 15px; border-top-right-radius: 15px; display: flex; align-items: center; justify-content: center; position: relative;">
                            <p style="margin: 0; flex-grow: 1; text-align: center;">Условия передачи информации</p>
                            <button onclick="CloseTermsInfoDialog(); return false;" aria-label="Закрыть модальное окно" style="position: absolute; right: 15px; top: 50%; transform: translateY(-50%); background: transparent; border: none; color: white; font-size: 1.5rem; cursor: pointer; line-height: 1; padding: 0; width: 30px; height: 30px;">
                                ×
                            </button>
                        </div>

                        <!-- Контент с прокруткой -->
                        <div style="padding-right: 20px;padding-left: 20px; overflow-y: auto; flex-grow: 1; font-size: 0.9rem; line-height: 1.4;">
                            <p>Подтверждаю, что введённый мной номер телефона является корректным.</p>
                            <p>Подтверждаю, что данные предоставляются добровольно.</p>
                            <p>Выражаю полное и безоговорочное согласие на использование введённого номера телефона для отправки СМС-сообщений (или whatsapp сообщения) на указанный номер телефона для идентификации номера телефона с помощью проверочного кода и для информирования о сделанной записи через сервис "Онлайн-запись с сайта".</p>
                            <p>Согласие предоставляется ИП Хари Михаил Игоревич (ОГРНИП 320547600078431) мной бессрочно.</p>
                            <p>Я проинформирован о том, что согласие может быть отозвано в любой момент путем направления электронного письма на адрес: medhelp54@yandex.ru.</p>
                        </div>
                    </div>
                </div>
                <div id="modalDialogBackground" class="modal-dialog-background">
                </div>
                <asp:Button CausesValidation="False" ID="updateButton" runat="server" Text="Обновить" CssClass="hidden" OnClick="UpdateButton_Click" />
                <asp:Label ID="isConfirmedLabel" runat="server" Text="0" Visible="false"></asp:Label>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="backToClientSiteButton" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <asp:UpdateProgress runat="server" ID="updateProgress">
        <ProgressTemplate>
            <div style="position: fixed; top: 0; right: 0; bottom: 0; left: 0; background: grey; z-index: 3; display: block; opacity: 0.8; text-align: center">
            </div>
            <div style="color: #fff; background: transparent; text-align: center; position: fixed; width: 240px; height: 240px; left: Calc(50% - 160px); top: Calc(50% - 120px); z-index: 4;">
                <div class="sk-cube-grid">
                    <div class="sk-cube sk-cube1"></div>
                    <div class="sk-cube sk-cube2"></div>
                    <div class="sk-cube sk-cube3"></div>
                    <div class="sk-cube sk-cube4"></div>
                    <div class="sk-cube sk-cube5"></div>
                    <div class="sk-cube sk-cube6"></div>
                    <div class="sk-cube sk-cube7"></div>
                    <div class="sk-cube sk-cube8"></div>
                    <div class="sk-cube sk-cube9"></div>
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <script src="js/jquery.maskedinput.min.js"></script>
    <%--<script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        function EndRequestHandler(sender, args) {
            if (args.get_error() != undefined) {
                args.set_errorHandled(true);
            }
        }
    </script>--%>
</asp:Content>
