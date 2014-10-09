var rule = (function ($) {
    'use strict';

    return {
        init: function () {
            $("#add").click(function (e) {
                e.preventDefault();
                rule.openPopup($(this).data('url'));
            });

            $("#save").click(function (e) {
                e.preventDefault();

                var sortIds = [];

                $(".id-rule").each(function () {
                    sortIds.push($(this).attr("data-value"));
                });

                rule.updateRule({ "sort": sortIds.join(',') }, $(this).data("url"));
            });

            $(".delete").click(function (e) {
                e.preventDefault();
                if (confirm("Do you want delete this rule?")) {
                    rule.updateRule(rule.getParams(this), $(this).data("url"));
                }
            });

            $(".edit").click(function (e) {
                e.preventDefault();
                rule.openPopup($(this).data('url'));
            });

            $(".rule-status").click(function (e) {
                var isChecked = $(this).prop("checked");
                if (confirm("Do you want change rule status?")) {
                    $(this).parent().attr("data-value", isChecked);
                    rule.updateRule(rule.getParams(this), $(this).parent().data("url"));
                }
                else {
                    e.preventDefault();
                }
            });

            $("form").submit(function (e) {
                e.preventDefault();
                if (!rule.checkValidData($("#Response").val())) {
                    return;
                }
                rule.addRule($(this).serializeObject(), $(this).attr("action"));
            });

            if ($("#sortable").length > 0) {
                $("#sortable").sortable();
                $("#sortable").disableSelection();
            }
        },
        getParams: function (action) {
            var params = {};
            $(action).parents(":eq(1)").find(".rule").each(function () {
                params[$(this).data("name")] = $(this).attr("data-value");
            });
            return params;
        },
        updateRule: function (params, url) {
            $.ajax({
                url: url,
                type: 'post',
                data: params,
                beforeSend: function () { },
                success: function (data) {
                    if (data.ErrCode !== 0) {
                        alert(data.ErrMsg);
                        return;
                    }
                    else {
                        document.location.reload();
                    }
                },
                complete: function () { }
            });
        },
        addRule: function (params, url) {
            $.ajax({
                url: url,
                type: 'post',
                data: params,
                beforeSend: function () { },
                success: function (data) {
                    if (data.ErrCode !== 0) {
                        alert(data.ErrMsg);
                        return;
                    }
                    else {
                        parent.rule.closePopup();
                    }
                },
                complete: function () { }
            });
        },
        closePopup: function () {
            $('.modal-body').modal('hide');
            document.location.reload();
        },
        checkValidData: function (response) {
            try {
                var data = $.parseJSON(response);

                if (data["ErrCode"] === undefined
                    || data["ErrMsg"] === undefined) {
                    alert("Response must has ErrCode and ErrMsg");
                    return false;
                }

                return true;
            }
            catch (err) {
                alert("Please input response correct format.");
                return false;
            }
        },
        openPopup: function (url) {
            $(".modal-body").html('<iframe width="100%" height="100%" frameborder="0" scrolling="no" allowtransparency="true" src="{0}"></iframe>'
                                .format(url));
        }
    };
} (jQuery));

$(function () {
    'use strict';
    rule.init();
});