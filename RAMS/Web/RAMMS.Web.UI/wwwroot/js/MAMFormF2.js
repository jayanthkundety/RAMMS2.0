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

    _hd.ddlRoadCode.chosen();
    _hd.ddlYear.chosen();
    _hd.ddlDivision.chosen();
    _hd.ddlCrewleader.chosen();
    _hd.ddlInspectedby.chosen();
    _hd.btnHSave.hide();
    _hd.btnHSubmit.hide();
    _hd.btnHSave.on("click", function () {
        saveHeader(true, false);
    });

    _hd.btnHSubmit.on("click", function () {
        saveHeader(true, true);
    });


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

    if (_hd.hdnRoadCodeText.val() != "") {

        bindRoadDetail(_hd.hdnRoadCodeText.val(), function (data) {
            //
            bindRMU(function () {
                _hd.ddlRMU.val(data.rmuCode);
                _hd.ddlRMU.prop("disabled", true);
                _hd.ddlRMU.trigger("chosen:updated");
               // 
                bindSection(function () {
                    _hd.txtSectionName.val(data.secName);
                    _hd.ddlSection.val(data.secCode);
                    _hd.ddlSection.prop("disabled", true);
                    _hd.ddlSection.trigger("chosen:updated");
                   // 
                    bindRoadCode(function () {
                        //
                        _hd.txtRoadName.val(data.roadName);

                        _hd.ddlRoadCode.empty();
                        var t = data.roadCode + '-' + data.roadName;
                        _hd.ddlRoadCode.append($("<option></option>").val(data.roadCode).html(t));
                        _hd.ddlRoadCode.prop("disabled", true);
                        _hd.ddlRoadCode.trigger("chosen:updated");


                        //_hd.ddlRoadCode.val(data.roadCode);
                        //_hd.ddlRoadCode.val(t).trigger('chosen:updated');
                        _hd.ddlRoadCode.prop("disabled", true);
                        //_hd.ddlRoadCode.trigger("chosen:updated");
                    });
                });
            });
        });
        _hd.ddlYear.prop("disabled", true);
        _hd.ddlYear.trigger("chosen:updated");
        _hd.txtDist.prop("disabled", true);
    }
    else {
        bindRMU();
        bindSection();
        bindRoadCode();
    }

    $("#ddlRMU").on("change", function () {
       // 

        _hd.txtRoadlength.val("");

        if (this.value == "") {
            _hd.txtHReferenceNo.val("");
            _hd.txtRoadlength.val("");
            _hd.ddlSection.val("");
            _hd.ddlRMU.trigger("chosen:updated");
            _hd.ddlSection.trigger("chosen:updated");
            _hd.ddlRoadCode.val("").trigger("chosen:updated");
            _hd.txtRoadName.val("");
            _hd.txtSectionName.val("");
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
       
        _hd.txtRoadlength.val("");
        bindRoadCode();
        if (this.value == "") {
            $("#txtSectionName").val("");
            _hd.txtHReferenceNo.val("");
            _hd.txtRoadlength.val("");
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

    //if (_hd.ddlDivision.val() != "") {
    //    bindRoadCode();
    //}

    _hd.btnHCancel.on("click", function () {

        if (_hd.IsView.val() == "1") {
            window.location.href = "/FormF2";
        }
        else if (app.Confirm("Unsaved changes will be lost. Are you sure you want to cancel?", function (e) {
            if (e) {
                window.location.href = "/FormF2";
            }
        }));
    });

    _hd.ddlYear.on("change", function () {
        generateHeaderReference();
    });

    _hd.ddlRoadCode.on("change", function () {
        
        var value = this.value;
        if (value != "") {
            bindRoadLength(value);
            
            bindRoadDetail(value, function (data) {
                
                _hd.txtRoadName.val(data.roadName);
            });
            generateHeaderReference();


        }
        else {
            _hd.txtRoadName.val("");
            _hd.txtHReferenceNo.val("");
            _hd.txtRoadlength.val("");
            //_hd.txtDist.val("");
            //_hd.txtDivCode.val("");
        }
    });

    if (_hd.IsAlreadyExists.val() == "1") {
        _hd.btnFindDetails.hide();
        //_hd.ddlRoadCode.prop("disabled", true);
        //_hd.ddlRoadCode.trigger("chosen:updated");

        if (_hd.hdnHIsViewMode.val() != "1") {
            _hd.btnHSave.show();
            _hd.btnHSubmit.show();
        }

        //_hd.txtHReferenceNo.prop("disabled", true);
        _hd.ddlDivision.prop("disabled", true);
        _hd.ddlDivision.trigger("chosen:updated");
        //bindRoadCode(function () {
        //    _hd.ddlRoadCode.val($("#hdnRoadCode").val());
        //    _hd.ddlRoadCode.prop("disabled", true);
        //    _hd.ddlRoadCode.trigger("chosen:updated");

        //    bindRoadDetail(function () {
        //        _hd.txtDist.prop("disabled", true);
        //        _hd.txtRmu.prop("disabled", true);
        //        _hd.ddlYear.prop("disabled", true);
        //        _hd.ddlYear.trigger("chosen:updated");
        //        _hd.txtRoadName.prop("disabled", true);
        //        getLocationCh();
        //    });

        //});

        _hd.btnFindDetails.hide();
        _hd.ddlCrewleader.trigger("chosen:updated");
        if (_hd.ddlInspectedby.val() == "99999999") {
            _hd.txtInspectedbyName.prop("disabled", false);
            _hd.txtInspectedDesignation.prop("disabled", false);
        }
        else {
            _hd.txtInspectedbyName.prop("disabled", true);
            _hd.txtInspectedDesignation.prop("disabled", true);
        }

        if (_hd.ddlCrewleader.val() == "99999999") {
            _hd.txtCrewLeaderName.prop("disabled", false);
            //_hd.ddlCrewleader.prop("disabled", false);
        }
        else {
            _hd.txtCrewLeaderName.prop("disabled", true);
            //_hd.ddlCrewleader.prop("disabled", true);
        }

    }

    /** Details */

    _dt.btnDSaveAndContinue.on("click", function () {
        saveDetail(false);
    });

    _dt.btnDSaveAndExit.on("click", function () {
        saveDetail(true);
    });

    _dt.btnDCancel.on("click", function () {

        if (_dt.IsView.val() != "1") {
            app.Confirm("Unsaved changes will be lost. Are you sure you want to cancel?", function (e) {
                if (e) {
                    $("#FormF2AdddetailsModal").modal("hide");
                    ClearDetail();

                }
            });
        }
        else {
            ClearDetail();
            $("#FormF2AdddetailsModal").modal("hide");
        }
    });

    if ($("#hdnHIsViewMode").val() == "1") {
        $(".view").prop("disabled", true);
        $(".viewD").prop("disabled", true);
        _hd.ddlCrewleader.prop("disabled", true);
        _hd.ddlCrewleader.trigger("chosen:updated");
        _hd.ddlInspectedby.prop("disabled", true);
        _hd.ddlInspectedby.trigger("chosen:updated");
        _hd.btnFindDetails.hide();
        _hd.btnHSave.hide();
        _hd.btnHSubmit.hide();
        _dt.btnDSaveAndContinue.hide();
        _dt.btnDSaveAndExit.hide();
        $("#txtCrewLeaderName").prop("disabled", true);
        $("#txtInspectedbyName").prop("disabled", true);
        $("#txtInspectedDesignation").prop("disabled", true);
        $("#txtInspectedDate").prop("disabled", true);

    }

});

function bindRMU(callback) {
    //
    var req = {};
    req.RMU = ''
    req.Section = '';
    req.RdCode = '';
    req.GrpCode = "GR"
    _hd.txtRmu.val("");
    _hd.txtSectionName.val("");
    _hd.txtRoadName.val("");

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
    _hd.txtRoadName.val("");
    _hd.txtSectionName.val("");

    $.ajax({
        url: '/FormF2/RMUSecRoad',
        dataType: 'JSON',
        data: req,
        type: 'Post',
        success: function (data) {
           // 
            _hd.ddlSection.empty();
            _hd.ddlSection.append($("<option></option>").val("").html("Select Section Code"));
            $.each(data.section, function (index, v) {
                _hd.ddlSection.append($("<option></option>").val(v.code).html(v.text).attr("code", v.code).attr("text", v.value));
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
    var _rmu = $("#ddlRMU");
    var _sec = $("#ddlSection");
    var _road = $("#ddlRoadCode");
    req.RMU = _hd.ddlRMU.val();
    req.SectionCode = _hd.ddlSection.val();
    req.RdCode = '';
    req.GrpCode="GR"
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
        var v = _hd.ddlRoadCode.find(":selected").text().split('-');
        if (v.length > 2) {
            var roadcode = v[0] + '-' + v[1];
        }
        else {
            var roadcode = v[0];
        }
        _hd.txtHReferenceNo.val(("CI/Form F2/" + roadcode + "/" + _hd.ddlYear.val()));
    }
    else {
        _hd.txtHReferenceNo.val("");
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

            var reference = _hd.txtHReferenceNo.val() + "/" + (data + 1);
            _dt.txtDReferenceNo.val(reference)

        },
        error: function (data) {

            console.error(data);
        }
    });

}

function editDetail(id) {
    
    _dt.IsView.val("0");
    openDetail(id);
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

function viewDetail(id) {

    _dt.IsView.val("1");
    openDetail(id);
    $("#FormF2AdddetailsModal").modal("show");
    _dt.btnDCancel.show();
    _dt.btnDSaveAndContinue.hide();
    _dt.btnDSaveAndExit.hide();
    $(".viewD").prop("disabled", true);
    //_dt.ddlBound.trigger("chosen:updated");
    _dt.ddlPostspacing.trigger("chosen:updated");
    _dt.ddlStartinch.trigger("chosen:updated");
    _dt.ddlStructurecode.trigger("chosen:updated");

}

function ClearDetail() {

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
            _hd.txtRoadlength.val(data);
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
            _hd.txtSectionName.val(data.secName);
            
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
//GetStructureCode
function getStructureCode(callback) {
    var req = {};

    req.roadcode = _hd.ddlRoadCode.find(":selected").text().split('-')[0].trim();
    req.locationch = _dt.ddlStartinch.val();
    $.ajax({
        url: '/FormF2/GetStructureCode',
        dataType: 'JSON',
        data: req,
        type: 'Post',
        success: function (data) {

            _dt.ddlStructurecode.empty();
            _dt.ddlStructurecode.append($("<option></option>").val("").html("Select Structure Code"));
            $.each(data, function (index, v) {
                _dt.ddlStructurecode.append($("<option></option>").val(v.value).html(v.text));
            });

            _dt.ddlStructurecode.trigger("chosen:updated");
            if (callback)
                callback();
        },
        error: function (data) {
            console.error(data);
        }
    });
}
//GetAIBound
function getAIBound(callback) {
    var req = {};
    req.roadcode = _hd.ddlRoadCode.find(":selected").text().split('-')[0].trim();
    req.locationch = _dt.ddlStartinch.val();
    req.structurecode = _dt.ddlStructurecode.val();
    $.ajax({
        url: '/FormF2/GetAIBound',
        dataType: 'JSON',
        data: req,
        type: 'Post',
        success: function (data) {

            _dt.ddlBound.empty();
            _dt.ddlBound.append($("<option></option>").val("").html("Select Bound"));
            $.each(data, function (index, v) {
                _dt.ddlBound.append($("<option></option>").val(v.value).html(v.text));
            });

            _dt.ddlBound.trigger("chosen:updated");
            if (callback)
                callback();
        },
        error: function (data) {
            console.error(data);
        }
    });
}

//GetPostSpacing
function getPostSpacing(callback) {
    var req = {};

    req.roadcode = _hd.ddlRoadCode.find(":selected").text().split('-')[0].trim();
    req.locationch = _dt.ddlStartinch.val();
    req.structurecode = _dt.ddlStructurecode.val();
    req.bound = _dt.ddlBound.val();
    $.ajax({
        url: '/FormF2/GetPostSpacing',
        dataType: 'JSON',
        data: req,
        type: 'Post',
        success: function (data) {

            _dt.ddlPostspacing.empty();
            _dt.ddlPostspacing.append($("<option></option>").val("").html("Select Post Spacing"));
            $.each(data, function (index, v) {
                _dt.ddlPostspacing.append($("<option></option>").val(v.value).html(v.text));
            });

            _dt.ddlPostspacing.trigger("chosen:updated");
            if (callback)
                callback();
        },
        error: function (data) {
            console.error(data);
        }
    });
}

function saveHeader(isSave = true, isSubmit = false) {
    
    if (isSubmit) {
        $("#F2UserDetail .svalidate").addClass("validate");
    }
    else {
        $("#F2UserDetail .svalidate").removeClass("validate");
    }
    Validation.ResetErrStyles("#F2UserDetail");
    if (ValidatePage(_hd.ValidateSave, "", "validate")) {
        var req = {};
        req.PkRefNo = _hd.HdnHeaderPkId.val();
        req.DivCode = _hd.ddlDivision.val();
        req.Dist = _hd.txtDist.val();
        req.RoadId = _hd.hdnRoadCode.val();
        if (req.RoadId != "") {
            var v = _hd.ddlRoadCode.find(":selected").text().split('-');
            //req.RoadCode = _hd.ddlRoadCode.find(":selected").text().split('-')[0].trim();
            if (v.length > 2) {
                req.RoadCode = v[0] + '-' + v[1];
            }
            else {
                req.RoadCode = v[0];
            }
            
            req.RoadName = _hd.txtRoadName.val();
        }
        req.RoadLength = _hd.txtRoadlength.val();
        req.YearOfInsp = _hd.ddlYear.val();
        req.DtOfInsp = _hd.txtInspectedDate.val();
        req.UserIdInspBy = _hd.ddlInspectedby.val();
        req.UserNameInspBy = _hd.txtInspectedbyName.val();
        req.UserDesignationInspBy = _hd.txtInspectedDesignation.val();
        req.DtInspBy = _hd.txtInspectedDate.val();
        req.SignpathInspBy = "";
        req.FormRefId = _hd.txtHReferenceNo.val();
        req.CrewLeaderId = _hd.ddlCrewleader.val();
        if (_hd.ddlCrewleader.val() != "") {
            req.CrewLeaderName = _hd.txtCrewLeaderName.val();
        }
        req.SubmitSts = isSubmit;
        $.ajax({
            url: '/FormF2/SaveHeader',
            dataType: 'JSON',
            data: req,
            type: 'Post',
            success: function (data) {
                
                if (data > 0) {
                    _hd.HdnHeaderPkId.val(data);
                    _hd.txtHReferenceNo.prop("disabled", true);
                    _hd.ddlRMU.prop("disabled", true);
                    _hd.ddlRMU.trigger("chosen:updated");
                    _hd.ddlSection.prop("disabled", true);
                    _hd.ddlSection.trigger("chosen:updated");
                    _hd.txtDist.prop("disabled", true);
                    _hd.txtRmu.prop("disabled", true);
                    _hd.ddlYear.prop("disabled", true);
                    _hd.ddlYear.trigger("chosen:updated");
                    _hd.ddlRoadCode.prop("disabled", true);
                    _hd.ddlRoadCode.trigger("chosen:updated");
                    _hd.txtRoadName.prop("disabled", true);
                    _hd.btnFindDetails.hide();
                    _hd.btnHSave.show();
                    _hd.btnHSubmit.show();
                    if (isSave) {
                        app.ShowSuccessMessage("Saved Successfully");
                       // setTimeout(2000);
                        //window.location.href = "/FormF2";
                        setTimeout(function () {
                            window.location.href = "/FormF2";
                        }, 3000);
                    }
                    _hd.btnFindDetails.hide();
                    InitializeDetailsGrid();
                   // window.location.href = "/FormF2";
                }
                else if (data == -1 && isSubmit) {
                    app.Alert("You cannot submit until verify the conditional inspection, please verify all.");
                }

                if (isSubmit) {
                    $("#F2UserDetail .svalidate").removeClass("validate");
                }

            },
            error: function (data) {

                console.error(data);
            }
        });
    }
}