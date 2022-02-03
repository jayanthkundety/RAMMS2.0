var formF4 = new function () {
    this.HeaderData = null;
    this.PageInit = function () {
        if (formF4.HeaderData && formF4.HeaderData.PkRefNo && formF4.HeaderData.PkRefNo > 0) {
            $("[saveSubmitBtn]").show();
            $("[findDetails]").hide();
        }
        else {
            $("[saveSubmitBtn]").hide();
            $("[findDetails]").show();
        }
        //this.Bind();
    }
    this.HeaderGrid = new function () {
        this.ActionRender = function (data, type, row, meta) {
            var actionSection = "<div class='btn-group dropright' rowidx='" + meta.row + "'><button type='button' class='btn btn-sm btn-themebtn dropdown-toggle' data-toggle='dropdown'> Click Me </button>";
            actionSection += "<div class='dropdown-menu'>";//dorpdown menu start

            if (data.Status != "Submitted" && tblF4HeaderGrid.Base.IsModify) {
                actionSection += "<button type='button' class='dropdown-item editdel-btns' onclick='formF4.HeaderGrid.ActionClick(this);'>";
                actionSection += "<span class='edit-icon'></span> Edit </button>";
            }
            if (tblF4HeaderGrid.Base.IsView) {
                actionSection += "<button type='button' class='dropdown-item editdel-btns' onclick='formF4.HeaderGrid.ActionClick(this);'>";
                actionSection += "<span class='view-icon'></span> View </button>";
            }
            if (tblF4HeaderGrid.Base.IsDelete) {
                actionSection += "<button type='button' class='dropdown-item editdel-btns'onclick='formF4.HeaderGrid.ActionClick(this)'>";
                actionSection += "<span class='del-icon'></span> Delete </button>";
            }
            actionSection += "<button type='button' class='dropdown-item editdel-btns'onclick='formF4.HeaderGrid.ActionClick(this)'>";
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
                var data = tblF4HeaderGrid.dataTable.row(rowidx).data();
                switch (type.toLowerCase()) {
                    case "edit":
                        window.location = _APPLocation + "FormF4/Edit/" + data.RefNo;
                        break;
                    case "view":
                        window.location = _APPLocation + "FormF4/View/" + data.RefNo;
                        break;
                    case "delete":
                        app.Confirm("Are you sure you want to delete this record? <br/>(Ref: " + data.RefID + ")", (status) => {
                            if (status) {
                                DeleteRequest("Delete/" + data.RefNo, "FormF4", {}, function (sdata) {
                                    tblF4HeaderGrid.Refresh();
                                    app.ShowSuccessMessage("Deleted Sucessfully! <br/>(Ref: " + data.RefID + ")");
                                });
                            }
                        }, "Yes", "No");
                        break;
                    case "print":
                        window.location = _APPLocation + "FormF4/FormF4Download/" + data.RefNo;
                        break;
                }
            }
        }
        this.DateOfIns = (data, type, row, meta) => {
            var result = "";
            if (data && data != "") {
                result = (new Date(data)).ToString(jsMaster.GridFormat);
            }
            return result;
        }
    }
    this.Bind = new function () {
        this.HeaderInfo = (data) => {
            var assignFormat = jsMaster.AssignFormat;
            var par = $("#divF4HeaderInfo");
            var refNo = $("#FormF4RefNo");
            refNo[0].PkID = data.PkRefNo;
            refNo[0].RefId = data.FormRefId;
            refNo.val(data.FormRefId);
            par.find("#F4HdrCrewId").val(data.CrewLeaderId).trigger("change").trigger("chosen:updated");
            par.find("#F4HdrUserId").val(data.UserIdInspBy).trigger("change").trigger("chosen:updated");
            par.find("#F4CrewLeaderName").val(data.CrewLeaderName);
            par.find("#F4UserameInspBy").val(data.UserNameInspBy);
            par.find("#F4UserDesignationInspBy").val(data.UserDesignationInspBy);
            if (data.DtInspBy && data.DtInspBy != null) {
                par.find("#F4DtInspBy").val((new Date(data.DtInspBy)).ToString(assignFormat))
            }
        }
        this.Header = (data) => {
            var assignFormat = jsMaster.AssignFormat;
            var par = $("#divF4FindDetails");
            par.find("#F4HdrRdCode").val(data.RoadCode).trigger("change").trigger("chosen:updated");
            par.find("#F4HdrYear").val(data.YearOfInsp).trigger("change").trigger("chosen:updated");
        }
    }
    this.GenRefNumber = () => {
        var refNo = $("#FormF4RefNo");
        var rdCode = $("#F4HdrRdCode").val();
        var year = $("#F4HdrYear").val();

        if (rdCode != "" && year != "") {
            var frmt = "CI/Form F4/{RMU}/{Year}";
            refNo.val(frmt.replace("{RMU}", rdCode).replace("{Year}", year));
        }
    }
    this.FindDetails = () => {
        if (ValidatePage("#divF4FindDetails", "", "")) {
            GetResponseValue("FindDetails", "FormF4", FormValueCollection("#divF4FindDetails"), function (data) {
                if (data && data != null) {
                    formF4.Bind.Header(data);
                    formF4.Bind.HeaderInfo(data);
                    formF4.HeaderData = data;
                    formF4.PageInit();
                    $("[finddetailhide]").hide();
                    $("#F4HdrRdCode,#F4HdrYear").prop("disabled", true).trigger("change").trigger("chosen:updated");
                    tblF4DtlGrid.dataTable.settings()[0].ajax.url = "/FormF4/DetailList/" + data.PkRefNo;
                    tblF4DtlGrid.Refresh();
                    if (data.SubmitSts) {
                        formF4.ResetToViewMode();
                    }
                }
                $("[saveSubmitBtn]").show();
            }, "Finding");
        }
    }
    this.ResetToViewMode = function () {
        $("#findDetails").remove();
        $(".custom-footer button[saveSubmitBtn]").remove();
        var obj = $("#divF4HeaderInfo");
        obj.find("input,textarea").prop("disabled", true);
        obj.find("select").prop("disabled", true).trigger("chosen:updated");
    }
    this.Save = (isSubmit) => {
        var tis = (this);
        if (isSubmit) {
            $("#divF4HeaderInfo .svalidate").addClass("validate");
        }
        else {
            $("#divF4HeaderInfo .svalidate").removeClass("validate");
        }
        Validation.ResetErrStyles("#divF4HeaderInfo");
        if (ValidatePage("#divFormF4", "", "")) {
            var refNo = $("#FormF4RefNo");
            var action = isSubmit ? "Submit" : "Save";
            GetResponseValue(action, "FormF4", FormValueCollection("#divFormF4", { PkRefNo: refNo[0].PkID }), function (data) {
                if (!refNo[0].PkID || refNo[0].PkID == 0) {
                    tblF4DtlGrid.dataTable.settings()[0].ajax.url = "/FormF4/DetailList/" + data.Id;
                }
                if (data.value.formRefId != null) {
                    refNo[0].PkID = data.value.pkRefNo;
                    refNo.val(data.value.formRefId);
                }
                app.ShowSuccessMessage("Successfully Saved", false);
                setTimeout(tis.NavToList, 2000);
            }, "Saving");
        }
        if (isSubmit) {
            $("#divF4HeaderInfo .svalidate").removeClass("validate");
        }
    }
    this.Cancel = () => {
        jsMaster.ConfirmCancel(() => { formF4.NavToList(); })
    }
    this.NavToList = () => {
        window.location = _APPLocation + "FormF4";
    }
    this.F4RoadCodeChange = (tis, isAdd) => {
        var ctrl = $(tis);
        if (ctrl.val() != null) {
            var req = {};
            if (isAdd) {
                req.RMU = $("[rmuCode]").attr("code");
                req.SectionCode = $("[sectionCode]").attr("code");
            }
            else {
                req.RMU = "";
                req.SectionCode = "";
            }
            req.RdCode = ctrl.find("option:selected").attr("code");
            req.GrpCode = "CV"
            formF4.DropDownBind(req);
            $("[F4RoadName]").val(ctrl.find("option:selected").attr("Item1"));
            $("#F4HdrRdLength").val(ctrl.find("option:selected").attr("item3"));
            $("#RoadId").val(ctrl.find("option:selected").attr("pkid"))
        }
        else {
            $("[F4RoadName]").val('');
            $("#F4HdrRdLength").val('');
            $("#RoadId").val('');
        }
    }

    this.F4SectionCodeChange = (tis, isAdd) => {
        var ctrl = $(tis);
        if (ctrl.val() != null) {
            var req = {};
            if (isAdd) {
                req.RMU = $("[rmuCode]").attr("code");
            }
            else {
                req.RMU = ""
            }
            req.SectionCode = ctrl.find("option:selected").attr("code");
            req.RdCode = '';
            req.GrpCode = "CV"
            formF4.DropDownBind(req);
            $("[F4SectionName]").val(ctrl.find("option:selected").attr("value"));
        }
        else
            $("[F4SectionName]").val('');
    }
    this.F4CrewLeader = (tis, isView) => {
        var ctrl = $(tis);
        var opt = ctrl.find(":selected");
        var item1 = opt.attr("item1") ? opt.attr("item1") : "";
        if (item1 == "others" && !isView) {
            $("#F4CrewLeaderName").val("").addClass("validate").prop("disabled", false);
        }
        else
            $("#F4CrewLeaderName").val(item1).removeClass("validate").prop("disabled", true);
    }
    this.F4UserDetails = (tis, isView) => {
        var ctrl = $(tis);
        var opt = ctrl.find(":selected");
        var item1 = opt.attr("item1") ? opt.attr("item1") : "";
        if (item1 == "others" && !isView) {
            $("#F4UserameInspBy").val("").addClass("validate").prop("disabled", false);
            $("#F4UserDesignationInspBy").val("").addClass("validate").prop("disabled", false);
        }
        else {
            var item2 = opt.attr("Item2") ? opt.attr("item2") : "";
            $("#F4UserameInspBy").val(item1).removeClass("validate").prop("disabled", true);
            $("#F4UserDesignationInspBy").val(item2).removeClass("validate").prop("disabled", true);
        }
    }
    this.F4RMUChange = (tis) => {
        var ctrl = $(tis);
        if (ctrl.val() != null) {
            var req = {};
            req.RMU = ctrl.find("option:selected").attr("code")
            req.SectionCode = '';
            req.RdCode = '';
            req.GrpCode = "CV"
            formF4.DropDownBind(req);

        }
    }
    this.DropDownBind = (req) => {
        _rmu = $("[rmuCode]");
        _sec = $("[sectionCode]");
        _road = $("[roadCode]");
        $.ajax({
            url: '/FormF2/RMUSecRoad',
            dataType: 'JSON',
            data: req,
            type: 'Post',
            success: function (data) {
                if (req.RMU == "") {
                    _rmu.empty();
                    if (data.rmu.length != 1) {
                        _rmu.append($("<option></option>").val("").html("Select RMU"));
                    }
                    $.each(data.rmu, function (index, x) {
                        _rmu.append($("<option></option>").val(x.value).html(x.text));
                    });
                    _rmu.trigger("chosen:updated");
                }
                if (req.SectionCode == "") {
                    _sec.empty();
                    if (data.section.length != 1) {
                        _sec.append($("<option></option>").val("").html("Select Section Code"));
                    }
                    $.each(data.section, function (index, v) {
                        _sec.append($("<option></option>").val(v.value).html(v.text).attr("code", v.code));
                    });
                    _sec.trigger("chosen:updated");
                    $("[F4SectionName]").val(_sec.find("option:selected").attr("value"));
                }
                if (req.RdCode == "") {
                    _road.empty();
                    _road.append($("<option></option>").val("").html("Select Road Code"));
                    $.each(data.rdCode, function (index, v) {
                        _road.append($("<option></option>").val(v.value).html(v.text).attr("Item1", v.item1).attr("Item3", v.item3).attr("PKId", v.pkId).attr("code", v.code));
                    });
                    _road.trigger("chosen:updated");
                    $("[F4RoadName]").val(_road.find("option:selected").attr("Item1"));
                    $("#F4HdrRdLength").val(_road.find("option:selected").attr("item3"));
                    $("#RoadId").val(_road.find("option:selected").attr("pkid"))
                }
            },
            error: function (data) {

                console.error(data);
            }
        });
    }

}
$(document).ready(function () {
    var refNo = $("#FormF4RefNo");
    formF4.PageInit();
    if (formF4.HeaderData && formF4.HeaderData != null) {
        formF4.Bind.Header(formF4.HeaderData);
        formF4.Bind.HeaderInfo(formF4.HeaderData);
    }
    else {
        $("[rmuCode]").trigger("change");
    }
    if (refNo.val() == '') {
        $("[refnumber]").on("change", function () {
            formF4.GenRefNumber();
        })
        refNo[0].PkID = 0;
        $("[saveSubmitBtn]").hide();
    }
    else {
        $("#divF4FindDetails [findDetails]").remove();
        $("[saveSubmitBtn]").show();
    }

    //Listener for Smart and Detail Search
    $("#F4SrchSection").find("#smartSearch").focus();
    element = document.querySelector("#formF4AdvSearch");
    if (element) {
        element.addEventListener("keyup", () => {
            if (event.keyCode === 13) {
                $('[searchsectionbtn]').trigger('onclick');
            }
        });
    }
    $("#smartSearch").keyup(function () {
        if (event.keyCode === 13) {
            $('[searchsectionbtn]').trigger('onclick');
        }
    })
})
