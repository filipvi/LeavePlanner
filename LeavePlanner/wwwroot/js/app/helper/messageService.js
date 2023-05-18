var MessageService = function () {

    // Clear all toastr messages
    var clearAllMessages = function () {
        toastr.clear();
    }

    // Funckija prikazuje alert poruku sa obavijesti korisniku koja se samostalno zatvara nakon predanog broja milisekundi ili default 4000ms
    // Prima tekst poruke i tip ikone (question, info, success, error, warning) i opcionalno trajanje
    var showToastrMessage = function (message, messageType, duration) {

        var positionClass = "toast-top-full-width";
        var timeOut = 4000;
        var shortCutFunction = "error";

        if (duration != undefined) {
            timeOut = duration;
        }

        if (messageType != undefined) {
            shortCutFunction = messageType;
        }

        toastr.options = {
            "positionClass": positionClass,
            "timeOut": timeOut,
            "closeButton": true,
            "closeMethod": 'fadeOut',
            "closeDuration": 500,
            "newestOnTop": true,
            "progressBar": true,
            "preventDuplicates": true,
            "onclick": null,
            "showDuration": 1000,
            "hideDuration": 500,
            "extendedTimeOut": 2000,
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        };

        $('#toastr-log').text('Command: toastr["' +
            shortCutFunction +
            '"]("' +
            message +
            (message) +
            '")\n\ntoastr.options = ' +
            JSON.stringify(toastr.options, null, 2)
        );

        var $toast = toastr[shortCutFunction](message);
    };


    // Funckija prikazuje modal sa obavijesti korisniku koji ima mogućnost zatvaranja obavijesti. 
    // Prima naslov poruke, tekst poruke i tip ikone (question, info, success, error, warning)
    var showConfirmationDialog = function (messageHeaderTitle, messageContent, iconStyle) {

        Swal.fire({
            title: messageHeaderTitle,
            icon: 'question',
            text: messageContent,
            type: iconStyle,
            confirmButtonColor: '#868e96',
            confirmButtonText: '<i class="far fa-times mr-2"></i> Zatvori obavijest',
        });
    };


    // Funkcija koja se koristi za radnje potvrde poslova (dodavanje novog entiteta i slično)
    // Prima naslov poruke, tekst poruke, tekst za gumb potvrde, tekst za gumb odustani, funkcija za potvrdu i funkciju za odustajanje
    var showConfirmationDialogSuccess = function (title, message, yesButtonText, noButtonText, passedFunction, cancelAction) {

        Swal.fire({
            type: 'success',
            title: "<span class='fw-900'</span>" + title,
            text: message,
            showCancelButton: true,
            confirmButtonText: '<i class="far fa-check mr-2"></i> ' + yesButtonText + '',
            confirmButtonColor: '#1dc9b7',
            cancelButtonText: '<i class="far fa-undo mr-2"></i> ' + noButtonText + '',
            cancelButtonColor: '#6c757d',
        }).then((result) => {
            if (result.value) {
                passedFunction();
            } else {
                cancelAction();
            }
        });
    };


    // Funkcija koja se koristi za radnje potvrde težih radnji (brisanje entiteta, zaključavanje i slično)
    // Prima naslov poruke, tekst poruke, tekst za gumb potvrde, tekst za gumb odustani, funkcija za potvrdu i funkciju za odustajanje
    var showConfirmationDialogDelete = function (title, message, yesButtonText, noButtonText, passedFunction, cancelAction) {

        Swal.fire({
            title: "<span class='fw-900'</span>" + title,
            text: message,
            type: 'question',
            icon: 'warning',
            showCancelButton: true,
            cancelButtonColor: '#6c757d',
            cancelButtonText: '<i class="far fa-undo mr-2"></i> ' + noButtonText + '',
            confirmButtonColor: '#e77070',
            confirmButtonText: '<i class="far fa-check mr-2"></i> ' + yesButtonText + '',
        }).then((result) => {
            if (result.value) {
                passedFunction();
            } else {
                cancelAction();
            }
        });
    };


    // Dismiss temp data messages 
    var dismissAlertMessages = function (duration = 5000) {
        $('.alert', '#infoMsgWrap').delay(duration).fadeOut(1000);
    };


    // Alert message - TEMP DATA
    var showAlertMessage = function (alertLevel = 'info', message = '') {
        var template = $("#msgTemplate").clone().html();

        var html = template
            .replace("{{alert-level}}", alertLevel)
            .replace("{{message}}", message);

        $("#infoMsgWrap").html(html);

        //$("html, body").animate({scrollTop: 0}, 1000);
        scrollToTop(0);

        dismissAlertMessages();
    };


    var cancelAction = function () {
        return "";
    };

    return {
        showToastrMessage: showToastrMessage,
        showConfirmationDialog: showConfirmationDialog,
        showConfirmationDialogSuccess: showConfirmationDialogSuccess,
        showConfirmationDialogDelete: showConfirmationDialogDelete,
        showAlertMessage: showAlertMessage,
        dismissAlertMessages: dismissAlertMessages,
        clearAllMessages: clearAllMessages,
        cancelAction: cancelAction
    };

}();

