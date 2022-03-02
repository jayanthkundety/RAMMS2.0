$(document).ready(function () {

    $("#.FormW1_ServPropName").chosen('destroy');
    $("#.FormW1_ServPropName").prop("disabled", true);
});


function OnUseridChange(tis) {

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
    if (length > 4) {
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
    cell4.innerHTML = " <span class='del-icon' onclick=\"Javascript:DeleteClauseRow(this," + length + ");\"></span>"; /*"<i class='fa fa-trash-alt fa-lg cursor-pointer text-danger' onclick=\"Javascript:DeleteClauseRow(this," + length  + ");\" ></i>";*/

    $("#txtReason").val("");
    $("#txtClause").val("");
    $("#txtExtension").val("");

}

function DeleteClauseRow(obj, Row) {

}