$(document).ready(function () {

    if ($("#hdnView").val() == "1") {
        $("#FormW1datapage *").prop("disabled", true);
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
        $("#ddlSectionCode").chosen('destroy');
        $("#ddlSectionCode").prop("disabled", true);
        $("#FormW1_ServPropName").chosen('destroy');
        $("#FormW1_ServPropName").prop("disabled", true);
        $("#btnSave").hide();
        $("#btnSubmit").hide();
        $("#addAttachment").hide();
        $("#btnBack").removeAttr("disabled");
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


    if ($("#FormW1_Status").val() == "Submitted" || $("#FormW1_Status").val() == "Verified") {

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


    if ($("#hdnRecommondedValue").val() != "0") {
        $('#ddlUseridRep').prop("disabled", false);
        $('#ddlUseridRep').trigger('chosen:updated');
        $("#FormW1_DtRep").prop("readonly", false);
        $("#FormW1_SignRep").prop("disabled", false);

    }
    else {
        $('#ddlUseridRep').prop("disabled", true);
        $('#ddlUseridRep').trigger('chosen:updated');
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
 
    nfObject = new Intl.NumberFormat('en-US');

    var PhyWorksAmt = 0, GenPrelimsAmt = 0, SurvyWorksAmt = 0, SiteInvestAmt = 0, ConsulFeeAmt = 0, OtherCostAmt = 0;
    if ($("#FormW1_PhyWorksAmt").val() != "") {

       
        PhyWorksAmt = $("#FormW1_PhyWorksAmt").val().replace(/,/g, '');

        if (!$.isNumeric(PhyWorksAmt)) {
            app.ShowErrorMessage("Invalid Physical Works");
            return;
        }

        $("#FormW1_PhyWorksAmt").val(nfObject.format(PhyWorksAmt));

       
        if (PhyWorksAmt > 999999999) {
            $("#FormW1_PhyWorksAmt").val("")
            app.ShowErrorMessage("Physical Works Invalid");
        }
    }
    else {
        PhyWorksAmt = 0;
    }
    if ($("#FormW1_GenPrelimsAmt").val() != "") {

        GenPrelimsAmt = $("#FormW1_GenPrelimsAmt").val().replace(/,/g, '');

        if (!$.isNumeric(GenPrelimsAmt)) {
            app.ShowErrorMessage("Invalid General and Preliminaries");
            return;
        }

        $("#FormW1_GenPrelimsAmt").val(nfObject.format(GenPrelimsAmt));

        if (GenPrelimsAmt > 999999999) {
            $("#FormW1_GenPrelimsAmt").val("")
            app.ShowErrorMessage("Invalid General and Preliminaries");
        }
    }
    else {
        GenPrelimsAmt = 0;
    }

    if ($("#FormW1_SurvyWorksAmt").val() != "") {
      
        SurvyWorksAmt = $("#FormW1_SurvyWorksAmt").val().replace(/,/g, '');

        if (!$.isNumeric(SurvyWorksAmt)) {
            app.ShowErrorMessage("Invalid Survey Works");
            return;
        }

        $("#FormW1_SurvyWorksAmt").val(nfObject.format(SurvyWorksAmt));


        if (SurvyWorksAmt > 999999999) {
            $("#FormW1_SurvyWorksAmt").val("")
            app.ShowErrorMessage("Invalid Survey Works ");
        }
    }
    else {
        SurvyWorksAmt = 0;
    }
    if ($("#FormW1_SiteInvestAmt").val() != "") {
       
        SiteInvestAmt = $("#FormW1_SiteInvestAmt").val().replace(/,/g, '');

        if (!$.isNumeric(SiteInvestAmt)) {
            app.ShowErrorMessage("Invalid Site Investigation");
            return;
        }

        $("#FormW1_SiteInvestAmt").val(nfObject.format(SiteInvestAmt));

        if (SiteInvestAmt > 999999999) {
            $("#FormW1_SiteInvestAmt").val("")
            app.ShowErrorMessage("Invalid Site Investigation");
        }
    }
    else {
        SiteInvestAmt = 0;
    }
    if ($("#FormW1_ConsulFeeAmt").val() != "") {
         
        ConsulFeeAmt = $("#FormW1_ConsulFeeAmt").val().replace(/,/g, '');

        if (!$.isNumeric(ConsulFeeAmt)) {
            app.ShowErrorMessage("Invalid Consultancy Fees");
            return;
        }

        $("#FormW1_ConsulFeeAmt").val(nfObject.format(ConsulFeeAmt));


        if (ConsulFeeAmt > 999999999) {
            $("#FormW1_ConsulFeeAmt").val("")
            app.ShowErrorMessage("Invalid Consultancy Fees");
        }
    }
    else {
        ConsulFeeAmt = 0;
    }

    if ($("#FormW1_OtherCostAmt").val() != "") {
        
        OtherCostAmt = $("#FormW1_OtherCostAmt").val().replace(/,/g, '');

        if (!$.isNumeric(OtherCostAmt)) {
            app.ShowErrorMessage("Invalid Other Cost");
            return;
        }

        $("#FormW1_OtherCostAmt").val(nfObject.format(OtherCostAmt));


        if (OtherCostAmt > 999999999) {
            $("#FormW1_OtherCostAmt").val("")
            app.ShowErrorMessage("Invalid Other Cost");
        }
    }
    else {
        OtherCostAmt = 0;
    }

    $("#FormW1_EstimTotalCostAmt").val(parseFloat(PhyWorksAmt) + parseFloat(GenPrelimsAmt) + parseFloat(SurvyWorksAmt) + parseFloat(SiteInvestAmt) + parseFloat(ConsulFeeAmt) + parseFloat(OtherCostAmt));

    $("#FormW1_EstimTotalCostAmt").val(nfObject.format($("#FormW1_EstimTotalCostAmt").val()));
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

    if (ctrl.find("option:selected").attr("fromkm") != undefined)
        $("#FormW1_Ch").val(ctrl.find("option:selected").attr("fromkm"));
    else
        $("#FormW1_Ch").val(ctrl.find("option:selected").attr("Item1"));

    if (ctrl.find("option:selected").attr("fromm") != undefined)
        $("#FormW1_ChDeci").val(ctrl.find("option:selected").attr("fromm"));
    else
        $("#FormW1_ChDeci").val(ctrl.find("option:selected").attr("Item2"));

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
                    $("#ddlRoadCode")
                        .append($("<option></option>")
                            .attr("value", value.value)
                            .attr("item1", value.item1)
                            .attr("item2", value.item2)
                            .text(value.text));
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
        if (ctrl.val() == "99999999") {
            $("#FormW1_UsernameRep").removeAttr("readonly");
            $("#FormW1_DesignationRep").removeAttr("readonly");
            $("#FormW1_OfficeRep").removeAttr("readonly");
        } else {
            $("#FormW1_UsernameRep").attr("readonly", "true");
            $("#FormW1_DesignationRep").attr("readonly", "true");
            $("#FormW1_OfficeRep").attr("readonly", "true");
        }

        $('#FormW1_SignRep').prop('checked', true);
    }
    else {
        $("#FormW1_UsernameRep").val('');
        $("#FormW1_DesignationRep").val('');
        $("#FormW1_OfficeRep").val('');
        $('#FormW1_SignRep').prop('checked', false);
    }

}


function OnUseridReqUserChange(tis) {

    var ctrl = $(tis);
    $('#FormW1_UseridReq').val(ctrl.val());
    if (ctrl.val() != null && ctrl.val() != "") {
        $("#FormW1_UsernameReq").val(ctrl.find("option:selected").attr("Item1"));
        $("#FormW1_DesignationReq").val(ctrl.find("option:selected").attr("Item2"));
        $("#FormW1_OfficeReq").val(ctrl.find("option:selected").attr("Item3"));
        if (ctrl.val() == "99999999") {
            $("#FormW1_UsernameReq").removeAttr("readonly");
            $("#FormW1_DesignationReq").removeAttr("readonly");
            $("#FormW1_OfficeReq").removeAttr("readonly");
        } else {
            $("#FormW1_UsernameReq").attr("readonly", "true");
            $("#FormW1_DesignationReq").attr("readonly", "true");
            $("#FormW1_OfficeReq").attr("readonly", "true");
        }
        $('#FormW1_SignReq').prop('checked', true);
    }
    else {
        $("#FormW1_UsernameReq").val('');
        $("#FormW1_DesignationReq").val('');
        $("#FormW1_OfficeReq").val('');
        $('#FormW1_SignReq').prop('checked', false);
    }



}

function OnVerifyUserChange(tis) {

    var ctrl = $(tis);
    $('#FormW1_UseridVer').val(ctrl.val());
    if (ctrl.val() != null && ctrl.val() != "") {
        $("#FormW1_UsernameVer").val(ctrl.find("option:selected").attr("Item1"));
        $("#FormW1_DesignationVer").val(ctrl.find("option:selected").attr("Item2"));
        $("#FormW1_OfficeVer").val(ctrl.find("option:selected").attr("Item3"));
        if (ctrl.val() == "99999999") {
            $("#FormW1_UsernameVer").removeAttr("readonly");
            $("#FormW1_DesignationVer").removeAttr("readonly");
            $("#FormW1_OfficeVer").removeAttr("readonly");
        } else {
            $("#FormW1_UsernameVer").attr("readonly", "true");
            $("#FormW1_DesignationVer").attr("readonly", "true");
            $("#FormW1_OfficeVer").attr("readonly", "true");
        }
        $('#FormW1_SignVer').prop('checked', true);
    }
    else {
        $("#FormW1_UsernameVer").val('');
        $("#FormW1_DesignationVer").val('');
        $("#FormW1_OfficeVer").val('');
        $('#FormW1_SignVer').prop('checked', false);

    }
}


function GetImageList(id, formName) {

    var group = $("#FormADetAssetGrpCode option:selected").val();

    $.ajax({
        url: '/InstructedWorks/GetIWImageList',
        data: { Id: id, assetgroup: group, Form: formName },
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

function RecommondedValue(e) {

    $("#hdnRecommondedValue").val(e.value);

}


function GoBack() {

    if ($("#hdnView").val() == "0" || $("#hdnView").val() == "" || $("#FormW1_Status").val() == "" || $("#FormW1_Status").val() == "Saved") {
        if (app.Confirm("Are you sure you want to close the form?", function (e) {
            if (e) {
                location.href = "/InstructedWorks/Index";
            }
        }));
    }
    else
        location.href = "/InstructedWorks/Index";
}


function Save(GroupName, SubmitType) {


    $("#ddlUseridReq").removeClass("validate");
    $("#ddlUseridVer").removeClass("validate");
    $("#ddlUseridRep").removeClass("validate");
    $("#ddlRMU").removeClass("validate");
    $("#FormW1_IwRefNo").removeClass("validate");
    $("#chkBQ").removeClass("validate");
    $("#chkDrawing").removeClass("validate");
    $("#FormW1_SignReq").removeClass("validate");
    $("#FormW1_SignVer").removeClass("validate");
    $("#FormW1_SignRep").removeClass("validate");

    if (SubmitType != "") {

        $("#FormW1page .svalidate").addClass("validate");

        if (SubmitType == "Submitted") {
            $("#FormW1_Status").val("Submitted");
            $("#FormW1_SubmitSts").val(true);
            $("#ddlUseridReq").addClass("validate");
            $("#FormW1_SignReq").addClass("validate");
            

            if ($("#chkBQ").prop('checked') == false) {
                $("#chkBQ").addClass("validate");
            }
            else {
                if ($("#chkBQ").prop('checked') == true) {
                    $("#FormW1_IsBq").val(true);
                }
                else {
                    $("#FormW1_IsBq").val(false);
                }
            }

            if ($("#chkDrawing").prop('checked') == false) {
                $("#chkDrawing").addClass("validate");
            }
            else {

                if ($("#chkDrawing").prop('checked') == true) {
                    $("#FormW1_IsDrawing").val(true);
                }
                else {
                    $("#FormW1_IsDrawing").val(false);
                }
            }
        }
        else if (SubmitType == "Verified") {
            $("#ddlUseridReq").addClass("validate");
            $("#ddlUseridVer").addClass("validate");
            $("#ddlRMU").addClass("validate");
            $("#ddlRMU").addClass("validate");
            $("#FormW1_IwRefNo").addClass("validate");
            $("#FormW1_SignVer").removeClass("validate");

            if ($("#hdnRecommondedValue").val() == 1 || $("#hdnRecommondedValue").val() == 2) {
                $("#ddlUseridRep").addClass("validate");
                $("#FormW1_SignRep").removeClass("validate");
            }
        }
    }
    else {
        $("#FormW1_Status").val("Saved");
    }

    if (ValidatePage('#FormW1page')) {
        InitAjaxLoading();
        $.post('/InstructedWorks/SaveFormW1', $("form").serialize(), function (data) {
            HideAjaxLoading();
            if (data == -1) {
                app.ShowErrorMessage(data.errorMessage);
            }
            else {

                $("#FormW1_PkRefNo").val(data);
                $("#hdnPkRefNo").val(data);

                if (SubmitType == "" || SubmitType == "Saved") {
                    app.ShowSuccessMessage('Saved Successfully', false);
                    location.href = "/InstructedWorks/Index";
                }
                else if (SubmitType == "Submitted") {
                    app.ShowSuccessMessage('Submitted Successfully', false);
                    location.href = "/InstructedWorks/Index";
                }
                else if (SubmitType == "Verified") {
                    process.ShowApprove(GroupName, SubmitType);
                }
            }
        });
    }



}

function DurationChange(ctrl, message) {
    if (parseFloat($(ctrl).val()) > 99) {
        app.ShowErrorMessage(message);
        $(ctrl).val('');
    }
}

function PercentChange(ctrl, message) {

    if (parseFloat($(ctrl).val()) > 100) {
        app.ShowErrorMessage(message);
        $(ctrl).val('');
    }
}

