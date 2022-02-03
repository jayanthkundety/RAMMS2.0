var process = new function () {
    this.Form = "";
    this.Stage = "";
    this.RefID = "";
    this.Init = function (form, stage, refid) {
        this.Form = form;
        this.Stage = stage;
        this.RefID = refid;
    }
    this.ShowApprove = function (type, title) {
        var post = {};
        post.group = type;
        $("#ApprovalModal").remove();
        GetResponseValue("ViewApprove", "Process", post, function (data) {
            $("body").append(data);
            var modal = $("#ApprovalModal");
            modal.modal('show');
            modal.find(".modal-title").text(title);
        }, "Saving");
    }
    this.UserChange = function (tis) {
        var sel = $(tis);
        var opt = sel.find(":selected");
        var item1 = opt.attr("item1") ? opt.attr("item1") : "";
        if (item1 == "others") {
            $("#txtAppUserName").val("").addClass("validate").prop("disabled", false);
            $("#txtAppUserDesignation").val("").addClass("validate").prop("disabled", false);
        }
        else {
            var item2 = opt.attr("Item2") ? opt.attr("item2") : "";
            $("#txtAppUserName").val(item1).removeClass("validate").prop("disabled", true);
            $("#txtAppUserDesignation").val(item2).removeClass("validate").prop("disabled", true);
        }
    }
    this.Rejected = function () { this.Save(false); }
    this.Approved = function () { this.Save(true); }
    this.Save = function (IsApproved) {
        if (ValidatePage("#ApprovalModal", "", "")) {
            var post = {};
            post.Form = this.Form;
            post.Stage = this.Stage;
            post.IsApprove = IsApproved;
            post.RefId = this.RefID;
            GetResponseValue("Save", "Process", FormValueCollection("#ApprovalModal", post), function (data) {
                app.ShowSuccessMessage('Successfully Saved', false);
                $("#ApprovalModal").modal("hide");
                setTimeout(function () { window.location = window.location; }, 1000);
            }, "Saving");
        }
    }
}