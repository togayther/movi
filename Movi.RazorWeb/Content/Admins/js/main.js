
var templates = {

};


var system = function () {

    //顶部动画
    var runAnimations = function () {

        $('.header-btns > div').on('show.bs.dropdown', function () {
            $(this).children('.dropdown-menu').addClass('animated animated-short flipInY');
        });
        $('.header-btns > div').on('hide.bs.dropdown', function () {
            $(this).children('.dropdown-menu').removeClass('animated flipInY');
        });

    };

    //菜单控制
    var runSideMenu = function () {

        var toggleSideMenu = function () {
            if ($('body').hasClass('mobile-viewport')) {
                $('body').toggleClass('sidebar-persist');
            } else {
                $('body').toggleClass('sidebar-collapsed');
            }
        };

        var sidebarCheck = function () {
            if ($(window).width() < 1200) {
                $('body').addClass('mobile-viewport').removeClass('sidebar-collapsed');
            } else {
                $('body').removeClass('mobile-viewport sidebar-persist');
            }
        };

        $(".sidebar-toggle").click(toggleSideMenu);
        $(document).ready(sidebarCheck);
        $(window).resize(sidebarCheck);

        $('#sidebar-menu .sidebar-nav a.accordion-toggle').click(function (e) {
            e.preventDefault();
            $('a.accordion-toggle.menu-open').next('.sub-nav').slideUp('fast', 'swing', function () {
                $(this).prev().removeClass('menu-open');
            });
            if (!$(this).hasClass('menu-open')) {
                $(this).next('.sub-nav').slideToggle('fast', 'swing', function () {
                    $(this).prev().toggleClass('menu-open');
                });
            }
        });

        (function () {
            var slidebar = $("#sidebar-menu");
            var thisController = slidebar.attr("data-controller");
            var thisAction = slidebar.attr("data-action");
            if (thisController && thisAction) {
                var thisHref = thisController.toLowerCase() + "/" + thisAction.toLowerCase();
                var thisLink = slidebar.find("a[href*='" + thisHref + "']");
                if (thisLink) {
                    thisLink.parent("li").addClass("active").end()
                        .parents("ul.sub-nav").prev("a").addClass("menu-open");
                }
            }
        })();
    };

    //媒体列表控制（Admin）
    var mediaListControl = function () {
        $("a.cus-media-list").hover(function () {
            var $thisEntity = $(this);
            var $thisCheckContainer = $thisEntity.find(".checker");

            $thisCheckContainer.show().find("input[type='checkbox']").off("change").on("change", function () {

                $thisEntity.toggleClass("checked");
            });

        }, function () {
            var thisCheck = $(this).find(".checker");
            if (!thisCheck.find("input[type='checkbox']").is(":checked")) {
                thisCheck.hide();
            }
        }).find("input[type='checkbox']").uniform().parents(".checker").hide();

        //删除选中
        $("#btnMediaDeleteBatch").on("click", function () {
            var deleteds = $("a.cus-media-list").find("input[type='checkbox']:checked");
            if (deleteds.length && confirm("确定要删除选中的条目吗？")) {
                var deletedData = [];
                for (var i = 0, j = deleteds.length; i < j; i++) {
                    var deletedVal = $(deleteds[i]).val();
                    var data = "<input type='hidden' name='ids[" + i + "]' value='" + deletedVal + "'>";
                    deletedData.push(data);
                }
                $("#mediaDeleteForm").find("input[name^='ids']").remove().end()
                    .append(deletedData.join(""));
                return true;
            } else {
                return false;
            }
        });
    };

    //媒体编辑控制（Admin）
    var mediaDetailControl = function () {

        //资源添加表单
        $("#btnAddMediaResource").on("click", function () {
            var text = $(this).text(),
                btnSubmit = $("#btnSubmitMediaResource"),
                resourceForm = $("#mediaResourceAddForm");
            if ($.trim(text) == "添加") {
                $(this).text("取消");
                btnSubmit.show();
                resourceForm.slideDown();
            } else {
                $(this).text("添加");
                btnSubmit.hide();
                resourceForm.slideUp();
            }
            return false;
        });

        //资源添加
        $("#btnSubmitMediaResource").on("click", function () {
            var resourceForm = $("#mediaResourceAddForm"),
                resourceTable = $("#tblMediaResources"),
                txtQuality = resourceForm.find(".resource_quality").val(),
                txtSize = resourceForm.find(".resource_size").val(),
                txtName = resourceForm.find(".resource_name").val(),
                txtAddress = resourceForm.find(".resource_address").val(),
                index = resourceTable.find("tr").length - 1;
            if (!txtQuality || !txtSize || !txtName || !txtAddress) {
                return false;
            }
            var data = {
                index: index,
                Id: "",
                Name: txtName,
                Address: txtAddress,
                Quality: txtQuality,
                Size: txtSize
            };

            var tmpl = $("#tmplResource").text();
            var renderHtml = doT.template(tmpl)(data);
            resourceTable.find("tbody").append(renderHtml);

            return false;
        });

        //资源删除
        $("#tblMediaResources").on("click", function (e) {
            var $target = $(e.target);
            if ($target.is(".btnMediaResourceDel")) {
                if (confirm("确定要删除选中的资源信息吗？")) {
                    var deletedResource = $target.parents("tr");
                    var remainResources = deletedResource.siblings("tr");
                    if (remainResources.length) {
                        remainResources.each(function (i, remainResource) {
                            var hiddens = $(remainResource).find("input[name^='Resources']");
                            if (hiddens) {
                                hiddens.each(function (j, hidden) {
                                    var orignVal = $(hidden).attr("name");
                                    var newVal = orignVal.replace(/\d/g, i);
                                    $(hidden).attr("name", newVal);
                                });
                            }
                        });
                    }
                    deletedResource.remove();
                }
            }
            return false;
        });

        //检测封面网络地址
        $("#btnCheckCoverUrl").on("click", function () {
            var coverUrlObj = $("#txtCoverUrl"),
                coverShowObj = $("#imgCoverShow"),
                coverUrl = coverUrlObj.val();
            if (coverUrl) {
                coverShowObj.attr("src", $.trim(coverUrl));
            }
            return false;
        });

        //检测封面网络地址
        $("#btnDownloadCover").on("click", function () {
            var localCoverUrlObj = $("#txtLocalCoverUrl"),
                localCoverShowObj = $("#imgLocalCoverShow"),
                coverUrl = $("#txtCoverUrl").val();

            if (coverUrl) {
                //TODO:发出异步请求，下载网络图片至本地
            }

            return false;
        });
    };

    //状态切换按钮
    var switchButtonControll = function () {
        $("input.checkSwitchState").bootstrapSwitch({
            onText: "正常",
            offText: "锁定",
            offColor: "danger",
            onSwitchChange: function (event, state) {
                var targetName = $(event.target).attr("data-target");
                if (targetName) {
                    var targetCheck = $("input[name='" + targetName + "']");
                    if (targetCheck) {
                        if (state) {
                            targetCheck.val("false");
                        } else {
                            targetCheck.val("true");
                        }
                    }
                }
            }
        });
    };

    //统计图形控制
    var visitStatChartControl = function () {
        var statChart = $("#stat-chart");
        if (statChart.length) {
            var chartData = eval(statChart.attr("data-source"));
            (function () {
                var options = {
                    series: {
                        shadowSize: 0,
                        lines: {
                            show: true,
                            fill: true,
                            fillColor: { colors: ['rgba(47, 137, 214, 0.4)', 'rgba(98, 174, 239, 0.3)'] },
                            lineWidth: 1,
                            steps: false
                        },
                        points: {
                            show: true,
                            radius: 3,
                            symbol: "circle",
                            lineWidth: 2
                        }
                    },
                    grid: {
                        show: true,
                        hoverable: true,
                        clickable: true,
                        borderWidth: 1,
                        borderColor: "#ddd"
                    },
                    colors: ['#5bc0de'],
                    yaxis: {
                        font: { size: 11, color: "#888888" }
                    },
                    xaxis: {
                        show: true,
                        font: { size: 11, color: "#888888" },
                        mode: "categories",
                        tickLength: 0
                    }
                };
                $.plot(statChart, [chartData], options);

                $("<div id='tooltip'></div>").css({
                    position: "absolute",
                    display: "none",
                    border: "1px solid #fdd",
                    padding: "2px",
                    "background-color": "#fee",
                    opacity: 0.80
                }).appendTo("body");


                $("#stat-chart").bind("plothover", function (event, pos, item) {
                    if (item) {
                        console.log(item);
                        var tooltipText = "当日访问量：" + item.datapoint[1];
                        $("#tooltip").html(tooltipText).css({ top: item.pageY + 5, left: item.pageX + 5 }).fadeIn(200);
                    } else {
                        $("#tooltip").hide();
                    }
                });

            })();
        }

        var sparklines = $(".sparkline-delay");
        if (sparklines.length) {
            $.each(sparklines, function(i, item) {
                var $chart = $(item);
                var datas = eval($chart.attr("data-source"));
                if (datas && datas.length) {
                    $chart.sparkline(datas, {
                            type: "bar",
                            barColor: "#5bc0de",
                            barWidth: "10",
                            height: "35"
                        }
		            );
                }
            });
        }
    };

    return {
        init: function () {
            runAnimations();
            runSideMenu();
            mediaListControl();
            mediaDetailControl();
            switchButtonControll();
            visitStatChartControl();
        }
    };

}();


$(function () {
    system.init();
});

