//import fa from "../assets/js/plugins/flatpickr/l10n/fa";
//var CustomGrid = function (defSortIndex = 0, defSortMode = "asc") {
//    this.paging = true;
//    this.lengthMenu = [
//        [25, 50, 100, 500],//value collection
//        ['25 rows', '50 rows', '100 rows', '500 rows']//display collection
//    ];
//    this.pagingType = "full_numbers";
//    this.pageLength = 25;//lengthMenu[0][0];

//    this.processing = true;
//    this.async = true;
//    this.serverSide = true;
//    this.searching = true;

//    this.scrollY = true;
//    this.scrollX = true;
//    this.scrollCollapse = true;
//    this.orderMulti = false;
//    this.aaSorting = [[defSortIndex, defSortMode]];
//    this.colReorder = true;
//    this.buttons = [
//        //'copyHtml5',
//        //'excelHtml5',
//        //'csvHtml5',
//        //'pdfHtml5',
//        //{
//        //    extend: 'print',
//        //    exportOptions: {
//        //        columns: 'th:not(:first-child)'
//        //    }
//        //}
//        {
//            extend: 'colvis',
//            text: '<i class="custom_buttons">Show/Hide Columns</i>',
//            columns: ':not(.noVis)',
//            collectionLayout: 'fixed three-column'
//        }
//    ];

//    //define your datatable structure here
//    this.dom = "<'row'<'col-sm-12 col-md-6'l><'col-sm-12 col-md-6 custom_buttons'B>>" +
//        "<'row'<'col-sm-12'tr>>" +
//        "<'row'<'col-sm-12 col-md-5'i><'col-sm-12 col-md-7'p>>";
//}  

var CustomGridSettings = function () {
    this.paging = true;
    this.lengthMenu = [
        [10, 25, 50, 100, 500, 100000],//value collection
        ['10 rows', '25 rows', '50 rows', '100 rows', '500 rows', 'All']//display collection
    ];
    this.responsive = true;
    this.processing = true;
    this.serverSide = true;
    this.pageLength = 10;
    this.scrollX = "100%";
    this.scrollY = "100%";
    this.orderMulti = false;
    this.colReorder = false;
    this.stateSave = false;
    this.aaSorting = [];
    //this.scrollCollapse = true;
    //this.fixedColumns = true;
    this.dom = '<"top"lp>rt<"bottom d-flex justify-content-md-between"ip><"clear">';
}


//Base Settings for all Grids in Application
var DefaultGridSettings = function (defSortIndex = 0, defSortMode = "asc", actionColPostion = "last") {
    this.paging = true;
    this._actionColName = actionColPostion == "first" ? 'th:not(:first-child)' : actionColPostion == "last" ? 'th:not(:last-child)' : '';
    this.lengthMenu = [
        [10, 25, 50, 100, 500, 100000],//value collection
        ['10 rows', '25 rows', '50 rows', '100 rows', '500 rows', 'All']//display collection
    ];
    this.responsive = true;
    this.processing = true;
    this.serverSide = true;
    this.pageLength = 25;
    this.scrollX = "100%";
    this.scrollY = "100%";
    this.orderMulti = false;
    this.aaSorting = [[defSortIndex, defSortMode]];
    this.colReorder = true;
    this.stateSave = false;
    //this.language = [
    //    {
    //        processing: "Loading. Please wait..."
    //    }
    //];
    this.buttons = [
        {
            extend: 'colvis',
            text: '<i class="fa-filter">Columns</i>',
            columns: ':not(.noVis)',
            collectionLayout: 'fixed one-column'
        }
    ];

    //define your datatable structure here
    this.dom = "<'dt-head'<'col-6 float-left'l><'col-6 float-left'B>>" +
        "<'row dt-body'<'col-sm-12'tr>>" +
        "<'row dt-foot'<'col-sm-12 col-md-5'i><'col-sm-12 col-md-7'p>>";
}

//Extending Settings from Base Settings
var ExportableGridSettings = function (defSortIndex = 0, defSortMode = "asc", actionColPostion = "last") {
    DefaultGridSettings.call(this, defSortIndex, defSortMode, actionColPostion);
    //Below code can be removed and added what is required
    this.buttons = [
        {
            extend: 'colvis',
            text: '<span class="dt-custom-btn"><i class="mdi mdi-view-week" title="Show/ Hide Columns"></i></span>',
            columns: ':not(.noVis)',
            collectionLayout: 'fixed one-column'
            //hide: [0, 1]
        },
        {
            extend: 'collection',
            text: '<span class="dt-custom-btn"><i class="mdi mdi-application-export" title="Export"></i></span>',

            buttons: [
                {
                    extend: 'copyHtml5',
                    text: '<i class="mdi mdi-file-multiple" title="Copy"></i> Copy',
                    //titleAttr: 'Copy',
                    exportOptions: {
                        columns: this._actionColName
                    }
                },
                {
                    extend: 'excelHtml5',
                    text: '<i class="mdi mdi-file-excel"></i> Excel',
                    //titleAttr: 'Excel',
                    autoFilter: true,
                    sheetName: 'Exported data',
                    exportOptions: {
                        columns: this._actionColName
                    }
                },
                {
                    extend: 'csvHtml5',
                    text: '<i class="mdi mdi-file"></i> CSV',
                    //titleAttr: 'CSV',
                    exportOptions: {
                        columns: this._actionColName
                    }
                }//,
                //{
                //    extend: 'pdfHtml5',
                //    text: '<i class="mdi mdi-file-pdf"></i> PDF',
                //    //titleAttr: 'PDF',
                //    orientation: 'landscape',
                //    pageSize: 'LEGAL',
                //    download: 'open',
                //    exportOptions: {
                //        columns: this._actionColName
                //    }
                //},
                //{
                //    extend: 'print',
                //    text: '<i class="mdi mdi-printer"></i> Print',
                //    exportOptions: {
                //        columns: this._actionColName
                //    }
                //}
            ]
        },//, //enable this section to show filter
        {
            extend: 'collection',
            text: '<span class="custom-btn-class"><span class="filter-count" style="display:block;">0</span><i class="mdi mdi-filter filter-action" title="Filter" id="btnminfilter"><i class="mdi mdi-check filtered-check-i" style="display:none;"></i></i></span>',
            action: function (e, dt, node, config) {
                $("#dbfilter-content").toggle('slow');
            }
        }

        //Creating Custom button
        //{
        //    text: 'Add Staff',
        //    action: function (e, dt, node, config) {
        //        //alert('Button activated');
        //        //window.open('https://www.google.com/');
        //        var params = [
        //            'height=' + screen.height,
        //            'width=' + screen.width,
        //            'fullscreen=yes' // only works in IE, but here for completeness
        //        ].join(',');
        //        window.open('AddStaff', 'popup_window', params);
        //        popup.moveTo(0, 0);
        //    }
        //}

    ];
}
//ExportableGridSettings.prototype = new DefaultGridSettings();

//Extending Settings from Extended Settings
var ExtendedGridSettingsNew = function (defSortIndex = 0, defSortMode = "asc") {
    ExportableGridSettings.call(this, defSortIndex, defSortMode);
    //Below code can be removed and added what is required
    this.ExtendedGridSettingsNewProp = true;
}
//ExtendedGridSettingsNew.prototype = new ExportableGridSettings();