var home = (function ($) {
    'use strict';

    return {
        init: function () {
            $("#log").click(function (e) {
                e.preventDefault();
                location.href = $(this).data("url");
            });
        }
    };
} (jQuery));

$(function () {
    'use strict';
    home.init();
});