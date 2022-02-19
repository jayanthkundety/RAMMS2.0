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




    $('input[type=radio][id=FormW1_RecomdType]').change(function () {

        if (this.value == '1' || this.value == '2') {
            $('#ddlUseridVer').prop("disabled", false);
            $('#ddlUseridVer').trigger('chosen:updated');
            $("#FormW1_DtVer").prop("readonly", false);
            $("#FormW1_SignVer").prop("disabled", false);
        }
        else {
            $('#ddlUseridVer').prop("disabled", true);
            $('#ddlUseridVer').val(0);
            $('#ddlUseridVer').trigger('chosen:updated');
            $("#FormW1_UsernameVer").val('');
            $("#FormW1_DesignationVer").val('');
            $("#FormW1_DtVer").val('');
            $("#FormW1_DtVer").prop("readonly", true);
            $("#FormW1_SignVer").prop("disabled", true);
        }
    });

    if (RecommondedInstrctedWorkValue != "None") {
        $('#ddlUseridVer').prop("disabled", false);
        $("#FormW1_DtVer").prop("readonly", false);
        $('#ddlUseridVer').trigger('chosen:updated');
    }
    else {
        $('#ddlUseridVer').prop("disabled", true);
    }

    CalculateCost();

    $("#FormW1_Dt").change(function () {
        var value = $(this).val();
        var commDate = new Date(formatDate(value));
        var minDate = new Date(2020, 0, 1);
        if (commDate < minDate) {
            app.ShowErrorMessage("Commencement date should not be less than 01-01-2020");
            $(this).val('');
            return;
        }

    });

});

function CalculateCost() {

    var PhyWorksAmtAmt = 0, GenPrelimsAmt = 0, SurvyWorksAmt = 0, SiteInvestAmt = 0, ConsulFeeAmt = 0, OtherCostAmt = 0;
    if ($("#FormW1_PhyWorksAmt").val() != "") {
        PhyWorksAmt = $("#FormW1_PhyWorksAmt").val();
    }
    else {
        PhyWorksAmt = 0;
    }
    if ($("#FormW1_GenPrelimsAmt").val() != "") {
        GenPrelimsAmt = $("#FormW1_GenPrelimsAmt").val();
    }
    else {
        GenPrelimsAmt = 0;
    }

    if ($("#FormW1_SurvyWorksAmt").val() != "") {
        SurvyWorksAmt = $("#FormW1_SurvyWorksAmt").val();
    }
    else {
        SurvyWorksAmt = 0;
    }
    if ($("#FormW1_SiteInvestAmt").val() != "") {
        SiteInvestAmt = $("#FormW1_SiteInvestAmt").val();
    }
    else {
        SiteInvestAmt = 0;
    }
    if ($("#FormW1_ConsulFeeAmt").val() != "") {
        ConsulFeeAmt = $("#FormW1_ConsulFeeAmt").val();
    }
    else {
        ConsulFeeAmt = 0;
    }

    if ($("#FormW1_OtherCostAmt").val() != "") {
        OtherCostAmt = $("#FormW1_OtherCostAmt").val();
    }
    else {
        OtherCostAmt = 0;
    }
    $("#FormW1_EstimTotalCostAmt").val(parseInt(PhyWorksAmt) + parseInt(GenPrelimsAmt) + parseInt(SurvyWorksAmt) + parseInt(SiteInvestAmt) + parseInt(ConsulFeeAmt) + parseInt(OtherCostAmt));
}


function Save() {
    //if (submit) {
    //    $("#div-addformd .svalidate").addClass("validate");
    //}
    
    if (ValidatePage('#FormW1page')) {
        InitAjaxLoading();
        $.post('/InstructedWorks/SaveFormW1', $("form").serialize(), function (data) {
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
    $('#FormW1_RmuCode').val(ctrl.val());
    if (ctrl.val() != null && ctrl.val() != "") {
        $("#FormW1_DivnCode").val(ctrl.find("option:selected").attr("Item1"));

        // to get value for RoadCode

            $.ajax({
                url: '/InstructedWorks/GetRoadCodeByRMU',
                dataType: 'JSON',
                data: { rmu: ctrl.val() },
                type: 'Post',
                success: function (data) {
                    if (data != null) {
                         debugger
                        $('#ddlRoadCode').empty();
                        $('#ddlRoadCode')
                            .append($("<option></option>")
                                .attr("value", "")
                                .text("Select Road Code"));
                        $.each(data, function (key, value) {
                            $('#ddlRoadCode')
                                .append($("<option></option>")
                                    .attr("value", value.value)
                                    .attr("Item1", value.item1)
                                    .text(value.text));
                        });
                        $("#ddlRoadCode").val($('#FormW1_RoadCode').val());
                        $('#ddlRoadCode').trigger("chosen:updated")
                        $("#ddlRoadCode").trigger("change");
                    }
                },
                error: function (data) {

                    console.error(data);
                }
            });
       
    }
    else {
        $("#FormW1_DivnCode").val('');
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

function OnUseridRepUserChange(tis) {


    var ctrl = $(tis);
    $('#FormW1_UseridRep').val(ctrl.val());
    if (ctrl.val() != null && ctrl.val() != "") {
        $("#FormW1_UsernameRep").val(ctrl.find("option:selected").attr("Item1"));
        $("#FormW1_DesignationRep").val(ctrl.find("option:selected").attr("Item2"));
    }
    else {
        $("#FormW1_UsernameRep").val('');
        $("#FormW1_DesignationRep").val('');
    }

}


function OnUseridReqUserChange(tis) {

    var ctrl = $(tis);
    $('#FormW1_UseridReq').val(ctrl.val());
    if (ctrl.val() != null && ctrl.val() != "") {
        $("#FormW1_UsernameReq").val(ctrl.find("option:selected").attr("Item1"));
        $("#FormW1_DesignationReq").val(ctrl.find("option:selected").attr("Item2"));
    }
    else {
        $("#FormW1_UsernameReq").val('');
        $("#FormW1_DesignationReq").val('');
    }

}

function OnVerifyUserChange(tis) {

    var ctrl = $(tis);
    $('#FormW1_UseridVer').val(ctrl.val());
    if (ctrl.val() != null && ctrl.val() != "") {
        $("#FormW1_UsernameVer").val(ctrl.find("option:selected").attr("Item1"));
        $("#FormW1_DesignationVer").val(ctrl.find("option:selected").attr("Item2"));
    }
    else {
        $("#FormW1_UsernameVer").val('');
        $("#FormW1_DesignationVer").val('');

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

function formatDate(date) {
    var d = new Date(date),
        month = '' + (d.getMonth() + 1),
        day = '' + d.getDate(),
        year = d.getFullYear();

    if (month.length < 2) month = '0' + month;
    if (day.length < 2) day = '0' + day;

    return [year, month, day].join('-');
}

 


