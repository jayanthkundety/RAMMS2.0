$(document).ready(function () {
    var val = $("#FN1HRef_No").val();


    if ($("#hdnView").val() == "0" || $("#hdnView").val() == "") {
        if (val == "0") {   
            $("#formN1rmu").attr("disabled", true);
            $("#formN1RoadCode").attr("disabled", true);
            $("#formN1ChinageFrom").attr("disabled", true);
            $("#formN1ChinageFromDeci").attr("disabled", true);
            $("#formN1ChinageTo").attr("disabled", true);
            $("#formN1ChinageToDeci").attr("disabled", true);
            $("#formN1DivisionDesc").attr("disabled", true);
            $("#formN1rmuDesc").attr("disabled", true);
            $("#formN1roadDesc").attr("disabled", true);
            //$('#formN1NCNo').removeClass("validate");
            $('#formN1S1RefNo').removeClass("validate");
            $('#formN1QA2RefNo').removeClass("validate");

            $('#divs1').css("display","none");
            $('#divqa2').css("display", "none");
            //$("#divncn").css("display", "none");

            $('#formN1AttentionName').attr("disabled", true);
            $('#formN1AttentionDesig').attr("disabled", true);
            $('#formN1CCName').attr("disabled", true);
            $('#formN1CCDesig').attr("disabled", true);
            $('#formN1IssuedUsername').attr("disabled", true);
            $('#formN1IssuedDesig').attr("disabled", true);
            $('#formN1ReceivedUsername').attr("disabled", true);
            $('#formN1ReceivedDesig').attr("disabled", true);
            $('#formN1CompletedUsername').attr("disabled", true);
            $('#formN1CompletedDesig').attr("disabled", true);
            $('#formN1AcceptedUsername').attr("disabled", true);
            $('#formN1AcceptedDesig').attr("disabled", true);
            $('#formN1VerifiedByUsername').attr("disabled", true);
            $('#formN1VerifiedByDesig').attr("disabled", true);

        }
        $("#formN1SourceType").chosen();
        $("#formN1AttnUserId").chosen();
        $("#formN1CC").chosen();
        $("#formN1IssuedUserId").chosen();
        $("#formN1ReceivedUserId").chosen();
        //$("#formN1ServiceProvider").chosen();
        $("#formN1CompletedUserId").chosen();
        $("#formN1AcceptedUserId").chosen();

        $("#formN1IsCorrectionTaken").chosen();
        $("#formN1NCRIssue").chosen();
        $("#formN1OtherFollowUpAction").chosen();
        $("#formN1VerifiedByUserId").chosen();

        $('#formN1ServiceProvider option[value="ENDAYA CONSTRUCTION SDN. BHD."]').prop('selected', true);
        $('#formN1ServiceProvider').trigger("chosen:update");
    }
});

function DeleteHeaderRecord(id) {
    var headerId = id;
    app.Confirm("Are you sure you want to delete the record?, If Yes click OK.", function (e) {
        if (e) {
            InitAjaxLoading();
            $.ajax({
                url: '/MAM/HeaderListFormN1Delete',
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

var tempHTTP;
function getHttp() {
    return tempHTTP;
}
$(document).on("click", "#saveFormN1Btn", function () {

    saveHdr(false);
});

$(document).on("click", "#SubmitFormN1Btn", function () {
    saveHdr(true);
});



function saveHdr(isSubmit) {
    if (isSubmit) {
        $("#FormAddHdrDetails .svalidate").addClass("validate");
    }
   // debugger;
    if (ValidatePage('#FormAddHdrDetails')) {
        InitAjaxLoading();

        var d = new Date();

        var month = d.getMonth() + 1;
        var day = d.getDate();

        var output = (('' + month).length < 2 ? '0' : '') + month + '/' +
            (('' + day).length < 2 ? '0' : '') + day + '/' + d.getFullYear();

        var saveObj = new Object;

        saveObj.No = $("#FN1HRef_No").val();
        saveObj.SourceType = $("#formN1SourceType").find(":selected").val();
        saveObj.QA2RefNo = $("#formN1QA2RefNo").find(":selected").val();
        saveObj.S1RefNo = $("#formN1S1RefNo").find(":selected").val();
        //saveObj.ReferenceID = $("#formN1ReferenceNo").val();

        if (saveObj.QA2RefNo != "") {
            saveObj.ReferenceID = $("#formN1QA2RefNo").find(":selected").text();
        }
        if (saveObj.S1RefNo != "") {
            saveObj.ReferenceID = $("#formN1S1RefNo").find(":selected").text();
        }
        saveObj.NCNo = $("#formN1ReferenceNo").val();
        //saveObj.NCNo = $("#formN1NCNo").val();
        saveObj.DivisionName = $("#formN1DivisionDesc").val();
        saveObj.IssueDateHdr = $("#formN1IssueDateHdr").val();
        saveObj.RMU = $("#formN1rmu").find(":selected").val();
        saveObj.ServiceProvider = $("#formN1ServiceProvider").find(":selected").val();

        saveObj.AttentionToUserid = $("#formN1AttnUserId").find(":selected").val();
        saveObj.AttnToUsername = $("#formN1AttentionName").val();
        saveObj.AttentionToDesignation = $("#formN1AttentionDesig").val();

        saveObj.CcUserid = $("#formN1CC").find(":selected").val();
        saveObj.CcUsername = $("#formN1CCName").val();
        saveObj.CCDesignation = $("#formN1CCDesig").val();

        saveObj.RoadCode = $("#formN1RoadCode").find(":selected").val();
        saveObj.RoadName = $("#formN1roadDesc").val();
        saveObj.FromChainage = $("#formN1ChinageFrom").val();
        saveObj.FromChainageDeci = $("#formN1ChinageFromDeci").val();
        saveObj.ToChainage = $("#formN1ChinageTo").val();
        saveObj.ToChainageDeci = $("#formN1ChinageToDeci").val();
        saveObj.NcDescription = $("#formN1DesOfNC").val();
        saveObj.CorrectionTakenBefore = $("#formN1CorrTakenB4Date").val();
        saveObj.ProposedReworkSpecification = $("#formN1ProposedRework").val();

        saveObj.IssuedByUserId = $("#formN1IssuedUserId").find(":selected").val();
        saveObj.IssuedByUsername = $("#formN1IssuedUsername").val();
        saveObj.IssuedByDesignation = $("#formN1IssuedDesig").val();

        saveObj.RecievedByUserId = $("#hdnformN1ReceivedUserId").val();
        saveObj.RecievedByUsername = $("#formN1ReceivedUsername").val();
        saveObj.RecievedByDesignation = $("#formN1ReceivedDesig").val();
        //Accordin 2

        saveObj.CorrectiveUserid = $("#hdnformN1CompletedUserId").val();
        saveObj.CorrectiveUsername = $("#formN1CompletedUsername").val();
        saveObj.CorrectiveDesignation = $("#formN1CompletedDesig").val();
        if ($("#formN1CorrectiveDate").val() != "mm/dd/yyyy") saveObj.CorrectiveDate = $("#formN1CorrectiveDate").val();

        saveObj.AcceptedByUserId = $("#hdnformN1AcceptedUserId").val();
        saveObj.AcceptedByUsername = $("#formN1AcceptedUsername").val();
        saveObj.AcceptedByDesignation = $("#formN1AcceptedDesig").val();
        if ($("#formN1AcceptedDate").val() != "mm/dd/yyyy") saveObj.AcceptedByDate = $("#formN1AcceptedDate").val();

        var taken = $("#formN1IsCorrectionTaken").find(":selected").val();
        saveObj.IsCorrectionTaken = taken == "true" ? true : false;
        var ncr = $("#formN1NCRIssue").find(":selected").val();
        saveObj.ISNcrIssued = ncr == "true" ? true : false;
        if ($("#formN1IssueDate").val() != "mm/dd/yyyy") saveObj.IssuedDate = $("#formN1IssueDate").val();
        saveObj.OtherFollowAction = $("#formN1OtherFollowUpAction").find(":selected").val();
        saveObj.Remarks = $("#formN1Remarks").val();

        saveObj.VerifiedByUserId = $("#txtformN1VerifiedByUserId").val();
        saveObj.VerifiedByUsername = $("#formN1VerifiedByUsername").val();
        saveObj.VerifiedByDesignation = $("#formN1VerifiedByDesig").val();
        saveObj.VerifiedByDate = output;

        saveObj.CorrectActionTakenDate = output;
        saveObj.RecievedByDate = output;
        //saveObj.ModifiedBy 
        saveObj.ModifiedDate = output;
        //saveObj.CreatedBy
        saveObj.CeratedDate = output;
        saveObj.SubmitStatus = isSubmit
        saveObj.isActive = true
        console.log(saveObj);
        $.ajax({
            url: '/MAM/SaveN1Hdr',
            data: saveObj,
            type: 'POST',
            success: function (data) {
                HideAjaxLoading();
                if (data.refNo == -1) {
                    app.ShowErrorMessage(data.errorMessage);
                   
                }
                else {
                    $("#FN1HRef_No").val(data.refNo);

                    //$("#val-summary-displayer").css("display", "none");

                    $("#formN1SourceType").chosen('destroy');
                    $("#formN1QA2RefNo").chosen('destroy');
                    $("#formN1S1RefNo").chosen('destroy');
                    $("#formN1rmu").chosen('destroy');
                    $("#formN1RoadCode").chosen('destroy');

                    $("#formN1ChinageFrom").attr("disabled", "disabled").off('click');
                    $("#formN1ChinageFromDeci").attr("disabled", "disabled").off('click');
                    $("#formN1ChinageTo").attr("disabled", "disabled").off('click');
                    $("#formN1ChinageToDeci").attr("disabled", "disabled").off('click');
                    $("#formN1DivisionDesc").attr("disabled", "disabled").off('click');
                    app.ShowSuccessMessage('Successfully Saved', false);
                    location.href = "/MAM/FormN1";
                }
            },
            error: function (data) {
                app.ShowErrorMessage(data.responseText, false);
                HideAjaxLoading();
            }

        });
    }
    if (isSubmit) {
        $("#FormAddHdrDetails .svalidate").removeClass("validate");
    }
}
