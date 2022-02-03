var formF5 = new function () {
    this.HeaderData = null;
    this.PageInit = function () {
        if (formF5.HeaderData && formF5.HeaderData.PkRefNo && formF5.HeaderData.PkRefNo > 0) {
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

            if (data.Status != "Submitted" && tblF5HeaderGrid.Base.IsModify) {
                actionSection += "<button type='button' class='dropdown-item editdel-btns' onclick='formF5.HeaderGrid.ActionClick(this);'>";
                actionSection += "<span class='edit-icon'></span> Edit </button>";
            }
            if (tblF5HeaderGrid.Base.IsView) {
                actionSection += "<button type='button' class='dropdown-item editdel-btns' onclick='formF5.HeaderGrid.ActionClick(this);'>";
                actionSection += "<span class='view-icon'></span> View </button>";
            }
            if (tblF5HeaderGrid.Base.IsDelete) {
                actionSection += "<button type='button' class='dropdown-item editdel-btns'onclick='formF5.HeaderGrid.ActionClick(this)'>";
                actionSection += "<span class='del-icon'></span> Delete </button>";
            }
            actionSection += "<button type='button' class='dropdown-item editdel-btns'onclick='formF5.HeaderGrid.ActionClick(this)'>";
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
                var data = tblF5HeaderGrid.dataTable.row(rowidx).data();
                switch (type.toLowerCase()) {
                    case "edit":
                        window.location = _APPLocation + "FormF5/Edit/" + data.RefNo;
                        break;
                    case "view":
                        window.location = _APPLocation + "FormF5/View/" + data.RefNo;
                        break;
                    case "delete":
                        app.Confirm("Are you sure you want to delete this record? <br/>(Ref: " + data.RefID + ")", (status) => {
                            if (status) {
                                DeleteRequest("Delete/" + data.RefNo, "FormF5", {}, function (sdata) {
                                    tblF5HeaderGrid.Refresh();
                                    app.ShowSuccessMessage("Deleted Sucessfully! <br/>(Ref: " + data.RefID + ")");
                                });
                            }
                        }, "Yes", "No");
                        break;
                    case "print":
                        window.location = _APPLocation + "FormF5/FormF5Download/" + data.RefNo;
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
            var par = $("#divF5HeaderInfo");
            var refNo = $("#FormF5RefNo");
            refNo[0].PkID = data.PkRefNo;
            refNo[0].RefId = data.FormRefId;
            refNo.val(data.FormRefId);
            par.find("#F5HdrCrewId").val(data.CrewLeaderId).trigger("change").trigger("chosen:updated");
            par.find("#F5CrewLeaderName").val(data.CrewLeaderName);
            par.find("#F5HdrUserId").val(data.UserIdInspBy).trigger("change").trigger("chosen:updated");
            par.find("#F5UserameInspBy").val(data.UserNameInspBy);
            par.find("#F5UserDesignationInspBy").val(data.UserDesignationInspBy);
            if (data.DtInspBy && data.DtInspBy != null) {
                par.find("#F5DtInspBy").val((new Date(data.DtInspBy)).ToString(assignFormat))
            }
        }
        this.Header = (data) => {
            var assignFormat = jsMaster.AssignFormat;
            var par = $("#divF5FindDetails");
            par.find("#F5HdrRdCode").val(data.RoadCode).trigger("change").trigger("chosen:updated");
            par.find("#F5HdrYear").val(data.YearOfInsp).trigger("change").trigger("chosen:updated");
        }
    }
    this.GenRefNumber = () => {
        var refNo = $("#FormF5RefNo");
        var rdCode = $("#F5HdrRdCode").val();
        var year = $("#F5HdrYear").val();

        if (rdCode != "" && year != "") {
            var frmt = "CI/Form F5/{RMU}/{Year}";
            refNo.val(frmt.replace("{RMU}", rdCode).replace("{Year}", year));
        }
    }
    this.FindDetails = () => {
        if (ValidatePage("#divF5FindDetails", "", "")) {
            GetResponseValue("FindDetails", "FormF5", FormValueCollection("#divF5FindDetails"), function (data) {
                if (data && data != null) {
                    formF5.Bind.Header(data);
                    formF5.Bind.HeaderInfo(data);
                    formF5.HeaderData = data;
                    formF5.PageInit();
                    $("[finddetailhide]").hide();
                    $("#F5HdrRdCode,#F5HdrYear").prop("disabled", true).trigger("change").trigger("chosen:updated");
                    tblF5DtlGrid.dataTable.settings()[0].ajax.url = "/FormF5/DetailList/" + data.PkRefNo;
                    tblF5DtlGrid.Refresh();
                    if (data.SubmitSts) {
                        formF5.ResetToViewMode();
                    }
                }
                $("[saveSubmitBtn]").show();
            }, "Finding");
        }
    }
    this.ResetToViewMode = function () {
        $("#findDetails").remove();
        $(".custom-footer button[saveSubmitBtn]").remove();
        var obj = $("#divF5HeaderInfo");
        obj.find("input,textarea").prop("disabled", true);
        obj.find("select").prop("disabled", true).trigger("chosen:updated");
    }
    this.Save = (isSubmit) => {
        var tis = (this);
        if (isSubmit) {
            $("#divF5HeaderInfo .svalidate").addClass("validate");
        }
        else {
            $("#divF5HeaderInfo .svalidate").removeClass("validate");
        }
        Validation.ResetErrStyles("#divF5HeaderInfo");
        if (ValidatePage("#divFormF5", "", "")) {
            var refNo = $("#FormF5RefNo");
            var action = isSubmit ? "Submit" : "Save";
            GetResponseValue(action, "FormF5", FormValueCollection("#divFormF5", { PkRefNo: refNo[0].PkID }), function (data) {
                if (!refNo[0].PkID || refNo[0].PkID == 0) {
                    tblF5DtlGrid.dataTable.settings()[0].ajax.url = "/FormF5/DetailList/" + data.Id;
                }
                refNo[0].PkID = data.value.pkRefNo;
                refNo.val(data.value.formRefId);
                app.ShowSuccessMessage("Successfully Saved", false);
                setTimeout(tis.NavToList, 2000);
            }, "Saving");
        }
        if (isSubmit) {
            $("#divF5HeaderInfo .svalidate").removeClass("validate");
        }
    }
    this.Cancel = () => {
        jsMaster.ConfirmCancel(() => { formF5.NavToList(); })
    }
    this.NavToList = () => {
        window.location = _APPLocation + "FormF5";
    }
    this.F5RoadCodeChange = (tis, isAdd) => {
        var ctrl = $(tis);
        if (ctrl.val() != null) {
            var req = {};
            if (isAdd) {
                req.RMU = $("[rmuCode]").attr("code");
                req.SectionCode = $("[sectionCode]").attr("code");
            }
            else {
                req.RMU = "";//$("[rmuCode]").attr("code");
                req.SectionCode = "";// $("[sectionCode]").attr("code");               
            }
            req.RdCode = ctrl.find("option:selected").attr("code");
            req.GrpCode = "BR"
            formF5.DropDownBind(req);
            $("[F5RoadName]").val(ctrl.find("option:selected").attr("Item1"));
            $("#F5HdrRdLength").val(ctrl.find("option:selected").attr("item3"));
            $("#RoadId").val(ctrl.find("option:selected").attr("pkid"))
        }
        else {
            $("[F5RoadName]").val('');
            $("#F5HdrRdLength").val('');
            $("#RoadId").val('');
        }
    }

    this.F5SectionCodeChange = (tis, isAdd) => {
        var ctrl = $(tis);
        if (ctrl.val() != null) {
            var req = {};
            if (isAdd) {
                req.RMU = $("[rmuCode]").attr("code");

            }
            else {
                req.RMU = '';//$("[rmuCode]").attr("code");
            }
            req.SectionCode = ctrl.find("option:selected").attr("code");
            req.RdCode = '';
            req.GrpCode = "BR"
            formF5.DropDownBind(req);
            $("[F5SectionName]").val(ctrl.find("option:selected").attr("value"));
        }
        else
            $("[F5SectionName]").val('');
    }
    this.F5CrewLeader = (tis, isView) => {
        var ctrl = $(tis);
        var opt = ctrl.find(":selected");
        var item1 = opt.attr("item1") ? opt.attr("item1") : "";
        if (item1 == "others" && !isView) {
            $("#F5CrewLeaderName").val("").addClass("validate").prop("disabled", false);
        }
        else {
            $("#F5CrewLeaderName").val(item1).removeClass("validate").prop("disabled", true);
        }
    }
    this.F5UserDetails = (tis, isView) => {
        var ctrl = $(tis);
        var opt = ctrl.find(":selected");
        var item1 = opt.attr("item1") ? opt.attr("item1") : "";
        if (item1 == "others" && !isView) {
            $("#F5UserameInspBy").val("").addClass("validate").prop("disabled", false);
            $("#F5UserDesignationInspBy").val("").addClass("validate").prop("disabled", false);
        }
        else {
            var item2 = opt.attr("Item2") ? opt.attr("item2") : "";
            $("#F5UserameInspBy").val(item1).removeClass("validate").prop("disabled", true);
            $("#F5UserDesignationInspBy").val(item2).removeClass("validate").prop("disabled", true);
        }
    }
    this.F5RMUChange = (tis) => {
        var ctrl = $(tis);
        if (ctrl.val() != null) {
            var req = {};
            req.RMU = ctrl.find("option:selected").attr("code")
            req.SectionCode = '';
            req.RdCode = '';
            req.GrpCode = "BR"
            formF5.DropDownBind(req);

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
                    $("[F5SectionName]").val(_sec.find("option:selected").attr("value"));
                }
                if (req.RdCode == "") {
                    _road.empty();
                    _road.append($("<option></option>").val("").html("Select Road Code"));
                    $.each(data.rdCode, function (index, v) {
                        _road.append($("<option></option>").val(v.value).html(v.text).attr("Item1", v.item1).attr("Item3", v.item3).attr("PKId", v.pkId).attr("code", v.code));
                    });
                    _road.trigger("chosen:updated");
                    $("[F5RoadName]").val(_road.find("option:selected").attr("Item1"));
                    $("#F5HdrRdLength").val(_road.find("option:selected").attr("item3"));
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
    var refNo = $("#FormF5RefNo");
    formF5.PageInit();
    if (formF5.HeaderData && formF5.HeaderData != null) {
        formF5.Bind.Header(formF5.HeaderData);
        formF5.Bind.HeaderInfo(formF5.HeaderData);
    }
    else {
        $("[rmuCode]").trigger("change");
    }
    if (refNo.val() == '') {
        $("[refnumber]").on("change", function () {
            formF5.GenRefNumber();
        })
        refNo[0].PkID = 0;
        $("[saveSubmitBtn]").hide();
    }
    else {
        $("#divF5FindDetails [findDetails]").remove();
        $("[saveSubmitBtn]").show();
    }

    //Listener for Smart and Detail Search
    $("#F5SrchSection").find("#smartSearch").focus();
    element = document.querySelector("#formF5AdvSearch");
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
