//custom sort for sifra (20/2020) column
// Define after datatables scripts
jQuery.extend(jQuery.fn.dataTableExt.oSort, {
    "sifra-pre": function (a) {
        var x = a.split('/');
        return parseInt(x[0]);
    },

    "sifra-asc": function (a, b) {
        return ((a < b) ? -1 : ((a > b) ? 1 : 0));
    },

    "sifra-desc": function (a, b) {
        return ((a < b) ? 1 : ((a > b) ? -1 : 0));
    }
});


//custom sort for date(20.10.2020) column
// Define after datatables scripts
jQuery.extend(jQuery.fn.dataTableExt.oSort, {
    "date-eu-pre": function (date) {
        if (date == null || date == '' || date === "N/A") {
            date = "01.01.1900.";
        }

        //var date = date.replace(" ", "");

        var euDate = date.split('.');

        var year;

        /*year (optional)*/
        if (euDate[2]) {
            year = euDate[2];
        } else {
            year = 0;
        }

        /*month*/
        var month = euDate[1];
        if (month.length == 1) {
            month = 0 + month;
        }

        /*day*/
        var day = euDate[0];
        if (day.length == 1) {
            day = 0 + day;
        }

        return (year + month + day) * 1;
    },

    "date-eu-asc": function (a, b) {
        return ((a < b) ? -1 : ((a > b) ? 1 : 0));
    },

    "date-eu-desc": function (a, b) {
        return ((a < b) ? 1 : ((a > b) ? -1 : 0));
    }
});


//Function for rendering datatable buttons
function renderButton(buttonClass, classSelector, buttonTitleInfo, buttonDataId, fontClass) {
    return "<button class='btn btn-xs " + buttonClass +
        " waves-effect waves-themed " + classSelector + "'" +
        "title='" + buttonTitleInfo + "'" +
        "data-id= '" + buttonDataId +
        "'><i class='far " + fontClass +
        "'></i></button>";
};

// Function for rendering datatable text links with js selector
function renderButtonTextLinkJsSelector(id, title, classSelector, linkName) {
    return "<button data-id='" + id + "' title='" + title + "'class='" + classSelector + " btn btn-link p-0 waves-effect waves-themed'>" + linkName + "</button>";
};

//Function for rendering datatable a href for redirect
function renderHref(redirectUrl, rowId, buttonClass, buttonTitle, fontClass) {
    return "<a href='" + redirectUrl + '/' + rowId +
        "'class='btn btn-xs " + buttonClass +
        " waves-effect waves-themed'" + "title='" + buttonTitle +
        "'><i class='far " + fontClass + "'></i></a>";
};

//Function for rendering datatable a href for redirect new page
function renderHrefBlank(redirectUrl, rowId, buttonClass, buttonTitle,  fontClass) {
    return "<a target='_blank' href='" + redirectUrl + '/' + rowId +
        "'class='btn btn-xs " + buttonClass +
        " waves-effect waves-themed'" + "title='" + buttonTitle +
        "'><i class='far " + fontClass + "'></i></a>";
};

// Function for rendering datatable a href link for redirect
function renderHrefTextLink(redirectUrl, id, linkTitle, linkName) {
    return "<a href='" + redirectUrl + '/' + id + "'title='" + linkTitle + " 'class='p-0 waves-effect waves-themed'>" + linkName + "</a>";
};

//Function for rendering datatable a href for redirect with query string
function renderHrefQueryString(queryString, buttonClass, buttonTitle, fontClass) {
    return "<a href='" + queryString + "'class='btn btn-xs " + buttonClass +
        " waves-effect waves-themed'" + "title='" + buttonTitle +
        "'><i class='far " + fontClass + "'></i></a>";
};

// Function for rendering datatable button for downloading attachments
function renderHrefAttachment(data) {
    return "<a download href='" + data + "' class='btn btn-xs btn-primary waves-effect waves-themed'" +
        " title='Preuzmi'><i class='far fa-download'></i></a>";
};

// Function for rendering datatable badge
function renderBadge(badgeClass, iconClass, badgeText) {
    return "<span class='badge badge-" + badgeClass + " mr-2 ml-2'><i class='far " + iconClass + " mr-2 ml-2'></i> " + badgeText +"</span>";
};

