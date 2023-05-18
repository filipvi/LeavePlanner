var IndexService = function () {
    
    var getLeaveInfoModal = function (url, id) {
        
        return new Promise((resolve, reject) => {
            
            $.ajax({
                url: url,
                type: 'POST',
                data: {id: id},
                datatype: 'html',
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


    var getCreateLeaveModal = function(url, employeeId, start, end) {

        return new Promise((resolve, reject) => {

            $.ajax({
                url: url,
                type: 'POST',
                data: {
                     employeeId: employeeId,
                     startDate: start,
                     endDate: end
                },
                datatype: 'html',
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

    var createLeave = function(url, data) {

        return new Promise((resolve, reject) => {

            $.ajax({
                url: url,
                type: 'POST',
                data: data,
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
    
    
    var pendingLeave = function(url, leaveId) {

        return new Promise((resolve, reject) => {

            $.ajax({
                url: url,
                type: 'POST',
                data: { id: leaveId },
                datatype: 'json',
                headers: {
                    Accept: "application/json; charset=utf-8"
                },
            }).done(function(result) {
                resolve(result);
            }).fail(function(xhr, status, error) {
                console.log(new Error(error));
                reject(xhr.responseText);
            });
        });
    };
    
    var approveLeave = function (url, leaveId) {

        return new Promise((resolve, reject) => {

            $.ajax({
                url: url,
                type: 'POST',
                data: { id: leaveId },
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
    
    var declineLeave = function (url, leaveId) {
        
        return new Promise((resolve, reject) => {
            
            $.ajax({
                url: url,
                type: 'POST',
                data: {id: leaveId},
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
    
    var deleteConfirmed = function (url, leaveId) {
        
        return new Promise((resolve, reject) => {
            
            $.ajax({
                url: url,
                type: 'POST',
                data: {id: leaveId},
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
        getLeaveInfoModal: getLeaveInfoModal,
        getCreateLeaveModal: getCreateLeaveModal,
        createLeave: createLeave,
        pendingLeave: pendingLeave,
        approveLeave: approveLeave,
        declineLeave: declineLeave,
        deleteConfirmed: deleteConfirmed
    };

}();