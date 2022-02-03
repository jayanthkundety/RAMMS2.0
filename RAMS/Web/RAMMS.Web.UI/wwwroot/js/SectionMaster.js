$(document).ready(function () {

    $("#btnCancel").on("click", function () {
        if (app.Confirm("Unsaved changes will be lost. Are you sure you want to cancel?", function (e) {
            if (e) {
                $("#hdnPkRefNo").val('');
                $("#txtDivCode").val('');
                $("#txtDivName").val('');
                $("#txtRmuCode").val('');
                $("#txtRmuName").val('');
                $("#txtSectionCode").val('');
                $("#txtSectionName").val('');
                $("#AddDivision").modal("hide");
                $(".border-error").removeClass("border-error");
                $("#title").text("Add Division / RMU / Section");
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
        url: "/Administration/GetSectionById",
        dataType: "JSON", data: req, type: "Post",
        success: function (data) {
            if (data != null) {
                $("#hdnPkRefNo").val(data.refNo);
                $("#txtDivCode").val(data.divCode);
                $("#txtDivName").val(data.division);
                $("#txtRmuCode").val(data.rmuCode);
                $("#txtRmuName").val(data.rmuName);
                $("#txtSectionCode").val(data.sectionCode);
                $("#txtSectionName").val(data.sectionName);
                $("#AddDivision").modal("show");
                $("#title").text("Edit Division / RMU / Section");
            }
        }, error: function (data) { console.error(data); }
    });
}

function clear() {
    $("#hdnPkRefNo").val('');
    $("#txtDivCode").val('');
    $("#txtDivName").val('');
    $("#txtRmuCode").val('');
    $("#txtRmuName").val('');
    $("#txtSectionCode").val('');
    $("#txtSectionName").val('');
    $(".border-error").removeClass("border-error");
    $("#title").text("Add Division / RMU / Section");
}

function save() {
    if (ValidatePage("#divSave", "", "validate")) {
        var req = {}
        req.RefNo = $("#hdnPkRefNo").val();
        req.DivCode = $("#txtDivCode").val();
        req.Division = $("#txtDivName").val();
        req.RmuCode = $("#txtRmuCode").val();
        req.RmuName = $("#txtRmuName").val();
        req.SectionCode = $("#txtSectionCode").val();
        req.SectionName = $("#txtSectionName").val();
        req.ActiveYn = true;
        $.ajax({
            url: "/Administration/SaveSection",
            dataType: "JSON", data: req, type: "Post",
            success: function (data) {
                if (data > 0) {
                    app.ShowSuccessMessage("Saved Successfully");
                    $("#AddDivision").modal("hide")
                }
                else if (data == -1) {
                    app.Alert("Already exists, please try other combination.");
                }
                clear();
            }, error: function (data) { console.error(data); }
        });
    }
}