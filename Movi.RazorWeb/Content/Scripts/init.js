(function () {
    $("#gototop").click(function () {
        $("html, body").animate({ scrollTop: 0 }, 120);
    });

    $(".cus-movie-rank-raty").raty({
        path: '/Content/Plugins/Raty/img',
        number: 5,
        halfShow: true,
        readOnly: true,
        score: function () {
            var score = $(this).attr('data-rank');
            return parseFloat(score) / 2;
        }
    });

})();