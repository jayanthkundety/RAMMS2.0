$(document).ready(function () {
    var val = $("#FDHRef_No").val();

    if ($("#hdnView").val() == "0" || $("#hdnView").val() == "") {
        if (val == "0") {
            $("#formDrmu").chosen();
            $("#formDroadCode").chosen();
            $("#formDWeekNo").chosen();
            $("#formDDay").chosen();
            $("#formDYear").chosen();
        }
        $("#formDReportedByUserId").chosen();
        $("#formDUseridVet").chosen();
        $("#formDUseridVer").chosen();

        $("#formDUseridVerSo").chosen();
        $("#formDUseridAgrdSo").chosen();
        $("#formDUseridPrcdSo").chosen();
    } else { //View
        userIdDisable()
    }

    if (val != 0 && val != undefined && val != "") {
        gridAddBtnDis()
        //document.getElementById("saveFormDBtn").disabled = true;
    }
    else {
        document.getElementById("btnEquipAdd").disabled = true;
        document.getElementById("btnLabourAdd").disabled = true;
        document.getElementById("btnMaterialAdd").disabled = true;
        document.getElementById("btnFormDDtlAdd").disabled = true;
        //document.getElementById("saveFormDUserBtn").disabled = true;
    }
});

var saveFormADetList = new Array();

function userIdDisable() {
    $('#formDReportedByUserId').prop('disabled', true).trigger("chosen:updated");
    $('#formDUseridVet').prop('disabled', true).trigger("chosen:updated");
    $('#formDUseridVer').prop('disabled', true).trigger("chosen:updated");
    $('#formDUseridVerSo').prop('disabled', true).trigger("chosen:updated");
    $('#formDUseridPrcdSo').prop('disabled', true).trigger("chosen:updated");
    $('#formDUseridAgrdSo').prop('disabled', true).trigger("chosen:updated");

  
}
function gridAddBtnDis() {    
    $("#btnEquipAdd,#btnLabourAdd,#btnMaterialAdd,#btnFormDDtlAdd").prop("disabled", false);
}
function UserDtDisable() {
    $('#FormDDtPrcdSo').prop('disabled', true);
    $('#FormDReportedByDate').prop('disabled', true);
    $('#FormDVerifiedDate').prop('disabled', true);
    $('#FormDVettedByDate').prop('disabled', true);
    $('#FormDDtVerSo').prop('disabled', true);
    $('#FormDDtAgrdSo').prop('disabled', true);
}

function ERTFormDHdrSave() {

    if (ValidatePage('#AccordPage0')) {
        InitAjaxLoading();
        var saveObj = new Object;

        // saveObj.No = $("#FDHRef_No").val();
        saveObj.No = $("#hdnNo").val();
        saveObj.ReferenceID = $("#formDReferenceNo").val();
        saveObj.CrewUnit = $("#formDCrewUnit").find(":selected").val();
        saveObj.CrewUnitName = $("#formDCrewName").val();
        saveObj.Rmu = $("#formDrmu").find(":selected").val();
        saveObj.RoadCode = $("#formDroadCode option:selected").text().split("-")[0];
        saveObj.WeekNo = $("#formDWeekNo").find(":selected").val();
        saveObj.DivisionName = $("#formDDivisionDesc").val();
        saveObj.Day = $("#formDDay").find(":selected").val();
        saveObj.Year = $("#formDYear").find(":selected").val();
        console.log(saveObj);

        $.ajax({
            url: '/ERT/FormDCheckandSaveHdr',
            data: saveObj,
            type: 'POST',
            success: function (data) {
                console.log(data);
                if (data == -1) {
                    app.ShowErrorMessage("Reference id already Exist");
                }
                //var refno = data.no;
                $("#FDHRef_No").val(data.no);
                lInitializeGrid(data.no);
                EqInitializeGrid(data.no);
                dInitializeGrid(data.no);
                mInitializeGrid(data.no);
                document.getElementById("btnEquipAdd").disabled = false;
                document.getElementById("btnLabourAdd").disabled = false;
                document.getElementById("btnMaterialAdd").disabled = false;
                document.getElementById("btnFormDDtlAdd").disabled = false;
                HideAjaxLoading();
            },
            error: function (data) {
                HideAjaxLoading();
                app.ShowErrorMessage(data.responseText);
            }

        });
    }



}

$(document).on("click", "#formDFindDetailsBtn", function () {
   
    if (ValidatePage("#AccordPage0", "", "")) {
        InitAjaxLoading();
        GetResponseValue("FindDetails", "ERT", FormValueCollection("#FormDHeaders"), function (data) {
            HideAjaxLoading();
            if (data != undefined && data != null) {

                $("#formDrmu").prop("disabled", true).trigger("chosen:updated");
                $("#formDroadCode").prop("disabled", true).trigger("chosen:updated");
                $("#formDCrewUnit").prop("disabled", true).trigger("chosen:updated");
                $("#formDWeekNo").prop("disabled", true).trigger("chosen:updated");
                $("#formDDay").prop("disabled", true).trigger("chosen:updated");
                $("#formDYear").prop("disabled", true).trigger("chosen:updated");
                $('#WeekDate').prop("disabled", true);
                $("#formDReferenceNo").val(data.ReferenceID)
                $("#formDFindDetailsBtn").hide();
                if (!data.SubmitSts) {
                    $("#saveFormDBtn").show();
                    $("#SubmitFormDBtn").show();
                    gridAddBtnDis();
                }
                else {
                    userIdDisable()
                    UserDtDisable()
                }
                $("#FDHRef_No").val(data.No);
               
                DtlGridLoad(data.No);
               
                $("#formDReportedByUserId").val(data.ReportedByUserId).trigger("chosen:updated");
                $("#formDUseridVer").val(data.ReportedByUserId).trigger("chosen:updated");
                $("#formDUseridVet").val(data.ReportedByUserId).trigger("chosen:updated");

                $("#formDUseridVerSo").val(data.ReportedByUserId).trigger("chosen:updated");
                $("#formDUseridPrcdSo").val(data.ReportedByUserId).trigger("chosen:updated");
                $("#formDUseridAgrdSo").val(data.ReportedByUserId).trigger("chosen:updated");

                $("#FormDReportedByName").val(data.ReportedByUsername);
                $("#FormDReportedByDesign").val(data.ReportedByDesignation);
                var Format = "YYYY-MM-DD";
                if (data.DateReported!=null) {
                    var date = new Date(data.DateReported);
                    $("#FormDReportedByDate").val(date.ToString(Format));
                }

                $("#FormDVerifiedByName").val(data.UsernameVer);
                $("#FormDVerifiedDesign").val(data.DesignationVer);
                if (data.DtVer != null) {
                    date = new Date(data.DtVer);
                    $("#FormDVerifiedDate").val(date.ToString(Format));
                }
                $("#FormDVettedByName").val(data.UsernameVet);
                $("#FormDVettedDesign").val(data.DesignationVet);
                if (data.DtVet != null) {
                    date = new Date(data.DtVet);
                    $("#FormDVettedByDate").val(date.ToString(Format));
                }
                $("#FormDUsernameVerSo").val(data.UsernameVerSo);
                $("#FormDDesignationVerSo").val(data.DesignationVerSo);
                if (data.DtVerSo != null) {
                    date = new Date(data.DtVerSo);
                    $("#FormDDtVerSo").val(date.ToString(Format));
                }
                $("#FormDUsernamePrcdSo").val(data.UsernamePrcdSo);
                $("#FormDDesignationPrcdSo").val(data.DesignationPrcdSo);
                if (data.DtAgrdSo != null) {
                    date = new Date(data.DtAgrdSo);
                    $("#FormDDtPrcdSo").val(date.ToString(Format));
                }
                $("#FormDUsernameAgrdSo").val(data.UsernameAgrdSo);
                $("#FormDDesignationAgrdSo").val(data.DesignationAgrdSo);
                if (data.DtPrcdSo != null) {
                    date = new Date(data.DtPrcdSo);
                    $("#FormDDtAgrdSo").val(date.ToString(Format));
                }
               
            }
        }, "Finding");
    }
    //saveHdr(false);
});



var tempHTTP;
function getHttp() {
    return tempHTTP;
}
$(document).on("click", "#saveFormDBtn", function () {
    saveHdr(false);
});

$(document).on("click", "#SubmitFormDBtn", function () {
    saveHdr(true);
});

$(document).on("click", "#saveContinueFormDLabBtn", function () {
    saveLabour(false, true);
});

$(document).on("click", "#saveFormDLabBtn", function () {
    saveLabour(false, false);
});

$(document).on("click", "#saveFormDDtlBtn", function () {
    saveDetails(false, false);
});

$(document).on("click", "#saveContinueFormDDtlBtn", function () {

    saveDetails(false, true);
});

$(document).on("click", "#saveFormDEqBtn", function () {
    saveEquipment(false, false);
});

$(document).on("click", "#saveContinueFormDEqBtn", function () {
    saveEquipment(false, true);
});

$(document).on("click", "#saveFormDMtBtn", function () {
    saveMaterial(false, false);
});

$(document).on("click", "#saveContinueFormDMtBtn", function () {
    saveMaterial(false, true);
});

$(document).on("click", "#saveFormDUserBtn", function () {
    saveUserDetails(false);
});

$("#formDLabCode").on("change", function () {
    val = $(this).find(":selected").text();
    $("#formDlabDesc").val(val);
});



function saveHdr(isSubmit) {

    if ((!isSubmit && ValidatePage('#AccordPage0')) || (isSubmit && ValidatePage('#AccordPage0,#div-addformd'))) {
        InitAjaxLoading();

        var d = new Date();

        var month = d.getMonth() + 1;
        var day = d.getDate();

        var output = (('' + month).length < 2 ? '0' : '') + month + '/' +
            (('' + day).length < 2 ? '0' : '') + day + '/' + d.getFullYear();

        var saveObj = new Object;

        // saveObj.No = $("#FDHRef_No").val();
        saveObj.No = $("#FDHRef_No").val();
        saveObj.ReferenceID = $("#formDReferenceNo").val();
        //var opt = $("#formDCrewUnit").find(":selected").val()
        //if (opt == "99999999") {
        //    saveObj.CrewUnitName = "99999999-" + $("#formDCrewName").val();
        //}
        //else {
        //    saveObj.CrewUnitName = $("#formDCrewUnit").find(":selected").val();
        //}
        saveObj.CrewUnit = $("#formDCrewUnit").find(":selected").val();
        saveObj.CrewUnitName = $("#formDCrewName").val();
        saveObj.Rmu = $("#formDrmu").find(":selected").val();
        saveObj.RoadCode = $("#formDroadCode option:selected").text().split("-")[0];
        saveObj.WeekNo = $("#formDWeekNo").find(":selected").val();

        saveObj.DivisionName = $("#formDDivisionDesc").val();
        saveObj.Day = $("#formDDay").find(":selected").val();
        saveObj.Year = $("#formDYear").find(":selected").val();

        //Reportedby
        if ($("#formDReportedByUserId").find(":selected").val() != "") saveObj.ReportedByUserId = $("#formDReportedByUserId").find(":selected").val();

        if ($("#FormDReportedByName").val() != "") saveObj.ReportedByUsername = $("#FormDReportedByName").val();

        if ($("#FormDReportedByDesign").val() != "") saveObj.ReportedByDesignation = $("#FormDReportedByDesign").val();

        if ($("#FormDReportedByDate").val() != "mm/dd/yyyy") saveObj.DateReported = $("#FormDReportedByDate").val();

        if ($("#formDUseridVer").find(":selected").val() != "") saveObj.UseridVet = $("#formDUseridVer").find(":selected").val();

        if ($("#FormDVettedByName").val() != "") saveObj.UsernameVet = $("#FormDVettedByName").val();

        if ($("#FormDVettedDesign").val() != "") saveObj.DesignationVet = $("#FormDVettedDesign").val();

        if ($("#FormDVettedByDate").val() != "mm/dd/yyyy") saveObj.DtVet = $("#FormDVettedByDate").val();

        if ($("#formDUseridVer").find(":selected").text() != "") saveObj.UseridVer = $("#formDUseridVer").find(":selected").val();

        if ($("#FormDVerifiedByName").val() != "") saveObj.UsernameVer = $("#FormDVerifiedByName").val();

        if ($("#FormDVerifiedDesign").val() != "") saveObj.DesignationVer = $("#FormDVerifiedDesign").val();

        if ($("#FormDVerifiedDate").val() != "mm/dd/yyyy") saveObj.DtVer = $("#FormDVerifiedDate").val();

        //So
        if ($("#formDUseridVerSo").find(":selected").val() != "") saveObj.UseridVerSo = $("#formDUseridVerSo").find(":selected").val();

        if ($("#FormDUsernameVerSo").val() != "") saveObj.UsernameVerSo = $("#FormDUsernameVerSo").val();
        if ($("#FormDDesignationVerSo").val() != "") saveObj.DesignationVerSo = $("#FormDDesignationVerSo").val();
        if ($("#FormDDtVerSo").val() != "mm/dd/yyyy") saveObj.DtVerSo = $("#FormDDtVerSo").val();

        if ($("#formDUseridAgrdSo").find(":selected").val() != "") saveObj.UseridAgrdSo = $("#formDUseridAgrdSo").find(":selected").val();
        if ($("#FormDUsernameAgrdSo").val() != "") saveObj.UsernameAgrdSo = $("#FormDUsernameAgrdSo").val();
        if ($("#FormDDesignationAgrdSo").val() != "") saveObj.DesignationAgrdSo = $("#FormDDesignationAgrdSo").val();
        if ($("#FormDDtAgrdSo").val() != "mm/dd/yyyy") saveObj.DtAgrdSo = $("#FormDDtAgrdSo").val();

        if ($("#formDUseridPrcdSo").find(":selected").val() != "") saveObj.UseridPrcdSo = $("#formDUseridPrcdSo").find(":selected").val();
        if ($("#FormDUsernamePrcdSo").val() != "") saveObj.UsernamePrcdSo = $("#FormDUsernamePrcdSo").val();
        if ($("#FormDDesignationPrcdSo").val() != "") saveObj.DesignationPrcdSo = $("#FormDDesignationPrcdSo").val();
        if ($("#FormDDtPrcdSo").val() != "mm/dd/yyyy") saveObj.DtPrcdSo = $("#FormDDtPrcdSo").val();

        //Created by

        if ($("#formDReportedByUserId").find(":selected").val() != "") saveObj.ModifeidBy = $("#formDReportedByUserId").find(":selected").val();
        if ($("#FormDReportedByDate").val() != "mm/dd/yyyy") saveObj.ModifiedDate = $("#FormDReportedByDate").val();
        if ($("#formDReportedByUserId").find(":selected").val() != "") saveObj.CreatedBy = $("#formDReportedByUserId").find(":selected").val();
        if ($("#FormDReportedByDate").val() != "mm/dd/yyyy") saveObj.CreatedDate = $("#FormDReportedByDate").val();
        if ($("#WeekDate").val() != "mm/dd/yyyy") saveObj.WeekDate = $("#WeekDate").val();
        saveObj.ActiveYn = true;
        saveObj.SubmitSts = isSubmit;
        console.log(saveObj);
        $.ajax({
            url: '/ERT/FormDSaveHdr',
            data: saveObj,
            type: 'POST',
            success: function (data) {
                HideAjaxLoading();
                if (data == -1) {
                    app.ShowErrorMessage("Reference id already Exist");
                }
                else {
                    if ($("#FDHRef_No").val() == "" || $("#FDHRef_No").val() == "0") {
                        DtlGridLoad(data)
                    }                   
                    $("#FDHRef_No").val(data);
                    $("#formDFindDetailsBtn").hide();
                    DataBind();
                    if (isSubmit) {
                        $("#saveFormDBtn").hide();
                        $("#SubmitFormDBtn").hide();
                        app.ShowSuccessMessage('Successfully Submitted', false);
                        location.href = "/ERT/FormD";
                    }
                    else {
                        $("#saveFormDBtn").show();
                        $("#SubmitFormDBtn").show();   
                        app.ShowSuccessMessage('Successfully Saved', false);
                    }                   
                }                                                
            },
            error: function (data) {
                HideAjaxLoading();
                app.ShowErrorMessage(data.responseText);
            }

        });
    }
}

function DtlGridLoad(data) {

    var table = $('#FormDEquipGridView').DataTable();
    table.ajax.url("/ERT/LoadFormDEquipmentList?id=" + data).load();

    var ltable = $('#FormDLabourGridView').DataTable();
    ltable.ajax.url("/ERT/LoadFormDLabourList?id=" + data).load();

    var mtable = $('#FormDMaterialGridView').DataTable();
    mtable.ajax.url("/ERT/LoadFormDMaterialList?id=" + data).load();

    var dtable = $('#FormDDetailsGridView').DataTable();
    dtable.ajax.url("/ERT/LoadFormDDetailsList?id=" + data).load();
}

function DataBind() {

    $("#formDFindDetailsBtn").hide();

    $("#val-summary-displayer").css("display", "none");
    $("#formDroadCode").chosen('destroy');
    $("#formDrmu").chosen('destroy');
    $("#formDWeekNo").chosen('destroy');
    $("#formDDay").chosen('destroy');
    $("#formDYear").chosen('destroy');

    $("#formDroadCode").attr("disabled", "disabled").off('click');
    $("#formDrmu").attr("disabled", "disabled").off('click');
    $("#formDWeekNo").attr("disabled", "disabled").off('click');
    $("#formDCrewUnit").attr("disabled", "disabled").off('click');
    $("#formDYear").attr("disabled", "disabled").off('click');
    $("#formDDay").attr("disabled", "disabled").off('click');
    $("#WeekDate").attr("disabled", "disabled").off('click');
    
    /*document.getElementById("btnEquipAdd").disabled = false;
    document.getElementById("btnLabourAdd").disabled = false;
    document.getElementById("btnMaterialAdd").disabled = false;
    document.getElementById("btnFormDDtlAdd").disabled = false;
    document.getElementById("saveFormDUserBtn").disabled = false;*/
    $("#saveFormDUserBtn,#btnEquipAdd,#btnLabourAdd,#btnMaterialAdd,#btnFormDDtlAdd").prop("disabled", false);
}

function saveEquipment(isSubmit, cont) {
    if (ValidatePage('#FormAddEquipDetails')) {
        InitAjaxLoading();
        var d = new Date();
        var month = d.getMonth() + 1;
        var day = d.getDate();
        var output = (('' + month).length < 2 ? '0' : '') + month + '/' +
            (('' + day).length < 2 ? '0' : '') + day + '/' + d.getFullYear();

        var saveObj = new Object;
        saveObj.SubmitStatus = isSubmit;
        saveObj.No = $("#hdnEquipid").val();
        saveObj.FormDEDFHeaderNo = $("#FDHRef_No").val();
        var tdlen = $("#FormDEquipGridView").find("tbody").find("tr td").length;
        var rowno = $("#FormDEquipGridView").find("tbody").find("tr").length;
        //  saveObj.SerialNo = tdlen == 1 ? 1 : rowno + 1;
        saveObj.EquipmentCode = $("#formDEquipCode").find(":selected").val();
        saveObj.CodeDesc = $("#formDEquipDesc").val();
        saveObj.EquipmentDesc = $("#FormDEquipDescription").val();
        saveObj.Quantity = $("#formDEquipQty").val();
        saveObj.Unit = $("#formDEquipUnit").find(":selected").val();
        saveObj.SerialNo = $("#EquiSerialNo").val();
        //saveObj.ContNo = $("#formDContNo").val();

        //saveObj.ReportedByUsername = $("#FormDAttnUsername").val();

        //saveObj.UseridVer = $("#FormDVeriBy").find(":selected").text();
        //saveObj.UsernameVer = $("#FormDVerUsername").val();
        //saveObj.DtVer = $("#formDverDate").val();

        //saveObj.UseridVet = $("#FormDSVetby").find(":selected").text();
        //saveObj.UsernameVet = $("#FormDSVetUsername").val();

        saveObj.DateReported = output
        // saveObj.ModifeidBy = $("#FormDSVerby").find(":selected").text();
        saveObj.ModifiedDate = output
        // saveObj.CreatedBy = $("#FormDSVerby").find(":selected").text();
        saveObj.CreatedDate = output
        saveObj.ActiveYn = true;
        $.ajax({
            url: '/ERT/FormDSaveEquipment',
            data: saveObj,
            type: 'POST',
            success: function (data) {
                app.ShowSuccessMessage('Successfully Saved', false);
                $("#val-summary-displayer").css("display", "none");
                $("#val-summary-displayer-equip").css("display", "none");
                if (!cont)
                    $("#FormDEquipModal").modal("hide");
                else {
                    $("#hdnEquipid").val('');
                    $("#formDEquipCode").val('').trigger('chosen:updated');
                    $("#formDEquipDesc").val("");
                    $("#FormDEquipDescription").val("");
                    $("#formDEquipQty").val("");
                    $("#formDEquipUnit").val("").trigger('chosen:updated');
                }
                HideAjaxLoading();
                FormEquipGridRefresh();
            },
            error: function (data) {
                app.ShowErrorMessage(data.responseText);
                HideAjaxLoading();
            }

        });
    }
    else {
        $("#val-summary-displayer-equip").css("display", "block");
    }
}

function saveLabour(isSubmit, cont) {
    if (ValidatePage('#FormDLabourModal')) {
        InitAjaxLoading();
        var d = new Date();

        var month = d.getMonth() + 1;
        var day = d.getDate();

        var output = (('' + month).length < 2 ? '0' : '') + month + '/' +
            (('' + day).length < 2 ? '0' : '') + day + '/' + d.getFullYear();

        var saveObj = new Object;
        saveObj.SubmitStatus = isSubmit;
        var tdlen = $("#FormDLabourGridView").find("tbody").find("tr td").length;
        var rowno = $("#FormDLabourGridView").find("tbody").find("tr").length;
        //  saveObj.SerialNo = tdlen == 1 ? 1 : rowno + 1;
        saveObj.FdmdFdhPkRefNo = $("#FDHRef_No").val();
        saveObj.No = $("#hdnLabid").val();
        saveObj.LabourCode = $("#formDLabCode").find(":selected").val();
        saveObj.CodeDesc = $("#formDlabDesc").val();
        saveObj.LabourDesc = $("#formDLabDescription").val();
        saveObj.Quantity = $("#formDLabQty").val();
        saveObj.Unit = $("#formDLabUnit").find(":selected").val();
        saveObj.SerialNo = $("#LabSerialNo").val();
        //saveObj.ContNo = $("#formDContNo").val();

        //saveObj.ReportedByUsername = $("#FormDAttnUsername").val();

        //saveObj.UseridVer = $("#FormDVeriBy").find(":selected").text();
        //saveObj.UsernameVer = $("#FormDVerUsername").val();
        //saveObj.DtVer = $("#formDverDate").val();

        //saveObj.UseridVet = $("#FormDSVetby").find(":selected").text();
        //saveObj.UsernameVet = $("#FormDSVetUsername").val();

        saveObj.DateReported = output
        // saveObj.ModifeidBy = $("#FormDSVerby").find(":selected").text();
        saveObj.ModifiedDate = output
        // saveObj.CreatedBy = $("#FormDSVerby").find(":selected").text();
        saveObj.CreatedDate = output
        saveObj.ActiveYn = true;

        $.ajax({
            url: '/ERT/FormDSaveLabour',
            data: saveObj,
            type: 'POST',
            success: function (data) {
                app.ShowSuccessMessage('Successfully Saved', false);
                $("#val-summary-displayer").css("display", "none");
                $("#val-summary-displayer-labour").css("display", "none");
                FormLabGridRefresh();
                if (!cont)
                    $("#FormDLabourModal").modal("hide");
                else {
                    $("#hdnLabid").val('');
                    $("#formDLabCode").val('').trigger('chosen:updated');
                    $("#formDlabDesc").val("");
                    $("#formDLabUnit").val("").trigger('chosen:updated');
                    $("#formDLabQty").val("");
                    $("#formDLabDescription").val("")
                }
                HideAjaxLoading();
            },
            error: function (data) {
                app.ShowErrorMessage(data.responseText, false);
                HideAjaxLoading();
            }

        });
    }
    else {
        $("#val-summary-displayer-labour").css("display", "block");
    }
}

function saveMaterial(isSubmit, cont) {
    if (ValidatePage('#FormDMaterialModal')) {
        InitAjaxLoading();
        var d = new Date();

        var month = d.getMonth() + 1;
        var day = d.getDate();

        var output = (('' + month).length < 2 ? '0' : '') + month + '/' +
            (('' + day).length < 2 ? '0' : '') + day + '/' + d.getFullYear();

        var saveObj = new Object;
        saveObj.SubmitStatus = isSubmit;
        var tdlen = $("#FormDMaterialGridView").find("tbody").find("tr td").length;
        var rowno = $("#FormDMaterialGridView").find("tbody").find("tr").length;
        // saveObj.SerialNo = tdlen == 1 ? 1 : rowno + 1;
        saveObj.FdmdFdhPkRefNo = $("#FDHRef_No").val();
        saveObj.No = $("#hdnMaterialNo").val();
        saveObj.MaterialCode = $("#formDMatCode").find(":selected").val();
        saveObj.CodeDesc = $("#formDMatDesc").val();
        saveObj.MaterialDesc = $("#FormDMatDescription").val();
        saveObj.Quantity = $("#formDMatQuantity").val();
        saveObj.Unit = $("#formDMatUnit").find(":selected").val();
        saveObj.SerialNo = $("#MaterialSerialNo").val();
        //saveObj.ContNo = $("#formDContNo").val();

        //saveObj.ReportedByUsername = $("#FormDAttnUsername").val();

        //saveObj.UseridVer = $("#FormDVeriBy").find(":selected").text();
        //saveObj.UsernameVer = $("#FormDVerUsername").val();
        //saveObj.DtVer = $("#formDverDate").val();

        //saveObj.UseridVet = $("#FormDSVetby").find(":selected").text();
        //saveObj.UsernameVet = $("#FormDSVetUsername").val();

        saveObj.DateReported = output
        // saveObj.ModifeidBy = $("#FormDSVerby").find(":selected").text();
        saveObj.ModifiedDate = output
        // saveObj.CreatedBy = $("#FormDSVerby").find(":selected").text();
        saveObj.CreatedDate = output
        saveObj.ActiveYn = true;
        $.ajax({
            url: '/ERT/FormDSaveMaterial',
            data: saveObj,
            type: 'POST',
            success: function (data) {
                app.ShowSuccessMessage('Successfully Saved', false);
                $("#val-summary-displayer").css("display", "none");
                $("#val-summary-displayer-material").css("display", "none");
                if (!cont)
                    $("#FormDMaterialModal").modal("hide");
                else {
                    $("#hdnMaterialNo").val('');
                    $("#FormDMaterialNo").val("");
                    $("#formDMatCode").val('').trigger('chosen:updated');
                    $("#formDMatDesc").val("");
                    $("#FormDMatDescription").val("");
                    $("#formDMatQuantity").val("");
                    $("#formDMatUnit").val("").trigger('chosen:updated');
                }
                FormMatGridRefresh();
                HideAjaxLoading();

            },
            error: function (data) {
                app.ShowErrorMessage(data.responseText, false);
                HideAjaxLoading();
            }

        });
    } else {
        $("#val-summary-displayer-material").css("display", "block");
    }
}

function saveDetails(isSubmit, cont) {
    var valdata = $("#FormAddDetails").serialize();
    if (ValidatePage('#FormAddDetails')) {
        InitAjaxLoading();
        var d = new Date();

        var month = d.getMonth() + 1;
        var day = d.getDate();

        var output = (('' + month).length < 2 ? '0' : '') + month + '/' +
            (('' + day).length < 2 ? '0' : '') + day + '/' + d.getFullYear();

        var saveObj = new Object;
        saveObj.SubmitSts = isSubmit;
        saveObj.FrmCh = $("#formDChinageFrom").val();
        saveObj.FormDHeaderNo = $("#FDHRef_No").val();
        saveObj.No = $("#FormDDtlNo").val();
        saveObj.FrmChDeci = $("#formDChinageFromDeci").val();
        saveObj.ToCh = $("#formDChinageTo").val();
        saveObj.ToChDeci = $("#formDChinageToDeci").val();
       // saveObj.SiteRef = $("#FormDSiteRef").find(":selected").val();
        saveObj.SiteRef_multiSelect = $("#FormDSiteRef").val();
        saveObj.RoadCode = $("#formDDtlRoadCode").find(":selected").val();
        saveObj.ActCode = $("#formDtlActivityCode").find(":selected").val();
        saveObj.TimeArr = $("#formDArrivalTime").val();
        saveObj.TimeDep = $("#formDDepartureTime").val();
        saveObj.Length = $("#formDLength").val();
        saveObj.Width = $("#formDWidth").val();
        saveObj.Height = $("#formDHeight").val();
        saveObj.ProdQty = $("#formDProductionQuality").val();
        saveObj.ProdUnit = $("#formDProductionUnit").find(":selected").val();
        saveObj.WorkSts = $("#FormDWorkStatus").find(":selected").val();
        saveObj.Remarks = $("#FormDRemarks").val();
        saveObj.SourceType = $("#FormDSourceType").find(":selected").val();
        saveObj.ReferenceID = $("#formDDtlReferenceNo").val();

        saveObj.SrNo = $("#FormDDtlSerialNo").val();
        if ($("#FormDisFormSource").val() == 'True') {
            saveObj.FormXPKRefNo = $("#FormDSourceRefID").find(":selected").val();
            $("#FormDTextReferenceID").val('');
        }
        else {
            saveObj.FormXPKRefNo = "";
            saveObj.SourceRefID = $("#FormDTextReferenceID").val();
        }

        //saveObj.ContNo = $("#formDContNo").val();

        //saveObj.ReportedByUsername = $("#FormDAttnUsername").val();

        //saveObj.UseridVer = $("#FormDVeriBy").find(":selected").text();
        //saveObj.UsernameVer = $("#FormDVerUsername").val();
        //saveObj.DtVer = $("#formDverDate").val();

        //saveObj.UseridVet = $("#FormDSVetby").find(":selected").text();
        //saveObj.UsernameVet = $("#FormDSVetUsername").val();

        saveObj.DateReported = output
        // saveObj.ModifeidBy = $("#FormDSVerby").find(":selected").text();
        saveObj.ModifiedDate = output
        // saveObj.CreatedBy = $("#FormDSVerby").find(":selected").text();
        saveObj.CreatedDate = output
        saveObj.ActiveYn = true;
        //HideAjaxLoading();
        //return false;
        $.ajax({
            url: '/ERT/FormDSaveDetails',
            data: saveObj,
            type: 'POST',
            success: function (data) {

                if (data == -1) {
                    app.ShowErrorMessage("Reference id already Exist");
                }
                else {
                    $("#FormDDtlNo").val(data);
                    document.getElementById("btnUCUOpenModal").disabled = false;
                    document.getElementById("btnWarOpenModal").disabled = false;
                    $("body").removeClass("loading");
                    app.ShowSuccessMessage('Successfully Saved', false);
                    if (!cont) {
                        $("#FormDDetailsModal").modal("hide");
                    } else {
                        app.Confirm("Click OK to add U SEE U Act and WAR", function (e) {
                            if (e) {
                                //Ok
                            } else {//Cancel
                                clearForm();
                            }
                        })
                    }

                    FormDDtlGridRefresh();
                }
                HideAjaxLoading();
                $("#val-summary-displayer").css("display", "none");
                $("#val-summary-displayer-details").css("display", "none");
            },
            error: function (data) {
                app.ShowErrorMessage(data.responseText, false);
                HideAjaxLoading();
            }

        });
    } else {
        $("#val-summary-displayer-details").css("display", "block");
    }
}

function clearForm() {
    $("#formDChinageFrom").val("");
    $("#formDChinageFromDeci").val("");
    $("#formDChinageTo").val("");
    $("#formDChinageToDeci").val("");
    $("#FormDSiteRef").val("").trigger("chosen:updated");
    $("#formDtlActivityCode").val("").trigger("chosen:updated");
    $("#formDActivityDesc").val("");
    $("#formDDtlRoadCode").val("").trigger("chosen:updated");
    $("#formDDtlroadDesc").val("");
    $("#FormDSourceType").val("").trigger("chosen:updated");
    $("#FormDSourceRefID").val("");
    $("#FormDTextReferenceID").val("");
    $("#formDArrivalTime").val("");
    $("#formDDepartureTime").val("");
    $("#formDProductionQuality").val("");
    $("#formDLength").val("");
    $("#formDWidth").val("");
    $("#formDHeight").val("");
    $("#formDProductionUnit").val("").trigger("chosen:updated");
    $("#FormDWorkStatus").val("").trigger("chosen:updated");
    $("#FormDRemarks").val("");
    var formDrefNo = $("#formDReferenceNo").val();
    var no = $("#FormDDetailsGridView").find("tbody>tr").length;
    no = no == 1 ? 1 : (no + 1);
    if (no < 10)
        no = "00" + no
    else if (no >= 10 && no <= 99)
        no = "0" + no;

    $("#formDDtlReferenceNo").val(formDrefNo + "-" + no);
    $("#formDDtlReferenceNo").prop("disabled", true);

    document.getElementById("btnUCUOpenModal").disabled = true;
    document.getElementById("btnWarOpenModal").disabled = true;
}

function saveUserDetails(isSubmit) {
    if (ValidatePage('#FormDUsers')) {
        InitAjaxLoading();
        var saveObj = new Object;
        saveObj.No = $("#FDHRef_No").val();

        saveObj.ReferenceID = $("#formDReferenceNo").val();
        //var opt = $("#formDCrewUnit").find(":selected").val()
        //if (opt == "99999999") {
        //    saveObj.CrewUnitName = "99999999-" + $("#formDCrewName").val();
        //}
        //else {
        //    saveObj.CrewUnitName = $("#formDCrewUnit").find(":selected").val();
        //}
        saveObj.CrewUnit = $("#formDCrewUnit").find(":selected").val();
        saveObj.CrewUnitName = $("#formDCrewName").val();
        saveObj.Rmu = $("#formDrmu").find(":selected").val();
        saveObj.RoadCode = $("#formDroadCode option:selected").text().split("-")[0];
        saveObj.WeekNo = $("#formDWeekNo").find(":selected").val();
        saveObj.DivisionName = $("#formDDivisionDesc").val();
        saveObj.Day = $("#formDDay").find(":selected").val();
        saveObj.Year = $("#formDYear").find(":selected").val();


        //Reportedby
        saveObj.ReportedByUserId = $("#formDReportedByUserId").find(":selected").val();
        saveObj.ReportedByUsername = $("#FormDReportedByName").val();
        saveObj.ReportedByDesignation = $("#FormDReportedByDesign").val();
        saveObj.DateReported = $("#FormDReportedByDate").val();

        saveObj.UseridVet = $("#formDUseridVer").find(":selected").val();
        saveObj.UsernameVet = $("#FormDVettedByName").val();
        saveObj.DesignationVet = $("#FormDVettedDesign").val();
        saveObj.DtVet = $("#FormDVettedByDate").val();

        saveObj.UseridVer = $("#formDUseridVer").find(":selected").val();
        saveObj.UsernameVer = $("#FormDVerifiedByName").val();
        saveObj.DesignationVer = $("#FormDVerifiedDesign").val();
        saveObj.DtVer = $("#FormDVerifiedDate").val();

        //So

        saveObj.UseridVerSo = $("#formDUseridVerSo").find(":selected").val();
        saveObj.UsernameVerSo = $("#FormDUsernameVerSo").val();
        saveObj.DesignationVerSo = $("#FormDDesignationVerSo").val();
        saveObj.DtVerSo = $("#FormDDtVerSo").val();

        saveObj.UseridAgrdSo = $("#formDUseridAgrdSo").find(":selected").val();
        saveObj.UsernameAgrdSo = $("#FormDUsernameAgrdSo").val();
        saveObj.DesignationAgrdSo = $("#FormDDesignationAgrdSo").val();
        saveObj.DtAgrdSo = $("#FormDDtAgrdSo").val();

        saveObj.UseridPrcdSo = $("#formDUseridPrcdSo").find(":selected").val();
        saveObj.UsernamePrcdSo = $("#FormDUsernamePrcdSo").val();
        saveObj.DesignationPrcdSo = $("#FormDDesignationPrcdSo").val();
        saveObj.DtPrcdSo = $("#FormDDtPrcdSo").val();

        //Created by

        saveObj.ModifeidBy = $("#formDReportedByUserId").find(":selected").val();
        saveObj.ModifiedDate = $("#FormDReportedByDate").val();
        saveObj.CreatedBy = $("#formDReportedByUserId").find(":selected").val();
        saveObj.CreatedDate = $("#FormDReportedByDate").val();

        saveObj.ActiveYn = true;
        saveObj.SubmitSts = isSubmit;

        $.ajax({
            url: '/ERT/FormDSaveHdr',
            data: saveObj,
            type: 'POST',
            success: function (data) {
                $("#val-summary-displayer").css("display", "none");
                if (data == -1) {
                    app.ShowSuccessMessage('Reference id already Exist', false);
                }
                else {
                    $("#hdnuserDetailsSaved").val("1");
                    app.ShowSuccessMessage('Successfully Saved', false);
                }
                HideAjaxLoading();
            },
            error: function (data) {
                app.ShowErrorMessage(data.responseText, false);
                HideAjaxLoading();
            }

        });
    }
}

function openPhoto(id) {
    if (typeof id == "undefined" || id == "" || id == null)
        id = $("#FormDDtlNo").val();
    $.ajax({
        url: '/ERT/GetImageListFormD',
        data: { id: id },
        type: 'POST',
        success: function (data) {
            $("#div-WarPhoto").html(data);
            if ($("#hdnView").val() == "1") {
                $("#div-WarPhoto *").attr("disabled", "disabled").off('click');
                $("#USeeUModal *").attr("disabled", "disabled").off('click');
                $("#AssetImageUploadBtn").attr("href", "#").off('click');
                $(".close").attr("disabled", false);
                $("#btnclosephto").hide();
                //USeeUModal
            }

            $("#WarphotoType").chosen();
            document.getElementById("btnWarImageUpload").disabled = true;
        },
        error: function (data) {
            app.ShowErrorMessage(data.responseText);
        }
    });
}

function openUSeeU(id) {
    if (typeof id == "undefined" || id == "" || id == null)
        id = $("#FormDDtlNo").val();
    $.ajax({
        url: '/ERT/GetUSeeUPageFormD',
        data: { id: id },
        type: 'POST',
        success: function (data) {
            $("#div-USeeUModal").html(data);
            if ($("#hdnView").val() == "1") {
                $("#div-USeeUModal *").attr("disabled", "disabled").off('click');
                $("#USeeUModal *").attr("disabled", "disabled").off('click');
                $("#ImageUploadBtn").attr("href", "#").off('click');
                $("#downloadPdfBtn").attr("href", "#").off('click');
                $("#downloadPdfBtn").attr("data-target", "");
                $(".close").attr("disabled", false);
                $("#btncoseucu").hide();
            }
            $("#UCUphotoType").chosen();
            document.getElementById("btnUCUDocBrowse").disabled = true;
            document.getElementById("btnUCUDocUpload").disabled = true;
        },
        error: function (data) {
            app.ShowErrorMessage(data.responseText);
        }
    });
}

function CloseWarModal() {
    if ($("#div-WarPhotod").is(':visible'))
        $("#WarPhotoModald").modal('hide');
    else
        $("#WarPhotoModal").modal('hide');
}

function CloseUSeeUModal() {
    if ($("#div-USeeUModald").is(':visible'))
        $("#USeeUModald").modal('hide');
    else
        $("#USeeUModal").modal('hide');
}

function CloseDownload() {
    $("#DownloadPdfModal").modal('hide')
}

function DeleteImage(id, type) {

    if (app.Confirm("Are you sure you want to delete the record?", function (e) {
        if (e) {
            $.ajax({
                url: '/ERT/DeleteImageFormD',
                data: { id: id, type: type },
                type: 'POST',
                success: function (data) {
                    if (data > 0) {

                        app.ShowSuccessMessage('Successfully Deleted', false);
                        if (type == 'War') {
                            if ($("#div-USeeUModald").is(':visible'))
                                openPhotod();
                            else
                                openPhoto();
                        }
                        else if (type == 'UCU') {
                            if ($("#div-USeeUModald").is(':visible'))
                                openUSeeUd()
                            else
                                openUSeeU()
                        }
                    }
                    else {
                        app.ShowErrorMessage("Error in Deleted. Kindly retry later.");
                        $("body").removeClass("loading");
                    }

                }
            });
        }
    }));
}

function EditFormDLabour(id, view) {
    
    InitAjaxLoading();
    $.ajax({
        url: '/ERT/EditFormLabour',
        data: { id: id },
        type: 'POST',
        success: function (data) {
            $("#div-lab-container").html(data);
            if (view == 1 || $("#hdnView").val() == "1") {
                $("#hdnLabView").val(1);
                $("#FormDLabourModalid").html("View Labour")
                $("#div-lab-container *").attr("disabled", "disabled").off('click');
                $("#saveFormDLabBtn").css("display", "none");
                $("#cancelAddModelBtn").attr("disabled", false);
                $("#saveContinueFormDLabBtn").css("display", "none");
            } else if ($("#hdnLabid").val() != "") {
                $("#FormDLabourModalid").html("Edit Labour")
                //$("#hdnLabView").val(0);
            }
            HideAjaxLoading();
            //$("body").removeClass("loading");
        }
    })
}

function EditFormDEquip(id, view) {
    InitAjaxLoading();
    $.ajax({
        url: '/ERT/EditFormDEquipmentDetails',
        data: { id: id },
        type: 'POST',
        success: function (data) {
            $("#div-equip-container").html(data);
            if (view == 1 || $("#hdnView").val() == "1") {
                $("#hdnEquView").val(1);
                $("#FormDEquipModalid").html("View Equipment");
                $("#div-equip-container *").attr("disabled", "disabled").off('click');
                $("#saveFormDEqBtn").css("display", "none");
                $("#cancelAddModelBtn").attr("disabled", false);
                $("#saveContinueFormDEqBtn").css("display", "none");
            }
            else if ($("#hdnEquipid").val() != "")
                $("#FormDEquipModalid").html("Edit Equipment");
            HideAjaxLoading();
        }
    })
}

function EditFormDMaterial(id, view) {
    InitAjaxLoading();
    $.ajax({
        url: '/ERT/EditFormMaterial',
        data: { id: id },
        type: 'POST',
        success: function (data) {
            $("#div-mat-container").html(data);
            if (view == 1 || $("#hdnView").val() == "1") {
                $("#hdnMatView").val(1);
                $("#FormDMaterialModalid").html("View Material")
                $("#div-mat-container *").attr("disabled", "disabled").off('click');
                $("#saveFormDMtBtn").css("display", "none");
                $("#cancelAddModelBtn").attr("disabled", false);
                $("#saveContinueFormDMtBtn").css("display", "none");
            } else if ($("#FormDMaterialNo").val() != "")
                $("#FormDMaterialModalid").html("Edit Material")
            HideAjaxLoading();
        }
    })
}

function EditFormDDetails(id, view) {
    InitAjaxLoading();
    $.ajax({
        url: '/ERT/EditFormDDetails',
        data: { id: id, headerid: $("#FDHRef_No").val() },
        type: 'POST',
        success: function (data) {
            $("#div-dtl-container").html(data);
            if (view == 1 || $("#hdnView").val() == "1") {
                $("#FormDDetailsModalid").html("View Details");
                $("#hdnDetView").val(1);
                $("#btnUCUOpenModal").css("display", "none");
                $("#btnWarOpenModal").css("display", "none");
                $("#saveFormDDtlBtn").css("display", "none");
                $("#saveContinueFormDDtlBtn").css("display", "none");                
                $("#div-dtl-container *").attr("disabled", "disabled").off('click');
                $("#cancelAddModelBtn").attr("disabled", false);

            } else if ($("#FormDDtlNo").val() != "")
                $("#FormDDetailsModalid").html("Edit Details");
            var _multiSite = $("#multiSiteHidden").val().split(',');
            if (_multiSite != "") {
                $("#FormDSiteRef").val(_multiSite).trigger("chosen:updated");
            }
            HideAjaxLoading();

        }
    })
}

function FormLabGridRefresh() {
    var filterData = new Object();
    oTable = $('#FormDLabourGridView').DataTable();
    oTable.data = filterData;
    oTable.draw();
}

function FormMatGridRefresh() {
    var filterData = new Object();
    oTable = $('#FormDMaterialGridView').DataTable();
    oTable.data = filterData;
    oTable.draw();
}

function FormDDtlGridRefresh() {
    var filterData = new Object();
    oTable = $('#FormDDetailsGridView').DataTable();
    oTable.data = filterData;
    oTable.draw();
}

function FormEquipGridRefresh() {
    var filterData = new Object();
    oTable = $('#FormDEquipGridView').DataTable();
    oTable.data = filterData;
    oTable.draw();
}

//Form D
function openPhotod(id) {
    if (typeof id == "undefined" || id == "" || id == null)
        id = $("#hdnDetailNo").val();
    else
        $("#hdnDetailNo").val(id);
    $.ajax({
        url: '/ERT/GetImageListFormD',
        data: { id: id },
        type: 'POST',
        success: function (data) {
            $("#div-WarPhotod").html(data);
            if ($("#hdnView").val() == "1") {
                $("#div-WarPhotod *").attr("disabled", "disabled").off('click');
                $("#USeeUModald *").attr("disabled", "disabled").off('click');
                $("#AssetImageUploadBtn").attr("href", "#").off('click');
                $(".close").attr("disabled", false);
                $("#btnclosephto").hide();
                //USeeUModal
            }
            $("#WarphotoType").trigger("chosen:updated");
            document.getElementById("btnWarImageUpload").disabled = true;
        },
        error: function (data) {
            app.ShowErrorMessage(data.responseText);
        }
    });
}

function openUSeeUd(id) {
    if (typeof id == "undefined" || id == "" || id == null)
        id = $("#hdnDetailNo").val();
    else
        $("#hdnDetailNo").val(id);

    $.ajax({
        url: '/ERT/GetUSeeUPageFormD',
        data: { id: id },
        type: 'POST',
        success: function (data) {
            $("#div-USeeUModald").html(data);
            if ($("#hdnView").val() == "1") {
                $("#div-USeeUModald *").attr("disabled", "disabled").off('click');
                $("#USeeUModald *").attr("disabled", "disabled").off('click');
                $("#ImageUploadBtn").attr("href", "#").off('click');
                $("#downloadPdfBtn").attr("href", "#").off('click');
                $("#downloadPdfBtn").attr("data-target", "");
                $(".close").attr("disabled", false);
                $("#btncoseucu").hide();
            }
            $("#UCUphotoType").chosen();
            document.getElementById("btnUCUDocBrowse").disabled = true;
            document.getElementById("btnUCUDocUpload").disabled = true;
        },
        error: function (data) {
            app.ShowErrorMessage(data.responseText);
        }
    });
}


function CloseWarModald() {
    $("#WarPhotoModald").modal('hide');
}

function CloseUSeeUModald() {
    $("#USeeUModald").modal('hide');
}

function CloseDownloadd() {
    $("#DownloadPdfModald").modal('hide')
}

