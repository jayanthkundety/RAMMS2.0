$(document).ready(function () {
    var val = $("#FN2HRef_No").val();
    if ($("#hdnView").val() == "0" || $("#hdnView").val() == "") {

        if (val == "0") {
            $("#formN2rmu").attr("disabled", true);
            $("#formN2rmuDesc").attr("disabled", true);

            $("#formN2N1RefNo").attr("disabled", true);

            $("#formN2DivisionDesc").attr("disabled", true);

            $('#formN2AttentionName').attr("disabled", true);
            $('#formN2AttentionDesig').attr("disabled", true);
            $('#formN2CCName').attr("disabled", true);
            $('#formN2CCDesig').attr("disabled", true);
            $('#formN2IssuedUsername').attr("disabled", true);
            $('#formN2IssuedDesig').attr("disabled", true);
            $('#formN2ReceivedUsername').attr("disabled", true);
            $('#formN2ReceivedDesig').attr("disabled", true);
            $('#formN2CompletedUsername').attr("disabled", true);
            $('#formN2CompletedDesig').attr("disabled", true);
            $('#formN2AcceptedUsername').attr("disabled", true);
            $('#formN2AcceptedDesig').attr("disabled", true);
            $('#formN2PRCompletedUsername').attr("disabled", true);
            $('#formN2PRCompletedDesig').attr("disabled", true);
            $('#formN2PRAcceptedUsername').attr("disabled", true);
            $('#formN2PRAcceptedDesig').attr("disabled", true);
            $('#formN2VerifiedByUsername').attr("disabled", true);
            $('#formN2VerifiedByDesig').attr("disabled", true);
        }
        $("#formN2AttnUserId").chosen();
        $("#formN2CC").chosen();
        $("#formN2IssuedUserId").chosen();
        $("#formN2ReceivedUserId").chosen();
        //$("#formN2ServiceProvider").chosen();
        $("#formN2CompletedUserId").chosen();
        $("#formN2AcceptedUserId").chosen();
        $("#formN2PRCompletedUserId").chosen();
        $("#formN2PRAcceptedUserId").chosen();
        $("#formN2OtherFollowUpAction").chosen();
        $("#formN2VerifiedByUserId").chosen();
        $('#formN2ServiceProvider option[value="ENDAYA CONSTRUCTION SDN. BHD."]').prop('selected', true);
        $('#formN2Region option[value="Sarawak"]').prop('selected', true);
    }
});


$(document).on("click", "#saveFormN2Btn", function () {
    saveHdr(false);
});

$(document).on("click", "#SubmitFormN2Btn", function () {
    saveHdr(true);
});

function saveHdr(isSubmit) {
    if (isSubmit) {
        $("#FormAddHdrDetails .svalidate").addClass("validate");
    }
    if (ValidatePage('#FormAddHdrDetails')) {
        InitAjaxLoading();

        var d = new Date();

        var month = d.getMonth() + 1;
        var day = d.getDate();

        var output = (('' + month).length < 2 ? '0' : '') + month + '/' +
            (('' + day).length < 2 ? '0' : '') + day + '/' + d.getFullYear();

        var saveObj = new Object;
        saveObj.No = $("#FN2HRef_No").val();
        saveObj.ReferenceID = $("#formN2N1RefNo").find(":selected").text();
        saveObj.NcrNo = $("#formN2ReferenceNo").val();
        saveObj.SourceType = $("#formN2SourceType").find(":selected").val();
        saveObj.FormN1RefNo = $("#formN2N1RefNo").find(":selected").val();
        //
        //saveObj.NcrNo = $("#formN2NCNo").val();
        saveObj.RMU = $("#formN2rmu").find(":selected").val();
        saveObj.Region = $("#formN2Region").val();

        saveObj.Division = $("#formN2DivisionDesc").val();
       
        saveObj.ServiceProvider = $("#formN2ServiceProvider").find(":selected").val();
        if ($("#formN2IssueDate").val() != "mm/dd/yyyy") saveObj.IssuedDate = $("#formN2IssueDate").val();

        saveObj.AttentionTo = $("#formN2AttnUserId").find(":selected").val();
        saveObj.AttentionToUsername = $("#formN2AttentionName").val();
        saveObj.AttentionToDesignation = $("#formN2AttentionDesig").val();

        saveObj.Cc = $("#formN2CC").find(":selected").val();
        saveObj.CCUsername = $("#formN2CCName").val();
        saveObj.CCDesignation = $("#formN2CCDesig").val();

        saveObj.Subject = $("#formN2Subject").val();
       
        saveObj.NonConfDetail = $("#formN2DesOfNC").val();
        saveObj.ProposedCrctAct = $("#formN2ProposedRework").val();

        saveObj.PreventiveAction = $("#formN2PreventiveAction").val();

        saveObj.IssuedByUserId = $("#formN2IssuedUserId").find(":selected").val();
        saveObj.IssuedByUsername = $("#formN2IssuedUsername").val();
        saveObj.IssuedByDesignation = $("#formN2IssuedDesig").val();

        saveObj.RcvdByUserid = $("#formN2ReceivedUserId").find(":selected").val();
        saveObj.RcvdByUsername = $("#formN2ReceivedUsername").val();
        saveObj.RcvdByDesignation = $("#formN2ReceivedDesig").val();
       
        //Corrective Action to Completed By
        saveObj.CorrectiveUserId = $("#formN2CompletedUserId").find(":selected").val();
        saveObj.CorrectiveUsername = $("#formN2CompletedUsername").val();
        saveObj.CorrectiveDesignation = $("#formN2CompletedDesig").val();
        if ($("#formN2CorrectiveDate").val() != "mm/dd/yyyy") saveObj.CorrectiveDate = $("#formN2CorrectiveDate").val();

        saveObj.AccptdByUserId = $("#formN2AcceptedUserId").find(":selected").val();
        saveObj.AccptdByUsername = $("#formN2AcceptedUsername").val();
        saveObj.AccptdByDesignation = $("#formN2AcceptedDesig").val();
        if ($("#formN2AcceptedDate").val() != "mm/dd/yyyy") saveObj.AccptdByDate = $("#formN2AcceptedDate").val();

        //Action to prevent recurrence to be completed by
        saveObj.PreventiveUserId = $("#formN2PRCompletedUserId").find(":selected").val();
        saveObj.PreventiveUsername = $("#formN2PRCompletedUsername").val();
        saveObj.PreventiveDesignation = $("#formN2PRCompletedDesig").val();
        if ($("#formN2CorrectiveDate").val() != "mm/dd/yyyy") saveObj.PreventiveDate = $("#formN2PRCompletedDate").val();

        saveObj.PreventiveAcceptedUserId = $("#formN2PRAcceptedUserId").find(":selected").val();
        saveObj.PreventiveAcceptedUsername = $("#formN2PRAcceptedUsername").val();
        saveObj.PreventiveAcceptedDessig = $("#formN2PRAcceptedDesig").val();
        if ($("#formN2PRAcceptedDate").val() != "mm/dd/yyyy") saveObj.PreventiveAcceptedDate = $("#formN2PRAcceptedDate").val();   

        if ($("#formN2FollowUpDate").val() != "mm/dd/yyyy") saveObj.CloseOutDate = $("#formN2FollowUpDate").val(); 
         //Accordin 2
        saveObj.OthrrFollowUpAction = $("#formN2OtherFollowUpAction").find(":selected").val();
        saveObj.CloseOutRemarks = $("#formN2Remarks").val();

        saveObj.VerifiedByUserId = $("#formN2VerifiedByUserId").find(":selected").val();
        saveObj.VerifiedByUsername = $("#formN2VerifiedByUsername").val();
        saveObj.VerifiedByDesignation = $("#formN2VerifiedByDesig").val();
        saveObj.VerifiedByDate = output;

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
                    app.ShowWarningMessage("Form N2 No. already Exist");
                    HideAjaxLoading();
                }
                else {
                    $("#FN2HRef_No").val(data);

                    $("#val-summary-displayer").css("display", "none");

                    $("#formN2SourceType").chosen('destroy');
                    $("#formN2N1RefNo").chosen('destroy');
                    $("#formN2rmu").chosen('destroy');

                    $("#formN2SourceType").attr("disabled", "disabled");
                    $("#formN2Region").attr("disabled", "disabled");
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
    if (isSubmit) {
        $("#FormAddHdrDetails .svalidate").removeClass("validate");
    }
}
