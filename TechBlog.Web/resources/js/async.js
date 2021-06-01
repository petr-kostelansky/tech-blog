///--------------------------
/// Async module ------------
//---------------------------

var Async = (function ($) {
    'use strict';

    function loadPartialViews() {
        $('.partial-view').each(function (index, partialView) {
            var url = $(partialView).data("url");
            if (url && url.length > 0) {
                var param = '';
                var reload = $(partialView).data("reload");
                if (reload === 'always') {
                    param = '?time=' + new Date().getTime();
                }
                $(partialView).load(url + param);
            }
        })
    };

    return {
        loadPartialViews: loadPartialViews
    };

})(jQuery);
