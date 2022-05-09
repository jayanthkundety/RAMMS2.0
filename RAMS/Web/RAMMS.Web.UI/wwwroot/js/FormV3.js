

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
 

    $('#AccordPage1').on('shown.bs.collapse', function () {
     
        $.each($.fn.dataTable.tables(true), function () {
            $(this).DataTable().columns.adjust().draw();
        });
    });

    HeaderLogic();

    if ($("#FormV3_Status").val() == "") {

        $("#saveFormV3Btn").hide();
        $("#SubmitFormV3Btn").hide();
    }
    else if ($("#FormV3_Status").val() == "Initialize" || $("#FormV3_Status").val() == "Saved") {

        $("#saveFormV3Btn").show();
        $("#SubmitFormV3Btn").show();
    } else {

        $("#saveFormV3Btn").hide();
        $("#SubmitFormV3Btn").hide();
    }

    if ($("#hdnView").val() == "1") {

        $('#FormV3_Remarks').attr("readonly", "true");
        $("#saveFormV3Btn").hide();
        $("#SubmitFormV3Btn").hide();
        $('#FormV3_SignFac').prop('disabled', true);
    }


    $('#ddlFacilitatedby').prop('disabled', true).trigger("chosen:updated");
    $('#ddlAgreedby').prop('disabled', true).trigger("chosen:updated");
    $('#ddlRecordedby').prop('disabled', true).trigger("chosen:updated");
    $('#FormV3_DtAgr').attr("readonly", "true");
    $('#FormV3_DtRec').attr("readonly", "true");
    $('#FormV3_DtFac').attr("readonly", "true");
    $('#FormV3_SignRec').prop('disabled', true);
    $('#FormV3_SignAgr').prop('disabled', true);
   



    $("#ddlActCode").on("change", function () {

        var val = $(this).find(":selected").text();
        val = val.split("-").length > 0 ? val.split("-")[1] : val;
        $("#FormV3_Actname").val(val);
    });





    $("#ddlrmu").on("change", function () {
        var val = $(this).find(":selected").text();
        val = val.split("-").length > 0 ? val.split("-")[1] : val;
        $("#ddlrmuDesc").val(val);
        var req = {};
        req.Section = '';
        req.RoadCode = '';
        req.RMU = $("#ddlrmu option:selected").text().split("-")[1];

        ;
        //RM_div_RMU_Sec_Master
        $.ajax({
            url: '/ERT/RMUSecRoad',
            dataType: 'JSON',
            data: req,
            type: 'Post',
            success: function (data) {
                if (data != null) {
                    $("#ddlSecCode").empty();
                    $("#ddlSecCode").append($("<option></option>").val("").html("Select Section Code"));
                    $.each(data.section, function (index, v) {
                        $("#ddlSecCode").append($("<option></option>").val(v.value).html(v.text));
                    });

                    if (onloadFlag) {
                        $("#ddlSecCode").val($("#hdnSecCode").val());
                        onloadFlag = false;
                    }

                    $('#ddlSecCode').trigger("chosen:updated");
                    $("#ddlSecCode").trigger("change");

                }
            },
            error: function (data) {

                console.error(data);
            }
        });


    });
    $("#ddlrmu").trigger('change');



    $("#ddlSecCode").on("change", function () {
        //var d = new Date();
        var ddldata = $(this).val();

        if (ddldata != "") {
            $.ajax({
                url: '/ERT/GetAllRoadCodeDataBySectionCode',
                dataType: 'JSON',
                data: { secCode: $("#ddlSecCode option:selected").text().split("-")[0] },
                type: 'Post',
                success: function (data) {
                    if (data != null) {
                        if (data._RMAllData != undefined && data._RMAllData != null) {
                            $("#FormV3_Secname").val(data._RMAllData.secName);

                            $("#FormV3_DivCode").val(data._RMAllData.divisionCode);

                        }

                    }
                },
                error: function (data) {

                    console.error(data);
                }
            });
        }
        else {
            $("#FormV3_Secname").val("");
            $("#FormV3_DivCode").val("");
        }

        return false;
    });
    $("#ddlSecCode").trigger('change');

    $("#ddlCrew").on("change", function () {
        var id = $("#ddlCrew option:selected").val();
        if (id != "99999999" && id != "") {
            $.ajax({
                url: '/ERT/GetUserById',
                dataType: 'JSON',
                data: { id },
                type: 'Post',
                success: function (data) {
                    $("#FormV3_Crewname").val(data.userName);

                },
                error: function (data) {
                    console.error(data);
                }
            });
        }
        else if (id == "99999999") {

            $("#FormV3_Crewname").val('');
        }
        else {

            $("#FormV3_Crewname").val('');
        }

        return false;
    });
    $("#ddlCrew").trigger('change');

});




function OnRoadChange(tis) {

    var ctrl = $("#ddlRoadCode");
    $('#FormV3Dtl_RoadCode').val(ctrl.val());

    if (ctrl.find("option:selected").attr("fromkm") != undefined)
        $("#FormV3Dtl_FrmCh").val(ctrl.find("option:selected").attr("fromkm"));
    else
        $("#FormV3Dtl_FrmCh").val(ctrl.find("option:selected").attr("Item1"));

    if (ctrl.find("option:selected").attr("fromm") != undefined)
        $("#FormV3Dtl_ToChDeci").val(ctrl.find("option:selected").attr("fromm"));
    else
        $("#FormV3Dtl_ToChDeci").val(ctrl.find("option:selected").attr("Item2"));

    var obj = new Object();
    obj.TypeCode = ctrl.val();
    obj.Type = "RD_Code";
    getNameByCode(obj)

}

function getNameByCode(obj) {
    $.ajax({
        url: '/InstructedWorks/GetNameByCode',
        data: obj,
        type: 'Post',
        success: function (data) {

            if (obj.Type == "RD_Code") {
                $("#FormV3Dtl_RoadName").val(data);
            }
        }
    })
}


function OnRecordedChange(tis) {

    var ctrl = $(tis);
    if (ctrl.val() != null)
        $('#FormV3_UseridRec').val(ctrl.val());
    if ($('#FormV3_UseridRec').val() != "") {
        $("#FormV3_UsernameRec").val(ctrl.find("option:selected").attr("Item1"));
        $("#FormV3_DesignationRec").val(ctrl.find("option:selected").attr("Item2"));

        if ($('#FormV3_UseridRec').val() == "99999999") {
            $("#FormV3_UsernameRec").removeAttr("readonly");
            $("#FormV3_DesignationRec").removeAttr("readonly");

        } else {
            $("#FormV3_UsernameRec").attr("readonly", "true");
            $("#FormV3_DesignationRec").attr("readonly", "true");
        }
        $('#FormV3_SignRec').prop('checked', true);
    }
    else {
        $("#FormV3_UsernameRec").val('');
        $("#FormV3_DesignationRec").val('');
        $('#FormV3_SignRec').prop('checked', false);
    }
}

function OnAgreedbyChange(tis) {

    var ctrl = $(tis);
    if (ctrl.val() != null)
        $('#FormV3_UseridAgr').val(ctrl.val());
    if ($('#FormV3_UseridAgr').val() != "") {
        $("#FormV3_UsernameAgr").val(ctrl.find("option:selected").attr("Item1"));
        $("#FormV3_DesignationAgr").val(ctrl.find("option:selected").attr("Item2"));

        if ($('#FormV3_UseridAgr').val() == "99999999") {
            $("#FormV3_UsernameAgr").removeAttr("readonly");
            $("#FormV3_DesignationAgr").removeAttr("readonly");

        } else {
            $("#FormV3_UsernameAgr").attr("readonly", "true");
            $("#FormV3_DesignationAgr").attr("readonly", "true");
        }
        $('#FormV3_SignAgr').prop('checked', true);
    }
    else {
        $("#FormV3_UsernameAgr").val('');
        $("#FormV3_DesignationAgr").val('');
        $('#FormV3_SignAgr').prop('checked', false);
    }
}

function OnFacilitatedbyChange(tis) {

    var ctrl = $(tis);
    if (ctrl.val() != null)
        $('#FormV3_UseridFac').val(ctrl.val());
    if ($('#FormV3_UseridFac').val() != "") {
        $("#FormV3_UsernameFac").val(ctrl.find("option:selected").attr("Item1"));
        $("#FormV3_DesignationFac").val(ctrl.find("option:selected").attr("Item2"));

        if ($('#FormV3_UseridFac').val() == "99999999") {
            $("#FormV3_UsernameFac").removeAttr("readonly");
            $("#FormV3_DesignationFac").removeAttr("readonly");

        } else {
            $("#FormV3_UsernameFac").attr("readonly", "true");
            $("#FormV3_DesignationFac").attr("readonly", "true");
        }
        $('#FormV3_SignFac').prop('checked', true);
    }
    else {
        $("#FormV3_UsernameFac").val('');
        $("#FormV3_DesignationFac").val('');
        $('#FormV3_SignFac').prop('checked', false);
    }
}



function Save(SubmitType) {


    if (SubmitType == "Submitted") {
        $("#FormV3_SubmitSts").val(true);
    }

    if (ValidatePage('#AccordPage0')) {

        if ($("#FormV3_Status").val() == "")
            $("#FormV3_Status").val("Initialize");
        else if ($("#FormV3_Status").val() == "Initialize")
            $("#FormV3_Status").val("Saved");

        InitAjaxLoading();
        EnableDisableElements(false);
        $.get('/MAM/SaveFormV3', $("form").serialize(), function (data) {
            EnableDisableElements(true)
            HideAjaxLoading();
            if (data == -1) {
                app.ShowErrorMessage(data.errorMessage);
            }
            else {
                 
                if (SubmitType == "") {
                    if (data.result == "Success") {

                        if (data.formExist) {
                            location.href = "/MAM/EditFormV3?Id=" + data.pkRefNo + "&view=0";
                            return;
                        }

                        $("#FormV3_PkRefNo").val(data.pkRefNo);
                        $("#FormV3_FV1PKRefId").val(data.fV1PKRefId)
                        $("#FormV3_RefId").val(data.refId);
                        $("#FormV3_Status").val(data.status)
                        $("#saveFormV3Btn").show();
                        $("#SubmitFormV3Btn").show();
                        HeaderLogic();
                        InitializeGrid();
                       // app.ShowSuccessMessage('Saved Successfully', false);
                    }
                    else {
                        EnableDisableElements(false);
                        app.ShowErrorMessage(data.msg, false);
                    }

                }
                else if (SubmitType == "Saved") {
                    app.ShowSuccessMessage('Saved Successfully', false);

                    location.href = "/MAM/FormV3";
                }
                else if (SubmitType == "Submitted") {
                    app.ShowSuccessMessage('Submitted Successfully', false);
                    location.href = "/MAM/FormV3";
                }

            }
        });
    }

}



function SaveFormV3Dtl() {

    if (ValidatePage('#WorkAccompModal')) {
        InitAjaxLoading();
        $.post('/MAM/SaveFormV3Dtl', $("form").serialize(), function (data) {
            HideAjaxLoading();
            if (data == -1) {
                app.ShowErrorMessage(data.errorMessage);
            }
            else {
                ClearFormV3Dtl();
                $('#WorkAccomplishmentGridView').DataTable().ajax.reload();
               // InitializeGrid();
                app.ShowSuccessMessage('Saved Successfully', false);
            }
        });
    }

}

function EditFormV3Dtl(obj, view) {

    var currentRow = $(obj).closest("tr");
    var data = $('#WorkAccomplishmentGridView').DataTable().row(currentRow).data();

    $("#FormV3Dtl_PkRefNo").val(data.pkRefNo);
    $("#FormV3Dtl_Fv1dPkRefNo").val(data.fv1dPkRefNo);
    $("#ddlRoadCode").val(data.roadCode);
    $("#ddlRoadCode").trigger('chosen:updated');
    $("#ddlRoadCode").trigger('change');
    $("#FormV3Dtl_FrmCh").val(data.frmCh);
    $("#FormV3Dtl_FrmChDeci").val(data.frmChDeci);
    $("#FormV3Dtl_ToCh").val(data.toCh);
    $("#FormV3Dtl_ToChDeci").val(data.toChDeci);
    $("#FormV3Dtl_Length").val(data.length);
    $("#FormV3Dtl_Width").val(data.width);
    $("#FormV3Dtl_Adp").val(data.adp);
    $("#FormV3Dtl_TimetakenFrm").val(data.timetakenFrm);
    $("#FormV3Dtl_TimeTakenTo").val(data.timeTakenTo);
    $("#FormV3Dtl_TimeTakenTotal").val(data.timeTakenTotal);
    $("#FormV3Dtl_TransitTimeFrm").val(data.transitTimeFrm);
    $("#FormV3Dtl_TransitTimeTo").val(data.transitTimeTo);
    $("#FormV3Dtl_TransitTimeTotal").val(data.transitTimeTotal);

    if (view == 1) {
        $('#ddlRoadCode').prop('disabled', true).trigger("chosen:updated");
        $('#FormV3Dtl_FrmCh').attr("readonly", true);
        $('#FormV3Dtl_FrmChDeci').attr("readonly", true);
        $('#FormV3Dtl_ToCh').attr("readonly", true);
        $('#FormV3Dtl_ToChDeci').attr("readonly", true);
        $("#FormV3Dtl_Length").attr("readonly", true);
        $("#FormV3Dtl_Width").attr("readonly", true);
        $("#FormV3Dtl_Adp").attr("readonly", true);
        $("#FormV3Dtl_TimetakenFrm").attr("readonly", true);
        $("#FormV3Dtl_TimeTakenTo").attr("readonly", true);
        $("#FormV3Dtl_TransitTimeFrm").attr("readonly", true);
        $("#FormV3Dtl_TransitTimeTo").attr("readonly", true);
    }
    else {
        $('#ddlRoadCode').prop('disabled', false).trigger("chosen:updated");
        $('#FormV3Dtl_FrmCh').attr("readonly", false);
        $('#FormV3Dtl_FrmChDeci').attr("readonly", false);
        $('#FormV3Dtl_ToCh').attr("readonly", false);
        $('#FormV3Dtl_ToChDeci').attr("readonly", false);
        $("#FormV3Dtl_Length").attr("readonly", false);
        $("#FormV3Dtl_Width").attr("readonly", false);
        $("#FormV3Dtl_Adp").attr("readonly", false);
        $("#FormV3Dtl_TimetakenFrm").attr("readonly", false);
        $("#FormV3Dtl_TimeTakenTo").attr("readonly", false);
        $("#FormV3Dtl_TransitTimeFrm").attr("readonly", false);
        $("#FormV3Dtl_TransitTimeTo").attr("readonly", false);
    }

}

function CalTotalTime() {
    if ($('#FormV3Dtl_TimeTakenTo').timeEntry('getTime') != null && $('#FormV3Dtl_TimetakenFrm').timeEntry('getTime') != null) {
        var milliseconds = ($('#FormV3Dtl_TimeTakenTo').timeEntry('getTime') - $('#FormV3Dtl_TimetakenFrm').timeEntry('getTime'))
        const secs = Math.floor(Math.abs(milliseconds) / 1000);
        const totalmins = Math.floor(secs / 60);
        const hours = Math.floor(totalmins / 60);
        const mins = Math.floor(totalmins % 60);
        $('#FormV3Dtl_TimeTakenTotal').val(hours + "." + mins);
    }
    else {
        $('#FormV3Dtl_TimeTakenTotal').val(0 + "." + 0);
    }
}

function CalTotalTransitTime() {

    if ($('#FormV3Dtl_TransitTimeTo').timeEntry('getTime') != null && $('#FormV3Dtl_TransitTimeFrm').timeEntry('getTime') != null) {
        var milliseconds = ($('#FormV3Dtl_TransitTimeTo').timeEntry('getTime') - $('#FormV3Dtl_TransitTimeFrm').timeEntry('getTime'))
        const secs = Math.floor(Math.abs(milliseconds) / 1000);
        const totalmins = Math.floor(secs / 60);
        const hours = Math.floor(totalmins / 60);
        const mins = Math.floor(totalmins % 60);
        $('#FormV3Dtl_TransitTimeTotal').val(hours + "." + mins);
    }
    else {
        $('#FormV3Dtl_TransitTimeTotal').val(0 + "." + 0);
    }
}

 

function ClearFormV3Dtl() {

    $('#ddlRoadCode').val("0");
    $('#ddlRoadCode').trigger('chosen:updated');
    $('#FormV3Dtl_FrmCh').val("");
    $('#FormV3Dtl_FrmChDeci').val("");
    $('#FormV3Dtl_ToCh').val("");
    $('#FormV3Dtl_ToChDeci').val("");
    $("#FormV3Dtl_Length").val("");
    $("#FormV3Dtl_Width").val("");
    $("#FormV3Dtl_Adp").val("");
    $("#FormV3Dtl_TimetakenFrm").val("");
    $("#FormV3Dtl_TimeTakenTo").val("");
    $("#FormV3Dtl_TransitTimeFrm").val("");
    $("#FormV3Dtl_TransitTimeTo").val("");

    $("#WorkAccompModal").modal("hide");
}

function DeleteFormV3Dtl(id) {

    InitAjaxLoading();
    $.post('/MAM/DeleteFormV3Dtl?id=' + id, function (data) {
        HideAjaxLoading();
        if (data == -1) {
            app.ShowErrorMessage(data.errorMessage);
        }
        else {
            InitializeGrid();
            app.ShowSuccessMessage('Deleted Successfully', false);
        }
    });
}

function HeaderLogic() {
    if ($("#FormV3_PkRefNo").val() != "0") {

        $("#FormV3_Dt").prop("disabled", true);
        $("#AccordPage0 * > select").attr('disabled', true).trigger("chosen:updated");

        $("#btnFindDetails").hide();

    }
}

    function EnableDisableElements(state) {

        $('#AccordPage0 * > select').prop('disabled', state).trigger("chosen:updated");
        $("#FormV3_Dt").prop("disabled", state);

    }




    function GoBack() {
        if ($("#hdnView").val() == "0") {
            if (app.Confirm("Unsaved changes will be lost. Are you sure you want to cancel?", function (e) {
                if (e) {
                    location.href = "/MAM/FormV3";

                }
            }));
        }
        else {
            location.href = "/MAM/FormV3";
        }
    }

