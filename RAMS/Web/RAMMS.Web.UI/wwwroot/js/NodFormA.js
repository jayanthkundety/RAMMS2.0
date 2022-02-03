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
    $('.nav-tabs a[href="#FormATabPage1"]').tab('show');
    var _detailid = $("#formAFadPKRefNO").val();
    if (ValidatePage("#AddFormADetForm", "", "validate")) {
        if ( ref_this == "#FormATabPage2"
            && callFrom == "Save&Continue" && _detailid != "0" && _detailid != "") {
            clearForm();
            $('.nav-tabs a[href="#FormATabPage1"]').tab('show');
        }
        else  {
            var saveFormADetObj = new Object();
            saveFormADetObj.CallFrom = callFrom;
            saveFormADetObj.Dt = $("#formADt").val();
            saveFormADetObj.DefCode = $("#formADefCode").val();
            saveFormADetObj.SiteRef_multiSelect = $("#formASiteRefmultiSelect").val();
            saveFormADetObj.FromCh = $("#formAFromCh").val();
            saveFormADetObj.FromChDeci = $("#formAFromChDeci").val();
            saveFormADetObj.ToCh = $("#formAToCh").val();
            saveFormADetObj.ToChDeci = $("#formAToChDeci").val();
            saveFormADetObj.Desc = $("#formADesc").val();
            saveFormADetObj.ActCode = $("#formAActCode").val();
            saveFormADetObj.Unit = $("#formAUnit").val();
            saveFormADetObj.Length = $("#formALength").val();
            saveFormADetObj.Width = $("#formAWidth").val();
            saveFormADetObj.Height = $("#formAHeight").val();
            saveFormADetObj.Adp = $("#formAAdp").val();
            saveFormADetObj.Cdr = $("#formACdr").val();
            saveFormADetObj.Priority = $("#formAPriority").val();
            saveFormADetObj.WI = $("#formAWI").val();
            saveFormADetObj.WS = $("#formAWS").val();
            saveFormADetObj.WTC = $("#formAWTC").val();
            saveFormADetObj.WC = $("#formAWC").val();
            saveFormADetObj.SftPs = $("#formASftPs").val();
            saveFormADetObj.SftWis = $("#formASftWis").val();
            saveFormADetObj.RT = $("#formART").val();
            saveFormADetObj.Remarks = $("#formARemarks").val();
            saveFormADetObj.HeaderNo = $("#formAHeaderNo").val();
            saveFormADetObj.FadRefNO = $("#formADetRefno").val();
            saveFormADetObj.HeaderRefNo = $("#FormADetReferenceNO").val();
            saveFormADetObj.Srno = $("#formASrno").val();
            saveFormADetObj.No = $("#formAFadPKRefNO").val();
            //saveFormADetList.push(saveFormADetObj);
            console.log(saveFormADetObj);
            InitAjaxLoading
            $("#saveContFormADetailsBtn").prop("disabled", true);
            $("#saveExitFormADetailsBtn").prop("disabled", true);
            $.ajax({
                // url: '/NOD/LoadDetailGridList',
                url: '/NOD/DetailsSaveV1',
                data: saveFormADetObj,
                type: 'POST',
                success: function (data) {
                    HideAjaxLoading();
                    $("#saveContFormADetailsBtn").prop("disabled", false);
                    $("#saveExitFormADetailsBtn").prop("disabled", false);


                    if (data != 0 && data != null) {
                        $("#formAFadPKRefNO").val(data);
                        if (saveFormADetObj.No != 0) {
                            app.ShowSuccessMessage("Updated Successfully.");
                        }
                        else {
                            app.ShowSuccessMessage("Added successfully");
                            $('.nav-tabs a[href="#FormATabPage2"]').tab('show');
                            GetImageList();
                        }

                    }
                    var id = $("#formAHeaderNo").val();
                    if (id != "") {
                        loadDetailGrid(id);
                    }                   
                    if (callFrom == "Save&Exit") {
                        clearForm();
                        $("#FormAAdddetailsModal").hide();
                    }
                },
                error: function (data) {
                    console.log("error");
                    HideAjaxLoading();
                    app.ShowErrorMessage(data.responseText);
                    $("#saveContFormADetailsBtn").prop("disabled", false);
                    $("#saveExitFormADetailsBtn").prop("disabled", false);
                }
            });
        }
    }
}

function loadDetailGrid(id) {
    InitAjaxLoading();
    $.ajax({
        // url: '/NOD/LoadDetailGridList',
        url: '/NOD/LoadGridList',
        data: {
            detailSave: id
        },
        type: 'POST',
        success: function (data) {
            $("#DetailListGrid").html(data);
            HideAjaxLoading();
        },
        error: function (data) {
            console.log("error");
            HideAjaxLoading();
            app.ShowErrorMessage(data.responseText);
            $("#saveContFormADetailsBtn").prop("disabled", false);
            $("#saveExitFormADetailsBtn").prop("disabled", false);
        }
    });
}

function close() {
    if ($("#IsViewDetail").val() == "1") {
        $('#FormAAdddetailsModal').modal('hide');
        clearForm();
    }
    else if (app.Confirm("Unsaved changes will be lost. Are you sure you want to cancel?", function (e) {
        if (e) {
            $('#FormAAdddetailsModal').modal('hide');
            clearForm();
            //$(".disableinput").prop("disabled", false);
        }
    }));
}
function clearForm() {
    var _ref = $("#formADetRefno").val();
    $("#formASiteRefmultiSelect").val("").trigger("chosen:updated");
    $("#formADt").val("");
    $("#formADefCode").val("").trigger("chosen:updated");
    $("#formASiteRefmultiSelect").val("");
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
    $("#formASftPs").val("").trigger("chosen:updated");
    $("#formASftWis").val("").trigger("chosen:updated");
    $("#formART").val("").trigger("chosen:updated");
    $("#formARemarks").val("");
    $("#formAFadPKRefNO").val("0");
    var headerno = $("#formAHeaderNo").val();

    $.ajax({
        url: "/NOD/GetLatestSRNO?headerno=" + headerno,
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



//    $.ajax({
//       // url: '/NOD/LoadDetailGridList',
//        url: '/NOD/DetailsSave',
//        data:  saveFormADetObj ,
//        type: 'POST',
//        success: function (data) {
//            console.log(data);
//            //debugger;
//            alert("Added Successfully.");
//            $("#DetailListGrid").html(data);
//            $("body").removeClass("loading");
//            $("#AddNODModal").hide();
//        },
//        error: function (data) {
//            console.log("error");
//            $("body").removeClass("loading");
//            alert(data.responseText);
//        }
//    });
//});

function cancel() {
    if ($("#IsViewDetail").val() == "1") {
        $("#FormAAddModal").modal('hide');
    }
    else if (app.Confirm("Unsaved changes will be lost. Are you sure you want to cancel?", function (e) {
        if (e) {
            $("#FormAAddModal").modal('hide');
        }
    }));
}

function saveHeader(isSubmit) {
    //debugger;
    if (isSubmit) {
        $("#divDetails .svalidate").addClass("validate");
    }
    else {
        $("#divDetails .svalidate").removeClass("validate");
    }
    Validation.ResetErrStyles("#divDetails");
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
        saveObj.UseridVer = $("#ddlVerifiedby option:selected").val();
        saveObj.UsernameVer = $("#VerifiedName").val();
        saveObj.DesignationVer = $("#VerifiedDesignation").val();
        saveObj.VerifiedDt = $("#VerifiedDate").val();
        saveObj.No = $("#FormADetPKId").val();
        saveObj.section = $("#FormADetsection").val();
        InitAjaxLoading();
        $("#saveFormABtn").prop("disabled", true);
        $("#SubmitFormABtn").prop("disabled", true);
        $.ajax({
            url: '/NOD/Save',
            data: saveObj,
            type: 'POST',
            success: function (data) {
                //debugger;
                HideAjaxLoading();
                searchHeaderList();
                $("#saveFormABtn").prop("disabled", false);
                $("#SubmitFormABtn").prop("disabled", false);
                app.ShowSuccessMessage('Successfully Saved', false);
                $("#cancelFormABtn").click();
                if (isSubmit) {
                    $("#FormAAddModal").modal('hide');
                    $("#divDetails .svalidate").removeClass("validate");
                }
            },
            error: function (data) {
                HideAjaxLoading();
                app.ShowErrorMessage(data.responseText);
            }

        });
    }
}