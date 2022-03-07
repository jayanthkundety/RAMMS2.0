var currentDate = new Date();
$(document).ready(function () {
    currentDate = formatDate(currentDate);

    $("#formWgIssuedBy").on("change", function () {
        var id = $("#formWgIssuedBy option:selected").val();
        var ctrl = $(this);
        if (id != "99999999" && id != "") {
            $("#formWgIssuedName").val(ctrl.find("option:selected").attr("Item1"));
            $("#formWgIssuedDesig").val(ctrl.find("option:selected").attr("Item2"));
            $("#formWgIssuedOffice").val(ctrl.find("option:selected").attr("Item3"));
            $("#formWgIssuedSig").prop("checked", true);
        }
        else if (id == "99999999") {
            $("#formWgIssuedName").prop("disabled", false);
            $("#formWgIssuedName").val('');
            $("#formWgIssuedDesig").prop("disabled", false);
            $("#formWgIssuedDesig").val('');
            $("#formWgIssuedOffice").prop("disabled", false);
            $("#formWgIssuedOffice").val('');
            $("#formWgIssuedSig").prop("checked", true);
        }
        else {
            $("#formWgIssuedName").prop("disabled", true);
            $("#formWgIssuedName").val('');
            $("#formWgIssuedDesig").prop("disabled", true);
            $("#formWgIssuedDesig").val('');
            $("#formWgIssuedOffice").prop("disabled", true);
            $("#formWgIssuedOffice").val('');
        }
        $("#formWgIssuedDate").val(currentDate);
        return false;
    });

    $("#formWcIssuedBy").on("change", function () {
        debugger;
        var id = $("#formWcIssuedBy option:selected").val();
        var ctrl = $(this);
        if (id != "99999999" && id != "") {
            $("#formWcIssuedName").val(ctrl.find("option:selected").attr("Item1"));
            $("#formWcIssuedDesig").val(ctrl.find("option:selected").attr("Item2"));
            $("#formWcIssuedOffice").val(ctrl.find("option:selected").attr("Item3"));
            $("#formWcIssuedSig").prop("checked", true);
        }
        else if (id == "99999999") {
            $("#formWcIssuedName").prop("disabled", false);
            $("#formWcIssuedName").val('');
            $("#formWcIssuedDesig").prop("disabled", false);
            $("#formWcIssuedDesig").val('');
            $("#formWcIssuedOffice").prop("disabled", false);
            $("#formWcIssuedOffice").val('');
            $("#formWcIssuedSig").prop("checked", true);
        }
        else {
            $("#formWcIssuedName").prop("disabled", true);
            $("#formWcIssuedName").val('');
            $("#formWcIssuedDesig").prop("disabled", true);
            $("#formWcIssuedDesig").val('');
            $("#formWcIssuedOffice").prop("disabled", true);
            $("#formWcIssuedOffice").val('');
        }
        $("#formWcIssuedDate").val(currentDate);
        return false;
    });

    $("#saveWCBtn").on("click", function () {
        SaveWC(false);
        return false;
    });

    $("#submitWCBtn").on("click", function () {
        SaveWC(true);
        return false;
    });

    $("#saveWGBtn").on("click", function () {
        SaveWG(false);
        return false;
    });

    $("#submitWGBtn").on("click", function () {
        SaveWG(true);
        return false;
    });

    if ($("#hdnWcView").val() == "1") {
        $("#divFormWc *").prop("disabled", true);
        $("#clearWCBtn").hide();
        $("#saveWCBtn").hide();
        $("#submitWCBtn").hide();
        $("#formWCDivisionCode").chosen('destroy');
        $("#formWCDivisionCode").prop("disabled", true);
    }

    if ($("#hdnWgView").val() == "1") {
        $("#divFormWg *").prop("disabled", true);
        $("#clearWGBtn").hide();
        $("#saveWGBtn").hide();
        $("#submitWGBtn").hide();
        $("#formWGDivisionCode").chosen('destroy');
        $("#formWGDivisionCode").prop("disabled", true);
    }
});

function SaveWC(submit) {
    if (submit) {
        $("#divFormWc .svalidate").addClass("validate");
    }

    if (!ValidatePage('#page')) {
        return false;
    }
    //debugger;
    InitAjaxLoading();

    var d = new Date();

    var month = d.getMonth() + 1;
    var day = d.getDate();

    var output = (('' + month).length < 2 ? '0' : '') + month + '/' +
        (('' + day).length < 2 ? '0' : '') + day + '/' + d.getFullYear();

    var saveObj = new Object;
    saveObj.PkRefNo = $("#hdnWcRefNo").val();
    saveObj.Fw1PkRefNo = $("#fw1PKRefNo").val();
    saveObj.IwWrksDeptId = $("#formWCDivisionCode option:selected").val();
    saveObj.RmuCode = ""
    saveObj.SecCode = "";
    saveObj.RoadCode = "";
    saveObj.RoadName = "";
    saveObj.Ch = "";
    saveObj.ChDeci = "";
    saveObj.IwRefNo = $("#fw1IWRefNo").val();
    saveObj.IwProjectTitle = $("#formWcProjectTitle").val()
    saveObj.OurRefNo = $("#formWcOurRef").val();
    saveObj.ServRefNo = $("#formWcServPropRefNo").val();
    saveObj.DtWc = $("#formWcDate").val();
    saveObj.DtCompl = $("#formWcDtCompl").val();
    saveObj.DtDlpExtn = $("#formWcDtDlpExtn").val();
    saveObj.DlpPeriod = "";


    if ($("#formWcIssuedBy").find(":selected").val() != "") saveObj.UseridIssu = $("#formWcIssuedBy option:selected").val();
    if ($("#formWcIssuedName").val() != "") saveObj.UsernameIssu = $("#formWcIssuedName").val();
    if ($("#formWcIssuedDate").val() != "dd/mm/yyyy" && $("#formWcIssuedDate").val() != "") saveObj.DtIssu = $("#formWcIssuedDate").val();
    saveObj.DesignationIssu = $("#formWcIssuedDesig").val();
    saveObj.OfficeIssu = $("#formWcIssuedOffice").val();
    saveObj.SignIssu = $("#formWcIssuedSig").prop("checked");


    //Created by

    if ($("#formWcIssuedBy").find(":selected").val() != "") saveObj.ModBy = $("#hdnUserId").val();
    if ($("#FWcHRef_No").val() != "0") saveObj.ModDt = output
    if ($("#formWcIssuedBy").find(":selected").val() != "") saveObj.CrBy = $("#hdnUserId").val();
    if ($("#hdnWcRefNo").val() == "0") saveObj.CrDt = output;
    if (submit) saveObj.Status = "Submitted"; else saveObj.Status = "Saved";
    saveObj.ActiveYn = true;
    saveObj.SubmitSts = submit;
    console.log(saveObj);
    $.ajax({
        url: '/InstructedWorks/SaveFormWC',
        data: saveObj,
        type: 'POST',
        success: function (data) {
            HideAjaxLoading();
            if (data == -1) {
                app.ShowErrorMessage("Reference id already Exist");
            }
            else {
                if ($("#FWcHRef_No").val() == "0") {
                    $("#FWcHRef_No").val(data);
                }
                if (submit) {
                    $("#saveWCBtn").hide();
                    $("#submitWCBtn").hide();
                    app.ShowSuccessMessage('Form is Submitted', false);
                    location.href = "/InstructedWorks";
                }
                else {
                    $("#saveWCBtn").show();
                    $("#submitWCBtn").show();
                    app.ShowSuccessMessage('Form is Saved', false);
                    location.href = "/InstructedWorks";
                }
            }
        },
        error: function (data) {
            HideAjaxLoading();
            app.ShowErrorMessage(data.responseText);
        }

    });

}

function SaveWG(submit) {
    if (submit) {
        $("#divFormWg .svalidate").addClass("validate");
    }

    if (!ValidatePage('#Div_wg')) {
        return false;
    }

    InitAjaxLoading();

    var d = new Date();

    var month = d.getMonth() + 1;
    var day = d.getDate();

    var output = (('' + month).length < 2 ? '0' : '') + month + '/' +
        (('' + day).length < 2 ? '0' : '') + day + '/' + d.getFullYear();

    var saveObj = new Object;

    saveObj.PkRefNo = $("#hdnWgRefNo").val();
    saveObj.Fw1PkRefNo = $("#fw1PKRefNo").val();
    saveObj.IwWrksDeptId = $("#formWGDivisionCode option:selected").val();
    saveObj.RmuCode = ""
    saveObj.SecCode = "";
    saveObj.RoadCode = "";
    saveObj.RoadName = "";
    saveObj.Ch = "";
    saveObj.ChDeci = "";
    saveObj.IwRefNo = $("#fw1IWRefNo").val();
    saveObj.IwProjectTitle = $("#formWgProjectTitle").val()
    saveObj.OurRefNo = $("#formWgOurRef").val();
    saveObj.ServRefNo = $("#formWgServiceProvider").val();
    saveObj.DtWg = $("#formWgDate").val();
    saveObj.DtDefectCompl = $("#formWgDftComp").val();

    if ($("#formWgIssuedBy").find(":selected").val() != "") saveObj.UseridIssu = $("#formWgIssuedBy option:selected").val();
    if ($("#formWgIssuedName").val() != "") saveObj.UsernameIssu = $("#formWgIssuedName").val();
    if ($("#formWgIssuedDate").val() != "dd/mm/yyyy" && $("#formWgIssuedDate").val() != "") saveObj.DtIssu = $("#formWgIssuedDate").val();
    saveObj.DesignationIssu = $("#formWgIssuedDesig").val();
    saveObj.OfficeIssu = $("#formWgIssuedOffice").val();
    saveObj.SignIssu = $("#formWgIssuedSig").prop("checked");


    //Created by

    if ($("#formWgIssuedBy").find(":selected").val() != "") saveObj.ModBy = $("#hdnUserId").val();
    if ($("#hdnWgRefNo").val() != "0") saveObj.ModDt = output
    if ($("#formWgIssuedBy").find(":selected").val() != "") saveObj.CrBy = $("#hdnUserId").val();
    if ($("#hdnWgRefNo").val() == "0") saveObj.CrDt = output;
    if (submit) saveObj.Status = "Submitted"; else saveObj.Status = "Saved";
    saveObj.ActiveYn = true;
    saveObj.SubmitSts = submit;
    console.log(saveObj);
    $.ajax({
        url: '/InstructedWorks/SaveFormWG',
        data: saveObj,
        type: 'POST',
        success: function (data) {
            HideAjaxLoading();
            if (data == -1) {
                app.ShowErrorMessage("Reference id already Exist");
            }
            else {
                if ($("#FWgHRef_No").val() == "0") {
                    $("#FWgHRef_No").val(data);
                }
                if (submit) {
                    $("#saveWCBtn").hide();
                    $("#submitWCBtn").hide();
                    app.ShowSuccessMessage('Form is Submitted', false);
                    location.href = "/InstructedWorks";
                }
                else {
                    $("#saveWCBtn").show();
                    $("#submitWCBtn").show();
                    app.ShowSuccessMessage('Form is Saved', false);
                    location.href = "/InstructedWorks";
                }
            }
        },
        error: function (data) {
            HideAjaxLoading();
            app.ShowErrorMessage(data.responseText);
        }

    });

}

function GoBack() {
    if ($("#hdnWgView").val() == "" || $("#hdnWcView").val() == "") {
        if (app.Confirm("Are you sure you want to close the form?", function (e) {
            if (e) {
                location.href = "/InstructedWorks/Index";
            }
        }));
    }
    else
        location.href = "/InstructedWorks/Index";
}

function formatDate(date) {
    var d = new Date(date),
        month = '' + (d.getMonth() + 1),
        day = '' + d.getDate(),
        year = d.getFullYear();

    if (month.length < 2) month = '0' + month;
    if (day.length < 2) day = '0' + day;

    return [day, month, year].join('/');
}

function ClearWG() {
    $("#formWGDivisionCode").val("").trigger("change").trigger("chosen:updated");
    $("#formWgIssuedBy").val("").trigger("change").trigger("chosen:updated");
    $("#formWgDate").val('');
    $("#formWcDtDlpExtn").val('');
    $("#formWcIssuedBy").val('');
    $("#formWgIssuedDate").val('');
}

function ClearWC() {
    $("#formWCDivisionCode").val("").trigger("change").trigger("chosen:updated");
    $("#formWcIssuedBy").val("").trigger("change").trigger("chosen:updated");
    $("#formWcDtCompl").val('');
    $("#formWcDtDlpExtn").val('');
    $("#formWcIssuedBy").val('');
    $("#formWcIssuedDate").val('');
}

function GetImageList(id, form) {
    //debugger;
    var group = $("#FormADetAssetGrpCode option:selected").val();
    if (id && id > 0) {
        $("#fw1IWRefNo").val(id);
    }
    else {
        id = $("#fw1IWRefNo").val();
    }
    $.ajax({
        url: '/InstructedWorks/GetIWImageList',
        data: { id, assetgroup: group, form },
        type: 'POST',
        success: function (data) {
            $("#ViewPhoto").html(data);
            if ($("#hdnView").val() == "1")
                $("div.img-btns *").prop("disabled", true);
            if (form == "FormWCWG") 
                $("divFormType").show();
            else if (form == "FormWDWN")
                $("divFormType").show();
            else
                $("divFormType").hide();
        },
        error: function (data) {
            alert(data.responseText);
        }

    });

    return true;
}

