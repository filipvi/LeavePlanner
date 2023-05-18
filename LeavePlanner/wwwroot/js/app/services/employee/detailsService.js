var DetailsService = function () {

    var deleteConfirmed = function (url, employeeId) {

        return new Promise((resolve, reject) => {

            $.ajax({
                url: url,
                type: 'POST',
                data: { id: employeeId },
                datatype: 'json',
                headers: {
                    Accept: "application/json; charset=utf-8"
                },
            }).done(function (result) {
                resolve(result);
            }).fail(function (xhr, status, error) {
                console.log(new Error(error));
                reject(xhr.responseText);
            });
        });
    };

    return {
        deleteConfirmed: deleteConfirmed
    };

}();