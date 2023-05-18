var displayLoading = function () {
    $('.js-customLoading').removeClass('d-none');
};

var hideDisplayLoading = function () {
    $('.js-customLoading').fadeOut(300,
        function () {
            $('.js-customLoading').addClass('d-none');
            $('.js-customLoading').css("display", "");
        });
};