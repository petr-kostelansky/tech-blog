///-------------------------
/// Common module ----------
///-------------------------

var Common = (function ($) {
    'use strict';

    // Reading cookie
    var getCookie = function getCookie(name) {
        var value = "; " + document.cookie;
        var parts = value.split("; " + name + "=");
        if (parts.length == 2) return parts.pop().split(";").shift();
    };

    return {
        getCookie: getCookie
    };

})(jQuery);

