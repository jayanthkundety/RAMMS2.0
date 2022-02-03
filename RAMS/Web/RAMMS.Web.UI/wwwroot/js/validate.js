/******* Validation - jQuery Plugin *******/
function ValidatePage(vid, Title, liTitle, validationClass = 'validate') {
    return Validation.Start(vid, Title, liTitle, validationClass);
}
var Validation = new function () {
    this._$divid = "";
    this._$container = "";
    this._errMessage = "";
    this._isrequired = false;
    this._nbr = '0123456789';
    this._$currentdate = "";
    this._lwr = 'abcdefghijklmnopqrstuvwxyz';
    this._uwr = 'ABCDEFGHIJKLMNOPQRSTUVWXYZ';
    this._msgTitle = "";
    this._msgliTitle = "";
    this.numPattern = /^([0-9])+$/;
    this.alphaNumPattern = /^([a-zA-Z0-9])+$/;
    this.toastmem = null;
    this.KeyPressValidate = function (e, obj) {
        if (e.keyCode == 13 || e.keyCode == 27) { return; }
        var _kp = $(obj).attr("onkeypressvalidate");
        var _parse = _kp ? _kp.split(",") : [];
        var _error = [];
        for (var i = 0; i < _parse.length - 1; i++) {
            var _type = _parse[i];
            switch (_type) {
                case "lower":
                    if (Validation.isValid(Validation._lwr, obj.value + e.key) == false) {
                        _error[_error.length] = _parse[_parse.length - 1] + " " + _$lower;
                    }
                    break;
                case "upper":
                    if (Validation.isValid(Validation._uwr, obj.value + e.key) == false) {
                        _error[_error.length] = _parse[_parse.length - 1] + " " + _$nbr;
                    }
                    break;
                case "alpha":
                case "cusalpha":
                    var _val = "";
                    if (_type == "cusalpha") { i = i + 1; _val = _parse[i]; }
                    if (Validation.isValid(Validation._lwr + Validation._uwr + _val, obj.value + e.key) == false) {
                        _error[_error.length] = _parse[_parse.length - 1];
                    }
                    break;
                case "alphanumeric":
                    if (Validation.isValidPattern(Validation.alphaNumPattern, obj.value + e.key) == false) {
                        _error[_error.length] = _parse[_parse.length - 1] + " " + _$anbr;
                    }
                    break;
                case "cusalphanumeric":
                    i = i + 1;
                    var _val = _parse[i];
                    if (Validation.isValid(Validation._lwr + Validation._uwr + Validation._nbr + _val, obj.value + e.key) == false) {
                        _error[_error.length] = _parse[_parse.length - 1];
                    }
                    break;
                case "num":
                case "number":
                    if (Validation.isValidPattern(Validation.numPattern, obj.value + e.key) == false) {
                        _error[_error.length] = _parse[_parse.length - 1] + " " + _$nbr;
                    }
                    break;
                case "double":
                case "decimal":
                    i = i + 1;
                    var _digit = parseInt(_parse[i]);
                    if (Validation.IsDouble(obj, _digit, e.key, e.target.selectionStart, e.target.selectionEnd) == false) {
                        _error[_error.length] = _parse[_parse.length - 1] + " " + Validation._errMessage;
                    }
                    break;
                case "cdecimal":
                    i = i + 1;
                    var _fdigit = parseInt(_parse[i]);
                    i = i + 1;
                    var _digit = parseInt(_parse[i]);
                    if (Validation.IsDouble(obj, _digit, e.key, e.target.selectionStart, e.target.selectionEnd, _fdigit) == false) {
                        _error[_error.length] = _parse[_parse.length - 1] + " " + Validation._errMessage;
                    }
                    break;
                case "maxcl":
                    i = i + 1;
                    if (_RTrim(obj.value).length + 1 > parseInt(_parse[i], 10)) {
                        _error[_error.length] = _parse[_parse.length - 1] + " " + _$maxcl.replace(/xxxx/, _parse[i]);

                    }
                    break;
                case "max":
                    i = i + 1;
                    var _grVal = _parse[i];
                    if (_RTrim(obj.value).length + 1 > parseInt(_parse[i], 10)) {
                        _error[_error.length] = _parse[_parse.length - 1] + " " + _$max + " " + _grVal + ".";

                    }
                    break;
            }
        }
        if (_error.length > 0
            //&& !((e.keyCode >= 112 && e.keyCode <= 123)-- Commented by Vijay P V. if you want to un comment, please ping me.
            //    || (e.keyCode >= 1 && e.keyCode <= 47))
        ) {
            e.preventDefault(); e.stopPropagation(); if (Validation.toastmem) { Validation.toastmem.reset(); } Validation.toastmem = app.ShowErrorMessage(_error.join("<br>"));  /*alertify.error(_error.join("<br>"));*/
        }
    }
    this.OnKeyPressInit = function () {
        $("[onkeypressvalidate]:not([ngkeypress])").keypress(function (e) {
            Validation.KeyPressValidate(e, this);
        }).attr("ngkeypress", "").bind('paste', function (event) {
            event.key = event.originalEvent.clipboardData.getData("text");
            Validation.KeyPressValidate(event, this);
        });
    }
    this.ResetErrStyles = function (sel) {
        $(sel).find(".border-error").each(function () {
            Validation.RemoveErrStyles($(this));
        });
    }
    this.RemoveErrStyles = function (ctrl) {
        var attr = ctrl.attr("insteadof");
        if (attr && attr != "") { $(attr).removeClass("border-error"); }
        ctrl.parent().find("[cusvaliderror]").remove();
        ctrl.removeClass('border-error');
        if (ctrl.next('div.chosen-container').length > 0) {
            ctrl.next('div.chosen-container').removeClass('border-error');
        }
        ctrl.next('div.chosen-container').find('ul.chosen-choices').removeClass('border-error');
    }
    this.Start = function (vid, Title, liTitle, validationClass = 'validate') {
        Validation._msgTitle = ""; //(Title ? Title : "");
        Validation._msgliTitle = (liTitle ? liTitle : "");;
        return Validation.initvalidation("", "", vid, validationClass);
    }
    this.initvalidation = function (divid, errcontainer, vid, validationClass = 'validate') {
        Validation._$container = "#" + errcontainer; Validation._$divid = "#" + divid; var _isValid = true; var _olcontent = "";
        var IsVisibleReq = Validation._msgliTitle == validationClass ? false : true;
        var filter = IsVisibleReq ? ":visible ." + validationClass : vid[0] == "." ? "." + validationClass : " ." + validationClass;
        var _id = vid + filter;
        $(_id).each(function () {
            var _this = this;
            var ctrl = $(this);
            //Visibility checking for both Text box & Chosen dropdown
            var Visible = $(this).parent().css("overflow") == "visible" ? true : false;
            var test = ctrl.next('div.chosen-container.chosen-choices');
            if ((!IsVisibleReq || Visible) && !Validation._$Jvalidate(_this)) {
                var attr = ctrl.attr("insteadof");
                if (attr && attr != "") { $(attr).addClass("border").addClass("border-error"); }
                ctrl.addClass('border-error');
                if (ctrl.next('div.chosen-container').length > 0) {
                    ctrl.next('div.chosen-container').addClass('border-error');
                }
                ctrl.next('div.chosen-container').find('ul.chosen-choices').addClass('border-error');

                if (Validation._msgTitle != "control") {
                    _olcontent = _olcontent + '<li id="li_' + _this.id + '"><label class="error" id="lbl_' + _this.id + '" for="' + _this.id + '">' + Validation._errMessage + '</label></li>';
                }
                else {
                    ctrl.parent().append("<div class='text-danger' cusvaliderror>" + Validation._errMessage + "</div>");
                    ctrl.filter(":not([valchange])").on("change", function () {
                        Validation.RemoveErrStyles($(this));
                    });
                }
                //_olcontent = _olcontent + '\n' + Validation._errMessage;
                _isValid = false;
            }
            else {
                Validation.RemoveErrStyles(ctrl);
            }
        });
        if (!_isValid && Validation._msgTitle != "control") {
            //IAlert((Validation._msgliTitle + "<ul class='msgul'>" + _olcontent + "</ul>"), Validation._msgTitle, "");
            //alert(_olcontent);
            Validation.ShowMessage('<ul> ' + _olcontent + '</ul>');

            //Call only if Dynamic Validation Summary Displayer button to be injected in forms
            this.$injectDynamicValSummaryDisplayer(vid);
        }
        return _isValid;
    }
    this.ShowMessage = function (content) {
        var modalElement = '<div class="modal fade" id="validationModal" aria-hidden="true" role="dialog" aria-labelledby="AssetDataDownloadCenterTitle">';
        modalElement = modalElement + '<div class="modal-dialog modal-dialog-centered modal-md" role="document">';
        modalElement = modalElement + '<div class="modal-content">';
        modalElement = modalElement + '<div class="modal-header"> <h5 class="modal-title" id="validationModalCenterTitle"> ';
        modalElement = modalElement + 'Validation Messages </h5>';
        modalElement = modalElement + '<button type="button" class="close" data-dismiss="modal" aria-label="Close"> <span aria-hidden="true">&times;</span> </button> </div> <div class="modal-body"> <div id="ValidationSummary">';
        modalElement = modalElement + content;
        modalElement = modalElement + '</div> </div> </div> </div> </div>';

        $("#validationModal").remove();
        $('body').append(modalElement);

        $('#validationModal').modal('show');
    }
    this.$injectDynamicValSummaryDisplayer = function (formId) {
        var valSummaryDisplayer = $(formId).find("#val-summary-displayer");
        if (valSummaryDisplayer.length) {
            valSummaryDisplayer.html('<button id="btnShowValidationMessageBox" type="button" class="show-validation-message-box btn btn-sm btn-themebtn" title="Click to View Validation Messages"><span class="validation-icon"></span></button>');

            $(".show-validation-message-box").bind('click', function () {
                if ($("#validationModal").length) {
                    $('#validationModal').modal('show');
                }
                else {
                    alert('No Validation Messages to show');
                }

            });
        }
    }

    this._$Jvalidate = function (obj) {
        if (obj.id == "") { obj.id = UUID(); }
        var _$obj = $(obj);
        var _clsName = obj.className;
        var _arrval = new Array();
        _arrval = _clsName.split("{");
        Validation._isrequired = false;
        if (_arrval.length > 0) {
            _clsName = _arrval[_arrval.length - 1];
            _arrval = _clsName.split("}");
            _clsName = _arrval[0];
            _arrval = _clsName.split(",");
            var _valtype = "";
            if (_arrval.length > 0) {
                for (var _evali = 0; _evali < _arrval.length - 1; _evali++) {
                    _valtype = _arrval[_evali].toLowerCase();
                    switch (_valtype) {
                        case "req":
                        case "required":
                        case "dreq":
                        case "visreq":
                        case "togglereq":
                        case "chkreq":
                        case "ereq":
                            Validation._isrequired = true; var _blnResult = true;
                            if (!_$obj.hasClass("vroff")) {
                                if (_valtype == "dreq") {
                                    _evali = _evali + 1;
                                    var _trimVal = _RTrim(obj.value).toLowerCase(); var notValue = _arrval[_evali].toLowerCase();
                                    _blnResult = (_trimVal == "" || _trimVal == notValue) ? false : _blnResult;
                                    if (!_blnResult && _trimVal != "") {
                                        Validation._errMessage = _arrval[_arrval.length - 1] + " value should not be " + notValue + ".";
                                        return false;
                                    }
                                }
                                else if (_valtype == "togglereq" || _valtype == "chkreq") {
                                    _evali = _evali + 1;
                                    var _chkobj = $('#' + _arrval[_evali]);
                                    if (_chkobj.length > 0) {
                                        if (_chkobj[0].type == 'text' || _chkobj[0].type == 'hidden') {

                                            if (Validation.RequiredValidate(obj) == false && false == Validation.RequiredValidate(_chkobj[0])) {
                                                _blnResult = false;
                                            }
                                        }
                                        else {
                                            _blnResult = (_valtype == "togglereq" && !_chkobj[0].checked) ? Validation.RequiredValidate(obj) : (_valtype == "chkreq" && _chkobj[0].checked) ? Validation.RequiredValidate(obj) : _blnResult;
                                        }
                                    }
                                }
                                else {
                                    _blnResult = (_valtype == "req" || _valtype == "required" || _valtype == "ereq" || (_valtype == "visreq" && ((obj.type.indexOf("select") > -1 && _$obj.next().filter(":visible").length > 0) || (obj.type.indexOf("select") == -1 && _$obj.filter(":visible").length > 0)) && _$obj.filter('[disabled]')).length == 0) ? Validation.RequiredValidate(obj) : _blnResult;
                                }
                                if (_blnResult == false) {
                                    switch (obj.type) {
                                        case "text":
                                        case "password":
                                            Validation._errMessage = (_valtype == "ereq") ? _arrval[_arrval.length - 1] : _arrval[_arrval.length - 1] + " " + _$required;
                                            break;
                                        case "select-one":
                                        case "select-multiple":
                                            Validation._errMessage = (_valtype == "ereq") ? _arrval[_arrval.length - 1] : _arrval[_arrval.length - 1] + " " + _$requiredsel;
                                            break;
                                        case "checkbox":
                                            Validation._errMessage = _arrval[_arrval.length - 1] + " " + _$requiredchk;
                                            break;
                                        default:
                                            Validation._errMessage = (_valtype == "ereq") ? _arrval[_arrval.length - 1] : _arrval[_arrval.length - 1] + " " + _$required;
                                            break;
                                    }
                                    return false;
                                }
                            }
                            break;
                        case "emailcmp":
                        case "pwdcmp":
                            _evali = _evali + 1;
                            var pwd = $('#' + _arrval[_evali]);
                            if (pwd.length > 0 && obj.value != pwd[0].value) {
                                Validation._errMessage = _valtype == "emailcmp" ? _$emailcmp : _$pwdcmp;
                                /*Validation._errMessage = Validation._errMessage.replace(/xxxx/, _arrval[_evali + 1]);*/
                                return false;
                            }
                            break;
                        case "domain":
                            var dname = _RTrim(obj.value);
                            for (var j = 0; j < dname.length; j++) {
                                var dh = dname.charAt(j);
                                var hh = dh.charCodeAt(0);
                                if ((hh > 47 && hh < 59) || (hh > 64 && hh < 91) || (hh > 96 && hh < 123) || hh == 45 || hh == 46) {
                                    if ((j == 0 || j == dname.length - 1) && hh == 45) {
                                        Validation._errMessage = _arrval[_evali + 1] + " should not begin or end with '-'";
                                        return false;
                                    }
                                }
                                else {
                                    Validation._errMessage = _arrval[_evali + 1] + " should not have special characters";
                                    return false;
                                }
                            }
                            break;
                        case "email":
                            if (Validation.EmailValidation(obj) == false) {
                                Validation._errMessage = _$email;
                                return false;
                            }
                            break;
                        case "url":
                            if (Validation.isUrl(obj.value) == false) {
                                Validation._errMessage = _arrval[_arrval.length - 1] + " " + _$url;
                                return false;
                            }
                            break;
                        case "cusurl": //validate start with "http" / "https" / "ftp" / "www" / "/"
                            if (Validation.isCusUrl(obj.value) == false) {
                                Validation._errMessage = _arrval[_arrval.length - 1] + " " + _$url;
                                return false;
                            }
                            break;
                        case "mincl":
                            _evali = _evali + 1;
                            if (_RTrim(obj.value).length < _arrval[_evali]) {
                                Validation._errMessage = _arrval[_arrval.length - 1] + " " + _$mincl.replace(/xxxx/, _arrval[_evali]);
                                return false;
                            }
                            break;
                        case "maxcl":
                            _evali = _evali + 1;
                            if (_RTrim(obj.value).length > parseInt(_arrval[_evali], 10)) {
                                Validation._errMessage = _arrval[_arrval.length - 1] + " " + _$maxcl.replace(/xxxx/, _arrval[_evali]);
                                return false;
                            }
                            break;
                        case "eqcl":
                            _evali = _evali + 1; var _trimVal = _RTrim(obj.value);
                            if (_trimVal.length > 0 && _trimVal.length != parseInt(_arrval[_evali], 10)) {
                                Validation._errMessage = _arrval[_arrval.length - 1] + " " + _$eqcl.replace(/xxxx/, _arrval[_evali]);
                                return false;
                            }
                            break;
                        case "date":
                        case "mmddyy":
                        case "ddmmyy":
                        case "ddmmyyyy":
                        case "yymmdd":
                        case "mmddyyyy":
                            var _type = (_valtype == "date" || _valtype == "mmddyy") ? 1 : (_valtype == "ddmmyy") ? 2 : (_valtype == "yymmdd") ? 3 : (_valtype == "mmddyyyy") ? 4 : (_valtype == "ddmmyyyy") ? 5 : 0;
                            if (_type != 0) {
                                if (Validation.datevalidate(obj, _type) == false) {
                                    _type = (_valtype == "date" || _valtype == "mmddyy") ? "mm/dd/yy" : (_valtype == "ddmmyy") ? "dd/mm/yy" : (_valtype == "yymmdd") ? "yy/mm/dd" : (_valtype == "mmddyyyy") ? "mm/dd/yyyy" : (_valtype == "ddmmyyyy") ? "dd/mm/yyyy" : "";
                                    Validation._errMessage = _arrval[_arrval.length - 1] + " " + Validation._errMessage.replace(/xxxx/, _type);
                                    return false;
                                }
                            }
                            break;
                        case "amt":
                        case "dbl":
                        case "double":
                        case "dbl3d":
                        case "dbl4d":
                        case "dbl5d":
                            var _digit = (_valtype == "dbl3d") ? 3 : (_valtype == "dbl4d") ? 4 : (_valtype == "dbl5d") ? 5 : 2;
                            if (Validation.IsDouble(obj, _digit) == false) {
                                Validation._errMessage = _arrval[_arrval.length - 1] + " " + Validation._errMessage;
                                return false;
                            }
                            break;
                        case "cdecimal":
                            _evali = _evali + 1;
                            var _fdigit = parseInt(_arrval[_evali]);
                            _evali = _evali + 1;
                            var _digit = parseInt(_arrval[_evali]);
                            if (Validation.IsDouble(obj, _digit, null, -1, -1, _fdigit) == false) {
                                Validation._errMessage = _arrval[_arrval.length - 1] + " " + Validation._errMessage;
                                return false;
                            }
                            break;
                        case "num":
                        case "number":
                            if (Validation.isValidPattern(Validation.numPattern, obj.value) == false) {
                                Validation._errMessage = _arrval[_arrval.length - 1] + " " + _$nbr;
                                return false;
                            }
                            break;
                        case "greaterthan":
                        case "graterthan":
                        case "gt":
                            _evali = _evali + 1;
                            var _grVal = _arrval[_evali];
                            if (parseFloat(obj.value, 10) <= parseFloat(_grVal, 10)) {
                                Validation._errMessage = _arrval[_arrval.length - 1] + " " + _$gt + " " + _grVal + ".";
                                return false;
                            }
                            break;
                        case "lessthan":
                        case "lt":
                            _evali = _evali + 1;
                            var _grVal = _arrval[_evali];
                            if (parseFloat(obj.value, 10) > parseFloat(_grVal, 10)) {
                                Validation._errMessage = _arrval[_arrval.length - 1] + " " + _$max + " " + _grVal + ".";
                                return false;
                            }
                            break;
                        case "cmptocurdate":
                            if (Validation.CurrentDateCompare(obj, _arrval[_evali - 1].toLowerCase()) == false) {
                                Validation._errMessage = _arrval[_arrval.length - 1] + " " + _$cmpdate;
                                return false;
                            }
                            break;
                        case "ext":
                            _evali = _evali + 1;
                            if (Validation.Validateext(obj, _arrval[_evali]) == false) {
                                Validation._errMessage = _arrval[_arrval.length - 1] + " " + _$filetype.replace(/xxxx/, _arrval[_evali].replace(/[.]/g, ','));
                                return false;
                            }
                            break;
                        case "pwd":
                            if (Validation.validatePassword(obj.value) == false) {
                                Validation._errMessage = _arrval[_arrval.length - 1] + " " + _$pwd;
                                return false;
                            }
                            break;
                        case "phone":
                            if (Validation.validatePhone(obj.value) == false) {
                                Validation._errMessage = _arrval[_arrval.length - 1] + " " + _$phone;
                                return false;
                            }
                            break;
                        case "alphanum":
                            if (Validation.isValidPattern(Validation.alphaNumPattern, obj.value) == false) {
                                Validation._errMessage = _arrval[_arrval.length - 1] + " " + _$anbr;
                                return false;
                            }
                            break;
                        case "notstartwith":
                            _evali = _evali + 1;
                            var _len = _arrval[_evali].length;
                            if (_RTrim(obj.value).substring(0, _len).toLowerCase() == _arrval[_evali].toLowerCase()) {
                                Validation._errMessage = _arrval[_arrval.length - 1] + " " + _$notstartwith.replace(/xxxx/, _RTrim(obj.value).substring(0, _len));
                                return false;
                            }
                            break;
                        case "chkgrpreq":
                        case "rdbgrpreq":
                            _evali = _evali + 1; var _chkRdoObj = $('#' + obj.id);
                            if (!_chkRdoObj.hasClass(_arrval[_evali])) {
                                _chkRdoObj.addClass(_arrval[_evali]);
                            }
                            if (Validation.ValidateCheckBoxList($('.' + _arrval[_evali])) == false) {
                                Validation._errMessage = _arrval[_arrval.length - 1];
                                return false;
                            }
                            break;
                        case "visichkgrpreq":
                        case "visirdbgrpreq":
                            _evali = _evali + 1; var _chkRdoObj = $('#' + obj.id);
                            if (!_chkRdoObj.filter(':visible').hasClass(_arrval[_evali])) {
                                _chkRdoObj.addClass(_arrval[_evali]);
                            }
                            if (Validation.ValidateCheckBoxList($('.' + _arrval[_evali]).filter(':visible')) == false) {
                                Validation._errMessage = _arrval[_arrval.length - 1];
                                return false;
                            }
                            break;
                        case "filegrpreq":
                            _evali += 1; var _fileObj = $('#' + obj.id);
                            if (!_fileObj.hasClass(_arrval[_evali])) {
                                _fileObj.addClass(_arrval[_evali]);
                            }
                            if (Validation.ValidateFilesList($('.' + _arrval[_evali])) == false) {
                                Validation._errMessage = _arrval[_arrval.length - 1] + " " + _$required;
                                return false;
                            }
                            break;
                        case "txtgrpreq":
                            _evali = _evali + 1;
                            var blnAllow = false;
                            $("." + _arrval[_evali]).each(function () {
                                if (Validation.RequiredValidate(this)) {
                                    blnAllow = true;
                                }
                            });
                            if (!blnAllow) {
                                Validation._errMessage = _arrval[_arrval.length - 1];
                                return false;
                            }
                            break;
                        case "datepattern":
                            if (_$obj.filter(":visible").length > 0) {
                                if (Validation.IsDatePattern(obj) == false) {
                                    Validation._errMessage = _arrval[_arrval.length - 1] + " " + _$dtptt;
                                    return false;
                                }
                            }
                            break;
                        case "timepattern":
                            if (_$obj.filter(":visible").length > 0) {
                                if (Validation.IsTimePattern(obj) == false) {
                                    Validation._errMessage = _arrval[_arrval.length - 1] + " " + _$dtptt;
                                    return false;
                                }
                            }
                            break;
                        case "methodreq":
                        case "mreq":
                            _evali = _evali + 1;
                            var _Val = _arrval[_evali];
                            _Val = eval(_Val);
                            if (!_Val) {
                                Validation._errMessage = _arrval[_arrval.length - 1];
                                return false;
                            }
                            break;
                    }
                }
                return true;
            }
        }
    }
    this.isValidPattern = function (pattern, val) {
        var _trimVal = _RTrim(val);
        if (_trimVal.length > 0) { return pattern.test(val); }
        return true;
    }
    this.isValid = function (key, val) {
        var parm = val;
        for (i = 0; i < parm.length; i++) { if (key.indexOf(parm.charAt(i), 0) == -1) { return false; } }
        return true;
    }
    this.RequiredValidate = function (obj) {
        var _trimVal = _RTrim(obj.value);
        switch (obj.type) {
            case "text":
            case "password":
            case "file":
            case "hidden":
            case "textarea":
            case "date":
            case "datetime-local":
            case "datetime":
                if (_trimVal == "") { return false; }
                break;
            case "select-one":
            case "select-multiple":
                if (_trimVal == "") { return false; }
                break;
            case "checkbox":
                if (obj.checked == false) { return false; }
                break;
        }
        return true;
    }
    this.IsDatePattern = function (obj) {
        return Validation.isValid("dMy ,-/", obj.value);
    }
    this.IsTimePattern = function (obj) {
        return Validation.isValid("hHmst :", obj.value);
    }

    this.validatePassword = function (pswd) {
        if (pswd.match(/^(?=.*[0-9])(?=.*[!@#$%^&*])[a-zA-Z0-9!@#$%^&*]{7,15}$/) != null && pswd.length >= 8 && pswd.length <= 15)
            return true;
        return false;
    }

    this.validatePhone = function (phone) {
        if (phone.match(/^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$/) != null && phone.match(/\d/g).length >= 10 && phone.match(/\d/g).length <= 12)
            return true;
        return false;
    }
    this.Validateext = function (obj, _type) {
        var _fval = _RTrim(obj.value).toLowerCase();
        var arr_strType = new Array();
        _type = _type.toLowerCase();
        arr_strType = _fval.split('.');
        if (arr_strType.length <= 1) { return false; }
        _fval = arr_strType[arr_strType.length - 1];
        if (_type.indexOf(_fval, 0) == -1) { return false; }
        return true;
    }
    this.CurrentDateCompare = function (obj, type) {
        if (_RTrim(obj.value) == "") { return true; }
        var ADate = _RTrim(obj.value);
        var arrDate = new Array();
        arrDate = ADate.split('/');
        if (type == "mmddyy") { var month = arrDate[0]; var day = arrDate[1]; var year = "20" + arrDate[2]; }
        else if (type == "ddmmyy") { var month = arrDate[1]; var day = arrDate[0]; var year = "20" + arrDate[2]; }
        else if (type == "yymmdd") { var month = arrDate[1]; var day = arrDate[2]; var year = "20" + arrDate[0]; }
        ADate = month + "/" + day + "/" + year;
        var _curdate = new Date(Validation._$currentdate);
        var _actdate = new Date(ADate);
        if (_curdate < _actdate) { return false; }
        return true;
    }
    this.IsDouble = function (obj, digit, key, start, end, fdigit) {
        if (!fdigit || fdigit == undefined) { fdigit = -1; }
        if (digit == undefined) { digit = 2; }
        var val = obj.value;
        if (key && start > -1 && end > -1) {
            if (val && val != "") { val = val.substr(0, start) + key + val.substr(end); } else { val = key; }
        }
        var val = _RTrim(val);
        if (val == "") { return true; }
        if (Validation.isValid(Validation._nbr + ".", val) == false) { Validation._errMessage = _$number; return false; }
        var arr = val.split('.');
        if (arr.length > 2) { Validation._errMessage = " is in incorrect format."; return false; }
        else {
            if (arr.length == 2) { if (arr[1].length > digit) { Validation._errMessage = " is in incorrect format. The decimal values should not be more than " + digit + " digits."; return false; } }
            if (fdigit > -1 && arr[0].length > fdigit) { Validation._errMessage = " is in incorrect format. The decimal values should not be more than " + digit + " digits."; return false; }

        }
        return true;
    }
    this.isUrl = function (s) { return /(ftp|http|https):\/\/(\w+:{0,1}\w*@)?(\S+)(:[0-9]+)?(\/|\/([\w#!:.?+=&%@!\-\/]))?/.test(s); }
    this.isWWWUrl = function (s) { return /^www.*/i.test(s); }
    this.isCusUrl = function (s) {
        var iResult = Validation.isUrl(s);
        if (iResult == false) {
            iResult = Validation.isWWWUrl(s);
            if (iResult == false) {
                iResult = /^\//.test(s);
            }
        }
        return iResult;
    }
    this.EmailValidation = function (obj) {
        var _pattern = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
        var _inputval = _RTrim(obj.value);
        if (_pattern.test(_inputval) || _inputval == "") { return true; }
        else { return false; }
    }
    this.isAlphaNumeric = function (val) {
        var parm = val;
        var key1 = Validation._nbr;
        var key2 = Validation._lwr + Validation._uwr; var isChar = false; var isNum = false;
        for (i = 0; i < parm.length; i++) {
            if (key1.indexOf(parm.charAt(i), 0) == -1) {
                isChar = true;
                if (key2.indexOf(parm.charAt(i), 0) == -1) {
                    return false;
                }
            }
            else {
                isNum = true;
            }
        }
        if (isChar == true && isNum == true) { return true; }
        else { return false; }
    }
    this.datevalidate = function (obj, type) {
        if (_RTrim(obj.value) == "") { return true; }
        var ADate = _RTrim(obj.value);
        var arrDate = new Array();
        arrDate = ADate.split('/');
        if (arrDate.length != 3) { Validation._errMessage = _$date; return false; }
        if (type == 2) {
            if (arrDate[0].length > 2 || arrDate[1].length > 2 || arrDate[2].length > 2) { Validation._errMessage = _$date; return false; }
        }
        else if (type == 4) {
            if (arrDate[0].length > 2 || arrDate[1].length > 2 || arrDate[2].length > 4) { Validation._errMessage = _$date; return false; }
        }
        if (type == 1) { var month = arrDate[0]; var day = arrDate[1]; var year = "20" + arrDate[2]; }
        else if (type == 2) { var month = arrDate[1]; var day = arrDate[0]; var year = "20" + arrDate[2]; }
        else if (type == 3) { var month = arrDate[1]; var day = arrDate[2]; var year = "20" + arrDate[0]; }
        else if (type == 4) { var month = arrDate[0]; var day = arrDate[1]; var year = "20" + arrDate[2]; }
        else if (type == 5) { var month = arrDate[1]; var day = arrDate[0]; var year = arrDate[2]; }
        ADate = month + "/" + day + "/" + year;
        if (Validation.isValid(Validation._nbr, day) == false) { Validation._errMessage = _$ndateday; return false; }
        if (Validation.isValid(Validation._nbr, month) == false) { Validation._errMessage = _$ndatemon; return false; }
        if (Validation.isValid(Validation._nbr, year) == false) { Validation._errMessage = _$ndateyr; return false; }
        var NDate = new Date(ADate);
        if (NDate.getDate() != day) { Validation._errMessage = _$dateday; return (false); }
        else if (NDate.getMonth() != month - 1) { Validation._errMessage = _$datemon; return (false); }
        else if (NDate.getFullYear() != year) { Validation._errMessage = _$dateyr; return (false); }
        return true;
    }
    this.ValidateCheckBoxList = function (_cblObj) {
        var _isValidCheck = _cblObj.length > 0 ? false : true;
        if (!_isValidCheck) {
            _isValidCheck = (_cblObj.filter(":" + _cblObj[0].type + ":checked").length > 0) ? true : false;
        }
        return _isValidCheck;
    }
    this.ValidateFilesList = function (_cblObj) {
        var isFileAvil = _cblObj.length > 0 ? false : true;
        if (!isFileAvil) {
            isFileAvil = (_cblObj.filter("[type=" + _cblObj[0].type + "][value!='']").length > 0) ? true : false;
        }
        return isFileAvil;
    }
}
function RValidate(id, msg) { var obj = $("#" + id)[0]; if (Validation.RequiredValidate(obj) == false) { jAlert(msg, "Alert Message"); return false; } return true; }
function _RTrim(Val) { return Val.replace(/\s+$/, ""); }
function ValCallBack() { HideAlertPopup(); }

var jsSession = new function () {
    this.setCookie = function (c_name, value, exdays) {
        var exdate = new Date();
        exdate.setDate(exdate.getDate() + exdays);
        var c_value = escape(value) + ((exdays == null) ? "" : "; expires=" + exdate.toUTCString());
        document.cookie = c_name + "=" + c_value;
    }

    this.getCookie = function (c_name) {
        var i, x, y, ARRcookies = document.cookie.split(";");
        for (i = 0; i < ARRcookies.length; i++) {
            x = ARRcookies[i].substr(0, ARRcookies[i].indexOf("="));
            y = ARRcookies[i].substr(ARRcookies[i].indexOf("=") + 1);
            x = x.replace(/^\s+|\s+$/g, "");
            if (x == c_name) {
                return unescape(y);
            }
        }
    }
}
function UUID() {
    var dt = new Date().getTime();
    var uuid = 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
        var r = (dt + Math.random() * 16) % 16 | 0;
        dt = Math.floor(dt / 16);
        return (c == 'x' ? r : (r & 0x3 | 0x8)).toString(16);
    });
    return uuid;
}
$(document).ready(function () {
    Validation.OnKeyPressInit();
});
