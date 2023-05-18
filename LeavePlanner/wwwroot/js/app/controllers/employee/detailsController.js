var detailsController = function (messageService, detailsService) {

    // properties
    var employeeId;
    var tblLeaves;

    // urls
    var urlDelete;
    var urlIndex;

    //#region Urls

    var initializeUrls = function (urlInfo) {
        urlIndex = urlInfo.indexUrl;
        urlDelete = urlInfo.deleteUrl;
    }

    //#endregion Urls

    //#region initializeDatatables

    var initializeDatatables = function() {
        tblLeaves = $('#dtLeaves').DataTable({
            responsive: true,
            lengthMenu: [10, 25, 50],
            order: [[1, "desc"]],
            'columns': [
                { data: 'dateFrom' },
                { data: 'dateTo' },
                { data: 'workingDaysUsed' },
                { data: 'status' }
            ],
            'columnDefs': [
                { type: 'date-eu', targets: 0 },
                { type: 'date-eu', targets: 1 },
                { className: 'dt-center', targets: "_all" },
                { responsivePriority: 1, targets: 0 },
                { responsivePriority: 2, targets: 1 }
            ]
        });

        $('#dtLeaves')
            .on('error.dt',
                function(e, settings, techNote, message) {
                    var splitted = message.split('-');
                    messageService.showToastrMessage(splitted[1], 'danger');
                })
            .DataTable();
    }

    //#endregion initializeDatatables

    //#region delete bid

    var handleDeleteResult = function(result) {
        if (result.success) {
            window.location.href = urlIndex;
        } else {
            messageService.showToastrMessage(result.message, 'error');
        }

        hideDisplayLoading();
    };

    var deleteConfirmed = function() {
        displayLoading();

        detailsService.deleteConfirmed(urlDelete, employeeId)
            .then(handleDeleteResult)
            .catch(function(errorMessage) {
                messageService.showToastrMessage(errorMessage, 'error');
                hideDisplayLoading();
            });
    };

    var confirmDelete = function() {

        messageService.showConfirmationDialogDelete("Delete employee?",
            "You will also delete all user roles and leave requests!",
            "Delete",
            "Cancel",
            deleteConfirmed,
            messageService.cancelAction);
    };

    //#endregion delete bid


    var init = function (urlInfo, employeeIdInfo) {

        employeeId = employeeIdInfo;
        initializeUrls(urlInfo);
        initializeDatatables();

        $('.js-delete').on('click',
            function() {
                confirmDelete();
            });
    };


    return {
        init: init
    };

}(MessageService, DetailsService);