$(document).ready(function () {

    $("#ddlDivision").chosen();
    $("#btnCancel").on("click", function () {
        if (app.Confirm("Unsaved changes will be lost. Are you sure you want to cancel?", function (e) {
            if (e) {
                $("#ddlDivision").val('');
                $("#ddlDivision").trigger("chosen:updated");
                $("#txtCode").val('');
                $("#txtName").val('');
                $("#hdnPkRefNo").val('');
                $("#AddDivision").modal("hide");
                $("#h5Title").text("Add RMU");
            }
        }));
    });

    $("#btnSave").on("click", function () {
        save();
    });


});

function edit(id) {
    var req = {};
    req.id = id;
    $.ajax({
        url: "/Administration/GetRMUById",
        dataType: "JSON", data: req, type: "Post",
        success: function (data) {
            if (data != null) {
                $("#h5Title").text("Edit RMU");
                $("#hdnPkRefNo").val(data.pkRefNo);
                $("#txtCode").val(data.code);
                $("#txtName").val(data.name);
                $("#ddlDivision").val(data.divCode);
                $("#ddlDivision").trigger("chosen:updated");
                $("#AddDivision").modal("show");
            }
            $(".border-error").removeClass("border-error");
        }, error: function (data) { console.error(data); }
    });
}

function save() {
    if (ValidatePage("#divSave", "", "validate")) {
        var req = {};
        req.PkRefNo = $("#hdnPkRefNo").val();
        req.Code = $("#txtCode").val();
        req.Name = $("#txtName").val();
        req.DivCode = $("#ddlDivision").val();
        req.Isactive = true;
        $.ajax({
            url: "/Administration/SaveRMU",
            dataType: "JSON", data: req, type: "Post",
            success: function (data) {
                if (data > 0) {
                    app.ShowSuccessMessage("Saved Successfully");
                }
                $("#h5Title").text("Add RMU");
                $("#hdnPkRefNo").val("");
                $("#txtCode").val("");
                $("#txtName").val("");
                $("#ddlDivision").val("");
                $("#ddlDivision").trigger("chosen:updated");
                $("#btnSearch").click();
                $("#AddDivision").modal("hide");
            }, error: function (data) { console.error(data); }
        });
    }
}