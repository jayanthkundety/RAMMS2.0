$(document).ready(function () {
    $("#SiteRefmultiSelect").chosen();

});

var saveFormADetList = new Array();

var tempHTTP;
function getHttp() {
    return tempHTTP;
}



function detailSave(callFrom) {
    var ref_this = $(".nav-link.active").attr('href');
    $('.nav-tabs a[href="#FormJTab1"]').tab('show');
    var _detailid = $("#formAFadPKRefNO").val();
    if (ValidatePage("#validateDiv")) {
        if (ref_this == "#FormJPhotoTab1"
            && callFrom == "Save&Continue" && _detailid != "0" && _detailid != "") {
            clearForm();
            $('.nav-tabs a[href="#FormJTab1"]').tab('show');
        }
        else {
            var saveFormADetObj = new Object();
            saveFormADetObj.CallFrom = callFrom;
            saveFormADetObj.Dt = $("#formADt").val();
            saveFormADetObj.DefCode = $("#formADefCode").val();
            saveFormADetObj.SiteRef_multiSelect = $("#formASiteRefmultiSelect").val();
            saveFormADetObj.FromCh = $("#formAFromCh").val();
            saveFormADetObj.FromChDeci = $("#formAFromChDeci").val();
            saveFormADetObj.ToCh = $("#formAToCh").val();
            saveFormADetObj.ToChDeci = $("#formAToChDeci").val();
            saveFormADetObj.Length = $("#formALength").val();
            saveFormADetObj.Width = $("#formAWidth").val();
            saveFormADetObj.Height = $("#formAHeight").val();
            saveFormADetObj.Problem = $("#formProblem").val();
            saveFormADetObj.WorkNeed = $("#formWorkNeed").val();
            saveFormADetObj.Priority = $("#formAPriority").val();
            saveFormADetObj.WI = + $("#formAWI").val();
            saveFormADetObj.WS = + $("#formAWS").val();
            saveFormADetObj.WTC = + $("#formAWTC").val();
            saveFormADetObj.Rt = + $("#formART").val();
            saveFormADetObj.WC = + $("#formAWC").val();
            saveFormADetObj.Remarks = $("#formARemarks").val();
            saveFormADetObj.HeaderNo = $("#formAHeaderNo").val();
            saveFormADetObj.FadRefNO = $("#formADetRefno").val();
            saveFormADetObj.HeaderRefNo = $("#FormADetReferenceNO").val();
            saveFormADetObj.Srno = + $("#formASrno").val();
            saveFormADetObj.No = $("#formAFadPKRefNO").val();
            //saveFormADetList.push(saveFormADetObj);
            console.log(saveFormADetObj);
            $("body").addClass("loading");
            $("#saveContFormADetailsBtn").prop("disabled", true);
            $("#saveExitFormADetailsBtn").prop("disabled", true);

            if (TryParseInt($("#formAFromCh").val()) > TryParseInt($("#formAToCh").val())) {
                app.ShowWarningMessage("Chainge From should not be greater than Chainage To");
            }
            else {

                $.ajax({
                    // url: '/NOD/LoadDetailGridList',
                    url: '/NOD/JDetailsSaveV1',
                    data: saveFormADetObj,
                    type: 'POST',
                    success: function (data) {
                        $("#saveContFormADetailsBtn").prop("disabled", false);
                        $("#saveExitFormADetailsBtn").prop("disabled", false);

                        if (data != 0 && data != null) {
                            $("#formAFadPKRefNO").val(data);
                            if (saveFormADetObj.No != 0) {
                                app.ShowSuccessMessage("Updated Successfully.");
                            }
                            else {
                                app.ShowSuccessMessage("Added successfully");
                                $('.nav-tabs a[href="#FormJPhotoTab1"]').tab('show');
                                GetImageList();
                            }

                        }
                        var id = $("#formAHeaderNo").val();
                        if (id != "") {
                            loadDetailGrid(id);
                        }
                        //$("#DetailListGrid").html(data);                
                        $("body").removeClass("loading");
                        if (callFrom == "Save&Exit") {
                            clearForm();
                            $("#FormJAdddetailsModal").hide();
                        }
                    },
                    error: function (data) {
                        console.log("error");
                        $("body").removeClass("loading");
                        alert(data.responseText);
                        $("#saveContFormADetailsBtn").prop("disabled", false);
                        $("#saveExitFormADetailsBtn").prop("disabled", false);
                    }
                });
            }
        }
    }
}

function loadDetailGrid(id) {
    $.ajax({
        // url: '/NOD/LoadDetailGridList',
        url: '/NOD/JLoadGridList',
        data: {
            detailSave: id
        },
        type: 'POST',
        success: function (data) {
            $("#DetailListGrid").html(data);
            $("body").removeClass("loading");
        },
        error: function (data) {
            console.log("error");
            $("body").removeClass("loading");
            alert(data.responseText);
            $("#saveContFormADetailsBtn").prop("disabled", false);
            $("#saveExitFormADetailsBtn").prop("disabled", false);
        }
    });
}
function close() {
    if ($("#IsViewDetail").val() == "1") {
        $('#FormJAdddetailsModal').modal('hide');
        clearForm();
    }
    else if (app.Confirm("Unsaved changes will be lost. Are you sure you want to cancel?", function (e) {
        if (e) {
            $('#FormJAdddetailsModal').modal('hide');
            clearForm();
        }
    }));

}
function clearForm() {
    var _ref = $("#formADetRefno").val();
    $("#formADt").val("");
    $("#formADefCode").val("").trigger("chosen:updated");
    $("#formASiteRefmultiSelect").val("-1").trigger("chosen:updated");
    $("#formAFromCh").val("");
    $("#formAFromChDeci").val("");
    $("#formAToCh").val("");
    $("#formAToChDeci").val("");
    $("#formADesc").val("");
    $("#formAActCode").val("").trigger("chosen:updated");
    $("#formAUnit").val("").trigger("chosen:updated");
    $("#formALength").val("");
    $("#formAWidth").val("");
    $("#formAHeight").val("");
    $("#formAAdp").val("");
    $("#formACdr").val("");
    $("#formAPriority").val("").trigger("chosen:updated");
    $("#formAWI").val("").trigger("chosen:updated");
    $("#formAWS").val("").trigger("chosen:updated");
    $("#formAWTC").val("").trigger("chosen:updated");
    $("#formAWC").val("").trigger("chosen:updated");
    $("#formART").val("").trigger("chosen:updated");
    $("#formASftPs").val("");
    $("#formASftWis").val("");
    $("#formARemarks").val("");
    $("#formProblem").val("");
    $("#formWorkNeed").val("");
    $("#formAFadPKRefNO").val("0");
    var headerno = $("#formAHeaderNo").val();

    $.ajax({
        url: "/NOD/JGetLatestSRNO?headerno=" + headerno,
        type: "GET",
        success: function (data) {
            var index = _ref.lastIndexOf('/');
            var value = _ref.substring(0, index + 1) + data;
            $("#formADetRefno").val(value);
        },
        error: function (e) {
        }
    });
}

function saveHeader(isSubmit) {
    //debugger;
    if (isSubmit) {
        $("#UserDetails .svalidate").addClass("validate");
    } else {
        $("#UserDetails .svalidate").removeClass("validate");
    }
    Validation.ResetErrStyles("#UserDetails");
    if (ValidatePage(("#headerDiv"), "", "validate")) {
        var saveObj = new Object();
        saveObj.FormADetails = saveFormADetList;
        saveObj.SubmitSts = isSubmit;
        saveObj.RoadCode = $("#FormADetRoadCode").val();
        saveObj.RoadName = $("#FormADetRoadName").val();
        saveObj.Rmu = $("#FormADetRmu").val();
        saveObj.Month = $("#FormADetMonth").val();
        saveObj.Year = $("#FormADetYear").val();
        saveObj.AssetGroupCode = $("#FormADetAssetGrpCode").val();
        saveObj.Id = $("#FormADetReferenceNO").val();
        saveObj.AssetGroupCode = $("#FormADetAssetGrpCode").val();
        saveObj.UsernamePrp = $("#InspectedName").val();
        saveObj.DesignationPrp = $("#InspectedDesignName").val();
        saveObj.DtPrp = $("#InspectedDate").val();
        saveObj.UseridPrp = $("#ddlInspectedby option:selected").val();
        saveObj.UseridVer = $("#ddlCheckedby option:selected").val();
        saveObj.UseridVet = $("#ddlAuditedby option:selected").val();

        saveObj.UsernameVet = $("#AuditedName").val();
        saveObj.DesignationVet = $("#AuditedDesignation").val();
        saveObj.AuditedDt = $("#AuditedDate").val();

        saveObj.UsernameVer = $("#VerifiedName").val();
        saveObj.DesignationVer = $("#VerifiedDesignation").val();
        saveObj.VerifiedDt = $("#VerifiedDate").val();
        saveObj.No = $("#FormADetPKId").val();
        saveObj.section = $("#FormADetsection").val();
        $("body").addClass("loading");
        $("#saveFormABtn").prop("disabled", true);
        $("#SubmitFormABtn").prop("disabled", true);
        $.ajax({
            url: '/NOD/JSave',
            data: saveObj,
            type: 'POST',
            success: function (data) {
                $("body").removeClass("loading");
                $("#saveFormABtn").prop("disabled", false);
                $("#SubmitFormABtn").prop("disabled", false);
                app.ShowSuccessMessage("Successfully Save", false);
                $("#UserDetails .svalidate").removeClass("validate");
            },
            error: function (data) {
                $("body").removeClass("loading");
                app.ShowErrorMessage(data.responseText);
            }

        });
    }
}