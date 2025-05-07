var itsOk = false;
var width = 0.0;
var height = 0.0;

var resultModalDialog;
var modalDialog;
var modalDialogBackground;
var serviceNameLabel;
var dateInfoLabel;
var timeInfoLabel;
var doctorNameInfoLabel;
var phoneConfirmForm;
var phoneConfirmOffer;
var sendCodeAgainButton;
var errorContainer;
var errorLabel;

$(document).ready(function () {
    $(window).resize(function () {
        modalDialog.css("left", "Calc(50% - " + modalDialog.width() / 2 + "px)");
        modalDialog.css("top", "Calc(50% - " + modalDialog.height() / 2 + "px)");
        var termsDialog = $('#TermsModalDialog');
        if (modalDialog.width() <= 480) {
            termsDialog.css('min-width', 'auto');
            termsDialog.css('min-height', 'auto');
        }

        termsDialog.css('top', 'Calc(50% - ' + termsDialog.height() / 2 + 'px)');
        termsDialog.css('left', 'Calc(50% - ' + termsDialog.width() / 2 + 'px)');
        width = modalDialog.width();
        height = modalDialog.height();
    });
});

function InitModalDialogElements() {
    resultModalDialog = $('#resultModalDialog');
    modalDialog = $('#modalDialog');
    modalDialogBackground = $('#modalDialogBackground');
    branchNameInfoLabel = $('#MainContentPlaceHolder_branchInfoLabel');
    branchAddressInfoLabel = $('#MainContentPlaceHolder_branchAddressInfoLabel');
    priceInfoLabel = $('#MainContentPlaceHolder_priceInfoLabel');
    serviceNameLabel = $('#MainContentPlaceHolder_serviceNameInfoLabel');
    dateInfoLabel = $('#MainContentPlaceHolder_dateInfoLabel');
    timeInfoLabel = $('#MainContentPlaceHolder_timeInfoLabel');
    doctorNameInfoLabel = $('#MainContentPlaceHolder_doctorNameInfoLabel');
    phoneConfirmForm = $('#phoneConfirmForm');
    phoneConfirmOffer = $('#phoneConfirmOffer');
    sendCodeAgainButton = $('#MainContentPlaceHolder_sendCodeAgainButton');
    errorContainer = $('#errorContainer');
    errorLabel = $('#MainContentPlaceHolder_errorLabel');
}

//Нужно для создания нового пациента, если будет доступен выбор привязанных к номеру пациентов из списка, и создание нового, если его в списке нет
//function CheckBoxWasChecked() {
//    var checkBox = $('#CheckBox1');
//    if (checkBox.is(':checked'))
//        NewPatientDataFilling();
//    else
//        NewPatientDataClosing();
//}

//Метод для пошаговой записи на приём (ввод номера телефона, подтверждение номера, выбор пациента, привязанного к номеру, или создание нового и сама запись)
//function TestShowModalDialog(serviceName, date, time, doctorName, step) {
//    var modalDialog = $('#modalDialog');
//    var modalDialogBackground = $('#modalDialogBackground');
//    var serviceNameLabel = $('#MainContentPlaceHolder_serviceNameLabel');
//    var dateInfoLabel = $('#MainContentPlaceHolder_dateInfoLabel');
//    var timeInfoLabel = $('#MainContentPlaceHolder_timeInfoLabel');
//    var doctorNameInfoLabel = $('#MainContentPlaceHolder_doctorNameInfoLabel');
//    modalDialogBackground.toggleClass("show-background-modal-dialog");
//    modalDialog.toggleClass("show");
//    modalDialog.focus();

//    serviceNameLabel.text(serviceName);
//    dateInfoLabel.text(date);
//    timeInfoLabel.text(time);
//    doctorNameInfoLabel.text(doctorName);
//    if (step == 1) {
//        console.log("Шаг " + step);
//        $('#phoneConfirmOffer').addClass('show');
//        $('#phoneConfirmForm').removeClass('show');
//        $('#inputPersonalInfo').removeClass('show');
//        $('#checkPatientForm').removeClass('show');
//        width = modalDialog.width();
//        modalDialog.css("left", "Calc(50% - " + width / 2 + "px)");
//        modalDialog.css("top", "Calc(50% - " + modalDialog.height() / 2 + "px)");

//        $('#MainContentPlaceHolder_phoneTextBox').mask("+7(999)-999-9999", { autoclear: false });
//    }
//    else if (step == 2) {
//        console.log("Шаг " + step);
//        $('#phoneConfirmForm').addClass('show');
//        $('#phoneConfirmOffer').removeClass('show');
//        $('#inputPersonalInfo').removeClass('show');
//        $('#checkPatientForm').removeClass('show');
//        modalDialog.css("width", width + "px");
//        modalDialog.css("left", "Calc(50% - " + width / 2 + "px)");
//        modalDialog.css("top", "Calc(50% - " + modalDialog.height() / 2 + "px)");
//    }
//    else if (step == 3) {
//        console.log("Шаг " + step);
//        $('#checkPatientForm').addClass('show');
//        console.log($('#checkPatientForm').width());
//        $('#phoneConfirmForm').removeClass('show');
//        $('#phoneConfirmOffer').removeClass('show');
//        $('#inputPersonalInfo').removeClass('show');
//        modalDialog.css("width", width + "px");
//        modalDialog.css("left", "Calc(50% - " + width / 2 + "px)");
//        modalDialog.css("top", "Calc(50% - " + modalDialog.height() / 2 + "px)");
//    }
//    else if (step == 4) {
//        console.log("Шаг " + step);
//        $('#inputPersonalInfo').addClass('show');
//        $('#phoneConfirmOffer').removeClass('show');
//        $('#phoneConfirmForm').removeClass('show');
//        //$('#checkPatientForm').removeClass('show');
//        modalDialog.css("top", "Calc(50% - " + modalDialog.height() / 2 + "px)");
//    }
//}

//Если поставили галочку, показать заполнение полей для создания нового пациента
//function NewPatientDataFilling() {
//    $('#inputPersonalInfo').addClass('show');
//    $('#modalDialog').css("top", "Calc(50% - " + $('#modalDialog').height() / 2 + "px)");
//}

//Если сняли галочку, скрыть заполнение полей для создания нового пациента
//function NewPatientDataClosing() {
//    $('#inputPersonalInfo').removeClass('show');
//    $('#modalDialog').css("top", "Calc(50% - " + $('#modalDialog').height() / 2 + "px)");
//}

function ShowModalDialogFirstStep(branchName, branchAddress, price, serviceName, date, time, doctorName, itsError, errorMessage) {
    if (itsError == 0) $('#MainContentPlaceHolder_personalDataHandlingCheckBox').prop('checked', true);
    modalDialogBackground.bind('click', HideConfirmAppointmentModalDialog);
    modalDialogBackground.addClass("show-background-modal-dialog");
    modalDialog.addClass("show");
    modalDialog.focus();
    //$(window).bind('scroll', ScrollHandler);

    branchNameInfoLabel.text(branchName);
    branchAddressInfoLabel.text(branchAddress);
    priceInfoLabel.text(price + ' руб.');
    serviceNameLabel.text(serviceName);
    dateInfoLabel.text(date);
    timeInfoLabel.text(time);
    doctorNameInfoLabel.text(doctorName);
    if (itsError == 1) {
        errorContainer.addClass('show');
        errorLabel.removeClass('infoMessage').addClass('errorMessage');
        errorLabel.text(errorMessage);
    }
    else {
        errorContainer.removeClass('show');
    }
    phoneConfirmOffer.addClass('show');
    phoneConfirmForm.removeClass('show');
    width = modalDialog.width();
    height = modalDialog.height();
    //if ($(window).width() >= 480)
    //    modalDialog.css("left", "Calc(50% - " + modalDialog.width() / 2 + "px)");
    //else {
    //    modalDialog.width(480);
    //    width = modalDialog.width();
    //    modalDialog.css("left", 240 - modalDialog.width() / 2 + "px");
    //}
    modalDialog.css("left", "Calc(50% - " + width / 2 + "px)");
    modalDialog.css("top", "Calc(50% - " + height / 2 + "px)");

    $('#MainContentPlaceHolder_phoneTextBox').mask("+7(999)-999-9999", { autoclear: false }).focus();

    //itemOffset = $(modalDialog).offset().top;
}

function ShowModalDialogSecondStep(branchName, branchAddress, price, serviceName, date, time, doctorName, itsError, errorMessage) {
    modalDialogBackground.bind('click', HideConfirmAppointmentModalDialog);
    modalDialogBackground.addClass("show-background-modal-dialog");
    modalDialog.addClass("show");
    modalDialog.focus();
    //$(window).bind('scroll', ScrollHandler);

    branchNameInfoLabel.text(branchName);
    branchAddressInfoLabel.text(branchAddress);
    priceInfoLabel.text(price + ' руб.');
    serviceNameLabel.text(serviceName);
    dateInfoLabel.text(date);
    timeInfoLabel.text(time);
    doctorNameInfoLabel.text(doctorName);
    if (itsError == 1) {
        errorContainer.addClass('show');
        errorLabel.removeClass('infoMessage').addClass('errorMessage');
        errorLabel.text(errorMessage);
    }
    else {
        errorContainer.removeClass('show');
    }
    $(sendCodeAgainButton).addClass('hide');
    phoneConfirmForm.addClass('show');
    phoneConfirmOffer.removeClass('show');
    modalDialog.css("width", width + "px");
    //if ($(window).width() >= 480)
    //    modalDialog.css("left", "Calc(50% - " + modalDialog.width() / 2 + "px)");
    //else {
    //    modalDialog.width(480);
    //    width = modalDialog.width();
    //    modalDialog.css("left", 240 - modalDialog.width() / 2 + "px");
    //}
    modalDialog.css("left", "Calc(50% - " + width / 2 + "px)");
    modalDialog.css("top", "Calc(50% - " + height / 2 + "px)");
    $('#MainContentPlaceHolder_codeTextBox').mask("************", { autoclear: false, placeholder: "" }).focus();
}

function ShowModalDialogErrorCodeAmountSecondStep(serviceName, date, time, doctorName, errorMessage) {
    modalDialogBackground.bind('click', HideConfirmAppointmentModalDialog);
    modalDialogBackground.addClass("show-background-modal-dialog");
    modalDialog.addClass("show");
    modalDialog.focus();

    //$(window).bind('scroll', ScrollHandler);

    serviceNameLabel.text(serviceName);
    dateInfoLabel.text(date);
    timeInfoLabel.text(time);
    doctorNameInfoLabel.text(doctorName);
    errorContainer.addClass('show');
    errorLabel.removeClass('infoMessage').addClass('errorMessage');
    errorLabel.text(errorMessage);
    sendCodeAgainButton.removeClass('hide');
    phoneConfirmForm.removeClass('show');
    phoneConfirmOffer.removeClass('show');
    modalDialog.css("width", width + "px");
    //if ($(window).width() >= 480)
    //    modalDialog.css("left", "Calc(50% - " + modalDialog.width() / 2 + "px)");
    //else {
    //    modalDialog.width(480);
    //    width = modalDialog.width();
    //    modalDialog.css("left", 240 - modalDialog.width() / 2 + "px");
    //}
    modalDialog.css("left", "Calc(50% - " + modalDialog.width() / 2 + "px)");
    modalDialog.css("top", "Calc(50% - " + modalDialog.height() / 2 + "px)");
    $('#MainContentPlaceHolder_codeTextBox').focus();
}


//function ShowModalDialog(serviceName, date, time, doctorName, step, itsError, phoneMessage, codeMessage) {
//    modalDialogBackground.addClass("show-background-modal-dialog");
//    modalDialog.addClass("show");
//    modalDialog.focus();

//    serviceNameLabel.text(serviceName);
//    dateInfoLabel.text(date);
//    timeInfoLabel.text(time);
//    doctorNameInfoLabel.text(doctorName);
//    if (itsError == 1) {
//        if (phoneMessage) {
//            errorPhoneLabel.removeClass('infoMessage').addClass('errorMessage');
//            errorPhoneLabel.text(phoneMessage);
//        }
//        else if (codeMessage) {
//            errorCodeLabel.removeClass('infoMessage').addClass('errorMessage');
//            errorCodeLabel.text(codeMessage);
//        }
//    }

//    if (step == 1) {
//        phoneConfirmOffer.addClass('show');
//        phoneConfirmForm.removeClass('show');
//        width = modalDialog.width();
//        modalDialog.css("left", "Calc(50% - " + modalDialog.width() / 2 + "px)");
//        modalDialog.css("top", "Calc(50% - " + modalDialog.height() / 2 + "px)");

//        $('#MainContentPlaceHolder_phoneTextBox').mask("+7(999)-999-9999", { autoclear: false });
//    }
//    else if (step == 2) {
//        phoneConfirmForm.addClass('show');
//        phoneConfirmOffer.removeClass('show');
//        modalDialog.css("width", width + "px");
//        modalDialog.css("left", "Calc(50% - " + width / 2 + "px)");
//        modalDialog.css("top", "Calc(50% - " + modalDialog.height() / 2 + "px)");
//    }
//}

function CloseModalDialog() {
    resultModalDialog.removeClass('show');
    modalDialogBackground.removeClass('show-background-modal-dialog');
    modalDialog.removeClass('show');

    var updateButton = $('#MainContentPlaceHolder_updateButton');
    if (updateButton != null)
        updateButton.click();
}

function ShowConfirmAppointmentModalDialog(ok, message) {
    modalDialog.removeClass("show");
    modalDialogBackground.addClass("show-background-modal-dialog");
    resultModalDialog.addClass("show");
    resultModalDialog.css('text-align', 'center');
    resultModalDialog.focus();
    //$(window).unbind('scroll');

    $('#MainContentPlaceHolder_resultLabel').text(message);
    if (ok == 0) {
        var backToServicesButton = $('#MainContentPlaceHolder_backToServicesButton');
        backToServicesButton.addClass("error-dialog-button").addClass("hover-border-none");
        backToServicesButton.prop('value', 'OK');
        $('#MainContentPlaceHolder_backToClientSiteButton').css('display', 'none');
    }
    else
        itsOk = true;
    $('.window').css('padding-bottom', '0');
    resultModalDialog.css("left", "Calc(50% - " + resultModalDialog.width() / 2 + "px)");
    resultModalDialog.css("top", "Calc(50% - " + resultModalDialog.height() / 2 + "px)");
}

function HideConfirmAppointmentModalDialog() {
    if (itsOk)
        setTimeout("window.history.go(-1)", 1000);
    else
        CloseModalDialog();
}

function ShowConfirmPhone() {
    phoneConfirmOffer.css('display', 'none');
    phoneConfirmForm.css('display', 'block');
}
/*
function ShowTermsForInfoTransfer() {
    console.log('ShowTermsForInfoTransfer()');
    var termsDialog = $('#TermsModalDialog');
    if (modalDialog.width() > 480) {
        termsDialog.css('min-width', width + 'px');
        termsDialog.css('min-height', height + 'px');
        $('#TermsWindow').css('width', 'Calc(100% - 4px)');
        $('#TermsWindow').css('min-height', height + 'px');
    }
    //termsDialog.width(width);
    //termsDialog.height(height);
    termsDialog.css('top', 'Calc(50% - ' + termsDialog.height() / 2 + 'px)');
    termsDialog.css('left', 'Calc(50% - ' + termsDialog.width() / 2 + 'px)');
    modalDialogBackground.unbind();
    termsDialog.addClass('show');
}

function CloseTermsInfoDialog() {
    modalDialogBackground.bind('click', HideConfirmAppointmentModalDialog);
    var termsDialog = $('#TermsModalDialog');
    termsDialog.removeClass('show');
}
*/

function ShowTermsForInfoTransfer() {
    var modal = document.getElementById('TermsModalDialog');
    if (modal) {
        modal.style.display = 'flex';
    }
}
// Функция скрытия модального окна
function CloseTermsInfoDialog() {
    console.log("CloseTermsInfoDialog");
    var modal = document.getElementById('TermsModalDialog');
    if (modal) {
        modal.style.display = 'none';
    }
}
