$(document).on("click", "#saveFormW2Btn", function () {

    Save(false);
});

$(document).on("click", "#submitFormW2Btn", function () {
    Save(true);
});

function Save(submit) {
    if (submit) {
        $("#div-addformd .svalidate").addClass("validate");
    }

    if (ValidatePage('#divpage')) {
        InitAjaxLoading();
        $.post('./SaveFormW2', $("form").serialize(), function (data) {

            HideAjaxLoading();
            if (data == -1) {
                app.ShowErrorMessage(data.errorMessage);

            }
            else {
                $("#FW2HRef_No").val(data.pkRefNo);
                app.ShowSuccessMessage('Successfully Saved', false);
                // location.href = "/InstructedWorks/AddFormW2";
            }

        });
    }
}

function Delete(id) {
    var headerId = id;
    app.Confirm("Are you sure you want to delete the record?, If Yes click OK.", function (e) {
        if (e) {
            InitAjaxLoading();
            $.ajax({
                url: '/InstructedWorks/Delete',
                data: { headerId },
                type: 'POST',
                success: function (data) {
                    if (data > 0) {
                        app.ShowSuccessMessage('Successfully Deleted', false);
                        FormDGridRefresh();
                    }
                    else {
                        app.ShowErrorMessage("Error in Deleted. Kindly retry later.", false);
                    }
                    HideAjaxLoading();
                }
            });
        }
    });
}

function GetImageList(id) {

    var group = $("#FormADetAssetGrpCode option:selected").val();
    var FormType = "FormW2"
    if (id && id > 0) {
        $("#FW2HRef_No").val(id);
    }
    else {
        id = $("#FW2HRef_No").val();
    }

    $.ajax({
        url: '/InstructedWorks/GetW2ImageList',
        data: { formW2Id: id, assetgroup: group },
        type: 'POST',
        success: function (data) {
            $("#ViewPhoto").html(data);
            $("#FW2HRef_No").val(id);
        },
        error: function (data) {
            alert(data.responseText);
        }

    });
}

function changeRegion(obj) {
    var ctrl = $(obj);

    if (ctrl.val() != null && ctrl.val() != "") {
        var name = ctrl.find("option:selected").text();
        name = name.split('-').length > 1 ? name.split('-')[1] : name;
        $("#formW2RegionName").val(name);
    }
    else {
        $("#formW2RegionName").val('');
    }
}

function changeDivision(obj) {
    var ctrl = $(obj);
    if (ctrl.val() != null && ctrl.val() != "") {
        var name = ctrl.find("option:selected").text();
        name = name.split('-').length > 1 ? name.split('-')[1] : name;
        $("#formW2DivisonName").val(name);
    }
    else {
        $("#formW2DivisonName").val('');
    }
}

function changeRMU(obj) {
    var ctrl = $(obj);
    if (ctrl.val() != null && ctrl.val() != "") {
        var name = ctrl.find("option:selected").text();
        name = name.split('-')[1];
        $("#formW2RMUName").val(name);
    }
    else {
        $("#formW2RMUName").val('');
    }
}

function OnRoadChange(tis) {
    var ctrl = $(tis);

    if (ctrl.val() != null && ctrl.val() != "") {
        $("#formW2roadDesc").val(ctrl.find("option:selected").attr("Item1"));
        $("#formW2Fromch").val(ctrl.find("option:selected").attr("fromm"));
        $("#formW2Toch").val(ctrl.find("option:selected").attr("tom"));
        $("#frmW2RoadCode").val(ctrl.val());
        var roadCode = ctrl.val();
        InitAjaxLoading();
        $.ajax({
            url: '/InstructedWorks/GetW1Details',
            dataType: 'JSON',
            data: { roadCode },
            type: 'Post',
            success: function (data) {
                $("#Fw1RefNo").val(data.referenceNo);
                $("#Fw1PKRefNo").val(data.pkRefNo);
                $("#Fw1ProjectTitle").val(data.projectTitle);
                $("#formW2TitleOfInstructWork").val(data.detailsOfWork);
                $("#formW2EstCost").val(data.estimTotalCost);
                HideAjaxLoading();
            },
            error: function (data) {
                console.error(data);
            }
        });

    }
    else {
        $("#formW2roadDesc").val('');
    }

}

function ChangeIUser(obj) {
    var ctrl = $(obj);

    if (ctrl.val() != null && ctrl.val() != "") {
        var name = ctrl.find("option:selected").text();
        name = name.split('-')[1];
        $("#formW2IssuedName").val(name);
    }
    else {
        $("#formW2IssuedName").val('');
    }
}

function ChangeRUser(obj) {
    var ctrl = $(obj);

    if (ctrl.val() != null && ctrl.val() != "") {
        var name = ctrl.find("option:selected").text();
        name = name.split('-')[1];
        $("#formW2ReceivedName").val(name);
    }
    else {
        $("#formW2ReceivedName").val('');
    }
}

function GoBack() {
    if ($("#hdnView").val() == "0") {
        if (app.Confirm("Unsaved changes will be lost. Are you sure you want to cancel?", function (e) {
            if (e) {
                location.href = "/InstructedWorks/Index";
            }
        }));
    }
    else
        location.href = "/InstructedWorks/Index";
}

