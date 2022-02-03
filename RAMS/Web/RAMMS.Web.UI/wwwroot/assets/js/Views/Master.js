var jsMaster = new function () {
    this.DisplayDateFormat = "DD-MMM-YYYY"; // Grid / display purpose
    this.AssignFormat = "YYYY-MM-DD"; // to HTML 5 input date control
    this.GridFormat = "DD/MM/YYYY"; // Grid Format
    this.Init = () => {
        $("select:not(.custom-select)").chosen();
        this.ClickAttrRegister("ShowSearch", function (ctrl) { $(ctrl.ShowSearch).slideToggle(500); });
        this.ClickAttrRegister("ClearSearch", function (ctrl) {
            var serSec = $(ctrl).closest("[searchSection]");
            serSec.find("input:text").val("");
            $(ctrl.ClearSearch).find("input,select,textarea").each(function () {
                switch (this.type) {
                    case "checkbox": this.checked = false; break;
                    case "select-one": if (this.clone) { $(this).empty(); $(this).append($(this.clone).clone().find("option")); } $(this).val(""); $(this).trigger("chosen:updated"); break;
                    case "select-multiple": $(this).val(null); $(this).trigger("chosen:updated"); break;
                    default: $(this).val(""); break;
                }
            });
            serSec.find("[SearchSectionBtn]").trigger("click");
        });
    }
    this.ClickAttrRegister = (attrName, fn) => {
        $("[" + attrName + "]").each(function () {
            this[attrName] = $(this).on("click", function () {
                if (fn) { fn(this); }
            }).attr(attrName);
            $(this).removeAttr(attrName);
            $(this[attrName]).find("select[clear='rebind']").each(function () {
                this.clone = $(this).clone();
            });
        });
    }
    this.CommonSearchKeypress = function (tis, evt) {
        var keyCode = evt.keyCode ? evt.keyCode : evt.charCode;
        if (keyCode == 13) { $(this).parent().find("[SearchSectionBtn]").trigger("click"); }
    }
    this.getDateOfISOWeek = function (week, year) {

        var simple = new Date(year, 0, 1 + (week - 1) * 7);

        var dow = simple.getDay();

        var ISOweekStart = simple;
        if (dow == 1)
            ISOweekStart = ISOweekStart;
        else if (dow < 1)
            ISOweekStart.setDate(simple.getDate() + 1);
        else {

            ISOweekStart.setDate(simple.getDate() + 8 - simple.getDay());
        }

        return ISOweekStart;
    }
    this.ConfirmCancel = function (sucess, failed) {
        app.Confirm("Unsaved changes will be lost. Are you sure you want to cancel?", function (ok) { if (ok) { if (sucess) { sucess(); } } else { if (failed) { failed(); } } }, "Yes", "No");
    }
    this.LocationCh = (locCh) => {
        var res = locCh;
        if (locCh) {
            var arrLoc = String(locCh).split('.');
            if (arrLoc.length >= 2) {
                if (arrLoc[1].length == 3) {
                    res = locCh;
                }
                else if (arrLoc[1].length == 2) {
                    res = arrLoc[0] + '.' + (arrLoc[1] + "0");
                }
                else if (arrLoc[1].length == 1) {
                    res = arrLoc[0] + '.' + (arrLoc[1] + "00");
                }
                else if (arrLoc[0].length != null && arrLoc[0].length != undefined && arrLoc[0].length != "") {
                    res = arrLoc[0] + '.' + ("000");
                }
            }
            else if (arrLoc[0].length != null && arrLoc[0].length != undefined && arrLoc[0].length != "") {
                res = arrLoc[0] + '.' + ("000");
            }
        }
        return res;
    }
    this.DateCtrlValidate = function (tis) {
        if (tis.value != "") {
            var obj = $(tis);
            var minval = obj.attr("min");
            var maxval = obj.attr("max");
            var msg = obj.attr("msg");
            if (minval && minval != "" && msg && msg != "") {
                var min = moment(minval).valueOf();
                var max = moment(maxval).valueOf();
                var time = tis.valueAsDate.getTime();
                if (time < min || max < time) {
                    app.ShowErrorMessage(msg + " should be between " + minval + " and " + maxval);
                    tis.value = "";
                }
            }
        }
    }
}
var dropDownInject = function (sourceSel, destSel, isValueCompare) {
    var tis = this;
    tis.SSel = sourceSel;
    tis.DSel = destSel;
    tis.DSelData = [];
    tis.IsValueCompare = typeof (isValueCompare) == "boolean" ? isValueCompare : true;
    tis.Init = function () {
        tis.DSelData = [];
        $(tis.DSel).find("option[cvalue]").each(function () {
            var obj = $(this);
            tis.DSelData[tis.DSelData.length] = { value: obj.val(), text: obj.text(), cvalue: obj.attr("cvalue") };
        });
        $(sourceSel + ":not('[ddinject]')").on("change", function () {
            var val = $(this).val();
            if (val != "") { val = tis.IsValueCompare ? val : $(this).find("option:selected").text(); }
            var sel = $(tis.DSel);
            sel.find("option[cvalue]").remove();
            $.each(tis.DSelData, function (idx, obj) {
                if (obj.cvalue == val || val == "") {
                    var opt = $("<option/>")
                    opt.val(obj.value);
                    opt.text(obj.text);
                    opt.attr("cvalue", obj.cvalue);
                    sel.append(opt);
                }

            });
            sel.trigger("chosen:updated");
            console.log(this);
            console.log(tis);
        }).attr("ddinject");
    };
    tis.Init();
}

jsMaster.Init();

