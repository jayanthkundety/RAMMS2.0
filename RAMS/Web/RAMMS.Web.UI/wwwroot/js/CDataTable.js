var CDataTable = function (obj) {
    this.dataTable = null;
    this.filter = {};
    this.Base = obj;
    this.default = {};
    this.default.paging = true;
    this.default.lengthMenu = [
        [10, 25, 50, 100, 500, 100000],//value collection
        ['10 rows', '25 rows', '50 rows', '100 rows', '500 rows', 'All']//display collection
    ];
    this.default.responsive = true;
    this.default.processing = true;
    this.default.serverSide = true;
    this.default.pageLength = 10;
    this.default.scrollX = "100%";
    this.default.scrollY = "100%";
    this.default.orderMulti = false;
    this.default.colReorder = false;
    this.default.stateSave = false;
    this.default.autoWidth = false;
    this.default.aaSorting = [];
    this.default.dom = '<"top"lp>rt<"bottom d-flex justify-content-md-between"ip><"clear">';
    this.default.ajax = {
        url: this.Base.APIURL,
        type: "POST",
        datatype: "json",
        data: (d) => {
            d.filter = this.filter
            return d;
        }
    }
    this.RenderColumns = function () {
        for (var i = 0; i < this.Base.Columns.length; i++) {
            if (this.Base.Columns[i].render && this.Base.Columns[i].render != "") {
                this.Base.Columns[i].CRender = this.Base.Columns[i].render;
                this.Base.Columns[i].render = function (data, type, row, meta) {
                    return eval(meta.settings.aoColumns[meta.col].CRender + "(data, type, row, meta)");
                }
            }
            //if()
        }
    }
    this.RenderColumns();
    this.default.columns = this.Base.Columns;
    if (this.Base.columnDefs && this.Base.columnDefs.length > 0) {
        this.default.columnDefs = this.Base.columnDefs;
    }
    if (this.Base.LeftFixedColumn > 0 && this.Base.RightFixedColumn > 0) {
        this.default.fixedColumns = { leftColumns: this.Base.LeftFixedColumn, rightColumns: this.Base.RightFixedColumn }
    }
    else if (this.Base.LeftFixedColumn > 0) {
        this.default.fixedColumns = { leftColumns: this.Base.LeftFixedColumn }
    }
    else if (this.Base.RightFixedColumn > 0) {
        this.default.fixedColumns = { rightColumns: this.Base.RightFixedColumn }
    }    
    this.Init = () => {

        this.dataTable = $("#" + this.Base.Name).DataTable(this.default);
    }    
    this.BindSearchData = function (tis) {
        var obj = $(tis);
        var src = obj.attr("param");
        this.filter[src] = obj.val();
    }
    this.Search = function (tis, section) {
        this.filter = {};
        if (this.dataTable != null) {
            var _this = this;
            $(tis).closest("[searchSection]").find("[param]").each(function () {
                _this.BindSearchData(this);
            });
            if (section != '') {
                $(section).find("[param]").each(function () {
                    _this.BindSearchData(this);
                });
            }
            this.dataTable.draw();
        }
    }
    this.Refresh = function () {
        if (this.dataTable != null) {
            this.dataTable.draw();
        }
    }
    this.Init();
}