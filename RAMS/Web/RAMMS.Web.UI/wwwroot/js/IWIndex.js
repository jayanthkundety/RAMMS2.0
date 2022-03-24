
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
                if ($this.val().substring($this.val().indexOf('.')).length > 2) {
                    $this.val($this.val().substring(0, $this.val().indexOf('.') + 2));
                }
            }, 1);
        }

        if ((text.indexOf('.') != -1) &&
            (text.substring(text.indexOf('.')).length > 2) &&
            (event.which != 0 && event.which != 8) &&
            ($(this)[0].selectionStart >= text.length - 2)) {
            event.preventDefault();
        }
    });

    $("#txtSmartSearch").on("keypress", function (e) {
        if (e.which == 13) {
            $("#btnSearch").click();
        }
    });
});

//Grid
function clearHeaderSearch() {
    $("#txtSmartSearch").val('');
    $("#txtIWRefNo").val('');
    $("#txtCommenceDtFrom").val('');
    $("#txtCommenceDtTo").val('');
    $("#txtProjectTitle").val('');
    $("#txtStPercFrom").val('');
    $("#txtStPercTo").val('');
    $("#txtStatus").val("").trigger("change").trigger("chosen:updated");
    $("#txtRmu").val("").trigger("change").trigger("chosen:updated");
    $("#txtRoadCode").val("").trigger("change").trigger("chosen:updated");
    $("#txtDEYN").val("").trigger("change").trigger("chosen:updated");
    $("#txtFormType").val("").trigger("change").trigger("chosen:updated");
    $("#txtTECM").val("").trigger("change").trigger("chosen:updated");
    $("#txtFECM").val("").trigger("change").trigger("chosen:updated");
    $("#btnSearch").click();
}

function PercentangeChange(ctrl) {
    if (parseFloat($(ctrl).val()) > 100) {
        app.ShowErrorMessage("Percentange should not be greater than 100");
        $(ctrl).val('');
    }
}

function changeRMU(obj) {
    var ctrl = $(obj);
    if (ctrl.val() != null && ctrl.val() != "") {
        var name = ctrl.find("option:selected").text();
        name = name.split('-')[1];
        $("#txtRmuName").val(name);

        $.ajax({
            url: '/InstructedWorks/GetRoadCodeByRMU',
            dataType: 'JSON',
            data: { rmu: ctrl.val() },
            type: 'Post',
            success: function (data) {
                if (data != null) {
                    $('#txtRoadCode').empty();
                    $('#txtRoadCode')
                        .append($("<option></option>")
                            .attr("value", "")
                            .text("Select Road Code"));
                    $.each(data, function (key, value) {
                        $('#txtRoadCode')
                            .append($("<option></option>")
                                .attr("item1", value.item1)
                                //.attr("fromkm", value.fromKm)
                                //.attr("fromm", value.fromM)
                                .attr("value", value.value)
                                .text(value.text));
                    });
                    $("#txtRoadCode").val($('#frmW2RoadCode').val());
                    $('#txtRoadCode').trigger("chosen:updated")
                    $("#txtRoadCode").trigger("change");
                }
            },
            error: function (data) {

                console.error(data);
            }
        });
    }
    else {
        $("#txtRmuName").val('');
    }
}

function OnRoadChange(tis) {
    var ctrl = $(tis);
    if (ctrl.val() != null && ctrl.val() != "") {
        $("#txtRoadName").val(ctrl.find("option:selected").attr("Item1"));
    }
    else {
        $("#txtRoadName").val('');
    }

}

function getGridSelectedData() {
    var table = $('#FormIWGridView').DataTable();
    var selectedIndex = table.$(".selected", { "page": "all" });
    return table.row(selectedIndex).data();
}

function GetFormIDByName(formName, form, action) {
    var value = -1;
    var daa = getGridSelectedData();
    if ((daa == null || typeof daa == "undefined") && typeof action != "undefined" && typeof form != "undefined") {
        app.ShowErrorMessage("Kindly select a row to " + action + " the " + form.toUpperCase());
        return value;
    }

    switch (formName) {
        case "w1":
            value = daa.w1RefNo;
            break;
        case "w2":
            value = daa.w2RefNo;
            break;
        case "wc":
            value = daa.wcRefNo;
            break;
        case "wg":
            value = daa.wgRefNo;
            break;
        case "wd":
            value = daa.wdRefNo;
            break;
        case "wn":
            value = daa.wnRefNo;
            break;
        case "w1Status":
            value = daa.w1Status;
            break;
        case "w1SubStatus":
            value = daa.w1SubStatus;
            break;
        case "w2Status":
            value = daa.w2Status;
            break;
        case "w2SubStatus":
            value = daa.w2SubStatus;
            break;
        case "wcStatus":
            value = daa.wcStatus;
            break;
        case "wcSubStatus":
            value = daa.wcSubStatus;
            break;
        case "wgStatus":
            value = daa.wgStatus;
            break;
        case "wgSubStatus":
            value = daa.wgSubStatus;
            break;
        case "wdStatus":
            value = daa.wdStatus;
            break;
        case "wdSubStatus":
            value = daa.wdSubStatus;
            break;
        case "wnStatus":
            value = daa.wnStatus;
            break;
        case "wnSubStatus":
            value = daa.wnSubStatus;
            break;
        default:
            value = daa.w1RefNo;
    }
    return value == null ? -2 : value;
}

//Form W1
function OpenFormW1(mode) {

    var id = 0;

    if (mode == "Add") {
        if (!checkAction("FormW1", 'Add')) return;
        url = "/InstructedWorks/AddFormW1";
    }
    else if (mode == "Edit") {

        id = GetFormIDByName("w1", "Form W1", "View / Edit");
        if (id == '-1') return;

        if (id == -2) {
            app.ShowErrorMessage("Form W1 is not created");
            return;
        }

        var w1status = GetFormIDByName("w1Status");

        var isEdit = false, isApprove = false, isView = false;

        if (checkAction("FormW1", 'Edit',false)) {
            isEdit = true;
        }
        if (w1status == 'Submitted' && checkAction("FormW1", 'Approve', false)) {
            isApprove = true;
        }

        if (checkAction("FormW1", 'View')) {
            isView = true;
        }

        if (!isEdit && !isApprove && !isView && $("#hdnAllView").val() == "0") return;
        if ($("#hdnAllView").val() == "1") {
            view = 1;
        }

        url = "/InstructedWorks/EditFormW1?id=" + id;
    }
    InitAjaxLoading();
    $.ajax({
        url: url,
        type: 'POST',
        success: function (data) {
            $("#formW1").modal('show');
            $("#formW1Content").html(data);
            HideAjaxLoading();
        },
        error: function (data) {
            alert(data.responseText);
            HideAjaxLoading();
        }

    });
}

function DeleteW1() {
    if (!checkAction("FormW1", 'Delete')) return;
    var id = GetFormIDByName("w1", "Form W1", "delete");
    if (id == -1) return;

    var w1status = GetFormIDByName("w1Status");

    if (w1status == "Approved") {
        app.ShowErrorMessage("Unable to delete as form is approved.")
        return;
    }

    if (app.Confirm("Are you sure you want to delete the record?", function (e) {
        if (e) {

            InitAjaxLoading();
            $.ajax({
                url: "/InstructedWorks/DeleteW1",
                data: { id },
                type: 'POST',
                success: function (data) {
                    HideAjaxLoading();
                    if (data > 0) {
                        $("body").removeClass("loading");
                        app.ShowSuccessMessage("Deleted Successfully", false);
                        FormIWGridRefresh();
                    }
                    else {
                        app.ShowWarningMessage("Please try again later.", false);
                        $("body").removeClass("loading");
                    }
                }
            });
        }
    }));
}

function PrintFormW1() {
    var id = GetFormIDByName("w1", "Form W1", "print");
    if (id == -1) return;
    window.location.href = '/download/PrintForm?id=' + id + "&formname=FormW1";
}

//Form W2

function OpenFormW2(mode) {
    var url = '';

    var view = 0;
    if (mode == 'Add') {
        if (!checkAction("FormW2", 'Add')) return;
        var id = GetFormIDByName("w1", "Form W2", "Add");

        if (id == -1) return;
        var w1status = GetFormIDByName("w1Status");
        var w2status = GetFormIDByName("w2Status");
        var w2id = GetFormIDByName("w2");
        if (w1status == "Approved" && w2id <= 0)
            url = '/InstructedWorks/AddFormW2?id=' + id;
        else if (w1status == "Approved" && w2status != "")
            app.ShowErrorMessage("A Form W2 is already created");
        else
            app.ShowErrorMessage("Form W1 is not approved");


    }
    else {

        id = GetFormIDByName("w2", "Form W2", "View / Edit");
        if (id == -1) return;

        if (id == -2) {
            app.ShowErrorMessage("Form W2 is not created");
            return;
        }
        var w2status = GetFormIDByName("w2Status");
        var w2substatus = GetFormIDByName("w2SubStatus");
        if (w2substatus == true) {
            view = 1;
        }

        var isEdit = false, isApprove = false, isView = false;

        if (checkAction("FormW2", 'Edit', false)) {
            isEdit = true;
        }
        if (w1status == 'Submitted' && checkAction("FormW2", 'Approve', false)) {
            isApprove = true;
        }

        if (checkAction("FormW2", 'View')) {
            isView = true;
        }

        if ((!isEdit && !isApprove && !isView) && $("#hdnAllView").val() == "0") return;
        if ($("#hdnAllView").val() == "1") {
            view = 1;
        }

        url = '/InstructedWorks/EditFormW2?id=' + id + "&view=" + view;
    }
    InitAjaxLoading();
    if (url == '') {
        HideAjaxLoading();
        return;
    }

    $.ajax({
        url: url,
        type: 'POST',
        success: function (data) {
            $("#formW2").modal('show');
            $("#formW2Content").html(data);
            HideAjaxLoading();
        },
        error: function (data) {
            app.ShowErrorMessage(data.responseText);
            HideAjaxLoading();
        }

    });

    return true;
}

function DeleteW2() {
    if (!checkAction("FormW2", 'Delete')) return;
    var id = GetFormIDByName("w2", "Form W2", "delete");
    if (id == -1) return;
    if (id == -2) {
        app.ShowErrorMessage("Form W2 is not created");
        return;
    }
    var w2status = GetFormIDByName("w2Status");
    if (w2status == "Received") {
        app.ShowErrorMessage("Unable to delete as form is received.")
        return;
    }

    if (app.Confirm("Are you sure you want to delete the record?", function (e) {
        if (e) {
            var id = GetFormIDByName("w2");
            InitAjaxLoading();
            $.ajax({
                url: "/InstructedWorks/DeleteW2",
                data: { id },
                type: 'POST',
                success: function (data) {
                    HideAjaxLoading();
                    if (data > 0) {
                        $("body").removeClass("loading");
                        app.ShowSuccessMessage("Deleted Successfully", false);
                        FormIWGridRefresh();
                    }
                    else {
                        app.ShowWarningMessage("Please try again later.", false);
                        $("body").removeClass("loading");
                    }
                }
            });
        }
    }));
}

function PrintFormW2() {
    if (!checkAction("FormW2", 'Print')) return;
    var id = GetFormIDByName("w2", "Form W2", "print");
    if (id == -1) return;
    if (id == -2) {
        app.ShowErrorMessage("Form W2 is not created");
        return;
    }
    window.location.href = '/download/PrintForm?id=' + id + "&formname=FormW2";
}

//Form WC & WG
function OpenFormWCWG(mode, form) {
    var w1id, w2id, wcid, wgid, w2status;
    if (mode == "Add") {
        if (!checkAction(form, 'Add')) return;
        var w1id = GetFormIDByName("w1", form, "Add");

        if (w1id == -1) return;

        var w2id = GetFormIDByName("w2", form, "Add");
        if (w2id == -1) return;


        var w2status = GetFormIDByName("w2Status");

        if (w2status != "Received") {
            app.ShowErrorMessage("Form W2 is not received");
            return;
        }

       

        var wnstatus = GetFormIDByName("wnStatus");
        var wdstatus = GetFormIDByName("wdStatus");

        if (wnstatus != "-2" || wdstatus == "Saved" ) {
            app.ShowErrorMessage(form + " cannot be created");
            return;
        }


        wcid = GetFormIDByName("wc");
        wgid = GetFormIDByName("wg");
        if (form == "FormWC") {

            if (wcid == -1) return;

            if (wcid > 0) {
                app.ShowErrorMessage("Form WC already created");
                return;
            }
        }
        else if (form == "FormWG") {
            wcsubstatus = GetFormIDByName("wcSubStatus");
            if ((wgid == -2 || wgid > 0) && wcsubstatus == false) {
                app.ShowErrorMessage("Form WC is not submitted");
                return;
            }

            if (wgid > 0) {
                app.ShowErrorMessage("Form WG is already created");
                return;
            }

        }
        url = "/InstructedWorks/OpenWCWG?w1id=" + w1id + "&w2id=" + w2id + "&wcid=" + wcid + "&form=" + form;

    }
    else if (mode == "Edit") {
        var view = 0;
        if (form == "FormWC") {
            wcid = GetFormIDByName("wc", "Form WC", "View / Edit");
            if (wcid == -1) return;
            if (wcid == -2) {
                app.ShowErrorMessage("Form WC is not created");
                return;
            }
        } else if (form == "FormWG") {
            wgid = GetFormIDByName("wg", "Form WG", "View / Edit");
            if (wgid == -1) return;
            if (wgid == -2) {
                app.ShowErrorMessage("Form WG is not created");
                return;
            }
        }
        wcid = GetFormIDByName("wc");
        wgid = GetFormIDByName("wg");

        if (wcid == -2) wcid = 0;
        if (wgid == -2) wgid = 0;

        var wcsubstatus = GetFormIDByName("wcSubStatus");
        var wgsubstatus = GetFormIDByName("wgSubStatus");

        if (form == 'FormWC' && wcsubstatus == "true") {
            view = 1;
        }

        if (form == 'FormWG' && wgsubstatus == "true") {
            view = 1;
        }

        if (!checkAction(form, view > 0 ? 'View' : 'Edit') && $("#hdnAllView").val() == "0") return;
        if ($("#hdnAllView").val() == "1") {
            view = 1;
        }
        //if (wcid > 0 && wcstatus != "Submitted" && wgid == -2 && form == "FormWG") {
        //    app.ShowErrorMessage("Form WC is not submitted");
        //    return;
        //}

        var w2id = GetFormIDByName("w2");
        url = "/InstructedWorks/EditFormWCWG?wcid=" + wcid + "&wgid=" + wgid + "&w2id=" + w2id + "&view=" + view + "&form=" + form;
    }

    InitAjaxLoading();
    $.ajax({
        url: url,
        type: 'POST',
        success: function (data) {
            $("#formWCWG").modal('show');
            $("#formWCWGContent").html(data);
            HideAjaxLoading();
        },
        error: function (data) {
            alert(data.responseText);
            HideAjaxLoading();
        }

    });
}

function DeleteWCWG(form) {
    var id, url;
    if (!checkAction(form, 'Delete')) return;
    if (form == "FormWC")
        id = GetFormIDByName("wc", "Form WC", "delete");
    else
        id = GetFormIDByName("wg", "Form WG", "delete");

    if (id == -1) return;
    if (id == -2) {
        app.ShowErrorMessage(form + " is not created");
        return;
    }

    var wcid = GetFormIDByName("wc");
    var wgid = GetFormIDByName("wg");

    if (form == "FormWC" && wgid > 0) {
        app.ShowErrorMessage(form + " cannot be deleted, first delete Form WG ");
        return;
    }

    if (app.Confirm("Are you sure you want to delete the record?", function (e) {
        if (e) {
            if (form == "FormWC")
                url = "/InstructedWorks/DeleteWC";
            else
                url = "/InstructedWorks/DeleteWG";


            InitAjaxLoading();
            $.ajax({
                url: url,
                data: { id },
                type: 'POST',
                success: function (data) {
                    HideAjaxLoading();
                    debugger;
                    if (data.id.result > 0) {
                        $("body").removeClass("loading");
                        app.ShowSuccessMessage("Deleted Successfully", false);
                        FormIWGridRefresh();
                    }
                    else {
                        app.ShowWarningMessage("Please try again later.", false);
                        $("body").removeClass("loading");
                    }
                }
            });
        }
    }));
}

function PrintFormWCWG(form) {
    var id;
    if (!checkAction(form, 'Print')) return;
    if (form == "FormWC")
        id = GetFormIDByName("wc", "Form WC", "print");
    else
        id = GetFormIDByName("wg", "Form WG", "print");

    if (id == -1) return;
    if (id == -2) {
        app.ShowErrorMessage(form + " is not created");
        return;
    }

    if (form == 'FormWC')
        window.location.href = '/download/PrintForm?id=' + id + "&formname=FormWC";
    else
        window.location.href = '/download/PrintForm?id=' + id + "&formname=FormWG";
}

//Form WD & WN
function OpenFormWDWN(mode, form) {
    var w1id, w2id, wdid, wnid, w2status;
    if (mode == "Add") {
        if (!checkAction(form, 'Add')) return;
        w1id = GetFormIDByName("w1", form, "Add");
        if (w1id == -1) return;

        w2id = GetFormIDByName("w2", form, "Add");
        if (w2id == -1) return;

        w2status = GetFormIDByName("w2Status");

        if (w2status != "Received") {
            app.ShowErrorMessage("Form W2 is not received");
            return;
        }

        if (form == "FormWD") {
            wdid = GetFormIDByName("wd");
            var wnstatus = GetFormIDByName("wnStatus");
            if (wdid == -1) return;

            if (wdid > 0) {
                app.ShowErrorMessage("Form WD already created");
                return;
            }

            if (wnstatus == "Saved" || wnstatus == "Submitted") {
                app.ShowErrorMessage("Form WD cannot be created");
                return;
            }
        }
        else if (form == "FormWN") {
            wdid = GetFormIDByName("wd");
            wdsubstatus = GetFormIDByName("wdSubStatus");

            if (wdid > 0 && wdid != -2 && wdsubstatus == false) {
                app.ShowErrorMessage("Form WD is not submitted");
                return;
            }

            wnid = GetFormIDByName("wn");
            if (wnid > 0) {
                app.ShowErrorMessage("Form WN already created");
                return;
            }
        }
        url = "/InstructedWorks/OpenWDWN";
    }
    else if (mode == "Edit") {
        var view = 0;
        if (form == "FormWD") {
            wdid = GetFormIDByName("wd", "Form WD", "View / Edit");
            if (wdid == -1) return;
            if (wdid == -2) {
                app.ShowErrorMessage("Form WD not created");
                return;
            }
        } else if (form == "FormWN") {
            wnid = GetFormIDByName("wn", "Form WN", "View / Edit");
            if (wnid == -1) return;
            if (wnid == -2) {
                app.ShowErrorMessage("Form WN not created");
                return;
            }
        }
        wdid = GetFormIDByName("wd");
        wnid = GetFormIDByName("wn");

        if (wnid == -2) wnid = 0;
        if (wdid == -2) wdid = 0;

        var wdsubstatus = GetFormIDByName("wdSubStatus");
        var wnsubstatus = GetFormIDByName("wnSubStatus");

        if (form == 'FormWD' && wdsubstatus == "true") {
            view = 1;
        }

        if (form == 'FormWN' && wnsubstatus == "true") {
            view = 1;
        }

        if (!checkAction(form, view > 0 ? 'View' : 'Edit') && $("#hdnAllView").val() == "0") return;
        if ($("#hdnAllView").val() == "1") {
            view = 1;
        }
        w1id = GetFormIDByName("w1");
        w2id = GetFormIDByName("w2");
        url = "/InstructedWorks/EditFormWDWN";
    }
    InitAjaxLoading();
    $.ajax({
        url: url,
        type: 'POST',
        data: { wdid, wnid, w1id, w2id, view, form },
        success: function (data) {
            $("#formWDWN").modal('show');
            $("#formWDWNContent").html(data);
            HideAjaxLoading();
        },
        error: function (data) {
            alert(data.responseText);
            HideAjaxLoading();
        }

    });
}

function DeleteWDWN(form) {
    var id, url;
    if (!checkAction(form, 'Delete')) return;
    if (form == "FormWD")
        id = GetFormIDByName("wd", "Form WD", "delete");
    else
        id = GetFormIDByName("wn", "Form WN", "delete");

    if (id == -1) return;

    if (id == -2) {
        app.ShowErrorMessage(form + " is not created");
        return;
    }

    if (app.Confirm("Are you sure you want to delete the record?", function (e) {
        if (e) {
            if (form == "FormWD")
                url = "/InstructedWorks/DeleteWD";
            else
                url = "/InstructedWorks/DeleteWN";

            InitAjaxLoading();
            $.ajax({
                url: url,
                data: { id },
                type: 'POST',
                success: function (data) {
                    HideAjaxLoading();
                    debugger;
                    if (data.id.result > 0) {
                        $("body").removeClass("loading");
                        app.ShowSuccessMessage("Deleted Successfully", false);
                        FormIWGridRefresh();
                    }
                    else {
                        app.ShowWarningMessage("Please try again later.", false);
                        $("body").removeClass("loading");
                    }
                }
            });
        }
    }));
}

function PrintFormWDWN(form) {
    var id;
    if (!checkAction(form, 'Print')) return;
    if (form == "FormWD")
        id = GetFormIDByName("wd", "Form WD", "print");
    else
        id = GetFormIDByName("wn", "Form WN", "print");

    if (id == -1) return;
    if (id == -2) {
        app.ShowErrorMessage(form + " is not created");
        return;
    }
    if (form == 'FormWD')
        window.location.href = '/download/PrintForm?id=' + id + "&formname=FormWD";
    else
        window.location.href = '/download/PrintForm?id=' + id + "&formname=FormWN";
}


//Form Rights
function GetRightsByForm(form) {

    var iwRoles = JSON.parse($("#hdnHasRights").val());

    for (var i = 0; i <= iwRoles.length; i++) {

        if (iwRoles[i][0].MfrModFormName == form) {
            var AllRoles = GetRightsByRole(iwRoles[i]);
            return AllRoles;
        }
    }
}

function GetRightsByRole(iwRights) {
    var iwroles = [];
    var roles = JSON.parse($("#hdnRole").val());
    for (var i = 0; i < iwRights.length; i++) {
        for (var j = 0; j < roles.length; j++) {
            if (iwRights[i].MfrGroupName == roles[j]) {
                iwroles.push(iwRights[i]);
            }
        }
    }
    return iwroles;
}

function hasRights(form, action) {
    var AllRoles = GetRightsByForm(form);
    for (var i = 0; i < AllRoles.length; i++) {
        if (AllRoles[i].MfrCanAdd && action == "Add")
            return true;

        if (AllRoles[i].MfrCanEdit && action == "Edit")
            return true;

        if (AllRoles[i].MfrCanView && action == "View")
            return true;

        if (AllRoles[i].MfrCanDelete && action == "Delete")
            return true;

        if (AllRoles[i].MfrCanPrint && action == "Print")
            return true;

        if (AllRoles[i].MfrCanApprove && action == "Approve")
            return true;
    }
    return false

}

function checkAction(form, action, alert = true) {
    //if (!hasRights(form, action) && alert) {
    //    app.ShowErrorMessage("User is not allowed to " + action + " " + form)
    //    return false;
    //}
    var isAdd = $("#hdnAdd").val();
    var isDelete = $("#hdnDelete").val();
    var isModify = $("#hdnModify").val();

    switch (action) {
        case "Add":
            if (isAdd == "") return true;
            break;
        case "Edit":
            if (isModify == "") {
                return true;
            } else {
                $("#hdnAllView").val(1);
                return false;
            }
            break;
        case "Delete":
            if (isDelete == "") return true;
            break;
        case "Print":
            return true;
            break;
        default:
            return false;
            break;
    }
    app.ShowErrorMessage("User is not allowed to " + action + " " + form)
    return false;
}
