$(function () {
    (function () {
        var renderContainer = $("div.cus-movie-captions").find("table"),
            movieName = renderContainer.attr("data-movie-name"),
            captionUrl = "http://shooter.cn/api/qhandler.php?t=sub&e=utf-8&q=" + encodeURIComponent(movieName);

        if (movieName) {
            window.shteAPI = {};
            window.shteAPI.results = [];
            window.shteAPI.showResults = function () { };
            Utils.AjaxRequest(captionUrl, null, "GET", "script", function () {

                if (shteAPI.results.length === 0) {
                    var noCaption = "<tr><td class='text-muted'>骚瑞，在 <a href='http://shooter.cn' target='_blank' class='text-danger'>射手网</a> 上面没有找到相关的字幕，你可以自己过去再试试...</td></tr>";
                    renderContainer.append(noCaption);
                    return;
                }
                var results = shteAPI.results,
                    captions = [];
                for (var i = 0, j = results.length; i < j; i++) {
                    var caption = results[i];
                    captions.push("<tr><td><a class='cus-movie-caption' href='http://shooter.cn" + caption.link + "' target='_blank'>" + caption.title + "</a></td></tr>");
                }

                renderContainer.append(captions.join(""));
            }, function () {
                var noCaption = "<tr><td class='text-muted'>骚瑞，获取 <a href='http://shooter.cn' target='_blank'>射手网</a> 上的字幕信息发生错误，你可以自己过去再试试。</td></tr>";
                renderContainer.append(noCaption);
            });
        }
    })();
});
