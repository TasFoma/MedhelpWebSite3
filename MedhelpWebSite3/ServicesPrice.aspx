<%@ Page Title="Онлайн-запись на приём" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ServicesPrice.aspx.cs" Inherits="ServicesPrice" Async="true" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="js/ListView.js"></script>
    <script> 
        function handleClick(element) {
            var hiddenRecord = $(element).find('.hiddenRecord');
            var signUpLinkButton = $(element).find('.my-link-sign-up');

            if (hiddenRecord.find('.comment').text().trim() !== '') {
                hiddenRecord.toggleClass('hidden-record');
            } else {
                if (signUpLinkButton.length > 0) {
                    signUpLinkButton[0].click();
                } else {
                    console.log('Кнопка "Записаться" не найдена');
                }
            }
        }

        $(document).ready(function () {
            if (/ip(hone|od)|ipad|macintosh/i.test(navigator.userAgent)) {
                $('#divBackground').remove();
                $('#divLoader').remove();
                return;
            }
            $("form").on('click', '.simple-button', ShowLoading);
            $("form").on('change', '.dropDownList', ShowLoading);
        });
    </script>
</asp:Content>
<asp:Content ID="BackButton" ContentPlaceHolderID="BackButton" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="Server">
    <div id="backgroundDiv" runat="server" style="background: #fff; height: 100%;">
        <div id="searchBlock" runat="server" class="search-block">
            <div class="inner-search-block">

                <div class="search-fields">
                    <div class="search-hint-block">
                        <asp:Label ID="hintSearchByPriceLabel" runat="server" Text="Воспользуйтесь поиском по прейскуранту"></asp:Label>
                    </div>
                    <div class="search-input-container">
                        <asp:TextBox EnableViewState="false" ID="SpecSearchTextBox" runat="server" placeholder="Поиск по услугам" CssClass="search-textbox"></asp:TextBox>
                        <button enableviewstate="false" runat="server" id="roleSearchButton" onserverclick="RoleSearchButton_ServerClick" class="search-button"><i class="fa fa-search search-icon"></i></button>
                    </div>
                </div>
            </div>
            <div class="inner-search-block">
                <div class="search-hint-block">
                    <asp:Label ID="hintSearchBySpecialtyLabel" runat="server" Text="Или выберите специальность"></asp:Label>
                </div>
                <asp:DropDownList ID="SpecCheckDropDownList" runat="server" CssClass="dropDownList custom-select" Font-Bold="True" AutoPostBack="True" OnSelectedIndexChanged="RoleCheckDropDownList_SelectedIndexChanged" AppendDataBoundItems="True">
                    <asp:ListItem Selected="true" Value="0" CssClass="search-textbox" Text="Выбор специальности"></asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <div id="servicesContainer" runat="server" class="search-block splitter-top" visible="false">
            <div class="inner-search-block">
                <div class="search-hint-block">
                    <asp:Label ID="pickServiceHintLabel" runat="server" Text="Выберите услугу для записи"></asp:Label>
                </div>
                <div class="price-list-view-container">
                    <asp:ListView ID="priceListView" runat="server">
                        <ItemTemplate>
                            <div class="price-list-view" id="price-list-view" onclick="handleClick(this)">
                                <table class="table-price" style="border-collapse: collapse; width: 100%;">
                                    <tr id="main-tr">
                                        <td class="service-name-td">
                                            <h3 class="title"><%# Eval("Title") %></h3>
                                        </td>
                                        <td class="service-price-td" style="padding-right:20px;" data-service-id='<%# Eval("Id_Service") %>'>
                                            <%# Eval("Value", "{0} руб.") %>
                                        </td>
                                    </tr>
                                    <tr id="FirstHiddenRecord" class="hiddenRecord hidden-record">
                                        <td colspan="2" class="appointment-button-container">
                                            <div style="display: flex; justify-content: center; align-items: center;">
                                                <div class="comment" style="width: 100%; padding-left:20px; display: <%# !string.IsNullOrEmpty(Eval("Komment").ToString()) ? "block" : "none" %>;">
                                                    <%# Eval("Komment") %>
                                                </div>
                                                <asp:LinkButton ID="signUpLinkButton" runat="server" CssClass="my-link-sign-up"
                                                    OnClick="AdditSignUpLinkButton_Click"
                                                    CommandArgument='<%# Eval("Id_Service") %>'>Записаться</asp:LinkButton>
                                            </div>
                                        </td>
                                    </tr>

                                </table>
                            </div>
                        </ItemTemplate>
                    </asp:ListView>
                </div>

            </div>
        </div>
        <div id="haventServicesDiv" runat="server" style="padding: 20px; margin: 0 auto; text-align: center; font-size: 16px;" visible="false">
            <asp:Label ID="haventServicesLabel" runat="server" Text="Нет услуг, удовлетворяющих поиску"></asp:Label>
        </div>
        <div id="errorLabelContainer" runat="server" style="padding: 20px; margin: 0 auto; text-align: center; font-size: 20px;" visible="false">
            <asp:Label ID="errorLabel" runat="server" Text="Что-то пошло не так..."></asp:Label>
        </div>
        <asp:Label ID="businessIDLabel" runat="server" Visible="false"></asp:Label>
        <asp:Label ID="hiddenLabel" runat="server" Text="" Visible="false"></asp:Label>
        <div id="divBackground" style="position: fixed; top: 0; right: 0; bottom: 0; left: 0; background: grey; z-index: 3; display: block; opacity: 0.8; text-align: center; display: none;">
        </div>
        <div id="divLoader" style="color: #fff; background: transparent; text-align: center; position: fixed; width: 240px; height: 240px; left: Calc(50% - 120px); top: Calc(50% - 120px); z-index: 4; display: none;">
            <%#Eval("Komment") %>
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
    </div>
</asp:Content>
