
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

    if ($("#FormV1_Status").val() == "") {
        $("#btnWorkScheduleAdd").hide();
        $("#btnWorkSchedulePull").hide();
        $("#saveFormV1Btn").hide();
        $("#SubmitFormV1Btn").hide();
    }
    else if ($("#FormV1_Status").val() == "Initialize" || $("#FormV1_Status").val() == "Saved") {
        $("#btnWorkScheduleAdd").hide();
        $("#btnWorkSchedulePull").hide();
        $("#saveFormV1Btn").show();
        $("#SubmitFormV1Btn").show();
    } else {

        $("#saveFormV1Btn").hide();
        $("#SubmitFormV1Btn").hide();
    }

    if ($("#hdnView").val() == "1") {
        $("#btnWorkScheduleAdd").hide();
        $("#btnWorkSchedulePull").hide();
        $('#FormV1_Remarks').attr("readonly", "true");
    }


    $('#ddlAcknowledgedby').prop('disabled', true).trigger("chosen:updated");
    $('#ddlAgreedby').prop('disabled', true).trigger("chosen:updated");
    $('#ddlScheduledby').prop('disabled', true).trigger("chosen:updated");
    $('#FormV1_DtAgr').attr("readonly", "true");
    $('#FormV1_DtAck').attr("readonly", "true");
    $('#FormV1_DtSch').attr("readonly", "true");
    $('#FormV1_SignAck').prop('disabled', true);
    $('#FormV1_SignSch').prop('disabled', true);
    $('#FormV1_SignAgr').prop('disabled', true);

    //if ($("#FormV1_Status").val() == "" || $("#FormV1_Status").val( ) == "Initialize" || $("#FormV1_Status").val() == "Saved") {
    //    $('#ddlAcknowledgedby').prop('disabled', true).trigger("chosen:updated");
    //    $('#ddlAgreedby').prop('disabled', true).trigger("chosen:updated");
    //    $('#FormV1_DtAgr').attr("readonly", "true");
    //    $('#FormV1_DtAck').attr("readonly", "true");
    //    $('#FormV1_SignAgr').prop('disabled', true);
    //    $('#FormV1_SignAck').prop('disabled', true);
    //}
    //else if ($("#FormV1_Status").val() == "Submitted") {
    //    $('#ddlScheduledby').prop('disabled', true).trigger("chosen:updated");
    //    $('#ddlAgreedby').prop('disabled', true).trigger("chosen:updated");
    //    $('#FormV1_DtAgr').attr("readonly", "true");
    //    $('#FormV1_DtSch').attr("readonly", "true");
    //    $('#FormV1_SignAgr').prop('disabled', true);
    //    $('#FormV1_SignSch').prop('disabled', true);
    //}
    //else if ($("#FormV1_Status").val() == "Verified") {
    //    $('#ddlAcknowledgedby').prop('disabled', true).trigger("chosen:updated");
    //    $('#ddlScheduledby').prop('disabled', true).trigger("chosen:updated");
    //    $('#FormV1_DtAck').attr("readonly", "true");
    //    $('#FormV1_DtSch').attr("readonly", "true");
    //    $('#FormV1_SignAck').prop('disabled', true);
    //    $('#FormV1_SignSch').prop('disabled', true);
    //}
    //else if ($("#FormV1_Status").val() == "Approved") {
    //    $('#ddlAcknowledgedby').prop('disabled', true).trigger("chosen:updated");
    //    $('#ddlAgreedby').prop('disabled', true).trigger("chosen:updated");
    //    $('#ddlScheduledby').prop('disabled', true).trigger("chosen:updated");
    //    $('#FormV1_DtAgr').attr("readonly", "true");
    //    $('#FormV1_DtAck').attr("readonly", "true");
    //    $('#FormV1_DtSch').attr("readonly", "true");
    //    $('#FormV1_SignAck').prop('disabled', true);
    //    $('#FormV1_SignSch').prop('disabled', true);
    //    $('#FormV1_SignAgr').prop('disabled', true);
    //}

    $("#ddlActCode").on("change", function () {

        var val = $(this).find(":selected").text();
        val = val.split("-").length > 0 ? val.split("-")[1] : val;
        $("#FormV1_ActName").val(val);
    });

    $("#ddlSource").on("change", function () {


        if ($(this).val() == "S1") {
            $(".divRefno").show();
            $("#btnWorkScheduleAdd").hide();
            $("#btnWorkSchedulePull").show();

        } else if ($(this).val() == "V1") {
            $(".divRefno").hide();
            $("#ddlRefNo").val(0);
            $('#ddlRefNo').trigger('chosen:updated');
            $("#btnWorkScheduleAdd").show();
            $("#btnWorkSchedulePull").hide();
            LoadS1(0);
        }
    });

    $("#ddlRefNo").on("change", function () {

        InitAjaxLoading();
        EnableDisableElements(false);
        LoadS1($(this).val());
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
                            $("#FormV1_SecName").val(data._RMAllData.secName);

                            $("#FormV1_DivCode").val(data._RMAllData.divisionCode);

                        }

                    }
                },
                error: function (data) {

                    console.error(data);
                }
            });
        }
        else {
            $("#FormV1_SecName").val("");
            $("#FormV1_DivCode").val("");
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
                    $("#FormV1_Crewname").val(data.userName);

                },
                error: function (data) {
                    console.error(data);
                }
            });
        }
        else if (id == "99999999") {

            $("#FormV1_Crewname").val('');
        }
        else {

            $("#FormV1_Crewname").val('');
        }

        return false;
    });
    $("#ddlCrew").trigger('change');

});




function OnRoadChange(tis) {

    var ctrl = $("#ddlRoadCode");
    $('#FormV1Dtl_RoadCode').val(ctrl.val());

    if (ctrl.find("option:selected").attr("fromkm") != undefined)
        $("#FormV1Dtl_FrmCh").val(ctrl.find("option:selected").attr("fromkm"));
    else
        $("#FormV1Dtl_FrmCh").val(ctrl.find("option:selected").attr("Item1"));

    if (ctrl.find("option:selected").attr("fromm") != undefined)
        $("#FormV1Dtl_ToChDeci").val(ctrl.find("option:selected").attr("fromm"));
    else
        $("#FormV1Dtl_ToChDeci").val(ctrl.find("option:selected").attr("Item2"));

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
                $("#FormV1Dtl_RoadName").val(data);
            }
        }
    })
}


function OnAcknowledgedChange(tis) {

    var ctrl = $(tis);
    if (ctrl.val() != null)
        $('#FormV1_UseridAck').val(ctrl.val());
    if ($('#FormV1_UseridAck').val() != "") {
        $("#FormV1_UsernameAck").val(ctrl.find("option:selected").attr("Item1"));
        $("#FormV1_DesignationAck").val(ctrl.find("option:selected").attr("Item2"));

        if ($('#FormV1_UseridAck').val() == "99999999") {
            $("#FormV1_UsernameAck").removeAttr("readonly");
            $("#FormV1_DesignationAck").removeAttr("readonly");

        } else {
            $("#FormV1_UsernameAck").attr("readonly", "true");
            $("#FormV1_DesignationAck").attr("readonly", "true");
        }
        $('#FormV1_SignAck').prop('checked', true);
    }
    else {
        $("#FormV1_UsernameAck").val('');
        $("#FormV1_DesignationAck").val('');
        $('#FormV1_SignAck').prop('checked', false);
    }
}

function OnAgreedbyChange(tis) {

    var ctrl = $(tis);
    if (ctrl.val() != null)
        $('#FormV1_UseridAgr').val(ctrl.val());
    if ($('#FormV1_UseridAgr').val() != "") {
        $("#FormV1_UsernameAgr").val(ctrl.find("option:selected").attr("Item1"));
        $("#FormV1_DesignationAgr").val(ctrl.find("option:selected").attr("Item2"));

        if ($('#FormV1_UseridAgr').val() == "99999999") {
            $("#FormV1_UsernameAgr").removeAttr("readonly");
            $("#FormV1_DesignationAgr").removeAttr("readonly");

        } else {
            $("#FormV1_UsernameAgr").attr("readonly", "true");
            $("#FormV1_DesignationAgr").attr("readonly", "true");
        }
        $('#FormV1_SignAgr').prop('checked', true);
    }
    else {
        $("#FormV1_UsernameAgr").val('');
        $("#FormV1_DesignationAgr").val('');
        $('#FormV1_SignAgr').prop('checked', false);
    }
}

function OnScheduledbyChange(tis) {

    var ctrl = $(tis);
    if (ctrl.val() != null)
        $('#FormV1_UseridSch').val(ctrl.val());
    if ($('#FormV1_UseridSch').val() != "") {
        $("#FormV1_UsernameSch").val(ctrl.find("option:selected").attr("Item1"));
        $("#FormV1_DesignationSch").val(ctrl.find("option:selected").attr("Item2"));

        if ($('#FormV1_UseridSch').val() == "99999999") {
            $("#FormV1_UsernameSch").removeAttr("readonly");
            $("#FormV1_DesignationSch").removeAttr("readonly");

        } else {
            $("#FormV1_UsernameSch").attr("readonly", "true");
            $("#FormV1_DesignationSch").attr("readonly", "true");
        }
        $('#FormV1_SignSch').prop('checked', true);
    }
    else {
        $("#FormV1_UsernameSch").val('');
        $("#FormV1_DesignationSch").val('');
        $('#FormV1_SignSch').prop('checked', false);
    }
}



function Save(GroupName, SubmitType) {

    
    if (SubmitType == "Submitted") {
        $("#FormV1_SubmitSts").val(true);
    }

    if (ValidatePage('#AccordPage0')) {

        if ($("#FormV1_Status").val() == "")
            $("#FormV1_Status").val("Initialize");
        else if ($("#FormV1_Status").val() == "Initialize")
            $("#FormV1_Status").val("Saved");

        InitAjaxLoading();
        EnableDisableElements(false);
        $.get('/MAM/SaveFormV1', $("form").serialize(), function (data) {
            EnableDisableElements(true)
            HideAjaxLoading();
            if (data == -1) {
                app.ShowErrorMessage(data.errorMessage);
            }
            else {

                if (SubmitType == "") {
                    UpdateFormAfterSave(data);
                    app.ShowSuccessMessage('Saved Successfully', false);
                }
                else if (SubmitType == "Saved") {
                    app.ShowSuccessMessage('Saved Successfully', false);
                    $('#ddlSource').prop('disabled', true).trigger("chosen:updated");
                    $('#ddlRefNo').prop('disabled', true).trigger("chosen:updated");
                    location.href = "/MAM/FormV1";
                }
                else if (SubmitType == "Submitted") {
                    app.ShowSuccessMessage('Submitted Successfully', false);
                    location.href = "/MAM/FormV1";
                }
                else if (SubmitType == "Verified") {
                    process.ShowApprove(GroupName, SubmitType);
                }
            }
        });
    }

}



function SaveFormV1WorkSchedule() {

    if (ValidatePage('#WorkScheduleModal')) {

        $('#ddlRoadCode').prop('disabled', false).trigger("chosen:updated");
        $('#FormV1Dtl_FrmCh').attr("readonly", false);
        $('#FormV1Dtl_FrmChDeci').attr("readonly", false);
        $('#FormV1Dtl_ToCh').attr("readonly", false);
        $('#FormV1Dtl_ToChDeci').attr("readonly", false);
        $('#ddlSiteRef').prop('disabled', false).trigger("chosen:updated");

        InitAjaxLoading();
        $.post('/MAM/SaveFormV1WorkSchedule', $("form").serialize(), function (data) {
            HideAjaxLoading();
            if (data == -1) {
                app.ShowErrorMessage(data.errorMessage);
            }
            else {
                ClearWorkSchedule()

                InitializeGrid();
                app.ShowSuccessMessage('Saved Successfully', false);
            }
        });
    }

}

function UpdateFormAfterSave(data) {

    $("#FormV1_PkRefNo").val(data.pkRefNo);
    $("#FormV1_RefId").val(data.refId);
    $("#FormV1_Status").val(data.status)
    $("#ddlSource").val(data.source)
   
    $("#hdnPkRefNo").val(data.pkRefNo);
    $("#saveFormV1Btn").show();
    $("#SubmitFormV1Btn").show();


    //  $("#btnWorkScheduleAdd").show();
    $("#btnFindDetails").hide();
    HeaderLogic();

    var dsRefNo = data.refNoDS;

    if (dsRefNo.length > 0) {
        $("#ddlRefNo").empty();
        $("#ddlRefNo").append($("<option></option>").val("0").html("Select Reference No"));
        $.each(dsRefNo, function (index, v) {
            $("#ddlRefNo").append($("<option></option>").val(v.value).html(v.text));
        });
        $("#ddlRefNo").val(data.s1RefNo)
        $('#ddlRefNo').trigger("chosen:updated");

        //if ($("#FormV1_Status").val() == "Initialize" || $("#FormV1_Status").val() == "") {
        //    $(".divRefno > select").attr('disabled', false).trigger("chosen:updated")
        //}
    }


    InitializeGrid();

}

function ClearWorkSchedule() {

    $('#ddlRoadCode').val("0");
    $('#ddlRoadCode').trigger('chosen:updated');
    $('#ddlSiteRef').val("0");
    $('#ddlSiteRef').trigger('chosen:updated');
    $("#FormV1Dtl_Remarks").val("");
    $("#FormV1Dtl_FrmCh").val("");
    $("#FormV1Dtl_ToCh").val("");
    $("#FormV1Dtl_RoadName").val("");
    $("#WorkScheduleModal").modal("hide");
}

function DeleteV1WorkSchedule(id) {

    InitAjaxLoading();
    $.post('/MAM/DeleteFormV1WorkSchedule?id=' + id, function (data) {
        HideAjaxLoading();
        if (data == -1) {
            app.ShowErrorMessage(data.errorMessage);
        }
        else {
            InitializeGrid();
            app.ShowErrorMessage('Deleted Successfully', false);
        }
    });
}


function LoadS1(S1PKRefNo) {
    InitAjaxLoading();
    $.ajax({
        url: '/MAM/LoadS1Data',
        dataType: 'JSON',
        data: { PKRefNo: $("#FormV1_PkRefNo").val(), S1PKRefNo: S1PKRefNo, ActCode: $("#ddlActCode").val() },
        type: 'Post',
        success: function (data) {
            HideAjaxLoading();
            EnableDisableElements(true)
            HideAjaxLoading();
            InitializeGrid();
        },
        error: function (data) {
            console.error(data);
        }
    });
}

function HeaderLogic() {
    if ($("#FormV1_PkRefNo").val() != "0") {

        $("#FormV1_Dt").prop("disabled", true);
        $("#AccordPage0 * > select").attr('disabled', true).trigger("chosen:updated");

        $("#btnFindDetails").hide();
        //   $("#btnWorkScheduleAdd").show();

        if ($("#ddlSource").val() == "S1") {
            $("#btnWorkScheduleAdd").hide();
            $("#btnWorkSchedulePull").show();
        }
        else {
            $(".divRefno").hide();
            if ($("#hdnView").val() != "1") {
                $("#btnWorkScheduleAdd").show();
                $("#btnWorkSchedulePull").hide();
            }
        }

        if ($("#FormV1_Status").val() == "Initialize" || $("#FormV1_Status").val() == "Saved") {

            $("#ddlRefNo").prop("disabled", false);
            if ($("#ddlSource").val() == "") {
                $('#ddlSource').val("V1");
                $('#ddlSource').trigger('chosen:updated');

            }

            $('#ddlSource').prop('disabled', false).trigger("chosen:updated");
            $('#ddlRefNo').prop('disabled', false).trigger("chosen:updated");
        }
        //else if ($("#FormV1_Status").val() == "Saved") {
        //    ///* $("#ddlSource").prop("disabled", true);*/
        //    //if ($("#ddlSource").val() == "S1") {
        //    //    $('#ddlSource').val("S1");
        //    //    $('#ddlSource').trigger('chosen:updated');
        //    //    $(".divRefno").show();
        //    //}

        //    $('#ddlSource').prop('disabled', true).trigger("chosen:updated");
        //    $('#ddlRefNo').prop('disabled', true).trigger("chosen:updated");
        //}
        else {
            $('#ddlSource').prop('disabled', true).trigger("chosen:updated");
            $('#ddlRefNo').prop('disabled', true).trigger("chosen:updated");
            //  $("select").attr('disabled', true).trigger("chosen:updated")
        }

    }
    else {
        $("#btnWorkScheduleAdd").hide();
        $("#btnWorkSchedulePull").hide();
    }

}

function EnableDisableElements(state) {

    // $("#AccordPage0 * :not(.divsource *.divrefno *)").prop("disabled", state);
    $('#AccordPage0 * > select').prop('disabled', state).trigger("chosen:updated");
    $("#FormV1_Dt").prop("disabled", state);
    $('#ddlSource').prop('disabled', false).trigger("chosen:updated");
    $('#ddlRefNo').prop('disabled', false).trigger("chosen:updated");
}


function EditFormV1WorkSchedule(obj, view) {

    var currentRow = $(obj).closest("tr");
    var data = $('#WorkScheduleGridView').DataTable().row(currentRow).data();

    $("#FormV1Dtl_PkRefNo").val(data.pkRefNo);
    $("#ddlRoadCode").val(data.roadCode);
    $("#ddlRoadCode").trigger('chosen:updated');
    $("#ddlRoadCode").trigger('change');
    $("#FormV1Dtl_StartTime").val(data.startTime);
    $("#FormV1Dtl_Remarks").val(data.remarks);
    $("#ddlSiteRef").val(data.siteRef);
    $("#ddlSiteRef").trigger('chosen:updated');
    $("#ddlSiteRef").trigger('change');

    if (view == 0) {
        if (data.fs1dPkRefNo != 0) {
            $('#ddlRoadCode').prop('disabled', true).trigger("chosen:updated");
            $('#FormV1Dtl_FrmCh').attr("readonly", true);
            $('#FormV1Dtl_FrmChDeci').attr("readonly", true);
            $('#FormV1Dtl_ToCh').attr("readonly", true);
            $('#FormV1Dtl_ToChDeci').attr("readonly", true);
            $('#ddlSiteRef').prop('disabled', true).trigger("chosen:updated");

        }
        else {
            $('#ddlRoadCode').prop('disabled', false).trigger("chosen:updated");
            $('#FormV1Dtl_FrmCh').attr("readonly", false);
            $('#FormV1Dtl_FrmChDeci').attr("readonly", false);
            $('#FormV1Dtl_ToCh').attr("readonly", false);
            $('#FormV1Dtl_ToChDeci').attr("readonly", false);
            $('#ddlSiteRef').prop('disabled', false).trigger("chosen:updated");
        }
    }
    else {
        $('#ddlRoadCode').prop('disabled', true).trigger("chosen:updated");
        $('#FormV1Dtl_FrmCh').attr("readonly", true);
        $('#FormV1Dtl_FrmChDeci').attr("readonly", true);
        $('#FormV1Dtl_ToCh').attr("readonly", true);
        $('#FormV1Dtl_ToChDeci').attr("readonly", true);
        $('#ddlSiteRef').prop('disabled', true).trigger("chosen:updated");
        $("#FormV1Dtl_Remarks").attr("readonly", true);
        $("#saveFormV1DtlBtn").hide();
    }


}

function PullS1Data() {
   
    if ($("#ddlRefNo").val() != "0" && $("#ddlRefNo").val() != "") {
        InitAjaxLoading();
        $.ajax({
            url: '/MAM/PullS1Data',
            dataType: 'JSON',
            data: { PKRefNo: $("#FormV1_PkRefNo").val(), S1PKRefNo: $("#ddlRefNo").val(), ActCode: $("#ddlActCode").val() },
            type: 'Post',
            success: function (data) {
                HideAjaxLoading();
                EnableDisableElements(true)
                HideAjaxLoading();
                InitializeGrid();
            },
            error: function (data) {
                console.error(data);
            }
        });
    }
    else {
        app.ShowWarningMessage('Please select Refrence No', false);
    }
}


function GoBack() {
    if ($("#hdnView").val() == "0") {
        if (app.Confirm("Unsaved changes will be lost. Are you sure you want to cancel?", function (e) {
            if (e) {
                location.href = "/MAM/FormV1";

            }
        }));
    }
    else
        location.href = "/MAM/FormV1";
}