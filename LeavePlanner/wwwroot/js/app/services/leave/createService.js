var CreateService = function () {


    var getMaxDate = function (url, employeeId, selectedDateFrom) {

        return new Promise((resolve, reject) => {

            $.ajax({
                url: url,
                type: 'POST',
                data: {
                    employeeId: employeeId,
                    dateFrom: selectedDateFrom
                },
                datatype: 'json',
                headers: {
                    Accept: "application/json; charset=utf-8"
                }
            }).done(function (result) {
                resolve(result);
            }).fail(function (xhr, status, error) {
                console.log(new Error(error));
                reject(xhr.responseText);
            });
        });
    };

    var countWorkingDaysUrl = function (url, selectedDateFrom, selectedDateTo) {

        return new Promise((resolve, reject) => {

            $.ajax({
                url: url,
                type: 'POST',
                data: {
                    dateFrom: selectedDateFrom,
                    dateTo: selectedDateTo
                },
                datatype: 'json',
                headers: {
                    Accept: "application/json; charset=utf-8"
                }
            }).done(function(result) {
                resolve(result);
            }).fail(function(xhr, status, error) {
                console.log(new Error(error));
                reject(xhr.responseText);
            });
        });
    };


    return {
        getMaxDate: getMaxDate,
        countWorkingDaysUrl: countWorkingDaysUrl
    };

}();