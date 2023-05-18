////var detailsApplicationController = function (detailsApplicationService, messageService) {

////    var applicationId;
////    var tblAppRoles;
////    var tblEmployees;
////    var applicationStatus;

////    var urlReturn;
////    var urlDetailsApplication;
////    var urlToogleApplicationStatus;
////    var urlGetEditApplicationModal;
////    var urlEditApplication;
////    var urlGetRoleDetails;
////    var urlGetEmployeeDetails;

////    //#region init urls

////    var initializeUrls = function (urlInfo) {

////        urlReturn = urlInfo.returnUrl;
////        urlDetailsApplication = urlInfo.detailsApplicationUrl;
////        urlToogleApplicationStatus = urlInfo.toogleApplicationStatusUrl;
////        urlGetEditApplicationModal = urlInfo.getEditApplicationModalUrl;
////        urlEditApplication = urlInfo.editApplicationUrl;
////        urlGetRoleDetails = urlInfo.getRoleDetailsUrl;
////        urlGetEmployeeDetails = urlInfo.urlGetEmployeeDetails;
////    };

////    //#endregion init urls

////    //#region init datatables

////    var initializeDatatables = function () {

////        $.fn.dataTable.ext.errMode = 'none';
////        tblAppRoles = $('#dtApplicationRoles').dataTable({
////            'iDisplayLength': 10,
////            'lengthMenu': [10, 25, 50],
////            'responsive': true,
////            'language': {
////                'url': datatableLanguageUrl
////            },
////            'columns': [
////                { data: 'name' },
////                { data: 'description' },
////                {
////                    data: 'active',
////                    render: function (data, type, row, full) {
////                        if (data === "True") {
////                            return "<span class='badge badge-success'>Aktivna</span>";
////                        } else {
////                            return "<span class='badge badge-danger'>Neaktivna</span>";
////                        }
////                    }
////                },
////                { data: 'id' }
////            ],
////            'columnDefs': [
////                {
////                    'render': function (data, type, row, full) {
////                        return dataRender(urlGetRoleDetails, data, 'Detalji', 'btn-primary', 'fa-info-circle');
////                    },
////                    'targets': 3
////                },
////                {
////                    "targets": 'no-sort',
////                    "orderable": false
////                },
////                {
////                    "className": 'dt-center',
////                    "targets": "_all"
////                }
////            ],
////            'aaSorting': [],
////            'order': [[0, 'asc']]
////        });

//       tblAppRoles.on('error.dt', function (e, settings, techNote, message) {
//         var splitted = message.split('-');
//         messageService.showToastrMessage(splitted[1], 'error');
//     })
//     .DataTable();

////        tblEmployees = $('#dtEmployees').dataTable({
////            'iDisplayLength': 10,
////            'lengthMenu': [10, 25, 50],
////            'language': {
////                'url': datatableLanguageUrl
////            },
////            'columns': [
////                { data: 'domainName' },
////                { data: 'firstName' },
////                { data: 'lastName' },
////                { data: 'startDate' },
////                { data: 'institute' },
////                { data: 'department' },
////                { data: 'employeeRole' },
////                { data: 'id' }
////            ],
////            'columnDefs': [
////                {
////                    'render': function (data, type, row, full) {
////                        return dataRender(urlGetEmployeeDetails, data, 'Detalji', 'btn-primary', 'fa-info-circle');
////                    },
////                    'targets': 7
////                },
////                {
////                    "type": "date-eu",
////                    "targets": [3]
////                },
////                {
////                    "targets": 'no-sort',
////                    "orderable": false
////                }
////            ],
////            'aaSorting': [],
////            'order': [[0, 'asc']]
////        });


////    };

////    //#endregion init datatables

////    //#region Toogle application status

////    var cancelAction = function () {
////        return;
////    };

////    var processToogleApplicationStatus = function (result) {

////        if (result.success) {

////            messageService.showToastrMessage(result.message, "success", "2500");

////            setTimeout(
////                function () {
////                    messageService.showToastrMessage("Osvježavam podatke...", "info", "2000");
////                }, 2000);

////            setTimeout(
////                function () {
////                    window.location.href = urlDetailsApplication;
////                }, 3000);
////        } else {
////            messageService.showToastrMessage(result.message, "error", "2500");
////        }
////    };

////    var toogleApplicationStatus = function () {

////        detailsApplicationService.toogleApplicationStatus(urlToogleApplicationStatus, applicationId, applicationStatus)
////            .then(processToogleApplicationStatus)
////            .catch(function (errorMessage) {
////                messageService.showToastrMessage(errorMessage, "error", "3000", "toast-top-center");
////            });
////    };

////    var confirmDeactivateApp = function () {
////        messageService.showConfirmationDialogDelete(
////            "Potvrda deaktiviranja aplikacije",
////            "Jeste li sigurni da želite deaktivirati aplikaciju?",
////            "Deaktiviraj",
////            "Odustani",
////            toogleApplicationStatus,
////            cancelAction
////        );
////    };

////    var confirmActivateApp = function () {
////        messageService.showConfirmationDialogSuccess(
////            "Potvrda aktivacije aplikacije",
////            "Jeste li sigurni da želite aktivirati aplikaciju?",
////            "Aktiviraj",
////            "Odustani",
////            toogleApplicationStatus,
////            cancelAction
////        );
////    };

////    //#endregion Toogle application status

////    //#region edit application

////    var processEditApplicationResult = function (result) {

////        if (result.success) {

////            messageService.showToastrMessage(result.message, "success", "2000", "toast-top-center");

////            $('.js-txtName').val(result.application.name);
////            $('.js-txtNameInfo').val(result.application.nameInfo);
////            $('.js-txtUrl').val(result.application.url);
////            $('.js-txtDescription').val(result.application.description);

////        } else {
////            messageService.showAlertMessage(result.message, "error", "4000", "toast-top-center");
////        }
////    }

////    var processGetEditApplicationModalResult = function (result) {

////        $('#editApplicationPlaceholder').html(result);
////        $('#editApplicationPlaceholder #editApplicationModal').modal('show');
////        markInputsRequired();
////    };

////    var handleGetEditApplicationModal = function () {

////        detailsApplicationService.getEditApplicationModal(urlGetEditApplicationModal, applicationId)
////            .then(processGetEditApplicationModalResult)
////            .catch(function (errormessage) {
////                messageService.showToastrMessage(errormessage, "error", "3000", "toast-top-center");
////            });
////    };

////    //#endregion edit application


////    var init = function (urlInfo, appIdInfo) {

////        // Properties
////        applicationId = appIdInfo;

////        // Init urls
////        initializeUrls(urlInfo);

////        // Init datatables
////        initializeDatatables();

////        // Tab defaults
////        $(".js-nav-link").each(function () {
////            $(this).removeClass('active');
////            $(this).prop('aria-selected', false);
////        });
////        $('#tabBasicInfoLink').addClass('active').prop('aria-selected', true);
////        $('.js-tab-pane').each(function () {
////            $(this).removeClass('active').removeClass('show');
////        });
////        $('#tabBasicInfo').addClass('active').addClass('show');


////        // Click events
////        $('.js-deactivateApp').on('click', function () {
////            applicationStatus = false;
////            confirmDeactivateApp();
////        });

////        $('.js-activateApp').on('click', function () {
////            applicationStatus = true;
////            confirmActivateApp();
////        });

////        $('.js-editApplication').on('click', function () {

////            handleGetEditApplicationModal();
////        });

////        $('.js-sazetakEngleski').on('click', function () {

////            handleGetEditApplicationModal();
////        });

////        //Submit edit 
////        $('#formEditApplication').submit(function (e) {

////            e.preventDefault();

////            if ($(this).valid()) {

////                var data = $(this).serialize();

////                $('#editApplicationPlaceholder #editApplicationModal').modal('hide');

////                detailsApplicationService.editApplication(urlEditApplication, data)
////                    .then(processEditApplicationResult)
////                    .catch(messageService.showErrorMessage);
////            }
////        });

////    };

////    return {
////        init: init
////    };

////}(DetailsApplicationService, MessageService);