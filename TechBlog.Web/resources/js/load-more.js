///------------------------------
/// Load More Module ------------
///------------------------------

var LoadMore = (function ($) {
    'use strict';

    var defaultSettings = {
        loadButton: '#button-load-more',
        links: '#area-load-more a',
        appendItemsArea: '#area-load-more',
        url: '',
        totalItemsCount: 0,
        loadedItemsCount: 0,
        paggingParam: "page",
        debug: 0
    };

    var defaultUrlParams = { };

    var LoadMore = function (settings, urlParams) {
        this.settings = $.extend(defaultSettings, settings);
        this.urlParams = $.extend(defaultUrlParams, urlParams);
    };

    LoadMore.prototype.init = function () {
        this._logInfo('LoadMore module - init()');

        var self = this;
        this._setButtonVisibility();
        $(this.settings.loadButton).on("click", function loadButtonClick(event) { self._loadMore(); });
        $('body').on("click", this.settings.links, function linkClick(event) { self._onClickLink(); });
    };

    LoadMore.prototype._onClickLink = function () {
        var returnUrl = location.protocol + '//' + location.host + location.pathname + this._createUrlParams(this.urlParams);
        window.history.pushState({ path: returnUrl }, '', returnUrl);
    }

    LoadMore.prototype._createUrlParams = function (obj) {
        var str = "";
        for (var key in obj) {
            if (str != "") {
                str += "&";
            }

            if (obj[key])
                str += key + "=" + encodeURIComponent(obj[key]);
        }

        if (str.length > 1)
            str = "?" + str;

        return str;
    }

    LoadMore.prototype._logInfo = function (message) {
        if (this.settings.debug)
            console.log(message);
    };

    LoadMore.prototype._beforeLoadMore = function () {
        this._logInfo('LoadMore module - beforeLoadMore()');

        this._toggleSpinnerVisibility();
    };

    LoadMore.prototype._loadMore = function () {
        this._logInfo('LoadMore module - loadMore()');

        var self = this;
        this._beforeLoadMore();
        // increase page number
        this.urlParams[this.settings.paggingParam]++;

        this._logInfo('LoadMore module - loading new items from server');
        $.ajax({
            method: "GET",
            url: this.settings.url,
            data: this.urlParams
        }).done(function (result) {
            self._loadMoreSuccess(result);
        }).fail(function () {
            this._logInfo("LoadMore module - error occured when loading new items from server.");
        }).always(function () {
            self._toggleSpinnerVisibility();
        });
    };

    LoadMore.prototype._loadMoreSuccess = function (result) {
        this._logInfo('LoadMore module - loadMoreSuccess()');

        $(result.data).appendTo(this.settings.appendItemsArea);

        this.settings.loadedItemsCount += result.count;
        this._setButtonVisibility();
    };

    LoadMore.prototype._toggleSpinnerVisibility = function () {
        this._logInfo('LoadMore module - toggleSpinnerVisibility()');
        var but = $(this.settings.loadButton);
        but.find('.fa').toggle();
        but.find('span').toggle();
    };

    LoadMore.prototype._setButtonVisibility = function () {
        this._logInfo('LoadMore module - setButtonVisibility()');

        var but = $(this.settings.loadButton);
        if (this.settings.loadedItemsCount >= this.settings.totalItemsCount) {
            but.hide();
        }
        else {
            but.show();
        }
    };

    return LoadMore;

})(jQuery);

///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
