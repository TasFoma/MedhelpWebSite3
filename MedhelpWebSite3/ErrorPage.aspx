<%@ Page Title="Ошибка на странице" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ErrorPage.aspx.cs" Inherits="ErrorPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="BackButton" ContentPlaceHolderID="BackButton" runat="server"></asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div style="padding: 20px; text-align: center;">
        <div style="clear: both; margin: 20px auto">
            <asp:Label ID="commonErrorLabel" CssClass="title center" runat="server"
                Text="Произошла ошибка на странице." EnableViewState="False" Font-Bold="True" Font-Size="20px"></asp:Label>
        </div>
        <div style="clear: both; margin: 20px auto">
            <asp:Label ID="errorStrLabel" runat="server" Text="Произошла непредвиденная ошибка. Попробуйте снова перейти на страницу записи с сайта медицинского учреждения или обратитесь к администратору медицинского центра для записи на приём." Font-Size="16px"></asp:Label>
        </div>
    </div>
</asp:Content>

