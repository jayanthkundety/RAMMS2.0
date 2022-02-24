$(document).ready(function () {

    if ($("#hdnView").val() == "1") {
        $("#FormW1page *").prop("disabled", true);
        $("#ddlRMU").chosen('destroy');
        $("#ddlRMU").prop("disabled", true);
        $("#ddlRoadCode").chosen('destroy');
        $("#ddlRoadCode").prop("disabled", true);
        $("#ddlUseridRep").chosen('destroy');
        $("#ddlUseridRep").prop("disabled", true);
        $("#ddlUseridVer").chosen('destroy');
        $("#ddlUseridVer").prop("disabled", true);
        $("#ddlUseridRep").chosen('destroy');
        $("#ddlUseridRep").prop("disabled", true);
        $("#ddlUseridReq").chosen('destroy');
        $("#ddlUseridReq").prop("disabled", true);
        $("#FormW1_Status").chosen('destroy');
        $("#FormW1_Status").prop("disabled", true);
        $("#btnSave").hide();
        $("#btnSubmit").hide();

    }

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

    //$("#ddlSectionCode").val($('#FormW1_SecCode').val());
    //$('#ddlSectionCode').trigger("chosen:updated")
    //$("#ddlSectionCode").trigger("change");


    if ($("#FormW1_Status").val() == "Submitted") {

        $(".approvesection").css("display", "flex");
    }
    else {

        $(".approvesection").css("display", "none");
    }


    $('input[type=radio][id=FormW1_RecomdType]').change(function () {

        if (this.value == '1' || this.value == '2') {
            $('#ddlUseridRep').prop("disabled", false);
            $('#ddlUseridRep').trigger('chosen:updated');
            $("#FormW1_DtRep").prop("readonly", false);
            $("#FormW1_SignRep").prop("disabled", false);
        }
        else {
            $('#ddlUseridRep').prop("disabled", true);
            $('#ddlUseridRep').val(0);
            $('#ddlUseridRep').trigger('chosen:updated');
            $("#FormW1_UsernameRep").val('');
            $("#FormW1_DesignationRep").val('');
            $("#FormW1_DtRep").val('');
            $("#FormW1_DtRep").prop("readonly", true);
            $("#FormW1_SignRep").prop("disabled", true);
        }
    });

    if (RecommondedInstrctedWorkValue != "0") {
        $('#ddlUseridRep').prop("disabled", false);
        $("#FormW1_DtRep").prop("readonly", false);
        $("#FormW1_SignRep").prop("disabled", false);
        $('#ddlUseridRep').trigger('chosen:updated');
    }
    else {
        $('#ddlUseridRep').prop("disabled", true);
        $("#FormW1_DtRep").prop("readonly", true);
        $("#FormW1_SignRep").prop("disabled", true);
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
    debugger

    $("#FormW1_EstimTotalCostAmt").val(parseFloat(PhyWorksAmt) + parseFloat(GenPrelimsAmt) + parseFloat(SurvyWorksAmt) + parseFloat(SiteInvestAmt) + parseFloat(ConsulFeeAmt) + parseFloat(OtherCostAmt));
}


function OnRMUChange(tis) {

    var ctrl = $(tis);
    $('#FormW1_RmuCode').val(ctrl.val());
    if (ctrl.val() != null && ctrl.val() != "") {
        $("#FormW1_DivnCode").val(ctrl.find("option:selected").attr("Item1"));

        // to get value for Section code ddl
        var obj = new Object();
        obj.RMU = ctrl.val();
        searchList(obj);

        // to get value for RoadCode

        //$.ajax({
        //    url: '/InstructedWorks/GetRoadCodeByRMU',
        //    dataType: 'JSON',
        //    data: { rmu: ctrl.val() },
        //    type: 'Post',
        //    success: function (data) {
        //        if (data != null) {

        //            $('#ddlRoadCode').empty();
        //            $('#ddlRoadCode')
        //                .append($("<option></option>")
        //                    .attr("value", "")
        //                    .text("Select Road Code"));
        //            $.each(data, function (key, value) {
        //                $('#ddlRoadCode')
        //                    .append($("<option></option>")
        //                        .attr("value", value.value)
        //                        .attr("Item1", value.item1)
        //                        .text(value.text));
        //            });
        //            $("#ddlRoadCode").val($('#FormW1_RoadCode').val());
        //            $('#ddlRoadCode').trigger("chosen:updated")
        //            $("#ddlRoadCode").trigger("change");
        //        }
        //    },
        //    error: function (data) {

        //        console.error(data);
        //    }
        //});

    }
    else {
        $("#FormW1_DivnCode").val('');
    }

}

function OnSectionChange(tis) {

    var ctrl = $("#ddlSectionCode");
    $('#FormW1_SecCode').val(ctrl.val());
    if (ctrl.val() != null && ctrl.val() != "") {

        // to get value for road code ddl
        var obj = new Object();
        obj.RMU = $("#ddlRMU").val();
        obj.SectionCode = ctrl.val();
        searchList(obj);

        //to get section name
        var TypeCode;
        var arrsec = $("#ddlSectionCode").find(":selected").text().split('-');
        if (arrsec.length > 0) {
            TypeCode = arrsec[0];
        }
        else {
            TypeCode = 0;
        }
        GetNames(TypeCode, "Section Code");
    }
    else {
        $("#FormW1_SectionCode").val('');
    }

}

function GetNames(TypeCode, Type) {
    var obj = new Object();
    obj.TypeCode = TypeCode;
    obj.Type = Type;
    getNameByCode(obj);
}

function OnRoadChange(tis) {

    //var ctrl = $(tis);
    //$('#FormW1_RoadCode').val(ctrl.val());
    //if (ctrl.val() != null && ctrl.val() != "") {
    //    $("#FormW1_RoadName").val(ctrl.find("option:selected").attr("Item1"));
    //}
    //else {
    //    $("#FormW1_RoadName").val('');
    //}

    var ctrl = $("#ddlRoadCode");
    $('#FormW1_RoadCode').val(ctrl.val());

    var obj = new Object();
    obj.TypeCode = ctrl.val();
    obj.Type = "RD_Code";
    getNameByCode(obj)

}



function searchList(obj) {
    $.ajax({
        url: '/InstructedWorks/detailSearchDdList',
        data: obj,
        type: 'Post',
        success: function (data) {

            if (obj.RdCode == "" || obj.RdCode == null || obj.RdCode == 0) {

                $('#ddlRoadCode').empty();
                $("#ddlRoadCode").append($('<option>').val(null).text("Select Road Code"));

                $.each(data.rdCode, function (index, value) {
                    $("#ddlRoadCode").append($('<option>').val(value.value).html(value.text));
                    $("#ddlRoadCode").trigger("chosen:updated");

                })
                $("#ddlRoadCode").val($('#FormW1_RoadCode').val());
                $('#ddlRoadCode').trigger("chosen:updated")
                $("#ddlRoadCode").trigger("change");
                $("#FormW1_RoadName").val(null);
            }


            if (obj.SectionCode == "" || obj.SectionCode == null || obj.SectionCode == 0) {

                $('#ddlSectionCode option').empty();
                $('#ddlSectionCode').append($('<option>').val(null).text('Select Section Code'))

                $.each(data.section, function (index, value) {
                    $('#ddlSectionCode').append($('<option>').val(value.value).text(value.text))
                    $('#ddlSectionCode').trigger("chosen:updated");
                })
                $("#ddlSectionCode").val($('#FormW1_SecCode').val());
                $('#ddlSectionCode').trigger("chosen:updated")
                $("#ddlSectionCode").trigger("change");
                $("#SecName").val(null)
            }

        }


    });
}

function getNameByCode(obj) {
    $.ajax({
        url: '/InstructedWorks/GetNameByCode',
        data: obj,
        type: 'Post',
        success: function (data) {
            if (obj.Type == "Section Code") {
                $("#SecName").val(data);
            }
            else if (obj.Type == "RD_Code") {
                $("#FormW1_RoadName").val(data);
            }
        }
    })
}



function OnUseridRepUserChange(tis) {


    var ctrl = $(tis);
    $('#FormW1_UseridRep').val(ctrl.val());
    if (ctrl.val() != null && ctrl.val() != "") {
        $("#FormW1_UsernameRep").val(ctrl.find("option:selected").attr("Item1"));
        $("#FormW1_DesignationRep").val(ctrl.find("option:selected").attr("Item2"));
        $("#FormW1_OfficeRep").val(ctrl.find("option:selected").attr("Item3"));
    }
    else {
        $("#FormW1_UsernameRep").val('');
        $("#FormW1_DesignationRep").val('');
        $("#FormW1_OfficeRep").val('');
    }

}


function OnUseridReqUserChange(tis) {

    var ctrl = $(tis);
    $('#FormW1_UseridReq').val(ctrl.val());
    if (ctrl.val() != null && ctrl.val() != "") {
        $("#FormW1_UsernameReq").val(ctrl.find("option:selected").attr("Item1"));
        $("#FormW1_DesignationReq").val(ctrl.find("option:selected").attr("Item2"));
        $("#FormW1_OfficeReq").val(ctrl.find("option:selected").attr("Item3"));
    }
    else {
        $("#FormW1_UsernameReq").val('');
        $("#FormW1_DesignationReq").val('');
        $("#FormW1_OfficeReq").val('');
    }

}

function OnVerifyUserChange(tis) {

    var ctrl = $(tis);
    $('#FormW1_UseridVer').val(ctrl.val());
    if (ctrl.val() != null && ctrl.val() != "") {
        $("#FormW1_UsernameVer").val(ctrl.find("option:selected").attr("Item1"));
        $("#FormW1_DesignationVer").val(ctrl.find("option:selected").attr("Item2"));
        $("#FormW1_OfficeVer").val(ctrl.find("option:selected").attr("Item3"));
    }
    else {
        $("#FormW1_UsernameVer").val('');
        $("#FormW1_DesignationVer").val('');
        $("#FormW1_OfficeVer").val('');

    }
}


function GetImageList(id) {

    var group = $("#FormADetAssetGrpCode option:selected").val();

    $.ajax({
        url: '/InstructedWorks/GetIWImageList',
        data: { Id: id, assetgroup: group },
        type: 'POST',
        success: function (data) {
            $("#ViewPhoto").html(data);
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


function Save(GroupName,SubmitType) {
    if (SubmitType !="") {

        $("#FormW1page .svalidate").addClass("validate");


        if (($("#FormW1_Status").val() == "") || ($("#FormW1_Status").val() == "0") || ($("#FormW1_Status").val() == "1") || ($("#FormW1_Status").val() == "2")) {
            $("#ddlUseridReq").addClass("validate");
        }
        else {
            $("#ddlUseridReq").removeClass("validate");
        }

        if (($("#FormW1_Status").val() == "1") || ($("#FormW1_Status").val() == "2")) {
            $("#ddlUseridRep").addClass("validate");
        }
        else {
            $("#ddlUseridRep").removeClass("validate");
        }

        if (($("#FormW1_Status").val() == "2")) {
            $("#ddlUseridVer").addClass("validate");
        }
        else {
            $("#ddlUseridVer").removeClass("validate");
        }

    }
    else {
        $("#ddlUseridReq").removeClass("validate");
    }

    if (ValidatePage('#FormW1page')) {
        InitAjaxLoading();
        $.post('/InstructedWorks/SaveFormW1', $("form").serialize(), function (data) {
            HideAjaxLoading();
            if (data == -1) {
                app.ShowErrorMessage(data.errorMessage);

            }
            else {
                $("#FormW1_pkRefNo").val(data.pkRefNo);
                app.ShowSuccessMessage('Successfully Saved', false);
            }
        });
    }
    else {
        process.ShowApprove(GroupName, SubmitType);
    }
}






