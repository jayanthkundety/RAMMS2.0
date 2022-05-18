var formS1 = new function () {
    this.HeaderData = null;
    this.WeekNoChange = function (tis) {
        
       
        var week = $(tis).val();
        var selectDate = $("#selecDate").val();
        if (selectDate != "" && week != "") {
            var format = "YYYY-MM-DD"; var assignFormat = "YYYY-MM-DD";
            var date = selectDate.ToDate(format);
            var frmDate = jsMaster.getDateOfISOWeek(week, date.getFullYear());
            $('#frmDate').val(frmDate.ToString(assignFormat));
            frmDate.setDate(frmDate.getDate() + 6)
            $('#toDate').val(frmDate.ToString(assignFormat));
            ValidatePage('#headerFindDiv');
        }
        else {
            
            $('#frmDate,#toDate').val("");
            $('#drpWeekNo').val("").trigger("chosen:updated");
            ValidatePage('#headerFindDiv');
            //if ($('#formADetSrchRMU').val() != "") {
            //    $('#formADetSrchRMU').removeClass("validate");
            //}
            //app.ShowErrorMessage("Select Date first and then Week Number");
            //alert("Select Date first and then Week Number");
        }
        //$("#frmS1HeaderData .svalidate").removeClass("validate");
      
    }
    this.GenRefNumber = () => {
        var refNo = $("#txtS1RefNumber");
        var rmu = $("#formADetSrchRMU").val();
        var selectDate = $("#selecDate").val();
        var weekno = $("#drpWeekNo").val();
        if (rmu != "" && selectDate != "" && weekno != "") {
            var frmt = "MM/Form S1/{RMU}/{Date}/{WeekNo}/????";
            refNo.val(frmt.replace("{RMU}", rmu).replace("{Date}", selectDate).replace("{WeekNo}", weekno));
        }
    }
    this.ResetToViewMode = function () {
        $("#btnAddDetails").remove();
        $(".custom-footer button[depfinddetail]").remove();
        var obj = $("#divFS1HeaderInfo");
        obj.find("input,textarea").prop("disabled", true);
        obj.find("select").prop("disabled", true).trigger("chosen:updated");
    }
    this.FindDetails = function () {
         
        if (ValidatePage("#divFindDetails", "", "")) {
            GetResponseValue("FindDetails", "FormS1", FormValueCollection("#divFindDetails"), function (data) {
                if (data && data != null) {
                    $("#divFindDetails").find("input,select").each(function () { $(this).prop("disabled", true); }).find("button").hide();
                    $("#divFindDetails").find("select").trigger("chosen:updated");
                   
                    var dsRefNo = data.RefNoDS;

                    if (dsRefNo.length > 0) {
                        $("#ddlRefNo").empty();
                        $("#ddlRefNo").append($("<option></option>").val("0").html("Select Reference No"));
                        $.each(dsRefNo, function (index, v) {
                            $("#ddlRefNo").append($("<option></option>").val(v.Value).html(v.Text));
                        });
                        $("#ddlRefNo").val(data.S2PKRefNo)
                        $("#ddlRefNo").trigger("chosen:updated");
                        $("#ddlRefNo").prop('disabled', false).trigger("chosen:updated");

                    }

                    formS1.Bind.Header(data); // find get new also
                    formS1.Bind.HeaderInfo(data);
                    formS1.HeaderData = data;
                    formS1.ShowAddDetailsButton();
                    var refNo = $("#txtS1RefNumber");
                    tblFS1DetailGrid.dataTable.settings()[0].ajax.url = "/FormS1/ListDetail/" + data.PkRefNo;
                    formS1.UpdateDaySchColName();
                    $("[finddetails]").remove();
                    tblFS1DetailGrid.Refresh();
                    if (data.SubmitSts) {
                        formS1.ResetToViewMode();
                    }
                }
                else {
                    var par = $("#divFS1HeaderInfo");
                    par.find("#FsihRemarks,#FsiihDtPlan,#FsiihDtVet,#FsiihDtAgrd").val("");
                    par.find("[userName],[userDest]").val("");
                    par.find("#formS1UserIdPlan,#formS1UserIdVet,#formS1UserIdAgrd").val("").trigger("chosen:updated");
                }
                $("[depfinddetail]").show();
            }, "Finding");
        }
    }
    this.UpdateDaySchColName = function () {
        var dt = tblFS1DetailGrid.dataTable;
        var stdt = new Date(formS1.HeaderData.FromDt).AddDays(-1);
        for (var i = 0; i < 7; i++) {
            var hdr = $(dt.column(11 + i).header());
            hdr.text(hdr.text().replace("???", stdt.AddDays(1).ToString("D-M")));
        }
    }
    this.Bind = new function () {
        this.HeaderInfo = (data) => {
            var assignFormat = jsMaster.AssignFormat;
            var par = $("#divFS1HeaderInfo");
            var refNo = $("#txtS1RefNumber");
            refNo[0].PkID = data.PkRefNo;
            refNo[0].RefId = data.RefId;
            refNo.val(data.RefId);
            par.find("#FsihRemarks").val(data.Remarks);
            par.find("#formS1UserIdPlan").val(data.UseridPlan).trigger("change").trigger("chosen:updated");
            par.find("#formS1UserIdVet").val(data.UseridVet).trigger("change").trigger("chosen:updated");
            par.find("#formS1UserIdAgrd").val(data.UseridAgrd).trigger("change").trigger("chosen:updated");

            if (data.UserNamePlan && data.UserNamePlan != "") {
                var pu = par.find("#formS1UserIdPlan").closest("[userIdGroup]");
                pu.find("[userName]").val(data.UserNamePlan);
                pu.find("[userDest]").val(data.UserDesignationPlan);
            }
            if (data.UserNameVet && data.UserNameVet != "") {
                var pu = par.find("#formS1UserIdVet").closest("[userIdGroup]");
                pu.find("[userName]").val(data.UserNameVet);
                pu.find("[userDest]").val(data.UserDesignationVet);
            }
            if (data.UserNameAgrd && data.UserNameAgrd != "") {
                var pu = par.find("#formS1UserIdAgrd").closest("[userIdGroup]");
                pu.find("[userName]").val(data.UserNameAgrd);
                pu.find("[userDest]").val(data.UserDesignationAgrd);
            }
            if (data.DtPlan && data.DtPlan != null)
                par.find("#FsiihDtPlan").val((new Date(data.DtPlan)).ToString(assignFormat));
            else
                par.find("#FsiihDtPlan").val("");
            if (data.DtVet && data.DtVet != null)
                par.find("#FsiihDtVet").val((new Date(data.DtVet)).ToString(assignFormat));
            else
                par.find("#FsiihDtVet").val("");
            if (data.DtAgrd && data.DtAgrd != null)
                par.find("#FsiihDtAgrd").val((new Date(data.DtAgrd)).ToString(assignFormat));
            else
                par.find("#FsiihDtAgrd").val("");
        }
        this.Header = (data) => {
            
            var assignFormat = jsMaster.AssignFormat;
            var par = $("#divFindDetails");
            par.find("#formADetSrchRMU").val(data.Rmu).trigger("chosen:updated");
            par.find("#selecDate").val((new Date(data.Dt)).ToString(assignFormat));
            par.find("#drpWeekNo").val(data.WeekNo).trigger("chosen:updated");
            par.find("#frmDate").val((new Date(data.FromDt)).ToString(assignFormat))[0].dt = data.FromDt;
            par.find("#toDate").val((new Date(data.ToDt)).ToString(assignFormat));
        }
    }
    this.UserIdChange = function (tis) {
        var sel = $(tis);
        var opt = sel.find(":selected");
        var par = sel.closest("[userIdGroup]");
        var item1 = opt.attr("item1") ? opt.attr("item1") : "";
        if (item1 == "others") {
            par.find("[userName]").val("").addClass("validate").prop("disabled", false);
            par.find("[userDest]").val("").addClass("validate").prop("disabled", false);
        }
        else {
            var item2 = opt.attr("Item2") ? opt.attr("item2") : "";
            par.find("[userName]").val(item1).removeClass("validate").prop("disabled", true);
            par.find("[userDest]").val(item2).removeClass("validate").prop("disabled", true);
        }
    }
    this.ShowAddDetailsButton = function () {
        var refNo = $("#txtS1RefNumber");
        if (refNo.length > 0 && refNo[0].PkID && parseInt(refNo[0].PkID, 10) > 0) {
            $("#btnAddDetails").show();
        }
    }
    this.Save = function (isSubmit) {
        var tis = this;
        if (isSubmit) {
            $("#divFS1HeaderInfo .svalidate").addClass("validate");
        }
        if (ValidatePage("#divFormS1Sec", "", "")) {
            var refNo = $("#txtS1RefNumber");
            var action = isSubmit ? "Submit" : "Save";
            GetResponseValue(action, "FormS1", FormValueCollection("#divFormS1Sec", { PkRefNo: refNo[0].PkID }), function (data) {
                if (!refNo[0].PkID || refNo[0].PkID == 0) {
                    tblFS1DetailGrid.dataTable.settings()[0].ajax.url = "/FormS1/ListDetail/" + data.Id;
                }
                refNo[0].PkID = data.Id;
                refNo.val(data.RefNo);
                app.ShowSuccessMessage('Successfully Saved', false);
                setTimeout(tis.NavToList, 2000);
                //formS1.ShowAddDetailsButton();
            }, "Saving");
        }
        if (isSubmit) {
            $("#divFS1HeaderInfo .svalidate").removeClass("validate");
        }
    }
    this.NavToList = function () {
        window.location = _APPLocation + "FormS1";
    }
    this.Cancel = function () {
        jsMaster.ConfirmCancel(() => { formS1.NavToList(); });
    }
    this.HeaderGrid = new function () {
        this.ActionRender = function (data, type, row, meta) {
            var actionSection = "<div class='btn-group dropright' rowidx='" + meta.row + "'><button type='button' class='btn btn-sm btn-themebtn dropdown-toggle' data-toggle='dropdown'> Click Me </button>";
            actionSection += "<div class='dropdown-menu'>";//dorpdown menu start

            if (data.Status != "Submitted" && tblFS1HeaderGrid.Base.IsModify) {
                actionSection += "<button type='button' class='dropdown-item editdel-btns' onclick='formS1.HeaderGrid.ActionClick(this);'>";
                actionSection += "<span class='edit-icon'></span> Edit </button>";
            }
            if (tblFS1HeaderGrid.Base.IsView) {
                actionSection += "<button type='button' class='dropdown-item editdel-btns' onclick='formS1.HeaderGrid.ActionClick(this);'>";
                actionSection += "<span class='view-icon'></span> View </button>";
            }
            if (tblFS1HeaderGrid.Base.IsDelete) {
                actionSection += "<button type='button' class='dropdown-item editdel-btns' onclick='formS1.HeaderGrid.ActionClick(this);'>";
                actionSection += "<span class='del-icon'></span> Delete </button>";
            }
            actionSection += "<button type='button' class='dropdown-item editdel-btns' onclick='formS1.HeaderGrid.ActionClick(this);'>";
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
                var data = tblFS1HeaderGrid.dataTable.row(rowidx).data();
                switch (type.toLowerCase()) {
                    case "edit":
                        window.location = _APPLocation + "FormS1/Edit/" + data.RefNo;
                        break;
                    case "view":
                        window.location = _APPLocation + "FormS1/View/" + data.RefNo;
                        break;
                    case "delete":
                        app.Confirm("Are you sure you want to delete this record? <br/>(Ref: " + data.RefID + ")", (status) => {
                            if (status) {
                                DeleteRequest("Delete/" + data.RefNo, "FormS1", {}, function (sdata) {
                                    tblFS1HeaderGrid.Refresh();
                                    app.ShowSuccessMessage("Deleted Sucessfully! <br/>(Ref: " + data.RefID + ")");
                                });
                            }
                        }, "Yes", "No");
                        break;
                    case "print":
                        window.location = _APPLocation + "FormS1/Print/" + data.RefNo;
                        break;
                }
            }
        }
        this.DateOfEntry = (data, type, row, meta) => {
            var result = "";
            if (data && data != "") {
                result = (new Date(data)).ToString(jsMaster.DisplayDateFormat);
            }
            return result;
        }
    }
    this.LoadDetails = function (id, isEdit) {
        var tis = this;
        var ctrl = $("#FormS1AdddetailsModal");
        if (ctrl.length == 0) {
            id = id ? id : 0;
            if (id == 0) { isEdit = true; }
            var post = {};
            PostResponseHTML("LoadDetails" + "/" + id, "FormS1", post, (data) => {
                $("#frmS1HeaderData").after(data);
                tis.AfterLoadDetails(isEdit);
            });
        }
        else {
            formS1D.F1DetailInfo = {};
            if (id > 0) {
                PostResponseHTML("LoadDetailData" + "/" + id, "FormS1", post, (data) => {
                    if (data) {
                        formS1D.F1DetailInfo = data;
                        tis.ShowDetails(ctrl, isEdit);
                    }
                });
            } else {
                if (id == 0) { isEdit = true; }
                tis.ShowDetails(ctrl, isEdit);
            }
        }
    }
    this.AfterLoadDetails = function (isEdit) {
        var tis = this;
        setTimeout(() => {
            if (formS1D) {
                ctrl = $("#FormS1AdddetailsModal");
                var ref = $("#txtS1RefNumber");
                var dref = ctrl.find("#txtDRefNumber");
                dref[0].FormS1Id = ref[0].PkID;
                dref[0].FormS1RefId = ref[0].RefId;
                formS1D.FromDate = new Date($("#frmDate")[0].dt);
                if (formS1.HeaderData && formS1.HeaderData != null) {
                    ctrl.find("#formS1DRoadCode option:not([cvalue='" + formS1.HeaderData.Rmu + "'])").hide();
                }
                formS1D.Refresh = function () {
                    tblFS1DetailGrid.Refresh();
                }
                tis.ShowDetails(ctrl, isEdit);
            }
            else {
                tis.AfterLoadDetails(isEdit);
            }
        }, 200);
    }
    this.ShowDetails = function (ctrl, isEdit) {
        if (isEdit) {
            $("#FormS1AdddetailsModal,#divScheduleTemplate").find("[viewdis]").prop("disabled", false).removeAttr("viewmode");
            var s = ctrl.find("[savesec]"); if (s.find("button").length == 1) { s.append(formS1D.SEButton.clone()); s.append(formS1D.SCButton.clone()); }

        }
        else {
            $("#FormS1AdddetailsModal,#divScheduleTemplate").find("[viewdis]").prop("disabled", true).attr("viewmode", "");
            ctrl.find("[savesec] button:not(:eq(0))").remove();
        }
        ctrl.find(".border-error").removeClass("border-error");
        ctrl.modal({ backdrop: 'static' });
        formS1D.Init();
        if (isEdit) {
            setTimeout(function () { $('#formS1DActivityCode').trigger('chosen:activate'); }, 500);
            if (formS1D.F1DetailInfo.PkRefNo && formS1D.F1DetailInfo.PkRefNo > 0) {
                $("[editdisabled]").prop("disabled", true);
            }
            else {
                $("[editdisabled]").prop("disabled", false);                
            }
            $("select[editdisabled]").trigger("chosen:updated");
        }
    }
    this.DetailGrid = new function () {
        this.ActionRender = function (data, type, row, meta) {
            var actionSection = "<div class='btn-group dropright' rowidx='" + meta.row + "'><button type='button' class='btn btn-sm btn-themebtn dropdown-toggle' data-toggle='dropdown'> Click Me </button>";
            actionSection += "<div class='dropdown-menu'>";//dorpdown menu start

            if (data.Status != "Submitted" && tblFS1DetailGrid.Base.IsModify && !formS1.HeaderData.SubmitSts) {
                actionSection += "<button type='button' class='dropdown-item editdel-btns' onclick='formS1.DetailGrid.ActionClick(this);'>";
                actionSection += "<span class='edit-icon'></span> Edit </button>";
            }
            if (tblFS1DetailGrid.Base.IsView) {
                actionSection += "<button type='button' class='dropdown-item editdel-btns' onclick='formS1.DetailGrid.ActionClick(this);'>";
                actionSection += "<span class='view-icon'></span> View </button>";
            }
            if (tblFS1DetailGrid.Base.IsDelete && !formS1.HeaderData.SubmitSts) {
                actionSection += "<button type='button' class='dropdown-item editdel-btns' onclick='formS1.DetailGrid.ActionClick(this);'>";
                actionSection += "<span class='del-icon'></span> Delete </button>";
            }
            //if (tblFS1DetailGrid.Base.IsPrint) {
            //    actionSection += "<button type='button' class='dropdown-item editdel-btns' onclick='formS1.DetailGrid.ActionClick(this);'>";
            //    actionSection += "<span class='print-icon'></span> Print </button>";
            //}
            actionSection += "</div>"; //dorpdown menu close
            actionSection += "</div>"; // action close

            return actionSection;
        }
        this.ActionClick = function (tis) {
            var obj = $(tis);
            var type = $.trim(obj.text());
            var rowidx = parseInt(obj.closest("[rowidx]").attr("rowidx"), 10);
            if (rowidx >= 0) {
                var data = tblFS1DetailGrid.dataTable.row(rowidx).data();
                switch (type.toLowerCase()) {
                    case "edit":
                        formS1.LoadDetails(data.RefNo, true);
                        break;
                    case "view":
                        formS1.LoadDetails(data.RefNo, false);
                        break;
                    case "delete":
                        app.Confirm("Are you sure you want to delete this record? <br/>(Ref: " + data.RefID + ")", (status) => {
                            if (status) {
                                DeleteRequest("DeleteDetail/" + data.RefNo, "FormS1", {}, function (sdata) {
                                    tblFS1DetailGrid.Refresh();
                                    app.ShowSuccessMessage("Deleted Sucessfully! <br/>(Ref: " + data.RefID + ")");
                                });
                            }
                        }, "Yes", "No");
                        break;
                    case "print":
                        break;
                }
            }
        }
        this.DaySchedule = function (data, type, row, meta) {
            if (parseInt(data) > 0) {
                var obj = $("#drpStatusLegend option[code='" + data + "']");
                return "<span class='dayschl - status'><span title=\"" + obj.text() + "\" icon class='" + obj.attr("cvalue") + "'></span></span>";
            }
            return "";
        }
    }
}
$(document).ready(function () {
    $("[useridChange]").on("change", function () {
        formS1.UserIdChange(this);
    });
    if (formS1.HeaderData && formS1.HeaderData != null) {
        formS1.Bind.Header(formS1.HeaderData);
        formS1.Bind.HeaderInfo(formS1.HeaderData);
    }
    var refNo = $("#txtS1RefNumber");
    if (refNo.val() == "") {
        $("[RefNumber]").on("change", function () {
            formS1.GenRefNumber();
        });
        refNo[0].PkID = 0;
        $("[depfinddetail]").hide();
    }
    else {
        $("#divFindDetails [finddetails]").remove();
        $("[depfinddetail]").show();
    }
    formS1.ShowAddDetailsButton();
    $("#smartSearch").focus();//Header Grid focus    
    if ($("#btnAddDetails:visible").length == 0) {
        $('#formADetSrchRMU').trigger('chosen:activate'); // Add / Edit focus
    }
    else {
        $("#FsihRemarks").focus();
    }

    $("#ddlRefNo").on("change", function () {

        if ($(this).val() != "") {
            LoadS2($(this).val());
            DisableS1DetailHeader(true);
            $("#btnAddDetails").hide();
        }
        else {
            LoadS2(0);
            $("#btnAddDetails").show();
            DisableS1DetailHeader(false);
        }
    });

    function LoadS2(S2PKRefNo) {
        
        InitAjaxLoading();
        $.ajax({
            url: '/FormS1/LoadS2Data',
            dataType: 'JSON',
            data: { PKRefNo: $("#txtS1RefNumber")[0].PkID, S2PKRefNo: S2PKRefNo },
            type: 'Post',
            success: function (data) {
                 
                tblFS1DetailGrid.dataTable.settings()[0].ajax.url = "/FormS1/ListDetail/" + $("#txtS1RefNumber")[0].PkID;
                tblFS1DetailGrid.Refresh();
                HideAjaxLoading();
 
            },
            error: function (data) {
                console.error(data);
            }
        });
    }

    function DisableS1DetailHeader(status) {
        $('#formS1DActivityCode').prop('disabled', status).trigger("chosen:updated");
        $('#formS1DRoadCode').prop('disabled', status).trigger("chosen:updated");
        $('#drpFormType').prop('disabled', status).trigger("chosen:updated");
        $('#drpFormS1DRefNo').prop('disabled', status).trigger("chosen:updated");
                
        
        $('#formAFromCh').attr("readonly", status);
        $('#formAFromChDeci').attr("readonly", status);
        $('#formAToCh').attr("readonly", status);
        $('#formAToChDeci').attr("readonly", status);
        
    }

});

