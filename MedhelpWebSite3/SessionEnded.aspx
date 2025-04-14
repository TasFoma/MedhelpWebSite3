<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SessionEnded.aspx.cs" Inherits="SessionEnded" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="BackButton" ContentPlaceHolderID="BackButton" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div style="padding: 20px; text-align: center;">
        <div style="clear: both; margin: 20px auto">
            <asp:Label ID="commonErrorLabel" CssClass="title center" runat="server"
                Text="Произошла ошибка на странице." EnableViewState="False" Font-Bold="True" Font-Size="20px"></asp:Label>
        </div>
        <div style="clear: both; margin: 20px auto">
            <asp:Label ID="errorStrLabel" runat="server" Font-Size="16px"
                Text="Время текущей сессии истекло. Перейдите на страницу записи с сайта Вашего медицинского центра."></asp:Label>
        </div>
    </div>
</asp:Content>

