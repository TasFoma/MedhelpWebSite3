<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ServerNotAvailable.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="BackButton" ContentPlaceHolderID="BackButton" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">
    <div style="border: 2px solid #7698f6; padding: 20px;">
        <asp:Label ID="errorLabel" CssClass="title center" runat="server" 
            Text="Не удалось получить информацию с сервера. Попробуйте снова перейти на страницу записи с сайта медицинского учреждения или обратитесь к администратору медицинского центра для записи на приём."></asp:Label>
    </div>
</asp:Content>