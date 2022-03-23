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


    $("#FormW1_ServPropName").chosen('destroy');
    $("#FormW1_ServPropName").prop("disabled", true);

    $("#ddlServiceProvider").chosen('destroy');
    $("#ddlServiceProvider").prop("disabled", true);

    OnAmtChange();

});


//FormWD Region
function OnWDUseridChange(tis) {

    var ctrl = $(tis);
    if (ctrl.val() != null)
        $('#ddlWDUserid').val(ctrl.val());
    $('#FormWD_UseridIssu').val($('#ddlWDUserid').val());

    if ($('#ddlWDUserid').val() != null && $('#ddlWDUserid').val() != "") {
        $("#FormWD_UsernameIssu").val(ctrl.find("option:selected").attr("Item1"));
        $("#FormWD_DesignationIssu").val(ctrl.find("option:selected").attr("Item2"));
        $("#FormWD_OfficeIssu").val(ctrl.find("option:selected").attr("Item3"));
        if ($('#ddlWDUserid').val() == "99999999") {
            $("#FormWD_UsernameIssu").removeAttr("readonly");
            $("#FormWD_DesignationIssu").removeAttr("readonly");
            $("#FormWD_OfficeIssu").removeAttr("readonly");
        } else {
            $("#FormWD_UsernameIssu").attr("readonly", "true");
            $("#FormWD_DesignationIssu").attr("readonly", "true");
            $("#FormWD_OfficeIssu").attr("readonly", "true");
        }

        $('#FormWD_SignIssu').prop('checked', true);
    }
    else {
        $("#FormWD_UsernameIssu").val('');
        $("#FormWD_DesignationIssu").val('');
        $("#FormWD_OfficeIssu").val('');
        $('#FormWD_SignIssu').prop('checked', false);
    }

}

function AddClauseData() {

    if (ValidatePage('#divClause')) {
        var tbl = document.getElementById('tblClause');

        length = tbl.rows.length;
        if (length > 3) {
            app.ShowErrorMessage("Only three rows allowed");
            return;
        }
        else {
            if ($("#txtReason").val() == "") {
                app.ShowErrorMessage("Reason Required");
                return;
            }
            if ($("#txtClause").val() == "") {
                app.ShowErrorMessage("Clause Required");
                return;
            }
            if ($("#txtExtension").val() == "") {
                app.ShowErrorMessage("Extension Required");
                return;
            }

        }

        var rowdata = tbl.insertRow(length);
        var cell1 = rowdata.insertCell(rowdata.cells.length);
        cell1.innerHTML = $("#txtReason").val();
        var cell2 = rowdata.insertCell(rowdata.cells.length);
        cell2.innerHTML = $("#txtClause").val();
        var cell3 = rowdata.insertCell(rowdata.cells.length);
        cell3.innerHTML = $("#txtExtension").val();
        var cell4 = rowdata.insertCell(rowdata.cells.length);
        cell4.innerHTML = " <span class='del-icon' onclick=\"Javascript:DeleteClauseRow(this);\"></span>"; /*"<i class='fa fa-trash-alt fa-lg cursor-pointer text-danger' onclick=\"Javascript:DeleteClauseRow(this," + length  + ");\" ></i>";*/

        $("#txtReason").val("");
        $("#txtClause").val("");
        $("#txtExtension").val("");
        $("#ClauseModal").modal("hide");
    }

}

function DeleteClauseRow(obj) {
    $(obj).closest('tr').remove();
}


function GetClauseDetails() {
    debugger
    var ClassDetails = [];
    var rows = $('#tblClause tbody >tr');
    var columns;
    for (var i = 0; i < rows.length; i++) {

        if (i != 0) {
            var ClassDetail = new Object();
            columns = $(rows[i]).find('td');
            for (var j = 0; j < columns.length; j++) {
                if (j == 0)
                    ClassDetail.Reason = $(columns[j]).html();
                else if (j == 1)
                    ClassDetail.Clause = $(columns[j]).html();
                else if (j == 2)
                    ClassDetail.ExtnPrd = $(columns[j]).html();
            }
            ClassDetails.push(ClassDetail);
        }
    }

    return ClassDetails;
}

function SaveWD(GroupName, SubmitType) {

    debugger
    $("#FormWD_SignIssu").removeClass("validate");
    $("#FormWDpage .validate").addClass("svalidate");
    $("#FormWDpage .svalidate").removeClass("validate");

    if (SubmitType != "") {
 
        var tbl = document.getElementById('tblClause');
        length = tbl.rows.length;
        if (length <= 1) {
            app.ShowErrorMessage("Atleast one clause is required");
            return;
        }

        $("#FormWDpage .svalidate").addClass("validate");

        if (SubmitType == "Submitted") {
            $("#FormWD_Status").val("Submitted");
            $("#FormWD_SubmitSts").val(true);
            $("#ddlWDUserid").addClass("validate");
            $("#FormWD_SignIssu").addClass("validate");
        }
    }
    else {
        $("#FormWD_Status").val("Saved");
        $("#FormWD_SubmitSts").val(false);
    }

    document.getElementById('ClauseDetails').value = JSON.stringify(GetClauseDetails());

    if (ValidatePage('#FormWDWNpage')) {
        InitAjaxLoading();
        $.post('/InstructedWorks/SaveFormWD', $("form").serialize(), function (data) {
            HideAjaxLoading();
            if (data == -1) {
                app.ShowErrorMessage(data.errorMessage);
            }
            else {
                if ($("#FormWD_PkRefNo").val() == 0 || $("#FormWD_PkRefNo").val() == "") {
                    $("#FormWD_PkRefNo").val(data);
                    $("#hdnWDPkRefNo").val(data);
                }

                if (SubmitType == "" || SubmitType == "Saved") {
                    app.ShowSuccessMessage('Saved Successfully', false);
                    location.href = "/InstructedWorks/Index";
                }
                else if (SubmitType == "Submitted") {
                    app.ShowSuccessMessage('Submitted Successfully', false);
                    location.href = "/InstructedWorks/Index";
                }

            }
        });
    }



}

function PrevCompDtValidation() {

    if ($("#FormWD_DtPervCompl").val() != "") {
        var value = new Date(formatDate($("#FormWD_DtPervCompl").val()));

        if ($("#FormWD_DtExtn").val() != "") {
            var ExtnDate = new Date(formatDate($("#FormWD_DtExtn").val()));
            if (value > ExtnDate) {
                app.ShowErrorMessage("Date of Completion should be greater than previously approved");
                $("#FormWD_DtPervCompl").val('');
                return;
            }
        }
    }
}


function CompDtValidation() {



    if ($("#FormWD_DtExtn").val() != "") {
        var value = new Date(formatDate($("#FormWD_DtExtn").val()));

        if ($("#FormWD_DtPervCompl").val() != "") {
            var PrevDate = new Date(formatDate($("#FormWD_DtPervCompl").val()));
            if (value < PrevDate) {
                app.ShowErrorMessage("Previously approved should be less than Date of Completion");
                $("#FormWD_DtExtn").val('');
                return;
            }
        }
    }
}


//FormWD RegionEnd


//FormWN Region
function OnWNUseridChange(tis) {

    var ctrl = $(tis);
    if (ctrl.val() != null)
        $('#ddlWNUserid').val(ctrl.val());
    $('#FormWN_UseridIssu').val($('#ddlWNUserid').val());

    if ($('#ddlWNUserid').val() != null && $('#ddlWNUserid').val() != "") {
        $("#FormWN_UsernameIssu").val(ctrl.find("option:selected").attr("Item1"));
        $("#FormWN_DesignationIssu").val(ctrl.find("option:selected").attr("Item2"));
        $("#FormWN_OfficeIssu").val(ctrl.find("option:selected").attr("Item3"));
        if ($('#ddlWNUserid').val() == "99999999") {
            $("#FormWN_UsernameIssu").removeAttr("readonly");
            $("#FormWN_DesignationIssu").removeAttr("readonly");
            $("#FormWN_OfficeIssu").removeAttr("readonly");
        } else {
            $("#FormWN_UsernameIssu").attr("readonly", "true");
            $("#FormWN_DesignationIssu").attr("readonly", "true");
            $("#FormWN_OfficeIssu").attr("readonly", "true");
        }

        $('#FormWN_SignIssu').prop('checked', true);
    }
    else {
        $("#FormWN_UsernameIssu").val('');
        $("#FormWN_DesignationIssu").val('');
        $("#FormWN_OfficeIssu").val('');
        $('#FormWN_SignIssu').prop('checked', false);
    }

}

function SaveWN(GroupName, SubmitType) {

    $("#FormWN_SignIssu").removeClass("validate");
    $("#FormWNpage .validate").addClass("svalidate");
    $("#FormWNpage .svalidate").removeClass("validate");

    if (SubmitType != "") {

        $("#FormWNpage .svalidate").addClass("validate");

        if (SubmitType == "Submitted") {
            $("#FormWN_Status").val("Submitted");
            $("#FormWN_SubmitSts").val(true);
            $("#ddlUseridReq").addClass("validate");
            $("#FormWN_SignIssu").addClass("validate");
        }
    }
    else {
        $("#FormWN_Status").val("Saved");
    }


    if (ValidatePage('#FormWNpage')) {
        InitAjaxLoading();
        $.post('/InstructedWorks/SaveFormWN', $("form").serialize(), function (data) {
            HideAjaxLoading();
            if (data == -1) {
                app.ShowErrorMessage(data.errorMessage);
            }
            else {
                if ($("#FormWN_PkRefNo").val() == 0 || $("#FormWN_PkRefNo").val() == "") {
                    $("#FormWN_PkRefNo").val(data);
                    $("#hdnWNPkRefNo").val(data);
                }

                if (SubmitType == "" || SubmitType == "Saved") {
                    app.ShowSuccessMessage('Saved Successfully', false);
                    location.href = "/InstructedWorks/Index";
                }
                else if (SubmitType == "Submitted") {
                    app.ShowSuccessMessage('Submitted Successfully', false);
                    location.href = "/InstructedWorks/Index";
                }

            }
        });
    }



}

 

function OnAmtChange() {
     
    if ($("#FormWN_LadAmt").val() != "") {

        var Amt = $("#FormWN_LadAmt").val().replace(/,/g, '');

        if (!$.isNumeric(Amt)) {
            app.ShowErrorMessage("Invalid Amount");
            return;
        }

        var val = Number(parseFloat(Amt).toFixed(2)).toLocaleString('en');
        if (val.indexOf('.') == -1) {
            val = val + ".00";
        }
        $("#FormWN_LadAmt").val(val);

        if (Amt > 999999999) {
            $("#FormWN_LadAmt").val("")
            app.ShowErrorMessage("Invalid Amount");
        }
    }
}

//FormWN RegionEnd


function formatDate(date) {
    var d = new Date(date),
        month = '' + (d.getMonth() + 1),
        day = '' + d.getDate(),
        year = d.getFullYear();

    if (month.length < 2) month = '0' + month;
    if (day.length < 2) day = '0' + day;

    return [year, month, day].join('-');
}


//Image

function GetImageList(id, form) {

    debugger
  
    var group = $("#FormADetAssetGrpCode option:selected").val();
    if (id && id > 0) {
        $("#fw1IWRefNo").val(id);
    }
    else {
        id = $("#fw1IWRefNo").val();
    }
    $.ajax({
        url: '/InstructedWorks/GetIWImageList',
        data: { id, assetgroup: group, form },
        type: 'POST',
        success: function (data) {
            $("#ViewPhoto").html(data);
            if ($("#hdnView").val() == "1")
                $("div.img-btns *").prop("disabled", true);
            if (form == "FormWDWN")
                $("#divFormType").show();
            else
                $("#divFormType").hide();

            if ($("#hdnFormWDStatus").val() == "Submitted" && $("#hdnFormWNStatus").val() == "Submitted") {
                $("#addAttachment").hide();
            }
        },
        error: function (data) {
            alert(data.responseText);
        }

    });

    return true;
}


function ClearWD() {
    $("#FormWD_IwWrksDeptId").val("").trigger("change").trigger("chosen:updated");
    $("#FormWD_CertificateDelay").val('');
    $("#FormWD_OurRefNo").val('');
    $("#FormWD_DtWd").val('');
    $("#FormWD_YourRefNo").val('');
    $("#FormWD_DtExtn").val('');

    var rows = $('#tblClause tbody >tr');
    for (var i = 0; i < rows.length; i++) {

        if (i != 0) {
            $(rows[i]).remove();
        }
    }
}

function ClearWN() {
    $("#FormWN_IwWrksDeptId").val("").trigger("change").trigger("chosen:updated");
    $("#FormWN_OurRefNo").val('');
    $("#FormWN_DtWn").val('');
    $("#FormWN_DtW2Initiation").val('');
    $("#FormWN_LadAmt").val('');
}

function GoBackWD() {

    if ($("#hdnView").val() == "0" || $("#hdnView").val() == "" || $("#FormWD_Status").val() == "" || $("#FormWD_Status").val() == "Saved") {
        if (app.Confirm("Are you sure you want to close the form?", function (e) {
            if (e) {
                location.href = "/InstructedWorks/Index";
            }
        }));
    }
    else
        location.href = "/InstructedWorks/Index";
}

function GoBackWN() {

    if ($("#hdnView").val() == "0" || $("#hdnView").val() == "" || $("#FormWN_Status").val() == "" || $("#FormWN_Status").val() == "Saved") {
        if (app.Confirm("Are you sure you want to close the form?", function (e) {
            if (e) {
                location.href = "/InstructedWorks/Index";
            }
        }));
    }
    else
        location.href = "/InstructedWorks/Index";
}

