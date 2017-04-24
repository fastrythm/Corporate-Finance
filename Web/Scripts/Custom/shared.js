var CONSTANT_MISSING_INFORMATION_TITLE = "Please correct the error(s) and try again";
var CONSTANT_ERROR_TYPE_CLASS = "error";
var CONSTANT_SUCCESS_TYPE_CLASS = "success";
 
// Filter and Export Options Show/Hide

$(document).ajaxStart(function () {
    $('.loader').show()
});

$(document).ajaxComplete(function () {
    $('.loader').hide();
});


function HideFilters() {
    if ($('table.usertable').length > 0) {
        $('table.usertable tr.thefilter').hide();
        $('div.dataTables_filter').hide();
    }
}

function ShowFilters() {
    if ($('table.usertable').length > 0) {
        $('table.usertable tr.thefilter').show();
        $('div.dataTables_filter').show();
    }
}
 
function ALBindAutoCompleteSelect2(selector, service) {
    $(selector).select2({
        ajax: {
            url: service,
            type: 'POST',
            params: {
                contentType: 'application/json; charset=utf-8'
            },
            dataType: 'json',
            cache: true,
            data: function (term, page) {
                return JSON.stringify({ text: term, page_limit: 50 }); //it must be converted to JSON
            },
            results: function (data, page) {
                return { results: JSON.parse(data.d) };
            }
        },
        escapeMarkup: function (m) { return m; }
    });
}

function ALBindAutoCompleteSelect2WithDefault(selector, service, receivedId, receivedText) {
    $(selector).select2({
        initSelection: function (element, callback) {
            callback({ id: receivedId, text: receivedText });
        },
        ajax: {
            url: service,
            type: 'POST',
            params: {
                contentType: 'application/json; charset=utf-8'
            },
            dataType: 'json',
            cache: true,
            data: function (term, page) {
                return JSON.stringify({ text: term, page_limit: 50 }); //it must be converted to JSON
            },
            results: function (data, page) {
                return { results: JSON.parse(data.d) };
            }
        },
        //formatResult: STSelect2FormatResult,
        //formatSelection: STSelect2FormatSelection,
        escapeMarkup: function (m) { return m; }
    });
}


function ALGritter(title, message, hasImage, isSticky, type) {
    if (isSticky) {
        $.gritter.removeAll();
    }
     
    $('.notification').attr('class', 'notification ' + type);
    $('.notification').html(message);
    $('.notification').slideDown('fast');
    window.setTimeout(ALCloseNotification, 5000);
}
 
function ALCloseNotification() {
    $('.notification').slideUp('fast');
}

function ALCurrencyFormat(amount) {
    if (!isNaN(parseFloat(amount))) {
        return addCommas(amount.toFixed(3)); //.toString().replace(/\B(?=(\d{4})+(?!\d))/g, ",");
 
    }
    else
        return parseFloat(0).toFixed(3);
}
function addCommas(nStr) {
    nStr += '';
    var x = nStr.split('.');
    var x1 = x[0];
    var x2 = x.length > 1 ? '.' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + ',' + '$2');
    }
    return x1 + x2;
}

function ALReturnCurrencyFormatToString(value) {
    if (!isNaN(parseFloat(value)))
        return value.replace(/,/g, "");
    else
        return 0.00;
}
function DateFormat(dateConvert) {
    if (dateConvert != null && dateConvert != "")
    { return $.datepicker.formatDate("mm/dd/yy", eval('new ' + dateConvert.slice(1, -1)));}
    else
        return "";
};

function addLeadingZeros(n, length) {
    var str = (n > 0 ? n : -n) + "";
    var zeros = "";
    for (var i = length - str.length; i > 0; i--)
        zeros += "0";
    zeros += str;
    return n >= 0 ? zeros : "-" + zeros;
}

function ALIsIEBrowser() {

    var ua = window.navigator.userAgent;
    var msie = ua.indexOf("MSIE ");

    if (msie > 0 || !!navigator.userAgent.match(/Trident.*rv\:11\./))      // If Internet Explorer, return version number
        return true;
    else                 // If another browser, return 0
        return false;

    return false;
}


 

var stTableToExcel = (function () {
    var uri = 'data:application/vnd.ms-excel;base64,'
    , template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table>{table}</table></body></html>'
    , base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) }
    , format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) }
    return function (table, name) {
        if (!table.nodeType) table = document.getElementById(table)
        var ctx = { worksheet: name || 'Worksheet', table: table.innerHTML }
        window.location.href = uri + base64(format(template, ctx))
    }
})();
 
// Custom Methods
function GetFileType(file) {
    var extension = file.substr((file.lastIndexOf('.') + 1));
    switch (extension) {
        case 'jpg':
        case 'png':
        case 'gif':
            alert('was jpg png gif');  // There's was a typo in the example where
            break;                         // the alert ended with pdf instead of gif.
        case 'zip':
        case 'rar':
            alert('was zip rar');
            break;
        case 'pdf':
            alert('was pdf');
            break;
        default:
            alert('who knows');
    }
};

function GetFileExtension(file) {
    return file.substr((file.lastIndexOf('.') + 1));
};

// Query String Parameters
function GetQueryStringParameterByName(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
    return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
}

 

/* Select2 Default Formater*/
function Select2DefaultFormatResult(repo) {
    var markup = '<div class="row-fluid"><div class="span10"><div class="row-fluid">' +
        '<div class="span9">' + repo.Value + '</div></div>';
    markup += '</div></div>';

    return markup;
}

function Select2DefaultFormatSelection(repo) {
    return repo.Value;
}

/* Select2 Generic Binding*/
function ALBindSelect2(selector, defaultOverrideConfig) {
    var defaultConfig = {
        id: function (e) { return e.Id },
        placeholder: "Search for a result",
        minimumInputLength: 3,
        allowClear: true,
        ajax: {
            //How long the user has to pause their typing before sending the next request
            quietMillis: 150,
            //The url of the json service
            url: '',
            dataType: 'json',
            //Our search term and what page we are on
            data: function (term, page) {
                return {
                    //pageSize: pageSize,
                    //pageNum: page,
                    query: term
                };
            },
            results: function (data, page) { // parse the results into the format expected by Select2.
                // since we are using custom formatting functions we do not need to alter remote JSON data
                return {
                    results: data
                };
            }
        },
        formatResult: Select2DefaultFormatResult,
        formatSelection: Select2DefaultFormatSelection,
        dropdownCssClass: "bigdrop", // apply css that makes the dropdown taller
        escapeMarkup: function (m) {
            return m;
        } //
    };
    var config = $.extend(true, defaultConfig, defaultOverrideConfig);
    $(selector).select2(config);
}

 
function AjaxCall(url,method, param, OnSuccess) {
    $.ajax({
        type: method,
        url: url,
        data: param,
        success: OnSuccess,
        error: OnError
    });
}

function OnError(type, status, error) {
    ALGritter('Error', error , false, false, CONSTANT_ERROR_TYPE_CLASS);
}
 
 
var GetTomorrowDate = function () {
    var dt = new Date();
    return new Date((dt.setDate(dt.getDate() + 1))).toString();
}

 
var GetYesterdayDate = function () {
    var dt = new Date();
    return new Date((dt.setDate(dt.getDate() - 1))).toString();
}