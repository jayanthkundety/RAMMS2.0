$(document).ready(function () {


    $("#btnCancel").on("click", function () {
        app.Confirm("Unsaved changes will be lost. Are you sure you want to cancel?", function (e) {
            if (e) {
                $("#txtCode").val('');
                $("#txtName").val('');
                $("#hdnPkRefNo").val('');
                $("#AddDivision").modal("hide");
                $(".border-error").removeClass("border-error");
                $("#title").text("Add Division");
            }
        });
    });

    $("#btnSave").on("click", function () {
        save();
    });


});

function edit(id) {
    var req = {};
    req.id = id;
    $.ajax({
        url: "/Administration/GetDivisionById",
        dataType: "JSON", data: req, type: "Post",
        success: function (data) {
            if (data != null) {
                $("#hdnPkRefNo").val(data.pkRefNo);
                $("#txtCode").val(data.code);
                $("#txtName").val(data.name);
                $("#AddDivision").modal("show");
                $(".border-error").removeClass("border-error");
                $("#title").text("Edit Division");
            }
        }, error: function (data) { console.error(data); }
    });
}

function save() {
    if (ValidatePage("#divSave", "", "validate")) {
        var req = {};
        req.PkRefNo = $("#hdnPkRefNo").val();
        req.Code = $("#txtCode").val();
        req.Name = $("#txtName").val();
        req.Isactive = true;
        $.ajax({
            url: "/Administration/SaveDivision",
            dataType: "JSON", data: req, type: "Post",
            success: function (data) {
                if (data > 0) {
                    app.ShowSuccessMessage("Saved Successfully");
                }
                else if (daa == -1) {
                    app.Alert("Already exists, please try other combination.");
                }
                $("#hdnPkRefNo").val("");
                $("#txtCode").val("");
                $("#txtName").val("");
                $("#btnSearch").click();
                $("#AddDivision").modal("hide");
            }, error: function (data) { console.error(data); }
        });
    }
}