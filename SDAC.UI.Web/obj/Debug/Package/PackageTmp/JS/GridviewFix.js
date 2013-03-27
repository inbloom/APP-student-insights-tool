
/*
An object required as an argument for the class names of the header , rows and footer of the target table.
Below are the default values of the class names.
{
header:"headerStyle" // header class name
,row:"rowStyle" //row class name
,footer:"footerStyle" //footer class name
}
*/
(function ($, undefined) {
    $.fn.GridviewFix = function (params) {
        var settings = $.extend({}, { header: "headerStyle", row: "rowStyle", footer: "footerStyle" }, params);
        return this.each(function () {
            var 
            ctxt = $(this)
	        , thead = $("<thead />").append($("tr.".concat(settings.header), ctxt))
	        , tbody = $("<tbody />").append($("tr.".concat(settings.row), ctxt))
	        , tfooter = $("<tfoot />").append($("tr.".concat(settings.footer), ctxt));
            $("tbody", ctxt).remove();
            ctxt.append(thead).append(tbody).append(tfooter);
        });
    }
})(jQuery);

