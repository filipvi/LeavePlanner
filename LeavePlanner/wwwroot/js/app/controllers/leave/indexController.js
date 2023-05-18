var indexController = function (messageService, indexService) {

    // properties
    var calendar;
    var leaveId;
    var employeeId;

    // urls
    var urlGetInfoModal;
    var urlGetCreateModal;
    var urlCreate;
    var urlPending;
    var urlApprove;
    var urlDecline;
    var urlDelete;

    //#region Urls

    var initializeUrls = function (urlInfo) {
        urlGetInfoModal = urlInfo.getInfoModalUrl;
        urlGetCreateModal = urlInfo.getCreateModalUrl;
        urlCreate = urlInfo.createUrl;
        urlPending = urlInfo.pendingUrl;
        urlApprove = urlInfo.approveUrl;
        urlDecline = urlInfo.declineUrl;
        urlDelete = urlInfo.deleteUrl;
    }

    //#endregion Urls

    //#region select2

    var initializeSelect2 = function () {
        $('.select2').select2();
    };

    //#endregion select2

    //#region initializeHub

    var initializeHub = function () {
        // Establish a connection to the SignalR hub
        var connection = new signalR.HubConnectionBuilder()
            .withUrl("/hubs/refetchEvents")
            .build();

        // Register a client-side method to refresh the calendar events
        connection.on("RefreshEvents", function () {
            calendar.refetchEvents();
        });

        // Start the connection
        connection.start().catch(function (err) {
            return console.error(err.toString());
        });
    };

    //#endregion initializeHub

    //#region approve

    var handleApproveLeaveResult = function (result) {
        if (result.success) {
            messageService.showToastrMessage(result.message, 'success');
        } else {
            messageService.showToastrMessage(result.message, 'error');
        }
    };

    var approveConfirmed = function () {
        $('#leaveModalPlaceholder #leaveDetailsModal').modal('hide');

        indexService.approveLeave(urlApprove, leaveId)
            .then(handleApproveLeaveResult)
            .catch(function (errorMessage) {
                messageService.showToastrMessage(errorMessage, 'error');
                hideDisplayLoading();
            });
    };

    //#endregion approve

    //#region pending

    var handlePendingLeaveResult = function (result) {
        if (result.success) {
            messageService.showToastrMessage(result.message, 'success');
        } else {
            messageService.showToastrMessage(result.message, 'error');
        }
    };

    var pendingConfirmed = function () {
        $('#leaveModalPlaceholder #leaveDetailsModal').modal('hide');

        indexService.pendingLeave(urlPending, leaveId)
            .then(handlePendingLeaveResult)
            .catch(function (errorMessage) {
                messageService.showToastrMessage(errorMessage, 'error');
                hideDisplayLoading();
            });
    };

    //#endregion pending

    //#region decline

    var handleDeclineLeaveResult = function (result) {
        if (result.success) {
            messageService.showToastrMessage(result.message, 'success');
        } else {
            messageService.showToastrMessage(result.message, 'error');
        }
    };

    var declineConfirmed = function () {
        $('#leaveModalPlaceholder #leaveDetailsModal').modal('hide');

        indexService.declineLeave(urlDecline, leaveId)
            .then(handleDeclineLeaveResult)
            .catch(function (errorMessage) {
                messageService.showToastrMessage(errorMessage, 'error');
                hideDisplayLoading();
            });
    };

    //#endregion decline

    //#region delete

    var handleDeleteLeaveResult = function (result) {
        if (result.success) {
            messageService.showToastrMessage(result.message, 'success');
        } else {
            messageService.showToastrMessage(result.message, 'error');
        }
    };

    var deleteConfirmed = function () {
        $('#leaveModalPlaceholder #leaveDetailsModal').modal('hide');

        indexService.deleteConfirmed(urlDelete, leaveId)
            .then(handleDeleteLeaveResult)
            .catch(function (errorMessage) {
                messageService.showToastrMessage(errorMessage, 'error');
                hideDisplayLoading();
            });
    };

    //#endregion delete

    //#region details

    var handleGetLeaveModalResult = function (htmlResult) {
        $('#leaveModalPlaceholder').html(htmlResult);
        $('#leaveModalPlaceholder #leaveDetailsModal').modal('show');
        hideDisplayLoading();

    };
    var getLeaveModalDetails = function () {

        indexService.getLeaveInfoModal(urlGetInfoModal, leaveId)
            .then(handleGetLeaveModalResult)
            .catch(function (errorMessage) {
                messageService.showToastrMessage(errorMessage, 'error');
                hideDisplayLoading();
            });
    };

    //#endregion details

    //#region create leave request

    var processCreateLeaveResult = function(result) {
        if (result.success) {
            messageService.showToastrMessage(result.message, 'success');
        } else {
            messageService.showToastrMessage(result.message, 'error');
        }
    };

    var handleGetCreateModalResult = function(html) {
        $('#createLeavePlaceholder').html(html);
        $('#createLeavePlaceholder #createLeaveModal').modal('show');
        $('#createLeavePlaceholder #createLeaveModal').find('.select2').select2({
            dropdownParent: $('#createLeaveModal')
        });

        revalidateForm();
        markInputsRequired();
        hideDisplayLoading();
    };

    var getCreateModal = function (start, end) {
        displayLoading();

        indexService.getCreateLeaveModal(urlGetCreateModal, employeeId, start, end)
            .then(handleGetCreateModalResult)
            .catch(function(errorMessage) {
                messageService.showToastrMessage(errorMessage, 'error');
                hideDisplayLoading();
            });
    };

    //#endregion create leave request

    //#region format date

    function formatDate(date) {
        const year = date.getFullYear();
        const month = String(date.getMonth() + 1).padStart(2, '0');
        const day = String(date.getDate()).padStart(2, '0');
        return `${year}-${month}-${day}`;
    }
    
    //#endregion format date
    
    //#region Calendar    

    var initializeCalendar = function () {

        calendar = new FullCalendar.Calendar(document.getElementById('calendar'),
            {
                plugins: ['dayGrid', 'list', 'timeGrid', 'interaction', 'bootstrap'],
                themeSystem: 'bootstrap',
                dateAlignment: "month",
                buttonText:
                {
                    today: 'Today',
                    month: 'Month'
                },
                bootstrapFontAwesome:
                {
                    close: 'fa-times',
                    prev: 'fa-chevron-left',
                    next: 'fa-chevron-right',
                    prevYear: 'fa-angle-double-left',
                    nextYear: 'fa-angle-double-right'
                },
                eventTimeFormat:
                {
                    hour: 'numeric',
                    minute: '2-digit'
                },
                navLinks: true,
                headerToolbar: {
                    left: 'prev,next today',
                    center: 'title'
                },
                header:
                {
                    left: 'prev,next today',
                    center: 'title'
                },
                footer:
                {
                    left: '',
                    center: '',
                    right: ''
                },
                initialView: 'dayGridMonth',
                selectable: true,
                eventLimit: true,
                eventSources: [
                    {
                        url: '/Leave/GetLeaves',
                        method: 'POST',
                        extraParams: function () {
                            return {
                                employeeId: employeeId
                            };
                        },
                        failure: function () {
                            messageService.showToastrMessage('Error while fetching leaves!', 'error');
                        }
                    },
                    {
                        url: '/Leave/GetHolidays',
                        method: 'POST',
                        failure: function () {
                            messageService.showToastrMessage('Error while fetching holidays!', 'error');
                        }
                    }
                ],
                eventRender: function (info) {
                    var eventEnd = info.event.end;

                    // get the current time
                    var currentTime = new Date();

                    // check if the event has ended
                    if (eventEnd < currentTime) {
                        info.el.style.opacity = '0.5';
                    }
                },
                eventClick: function (info) {
                    if (info.event.id != "null") {
                        leaveId = info.event.id;
                        getLeaveModalDetails();
                    }
                },
                // Select callback
                select: function (info) {

                    var currentDate = new Date();
                    const formattedDate = formatDate(currentDate);
                    
                    if (info.startStr <= formattedDate) {
                        return;

                    } else {
                        Swal.fire({
                            icon: 'question',
                            title: "<span class='fw-900 text-primary'</span>" + 'Create new leave request',
                            html: "<div class='panelTagCustom'</div>" +
                                'Creating leave request from </br>' +
                                "<span class='fw-900 text-danger'</span>" +
                                info.startStr +
                                ' - ' +
                                "<span class='fw-900 text-danger'</span>" +
                                info.endStr,
                            showCancelButton: true,
                            confirmButtonText: '<i class="far fa-check mr-2"></i> ' + 'Confirm' + '',
                            confirmButtonColor: '#1dc9b7',
                            cancelButtonText: '<i class="far fa-undo mr-2"></i> ' + 'Cancel' + '',
                            cancelButtonColor: '#6c757d'
                        }).then((result) => {
                            if (result.value) {
                                getCreateModal(info.startStr, info.endStr);
                            } else {
                                return;
                            }
                        });
                    }
                },
                unselect: function (info) {
                    $('.js-create').addClass('d-none');
                }
            });

        calendar.render();
        hideDisplayLoading();
    };

    //#endregion Calendar

    var init = function (urlInfo, employeeIdInfo) {

        employeeId = employeeIdInfo;
        displayLoading();
        initializeUrls(urlInfo);
        initializeSelect2();
        initializeHub();
        initializeCalendar();

        $('.js-ddlEmployee').on('change',
            function () {
                employeeId = parseInt($(this).val());

                if (employeeId != null && employeeId != '') {
                    calendar.refetchEvents();
                }
            });

        $('#leaveModalPlaceholder').on('click', '.btn', function () {

            if ($(this).hasClass('js-approve')) {
                approveConfirmed();
            } else if ($(this).hasClass('js-decline')) {
                declineConfirmed();
            } else if ($(this).hasClass('js-pending')) {
                pendingConfirmed();
            } else if ($(this).hasClass('js-delete')) {
                deleteConfirmed();
            }
        });

        $('#formCreateLeave').on('submit',
            function(e) {
                e.preventDefault();

                if ($(this).valid()) {
                    var data = $(this).serialize();
                    $('#createLeavePlaceholder #createLeaveModal').modal('hide');

                    indexService.createLeave(urlCreate, data)
                        .then(processCreateLeaveResult)
                        .catch(function(errorMessage) {
                            messageService.showToastrMessage(errorMessage, 'error');
                        });
                }
            });
    };

    function revalidateForm() {
        var form = $('#formCreateLeave');
        form.removeData('validator');
        form.removeData('unobtrusiveValidation');
        $.validator.unobtrusive.parse(form);
    }

    return {
        init: init
    };

}(MessageService, IndexService);