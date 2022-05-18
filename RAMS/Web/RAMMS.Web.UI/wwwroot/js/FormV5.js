

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

    if ($("#FormV5_PkRefNo").val() == "0") {
        $("#btnPhotoVideoModal").hide();
    }

    $('#AccordPage1').on('shown.bs.collapse', function () {
  
        $.each($.fn.dataTable.tables(true), function () {
            $(this).DataTable().columns.adjust().draw();
        });
    });

    HeaderLogic();

    if ($("#FormV5_Status").val() == "") {

        $("#saveFormV5Btn").hide();
        $("#SubmitFormV5Btn").hide();
    }
    else if ($("#FormV5_Status").val() == "Initialize" || $("#FormV5_Status").val() == "Saved") {

        $("#saveFormV5Btn").show();
        $("#SubmitFormV5Btn").show();
    } else {

        $("#saveFormV5Btn").hide();
        $("#SubmitFormV5Btn").hide();
    }

    if ($("#hdnView").val() == "1") {

        $('#FormV5_Remarks').attr("readonly", "true");
        $("#saveFormV5Btn").hide();
        $("#SubmitFormV5Btn").hide();
    }



    $('#ddlRecordedby').prop('disabled', true).trigger("chosen:updated");
    $('#FormV5_DtRec').attr("readonly", "true");
  //  $('#FormV5_SignRec').prop('disabled', true);




    $("#ddlActCode").on("change", function () {

        var val = $(this).find(":selected").text();
        val = val.split("-").length > 0 ? val.split("-")[1] : val;
        $("#FormV5_ActName").val(val);
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
                            $("#FormV5_SecName").val(data._RMAllData.secName);

                            $("#FormV5_Division").val(data._RMAllData.divisionCode);

                        }

                    }
                },
                error: function (data) {

                    console.error(data);
                }
            });
        }
        else {
            $("#FormV5_Secname").val("");
            $("#FormV5_DivCode").val("");
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
                    $("#FormV5_Crewname").val(data.userName);

                },
                error: function (data) {
                    console.error(data);
                }
            });
        }
        else if (id == "99999999") {

            $("#FormV5_Crewname").val('');
        }
        else {

            $("#FormV5_Crewname").val('');
        }

        return false;
    });
    $("#ddlCrew").trigger('change');

});


function OnRoadChange(tis) {

    var ctrl = $("#ddlRoadCode");
    $('#FormV5Dtl_RoadCode').val(ctrl.val());

    if (ctrl.find("option:selected").attr("fromkm") != undefined)
        $("#FormV5Dtl_FrmCh").val(ctrl.find("option:selected").attr("fromkm"));
    else
        $("#FormV5Dtl_FrmCh").val(ctrl.find("option:selected").attr("Item1"));

    if (ctrl.find("option:selected").attr("fromm") != undefined)
        $("#FormV5Dtl_ToChDeci").val(ctrl.find("option:selected").attr("fromm"));
    else
        $("#FormV5Dtl_ToChDeci").val(ctrl.find("option:selected").attr("Item2"));

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
                $("#FormV5Dtl_RoadName").val(data);
            }
        }
    })
}


function OnRecordedChange(tis) {

    var ctrl = $(tis);
    if (ctrl.val() != null)
        $('#FormV5_UseridRec').val(ctrl.val());
    if ($('#FormV5_UseridRec').val() != "") {
        $("#FormV5_UsernameRec").val(ctrl.find("option:selected").attr("Item1"));
        $("#FormV5_DesignationRec").val(ctrl.find("option:selected").attr("Item2"));

        if ($('#FormV5_UseridRec').val() == "99999999") {
            $("#FormV5_UsernameRec").removeAttr("readonly");
            $("#FormV5_DesignationRec").removeAttr("readonly");

        } else {
            $("#FormV5_UsernameRec").attr("readonly", "true");
            $("#FormV5_DesignationRec").attr("readonly", "true");
        }
        $('#FormV5_SignRec').prop('checked', true);
    }
    else {
        $("#FormV5_UsernameRec").val('');
        $("#FormV5_DesignationRec").val('');
        $('#FormV5_SignRec').prop('checked', false);
    }
}



function Save(SubmitType) {


    if (SubmitType == "Submitted") {
        $("#FormV5_SubmitSts").val(true);
    }

    if (ValidatePage('#AccordPage0')) {

        if ($("#FormV5_Status").val() == "")
            $("#FormV5_Status").val("Initialize");
        else if ($("#FormV5_Status").val() == "Initialize")
            $("#FormV5_Status").val("Saved");

        InitAjaxLoading();
        EnableDisableElements(false);
        $.get('/MAM/SaveFormV5', $("form").serialize(), function (data) {
            EnableDisableElements(true)
            HideAjaxLoading();
            if (data == -1) {
                app.ShowErrorMessage(data.errorMessage);
            }
            else {

                if (SubmitType == "") {
                     
                    if (data.result == "Success") {

                        if (data.formExist) {
                            location.href = "/MAM/EditFormV5?Id=" + data.pkRefNo + "&view=0";
                            return;
                        }

                        $("#FormV5_PkRefNo").val(data.pkRefNo);
                        $("#FormV5_RefId").val(data.refId);
                        $("#FormV5_Status").val(data.status);

                        $("#FormV5_FV4PKRefNo").val(data.fV4PKRefNo)
                        $("#FormV5_FV4PKRefID").val(data.fV4PKRefID)
                        $("#saveFormV5Btn").show();
                        $("#SubmitFormV5Btn").show();
                        $("#btnPhotoVideoModal").show();
                       
                        HeaderLogic();
 
                    }
                    else {
                        EnableDisableElements(false);
                        app.ShowErrorMessage(data.msg, false);
                    }

                }
                else if (SubmitType == "Saved") {
                    app.ShowSuccessMessage('Saved Successfully', false);

                    location.href = "/MAM/FormV5";
                }
                else if (SubmitType == "Submitted") {
                    app.ShowSuccessMessage('Submitted Successfully', false);
                    location.href = "/MAM/FormV5";
                }

            }
        });
    }

}



function SaveFormV5Dtl() {

    if (ValidatePage('#PhotoVideoModal')) {
        InitAjaxLoading();
        $.post('/MAM/SaveFormV5Dtl', $("form").serialize(), function (data) {
            HideAjaxLoading();
            if (data == -1) {
                app.ShowErrorMessage(data.errorMessage);
            }
            else {
                ClearFormV5Dtl();
                $('#PhotoVideoGridView').DataTable().settings()[0].ajax.url = "/MAM/GetV5DtlGridList?V5PkRefNo=" + $("#FormV5_PkRefNo").val();
                $('#PhotoVideoGridView').DataTable().ajax.reload();
                app.ShowSuccessMessage('Saved Successfully', false);
                $('#PhotoVideoModal').modal('hide');
            }
        });
    }

}

function EditFormV5Dtl(obj, view) {

    var currentRow = $(obj).closest("tr");
    var data = $('#PhotoVideoGridView').DataTable().row(currentRow).data();

    $("#FormV5Dtl_PkRefNo").val(data.pkRefNo);
    $("#FormV5Dtl_Fv1dPkRefNo").val(data.fv1dPkRefNo);
    $('#spnfilename').html(data.imageFilenameSys);
    $('#FormV5Dtl_ImageFilenameSys').val(data.imageFilenameSys);
    $('#FormV5Dtl_ImageFilenameUpload').val(data.imageFilenameUpload);
    $('#FormV5Dtl_ImageUserFilePath').val(data.imageUserFilePath);
    $('#FormV5Dtl_FileNameFrm').val(data.fileNameFrm);
    $('#FormV5Dtl_FileNameTo').val(data.fileNameTo);
    $('#FormV5Dtl_Desc').val(data.desc);

    if (view == 1) {
        
        $('#FormV5Dtl_Desc').attr("readonly", true);
        $('#FormV5Dtl_FileNameFrm').attr("readonly", true);
        $('#FormV5Dtl_FileNameTo').attr("readonly", true);
        $("#divattach").hide();
        $("#saveFormv5Btn").hide();
       
    }
    else {
        $('#FormV5Dtl_Desc').attr("readonly", false);
        $('#FormV5Dtl_FileNameFrm').attr("readonly", false);
        $('#FormV5Dtl_FileNameTo').attr("readonly", false);
        $("#saveFormv5Btn").show();
        $("#divattach").show();
    }

}

function ClearFormV5Dtl() {

    $('#spnfilename').html("");
    $('#FormV5Dtl_ImageFilenameSys').val("");
    $('#FormV5Dtl_ImageFilenameUpload').val("");
    $('#FormV5Dtl_ImageUserFilePath').val("");
    $('#FormV5Dtl_FileNameFrm').val("");
    $('#FormV5Dtl_FileNameTo').val("");
    $('#FormV5Dtl_Desc').val("");
    $('#AttachmentModal').modal('hide');
    
}

function DeleteFormV5Dtl(id) {

    InitAjaxLoading();
    $.post('/MAM/DeleteFormV5Dtl?id=' + id, function (data) {
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
    if ($("#FormV5_PkRefNo").val() != "0") {

        $("#FormV5_Dt").prop("disabled", true);
        $("#AccordPage0 * > select").attr('disabled', true).trigger("chosen:updated");

        $("#btnFindDetails").hide();
        $("#FormV5_StartTime").attr("readonly", "true");
        $("#FormV5_EndTime").attr("readonly", "true");
    }
}

function EnableDisableElements(state) {

    $('#AccordPage0 * > select').prop('disabled', state).trigger("chosen:updated");
    $("#FormV5_Dt").prop("disabled", state);

}



function GoBack() {
    if ($("#hdnView").val() == "0") {
        if (app.Confirm("Unsaved changes will be lost. Are you sure you want to cancel?", function (e) {
            if (e) {
                location.href = "/MAM/FormV5";

            }
        }));
    }
    else {
        location.href = "/MAM/FormV5";
    }
}


function AttachmentDialog() {
    $("#AttachmentModal").modal('show');
    InitAjaxLoading();
    $.ajax({
        url: '/MAM/GetAttachment',
        type: 'POST',
        success: function (data) {
            $("#divAttachment").html(data);
            HideAjaxLoading();
        },
        error: function (data) {
            alert(data.responseText);
        }

    });

    return true;
}


function UploadModalClose() {
    document.getElementById("files").disabled = true;
    document.getElementById("FormBrowseBtn").disabled = true;
    document.getElementById("btnImageUpload").disabled = true;
    $("#photolist").empty();
    $('#AttachmentModal').modal('hide');
}