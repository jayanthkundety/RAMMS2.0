

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


    HeaderLogic();

    if ($("#FormV4_Status").val() == "") {

        $("#saveFormV4Btn").hide();
        $("#SubmitFormV4Btn").hide();
    }
    else if ($("#FormV4_Status").val() == "Initialize" || $("#FormV4_Status").val() == "Saved") {

        $("#saveFormV4Btn").show();
        $("#SubmitFormV4Btn").show();
    } else {

        $("#saveFormV4Btn").hide();
        $("#SubmitFormV4Btn").hide();
    }

    if ($("#hdnView").val() == "1") {

        $('#FormV4_Remarks').attr("readonly", "true");
    }


    $('#ddlFacilitatedby').prop('disabled', true).trigger("chosen:updated");
    $('#ddlAgreedby').prop('disabled', true).trigger("chosen:updated");
    $('#ddlVettedby').prop('disabled', true).trigger("chosen:updated");
    $('#FormV4_DtAgr').attr("readonly", "true");
    $('#FormV4_DtVet').attr("readonly", "true");
    $('#FormV4_DtFac').attr("readonly", "true");
    $('#FormV4_SignVet').prop('disabled', true);
    $('#FormV4_SignFac').prop('disabled', true);
    $('#FormV4_SignAgr').prop('disabled', true);



    $("#ddlActCode").on("change", function () {

        var val = $(this).find(":selected").text();
        val = val.split("-").length > 0 ? val.split("-")[1] : val;
        $("#FormV4_ActName").val(val);
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
                            $("#FormV4_SecName").val(data._RMAllData.secName);

                            $("#FormV4_Division").val(data._RMAllData.divisionCode);

                        }

                    }
                },
                error: function (data) {

                    console.error(data);
                }
            });
        }
        else {
            $("#FormV4_Secname").val("");
            $("#FormV4_DivCode").val("");
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
                    $("#FormV4_Crewname").val(data.userName);

                },
                error: function (data) {
                    console.error(data);
                }
            });
        }
        else if (id == "99999999") {

            $("#FormV4_Crewname").val('');
        }
        else {

            $("#FormV4_Crewname").val('');
        }

        return false;
    });
    $("#ddlCrew").trigger('change');

});
 

function OnRoadChange(tis) {

    var ctrl = $("#ddlRoadCode");
    $('#FormV4Dtl_RoadCode').val(ctrl.val());

    if (ctrl.find("option:selected").attr("fromkm") != undefined)
        $("#FormV4Dtl_FrmCh").val(ctrl.find("option:selected").attr("fromkm"));
    else
        $("#FormV4Dtl_FrmCh").val(ctrl.find("option:selected").attr("Item1"));

    if (ctrl.find("option:selected").attr("fromm") != undefined)
        $("#FormV4Dtl_ToChDeci").val(ctrl.find("option:selected").attr("fromm"));
    else
        $("#FormV4Dtl_ToChDeci").val(ctrl.find("option:selected").attr("Item2"));

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
                $("#FormV4Dtl_RoadName").val(data);
            }
        }
    })
}


function OnVettedChange(tis) {

    var ctrl = $(tis);
    if (ctrl.val() != null)
        $('#FormV4_UseridVet').val(ctrl.val());
    if ($('#FormV4_UseridVet').val() != "") {
        $("#FormV4_UsernameVet").val(ctrl.find("option:selected").attr("Item1"));
        $("#FormV4_DesignationVet").val(ctrl.find("option:selected").attr("Item2"));

        if ($('#FormV4_UseridVet').val() == "99999999") {
            $("#FormV4_UsernameVet").removeAttr("readonly");
            $("#FormV4_DesignationVet").removeAttr("readonly");

        } else {
            $("#FormV4_UsernameVet").attr("readonly", "true");
            $("#FormV4_DesignationVet").attr("readonly", "true");
        }
        $('#FormV4_SignVet').prop('checked', true);
    }
    else {
        $("#FormV4_UsernameVet").val('');
        $("#FormV4_DesignationVet").val('');
        $('#FormV4_SignVet').prop('checked', false);
    }
}

function OnAgreedbyChange(tis) {

    var ctrl = $(tis);
    if (ctrl.val() != null)
        $('#FormV4_UseridAgr').val(ctrl.val());
    if ($('#FormV4_UseridAgr').val() != "") {
        $("#FormV4_UsernameAgr").val(ctrl.find("option:selected").attr("Item1"));
        $("#FormV4_DesignationAgr").val(ctrl.find("option:selected").attr("Item2"));

        if ($('#FormV4_UseridAgr').val() == "99999999") {
            $("#FormV4_UsernameAgr").removeAttr("readonly");
            $("#FormV4_DesignationAgr").removeAttr("readonly");

        } else {
            $("#FormV4_UsernameAgr").attr("readonly", "true");
            $("#FormV4_DesignationAgr").attr("readonly", "true");
        }
        $('#FormV4_SignAgr').prop('checked', true);
    }
    else {
        $("#FormV4_UsernameAgr").val('');
        $("#FormV4_DesignationAgr").val('');
        $('#FormV4_SignAgr').prop('checked', false);
    }
}

function OnFacilitatedbyChange(tis) {

    var ctrl = $(tis);
    if (ctrl.val() != null)
        $('#FormV4_UseridFac').val(ctrl.val());
    if ($('#FormV4_UseridFac').val() != "") {
        $("#FormV4_UsernameFac").val(ctrl.find("option:selected").attr("Item1"));
        $("#FormV4_DesignationFac").val(ctrl.find("option:selected").attr("Item2"));

        if ($('#FormV4_UseridFac').val() == "99999999") {
            $("#FormV4_UsernameFac").removeAttr("readonly");
            $("#FormV4_DesignationFac").removeAttr("readonly");

        } else {
            $("#FormV4_UsernameFac").attr("readonly", "true");
            $("#FormV4_DesignationFac").attr("readonly", "true");
        }
        $('#FormV4_SignFac').prop('checked', true);
    }
    else {
        $("#FormV4_UsernameFac").val('');
        $("#FormV4_DesignationFac").val('');
        $('#FormV4_SignFac').prop('checked', false);
    }
}



function Save(SubmitType) {


    if (SubmitType == "Submitted") {
        $("#FormV4_SubmitSts").val(true);
    }

    if (ValidatePage('#AccordPage0')) {

        if ($("#FormV4_Status").val() == "")
            $("#FormV4_Status").val("Initialize");
        else if ($("#FormV4_Status").val() == "Initialize")
            $("#FormV4_Status").val("Saved");

        InitAjaxLoading();
        EnableDisableElements(false);
        $.get('/MAM/SaveFormV4', $("form").serialize(), function (data) {
            EnableDisableElements(true)
            HideAjaxLoading();
            if (data == -1) {
                app.ShowErrorMessage(data.errorMessage);
            }
            else {
                 
                if (SubmitType == "") {
                    debugger
                    if (data.result == "Success") {

                        if (data.formExist) {
                            location.href = "/MAM/EditFormV4?Id=" + data.pkRefNo + "&view=0";
                            return;
                        }

                        $("#FormV4_PkRefNo").val(data.pkRefNo);
                        $("#FormV4_RefId").val(data.refId);
                        $("#FormV4_Status").val(data.status);
                        $("#FormV4_TotalProduction").val(data.totalProduction)
                        $("#FormV4_FV3PKRefNo").val(data.fV3PKRefNo)
                        $("#FormV4_FV3PKRefID").val(data.fV3PKRefID)
                        $("#saveFormV4Btn").show();
                        $("#SubmitFormV4Btn").show();
                        HeaderLogic();
                       
                    //    app.ShowSuccessMessage('Saved Successfully', false);
                    }
                    else {
                        EnableDisableElements(false);
                        app.ShowErrorMessage(data.msg, false);
                    }

                }
                else if (SubmitType == "Saved") {
                    app.ShowSuccessMessage('Saved Successfully', false);

                    location.href = "/MAM/FormV4";
                }
                else if (SubmitType == "Submitted") {
                    app.ShowSuccessMessage('Submitted Successfully', false);
                    location.href = "/MAM/FormV4";
                }

            }
        });
    }

}


 
function HeaderLogic() {
    if ($("#FormV4_PkRefNo").val() != "0") {

        $("#FormV4_Dt").prop("disabled", true);
        $("#AccordPage0 * > select").attr('disabled', true).trigger("chosen:updated");

        $("#btnFindDetails").hide();
        $("#FormV4_StartTime").attr("readonly", "true");
        $("#FormV4_EndTime").attr("readonly", "true");
    }
}

    function EnableDisableElements(state) {

        $('#AccordPage0 * > select').prop('disabled', state).trigger("chosen:updated");
        $("#FormV4_Dt").prop("disabled", state);

    }



    function GoBack() {
        if ($("#hdnView").val() == "0") {
            if (app.Confirm("Unsaved changes will be lost. Are you sure you want to cancel?", function (e) {
                if (e) {
                    location.href = "/MAM/FormV4";

                }
            }));
        }
        else {
            location.href = "/MAM/FormV4";
        }
    }

