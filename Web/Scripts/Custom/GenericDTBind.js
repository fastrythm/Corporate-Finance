function ShowOrHideExportOptions() {
    $("#ulExportOptions").click(function () {
        $("#printOption").slideToggle("slow", function () {
            // Animation complete.
        });
    });
}


function DTToggler() {
    $('#div-table').toggle();
}

function DTTogglerById(Id, IsShow) {

    if (IsShow)
        $(Id).show();
    else
        $(Id).hide();
}

var DeleteLevel = 1;
function InitializeDialogConfirmation() {
    $("#dialog_confirm").dialog({
        dialogClass: 'ui-dialog-green',
        autoOpen: false,
        resizable: false,
        height: 210,
        modal: true,
        buttons: [
          {
              'class': 'btn red',
              "text": "Delete",
              click: function () {
                  if (DeleteLevel == 1)
                      DeleteConfirmation($(this));
                  else if (DeleteLevel == 2)
                      Level2DeleteConfirmation($(this));
                  else if (DeleteLevel == 3)
                      Level3DeleteConfirmation($(this));
                  else if (DeleteLevel == 4)
                      Level4DeleteConfirmation($(this));
                  //else
                  //    DeleteConfirmation($(this));
              }
          },
          {
              'class': 'btn',
              "text": "Cancel",

              click: function () {
                  $(this).dialog("close");
              }
          }
        ]
    });

   
}
// DatTable Configurations & Settings
var oTable;
var oTableDetail;

var IsDTHasDetail = false;

var customParamLevel1 = [];
var customParamLevel2 = [];
var customParamLevel3 = [];

var _DTGlobalConfig = {
    bInfo: true,
    bLengthChange: true,
    bProcessing: true,
    bServerSide: true,
    bScrollCollapse: true,
    sDom: '<\'row\'<\'col-sm-4\'B> <\'col-sm-4\'l><\'col-sm-4\'f> >' + "<'row'<'col-sm-12'tr>>" + "<'row'<'col-sm-6'i><'col-sm-6'p>>",
    oLanguage: {
        "sProcessing": " "
    }
};

var _DTGlobalManualConfig = {
    bInfo: true,
    bLengthChange: true,
    bProcessing: true,
    bServerSide: false,
    bScrollCollapse: true,
    sDom: '<\'row\'<\'col-sm-4\'B> <\'col-sm-4\'l><\'col-sm-4\'f> >' + "<'row'<'col-sm-12'tr>>" + "<'row'<'col-sm-6'i><'col-sm-6'p>>",
    oLanguage: {
        "sProcessing": " "
    }
};
 

// For Level 1
function GenerateDTVersion1(selector, aoColumns, servicePath, otherParams) {
    var defaultParam = {
        "bInfo": _DTGlobalConfig.bInfo,
        "bLengthChange": _DTGlobalConfig.bLengthChange,
        "bProcessing": _DTGlobalConfig.bProcessing,
        "bServerSide": _DTGlobalConfig.bServerSide,
        //initComplete: DataTableInitComplete,
        'ajax': {
            url: servicePath,
            type: 'POST',
            contentType: "application/json; charset=utf-8",
            data: function (data) {
                return data = JSON.stringify(data);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                //if (thrownError.message.lastIndexOf('Unexpected token') != -1) {
                //    window.location.href = '/Login';
                //}
            }
        },
        "sDom": _DTGlobalConfig.sDom,
        "lengthMenu": [[25, 50, 100, 99999999], [25, 50, 100, "All"]],
        "oLanguage": _DTGlobalConfig.oLanguage
    };

    var options = $.extend(defaultParam, { "aoColumns": aoColumns });
    if (otherParams != null)
        options = $.extend(options, otherParams);


    $(function () {
        oTable = $(selector).dataTable(options);
        //oTable.fnFilter('test string'); // Starting Filter

        jQuery(selector + '_wrapper .dataTables_filter input').addClass("m-wrap small"); // modify table search input
        jQuery(selector + '_wrapper .dataTables_length select').addClass("m-wrap small"); // modify table per page dropdown
      //  jQuery(selector + '_wrapper .dataTables_length select').select2(); // initialize select2 dropdown
        $(selector + '_filter input').unbind();
        $(selector + '_filter input').bind('keyup', function (e) {
            if (e.keyCode == 13) {
                oTable.fnFilter(this.value);
            }
            else
                e.preventDefault();
        });

        $('.dataTables_filter input').insertAfter($('.dataTables_filter label:first'));
        $('.dataTables_filter label').remove();
        $('.dataTables_filter input').attr("placeholder", "Search");
    });
}


function GenerateDTManual(selector, aoColumns, servicePath, otherParams,isRowGrouping,groupIndexNumber) {
    var defaultParam = {
        "bInfo": _DTGlobalManualConfig.bInfo,
        "bLengthChange": _DTGlobalManualConfig.bLengthChange,
        "bProcessing": _DTGlobalManualConfig.bProcessing,
        "bServerSide": _DTGlobalManualConfig.bServerSide,
        //initComplete: DataTableInitComplete,
        'ajax': {
            url: servicePath,
            type: 'POST',
            contentType: "application/json; charset=utf-8",
            data: function (data) {
                return data = JSON.stringify(data);
            },
            error: function (xhr, ajaxOptions, thrownError) {
                //if (thrownError.message.lastIndexOf('Unexpected token') != -1) {
                //    window.location.href = '/Login';
                //}
            }
        },
        "sDom": _DTGlobalManualConfig.sDom,
        "lengthMenu": [[25, 50, 100, 99999999], [25, 50, 100, "All"]],
        "oLanguage": _DTGlobalManualConfig.oLanguage
    };

    var options = $.extend(defaultParam, { "aoColumns": aoColumns });
    if (otherParams != null)
        options = $.extend(options, otherParams);


    $(function () {
        if (isRowGrouping)
            oTable = $(selector).dataTable(options).rowGrouping({ iGroupingColumnIndex: groupIndexNumber });
        else
            oTable = $(selector).dataTable(options);
        //oTable.fnFilter('test string'); // Starting Filter

        jQuery(selector + '_wrapper .dataTables_filter input').addClass("m-wrap small"); // modify table search input
        jQuery(selector + '_wrapper .dataTables_length select').addClass("m-wrap small"); // modify table per page dropdown
        //  jQuery(selector + '_wrapper .dataTables_length select').select2(); // initialize select2 dropdown
        $(selector + '_filter input').unbind();
        $(selector + '_filter input').bind('keyup', function (e) {
            if (e.keyCode == 13) {
                oTable.fnFilter(this.value);
            }
            else
                e.preventDefault();
        });

        $('.dataTables_filter input').insertAfter($('.dataTables_filter label:first'));
        $('.dataTables_filter label').remove();
        $('.dataTables_filter input').attr("placeholder", "Search");
    });
}

 
// DT Init Complete Function
function DataTableInitComplete() {
    this.api().columns().every(function () {
        var column = this;
        var select = $('<input type="text" class="m-wrap span12" >')
            //.appendTo($(column.footer()).empty())
            //.appendTo($(column.header()).empty())
            .appendTo($(column.header()))
            .on('keyup', function (e) {
                if (e.which == 13) {
                    //var val = $.fn.dataTable.util.escapeRegex(
                    //    $(this).val()
                    //);

                    column
                        .search($(this).val(), false, false)
                        .draw();
                }
            });
    });
    $('.dataTable thead th :input').hide();

}


 

function DTToggler() {
    $('#div-table').toggle();
}
