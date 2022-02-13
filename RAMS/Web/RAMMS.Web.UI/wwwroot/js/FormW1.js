$(document).ready(function () {

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

    $('[data-toggle="tooltip"]').tooltip();

   


    $('input[type=radio][id=FormW1_RecommendedInstructedWork]').change(function () {

        if (this.value == 'Critical' || this.value == 'Urgent') {
            $('#ddlVerifiedBy').prop("disabled", false);
            $('#ddlVerifiedBy').trigger('chosen:updated');
            $("#FormW1_VerifiedDate").prop("readonly", false);
            $("#FormW1_VerifiedSign").prop("disabled", false);
        }
        else {
            $('#ddlVerifiedBy').prop("disabled", true);
            $('#ddlVerifiedBy').val(0);
            $('#ddlVerifiedBy').trigger('chosen:updated');
            $("#FormW1_VerifiedName").val('');
            $("#FormW1_VerifiedDesig").val('');
            $("#FormW1_VerifiedDate").val('');
            $("#FormW1_VerifiedDate").prop("readonly", true);
            $("#FormW1_VerifiedSign").prop("disabled", true);
        }
    });

    if (RecommondedInstrctedWorkValue != "None") {
        $('#ddlVerifiedBy').prop("disabled", false);
        $("#FormW1_VerifiedDate").prop("readonly", false);
        $('#ddlVerifiedBy').trigger('chosen:updated');
    }

    CalculateCost();

});

function CalculateCost() {

    var PhyWorks = 0, GenPrelims = 0, SurvyWorks = 0, SiteInvest = 0, ConsulFee = 0, OtherCost = 0;
    if ($("#FormW1_PhyWorks").val() != "") {
        PhyWorks = $("#FormW1_PhyWorks").val();
    }
    else {
        PhyWorks = 0;
    }
    if ($("#FormW1_GenPrelims").val() != "") {
        GenPrelims = $("#FormW1_GenPrelims").val();
    }
    else {
        GenPrelims = 0;
    }

    if ($("#FormW1_SurvyWorks").val() != "") {
        SurvyWorks = $("#FormW1_SurvyWorks").val();
    }
    else {
        SurvyWorks = 0;
    }
    if ($("#FormW1_SiteInvest").val() != "") {
        SiteInvest = $("#FormW1_SiteInvest").val();
    }
    else {
        SiteInvest = 0;
    }
    if ($("#FormW1_ConsulFee").val() != "") {
        ConsulFee = $("#FormW1_ConsulFee").val();
    }
    else {
        ConsulFee = 0;
    }

    if ($("#FormW1_OtherCost").val() != "") {
        OtherCost = $("#FormW1_OtherCost").val();
    }
    else {
        OtherCost = 0;
    }
    $("#FormW1_EstimTotalCost").val(parseInt(PhyWorks) + parseInt(GenPrelims) + parseInt(SurvyWorks) + parseInt(SiteInvest) + parseInt(ConsulFee) + parseInt(OtherCost));
}


function Save() {
    //if (submit) {
    //    $("#div-addformd .svalidate").addClass("validate");
    //}
    if (ValidatePage('#page')) {
        InitAjaxLoading();
        $.post('./SaveFormW1', $("form").serialize(), function (data) {
            HideAjaxLoading();
            if (data == -1) {
                app.ShowErrorMessage(data.errorMessage);

            }
            else {
                $("#FW2HRef_No").val(data.pkRefNo);
                app.ShowSuccessMessage('Successfully Saved', false);
                // location.href = "/InstructedWorks/AddFormW2";
            }
        });
    }
}

function OnRMUChange(tis) {

    var ctrl = $(tis);
    $('#FormW1_Rmu').val(ctrl.val());
    if (ctrl.val() != null && ctrl.val() != "") {
        $("#FormW1_Division").val(ctrl.find("option:selected").attr("Item1"));
    }
    else {
        $("#FormW1_Division").val('');
    }

}

function OnRoadChange(tis) {

    var ctrl = $(tis);
    $('#FormW1_RoadCode').val(ctrl.val());
    if (ctrl.val() != null && ctrl.val() != "") {
        $("#FormW1_RoadName").val(ctrl.find("option:selected").attr("Item1"));
    }
    else {
        $("#FormW1_RoadName").val('');
    }

}

function OnReportedByUserChange(tis) {


    var ctrl = $(tis);
    $('#FormW1_ReportedBy').val(ctrl.val());
    if (ctrl.val() != null && ctrl.val() != "") {
        $("#FormW1_ReportedName").val(ctrl.find("option:selected").attr("Item1"));
        $("#FormW1_ReportedDesig").val(ctrl.find("option:selected").attr("Item2"));
    }
    else {
        $("#FormW1_ReportedName").val('');
        $("#FormW1_ReportedDesig").val('');
    }

}


function OnRequestedByUserChange(tis) {

    var ctrl = $(tis);
    $('#FormW1_RequestedBy').val(ctrl.val());
    if (ctrl.val() != null && ctrl.val() != "") {
        $("#FormW1_RequestedByName").val(ctrl.find("option:selected").attr("Item1"));
        $("#FormW1_RequestedByDesig").val(ctrl.find("option:selected").attr("Item2"));
    }
    else {
        $("#FormW1_RequestedByName").val('');
        $("#FormW1_RequestedByDesig").val('');
    }

}

function OnVerifyUserChange(tis) {

    var ctrl = $(tis);
    $('#FormW1_VerifiedBy').val(ctrl.val());
    if (ctrl.val() != null && ctrl.val() != "") {
        $("#FormW1_VerifiedName").val(ctrl.find("option:selected").attr("Item1"));
        $("#FormW1_VerifiedDesig").val(ctrl.find("option:selected").attr("Item2"));
    }
    else {
        $("#FormW1_VerifiedName").val('');
        $("#FormW1_VerifiedDesig").val('');

    }
}


function GetImageList(id) {

    var group = $("#FormADetAssetGrpCode option:selected").val();
    var FormType = "FormW2"
    if (id && id > 0) {
        $("#FW2HRef_No").val(id);
    }
    else {
        id = $("#FW2HRef_No").val();
    }

    $.ajax({
        url: '/InstructedWorks/GetW1ImageList',
        data: { formW1Id: id, assetgroup: group },
        type: 'POST',
        success: function (data) {
            $("#ViewPhoto").html(data);
            $("#FW2HRef_No").val(id);
        },
        error: function (data) {
            alert(data.responseText);
        }

    });
}

