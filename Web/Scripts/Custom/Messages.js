(function ($) {

    $.fn.displayMessage = function (options) {

        // This is the easiest way to have default options.
        var settings = $.extend({
            // These are the defaults.
            message: "",
            type: "",
            login: false,
            UserHomePageUrl: ""
        }, options);

        this.each(function () {
            $('div.alert').remove();
            if (settings.type == "success")
                $(this).prepend("<div class='alert alert-success'><button type='button' class='close' data-dismiss='alert'>×</button><strong>Success!</strong> " + settings.message + "</div>");
            else if (settings.type == "info")
                $(this).prepend("<div class='alert alert-info'><button type='button' class='close' data-dismiss='alert'>×</button><strong>Info!</strong> " + settings.message + "</div>");
            else if (settings.type == "warning")
                $(this).prepend("<div class='alert alert-info'><button type='button' class='close' data-dismiss='alert'>×</button><strong>Warning!</strong> " + settings.message + "</div>");
            else if (settings.type == "error")
                $(this).prepend("<div class='alert alert-error'><button type='button' class='close' data-dismiss='alert'>×</button><strong>Error!</strong> " + settings.message + "</div>");
            else
                $(this).prepend("<div class='alert'><button type='button' class='close' data-dismiss='alert'>×</button><strong>Alert!</strong> " + settings.message + "</div>");

        });


        //return this.css({
        //    color: settings.color,
        //    backgroundColor: settings.backgroundColor
        //});
        return this;

    };

}(jQuery));