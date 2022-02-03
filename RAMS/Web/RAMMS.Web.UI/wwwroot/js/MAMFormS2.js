var _hd = {
    btnHCancel: $("#btnHCancel"),
    btnHSave: $("#btnHSave"),
    btnHSubmit: $("#btnHSubmit"),
    txtAgreedDate: $("#txtAgreedDate"),
    txtAgreedDesignation: $("#txtAgreedDesignation"),
    txtAgreedName: $("#txtAgreedName"),
    ddlAgreedUserId: $("#ddlAgreedUserId"),
    txtVettedDate: $("#txtVettedDate"),
    txtVettedDesignation: $("#txtVettedDesignation"),
    txtVettedName: $("#txtVettedName"),
    ddlVettedUserId: $("#ddlVettedUserId"),
    txtScheduledDate: $("#txtScheduledDate"),
    txtScheduledDesignation: $("#txtScheduledDesignation"),
    txtScheduledName: $("#txtScheduledName"),
    ddlSchduledUserId: $("#ddlSchduledUserId"),
    txtPrioritizedDate: $("#txtPrioritizedDate"),
    txtPrioritizedDesignation: $("#txtPrioritizedDesignation"),
    txtPrioritizedName: $("#txtPrioritizedName"),
    ddlPrioritizedId: $("#ddlPrioritizedId"),
    btnFindDetails: $("#btnFindDetails"),
    txtReferenceNo: $("#txtReferenceNo"),
    ddlYear: $("#ddlYear"),
    ddlQuarter: $("#ddlQuarter"),
    txtActivityName: $("#txtActivityName"),
    ddlActivityCode: $("#ddlActivityCode"),
    ddlActivityType: $("#ddlActivityType"),
    ddlRmu: $("#ddlRmu"),
    ddlSubmittedUserId: $("#ddlSubmittedUserId"),
    txtSubmittedName: $("#txtSubmittedName"),
    txtSubmittedDesignation: $("#txtSubmittedDesignation"),
    txtSubmittedDate: $("#txtSubmittedDate"),
    HdnHeaderPkId: $("#HdnHeaderPkId"),
    IsAlreadyExists: $("#IsAlreadyExists"),
    btnOpenDetailView: $("#btnOpenDetailView"),
    IsView: $("#IsHView"),
    ValidateID: "#headerFindDiv"
};

var _dt = {
    btnDSaveAndExit: $("#btnDSaveAndExit"),
    btnDSaveAndContinue: $("#btnDSaveAndContinue"),
    btnDCancel: $("#btnDCancel"),
    txtDRemarks: $("#txtDRemarks"),
    txtDTarget: $("#txtDTarget"),
    txtDCRemainingDay: $("#txtDCRemainingDay"),
    txtDCCrewDay: $("#txtDCCrewDay"),
    txtDCTotalDay: $("#txtDCTotalDay"),
    txtDCrewday: $("#txtDCrewday"),
    txtDADP: $("#txtDADP"),
    ddlDPriority: $("#ddlDPriority"),
    txtDRoadLocationSequence: $("#txtDRoadLocationSequence"),
    txtDUnpavedLength: $("#txtDUnpavedLength"),
    txtDPavedLength: $("#txtDPavedLength"),
    ddlDRoadCode: $("#ddlDRoadCode"),
    txtDRoadName: $("#txtDRoadName"),
    txtDReference: $("#txtDReference"),
    hdnDetailPkNo: $("#hdnDetailPkNo"),
    weekDetails: [],
    RoadCodeMaster: [],
    IsView: $("#IsDView"),
    txtWorkQty: $("#txtWorkQty")
}

$(document).ready(function () {
    _hd.ddlActivityCode.chosen();
    _hd.ddlActivityType.chosen();
    _hd.ddlAgreedUserId.chosen();
    _hd.ddlPrioritizedId.chosen();
    _hd.ddlQuarter.chosen();
    _hd.ddlRmu.chosen();
    _hd.ddlSchduledUserId.chosen();
    _hd.ddlVettedUserId.chosen();
    _hd.ddlYear.chosen();
    _hd.ddlSubmittedUserId.chosen();
    _hd.btnOpenDetailView.hide();
    _hd.btnHSave.hide();
    _hd.btnHSubmit.hide();
    _hd.ddlRmu.focus();
    _hd.ddlRmu.trigger("chosen:updated");
    _hd.ddlRmu.on("change", function () {
        bindRoadCode();
    });
    _hd.ddlYear.on("change", function () {
        bindQuarter();
        generateHeaderReference();
    });
    _hd.ddlPrioritizedId.on("change", function () {
        var value = this.value;

        if (value == "") {
            _hd.txtPrioritizedName.val('');
            _hd.txtPrioritizedDesignation.val('');
            _hd.txtPrioritizedName.prop("disabled", true);
            _hd.txtPrioritizedDesignation.prop("disabled", true);
        }
        else if (value == "99999999") {
            _hd.txtPrioritizedName.val('');
            _hd.txtPrioritizedDesignation.val('');
            _hd.txtPrioritizedName.prop("disabled", false);
            _hd.txtPrioritizedDesignation.prop("disabled", false);
        }
        else {
            getUserDetail(value, function (data) {
                _hd.txtPrioritizedName.val(data.userName);
                _hd.txtPrioritizedDesignation.val(data.position);
                _hd.txtPrioritizedName.prop("disabled", true);
                _hd.txtPrioritizedDesignation.prop("disabled", true);
            });
        }
    });

    _hd.ddlAgreedUserId.on("change", function () {
        var value = this.value;
        if (value == "") {
            _hd.txtAgreedName.val('');
            _hd.txtAgreedDesignation.val('');
            _hd.txtAgreedName.prop("disabled", true);
            _hd.txtAgreedDesignation.prop("disabled", true);
        }
        else if (value == "99999999") {
            _hd.txtAgreedName.val('');
            _hd.txtAgreedDesignation.val('');
            _hd.txtAgreedName.prop("disabled", false);
            _hd.txtAgreedDesignation.prop("disabled", false);
        }
        else {
            getUserDetail(value, function (data) {
                _hd.txtAgreedName.val(data.userName);
                _hd.txtAgreedDesignation.val(data.position);
                _hd.txtAgreedName.prop("disabled", true);
                _hd.txtAgreedDesignation.prop("disabled", true);
            });
        }
    });

    _hd.ddlSchduledUserId.on("change", function () {
        var value = this.value;
        if (value == "") {
            _hd.txtScheduledName.val('');
            _hd.txtScheduledDesignation.val('');
            _hd.txtScheduledName.prop("disabled", true);
            _hd.txtScheduledDesignation.prop("disabled", true);
        }
        else if (value == "99999999") {
            _hd.txtScheduledName.val('');
            _hd.txtScheduledDesignation.val('');
            _hd.txtScheduledName.prop("disabled", false);
            _hd.txtScheduledDesignation.prop("disabled", false);
        }
        else {
            getUserDetail(value, function (data) {
                _hd.txtScheduledName.val(data.userName);
                _hd.txtScheduledDesignation.val(data.position);
                _hd.txtScheduledName.prop("disabled", true);
                _hd.txtScheduledDesignation.prop("disabled", true);
            });
        }
    });

    _hd.ddlVettedUserId.on("change", function () {
        var value = this.value;
        if (value == "") {
            _hd.txtVettedName.val('');
            _hd.txtVettedDesignation.val('');
            _hd.txtVettedName.prop("disabled", true);
            _hd.txtVettedDesignation.prop("disabled", true);
        }
        else if (value == "99999999") {
            _hd.txtVettedName.val('');
            _hd.txtVettedDesignation.val('');
            _hd.txtVettedName.prop("disabled", false);
            _hd.txtVettedDesignation.prop("disabled", false);
        }
        else {
            getUserDetail(value, function (data) {
                _hd.txtVettedName.val(data.userName);
                _hd.txtVettedDesignation.val(data.position);
                _hd.txtVettedName.prop("disabled", true);
                _hd.txtVettedDesignation.prop("disabled", true);
            });
        }
    });

    _hd.ddlSubmittedUserId.on("change", function () {
        var value = this.value;
        if (value == "") {
            _hd.txtSubmittedName.val('');
            _hd.txtSubmittedDesignation.val('');
            _hd.txtSubmittedName.prop("disabled", true);
            _hd.txtSubmittedDesignation.prop("disabled", true);
        }
        else if (value == "99999999") {
            _hd.txtSubmittedName.val('');
            _hd.txtSubmittedDesignation.val('');
            _hd.txtSubmittedName.prop("disabled", false);
            _hd.txtSubmittedDesignation.prop("disabled", false);
        }
        else {
            getUserDetail(value, function (data) {
                _hd.txtSubmittedName.val(data.userName);
                _hd.txtSubmittedDesignation.val(data.position);
                _hd.txtSubmittedName.prop("disabled", true);
                _hd.txtSubmittedDesignation.prop("disabled", true);
            });
        }
    });

    _hd.ddlActivityCode.on("change", function () {
        var value = this.value;
        if (value != "") {
            var txt = $(this).find("option:selected").text().split("-")[1];
            _hd.txtActivityName.val(txt);
            generateHeaderReference();
        }
        else {
            _hd.txtActivityName.val("");
        }
    });

    _hd.ddlRmu.on("change", function () { generateHeaderReference(); });
    _hd.ddlQuarter.on("change", function () { generateHeaderReference(); });

    _hd.btnFindDetails.on("click", function () {
        //debugger;
        if (_hd.IsAlreadyExists.val() == "1") {
            window.location.href = "/FormS2/AddS2?id=" + _hd.HdnHeaderPkId.val() + "&isview=0";
        }
        else {

            if (ValidatePage(_hd.ValidateID)) {
                saveHeader(false);
            }

        }

    });

    _hd.btnHSave.on("click", function () {
        saveHeader();
    });

    _hd.btnHSubmit.on("click", function () {
        saveHeader(true, true);
    });

    //if (_hd.ddlPrioritizedId.val() != "") {
    //    getUserDetail(_hd.ddlPrioritizedId.val(), function (data) {
    //        _hd.txtPrioritizedName.val(data.userName);
    //        _hd.txtPrioritizedDesignation.val(data.position);
    //    });
    //}

    //if (_hd.ddlAgreedUserId.val() != "") {
    //    getUserDetail(_hd.ddlAgreedUserId.val(), function (data) {
    //        _hd.txtAgreedName.val(data.userName);
    //        _hd.txtAgreedDesignation.val(data.position);
    //    });
    //}

    //if (_hd.ddlVettedUserId.val() != "" && _hd.ddlVettedUserId.val() != "99999999") {
    //    getUserDetail(_hd.ddlVettedUserId.val(), function (data) {
    //        _hd.txtVettedName.val(data.userName);
    //        _hd.txtVettedDesignation.val(data.position);
    //    });
    //}

    //if (_hd.ddlSubmittedUserId.val() != "" && _hd.ddlSubmittedUserId.val() != "999999999") {
    //    getUserDetail(_hd.ddlSubmittedUserId.val(), function (data) {
    //        _hd.txtSubmittedName.val(data.userName);
    //        _hd.txtSubmittedDesignation.val(data.position);
    //    });
    //}
    //if (_hd.ddlSchduledUserId.val() != "" && _hd.ddlSchduledUserId.val() != "999999999") {
    //    getUserDetail(_hd.ddlSchduledUserId.val(), function (data) {
    //        _hd.txtScheduledName.val(data.userName);
    //        _hd.txtScheduledDesignation.val(data.position);
    //    });
    //}

    if (_hd.ddlActivityCode.val() != "") {
        var txt = _hd.ddlActivityCode.find("option:selected").text().split("-")[1];
        _hd.txtActivityName.val(txt);
    }

    if (_hd.ddlRmu.val() != "") {
        bindRoadCode();
    }

    _hd.btnOpenDetailView.on("click", function () {
        openDetail(0);
    });

    _dt.btnDSaveAndContinue.on("click", function () {
        saveDetail(false);
    });

    _dt.btnDSaveAndExit.on("click", function () {
        saveDetail(true);
    });

    _hd.btnHCancel.on("click", function () {
        if (_hd.IsView.val() == "1") {
            window.location.href = "/FormS2";
        }
        else if (app.Confirm("Unsaved changes will be lost. Are you sure you want to cancel?", function (e) {
            if (e) {
                window.location.href = "/FormS2";
            }
        }));
    });

    _dt.btnDCancel.on("click", function () {
        if (_dt.IsView.val() == "1") {
            ClearDetail();
            $("#FormS2AdddetailsModal").modal("hide");
        }
        else if (app.Confirm("Unsaved changes will be lost. Are you sure you want to cancel?", function (e) {
            if (e) {
                ClearDetail();
                $("#FormS2AdddetailsModal").modal("hide");
            }
        }));
    });

    _dt.txtDCrewday.on("change", function () {
        _dt.txtDCCrewDay.val(this.value);
        var days = getDaysInQuarter(_hd.ddlQuarter.val(), _hd.ddlYear.val());
        _dt.txtDCTotalDay.val(days);
        var totalDaysinquarter = TryParseInt(_dt.txtDCTotalDay.val());
        var crewDays = TryParseInt(_dt.txtDCCrewDay.val());
        totalDaysinquarter = totalDaysinquarter != undefined ? totalDaysinquarter : 0;
        crewDays = crewDays != undefined ? crewDays : 0;
        var result = crewDays * totalDaysinquarter;
        //alert(result);
        _dt.txtDCRemainingDay.val(result);
    });

    _dt.txtDCTotalDay.on("change", function () {
        var totalDaysinquarter = TryParseInt(this.value);
        var crewDays = TryParseInt(_dt.txtDCCrewDay.val());
        totalDaysinquarter = totalDaysinquarter != undefined ? totalDaysinquarter : 0;
        crewDays = crewDays != undefined ? crewDays : 0;
        var result = crewDays * totalDaysinquarter;
        _dt.txtDCRemainingDay.val(result);
    });

    _dt.txtDCCrewDay.on("change", function () {
        var totalDaysinquarter = TryParseInt(_dt.txtDCTotalDay.val());
        var crewDays = TryParseInt(_dt.txtDCCrewDay.val());
        totalDaysinquarter = totalDaysinquarter != undefined ? totalDaysinquarter : 0;
        crewDays = crewDays != undefined ? crewDays : 0;
        var result = crewDays * totalDaysinquarter;
        _dt.txtDCRemainingDay.val(result);
    });

    if (_hd.IsAlreadyExists.val() == "1") {
        _hd.ddlActivityCode.prop("disabled", true);
        _hd.ddlActivityCode.trigger("chosen:updated");
        _hd.ddlRmu.prop("disabled", true);
        _hd.ddlRmu.trigger("chosen:updated");
        _hd.ddlQuarter.prop("disabled", true);
        _hd.ddlQuarter.trigger("chosen:updated");
        _hd.ddlYear.prop("disabled", true);
        _hd.ddlYear.trigger("chosen:updated");
        _hd.btnFindDetails.hide();
    }

    _dt.ddlDRoadCode.on("change", function () {
        getRoadDetail();
    });

    if (_hd.HdnHeaderPkId.val() != "0") {
        _hd.btnOpenDetailView.show();
        _hd.btnHSave.show();
        _hd.btnHSubmit.show();
    }

    if (_hd.IsView.val() == "1") {
        $(".view").prop("disabled", true);
        $(".view").trigger("chosen:updated");
        _hd.btnOpenDetailView.hide();
    }
});

var getDaysInMonth = function (month, year) {
    return new Date(year, month, 0).getDate();
};

var getDaysInQuarter = function (quarter, year) {

    switch (quarter) {
        case "1":
            return getDaysInMonth(1, year) + getDaysInMonth(2, year) + getDaysInMonth(3, year);
            break;
        case "2":
            return getDaysInMonth(4, year) + getDaysInMonth(5, year) + getDaysInMonth(6, year);
            break;
        case "3":
            return getDaysInMonth(7, year) + getDaysInMonth(8, year) + getDaysInMonth(9, year);
            break;
        case "4":
            return getDaysInMonth(11, year) + getDaysInMonth(10, year) + getDaysInMonth(12, year);
            break;
    }
}

function getRoadDetail() {
    var req = {};
    req.id = _dt.ddlDRoadCode.val();
    _dt.txtDUnpavedLength.val('');
    _dt.txtDPavedLength.val('');
    _dt.txtDRoadName.val('');

    $.ajax({
        url: '/FormS2/CheckS2DtlExist',
        data: { headerId: _hd.HdnHeaderPkId.val(), RdCode: +req.id },
        type: 'Post',
        success: function (data) {
            if (data) {
                $("#btnDSaveAndContinue").prop("disabled", true);
                $("#btnDSaveAndExit").prop("disabled", true);
                app.ShowErrorMessage("This road Code is already exist. Please choose another one.");
            }
            else {
                $("#btnDSaveAndContinue").prop("disabled", false);
                $("#btnDSaveAndExit").prop("disabled", false);
                $.each(_dt.RoadCodeMaster, function (index, value) {
                    if (value.no == req.id) {
                        _dt.txtDUnpavedLength.val(value.lengthUnpaved);
                        _dt.txtDPavedLength.val(value.lengthPaved);
                        _dt.txtDRoadName.val(value.roadName);
                        return;
                    }
                });
            }
        },
        error: function (data) {
            console.error(data);
        }
    });
}

function saveHeader(isSave = true, isSubmit = false) {

    if (isSubmit) {
        //_hd.ddlVettedUserId.addClass("validate {visreq, Vetted by }");
        //_hd.txtVettedDate.addClass("validate {visreq, Vetted Date}");
        //_hd.txtVettedName.addClass("validate {visreq, Vetted Name}");
        //_hd.txtVettedDesignation.addClass("validate {visrequired, Vetted Designation}");
        _hd.ddlPrioritizedId.addClass("validate {required, Prioritized by}");
        _hd.txtPrioritizedDate.addClass("validate {required, Prioritized Date}");
        _hd.txtPrioritizedDesignation.addClass("validate {required, Prioritized Designation}");
        _hd.txtPrioritizedName.addClass("validate {required, Prioritized Name}");
        _hd.ddlSchduledUserId.addClass("validate {required, Scheduled by}");
        _hd.txtScheduledDate.addClass("validate {required, Scheduled Date}");
        _hd.txtScheduledDesignation.addClass("validate {required, Scheduled Designation}");
        _hd.txtScheduledName.addClass("validate {required, Scheduled Name}");
        _hd.ddlSubmittedUserId.addClass("validate {required, Submitted by}");
        _hd.txtSubmittedDate.addClass("validate {required, Submitted Date}");
        _hd.txtSubmittedDesignation.addClass("validate {required, Submitted Designation}");
        _hd.txtSubmittedName.addClass("validate {required, Submitted Name}");
        //_hd.ddlAgreedUserId.addClass("validate {required, Agreed by}");
        //_hd.txtAgreedName.addClass("validate {required, Agreed Name}");
        //_hd.txtAgreedDesignation.addClass("validate {required, Agreed Designation}");
        //_hd.txtAgreedDate.addClass("validate {required, Agreed Date}");
    }
    else {
        _hd.ddlVettedUserId.removeClass("validate {required, Vetted by }");
        _hd.txtVettedDate.removeClass("validate {required, Vetted Date}");
        _hd.txtVettedName.removeClass("validate {required, Vetted Name}");
        _hd.txtVettedDesignation.removeClass("validate {required, Vetted Designation}");
        _hd.ddlPrioritizedId.removeClass("validate {required, Prioritized by}");
        _hd.txtPrioritizedDate.removeClass("validate {required, Prioritized Date}");
        _hd.txtPrioritizedDesignation.removeClass("validate {required, Prioritized Designation}");
        _hd.txtPrioritizedName.removeClass("validate {required, Prioritized Name}");
        _hd.ddlSchduledUserId.removeClass("validate {required, Scheduled by}");
        _hd.txtScheduledDate.removeClass("validate {required, Scheduled Date}");
        _hd.txtScheduledDesignation.removeClass("validate {required, Scheduled Designation}");
        _hd.txtScheduledName.removeClass("validate {required, Scheduled Name}");
        _hd.ddlSubmittedUserId.removeClass("validate {required, Submitted by}");
        _hd.txtSubmittedDate.removeClass("validate {required, Submitted Date}");
        _hd.txtSubmittedDesignation.removeClass("validate {required, Submitted Designation}");
        _hd.txtSubmittedName.removeClass("validate {required, Submitted Name}");
        _hd.ddlAgreedUserId.removeClass("validate {required, Agreed by}");
        _hd.txtAgreedName.removeClass("validate {required, Agreed Name}");
        _hd.txtAgreedDesignation.removeClass("validate {required, Agreed Designation}");
        _hd.txtAgreedDate.removeClass("validate {required, Agreed Date}");
    }

    if (ValidatePage("#headerDiv", "", "validate")) {

        var req = {};
        req.PkRefNo = _hd.HdnHeaderPkId.val();
        req.Rmu = _hd.ddlRmu.val();
        req.SubmitSts = isSubmit;
        req.ActId = _hd.ddlActivityCode.val();
        req.Year = _hd.ddlYear.val();
        req.QuaterId = _hd.ddlQuarter.val();
        req.RefId = _hd.txtReferenceNo.val();
        req.UseridPrioritised = _hd.ddlPrioritizedId.val();
        req.DtPrioritised = _hd.txtPrioritizedDate.val();
        req.DtSchld = _hd.txtScheduledDate.val();
        req.DtSub = _hd.txtSubmittedDate.val();
        req.DtVet = _hd.txtVettedDate.val();
        req.DtAgrd = _hd.txtAgreedDate.val();
        req.UseridSub = _hd.ddlSubmittedUserId.val();
        req.UseridVet = _hd.ddlVettedUserId.val();
        req.UseridAgrd = _hd.ddlAgreedUserId.val();
        req.UseridSchld = _hd.ddlSchduledUserId.val();
        //req.ActCode = _hd.ddlActivityCode.val();
        if (req.ActId != "") {
           // req.ActName = _hd.ddlActivityCode.text();
            req.ActCode = _hd.ddlActivityCode.find("option:selected").text().split("-")[0];
            req.ActName = _hd.ddlActivityCode.find("option:selected").text().split("-")[1];
        }
        req.UserNamePrioritised = _hd.txtPrioritizedName.val();
        req.UserDesignationPrioritised = _hd.txtPrioritizedDesignation.val();
        req.UserNameSchId = _hd.txtScheduledName.val();
        req.UserDesignationSchId = _hd.txtScheduledDesignation.val();
        req.UserNameSub = _hd.txtSubmittedName.val();
        req.UserDesignationSub = _hd.txtSubmittedDesignation.val();
        req.UserNameVet = _hd.txtVettedName.val();
        req.UserDesignationVet = _hd.txtVettedDesignation.val();
        req.UserNameAgrd = _hd.txtAgreedName.val();
        req.UserDesignationAgrd = _hd.txtAgreedDesignation.val();
        InitAjaxLoading();
        $.ajax({
            url: '/FormS2/SaveS2Header',
            dataType: 'JSON',
            data: req,
            type: 'Post',
            success: function (data) {
                HideAjaxLoading();
                if (data.id > 0) {
                    _hd.HdnHeaderPkId.val(data.id);
                    _hd.txtReferenceNo.val(data.refid);
                    _hd.ddlActivityCode.prop("disabled", true);
                    _hd.ddlActivityCode.trigger("chosen:updated");
                    _hd.ddlRmu.prop("disabled", true);
                    _hd.ddlRmu.trigger("chosen:updated");
                    _hd.ddlQuarter.prop("disabled", true);
                    _hd.ddlQuarter.trigger("chosen:updated");
                    _hd.ddlYear.prop("disabled", true);
                    _hd.ddlYear.trigger("chosen:updated");
                    _hd.btnFindDetails.hide();
                    _hd.btnHSave.show();
                    _hd.btnHSubmit.show();
                    if (isSave) {
                        app.ShowSuccessMessage("Saved Successfully");
                        window.location.href = "/FormS2";
                    }
                    _hd.btnOpenDetailView.show();
                }
                else {
                    _hd.HdnHeaderPkId.val('');
                    _hd.btnOpenDetailView.hide();
                }
            },
            error: function (data) {
                HideAjaxLoading();
                console.error(data);
            }
        });
    }
}

function bindQuarter() {
    var req = {};
    req.year = _hd.ddlYear.val();
    //$.ajax({
    //    url: '/FormS2/GetQuarter',
    //    dataType: 'JSON',
    //    data: req,
    //    type: 'Post',
    //    success: function (data) {
    //        _hd.ddlQuarter.empty();
    //        _hd.ddlQuarter.append($("<option></option>").val("").html("Select Quarter"));
    //        $.each(data, function (index, v) {
    //            _hd.ddlQuarter.append($("<option></option>").val(v.value).html(v.text));
    //        });

    //        _hd.ddlQuarter.trigger("chosen:updated");
    //    },
    //    error: function (data) {

    //        console.error(data);
    //    }
    //});
}

function generateHeaderReference() {
    //debugger;
    var req = {};
    if (_hd.ddlRmu.val() != "" && _hd.ddlActivityCode.val() != ""
        && _hd.ddlYear.val() != "" && _hd.ddlQuarter.val() != "") {
        req.rmu = _hd.ddlRmu.val();
        req.year = _hd.ddlYear.val();
        req.activitycode = _hd.ddlActivityCode.val();
        req.quarter = _hd.ddlQuarter.val();
        InitAjaxLoading();
        $.ajax({
            url: '/FormS2/GetHeaderLastInsertedNo',
            dataType: 'JSON',
            data: req,
            type: 'Post',
            success: function (data) {
                HideAjaxLoading();
                var reference = 'MM/Form S2/';
                reference += _hd.ddlRmu.val();
                reference += "/";
                reference += _hd.ddlQuarter.val();
                reference += "/";
                reference += _hd.ddlYear.val();
                reference += "/";
                reference += _hd.ddlActivityCode.val();
                reference += "/";
                reference += "???";
                _hd.txtReferenceNo.val(reference);
                var res = data.id + (data.aleadyexists ? 0 : 1);

                if (data.aleadyexists) {
                    _hd.HdnHeaderPkId.val(data.id);
                    _hd.IsAlreadyExists.val("1");
                }
                else {
                    _hd.HdnHeaderPkId.val('');
                    _hd.IsAlreadyExists.val("0");
                }
            },
            error: function (data) {
                HideAjaxLoading();
                console.log("getLastInsertedHeaderNo", data);
            }
        });
    }
    else {
        _hd.txtReferenceNo.val("");
    }
}

function getUserDetail(id, callback) {
    var req = {};
    req.id = id;
    $.ajax({
        url: '/NOD/GetUserById',
        dataType: 'JSON',
        data: req,
        type: 'Post',
        success: function (data) {
            callback(data);
        },
        error: function (data) {
            console.error(data);
        }
    });
}

function openDetail(id) {
    //debugger;
    var req = {};
    req.id = id;
    InitAjaxLoading();
    $.ajax({
        url: '/FormS2/GetS2DetailById',
        dataType: 'JSON',
        data: req,
        type: 'Post',
        success: function (data) {
           // debugger;
            HideAjaxLoading();
            if (data.pkRefNo == 0) {
                // generateDetailReferenceId(_hd.HdnHeaderPkId.val());
                var _headerRef = _hd.txtReferenceNo.val();
                _headerRef += "/???";
                _dt.txtDReference.val(_headerRef);
            }
            else {
                _dt.txtDReference.val(data.refId);
            }
            _dt.hdnDetailPkNo.val(data.pkRefNo);

            _dt.ddlDRoadCode.val(data.roadId);
            _dt.ddlDRoadCode.trigger("chosen:updated");
            _dt.txtDPavedLength.val(data.pavedlength);
            _dt.txtDUnpavedLength.val(data.unPavedlength);
            _dt.txtDRoadName.val(data.roadName);
            _dt.txtDRoadLocationSequence.val(data.rdLocSeq);
            _dt.txtDCrewday.val(data.crwDaysReq);
            _dt.txtDCCrewDay.val(data.crwDaysReq);
            _dt.txtDCTotalDay.val(data.crwAllwcdQuar);
            _dt.txtDADP.val(data.adp);
            _dt.txtDTarget.val(data.targetPercent);
            _dt.txtDRemarks.val(data.remarks);
            _dt.ddlDPriority.val(data.priority);
            _dt.ddlDPriority.trigger("chosen:updated");
            _dt.weekDetails = data.weekDetail != null ? data.weekDetail : [];
            _dt.txtWorkQty.val(data.workQty);
            //getRoadDetail();
            bindWeekDetail();
            _dt.ddlDRoadCode.focus();
            var totalDaysinquarter = TryParseInt(_dt.txtDCTotalDay.val());
            var crewDays = TryParseInt(_dt.txtDCCrewDay.val());
            totalDaysinquarter = totalDaysinquarter != undefined ? totalDaysinquarter : 0;
            crewDays = crewDays != undefined ? crewDays : 0;
            var result = crewDays * totalDaysinquarter;
            //_dt.txtDCRemainingDay.val((result != NaN ? result : ''));
            _dt.txtDCRemainingDay.val((result == "0" ? '' : result));

        },
        error: function (data) {
            HideAjaxLoading();
            console.error(data);
        }
    });

}

function viewDetail(id) {
    _dt.IsView.val("1");
    openDetail(id);
    $("#FormS2AdddetailsModal").modal("show");
    _dt.btnDSaveAndContinue.hide();
    _dt.btnDSaveAndExit.hide();
    $(".viewD").prop("disabled", true);
}

function generateDetailReferenceId(headerid) {
    var req = {};
    req.header = headerid;
    InitAjaxLoading();
    $.ajax({
        url: '/FormS2/GetDetailLastInsertedNo',
        dataType: 'JSON',
        data: req,
        type: 'Post',
        success: function (data) {
            HideAjaxLoading();
            var _headerRef = _hd.txtReferenceNo.val();
            _headerRef += "/";
            _headerRef += data + 1;
            _dt.txtDReference.val(_headerRef);

        },
        error: function (data) {
            HideAjaxLoading();
            console.error(data);
        }
    });
}

function bindWeekDetail() {
    var req = {};
    req.year = _hd.ddlYear.val();
    req.quarter = _hd.ddlQuarter.val();
    $("#spanYear").text(_hd.ddlYear.val());
    $.ajax({
        url: '/FormS2/GetWeeks',
        dataType: 'JSON',
        data: req,
        type: 'Post',
        success: function (data) {
            var body = '';
            $.each(data, function (index, value) {
                var subbody = '<div class="flex-md-fill sch-colgroup"><div class="month-title">';
                subbody += value.name;
                subbody += '</div><ul>';
                $.each(value.week, function (sIndex, sValue) {
                    if (_dt.weekDetails.indexOf(parseInt(sValue.value)) > -1) {
                        subbody += '<li><button class="active" type="button" id="';
                    }
                    else {
                        subbody += '<li><button type="button" id="';
                    }

                    subbody += sValue.value;
                    subbody += '"><span class="week-name">W';
                    subbody += sValue.text;
                    subbody += '</span></button></li>';
                });
                subbody += "</ul></div>";
                body += subbody;

            });
            $("#divWeeks").html(body);
            $('.sch-colgroup li button').on('click', function () {

                if (_dt.IsView.val() != "1") {
                    $(this).toggleClass('active')
                    var _id = this.id;
                    _dt.weekDetails = _dt.weekDetails == null ? [] : _dt.weekDetails;
                    if ($(this).hasClass('active')) {
                        _dt.weekDetails.push(_id);
                    }
                    else {
                        const index = _dt.weekDetails.indexOf(parseInt(_id));
                        if (index > -1) {
                            _dt.weekDetails.splice(index, 1);
                        }
                    }
                }

                console.log(_dt);
            });
        },
        error: function (data) {

            console.error(data);
        }
    });
}

function bindRoadCode() {
    var req = {};
    req.rmu = _hd.ddlRmu.val();
    $.ajax({
        url: '/FormS2/GetRoadListByRmu',
        dataType: 'JSON',
        data: req,
        type: 'Post',
        success: function (data) {
            _dt.RoadCodeMaster = data;
            _dt.ddlDRoadCode.empty();
            _dt.ddlDRoadCode.append($("<option></option>").val("").html("Select Road Code"));
            $.each(data, function (index, v) {
                var text = v.roadCode + " - " + v.roadName;
                _dt.ddlDRoadCode.append($("<option></option>").val(v.no).html(text));
            });

            _dt.ddlDRoadCode.trigger("chosen:updated");
        },
        error: function (data) {

            console.error(data);
        }
    });
}

function saveDetail(isSaveAndExit = false) {
    //debugger;
    if (ValidatePage("#FormAddDetails")) {
        var req = {};
        req.HeaderPkRefNo = _hd.HdnHeaderPkId.val();
        req.PkRefNo = _dt.hdnDetailPkNo.val();
        req.RefId = _dt.txtDReference.val();
        req.RoadId = _dt.ddlDRoadCode.val();
        req.RoadName = _dt.txtDRoadName.val();
        req.Pavedlength = _dt.txtDPavedLength.val();
        req.UnPavedlength = _dt.txtDUnpavedLength.val();
        req.RdLocSeq = _dt.txtDRoadLocationSequence.val();
        req.CrwDaysReq = _dt.txtDCrewday.val();
        req.CrwAllwcdQuar = _dt.txtDCTotalDay.val();
        req.Adp = _dt.txtDADP.val();
        req.TargetPercent = _dt.txtDTarget.val();
        req.Remarks = _dt.txtDRemarks.val();
        req.WeekDetail = _dt.weekDetails;
        req.Priority = _dt.ddlDPriority.val();
        req.WorkQty = _dt.txtWorkQty.val();
        InitAjaxLoading();
        $.ajax({
            url: '/FormS2/SaveS2Detail',
            dataType: 'JSON',
            data: req,
            type: 'Post',
            success: function (data) {
                HideAjaxLoading();
                if (data > 0) {
                    app.ShowSuccessMessage("Saved Successfully");
                    ClearDetail();
                }

                if (isSaveAndExit) {
                    ClearDetail();
                    $("#FormS2AdddetailsModal").modal("hide");
                }
                InitializeDetailsGrid();
            },
            error: function (data) {
                HideAjaxLoading();
                console.error(data);
            }
        });
    }
}

function ClearDetail() {

    //generateDetailReferenceId(_hd.HdnHeaderPkId.val());
    var _headerRef = _hd.txtReferenceNo.val();
    _headerRef += "/???";
    _dt.txtDReference.val(_headerRef);
    _dt.hdnDetailPkNo.val('0');
    _dt.txtDRoadName.val('');
    _dt.ddlDRoadCode.val('');
    _dt.ddlDRoadCode.trigger("chosen:updated");
    _dt.ddlDPriority.val('');
    _dt.ddlDPriority.trigger("chosen:updated");
    _dt.txtDRoadLocationSequence.val('');
    _dt.txtDPavedLength.val('');
    _dt.txtDUnpavedLength.val('');
    _dt.txtDCrewday.val('');
    _dt.txtDCTotalDay.val('');
    _dt.txtDADP.val('');
    _dt.txtDTarget.val('');
    _dt.txtDCRemainingDay.val('');
    _dt.txtDCCrewDay.val('');
    _dt.txtDRemarks.val('');
    _dt.weekDetails = [];
    _dt.txtWorkQty.val('');
    bindWeekDetail();
}