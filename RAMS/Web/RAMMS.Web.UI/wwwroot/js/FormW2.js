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

