$(document).ready(function () {
    var val = $("#FN2HRef_No").val();

    if ($("#hdnView").val() == "0" || $("#hdnView").val() == "") {

        if (val == "0") {
            $("#formN2SourceType").chosen();

            $("#formN2rmu").chosen();
            $("#formN2RoadCode").chosen();

            $('#divs1').css("display", "none");
            $('#divqa2').css("display", "none");
            $("#divncn").css("display", "none");
        }
        $("#formN2AttnUserId").chosen();
        $("#formN2CC").chosen();
        $("#formN2IssuedUserId").chosen();
        $("#formN2ReceivedUserId").chosen();
        $("#formN2ServiceProvider").chosen();
        $("#formN2CompletedUserId").chosen();
        $("#formN2AcceptedUserId").chosen();

        $("#formN2IsCorrectionTaken").chosen();
        $("#formN2NCRIssue").chosen();
        $("#formN2OtherFollowUpAction").chosen();
        $("#formN2VerifiedByUserId").chosen();

    }


});



function DeleteHeaderRecord(id) {
    var headerId = id;
    if (confirm("Are you sure you want to delete the record?")) {
        InitAjaxLoading();
        $.ajax({
            url: '/ERT/HeaderListFormDDelete',
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
}


var tempHTTP;
function getHttp() {
    return tempHTTP;
}
$(document).on("click", "#saveFormN2Btn", function () {
    saveHdr(false);
});

$(document).on("click", "#SubmitFormN2Btn", function () {
    saveHdr(true);
});


function saveHdr(isSubmit) {

    if (ValidatePage('#FormAddHdrDetails')) {
        InitAjaxLoading();

        var d = new Date();

        var month = d.getMonth() + 1;
        var day = d.getDate();

        var output = (('' + month).length < 2 ? '0' : '') + month + '/' +
            (('' + day).length < 2 ? '0' : '') + day + '/' + d.getFullYear();

        var saveObj = new Object;

        saveObj.No = $("#FN2HRef_No").val();
        saveObj.SourceType = $("#formN2SourceType").find(":selected").val();
        saveObj.QA2RefNo = $("#formN2QA2RefNo").find(":selected").val();
        saveObj.S1RefNo = $("#formN2S1RefNo").find(":selected").val();
        saveObj.ReferenceID = $("#formN2ReferenceNo").val();
        saveObj.NCNo = $("#formN2NCNo").val();
        saveObj.DivisionName = $("#formN2DivisionDesc").val();
        saveObj.RMU = $("#formN2rmu").find(":selected").val();
        saveObj.ServiceProvider = $("#formN1ServiceProvider").find(":selected").val();

        saveObj.AttentionToUserid = $("#formN2AttnUserId").find(":selected").val();
        saveObj.AttnToUsername = $("#formN2AttentionName").val();

        saveObj.CcUserid = $("#formN2CC").find(":selected").val();
        saveObj.CcUsername = $("#formN2CCName").val();

        saveObj.RoadCode = $("#formN2RoadCode").find(":selected").val();
        saveObj.RoadName = $("#formN2roadDesc").val();
        saveObj.FromChainage = $("#formN2ChinageFrom").val();
        saveObj.FromChainageDeci = $("#formN2ChinageFromDeci").val();
        saveObj.ToChainage = $("#formN2ChinageTo").val();
        saveObj.ToChainageDeci = $("#formN2ChinageToDeci").val();
        saveObj.NcDescription = $("#formN2DesOfNC").val();
        saveObj.ProposedReworkSpecification = $("#formN2ProposedRework").val();

        saveObj.IssuedByUserId = $("#formN2IssuedUserId").find(":selected").val();
        saveObj.IssuedByUsername = $("#formN2IssuedUsername").val();
        saveObj.IssuedByDesignation = $("#formN2IssuedDesig").val();

        saveObj.RecievedByUserId = $("#formN2ReceivedUserId").find(":selected").val();
        saveObj.RecievedByUsername = $("#formN2ReceivedUsername").val();
        saveObj.RecievedByDesignation = $("#formN2ReceivedDesig").val();
        //Accordin 2

        saveObj.CorrectiveUserid = $("#formN2CompletedUserId").find(":selected").val();
        saveObj.CorrectiveUsername = $("#formN2CompletedUsername").val();
        saveObj.CorrectiveDesignation = $("#formN2CompletedDesig").val();
        if ($("#formN2CorrectiveDate").val() != "mm/dd/yyyy") saveObj.CorrectiveDate = $("#formN2CorrectiveDate").val();

        saveObj.AcceptedByUserId = $("#formN2AcceptedUserId").find(":selected").val();
        saveObj.AcceptedByUsername = $("#formN2AcceptedUsername").val();
        saveObj.AcceptedByDesignation = $("#formN2AcceptedDesig").val();
        if ($("#formN2AcceptedDate").val() != "mm/dd/yyyy") saveObj.AcceptedByDate = $("#formN2AcceptedDate").val();

        var taken = $("#formN2IsCorrectionTaken").find(":selected").val();
        saveObj.IsCorrectionTaken = taken == "true" ? true : false;
        var ncr = $("#formN2NCRIssue").find(":selected").val();
        saveObj.ISNcrIssued = ncr == "true" ? true : false;
        if ($("#formN2IssueDate").val() != "mm/dd/yyyy") saveObj.IssuedDate = $("#formN2IssueDate").val();
        saveObj.OtherFollowAction = $("#formN2OtherFollowUpAction").find(":selected").val();
        saveObj.Remarks = $("#formN2Remarks").val();

        saveObj.VerifiedByUserId = $("#formN2VerifiedByUserId").find(":selected").val();
        saveObj.VerifiedByUsername = $("#formN2VerifiedByUsername").val();
        saveObj.VerifiedByDesignation = $("#formN2VerifiedByDesig").val();
        saveObj.VerifiedByDate = output;

        saveObj.CorrectActionTakenDate = output;
        saveObj.RecievedByDate = output;
        //saveObj.ModifiedBy 
        saveObj.ModifiedDate = output;
        //saveObj.CreatedBy
        saveObj.CeratedDate = output;
        saveObj.SubmitStatus = isSubmit
        saveObj.isActive = true

        $.ajax({
            url: '/MAM/SaveN2Hdr',
            data: saveObj,
            type: 'POST',
            success: function (data) {
                if (data == -1) {
                    app.ShowWarningMessage("Reference No. already Exist");
                    HideAjaxLoading();
                }
                else {
                    $("#FN2HRef_No").val(data);

                    $("#val-summary-displayer").css("display", "none");

                    $("#formN2SourceType").chosen('destroy');
                    $("#formN2QA2RefNo").chosen('destroy');
                    $("#formN2S1RefNo").chosen('destroy');
                    $("#formN2rmu").chosen('destroy');
                    $("#formN2RoadCode").chosen('destroy');

                    $("#formN2ChinageFrom").attr("disabled", "disabled").off('click');
                    $("#formN2ChinageFromDeci").attr("disabled", "disabled").off('click');
                    $("#formN2ChinageTo").attr("disabled", "disabled").off('click');
                    $("#formN2ChinageToDeci").attr("disabled", "disabled").off('click');
                    $("#formN2DivisionDesc").attr("disabled", "disabled").off('click');
                    app.ShowSuccessMessage('Successfully Saved', false);
                    location.href = "/MAM/FormN2";
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
