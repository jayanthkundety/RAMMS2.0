var _progressbarcolor = "#000000"; var _progresstext = ""; var _progressbg = ""; var _progressborder = "";
var _ISXHR = false;
var _APPLocation = "";
function HttpRequestDispose(xhr) {
    if (typeof (xhr) !== 'undefined') {
        if (typeof (xhr.onreadystatechange) !== 'unknown') {
            xhr.onreadystatechange = null;
        }
        if (typeof (xhr.abort) !== 'unknown') {
            if (typeof (xhr.abort) == "function") {
                xhr.abort();
            }
            xhr.abort = null;
        }
        xhr = null;
    }
}
function SubmitFormWithFile(formID, dtype, callBackMethod, Loading) {
    InitAjaxLoading(Loading);
    $("#" + formID).ajaxSubmit({
        dataType: dtype,
        success: function (data) {
            callBackMethod(data);
            HideAjaxLoading();
        }
    });
}
function ValidateAjaxResult(data) {
    var _result = $(data);
    if (_result.length > 0) {
        _result = _result.filter("form");
        if (_result.length > 0) {
            _result = _result.attr("action");
            if (_result != null) {
                if (_result.toLowerCase().indexOf("login/login") > -1) { return false; }
            }
        }
    }
    return true;
}
function SubmitForm(formID, Type, callBackMethod, Loading) {
    var _form = $("#" + formID);
    InitAjaxLoading(Loading);
    //debugger;
    InvokeRes = $.ajax({
        type: "POST",
        data: _form.serialize(),
        url: _form.attr("action") + window.location.search,
        dataType: Type,
        success: function (data, s, xhr) {
            if (ValidateAjaxResult(data)) {
                callBackMethod(data);
                HideAjaxLoading();
                HttpRequestDispose(xhr);
            }
            else {
                HideAjaxLoading();
                HttpRequestDispose(xhr);
                //CAlert(_$session, _$sessionhead, "window.location.reload();", "Continue");
            }
        },
        error: function (request, text, err) {
            AjaxErrorHandler(request, text, err, true);
        }
    });
}
function DeleteRequest(action, controller, data, callback) {
    AjaxResponseHTML(action, controller, "POST", data, callback);
}
function AjaxResponseHTML(action, controller, Type, postData, CallBackMethod) {
    InitAjaxLoading("Loading");
    if (_APPLocation == null || _APPLocation == undefined) { _APPLocation = "/"; }
    $.ajax({
        type: Type,
        contentType: "application/json; charset=utf-8",
        data: postData,
        url: _APPLocation + controller + "/" + action + window.location.search,
        success: function (data, s, xhr) {
            if (ValidateAjaxResult(data)) {
                CallBackMethod(data);
                HideAjaxLoading();
                HttpRequestDispose(xhr);
            }
            else {
                HttpRequestDispose(xhr);
                HideAjaxLoading();
                //CAlert(_$session, _$sessionhead, "window.location.reload();", "Continue");
            }
        },
        error: function (request, text, err) {
            AjaxErrorHandler(request, text, err, true);
        }
    });
}
function FormValueCollection(selector, post) {
    if (!post) { post = {}; }
    var obj = $(selector);
    var _form = obj.find("[name]:not(':radio'):not(':checkbox')");
    _form.each(function () {
        post[this.name] = $(this).val();
    });
    _form = obj.find("[name]:radio:checked");
    _form.each(function () {
        post[this.name] = $(this).val();
    });
    _form = obj.find("[name]:checkbox");
    _form.each(function () {
        post[this.name] = this.checked;
    });
    return post;
}
function PostData(url, data, callBackMethod, Loading, showAjaxLoading) {
    InvokeResponseValue(_APPLocation + url, "", data, callBackMethod, Loading, showAjaxLoading)
}
function PostResponseHTML(action, controller, postData, CallBackMethod) {
    AjaxResponseHTML(action, controller, "POST", postData, CallBackMethod);
}
function GetResponseHTML(action, controller, postData, CallBackMethod) {
    AjaxResponseHTML(action, controller, "GET", postData, CallBackMethod);
}
function GetResponseData(action, controller, postData, CallBackMethod) {
    GetResponseValue(action, controller, postData, CallBackMethod, "Retrieving");
}
function GetResponseValue(action, controller, postData, CallBackMethod, Loading) {
    if (typeof (postData) == "string") {
        postData = FormValueCollection(postData, {});
    }
    InvokeResponseValue(_APPLocation + controller, action, postData, CallBackMethod, Loading, true);
}
var InvokeRes = null;
function InvokeResponseValue(URL, MethodName, postData, CallBackMethod, Loading, showAjaxLoading) {
    if (showAjaxLoading) { InitAjaxLoading(Loading); }
    InvokeRes = $.ajax({
        type: "POST",
        data: postData,
        url: URL + "/" + MethodName + window.location.search,
        success: function (data, s, xhr) {
            if (ValidateAjaxResult(data)) {
                if (showAjaxLoading) { HideAjaxLoading(); }
                CallBackMethod(data);
                HttpRequestDispose(xhr);
            }
            else {
                HttpRequestDispose(xhr);
                if (showAjaxLoading) { HideAjaxLoading(); }
                CAlert(_$session, _$sessionhead, "window.location.reload();", "Continue");
            }
        },
        error: function (request, text, err) {
            AjaxErrorHandler(request, text, err, showAjaxLoading);
        }
    });
}
function AjaxErrorHandler(request, txt, err, showAjaxLoading) {
    if (showAjaxLoading) { HideAjaxLoading(); }
    //debugger;
    console.log(request.responseText);
    switch (request.status) {
        case 404:
            alert("The requested URL was not found on this server.");
            break;
        default:
            //if (ValidateAjaxResult(request.responseText)) {
            //    alert("Sorry, This Portal has encountered an error. Please close the current browser window and reopen it. Contact your administrator if the problem persists.");
            //}
            //else {
            //    CAlert(_$session, _$sessionhead, "window.location.reload();", "Continue");
            //}
            alert("We are sorry an error occured. Kindly contact Administrator.");
            //EAlert(request.responseText, "Error", "");
            break;
    }
    HttpRequestDispose(request);
}
//This method is used to encode the text like as html encode
function ConvString(Value) {
    return encodeURI(Value);
}
//This class is used to create the new json format data
function JsonData(data, value, enc) {
    this.Encrypt = false;
    if (arguments.length == 2 || typeof (enc) != "boolean") { this.Encrypt = true; } else { this.Encrypt = enc; }
    this.content = this.Encrypt ? (data + ": \"" + ConvString(value) + "\"") : (data + ": \"" + value + "\"");
    this.AddData = AddJsonData;
    this.GetData = GetJsonData;
}
//This method is used to get/return json data format
function GetJsonData() {
    return this.content;
}
//This method is used to add/append json data
function AddJsonData(data, value) {

    this.content += (this.Encrypt ? ("," + data + ": \"" + ConvString(value) + "\"") : ("," + data + ": \"" + value + "\""));
}

function InitAjaxLoading(content) {
    $("#page-preloader").css("display", "block");
}

function HideAjaxLoading(content) {
    $("#page-preloader").css("display", "none");
    MVC_Disabled();
    $("select:not(.custom-select)").chosen();
    Validation.OnKeyPressInit();
}
var MVC_Disabled = () => {
    $(".ctldisabled").prop("disabled", true).removeClass("ctldisabled");
    $(".grpctrldisabled").each(function () { var ctl = $(this); ctl.find("input,select,textarea").prop("disabled", true); ctl.removeClass("grpctrldisabled"); ctl.find("select").trigger("chosen:updated"); });
    $(".ctlenabled").prop("disabled", false).removeClass("ctlenabled");
}
$(document).ready(function () {
    MVC_Disabled();
});