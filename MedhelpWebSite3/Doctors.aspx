<%@ Page Title="Специалисты" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Doctors.aspx.cs" Inherits="Doctors" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="BackButton" ContentPlaceHolderID="BackButton" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <asp:ScriptManager ID="DoctorsPage_ScriptManager" runat="server"></asp:ScriptManager>
    
    <asp:UpdatePanel ID="Doctors_UpdatePanel" runat="server">
        <ContentTemplate>
            <div class="search-block">
                <asp:TextBox ID="doctorsSearchTextBox" runat="server" placeholder="Поиск по сотрудникам" CssClass="search-textbox"></asp:TextBox>
        <button runat="server" id="doctorsSearchButton" onserverclick="doctorsSearchButton_ServerClick" class="simple-button search-button"><i class="fa fa-search"></i></button>
            </div>
            <asp:DropDownList ID="roleCheckDropDownList" runat="server" CssClass="dropDownList" Font-Bold="True" AutoPostBack="true" OnSelectedIndexChanged="roleCheckDropDownList_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:ListView ID="doctorsListView" runat="server" EnableViewState="false">
                <ItemTemplate>
                    <div class="list-view">
                        <table>
                            <tr>
                                <td rowspan="5">
                                    <div class="photo-container">
                                        <img src="data:image/png;base64,<%#Eval("StringDoctorPhoto") %>" width="100" height="100" />
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="table-info"><a href="#" class="main-additional-info">ФИО: <span class="main-span"><%#Eval("Full_Name") %></span></a></div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="table-info">
                                        <p class="additional-info">Стаж: <span class="additional-span"><%#Eval("Stag") %></span></p>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="table-info">
                                        <p class="additional-info">Специальность: <span class="additional-span"><%#Eval("Specialty") %></span></p>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="table-info">
                                        <p class="additional-info mh">Дополнительно: <span class="additional-span-dop-info"><%#Eval("Dop_Info") %></span></p>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </ItemTemplate>
            </asp:ListView>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="doctorsSearchTextBox" EventName="TextChanged" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

