
var Utils = (function (module) {
    
    //Ajax请求
    var ajaxRequest = function (url, data, method, dataType, onSucceed, onError, onBeforeSend, onCompleted) {

        $.ajaxSetup({
            headers: {

            },
            statusCode: {
                400: function (ex) {

                },
                401: function () {

                }
            }
        });

        $.ajax({
            type: method || "POST",
            url: url,
            data: data,
            dataType: dataType || "json",
            async: true,
            contentType: "application/x-www-form-urlencoded:charset=UTF-8",
            success: function (result) {
                if (onSucceed) {
                    onSucceed.call(this, result);
                }
            },
            error: function (result) {
                if (onError) {
                    onError.call(this, result);
                }
            },
            beforeSend: function () {
                if (onBeforeSend) {
                    beforeSend();
                }
            },
            complete: function (result) {
                if (onCompleted) {
                    onCompleted.call(this, result);
                }
            }
        });
    };

    //获取请求URL参数
    var getUrlParams = function (name) {
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
        var r = window.location.search.substr(1).match(reg);
        if (r != null) {
            return unescape(r[2]);
        }
        return null;
    };

    module.AjaxRequest = ajaxRequest;

    module.GetUrlParam = function (paramName) {
        return getUrlParams(paramName);
    };
   
    return module;
})(Utils || {});