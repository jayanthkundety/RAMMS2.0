var jsRights = new function () {
    this.GroupOnChange = function (tis) {
        if (tis.value != "") {
            this.ClearMolueRights();
            var post = {};
            post.groupId = tis.value;
            GetResponseValue("GetModuleRights", "Account", post, function (data) {
                var tbl = $("#tblModuleList");
                tbl[0].GroupId = post.groupId;
                var tbody = tbl.find("tbody");
                if (data && data.length > 0) {
                    $.each(data, function (idx, obj) {
                        var tr = tbody.find("tr[modid='" + obj.ModPkId + "']");
                        if (tr.length > 0) {
                            tr[0].PkId = obj.PkId;
                            tr.find("[dvview]")[0].checked = obj.DIsView;
                            tr.find("[dvadd]")[0].checked = obj.DIsAdd;
                            tr.find("[dvmodify]")[0].checked = obj.DIsModify;
                            tr.find("[dvdelete]")[0].checked = obj.DIsDelete;
                            tr.find("[pcadd]")[0].checked = obj.PIsAdd;
                            tr.find("[pcmodify]")[0].checked = obj.PIsModify;
                            tr.find("[pcdelete]")[0].checked = obj.PIsDelete;
                            tr.find("[pcview]")[0].checked = obj.PIsView;
                        }
                    });
                }
                tbl.slideDown('slow');
            }, "Finding");
        }
        else {
            this.ClearMolueRights();
        }
    }
    this.GetList = function () {
        var modlist = [];
        var tbl = $("#tblModuleList");
        var GroupId = tbl[0].GroupId;
        tbl.find("tbody tr").each(function () {
            var tr = $(this);
            if (tr.find("input:checked").length > 0) {
                var obj = {};
                obj.ModPkId = tr.attr("modid");
                obj.PkId = tr[0].PkId;
                obj.GroupPkId = GroupId;
                obj.DIsView = tr.find("[dvview]")[0].checked;
                obj.DIsAdd = tr.find("[dvadd]")[0].checked;
                obj.DIsModify = tr.find("[dvmodify]")[0].checked;
                obj.DIsDelete = tr.find("[dvdelete]")[0].checked;
                obj.PIsAdd = tr.find("[pcadd]")[0].checked;
                obj.PIsModify = tr.find("[pcmodify]")[0].checked;
                obj.PIsDelete = tr.find("[pcdelete]")[0].checked;
                obj.PIsView = tr.find("[pcview]")[0].checked;
                modlist[modlist.length] = obj;
            }
        });
        return modlist;
    }
    this.SaveModuleRights = function () {
        var tis = this;
        if (ValidatePage("#headerDiv", "", "")) {
            var post = {};
            post.GroupId = $("#tblModuleList")[0].GroupId;
            post.ModuleRights = this.GetList();
            GetResponseValue("SaveModuleRights", "Account", post, function (data) {
                app.ShowSuccessMessage('Successfully Saved', false);
                setTimeout(tis.NavToList, 2000);
            }, "Saving");
        }
    }
    this.Cancel = function () {
        var tis = this;
        jsMaster.ConfirmCancel(() => { tis.NavToList(); });
    }
    this.NavToList = function () {
        window.location = _APPLocation + "Account/UserGroup";
    }
    this.ClearMolueRights = function () {
        var tbl = $("#tblModuleList").hide();
        tbl[0].GroupId = 0;
        tbl.find("tbody input:checked").prop("checked", false);
        tbl.find("tbody tr").each(function () {
            this.PkId = 0;
        });
    }
    this.ShowGroup = function (isEdit) {
        var isVal = true;
        var id = 0; var name = ""; var code = "";
        if (isEdit) {
            $("#GroupTitle").text("Edit Group Name");
            isVal = ValidatePage("#divUserGroup", "", "");
            if (isVal) {
                var obj = $("#selGroupList");
                id = obj.val();
                name = obj.find("option:selected").text();
                code = obj.find("option:selected").attr("cvalue");
                if (name == "Admin") { isVal = false; app.ShowErrorMessage("Sorry, You canot edit Admin Group name."); }
            }
        }
        else {
            $("#GroupTitle").text("Add Group Name");
        }
        if (isVal) {
            $("#hdnGroupId").val(id);
            $("#txtGroupName").val(name);
            var gcode = $("#txtGroupCode").val(code);
            switch (code) {
                case "Ops-RM":
                case "Supervisor":
                case "Ops-Exec":
                case "Ops-HM":
                case "JKRS-SO":
                case "ERT-Ops":
                case "ERT-PT":
                    gcode.prop("disabled", true);
                    break;
                default:
                    gcode.prop("disabled", false);
                    break;
            }
            $("#GroupModal").modal('show');
        }
    }
    this.SaveGroupName = function () {
        if (ValidatePage("#secGroupName", "", "")) {
            var post = {};
            post.GroupId = $("#hdnGroupId").val();
            post.GroupName = $("#txtGroupName").val();
            post.GroupCode = $("#txtGroupCode").val();
            GetResponseValue("SaveGroup", "Account", post, function (data) {
                if (data.IsSuccess) {
                    app.ShowSuccessMessage(data.Message);
                    $("#GroupModal").modal('hide');
                    setTimeout(function () { window.location = _APPLocation + "Account/GroupRights/" + data.Data; }, 2000);
                }
                else {
                    app.ShowErrorMessage(data.Message);
                }
            }, "Saving");
        }
    }
    this.CancelGroupName = function () {
        jsMaster.ConfirmCancel();
    }
}
$(document).ready(function () {
    $("#btnHSave").on("click", function () { jsRights.SaveModuleRights(); });
    $("#btnHCancel").on("click", function () { jsRights.Cancel(); }); // jsMaster.ConfirmCancel(
});