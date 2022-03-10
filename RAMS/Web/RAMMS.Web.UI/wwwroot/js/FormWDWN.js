$(document).ready(function () {


    if ($("#hdnView").val() == "1") {
        $("#FormWDdatapage *").prop("disabled", true);
        $("#FormWDNdatapage *").prop("disabled", true);
      
        $("#ddlWDUserid").chosen('destroy');
        $("#ddlWDUserid").prop("disabled", true);
        $("#ddlWNUserid").chosen('destroy');
        $("#ddlWNUserid").prop("disabled", true);
      
        $("#btnSave").hide();
        $("#btnSubmit").hide();
        $("#addAttachment").hide();
        $("#btnBack").removeAttr("disabled");
    }

    $("#FormW1_ServPropName").chosen('destroy');
    $("#FormW1_ServPropName").prop("disabled", true);

    $("#ddlServiceProvider").chosen('destroy');
    $("#ddlServiceProvider").prop("disabled", true);

    
});


 //FormWD Region
function OnWDUseridChange(tis) {

    var ctrl = $(tis);
    $('#FormW1_UseridRep').val(ctrl.val());
    if (ctrl.val() != null && ctrl.val() != "") {
        $("#FormWD_UsernameIssu").val(ctrl.find("option:selected").attr("Item1"));
        $("#FormWD_DesignationIssu").val(ctrl.find("option:selected").attr("Item2"));
        $("#FormWD_OfficeIssu").val(ctrl.find("option:selected").attr("Item3"));
        if (ctrl.val() == "99999999") {
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

    var tbl = document.getElementById('tblClause');

    length = tbl.rows.length;
    if (length > 3) {
        app.ShowErrorMessage("Only three rows allowed");
        return;
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

function DeleteClauseRow(obj) {
    $(obj).closest('tr').remove();
}

 
function GetClauseDetails() {
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
    if (SubmitType != "") {

        $("#FormWDpage .svalidate").addClass("validate");

        if (SubmitType == "Submitted") {
            $("#FormWD_Status").val("Submitted");
            $("#FormWD_SubmitSts").val(true);
            $("#ddlUseridReq").addClass("validate");
        }
    }
    else {
        $("#FormWD_Status").val("Saved");
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

                $("#FormWD_PkRefNo").val(data);
                $("#hdnWDPkRefNo").val(data);

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
//FormWD RegionEnd


//FormWN Region
function OnWNUseridChange(tis) {

    var ctrl = $(tis);
    $('#FormW1_UseridRep').val(ctrl.val());
    if (ctrl.val() != null && ctrl.val() != "") {
        $("#FormWN_UsernameIssu").val(ctrl.find("option:selected").attr("Item1"));
        $("#FormWN_DesignationIssu").val(ctrl.find("option:selected").attr("Item2"));
        $("#FormWN_OfficeIssu").val(ctrl.find("option:selected").attr("Item3"));
        if (ctrl.val() == "99999999") {
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

     
    if (SubmitType != "") {

        $("#FormWNpage .svalidate").addClass("validate");

        if (SubmitType == "Submitted") {
            $("#FormWN_Status").val("Submitted");
            $("#FormWN_SubmitSts").val(true);
            $("#ddlUseridReq").addClass("validate");
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

                $("#FormWN_PkRefNo").val(data);
                $("#hdnWDPkRefNo").val(data);

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

//FormWN RegionEnd
