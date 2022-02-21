var currentDate = new Date();
$(document).ready(function () {
    currentDate = formatDate(currentDate);

    $('.allow_numeric').keypress(function (event) {
        var $this = $(this);
        if ((event.which != 46 || $this.val().indexOf('.') != -1) &&
            ((event.which < 48 || event.which > 57) &&
                (event.which != 0 && event.which != 8))) {
            event.preventDefault();
        }

        var text = $(this).val();
        if ((event.which == 46) && (text.indexOf('.') == -1)) {
            setTimeout(function () {
                if ($this.val().substring($this.val().indexOf('.')).length > 3) {
                    $this.val($this.val().substring(0, $this.val().indexOf('.') + 3));
                }
            }, 1);
        }

        if ((text.indexOf('.') != -1) &&
            (text.substring(text.indexOf('.')).length > 3) &&
            (event.which != 0 && event.which != 8) &&
            ($(this)[0].selectionStart >= text.length - 3)) {
            event.preventDefault();
        }
    });

    $("#formW2IssuedBy").on("change", function () {
        var id = $("#formW2IssuedBy option:selected").val();
        if (id != "99999999" && id != "") {
            $.ajax({
                url: '/ERT/GetUserById',
                dataType: 'JSON',
                data: { id },
                type: 'Post',
                success: function (data) {
                    $("#formW2IssuedName").val(data.userName);
                    $("#formW2IssuedName").prop("disabled", true);
                },
                error: function (data) {
                    console.error(data);
                }
            });
        }
        else if (id == "99999999") {
            $("#formW2IssuedName").prop("disabled", false);
            $("#formW2IssuedName").val('');
        }
        else {
            $("#formW2IssuedName").prop("disabled", true);
            $("#formW2IssuedName").val('');
        }
        $("#formW2IssuedDate").val(currentDate);
        return false;
    });


    $("#formW2CommencementDate").change(function () {
        var value = $(this).val();
        var commDate = new Date(formatDate(value));
        var minDate = new Date(2020, 0, 1);
        if (commDate < minDate) {
            app.ShowErrorMessage("Commencement date should not be less than 01-01-2020");
            $(this).val('');
            return;
        }

    });


    $("#formW2CompletionDate").change(function () {
        var value = $(this).val();
        var dateVal = $("#formW2CommencementDate").val();
        commDate = new Date(formatDate(dateVal));

        var compDate = new Date(formatDate(value));

        if (compDate < commDate) {
            app.ShowErrorMessage("Completion date should not be less than Commencment date");
            $(this).val('');
            return;
        }

    });


    if ($("#FW2HRef_No").val() == "0") {

        $("#formW2DivisionCode").trigger("chosen:updated")
        $("#formW2DivisionCode").trigger("change");

        $('#formW2RMU').trigger("chosen:updated")
        $("#formW2RMU").trigger("change");
    }
    else {
        $("#formW2DivisionCode").trigger("chosen:updated");
        $('#formW2RMU').trigger("chosen:updated");
    }


});

$(document).on("click", "#saveFormW2Btn", function () {

    Save(false);
});

$(document).on("click", "#submitFormW2Btn", function () {
    Save(true);
});

$(document).on("click", "#saveFCEMBtn", function () {

    SaveFCEM(false);
});

$(document).on("click", "#submitFCEMBtn", function () {
    SaveFCEM(true);
});


function SaveFCEM(submit) {
    if (submit) {
        $("#divPage .svalidate").addClass("validate");
    }

    if (!ValidatePage('#divPage')) {
        return false;
    }

    InitAjaxLoading();

    var d = new Date();

    var month = d.getMonth() + 1;
    var day = d.getDate();

    var output = (('' + month).length < 2 ? '0' : '') + month + '/' +
        (('' + day).length < 2 ? '0' : '') + day + '/' + d.getFullYear();

    var saveObj = new Object;

    saveObj.PkRefNo = $("#fcemPkRefNo").val();
    saveObj.Fw2PkRefNo = $("#FW2HRef_No").val();
    saveObj.Date = $("#fcemDate").val();
    saveObj.Sstatus = $("#fcemStatus").find(":selected").val();
    saveObj.Progress = $("#fcemProgress").val();
    saveObj.IsBq = $("#fcemBQ").prop("checked");
    saveObj.IsDrawing = $("#fcemDrawing").prop("checked");
    saveObj.Remark = $("#fcemRemark").val();

    //Created by
    if ($("#formw2RequestedBy").find(":selected").val() != "") saveObj.ModBy = $("#formw2RequestedBy").find(":selected").val();
    if ($("#FW2HRef_No").val() != "") saveObj.ModDt = output
    if ($("#formW2IssuedBy").find(":selected").val() != "") saveObj.CrBy = $("#formW2IssuedBy").find(":selected").val();
    if ($("#FW2HRef_No").val() == "") saveObj.CrDt = output;

    saveObj.ActiveYn = true;
    saveObj.SubmitSts = submit;
    console.log(saveObj);
    $.ajax({
        url: '/InstructedWorks/SaveFCEM',
        data: saveObj,
        type: 'POST',
        success: function (data) {
            HideAjaxLoading();
            if (data == -1) {
                app.ShowErrorMessage("Reference id already Exist");
            }
            else {
                $("#FW2HRef_No").val(data);
                if (submit) {
                    $("#saveFCEMBtn").hide();
                    $("#submitFCEMBtn").hide();
                    app.ShowSuccessMessage('Successfully Submitted', false);
                    location.href = "/InstructedWorks";
                }
                else {
                    $("#saveFCEMBtn").show();
                    $("#submitFCEMBtn").show();
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

function openW1() {
    $("#saveFormW2Btn").hide();
    $("#submitFormW2Btn").hide();

    $("#saveFCEMBtn").hide();
    $("#submitFCEMBtn").hide();
}

function openFCEM() {
    $("#saveFormW2Btn").hide();
    $("#submitFormW2Btn").hide();

    $("#saveFCEMBtn").show();
    $("#submitFCEMBtn").show();
}

function checkW2Exist() {
    if (ValidatePage("#divpage")) {
        if ($("#FW2HRef_No").val() == "0" || $("#FW2HRef_No").val() == "") {
            $("#lnkPage").click();
            app.ShowErrorMessage("Required to save the Form W2 details and then try to create FCEM");
        }
    }
    else {
        $("#saveFormW2Btn").click();
        app.ShowErrorMessage("Required fields are incomplete in Form W2");
    }
}

function openW2() {

    $("#saveFormW2Btn").show();
    $("#submitFormW2Btn").show();

    $("#saveFCEMBtn").hide();
    $("#submitFCEMBtn").hide();
}

function Save(submit) {
    if (submit) {
        $("#divPage .svalidate").addClass("validate");
    }

    if (!ValidatePage('#divPage')) {
        return false;
    }

    InitAjaxLoading();

    var d = new Date();

    var month = d.getMonth() + 1;
    var day = d.getDate();

    var output = (('' + month).length < 2 ? '0' : '') + month + '/' +
        (('' + day).length < 2 ? '0' : '') + day + '/' + d.getFullYear();

    var saveObj = new Object;

    saveObj.PkRefNo = $("#FW2HRef_No").val();

    saveObj.Fw1IwRefNo = $("#fw1IWRefNo").val();
    saveObj.Fw1PkRefNo = $("#fw1PKRefNo").val();
    saveObj.Fw1ProjectTitle = $("#fw1ProjectTitle").val()
    saveObj.RegionText = $("#formW2Region").val();
    saveObj.RegionName = $("#formW2RegionName").val();
    saveObj
    saveObj.DivCode = $("#formW2DivisionCode").find(":selected").val();
    saveObj.DivisonName = $("#formW2DivisonName").val();

    saveObj.Rmu = $("#formW2RMU").find(":selected").val();
    saveObj.RmuName = $("#formW2RMUName").val();

    saveObj.JkrRefNo = $("#fw2JkrRefNo").val();
    saveObj.DateOfInitation = $("#formW2InitiationDate").val();
    saveObj.SerProvRefNo = $("#fw2SerProviderRef").val();
    saveObj.ServProvName = $("#formW2ServiceProvider").find(":selected").val();
    saveObj.Attn = $("#fw2Attn").val();
    saveObj.Cc = $("#fw2cc").val();
    saveObj.RoadCode = $("#frmW2RoadCode").val().split("-")[0];
    saveObj.RoadName = $("#formW2roadDesc").val();

    if ($("#formW2chkm").val() != "") saveObj.Ch = $("#formW2chkm").val();
    if ($("#formW2chm").val() != "") saveObj.ChDeci = $("#formW2chm").val();
    //saveObj.TitleOfInstructWork = $("#formW2TitleOfInstructWork").val();
    if ($("#formW2CommencementDate").val() != "dd/mm/yyyy" && $("#formW2CommencementDate").val() != "") saveObj.DtCommence = $("#formW2CommencementDate").val();
    if ($("#formW2CompletionDate").val() != "dd/mm/yyyy" && $("#formW2CompletionDate").val() != "") saveObj.DtCompl = $("#formW2CompletionDate").val();
    if ($("#fw2InstructWorkDuration").val() != "") saveObj.IwDuration = $("#fw2InstructWorkDuration").val();
    saveObj.Remarks = $("#formW2Remarks").val();
    saveObj.DetailsOfWorks = $("#formW2DetailsOfWorks").val();
    saveObj.EstCostAmt = $("#formW2EstCost").val();

    if ($("#formW2IssuedBy").find(":selected").val() != "") saveObj.UseridIssu = $("#formW2IssuedBy option:selected").val();
    if ($("#formW2IssuedName").val() != "") saveObj.UsernameIssu = $("#formW2IssuedName").val();
    if ($("#formW2IssuedDate").val() != "dd/mm/yyyy" && $("#formW2IssuedDate").val() != "") saveObj.DtIssu = $("#formW2IssuedDate").val();
    saveObj.DesignationIssu = "";
    saveObj.OfficeIssu = "";
    saveObj.SignIssu = $("#formW2IssuedSignture").prop("checked");

    if ($("#formw2RequestedBy").find(":selected").val() != "") saveObj.UseridReq = $("#formw2RequestedBy option:selected").val();
    if ($("#formW2RequestedName").val() != "") saveObj.UsernameReq = $("#formW2RequestedName").val();
    if ($("#formW2RequestedDate").val() != "dd/mm/yyyy" && $("#formW2RequestedDate").val() != "") saveObj.DtReq = $("#formW2RequestedDate").val();
    saveObj.DesignationReq = "";
    saveObj.OfficeReq = "";
    saveObj.SignReq = $("#formW2RequestedSign").prop("checked");

    //Created by

    if ($("#formw2RequestedBy").find(":selected").val() != "") saveObj.ModBy = $("#formw2RequestedBy").find(":selected").val();
    if ($("#FW2HRef_No").val() != "0") saveObj.ModDt = output
    if ($("#formW2IssuedBy").find(":selected").val() != "") saveObj.CrBy = $("#formW2IssuedBy").find(":selected").val();
    if ($("#FW2HRef_No").val() == "0") saveObj.CrDt = output;

    saveObj.ActiveYn = true;
    saveObj.SubmitSts = submit;
    console.log(saveObj);
    $.ajax({
        url: '/InstructedWorks/SaveFormW2',
        data: saveObj,
        type: 'POST',
        success: function (data) {
            HideAjaxLoading();
            if (data == -1) {
                app.ShowErrorMessage("Reference id already Exist");
            }
            else {
                $("#FW2HRef_No").val(data);
                if (submit) {
                    $("#saveFormW2Btn").hide();
                    $("#submitFormW2Btn").hide();
                    app.ShowSuccessMessage('Successfully Submitted', false);
                    location.href = "/InstructedWorks";
                }
                else {
                    $("#saveFormW2Btn").show();
                    $("#submitFormW2Btn").show();
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

function Delete(id) {
    var headerId = id;
    app.Confirm("Are you sure you want to delete the record?, If Yes click OK.", function (e) {
        if (e) {
            InitAjaxLoading();
            $.ajax({
                url: '/InstructedWorks/Delete',
                data: { headerId },
                type: 'POST',
                success: function (data) {
                    if (data > 0) {
                        app.ShowSuccessMessage('Successfully Deleted', false);
                        FormDGridRefresh();
                    }
                    else {
                        app.ShowErrorMessage("Error in Deleted. Kindly retry later.", false);
                    }
                    HideAjaxLoading();
                }
            });
        }
    });
}

function GetImageList(id) {
    var group = $("#FormADetAssetGrpCode option:selected").val();

    $("#saveFormW2Btn").hide();
    $("#submitFormW2Btn").hide();
    $("#saveFCEMBtn").hide();
    $("#submitFCEMBtn").hide();

    if (id && id > 0) {
        $("#fw1IWRefNo").val(id);
    }
    else {
        id = $("#fw1IWRefNo").val();
    }

    $.ajax({
        url: '/InstructedWorks/GetIWImageList',
        data: { id, assetgroup: group },
        type: 'POST',
        success: function (data) {
            $("#ViewPhoto").html(data);
            //$("#FW2HRef_No").val(id);
        },
        error: function (data) {
            alert(data.responseText);
        }

    });

    return true;
}

//function changeRegion(obj) {
//    var ctrl = $(obj);

//    if (ctrl.val() != null && ctrl.val() != "") {
//        var name = ctrl.find("option:selected").text();
//        name = name.split('-').length > 1 ? name.split('-')[1] : name;
//        $("#formW2RegionName").val(name);
//    }
//    else {
//        $("#formW2RegionName").val('');
//    }
//}

function changeDivision(obj) {
    var ctrl = $(obj);
    if (ctrl.val() != null && ctrl.val() != "") {
        var name = ctrl.find("option:selected").text();
        name = name.split('-').length > 1 ? name.split('-')[1] : name;
        $("#formW2DivisonName").val(name);
    }
    else {
        $("#formW2DivisonName").val('');
    }
}

function changeRMU(obj) {
    var ctrl = $(obj);
    if (ctrl.val() != null && ctrl.val() != "") {
        var name = ctrl.find("option:selected").text();
        name = name.split('-')[1];
        $("#formW2RMUName").val(name);

        $.ajax({
            url: '/InstructedWorks/GetRoadCodeByRMU',
            dataType: 'JSON',
            data: { rmu: ctrl.val() },
            type: 'Post',
            success: function (data) {
                debugger;
                if (data != null) {
                    $('#frmW2RoadCodeDD').empty();
                    $('#frmW2RoadCodeDD')
                        .append($("<option></option>")
                            .attr("value", "")
                            .text("Select Road Code"));
                    $.each(data, function (key, value) {
                        $('#frmW2RoadCodeDD')
                            .append($("<option></option>")
                                .attr("item1", value.item1)
                                .attr("fromkm", value.fromKm)
                                .attr("fromm", value.fromM)
                                .attr("value", value.value)
                                .text(value.text));
                    });
                    $("#frmW2RoadCodeDD").val($('#frmW2RoadCode').val());
                    $('#frmW2RoadCodeDD').trigger("chosen:updated")
                    $("#frmW2RoadCodeDD").trigger("change");
                }
            },
            error: function (data) {

                console.error(data);
            }
        });
    }
    else {
        $("#formW2RMUName").val('');
    }
}

function GetW1Details(obj) {
    var w1PkRefNo = $(obj).val();
    InitAjaxLoading();
    $.ajax({
        url: '/InstructedWorks/GetW1Details',
        dataType: 'JSON',
        data: { w1PkRefNo },
        type: 'Post',
        success: function (data) {
            if (data != null) {
                $("#fw1RefNo").val(data.referenceNo);
                $("#fw1PKRefNo").val(data.pkRefNo);
                $("#fw1ProjectTitle").val(data.projectTitle);
                $("#formW2TitleOfInstructWork").val(data.detailsOfWork);
                $("#formW2EstCost").val(data.estimTotalCost);
                $("#formW2RMU").val("MIRI");

                //$("#formW2RMUName").val();
                $("#fw2SerProviderRef").val(data.servPropRefNo);
                $("#formW2ServiceProvider").val();
                $("#address1").val(data.servAddress1);
                $("#address2").val(data.servAddress2);
                $("#address3").val(data.servAddress3);
                $("#phone").val(data.servPhone);
                $("#fax").val(data.servFax);
                $("#frmW2RoadCodeDD").val(data.roadCode);
            }
            HideAjaxLoading();
        },
        error: function (data) {
            console.error(data);
        }
    });
}

function OnRoadChange(tis) {
    var ctrl = $(tis);
    debugger;
    if (ctrl.val() != null && ctrl.val() != "") {
        $("#formW2roadDesc").val(ctrl.find("option:selected").attr("Item1"));
        $("#formW2chkm").val(ctrl.find("option:selected").attr("fromkm"));
        $("#formW2chm").val(ctrl.find("option:selected").attr("fromm"));
        $("#frmW2RoadCode").val(ctrl.val());
    }
    else {
        $("#formW2roadDesc").val('');
        $("#formW2chkm").val('');
        $("#formW2chm").val('');
    }

}

function ChangeRUser(obj) {
    var id = $(obj).find("option:selected").val();
    if (id != "99999999" && id != "") {
        $.ajax({
            url: '/ERT/GetUserById',
            dataType: 'JSON',
            data: { id },
            type: 'Post',
            success: function (data) {
                if (data != null) {
                    $("#formW2RequestedName").val(data.userName);
                    $("#formW2RequestedName").prop("disabled", true);
                }
            },
            error: function (data) {
                console.error(data);
            }
        });
    }
    else if (id == "99999999") {
        $("#formW2RequestedName").prop("disabled", false);
        $("#formW2RequestedName").val('');
    }
    else {
        $("#formW2RequestedName").prop("disabled", true);
        $("#formW2RequestedName").val('');
    }
    $("#formW2RequestedDate").val(currentDate);
    return false;
}

function GoBack() {
    if ($("#hdnView").val() == "0" || $("#hdnView").val() == "") {
        if (app.Confirm("Unsaved changes will be lost. Are you sure you want to cancel?", function (e) {
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

    return [year, month, day].join('-');
}

