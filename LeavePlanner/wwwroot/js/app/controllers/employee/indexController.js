var indexController = function (messageService, indexService) {

    // properties
    var tblEmployees;

    // urls
    var urlGetData;
    var urlDetails;

    //#region Urls

    var initializeUrls = function (urlInfo) {
        urlGetData = urlInfo.getDataUrl;
        urlDetails = urlInfo.detailsUrl;
    }

    //#endregion Urls

    //#region select2

    var initializeSelect2 = function () {
        $('.select2').select2();
    };

    //#endregion select2
    
    //#region initializeDatatables

    var initializeDatatables = function() {
        $.fn.dataTable.ext.errMode = 'none';

        tblEmployees = $('#dtEmployees').DataTable({
            responsive: true,
            'processing': true,
            'serverSide': true,
            'iDisplayLength': 10,
            lengthMenu: [10, 25, 50],
            order: [[0, "asc"]],
            "ajax": {
                'url': urlGetData,
                'type': 'POST',
                'dataType': 'JSON',
                error: function(jqXhr, ajaxOptions, thrownError) {
                    messageService.showToastrMessage(jqXHR.responseText, 'error');
                    hideDisplayLoading();
                }
            },
            'columns': [
                { data: 'name' },
                { data: 'email' },
                {
                     data: 'userRole',
                     'render': function(data, type, row, full) {
                         return renderBadge('success', 'fa-tag', data);
                     }
                },
                { data: 'leaveDaysPerYear' },
                { data: 'remainingDaysCurrentYear' },
                { data: 'id'}
            ],
            'columnDefs': [
                { className: 'dt-center', targets: "_all" },
                { orderable: false, targets: "no-sort" },
                {
                    'render': function(data, type, row, full) {
                        return renderHref(urlDetails, data, 'btn-secondary','Details', 'fa-info-circle');
                    },
                    'targets': 5
                },
                { responsivePriority: 1, targets: 0 },
                { responsivePriority: 2, targets: -1 }
            ]
        });
    };

    //#endregion initializeDatatables


    var init = function (urlInfo) {

        initializeUrls(urlInfo);
        initializeSelect2();
        initializeDatatables();
    };


    return {
        init: init
    };

}(MessageService, IndexService);