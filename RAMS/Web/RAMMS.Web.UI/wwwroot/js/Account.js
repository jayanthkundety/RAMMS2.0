var userModules = [];
var _ctrl = {
    hdnPkId: $("#hdnPkId"),
    txtUserid: $("#txtUserid"),
    txtPassword: $("#txtPassword"),
    txtUsername: $("#txtUsername"),
    txtPosition: $("#txtPosition"),
    txtDepartment: $("#txtDepartment"),
    txtCompany: $("#txtCompany"),
    txtEmail: $("#txtEmail"),
    txtContactNo: $("#txtContactNo"),
    ddlReportingTo: $("#ddlReportingTo"),
    chkIsDisabled: $("#chkIsDisabled"),
    txtLockedUpto: $("#txtLockedUpto"),
    txtPasswordexpiry: $("#txtPasswordexpiry"),
    btnHSave: $("#btnHSave"),
    btnHCancel: $("#btnHCancel"),
    btnPwdSave: $("#btnPwdSave"),
    btnGSave: $("#btnGSave"),
    selModuleList: $("#selModuleList")
};

function get() {
    var req = {};
    req.PkId = _ctrl.hdnPkId.val();

    req.Username = _ctrl.txtUsername.val();
    req.Password = _ctrl.txtPassword.val();
    req.Position = _ctrl.txtPosition.val();
    req.Department = _ctrl.txtDepartment.val();
    req.Companyname = _ctrl.txtCompany.val();
    req.Email = _ctrl.txtEmail.val();
    req.Contactno = _ctrl.txtContactNo.val();
    req.ReportingPkId = _ctrl.ddlReportingTo.val();
    req.SubmitSts = true;
    req.ActiveYn = true;

    req.Isdisabled = _ctrl.chkIsDisabled.find(":checked").val();
    req.Lockeduntil = _ctrl.txtLockedUpto.val();
    req.Passwordexpiry = _ctrl.txtPasswordexpiry.val();
    req.Userid = _ctrl.txtUserid.val();
    req.ForceRstPwd = true;
    req.GroupId = $("#selGroupList").val();
    //req.Logindate = "";
    //req.Retrycount = "";
    //req.DfltUserrole = "";
    //req.UgDfltYn = "";
    //req.Sign = "";
    //req.ContrPkId = "";
    req.ModuleRights = ModuleList.GetList();
    return req;
}

function saveDetail() {
    if (ValidatePage("#headerDiv", "", "validate")) {
        var req = get();
        $("#page-preloader").css("display", "block");
        $.ajax({
            url: "/Account/SaveDetail",
            dataType: "JSON",
            data: req, type: "Post",
            success: function (data) {
                $("#page-preloader").css("display", "none");
                if (data > 0) {
                    app.ShowSuccessMessage("Saved Successfully");
                    setTimeout(function () {
                        window.location.href = "/Account";
                    }, 3000);
                }
                else if (data == -1) {
                    app.Alert(`User Id: "<b>${req.Userid}</b>" already exists, please try with other user Id.`);
                }
            }, error: function (data) {
                console.error(data);
            }
        });
    }
}


function changePassword() {
    if (ValidatePage("#pwdModal", "", "validate")) {
        app.Confirm("Are you sure you want to change the password?, If Yes click OK.", function (e) {
            if (e) {
                var req = {};
                req.PkId = $("#pkId").val();
                req.Password = $("#txtNewPassword").val();
                $("#page-preloader").css("display", "block");
                $.ajax({
                    url: "/Account/ChangePassword",
                    dataType: "JSON",
                    data: req, type: "Post",
                    success: function (data) {
                        $("#page-preloader").css("display", "none");
                        if (data > 0) {
                            app.ShowSuccessMessage("Changed Successfully");
                            $("#pwdModal").modal("hide");

                        }
                        else if (data == -1) {
                            app.Alert(`Internal error, Please try again.`);
                        }
                    }, error: function (data) {
                        console.error(data);
                    }
                });
            }
        });
    }
}


function changeGroup() {
    if (ValidatePage("#usergroupModal", "", "validate")) {
        app.Confirm("Are you sure you want to change the Group?, If Yes click OK.", function (e) {
            if (e) {
                var req = {};
                req.PkId = $("#pkId").val();
                req.GroupUgPkId = $("#ddlUserGroup").val();
                req.GroupId = $("#ddlGroup").val();
                $("#page-preloader").css("display", "block");
                $.ajax({
                    url: "/Account/ChangeGroup",
                    dataType: "JSON",
                    data: req, type: "Post",
                    success: function (data) {
                        $("#page-preloader").css("display", "none");
                        if (data > 0) {
                            app.ShowSuccessMessage("Changed Successfully");
                            $("#usergroupModal").modal("hide");

                        }
                        else if (data == -1) {
                            app.Alert(`Internal error, Please try again.`);
                        }
                    }, error: function (data) {
                        console.error(data);
                    }
                });
            }
        });
    }
}
var ModuleList = new function () {
    this.GetIndex = function (moduleId) {
        var index = -1;
        if (userModules.length > 0) {
            $.each(userModules, function (idx, obj) {
                if (obj.ModPkId == moduleId) {
                    index = idx;
                }
            });
        }
        return index;
    }
    this.GetModule = function (moduleId) {
        var result = null;
        if (userModules.length > 0) {
            $.each(userModules, function (idx, obj) {
                if (obj.ModPkId == moduleId) {
                    result = obj;
                }
            });
        }
        return result;
    }
    this.GetList = function () {
        var modlist = [];
        $("#tblModuleList tbody tr").each(function () {
            var tr = $(this);
            var obj = {};
            obj.ModPkId = tr.attr("cval");
            obj.DIsView = tr.find("[dvview]")[0].checked;
            obj.DIsAdd = tr.find("[dvadd]")[0].checked;
            obj.DIsModify = tr.find("[dvmodify]")[0].checked;
            obj.DIsDelete = tr.find("[dvdelete]")[0].checked;
            obj.PIsAdd = tr.find("[pcadd]")[0].checked;
            obj.PIsModify = tr.find("[pcmodify]")[0].checked;
            obj.PIsDelete = tr.find("[pcdelete]")[0].checked;
            obj.PIsView = tr.find("[pcview]")[0].checked;
            modlist[modlist.length] = obj;
        });
        return modlist;
    }
    this.UpdateList = function () {
        var vals = _ctrl.selModuleList.val();
        var tbl = $("#tblModuleList tbody");
        tbl.find("tr").hide();
        if (vals != null && vals.length > 0) {
            $.each(vals, function (idx, val) {
                var tr = tbl.find("tr[cval='" + val + "']");
                if (tr.length == 0) {
                    var mod = ModuleList.GetModule(val);
                    var opt = _ctrl.selModuleList.find("[value='" + val + "']");
                    tr = $("<tr></tr>");
                    tr.attr("cval", val);
                    tr.append("<td>" + opt.text() + "</td>");
                    tr.append("<td><input type='checkbox' pcview " + (mod == null ? "" : (mod.PIsView ? "checked='checked'" : "")) + " /></td>");
                    tr.append("<td><input type='checkbox' pcadd " + (mod == null ? "" : (mod.PIsAdd ? "checked='checked'" : "")) + " /></td>");
                    tr.append("<td><input type='checkbox' pcmodify " + (mod == null ? "" : (mod.PIsModify ? "checked='checked'" : "")) + " /></td>");
                    tr.append("<td><input type='checkbox' pcdelete " + (mod == null ? "" : (mod.PIsDelete ? "checked='checked'" : "")) + " /></td>");
                    tr.append("<td><input type='checkbox' dvview " + (mod == null ? "" : (mod.DIsView ? "checked='checked'" : "")) + " /></td>");
                    tr.append("<td><input type='checkbox' dvadd " + (mod == null ? "" : (mod.DIsAdd ? "checked='checked'" : "")) + " /></td>");
                    tr.append("<td><input type='checkbox' dvmodify " + (mod == null ? "" : (mod.DIsModify ? "checked='checked'" : "")) + " /></td>");
                    tr.append("<td><input type='checkbox' dvdelete " + (mod == null ? "" : (mod.DIsDelete ? "checked='checked'" : "")) + "/></td>");
                    tbl.append(tr);
                }
                else {
                    tr.show();
                }
            });
        }
        tbl.find("tr:not(:visible)").remove();
    }
}
function onChangeModuleList() {
    ModuleList.UpdateList();
}
$(document).ready(function () {

    _ctrl.btnHSave.on("click", function () {
        saveDetail();
    });

    _ctrl.btnHCancel.on("click", function () {
        app.Confirm("Unsaved changes will be lost. Are you sure you want to cancel?", function (e) {
            if (e) {
                window.location.href = "/Account";
            }
        });
    });

    _ctrl.btnPwdSave.on("click", function () {
        changePassword();
    });

    _ctrl.btnGSave.on("click", function () {
        changeGroup();
    });
    _ctrl.selModuleList.on("change", function () { onChangeModuleList(); });
    ModuleList.UpdateList();
});