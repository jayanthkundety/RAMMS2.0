
var _hd = {
    txtHReferenceNo: $("#txtHReferenceNo"),
    ddlDivision: $("#ddlDivision"),
    txtDist: $("#txtDist"),
    txtRmu: $("#txtRmu"),
    ddlYear: $("#ddlYear"),
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
    btnHCancel: $("#btnHCancel"),
    btnHSave: $("#btnHSave"),
    btnHSubmit: $("#btnHSubmit"),
    HdnHeaderPkId: $("#hdnHeaderId"),
    hdnHIsViewMode: $("#hdnHIsViewMode"),
    ValidateFind: "#headerFindDiv",
    ValidateSave: "#headerDiv",
    IsView: $("#hdnHIsViewMode"),
    IsAlreadyExists: $("#IsAlreadyExists"),
    hdnRoadCodeText: $("#hdnRoadCodeText"),
    ddlRMU: $("#ddlRMU"),
    ddlSection: $("#ddlSection"),
    txtSectionName: $("#txtSectionName"),
    hdnRoadCode: $("#hdnRoadCode")
}

var _dt = {
    btnDCancel: $("#btnDCancel"),
    btnDSaveAndContinue: $("#btnDSaveAndContinue"),
    btnDSaveAndExit: $("#btnDSaveAndExit"),
    txtRemarks: $("#txtRemarks"),
    txtCondition3: $("#txtCondition3"),
    txtCondition2: $("#txtCondition2"),
    txtCondition1: $("#txtCondition1"),
    txtPostspacing: $("#txtPostspacing"),
    txtBound: $("#txtBound"),
    txtStructurecode: $("#txtStructurecode"),
    txtStartinchKm: $("#txtStartinchKm"),
    txtStartinchM: $("#txtStartinchM"),
    txtDReferenceNo: $("#txtDReferenceNo"),
    IsView: $("#IsDView"),
    hdnDetailPkNo: $("#hdnDetailPkNo"),
    txtLength: $("#txtLength")
};

$(document).ready(function () {


    _hd.btnHSave.hide();
    _hd.btnHSubmit.hide();
    _hd.btnHSave.on("click", function () {
        saveHeader(true, false);
    });

    _hd.btnHSubmit.on("click", function () {
        saveHeader(true, true);
    });



    $("#ddlInspectedby").on("change", function () {
        var value = this.value;

        if (value == "") {
            $("#txtInspectedbyName").val('');
            $("#txtInspectedbyName").attr("readonly", "true");
        }
        else if (value == "99999999") {
            $("#txtInspectedbyName").val('');
            $("#txtInspectedbyName").removeAttr("readonly");
        }
        else {
            getUserDetail(value, function (data) {
                $("#txtInspectedbyName").val(data.userName);
                $("#txtInspectedbyName").attr("readonly", "true");
            });
        }
    });


    $("#ddlCrewleader").on("change", function () {
        var value = this.value;

        if (value == "") {
            $("#txtCrewLeaderName").val('');
            $("#txtCrewLeaderName").attr("readonly", "true");
        }
        else if (value == "99999999") {
            $("#txtCrewLeaderName").val('');
            $("#txtCrewLeaderName").removeAttr("readonly");
        }
        else {
            getUserDetail(value, function (data) {
                $("#txtCrewLeaderName").val(data.userName);
                $("#txtCrewLeaderName").attr("readonly", "true");
            });
        }
    });



    $("#ddlRMU").on("change", function () {
        // 

        $("#txtRoadlength").val("");

        if (this.value == "") {
            $("#txtHReferenceNo").val("");
            $("#txtRoadlength").val("");
            _hd.ddlSection.val("");
            _hd.ddlRMU.trigger("chosen:updated");
            _hd.ddlSection.trigger("chosen:updated");
            _hd.ddlRoadCode.val("").trigger("chosen:updated");
            $("#txtRoadName").val("");
            $("#txtSectionName").val("");
            // bindRMU();
            bindSection();
            bindRoadCode();
        }
        else {
            bindSection();
            bindRoadCode();
        }
    });

    $("#ddlSection").on("change", function () {

        $("#txtRoadlength").val("");
        bindRoadCode();
        if (this.value == "") {
            $("#txtSectionName").val("");
            $("#txtHReferenceNo").val("");
            $("#txtRoadlength").val("");
        }
        else {
            $("#txtSectionName").val($("#ddlSection").find("option:selected").text().split("-")[1]);

        }
    });

    _hd.btnFindDetails.on("click", function () {
        debugger;
        if (_hd.IsAlreadyExists.val() == "1") {
            window.location.href = "/FormF2/Add?id=" + _hd.HdnHeaderPkId.val() + "&isview=0";
        }
        else {

            if (ValidatePage(_hd.ValidateFind)) {
                saveHeader(false);
            }

        }

    });


    _hd.btnHCancel.on("click", function () {

        if (_hd.IsView.val() == "1") {
            window.location.href = "/FormF3";
        }
        else if (app.Confirm("Unsaved changes will be lost. Are you sure you want to cancel?", function (e) {
            if (e) {
                window.location.href = "/FormF2";
            }
        }));
    });


    $("#ddlYear").on("change", function () {
        generateHeaderReference();
    });

    $("#ddlRoadCode").on("change", function () {

         
        var value = this.value;
        if (value != "") {
            bindRoadLength(value);

            bindRoadDetail(value, function (data) {

                $("#txtRoadName").val(data.roadName);
            });
            generateHeaderReference();


        }
        else {
            $("#txtRoadName").val("");
            $("#txtHReferenceNo").val("");
            $("#txtRoadlength").val("");
            //_hd.txtDist.val("");
            //_hd.txtDivCode.val("");
        }
    });



});



function bindRMU(callback) {
    //
    var req = {};
    req.RMU = ''
    req.Section = '';
    req.RdCode = '';
    req.GrpCode = "GR"
    $("#txtRmu").val("");
    $("#txtSectionName").val("");
    $("#txtRoadName").val("");

    $.ajax({
        url: '/FormF2/RMUSecRoad',
        dataType: 'JSON',
        data: req,
        type: 'Post',
        success: function (data) {
            // 
            _hd.ddlRMU.empty();
            _hd.ddlRMU.append($("<option></option>").val("").html("Select RMU"));
            $.each(data.rmu, function (index, v) {
                _hd.ddlRMU.append($("<option></option>").val(v.value).html(v.text));
            });
            _hd.ddlRMU.trigger("chosen:updated");

            if (callback)
                callback();
        },
        error: function (data) {

            console.error(data);
        }
    });
}

function bindSection(callback) {
    // 
    var req = {};
    var _rmu = $("#ddlRMU");
    var _sec = $("#ddlSection");
    var _road = $("#ddlRoadCode");
    req.RMU = _hd.ddlRMU.val();
    req.SectionCode = '';
    req.RdCode = '';
    req.GrpCode = "GR"
    $("#txtRoadName").val("");
    $("#txtSectionName").val("");

    $.ajax({
        url: '/FormF2/RMUSecRoad',
        dataType: 'JSON',
        data: req,
        type: 'Post',
        success: function (data) {
            // 
            _sec.empty();
            _sec.append($("<option></option>").val("").html("Select Section Code"));
            $.each(data.section, function (index, v) {
                _sec.append($("<option></option>").val(v.code).html(v.text).attr("code", v.code).attr("text", v.value));
            });
            _sec.trigger("chosen:updated");
            _sec.trigger("change");
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
    var _rmu = $("#ddlRMU");
    var _sec = $("#ddlSection");
    var _road = $("#ddlRoadCode");
    req.RMU = _hd.ddlRMU.val();
    req.SectionCode = _hd.ddlSection.val();
    req.RdCode = '';
    req.GrpCode = "GR"
    $("#txtRoadName").val("");

    $.ajax({
        url: '/FormF2/RMUSecRoad',
        dataType: 'JSON',
        data: req,
        type: 'Post',
        success: function (data) {

            _road.empty();
            _road.append($("<option></option>").val("").html("Select Road Code"));
            $.each(data.rdCode, function (index, v) {
                _road.append($("<option></option>").val(v.value).html(v.text));
                // _road.append($("<option></option>").val(v.value).html(v.text).attr("Item1", v.item1).attr("Item3", v.item3).attr("PKId", v.pkId).attr("code", v.code));
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


function generateHeaderReference() {
    if (_hd.ddlRoadCode.val() != "" && _hd.ddlYear.val() != "") {
        //var roadcode = _hd.ddlRoadCode.find(":selected").text().split('-')[0].trim();
        var v = $("#ddlRoadCode").find(":selected").text().split('-');
        if (v.length > 2) {
            var roadcode = v[0] + '-' + v[1];
        }
        else {
            var roadcode = v[0];
        }
        $("#txtHReferenceNo").val(("CI/Form F3/" + roadcode + "/" + $("#ddlYear").val()));
    }
    else {
        $("#txtHReferenceNo").val("");
    }
}



function saveDetail(isSaveAndExit = false) {

    if (ValidatePage("#FormAddDetails")) {
        var req = {};
        req.PkRefNo = _dt.hdnDetailPkNo.val();
        req.GrCondition1 = _dt.txtCondition1.val() == "" ? 0 : _dt.txtCondition1.val();
        req.GrCondition2 = _dt.txtCondition2.val() == "" ? 0 : _dt.txtCondition2.val();
        req.GrCondition3 = _dt.txtCondition3.val() == "" ? 0 : _dt.txtCondition3.val();
        req.Remarks = _dt.txtRemarks.val();
        req.PostSpac = _dt.txtPostspacing.val();

        var c1 = req.GrCondition1 != "" ? parseFloat(req.GrCondition1).toFixed(5) : "0";
        var c2 = req.GrCondition2 != "" ? parseFloat(req.GrCondition2).toFixed(5) : "0";
        var c3 = req.GrCondition3 != "" ? parseFloat(req.GrCondition3).toFixed(5) : "0";

        //var tLength = TryParseInt(_dt.txtLength.val());
        // var ConLength = (TryParseInt(req.GrCondition1) + TryParseInt(req.GrCondition2) + TryParseInt(req.GrCondition3));
        var tLength = parseFloat((_dt.txtLength.val() != "" ? _dt.txtLength.val() : "0"));
        var ConLength = parseFloat(c1) + parseFloat(c2) + parseFloat(c3);
        if (tLength < ConLength) {
            //app.Alert("<b>(Condition 1 + Condition 2 + Condition 3)</b> should not exceed <b>length</b> !!!");
            app.Alert("The total Guardrail length (Condition 1 + Condition 2 + Condition 3) should not exceed the initially registered length.")
        }
        if (tLength > ConLength) {
            app.Alert("<b>The total Guardrail length should be equal to (Condition 1 + Condition 2 + Condition 3)</b>");
        }
        if (tLength == ConLength) {
            $.ajax({
                url: '/FormF2/SaveDetail',
                dataType: 'JSON',
                data: req,
                type: 'Post',
                success: function (data) {
                    if (data > 0) {
                        app.ShowSuccessMessage("Saved Successfully");
                        ClearDetail();
                    }

                    if (isSaveAndExit) {
                        ClearDetail();

                    }
                    $("#FormF2AdddetailsModal").modal("hide");
                    InitializeDetailsGrid();
                },
                error: function (data) {

                    console.error(data);
                }
            });
        }
    }
}

function bindreference() {

    var req = {};
    req.headerId = _hd.HdnHeaderPkId.val();
    $.ajax({
        url: '/FormF2/LastInsertedDetailNo',
        dataType: 'JSON',
        data: req,
        type: 'Post',
        success: function (data) {

            var reference = $("#txtHReferenceNo").val() + "/" + (data + 1);
            _dt.txtDReferenceNo.val(reference)

        },
        error: function (data) {

            console.error(data);
        }
    });

}


function openDetail(id) {

    var req = {};
    req.id = id;
    $.ajax({
        url: '/FormF2/GetDetailById',
        dataType: 'JSON',
        data: req,
        type: 'Post',
        success: function (data) {
            //
            if (data.pkRefNo == 0) {
                bindreference();
            }
            _dt.hdnDetailPkNo.val(data.pkRefNo);
            _dt.txtStartinchKm.val(data.startingChKm);
            _dt.txtStartinchM.val(data.startingChM);
            _dt.txtStructurecode.val(data.grCode);
            _dt.txtBound.val(data.rhsMLhs);
            _dt.txtPostspacing.val(data.postSpac);
            _dt.txtCondition1.val(data.grCondition1);
            _dt.txtCondition2.val(data.grCondition2);
            _dt.txtCondition3.val(data.grCondition3);
            _dt.txtRemarks.val(data.remarks);
            _dt.txtLength.val(data.length);
            $("#FormF2AdddetailsModal").modal("show");

        },
        error: function (data) {

            console.error(data);
        }
    });

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

function bindRoadLength(code, callback) {

    var req = {};
    req.roadcode = code;
    $.ajax({
        url: '/FormF2/GetRoadLength',
        dataType: 'JSON',
        data: req,
        type: 'Post',
        success: function (data) {
            $("#txtRoadlength").val(data);
            if (callback)
                callback(data);
        },
        error: function (data) {

            console.error(data);
        }
    });
}
function bindRoadDetail(code, callback) {
    var req = {};

    req.code = code;
    $.ajax({
        url: '/FormF2/GetRoadDetailByCode',
        dataType: 'JSON',
        data: req,
        type: 'Post',
        success: function (data) {

            _hd.hdnRoadCode.val(data.no);
            _hd.ddlDivision.val(data.divisionCode);
            _hd.ddlDivision.trigger("chosen:updated");
            _hd.ddlRMU.val(data.rmuCode);
            _hd.ddlRMU.trigger("chosen:updated");
            _hd.ddlSection.val(data.secCode);
            _hd.ddlSection.trigger("chosen:updated");
            $("#txtSectionName").val(data.secName);

            if (callback)
                callback(data);
        },
        error: function (data) {

            console.error(data);
        }
    });
}



//GetLocationCh
function getLocationCh(callback) {
    var req = {};

    req.roadcode = _hd.ddlRoadCode.find(":selected").text().split('-')[0].trim();
    $.ajax({
        url: '/FormF2/GetLocationCh',
        dataType: 'JSON',
        data: req,
        type: 'Post',
        success: function (data) {
            _dt.ddlStartinch.empty();
            _dt.ddlStartinch.append($("<option></option>").val("").html("Select Starting Ch"));
            $.each(data, function (index, v) {
                _dt.ddlStartinch.append($("<option></option>").val(v.value).html(v.text));
            });

            _dt.ddlStartinch.trigger("chosen:updated");
            if (callback)
                callback();
        },
        error: function (data) {

            console.error(data);
        }
    });
}



function Save(GroupName, SubmitType) {


    if (SubmitType == "Submitted") {

        if ($("#ddlSource").val() == "G1G2") {

            if ($("#ddlRefNo").val() == "") {
                app.ShowErrorMessage('please select reference no', false);
                return;
            }
        }
        $("#FormF3_SubmitSts").val(true);
    }

    if (ValidatePage('#headerDiv')) {

        if ($("#FormF3_Status").val() == "")
            $("#FormF3_Status").val("Initialize");
        else if ($("#FormF3_Status").val() == "Initialize")
            $("#FormF3_Status").val("Saved");

        InitAjaxLoading();
        EnableDisableElements(false);
        $.get('/MAM/SaveFormF3', $("form").serialize(), function (data) {
            EnableDisableElements(true)
            HideAjaxLoading();
            if (data == -1) {
                app.ShowErrorMessage(data.errorMessage);
            }
            else {

                if (SubmitType == "") {
                    if (data.formExist) {
                        location.href = "/MAM/EditFormF3?Id=" + data.pkRefNo + "&view=0";
                        return;
                    }
                    $('#ddlSource').prop('disabled', false).trigger("chosen:updated");
                    $('#ddlRefNo').prop('disabled', false).trigger("chosen:updated");
                    UpdateFormAfterSave(data);
                    // app.ShowSuccessMessage('Saved Successfully', false);
                }
                else if (SubmitType == "Saved") {
                    app.ShowSuccessMessage('Saved Successfully', false);
                    $('#ddlSource').prop('disabled', true).trigger("chosen:updated");
                    $('#ddlRefNo').prop('disabled', true).trigger("chosen:updated");
                    location.href = "/MAM/FormF3";
                }
                else if (SubmitType == "Submitted") {
                    app.ShowSuccessMessage('Submitted Successfully', false);
                    location.href = "/MAM/FormF3";
                }
                else if (SubmitType == "Verified") {
                    process.ShowApprove(GroupName, SubmitType);
                }
            }
        });
    }

}


function EnableDisableElements(state) {
 
    $('#headerDiv * > select').prop('disabled', state).trigger("chosen:updated");
    $('#ddlSource').prop('disabled', false).trigger("chosen:updated");
    $('#ddlRefNo').prop('disabled', false).trigger("chosen:updated");
}