
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

function DeleteW1() {
    if (!checkAction("FormW1", 'Delete')) return;
    if (app.Confirm("Are you sure you want to delete the record?", function (e) {
        if (e) {
            var id = GetFormIDByName("w1");
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

function DeleteW2() {
    if (!checkAction("FormW2", 'Delete')) return;
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

function DeleteWCWG(form) {
    var id, url;
    if (!checkAction(form, 'Delete')) return;
    if (app.Confirm("Are you sure you want to delete the record?", function (e) {
        if (e) {
            if (form == "FormWC") {
                id = GetFormIDByName("wc");
                url = "/InstructedWorks/DeleteWC";
            }
            else {
                id = GetFormIDByName("wg");
                url = "/InstructedWorks/DeleteWG";
            }

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

function DeleteWDWN(form) {
    var id, url;
    if (!checkAction(form, 'Delete')) return;
    if (app.Confirm("Are you sure you want to delete the record?", function (e) {
        if (e) {
            if (form == "FormWD") {
                id = GetFormIDByName("wd");
                url = "/InstructedWorks/DeleteWD";
            }
            else {
                id = GetFormIDByName("wn");
                url = "/InstructedWorks/DeleteWN";
            }

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

function getGridSelectedData() {
    var table = $('#FormIWGridView').DataTable();
    var selectedIndex = table.$(".selected", { "page": "all" });
    return table.row(selectedIndex).data();
}

function GetFormIDByName(formName) {
    var value = -1;
    var daa = getGridSelectedData();
    if (daa == null || typeof daa == "undefined") {
        app.ShowErrorMessage("Kindly select a row to open the " + formName.toUpperCase() + " Form");
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
    return value;
}

function OpenFormW1(mode) {

    var SelectedIWGridId = 0;

    if (mode == "Add") {
        if (!checkAction("FormW1", 'Add')) return;
        url = "/InstructedWorks/AddFormW1";
    }
    else if (mode == "Edit") {
        if (!checkAction("FormW1", 'Edit')) return;
        SelectedIWGridId = GetFormIDByName("w1");
        if (SelectedIWGridId == null || SelectedIWGridId == '-1') return;
        url = "/InstructedWorks/EditFormW1?id=" + SelectedIWGridId;
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

function OpenFormW2(mode) {
    var url = '';

    var view = 0;
    if (mode == 'Add') {
        if (!checkAction("FormW2", 'Add')) return;
        var id = GetFormIDByName("w1");

        if (id != -1) {
            var w1status = GetFormIDByName("w1Status");
            var w2status = GetFormIDByName("w2Status");
            if (w1status == "Approved" && w2status == "") {
                url = '/InstructedWorks/AddFormW2?id=' + id;
            }
            else if (w1status == "Approved" && w2status != "") {
                app.ShowErrorMessage("A Form W2 is already created");
            } else {

                app.ShowErrorMessage("Form W1 is not approved");
            }
        }
    }
    else {
        //var w2status = GetFormIDByName("w2Status");
        var w2substatus = GetFormIDByName("w2SubStatus");

        if (w2substatus == true) {
            view = 1;
        }

        if (!checkAction("FormW2", view > 0 ? 'View' : 'Edit')) return;
        id = GetFormIDByName("w2");
        if (id > 0 && id != null)
            url = '/InstructedWorks/EditFormW2?id=' + id + (view > 0 ? "&view=1" : "");
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

function OpenFormWDWN(mode) {
    var W1Id = GetFormIDByName("w1");
    var W2Id = GetFormIDByName("w2");
    var Wdid = GetFormIDByName("wd");
    var Wnid = GetFormIDByName("wn");
    var wdsubstatus = GetFormIDByName("wdsubstatus");
    var wnsubstatus = GetFormIDByName("wnsubstatus");

    var view;

    if (form == 'FormWD' && wdsubstatus == "true") {
        view = 1;
    }

    if (form == 'FormWN' && wnsubstatus == "true") {
        view = 1;
    }

    //EditFormWDWN(int Wdid, int Wnid, int W1Id, int W2Id, int View)
    if (mode == "Add") {
        if (!checkAction(form, 'Add') && !wdsubstatus) return;
        url = "/InstructedWorks/OpenWDWN";
    }
    else if (mode == "Edit") {
        if (!checkAction(form, 'Edit') && !wnsubstatus) return;
        url = "/InstructedWorks/EditFormWDWN";
    }
    InitAjaxLoading();
    $.ajax({
        url: url,
        type: 'POST',
        data: { Wdid, Wnid, W1Id, W2Id, view },
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
    }
    return false

}

function checkAction(form, action) {
    if (!hasRights(form, action)) {
        app.ShowErrorMessage("User is not allowed to " + action + " form")
        return false;
    }
    return true;
}

function OpenFormWCWG(mode, form) {
    if (mode == "Add") {
        if (!checkAction(form, 'Add')) return;
        var w1id = GetFormIDByName("w1");
        var w2id = GetFormIDByName("w2");
        url = "/InstructedWorks/OpenWCWG?w1id=" + w1id + "&w2id=" + w2id;
    }
    else if (mode == "Edit") {
        var wcsubstatus = GetFormIDByName("wcsubstatus");
        var wgsubstatus = GetFormIDByName("wgsubstatus");
        var view = 0;
        if (form == 'FormWC' && wcsubstatus == "true") {
            view = 1;
        }

        if (form == 'FormWG' && wgsubstatus == "true") {
            view = 1;
        }
        if (!checkAction(form, view > 0 ? 'View' :'Edit')) return;
        var wcid = GetFormIDByName("wc");
        var wgid = GetFormIDByName("wg");
        var w2id = GetFormIDByName("w2");
        url = "/InstructedWorks/EditFormWCWG?wcid=" + wcid + "&wgid=" + wgid + "&w2id=" + w2id + (view > 0 && "&view=1");
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

function PrintFormW1() {
    var id = GetFormIDByName("w1");
    window.location.href = '/download/PrintForm?id=' + id + "&formname=FormW1";
}

function PrintFormW2() {
    var id = GetFormIDByName("w2");
    window.location.href = '/download/PrintForm?id=' + id + "&formname=FormW2";
}

function PrintFormWCWG(form) {
    if (form == 'WC') {
        var id = GetFormIDByName("wc");
        window.location.href = '/download/PrintForm?id=' + id + "&formname=FormWC";
    }
    else {
        var id = GetFormIDByName("wg");
        window.location.href = '/download/PrintForm?id=' + id + "&formname=FormWG";
    }
}

function PrintFormWDWN(form) {
    if (form == 'WD') {
        var id = GetFormIDByName("wd");
        window.location.href = '/download/PrintForm?id=' + id + "&formname=FormWD";
    }
    else {
        var id = GetFormIDByName("wn");
        window.location.href = '/download/PrintForm?id=' + id + "&formname=FormWN";
    }
}


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