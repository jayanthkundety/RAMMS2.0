var jsRSP = new function () {
    this.Load = function () {
        var ctrl = $("#resetpassword");
        var _this = this;
        if (ctrl.length == 0) {
            PostResponseHTML("ResetPassword", "SignIn", {}, function (html) {
                $("body").append(html);
                _this.ShowRSPModal();
                $("[SaveResetPassword]").on("click", () => { jsRSP.Save(); }).removeAttr("SaveResetPassword");
            });
        }
        else {
            this.Clear();
            this.ShowRSPModal();
            $("[SaveResetPassword]").on("click", () => { jsRSP.Save(); }).removeAttr("SaveResetPassword");
        }
    }
    this.Save = () => {
        if (ValidatePage("#resetpassword", "control", "")) {
            var par = $("#resetpassword");
            var post = {};
            post.OldPassword = par.find("[name='txtOldPassword']").val();
            post.NewPassword = par.find("[name='txtNewPassword']").val();
            post.ConfirmPassword = par.find("#txtNewConfirmPassword").val();
            GetResponseValue("SaveResetPassword", "SignIn", post, function(data){
                if (data.isSuccess) {
                    app.ShowSuccessMessage(data.message);
                    jsRSP.Clear();
                    $("#resetpassword").modal('hide');
                    setTimeout(function () {
                        window.location.href = '/SignIn/Index?id=logout';
                    }, 3000);
                }
                else {
                    app.ShowErrorMessage(data.message);
                }
            }, "Saving");
        }
    }
    this.Clear = () => {
        $("#resetpassword").find("input").each(function () { Validation.RemoveErrStyles($(this).val("")); });
    }
    this.ShowRSPModal = () => { $("#resetpassword").modal('show'); }
}
$("[ResetPassword]").on("click", () => { jsRSP.Load(); }).removeAttr("ResetPassword");