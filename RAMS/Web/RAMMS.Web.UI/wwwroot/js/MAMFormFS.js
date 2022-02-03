var _hd = {
    hdnHeaderId: $("#hdnHeaderId"),
    ddlFormType: $("#ddlFormType"),
    ddlYear: $("#ddlYear"),
    txtDist: $("#txtDist"),
    ddlRmuCode: $("#ddlRMU"),
    ddlSection: $("#ddlSection"),
    txtSectionName: $("#txtSectionName"),
    ddlRoadCode: $("#ddlRoadCode"),
    txtRoadName: $("#txtRoadName"),
    txtRoadlength: $("#txtRoadlength"),
    btnFindDetails: $("#btnFindDetails"),
    ddlCrewleader: $("#ddlCrewleader"),
    txtCrewLeaderName: $("#txtCrewLeaderName"),
    ddlInspectedby: $("#ddlInspectedby"),
    txtInspectedbyName: $("#txtInspectedbyName"),
    txtInspectedDesignation: $("#txtInspectedDesignation"),
    txtInspectedDate: $("#txtInspectedDate"),
    ddlCheckedby: $("#ddlCheckedby"),
    txtCheckedbyName: $("#txtCheckedbyName"),
    txtCheckedDesignation: $("#txtCheckedDesignation"),
    txtCheckedDate: $("#txtCheckedDate"),
    ddlSmzdby: $("#ddlSmzdby"),
    txtSmzdbyName: $("#txtSmzdbyName"),
    txtSmzdDesignation: $("#txtSmzdDesignation"),
    txtSmzdDate: $("#txtSmzdDate"),
    btnHCancel: $("#btnHCancel"),
    btnHSave: $("#btnHSave"),
    btnHSubmit: $("#btnHSubmit"),
    DtChckdBy: $("#txtCheckedDate"),
    txtHReferenceNo: $("#txtHReferenceNo"),
    hdnHIsViewMode: $("#hdnHIsViewMode")
};

$(document).ready(function () {

    _hd.ddlFormType.chosen();
    _hd.ddlYear.chosen();
    _hd.ddlRmuCode.chosen();
    _hd.ddlSection.chosen();
    _hd.ddlRoadCode.chosen();
    _hd.ddlCrewleader.chosen();
    _hd.ddlInspectedby.chosen();
    _hd.ddlSmzdby.chosen();
    _hd.ddlCheckedby.chosen();
    _hd.btnHSave.hide();
    _hd.btnHSubmit.hide();

    _hd.ddlInspectedby.on("change", function () {
        var value = this.value;
        if (value == "") {
            _hd.txtInspectedbyName.val('');
            _hd.txtInspectedDesignation.val('');
            _hd.txtInspectedbyName.prop("disabled", true);
            _hd.txtInspectedDesignation.prop("disabled", true);
        }
        else if (value == "99999999") {
            _hd.txtInspectedbyName.val('');
            _hd.txtInspectedDesignation.val('');
            _hd.txtInspectedbyName.prop("disabled", false);
            _hd.txtInspectedDesignation.prop("disabled", false);
        }
        else {
            getUserDetail(value, function (data) {
                _hd.txtInspectedbyName.val(data.userName);
                _hd.txtInspectedDesignation.val(data.position);
                _hd.txtInspectedbyName.prop("disabled", true);
                _hd.txtInspectedDesignation.prop("disabled", true);
            });
        }
    });

    _hd.ddlSmzdby.on("change", function () {
        var value = this.value;
        if (value == "") {
            _hd.txtSmzdbyName.val('');
            _hd.txtSmzdDesignation.val('');
            _hd.txtSmzdbyName.prop("disabled", true);
            _hd.txtSmzdDesignation.prop("disabled", true);
        }
        else if (value == "99999999") {
            _hd.txtSmzdbyName.val('');
            _hd.txtSmzdDesignation.val('');
            _hd.txtSmzdbyName.prop("disabled", false);
            _hd.txtSmzdDesignation.prop("disabled", false);
        }
        else {
            getUserDetail(value, function (data) {
                _hd.txtSmzdbyName.val(data.userName);
                _hd.txtSmzdDesignation.val(data.position);
                _hd.txtSmzdbyName.prop("disabled", true);
                _hd.txtSmzdDesignation.prop("disabled", true);
            });
        }
    });

    _hd.ddlCheckedby.on("change", function () {
        var value = this.value;
        if (value == "") {
            _hd.txtCheckedbyName.val('');
            _hd.txtCheckedDesignation.val('');
            _hd.txtCheckedbyName.prop("disabled", true);
            _hd.txtCheckedDesignation.prop("disabled", true);
        }
        else if (value == "99999999") {
            _hd.txtCheckedbyName.val('');
            _hd.txtCheckedDesignation.val('');
            _hd.txtCheckedbyName.prop("disabled", false);
            _hd.txtCheckedDesignation.prop("disabled", false);
        }
        else {
            getUserDetail(value, function (data) {
                _hd.txtCheckedbyName.val(data.userName);
                _hd.txtCheckedDesignation.val(data.position);
                _hd.txtCheckedbyName.prop("disabled", true);
                _hd.txtCheckedDesignation.prop("disabled", true);
            });
        }
    });

    _hd.ddlCrewleader.on("change", function () {
        var value = this.value;
        if (value == "") {
            _hd.txtCrewLeaderName.val('');
            _hd.txtCrewLeaderName.prop("disabled", true);
        }
        else if (value == "99999999") {
            _hd.txtCrewLeaderName.val('');
            _hd.txtCrewLeaderName.prop("disabled", false);
        }
        else {
            getUserDetail(value, function (data) {
                _hd.txtCrewLeaderName.val(data.userName);
                _hd.txtCrewLeaderName.prop("disabled", true);
            });
        }
    });

    _hd.ddlRmuCode.on("change", function () {
        if (this.value == "") {
            _hd.txtRoadlength.val("");
            _hd.ddlSection.val("");

        }
        else {
            bindSection();
            bindRoadCode();
        }
    });

    _hd.ddlSection.on("change", function () {
        bindRoadCode();
        if (this.value == "") {
            _hd.txtSectionName.val('');
        }
        else {
            _hd.txtSectionName.val(_hd.ddlSection.find("option:selected").text().split("-")[1]);
        }

    });

    _hd.ddlRoadCode.on("change", function () {
        if (this.value == "") {
            _hd.txtRoadName.val("");
        }
        else {
            getRoadDetail();
            var roadname = _hd.ddlRoadCode.find("option:selected").text().split('-');
            if (roadname.length > 2) {
                _hd.txtRoadName.val(roadname[2]);
            }
            else {
                _hd.txtRoadName.val(roadname[1]);
            }
        }
    });

    _hd.btnHSave.on("click", function () {
        saveHeader(true, false);
    });

    _hd.btnHSubmit.on("click", function () {
        saveHeader(true, true);
    });

    _hd.btnHCancel.on("click", function () {
        if (_hd.hdnHIsViewMode.val() != "1") {
            app.Confirm("Unsaved changes will be lost. Are you sure you want to cancel?", function (e) {
                if (e) {
                    window.location = _APPLocation + "FormFS";
                }
            });
        }
        else {
            window.location = _APPLocation + "FormFS";
        }
    });

    if (_hd.hdnHeaderId.val() != "" && _hd.hdnHeaderId.val() != "0") {
        _hd.ddlFormType.prop("disabled", true);
        _hd.ddlFormType.trigger("chosen:updated");
        _hd.txtDist.prop("disabled", true);
        _hd.ddlYear.prop("disabled", true);
        _hd.ddlYear.trigger("chosen:updated");
        _hd.ddlRmuCode.prop("disabled", true);
        _hd.ddlRmuCode.trigger("chosen:updated");
        _hd.ddlSection.prop("disabled", true);
        _hd.ddlSection.trigger("chosen:updated");
        _hd.ddlRoadCode.prop("disabled", true);
        _hd.ddlRoadCode.trigger("chosen:updated");
        _hd.btnFindDetails.hide();
        _hd.btnHSave.show();
        _hd.btnHSubmit.show();

        if (_hd.hdnHIsViewMode.val() == "1") {
            _hd.ddlCrewleader.prop("disabled", true);
            _hd.ddlCrewleader.trigger("chosen:updated");
            _hd.ddlInspectedby.prop("disabled", true);
            _hd.ddlInspectedby.trigger("chosen:updated");
            _hd.ddlCheckedby.prop("disabled", true);
            _hd.ddlCheckedby.trigger("chosen:updated");
            _hd.ddlSmzdby.prop("disabled", true);
            _hd.ddlSmzdby.trigger("chosen:updated");
            _hd.DtChckdBy.prop("disabled", true);
            _hd.txtInspectedDate.prop("disabled", true);
            _hd.txtSmzdDate.prop("disabled", true);
            _hd.btnHSave.hide();
            _hd.btnHSubmit.hide();
        }
    }

    _hd.btnFindDetails.on("click", function () {

        findDetail();
    });
});

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

function bindRMU(callback) {
    var req = {};
    req.RMU = ''
    req.Section = '';
    req.RdCode = '';
    _hd.txtRmu.val("");
    _hd.txtSectionName.val("");
    _hd.txtRoadName.val("");
    $.ajax({
        url: '/NOD/RMUSecRoad',
        dataType: 'JSON',
        data: req,
        type: 'Post',
        success: function (data) {

            //if (req.Section == "") {
            _hd.ddlRmuCode.empty();
            _hd.ddlRmuCode.append($("<option></option>").val("").html("Select Section Code"));
            $.each(data.rmu, function (index, v) {
                _hd.ddlRmuCode.append($("<option></option>").val(v.value).html(v.text));
            });

            _hd.ddlRmuCode.trigger("chosen:updated");

            if (callback)
                callback();
        },
        error: function (data) {

            console.error(data);
        }
    });
}

function bindSection(callback) {

    var req = {};
    var _rmu = _hd.ddlRmuCode;
    var _sec = _hd.ddlSection;
    var _road = _hd.ddlRoadCode;
    req.RMU = _hd.ddlRmuCode.val();
    req.SectionCode = '';
    req.RdCode = '';
    _hd.txtRoadName.val("");
    _hd.txtSectionName.val("");
    $.ajax({
        url: '/NOD/RMUSecRoad',
        dataType: 'JSON',
        data: req,
        type: 'Post',
        success: function (data) {

            _hd.ddlSection.empty();
            _hd.ddlSection.append($("<option></option>").val("").html("Select Section Code"));
            $.each(data.section, function (index, v) {
                _hd.ddlSection.append($("<option></option>").val(v.value).html(v.text));
            });
            _hd.ddlSection.trigger("chosen:updated");
            if (callback)
                callback();
        },
        error: function (data) {

            console.error(data);
        }
    });
}


function bindRoadCode(callback) {

    var req = {};
    var _rmu = _hd.ddlRmuCode
    var _sec = _hd.ddlSection;
    var _road = _hd.ddlRoadCode;
    req.RMU = _hd.ddlRmuCode.val();
    req.SectionCode = _hd.ddlSection.val();
    req.RdCode = '';
    $("#txtRoadName").val("");
    $.ajax({
        url: '/NOD/RMUSecRoad',
        dataType: 'JSON',
        data: req,
        type: 'Post',
        success: function (data) {

            _road.empty();
            _road.append($("<option></option>").val("").html("Select Road Code"));
            $.each(data.rdCode, function (index, v) {
                _road.append($("<option></option>").val(v.value).html(v.text));
            });
            _road.trigger("chosen:updated");
            if (callback)
                callback();
        },
        error: function (data) {

            console.error(data);
        }
    });
}

function getRoadDetail() {
    var req = {};
    req.code = _hd.ddlRoadCode.val();
    $.ajax({
        url: '/FormF2/GetRoadDetailByCode',
        dataType: 'JSON',
        data: req,
        type: 'Post',
        success: function (data) {

            _hd.ddlDivision.val(data.divisionCode);
            _hd.ddlDivision.trigger("chosen:updated");
            _hd.ddlRmuCode.val(data.rmuCode);
            _hd.ddlRmuCode.trigger("chosen:updated");
            $("#ddlSection option").filter(function () {
                return this.text.indexOf(data.secName) != -1;
            }).attr('selected', true);
            _hd.ddlSection.trigger("chosen:updated");
            _hd.txtSectionName.val(data.secName);
        }
    });
}

function getRoadDetail() {
    var req = {};
    req.code = _hd.ddlRoadCode.val();
    $.ajax({
        url: '/FormF2/GetRoadDetailByCode',
        dataType: 'JSON',
        data: req,
        type: 'Post',
        success: function (data) {
            _hd.ddlRmuCode.val(data.rmuCode);
            _hd.ddlRmuCode.trigger("chosen:updated");
            console.log(data);
            $("#ddlSection option").filter(function () {
                return this.text.indexOf(data.secName) != -1;
            }).attr('selected', true);
            _hd.ddlSection.trigger("chosen:updated");
            _hd.txtSectionName.val(data.secName);
        }
    });
}

function findDetail() {
    if (ValidatePage("#headerFindDiv", "", "validate")) {
        var req = {};
        req.PkRefNo = _hd.hdnHeaderId.val();
        req.DivCode = "";
        req.Dist = _hd.txtDist.val();
        req.RmuName = _hd.ddlRmuCode.val();
        req.RoadCode = _hd.ddlRoadCode.val();
        req.RoadName = _hd.txtRoadName.val();
        req.YearOfInsp = _hd.ddlYear.val();
        req.UserIdInspBy = _hd.ddlInspectedby.val();
        req.UserNameInspBy = _hd.txtInspectedbyName.val();
        req.UserDesignationInspY = _hd.txtInspectedDesignation.val();
        req.DtInspBy = _hd.txtInspectedDate.val();
        req.SignpathInspBy = "";
        req.FormRefId = _hd.txtHReferenceNo.val();
        req.CrewLeaderId = _hd.ddlCrewleader.val();
        req.CrewLeaderName = _hd.txtCrewLeaderName.val();
        req.UserIdSmzdBy = _hd.ddlSmzdby.val();
        req.UserNameSmzdBy = _hd.txtSmzdbyName.val();
        req.UserDesignationSmzdY = _hd.txtSmzdDesignation.val();
        req.DtSmzdBy = _hd.txtSmzdDate.val();
        req.SignpathSmzdBy = "";
        req.UserIdChckdBy = _hd.ddlCheckedby.val();
        req.UserNameChckdBy = _hd.txtCheckedbyName.val();
        req.UserDesignationChckdBy = _hd.txtCheckedDesignation.val();
        req.DtChckdBy = _hd.DtChckdBy.val();
        req.SignpathChckdBy = "";
        req.SubmitSts = false;
        $("#page-preloader").css("display", "block");
        $.ajax({
            url: "/FormFS/FindDetail",
            dataType: "JSON",
            data: req,
            type: "Post",
            success: function (data) {
                if (data > 0) {
                    _hd.btnFindDetails.hide();
                    window.location = _APPLocation + "FormFS/Add?id=" + data + "&view=false";
                }
                else if (data == -1) {
                    app.Alert("No data found to generate report.");
                }
                else if (data == -2) {
                    app.Alert("You cannot submit without giving remarks and needed.");
                }
                $("#page-preloader").css("display", "none");
            }, error: function (data) { console.error(data); $("#page-preloader").css("display", "none"); }
        });
    }
}

function saveHeader(isSave = true, isSubmit = false) {
    var isvalid = true;
    if (isSubmit) {

        isvalid = ValidatePage("#headerDiv", "", "", "svalidate");
    }
    if (isvalid && ValidatePage("#headerDiv", "", "validate")) {
        var req = {};
        req.PkRefNo = _hd.hdnHeaderId.val();
        req.DivCode = "";
        req.Dist = _hd.txtDist.val();
        req.RmuName = _hd.ddlRmuCode.val();
        req.RoadCode = _hd.ddlRoadCode.val();
        req.RoadName = _hd.txtRoadName.val();
        req.YearOfInsp = _hd.ddlYear.val();
        req.UserIdInspBy = _hd.ddlInspectedby.val();
        req.UserNameInspBy = _hd.txtInspectedbyName.val();
        req.UserDesignationInspY = _hd.txtInspectedDesignation.val();
        req.DtInspBy = _hd.txtInspectedDate.val();
        req.SignpathInspBy = "";
        req.FormRefId = _hd.txtHReferenceNo.val();
        req.CrewLeaderId = _hd.ddlCrewleader.val();
        req.CrewLeaderName = _hd.txtCrewLeaderName.val();
        req.UserIdSmzdBy = _hd.ddlSmzdby.val();
        req.UserNameSmzdBy = _hd.txtSmzdbyName.val();
        req.UserDesignationSmzdY = _hd.txtSmzdDesignation.val();
        req.DtSmzdBy = _hd.txtSmzdDate.val();
        req.SignpathSmzdBy = "";
        req.UserIdChckdBy = _hd.ddlCheckedby.val();
        req.UserNameChckdBy = _hd.txtCheckedbyName.val();
        req.UserDesignationChckdBy = _hd.txtCheckedDesignation.val();
        req.DtChckdBy = _hd.DtChckdBy.val();
        req.SignpathChckdBy = "";
        req.SubmitSts = isSubmit;
        $.ajax({
            url: "/FormFS/SaveHeader",
            dataType: "JSON",
            data: req,
            type: "Post",
            success: function (data) {
                if (data > 0) {
                    if (isSave && !isSubmit) {
                        app.ShowSuccessMessage("Saved Successfully");
                        window.location = _APPLocation + "FormFS";
                    }
                    else if(isSubmit) {
                        app.ShowSuccessMessage("Submitted Successfully");
                        window.location = _APPLocation + "FormFS";
                    }
                }
                else if (data == -1) {
                    app.Alert("No data found to generate report.");
                }
                else if (data == -2) {
                    app.Alert("You cannot submit without giving needed for all types.");
                }
            }, error: function (data) { console.error(data); }
        });
    }
}

function bindDetails(headerid) {
    var req = {};
    req.headerid = headerid;
    $.ajax({
        url: "/FormFS/GetRecordList",
        dataType: "JSON",
        data: req,
        type: "Post",
        success: function (data) {
            assignValuetothegrid(data);
        }, error: function (data) { console.error(data); }
    });

}
var tbl = $("#FormV2DetailGridView");
function assignValuetothegrid(data) {
    if (data && data.length > 0) {
        $.each(data, function (i, d) {
            if (d.groupCode != "" && d.strucCode != "") {
                tbl.find(`[${d.groupCode}${d.strucCode}width]`).text((data.width != null ? data.width.toFixed(3) : "0.000"));

                if (d.length != null && (d.unit == "km")) {
                    var result = (d.condition1 != null ? (d.condition1 / 10) : 0.0)
                        + (d.condition2 != null ? (d.condition2 / 10) : 0.0)
                        + (d.condition3 != null ? (d.condition3 / 10) : 0.0);
                    tbl.find(`[${d.groupCode}${d.strucCode}Totallength]`).text(result.toFixed(1));
                    tbl.find(`[${d.groupCode}${d.strucCode}width]`).text(d.width);
                    tbl.find(`[${d.groupCode}${d.strucCode}c1]`).text((d.condition1 != null ? (d.condition1 / 10).toFixed(1) : ""));
                    tbl.find(`[${d.groupCode}${d.strucCode}c2]`).text((d.condition2 != null ? (d.condition2 / 10).toFixed(1) : ""));
                    tbl.find(`[${d.groupCode}${d.strucCode}c3]`).text((d.condition3 != null ? (d.condition3 / 10).toFixed(1) : ""));
                }
                else if (d.length != null && (d.unit == "m" || d.unit == "nr")) {
                    tbl.find(`[${d.groupCode}${d.strucCode}Totallength]`).text(d.length);
                    tbl.find(`[${d.groupCode}${d.strucCode}width]`).text(d.width);
                    tbl.find(`[${d.groupCode}${d.strucCode}c1]`).text((d.condition1 != null ? (d.condition1).toFixed(0) : ""));
                    tbl.find(`[${d.groupCode}${d.strucCode}c2]`).text((d.condition2 != null ? (d.condition2).toFixed(0) : ""));
                    tbl.find(`[${d.groupCode}${d.strucCode}c3]`).text((d.condition3 != null ? (d.condition3).toFixed(0) : ""));
                }

                tbl.find(`[${d.groupCode}${d.strucCode}Needed]`).text((d.needed != null ? d.needed : ""));
                tbl.find(`[${d.groupCode}${d.strucCode}Remark]`).text((d.remarks != null ? d.remarks : ""));
                tbl.find(`[${d.groupCode}${d.strucCode}Id`).attr('id', d.pkRefNo);
                tbl.find(`[${d.groupCode}${d.strucCode}Id`).on("click", function () {
                    var d = this;
                    var needed = tbl.find(`[${d.attributes[0].name.replace("id", "Needed")}]`).text();
                    var remark = tbl.find(`[${d.attributes[0].name.replace("id", "Remark")}]`).text();
                    editDetail(this.id, needed, remark);
                });
            }
        });
    }
}
