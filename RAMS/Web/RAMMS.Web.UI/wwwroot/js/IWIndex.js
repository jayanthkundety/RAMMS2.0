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
    var id,url;
    if (app.Confirm("Are you sure you want to delete the record?", function (e) {
        if (e) {
            if (form == "WC") {
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

function OpenFormW1(mode, view) {

    var SelectedIWGridId = 0;

    if (mode == "Add") {
        url = "/InstructedWorks/AddFormW1";
    }
    else if (mode == "Edit") {

        SelectedIWGridId = GetFormIDByName("w1");
        url = "/InstructedWorks/EditFormW1?id=" + SelectedIWGridId;
    }
    else if (mode == "View") {
        SelectedIWGridId = GetFormIDByName("w1");
        url = "/InstructedWorks/EditFormW1?id=" + SelectedIWGridId + "&View=1";
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

function OpenFormW2(mode, view) {

    InitAjaxLoading();
    var url = '';
    var id = "";

    if (mode == 'Add') {
        id = GetFormIDByName("w1");
        if (id > 0 && id != null) {
            url = '/InstructedWorks/AddFormW2?id=' + id;
        }

    }
    else {
        id = GetFormIDByName("w2");
        if (id > 0 && id != null)
            url = '/InstructedWorks/EditFormW2?id=' + id + (view == 1 ? '&view=1' : '');
    }

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

function OpenFormWDWN(mode, view) {
    var W1Id = GetFormIDByName("w1");
    var W2Id = GetFormIDByName("w2");
    var Wdid = GetFormIDByName("wd");
    var Wnid = GetFormIDByName("wn");
    var view;
    //EditFormWDWN(int Wdid, int Wnid, int W1Id, int W2Id, int View)
    if (mode == "Add") {
        url = "/InstructedWorks/OpenWDWN";
    }
    else if (mode == "Edit") {
        url = "/InstructedWorks/EditFormWDWN";
    }
    else if (mode == "View") {
        view = 1;
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

function checkAction(form) {

    var si = getGridSelectedData();
    if (form == 'w2') {

        if (si == null) {
            $("#w2Add").hide();
            $("#w2Edit").hide();
            $("#w2View").hide();
            $("#w2Print").hide();
            $("#w2Delete").hide();
            app.ShowErrorMessage("Kindly select a row to open to Add/Edit W2 Form");
            return;
        }
        var id = GetFormIDByName("w2");
        var w1status = GetFormIDByName("w1Status");
        var w2status = GetFormIDByName("w2Status");
        var w2substatus = GetFormIDByName("w2SubStatus");

        if (id == "-1" || id == null) $("#w2Delete").hide();

        if ((id == "-1" || id == null) && (w1status == "Approved")) {
            $("#w2Add").show();
            $("#w2Edit").hide();
            $("#w2View").hide();
            $("#w2Print").hide();
            $("#w2Delete").hide();
        }
        else if (id != "-1" && id != null) {
            if (w2status == "Saved" || w2status == "Submitted" || w2status == "Rejected")
                $("#w2Delete").show();
            else
                $("#w2Delete").hide();

            if (w2status == "Approved") {
                $("#w2Add").hide();
                $("#w2Edit").hide();
                $("#w2View").show();
                $("#w2Print").show();
                return;
            }
            else if (w2status == "Rejected" || w2status == "Saved") {
                $("#w2Add").hide();
                $("#w2Edit").show();
                $("#w2View").show();
                $("#w2Print").show();
                return;
            }

            if (w2substatus) {
                $("#w2Add").hide();
                $("#w2Edit").hide();
                $("#w2View").show();
                $("#w2Print").show();
                return;
            }
        }
        else {
            $("#w2Add").hide();
            $("#w2Edit").hide();
            $("#w2View").hide();
            $("#w2Print").hide();
        }

    }
    else if (form == 'w1') {
        if (si == null) {
            $("#w1Edit").hide();
            $("#w1View").hide();
            $("#w1Print").hide();
            $("#w1Delete").hide();
            return;
        }
        var id = GetFormIDByName("w1");
        var w1status = GetFormIDByName("w1Status");
        if (id != "-1" && id != null) {
            if (w1status == "Saved" || w1status == "Submitted" || w1status == "Rejected")
                $("#w1Delete").show();
            else
                $("#w1Delete").hide();

            if (w1status == "Approved") {
                $("#w1Edit").hide();
                $("#w1View").show();
                $("#w1Print").show();

                return;
            }

            if (w1status == "Rejected") {
                $("#w1Edit").show();
                $("#w1View").show();
                $("#w1Print").show();

                return;
            }
        }
        $("#w1Edit").show();
        $("#w1View").show();
        $("#w1Print").show();
    }
    else if (form == 'wc') {
        if (si == null) {
            $("#wcAdd").hide();
            $("#wcEdit").hide();
            $("#wcView").hide();
            $("#wcPrint").hide();
            $("#wgPrint").hide();
            $("#wcDelete").hide();
            $("#wgDelete").hide();
            return;
        }
        var id = GetFormIDByName("wc");
        var w2status = GetFormIDByName("w2Status"); //To Do change to wd Status
        var wcsubstatus = GetFormIDByName("wcSubStatus");
        var wgsubstatus = GetFormIDByName("wgSubStatus");
        if ((id == "-1" || id == null) && (w2status == "Received")) {
            $("#wcAdd").show();
            $("#wcEdit").hide();
            $("#wcView").hide();
            $("#wcPrint").hide();
            $("#wcDelete").hide();
            $("#wgPrint").hide();
            $("#wgDelete").hide();
            return;
        }
        else if (id != "-1" && id != null) {
            if (wcsubstatus && wgsubstatus) {
                $("#wcAdd").hide();
                $("#wcEdit").hide();
                $("#wcView").show();
                $("#wcPrint").show();
                $("#wcDelete").show();
                $("#wgPrint").show();
                $("#wgDelete").show();
                return;
            }
            $("#wcAdd").hide();
            $("#wcEdit").show();
            $("#wcView").show();
            $("#wcPrint").show();
            $("#wcDelete").show();
            $("#wgPrint").show();
            $("#wgDelete").show();
        }
        else {
            $("#wcAdd").hide();
            $("#wcEdit").hide();
            $("#wcView").hide();
            $("#wcPrint").hide();
            $("#wcDelete").hide();
            $("#wgPrint").hide();
            $("#wgDelete").hide();
        }

    }
    else if (form == 'wd') {
        if (si == null) {
            $("#wdAdd").hide();
            $("#wdEdit").hide();
            $("#wdView").hide();
            $("#wdPrint").hide();
            $("#wdDelete").hide();
            $("#wnPrint").hide();
            $("#wnDelete").hide();
            return;
        }
        var id = GetFormIDByName("wd");
        var w2status = GetFormIDByName("w2Status");
        var wdsubstatus = GetFormIDByName("wdSubStatus");
        var wnsubstatus = GetFormIDByName("wnSubStatus");
        if ((id == "-1" || id == null) && (w2status == "Received")) {
            $("#wdAdd").show();
            $("#wdEdit").hide();
            $("#wdView").hide();
            $("#wdPrint").hide();
            $("#wdDelete").hide();
            $("#wnPrint").hide();
            $("#wnDelete").hide();
            return;
        }
        else if (id != "-1" && id != null) {
            if (wdsubstatus && wnsubstatus) {
                $("#wdAdd").hide();
                $("#wdEdit").hide();
                $("#wdView").show();
                $("#wdPrint").show();
                $("#wdDelete").show();
                $("#wnPrint").show();
                $("#wnDelete").show();
                return;
            }
            $("#wdAdd").hide();
            $("#wdEdit").show();
            $("#wdView").show();
            $("#wdPrint").show();
            $("#wdDelete").show();
            $("#wnPrint").show();
            $("#wnDelete").show();
        }
        else {
            $("#wdAdd").hide();
            $("#wdEdit").hide();
            $("#wdView").hide();
            $("#wdPrint").hide();
            $("#wdDelete").hide();
            $("#wnPrint").hide();
            $("#wnDelete").hide();
        }
    }
}

function OpenFormWCWG(mode, view) {
    var w1id = GetFormIDByName("w1");
    var w2id = GetFormIDByName("w2");
    var wcid = GetFormIDByName("wc");
    var wgid = GetFormIDByName("wg");

    if (mode == "Add") {
        url = "/InstructedWorks/OpenWCWG?w1id=" + w1id + "&w2id=" + w2id;
    }
    else if (mode == "Edit") {
        url = "/InstructedWorks/EditFormWCWG?wcid=" + wcid + "&wgid=" + wgid + "&w2id=" + w2id;
    }
    else if (mode == "View") {
        url = "/InstructedWorks/EditFormWCWG?wcid=" + wcid + "&wgid=" + wgid + "&w2id=" + w2id + "&View=1";
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