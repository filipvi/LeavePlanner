var createController = function (messageService, createService) {

    // properties
    var selectedDateFrom;
    var availableDays;
    var employeeId;
    var minDate;
    var maxDate;

    // urls
    var urlGetPrepareMaxDate;
    var urlGetCountWorkingDays;

    //#region Urls

    var initializeUrls = function (urlInfo) {
        urlGetPrepareMaxDate = urlInfo.getPrepareMaxDateUrl;
        urlGetCountWorkingDays = urlInfo.getCountWorkingDaysUrl;
    }

    //#endregion Urls

    //#region select2

    var initializeSelect2 = function () {
        $('.select2').select2();
    };

    //#endregion select2

    //#region Date pickers

    var initializeDatePicker = function (yearInfo) {

        $('.js-dateFrom').datepicker({
            language: 'hr-HR',
            format: 'dd.mm.yyyy',
            maxViewMode: 'months',
            autoclose: true,
            todayHighlight: true,
            orientation: 'bottom',
            startDate: minDate,
            endDate: maxDate,
            clearBtn: true
        });

        //$('.js-dateTo').datepicker({
        //    language: 'hr-HR',
        //    format: 'dd.mm.yyyy',
        //    maxViewMode: 'months',
        //    autoclose: true,
        //    todayHighlight: true,
        //    orientation: 'bottom',
        //    clearBtn: true
        //});
    };

    var initializeDatePickerDateTo = function(maxDate) {
        $('.js-dateTo').datepicker({
            language: 'hr-HR',
            format: 'dd.mm.yyyy',
            maxViewMode: 'months',
            autoclose: true,
            todayHighlight: true,
            orientation: 'bottom',
            startDate: selectedDateFrom,
            endDate: maxDate,
            clearBtn: true
        });
    };

    //#endregion Datepickers

    //#region countWorkingDaysUrl

    var handleCountWorkingDaysResult = function(result) {
        if (result.success) {
            $('#workingDaysUsed').text(result.selectedWorkingDays);
            if (result.selectedWorkingDays > availableDays) {
                messageService.showToastrMessage('You exceeded limit of available days', 'error');
                $('#workingDaysCard').addClass('bg-danger-100');
            } else {
                $('#workingDaysCard').removeClass('bg-danger-100');
            }

        } else {
            messageService.showToastrMessage(result.message, 'error');
        }
    };

    var countWorkingDaysUrl = function (selectedDateFrom, selectedDateTo) {

        createService.countWorkingDaysUrl(urlGetCountWorkingDays, selectedDateFrom, selectedDateTo)
            .then(handleCountWorkingDaysResult)
            .catch(function(errorMessage) {
                messageService.showToastrMessage(errorMessage, 'error');
                hideDisplayLoading();
            });
    };

    //#endregion countWorkingDaysUrl

    //#region getPrepareMaxDate

    var handleGetMaxDateResult = function (result) {
        if (result.success) {
            initializeDatePickerDateTo(result.maxDate);
        } else {
            messageService.showToastrMessage(result.message, 'error');
        }
    };

    var getPrepareMaxDate = function (selectedDateFrom) {

        createService.getMaxDate(urlGetPrepareMaxDate, employeeId, selectedDateFrom)
            .then(handleGetMaxDateResult)
            .catch(function (errorMessage) {
                messageService.showToastrMessage(errorMessage, 'error');
                hideDisplayLoading();
            });
    };

    //#endregion getPrepareMaxDate



    var init = function (urlInfo, employeeIdInfo, availableDaysInfo, minDateInfo, maxDateInfo) {

        availableDays = availableDaysInfo;
        employeeId = employeeIdInfo;
        minDate = minDateInfo;
        maxDate = maxDateInfo;

        initializeUrls(urlInfo);
        initializeSelect2();
        initializeDatePicker();

        $('.js-dateFrom').on('change',
            function () {
                selectedDateFrom = $(this).val();
                getPrepareMaxDate(selectedDateFrom);
            });

        $('.js-dateTo').on('change',
            function() {
                selectedDateFrom = $('.js-dateFrom').val();
                var selectedDateTo = $(this).val();
                countWorkingDaysUrl(selectedDateFrom, selectedDateTo);
            });

        $('.js-dateTo').on('change',
            function () {
                var selectedDateTo = $(this).val();
                $('.js-dateFrom').datepicker('setEndDate', selectedDateTo);
            });

    };


    return {
        init: init
    };

}(MessageService, CreateService);