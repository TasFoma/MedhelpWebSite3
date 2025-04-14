<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="BackButton" ContentPlaceHolderID="BackButton" runat="server"></asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">  
    <%--<a href="https://medhelponline.ru/ServicesPrice.aspx?BusinessID=A6Py2GvhRFsa1" style="padding: 20px; position: absolute;">Туц</a>--%>
    <a href="ServicesPrice.aspx?BusinessID=foai!231xz@af2" style="padding: 20px; position: absolute;">Туц</a>
    <%--<a href="ServicesPrice.aspx?BusinessID=b1TFYQutXH4plOi&SpecID=УЗИ&ClientID=113373&FilialID=1&TokenID=KATAXZ40CC8QH4KS4KBH" style="padding: 20px; position: absolute;">Туц с спец</a>--%>
    <%--<a href="ServicesPrice.aspx?BusinessID=RvzUwSiPKm&SpecID=УЗИ" style="padding: 20px; position: absolute;">Туц с спец</a>--%>
    <div style="padding: 20px; text-align: center;">
        <div style="clear: both; margin: 20px auto">
            <asp:Label ID="commonErrorLabel" CssClass="title center" runat="server" EnableViewState="False" Font-Bold="True" Font-Size="20px"
                Text="Сервис онлайн-записи доступен при переходе с сайта медицинского центра.">
            </asp:Label>
        </div>
        <div style="clear: both; margin: 20px auto">            
            <asp:Label ID="errorStrLabel2" runat="server" Font-Size="16px"
                Text="Пожалуйста, перейдите на страницу записи с сайта Вашего медицинского центра." >
            </asp:Label>
        </div>
    </div>
</asp:Content>