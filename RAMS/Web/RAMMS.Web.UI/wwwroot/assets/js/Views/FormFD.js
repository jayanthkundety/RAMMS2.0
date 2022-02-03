var formFD = new function () {
    this.HeaderData = {};
    this.Pattern = "";
    this.IsEdit = true;
    this.FindDetails = function () {
        if (ValidatePage("#frmFDHeaderInformation")) {
            var tis = this;
            var rd = $("#selRoadCode option:selected");
            var post = {};
            post.RoadId = rd.attr("pid");
            post.RmuName = $("#selRMU option:selected").attr("cvalue");
            GetResponseValue("FindDetails", "FormFD", FormValueCollection("#frmFDHeaderInformation", post), function (data) {
                if (data && !data._error) {
                    $("[finddetailhide]").hide();
                    $("#selRoadCode,#formFDInsYear").prop("disabled", true).trigger("chosen:updated");
                    tis.HeaderData = data;
                    tis.PageInit();
                }
                else {
                    app.ShowErrorMessage(data._error);
                }
            }, "Finding");
        }
    }
    this.PageInit = function () {
        if (this.HeaderData && this.HeaderData.PkRefNo > 0) {
            $("[finddetailsdep]").show();
            $("#btnFindDetails").hide();
            this.BindData();
            this.InitConditionTable();
        }
        else {
            $("[finddetailsdep]").hide();
            $("#btnFindDetails").show();
        }
    }
    this.BindData = function () {
        if (this.HeaderData && this.HeaderData.PkRefNo && this.HeaderData.PkRefNo > 0) {
            if (this.IsEdit) { this.IsEdit = this.HeaderData.SubmitSts ? false : true; }
            var tis = this;
            var assignFormat = jsMaster.AssignFormat;
            $("#frmFDHeader").find("input,select,textarea").filter("[name]").each(function () {
                var obj = $(this);
                var name = obj.attr("name");
                if (tis.HeaderData[name] != null) {
                    if (this.type == "select-one") { obj.val("" + tis.HeaderData[name]).trigger("change").trigger("chosen:updated"); }
                    else if (this.type == "date") { obj.val((new Date(tis.HeaderData[name])).ToString(assignFormat)); }
                    else { obj.val(tis.HeaderData[name]); }
                }
                else { obj.val(""); }
                if (!tis.IsEdit) {
                    obj.prop("disabled", true);
                    if (this.type == "select-one") { obj.trigger("chosen:updated"); };
                }
            });
            $("#dtInspection").attr("min", this.HeaderData.YearOfInsp + "-01-01").attr("max", this.HeaderData.YearOfInsp + "-12-31");
            if (!this.IsEdit) {
                $(".custom-footer [finddetailsdep]").hide();
            }
        }
    }
    this.InitCTableStructure = function (tbl) {
        var body = tbl.find("tbody");
        body.empty();
        var data = this.HeaderData.AssetTypes;
        if (data) {
            this.Assets = eval("[" + data + "]")[0];
            this.InitRows(body, true);
            this.InitRows(body, false);
            var txtAvgWidth = body.find("input:text[txtavgwidth]");
            if (this.IsEdit) {
                txtAvgWidth.on("blur", function () {
                    var obj = $(this);
                    var tr = obj.closest("tr");
                    var ctype = tr.attr("ctype");
                    if (ctype == "L") {
                        tr[0].Asset.LAvgWidth = obj.val();
                    }
                    else if (ctype == "R") {
                        tr[0].Asset.RAvgWidth = obj.val();
                    }
                    else {
                        tr[0].Asset.AvgWidth = obj.val();
                    }
                    if (typeof (JSON.stringify) == "function") {
                        formFD.HeaderData.AssetTypes = JSON.stringify(formFD.Assets);
                    }
                });
            }
            else {
                txtAvgWidth.prop("disabled", true);
            }
            Validation.OnKeyPressInit();
        }
    }
    this.InitRows = function (body, isLeft) {
        var assets = this.Assets;
        var len = assets["DI"] ? assets["DI"].length : 0;
        len += assets["DR"] ? assets["DR"].length : 0;
        len += assets["SH"] ? assets["SH"].length : 0;
        var types = ["DI", "DR", "SH"];
        var typeNames = ["Ditch", "Drain", "Shoulder"];
        var hdr = isLeft ? "L<br/>E<br/>F<br/>T" : "R<br/>I<br/>G<br/>H<br/>T";
        var ctype = isLeft ? "L" : "R";
        for (var typ = 0; typ < types.length; typ++) {
            var elm = assets[types[typ]];
            if (elm) {
                for (var i = 0; i < elm.length; i++) {
                    var tr = $("<tr ctype='" + ctype + "'/>");
                    if (typ == 0) {
                        tr.append("<td class='fixed rowspantd' rowspan='" + len + "' style='line-height:13px;'>" + hdr + "<br/><br/>H<br/>A<br/>N<br/>D<br/><br/>S<br/>I<br/>D<br/>E</td>");
                    }
                    if (i == 0) {
                        tr.append("<td class='fixed rowspantd' rowspan='" + elm.length + "' style='width:70px; line-height:13px;'>" + typeNames[typ] + "</td>");
                    }
                    tr.attr("asset", ctype + "_" + types[typ] + "_" + elm[i].Desc);
                    tr[0].Asset = elm[i];
                    tr.append("<td class='fixed' style='width: 100px;'>" + elm[i].Desc + "</td>");
                    tr.append("<td class='fixed'>" + elm[i].Value + "</td>");
                    tr.append("<td class='fixed'><input value='" + (ctype == "L" ? (elm[i].LAvgWidth ? elm[i].LAvgWidth : '') : (elm[i].RAvgWidth ? elm[i].RAvgWidth : '')) + "' txtavgwidth type='text' style='width:50px;' onkeypressvalidate='cdecimal,5,3,Left " + elm[i].Desc + "'/></td>");
                    tr.append("<td class='fixed'>m</td>");
                    tr.append("<td condaftertd class='rfixed'>Km</td>");
                    tr.append("<td 1con class='rfixed'></td>");
                    tr.append("<td 2con class='rfixed'></td>");
                    tr.append("<td 3con class='rfixed'></td>");
                    tr.append("<td tcon class='rfixed'></td>");
                    body.append(tr);
                }
            }
        }
    }
    this.InitConditionTable = function () {
        var tbl = $("#tblFormFD");
        if (this.HeaderData && this.HeaderData.PkRefNo > 0) {
            this.InitCTableStructure(tbl);
            var dtl = this.HeaderData.InsDtl;
            var minKM = this.HeaderData.FrmCh;
            var maxKM = this.HeaderData.ToCh;
            //if (minKM > maxKM) { var maxTemp = minKM; minKM = maxKM; maxKM = maxTemp; }
            var asset = "";
            if (dtl && dtl.length > 0) {
                var th1 = tbl.find("thead tr:eq(0)");
                var th2 = tbl.find("thead tr:eq(1)");
                var thCon = th2.find("[condafterhd]");
                var iColCount = 0; var minchar, maxchar;
                var trs = tbl.find("tbody tr");
                if (minKM > maxKM) {
                    //debugger;
                    while (minKM > maxKM) {
                        iColCount += 5;
                        minchar = minKM.toFixed(3).replace(".", "+");
                        for (var i = 0; i < 5; i++) {
                            if (i > 0) { minKM -= 0.1; }
                            trs.each(function () {
                                $(this).find("[condaftertd]").before("<td km='" + minKM.toFixed(3) + "' fccondition class='fcconditiontd fcblock'><div condtion> </div></td>");
                            });
                        }
                        maxchar = (minKM + 0.099).toFixed(3).replace(".", "+");
                        thCon.before("<th colspan='5' class='kmrange'>" + minchar + " to " + maxchar + "</th>");
                        minKM -= 0.1;
                    }
                }
                else {
                    while (minKM < maxKM) {
                        iColCount += 5;
                        minchar = minKM.toFixed(3).replace(".", "+");
                        for (var i = 0; i < 5; i++) {
                            if (i > 0) { minKM += 0.1; }
                            trs.each(function () {
                                $(this).find("[condaftertd]").before("<td km='" + minKM.toFixed(3) + "' fccondition class='fcconditiontd fcblock'><div condtion> </div></td>");
                            });
                        }
                        maxchar = (minKM + 0.099).toFixed(3).replace(".", "+");
                        thCon.before("<th colspan='5' class='kmrange'>" + minchar + " to " + maxchar + "</th>");
                        minKM += 0.1;
                    }
                }

                th1.find("[condhd1]").attr("colspan", iColCount > 35 ? 35 : iColCount);
                if (iColCount > 35) {
                    var condhd1 = th1.find("[condhd1after]");
                    var icount = 35; var icol = 0;
                    while (iColCount > icount) {
                        icol = 5;
                        icount += icol;
                        if (iColCount < icount) { icol = icol - (icount - iColCount); }
                        condhd1.before("<th colspan='5'></th>");
                    }
                }
                $.each(dtl, function (idx, obj) {
                    asset = "";
                    var bound = obj.AiBound;
                    bound = bound == "Left" ? "L" : (bound == "Right" ? "R" : bound);
                    asset = bound + "_" + obj.AiAssetGrpCode + "_" + obj.AiGrpType;
                    if (asset != "") {
                        var _td = trs.filter("[asset='" + asset + "']").find("[km='" + obj.FromCHKm.toFixed(3) + "']");
                        if (_td.length > 0) {
                            _td.removeClass("fcblock");
                            _td[0].Asset = obj;
                            if (obj.Condition && obj.Condition > 0) { _td.find("[condtion]").attr("con", obj.Condition).text(obj.Condition); }
                        }
                    }
                });
            }
            tbl.show();
        }
        else {
            tbl.hide();
        }
        this.RefreshCondition();
        setTimeout(function () { formFD.RefreshConditionTable(); }, 200);

    }
    this.RefreshConditionTable = function () {
        var twidth = $("#tblFormFD").width();
        $("#tblFormFD .fixed").each(function () {
            var obj = $(this);
            obj.css("left", (obj.position().left - 1) + "px");
        });
        $("#tblFormFD .rfixed").each(function () {
            var obj = $(this);
            var rgt = twidth - (obj.position().left + obj.width()) - 3;
            rgt = rgt > 6 ? rgt : 0;
            obj.css("right", rgt + "px");
        });
        if (this.IsEdit) {
            $("#tblFormFD td[fccondition]:not(.fcblock)").on("click", function () {
                var obj = $(this);
                var csel = obj.find("div.fcconditionsel");
                if (csel.length == 0) { csel = $("#fcconditionseltemplate .fcconditionsel").clone(); csel.hide(); obj.append(csel); }
                if (csel.is(":visible")) { csel.slideUp(); }
                else { csel.slideDown(); }
            });
            $("#DetailListGrid").on("mouseleave", function () { $(this).find("div.fcconditionsel:visible").hide(); });
        }
    }
    this.ConSelect = function (tis, evt) {
        if (evt.stopImmediatePropagation) { event.stopImmediatePropagation(); }
        if (evt.stopPropagation) { event.stopPropagation(); }
        var obj = $(tis);
        var td = obj.closest("td");
        td[0].Asset.Condition = obj.attr("val");
        td.find("[condtion]").attr("con", obj.attr("val")).text(obj.attr("val"));
        td.find("div.fcconditionsel").hide();
        this.RefreshCondition(td.closest("tr"));
    }
    this.RefreshCondition = function (trs) {
        var len = 0; tlen = 0;
        if (!trs) { trs = $("#tblFormFD tbody tr"); }

        trs.each(function () {
            var obj = $(this);
            len = obj.find("[con='1']").length
            obj.find("td[1con]").text(len > 0 ? (len / 10) : "");
            tlen = len;
            len = obj.find("[con='2']").length
            obj.find("td[2con]").text(len > 0 ? (len / 10) : "");
            tlen += len;
            len = obj.find("[con='3']").length
            obj.find("td[3con]").text(len > 0 ? (len / 10) : "");
            tlen += len;
            obj.find("td[tcon]").text(tlen > 0 ? (tlen / 10) : "");
        });
    }
    this.HeaderGrid = new function () {
        this.ActionRender = function (data, type, row, meta) {
            var actionSection = "<div class='btn-group dropright' rowidx='" + meta.row + "'><button type='button' class='btn btn-sm btn-themebtn dropdown-toggle' data-toggle='dropdown'> Click Me </button>";
            actionSection += "<div class='dropdown-menu'>";//dorpdown menu start

            if (data.Status != "Submitted" && tblFDHeaderGrid.Base.IsModify) {
                actionSection += "<button type='button' class='dropdown-item editdel-btns' onclick='formFD.HeaderGrid.ActionClick(this);'>";
                actionSection += "<span class='edit-icon'></span> Edit </button>";
            }
            if (tblFDHeaderGrid.Base.IsView) {
                actionSection += "<button type='button' class='dropdown-item editdel-btns' onclick='formFD.HeaderGrid.ActionClick(this);'>";
                actionSection += "<span class='view-icon'></span> View </button>";
            }
            if (tblFDHeaderGrid.Base.IsDelete) {
                actionSection += "<button type='button' class='dropdown-item editdel-btns' onclick='formFD.HeaderGrid.ActionClick(this);'>";
                actionSection += "<span class='del-icon'></span> Delete </button>";
            }
            actionSection += "<button type='button' class='dropdown-item editdel-btns' onclick='formFD.HeaderGrid.ActionClick(this);'>";
            actionSection += "<span class='print-icon'></span> Print </button>";

            actionSection += "</div>"; //dorpdown menu close
            actionSection += "</div>"; // action close

            return actionSection;
        }
        this.ActionClick = function (tis) {
            var obj = $(tis);
            var type = $.trim(obj.text());
            var rowidx = parseInt(obj.closest("[rowidx]").attr("rowidx"), 10);
            if (rowidx >= 0) {
                var data = tblFDHeaderGrid.dataTable.row(rowidx).data();
                switch (type.toLowerCase()) {
                    case "edit":
                        window.location = _APPLocation + "FormFD/Edit/" + data.RefNo;
                        break;
                    case "view":
                        window.location = _APPLocation + "FormFD/View/" + data.RefNo;
                        break;
                    case "delete":
                        app.Confirm("Are you sure you want to delete this record? <br/>(Ref: " + data.RefID + ")", (status) => {
                            if (status) {
                                DeleteRequest("Delete/" + data.RefNo, "FormFD", {}, function (sdata) {
                                    tblFDHeaderGrid.Refresh();
                                    app.ShowSuccessMessage("Deleted Sucessfully! <br/>(Ref: " + data.RefID + ")");
                                });
                            }
                        }, "Yes", "No");
                        break;
                    case "print":
                        window.location = _APPLocation + "FormFD/download?id=" + data.RefNo;
                        break;
                }
            }
        }
        this.DateOfEntry = (data, type, row, meta) => {
            var result = "";
            if (row.InsDate && row.InsDate != null && row.InsDate != "") {
                result = (new Date(row.InsDate)).ToString(jsMaster.DisplayDateFormat);
                result = " (" + result + ")";
            }
            result = data + result;
            return result;
        }
        this.DateOfIns = (data, type, row, meta) => {
            var result = "";
            if (data && data != "") {
                result = (new Date(data)).ToString(jsMaster.GridFormat);
            }
            return result;
        }
    }
    this.Search = new function () {
        this.SecCodeChange = function (tis) {
            var sel = $(tis);
            var obj = sel.find("option:selected");
            $("#txtSectionName").val(sel.val());
            var rmu = $("#selRMU");
            var val = obj.attr("cvalue");
            if (val) { rmu.val(val == "Batu Niah" ? "BTN" : "MRI").trigger("chosen:updated"); }
            var div = $("#selDivision");
            if (div.val() == "") { div.val("MIRI").trigger("chosen:updated"); }
            formFD.FilterRoadCode();
        }
        this.RoadCodeChange = function (tis) {
            var sel = $(tis);
            var obj = sel.find("option:selected")
            $("#txtRoadName").val(obj.attr("Item1"));
            if (sel.val() != "") {
                var sec = $("#selSectionCode");
                if (sec.val() == "") { sec.val(sec.find("[code='" + obj.attr("item2") + "']").val()).trigger("chosen:updated"); $("#txtSectionName").val(sec.val()); }
                var rmu = $("#selRMU");
                if (rmu.val() == "") { rmu.val(obj.attr("cvalue")).trigger("chosen:updated"); }
                var div = $("#selDivision");
                if (div.val() == "") { div.val("MIRI").trigger("chosen:updated"); }
            }
            formFD.BindRefNumber();
        }
        this.RmuChange = function (tis) {
            if (tis.value != "") {
                var sec = $("#selSectionCode"); sec.find("option").hide().filter("[cvalue='" + $(tis).find("option:selected").attr("cvalue") + "']").show();
                if (sec.find("option:selected:visible").length == 0) { sec.val("").trigger("change"); }
                sec.trigger("chosen:updated");
            }
            else {
                var ctrl = $("#selSectionCode"); ctrl.find("option:hidden").show(); ctrl.val("").trigger("change").trigger("chosen:updated");
            }
            formFD.FilterRoadCode();

            var div = $("#selDivision");
            if (div.val() == "") { div.val("MIRI").trigger("chosen:updated"); }
        }
    }
    this.FilterRoadCode = function () {
        var asset = $("#selRoadCode");
        if (asset.length > 0) {
            var opt = asset.find("option").show();

            var rmu = $("#selRMU");
            if (rmu.val() != "") { opt.filter(":not([cvalue='" + rmu.val() + "'])").hide(); }

            var sec = $("#selSectionCode");
            if (sec.val() != "") { opt.filter(":not([item2='" + sec.find("option:selected").attr("code") + "'])").hide(); }

            asset.val("").trigger("change").trigger("chosen:updated");
        }
    }
    this.InsYearChange = function (tis) { this.BindRefNumber(); }
    this.BindRefNumber = function () {
        var tis = this;
        var yr = $("#formFDInsYear").val();
        var assid = $("#selRoadCode");
        if (yr && yr != "" && assid.val() != "") {
            $("#formFDRefNO").val(tis.Pattern.replace("{RoadCode}", assid.val()).replace("{Year}", yr));
        }
        else {
            $("#formFDRefNO").val("");
        }
    }
    this.CrewLeaderChange = function (tis) {
        var sel = $(tis);
        var opt = sel.find(":selected");
        var item1 = opt.attr("item1") ? opt.attr("item1") : "";
        if (item1 == "others") {
            $("#txtCrewLeaderName").val("").addClass("validate").prop("disabled", false);
        }
        else {
            $("#txtCrewLeaderName").val(item1).removeClass("validate").prop("disabled", true);
        }
    }
    this.UserIDChange = function (tis) {
        var sel = $(tis);
        var opt = sel.find(":selected");
        var item1 = opt.attr("item1") ? opt.attr("item1") : "";
        if (item1 == "others") {
            $("#txtUserNameInspBy").val("").addClass("validate").prop("disabled", false);
            $("#txtUserDesignationInspBy").val("").addClass("validate").prop("disabled", false);
        }
        else {
            var item2 = opt.attr("Item2") ? opt.attr("item2") : "";
            $("#txtUserNameInspBy").val(item1).removeClass("validate").prop("disabled", true);
            $("#txtUserDesignationInspBy").val(item2).removeClass("validate").prop("disabled", true);
        }
    }
    this.Save = function (isSubmit) {
        var tis = this;
        if (isSubmit) {
            $("#frmFDHeader .svalidate").addClass("validate");
        }
        if (ValidatePage("#frmFDHeader", "", "")) {
            var action = isSubmit ? "Submit" : "Save";
            GetResponseValue(action, "FormFD", FormValueCollection("#frmFDHeader", tis.HeaderData), function (data) {
                app.ShowSuccessMessage('Successfully Saved', false);
                setTimeout(tis.NavToList, 2000);
            }, "Saving");
        }
        if (isSubmit) {
            $("#frmFDHeader .svalidate").removeClass("validate");
        }
    }
    this.NavToList = function () {
        window.location = _APPLocation + "FormFD";
    }
    this.Cancel = function () {
        jsMaster.ConfirmCancel(() => { formFD.NavToList(); });
    }
}
$(document).ready(function () {

    formFD.PageInit();
    $("#smartSearch").focus();
    if ($("#btnFindDetails:visible").length > 0) {
        setTimeout(function () { $('#selDivision').trigger('chosen:activate'); }, 200);
    }
    else {
        setTimeout(function () { $("#selCrewLeaderName").trigger('chosen:activate'); }, 200);
    }

})