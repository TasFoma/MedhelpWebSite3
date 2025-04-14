<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="NotFound.aspx.cs" Inherits="NotFound" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="BackButton" ContentPlaceHolderID="BackButton" runat="server"></asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">
    <div style="padding: 20px; text-align: center;">
        <asp:Label ID="reloginLabel" CssClass="title center" runat="server" 
            Text="Ошибка 404 - страница не найдена. Среда ASP.NET не нашла указанную страницу."></asp:Label>
    </div>
</asp:Content>

