
var _hd = {
    FormF3_PkRefId: $("#FormF3_PkRefId"),
    ddlDivision: $("#ddlDivision"),
    txtDist: $("#txtDist"),
    txtRmu: $("#txtRmu"),
    ddlYear: $("#ddlYear"),
    ddlRoadCode: $("#ddlRoadCode"),
    txtRoadName: $("#FormF3_RdName"),
    FormF3_RoadLength: $("#FormF3_RoadLength"),
    btnFindDetails: $("#btnFindDetails"),
    ddlCrewleader: $("#ddlCrewleader"),
    FormF3_CrewName: $("#FormF3_CrewName"),
    ddlInspectedby: $("#ddlInspectedby"),
    FormF3_CrewName: $("#FormF3_CrewName"),
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
    FormF3_SecName: $("#FormF3_SecName"),
    hdnRoadCode: $("#hdnRoadCode")
}



$(document).ready(function () {

    DisableHeader();

    if ($("#FormF3_PkRefNo").val() == 0 || $("#ddlSource").val() == "G1G2")
        $("#btnDtlModal").hide();


    if ($("#hdnView").val() == 1) {
        $("#saveFormF3Btn").hide();
        $("#SubmitFormF3Btn").hide();
        $("#btnDtlModal").hide();
    }


    $("#ddlInspectedby").on("change", function () {
        var value = this.value;

        if (value == "") {
            $("#FormF3_InspectedName").val('');
            $("#FormF3_InspectedName").attr("readonly", "true");
        }
        else if (value == "99999999") {
            $("#FormF3_InspectedName").val('');
            $("#FormF3_InspectedName").removeAttr("readonly");
        }
        else {
            getUserDetail(value, function (data) {
                $("#FormF3_InspectedName").val(data.userName);
                $("#FormF3_InspectedDesig").val(data.position);
                $("#FormF3_InspectedDesig").attr("readonly", "true");
            });
        }
    });
    $("#ddlInspectedby").trigger('change');

    $("#ddlCrew").on("change", function () {
        var value = this.value;

        if (value == "") {
            $("#FormF3_CrewName").val('');
            $("#FormF3_CrewName").attr("readonly", "true");
        }
        else if (value == "99999999") {
            $("#FormF3_CrewName").val('');
            $("#FormF3_CrewName").removeAttr("readonly");
        }
        else {
            getUserDetail(value, function (data) {
                $("#FormF3_CrewName").val(data.userName);
                $("#FormF3_CrewName").attr("readonly", "true");
            });
        }
    });



    $("#ddlRMU").on("change", function () {
        // 

        $("#FormF3_RoadLength").val("");

        if (this.value == "") {
            $("#FormF3_PkRefId").val("");
            $("#FormF3_RoadLength").val("");
            _hd.ddlSection.val("");
            _hd.ddlRMU.trigger("chosen:updated");
            _hd.ddlSection.trigger("chosen:updated");
            _hd.ddlRoadCode.val("").trigger("chosen:updated");
            $("#FormF3_RdName").val("");
            $("#FormF3_SecName").val("");
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

        $("#FormF3_RoadLength").val("");
        bindRoadCode();
        if (this.value == "") {
            $("#FormF3_SecName").val("");
            $("#FormF3_PkRefId").val("");
            $("#FormF3_RoadLength").val("");
        }
        else {
            $("#FormF3_SecName").val($("#ddlSection").find("option:selected").text().split("-")[1]);

        }
    });



    $("#ddlYear").on("change", function () {
        generateHeaderReference();
    });

    $("#ddlRoadCode").on("change", function () {


        var value = this.value;
        if (value != "") {
            bindRoadLength(value);

            bindRoadDetail(value, function (data) {

                $("#FormF3_RdName").val(data.roadName);
            });
            generateHeaderReference();


        }
        else {
            $("#FormF3_RdName").val("");
            $("#FormF3_PkRefId").val("");
            $("#FormF3_RoadLength").val("");

        }
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
    //
    var req = {};
    req.RMU = ''
    req.Section = '';
    req.RdCode = '';
    req.GrpCode = "GR"
    $("#txtRmu").val("");
    $("#FormF3_SecName").val("");
    $("#FormF3_RdName").val("");

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
    $("#FormF3_RdName").val("");
    $("#FormF3_SecName").val("");

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
             
            if ($("#hdnSecCode").val() != "" && $("#hdnSecCode").val() != undefined) {
                $("#ddlSection").val($("#hdnSecCode").val());
                $("#hdnSecCode").val("");
            }

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
    var _road = $("#ddlRoadCode");
    req.RMU = _hd.ddlRMU.val();
    req.SectionCode = _hd.ddlSection.val();
    req.RdCode = '';
    req.GrpCode = "GR"
    $("#FormF3_RdName").val("");

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

            if ($("#hdnRdCode").val() != "" && $("#hdnRdCode").val() != undefined) {
                $("#ddlRoadCode").val($("#hdnRdCode").val());
                $("#hdnRdCode").val("");
            }
            _road.trigger("chosen:updated");
            _road.trigger("change");
            if (callback)
                callback();
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
            $("#FormF3_RoadLength").val(data);
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
            _hd.FormF3_SecName.val(data.FormF3_SecName);

            if (callback)
                callback(data);
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
        $("#FormF3_PkRefId").val(("CI/Form F3/" + roadcode + "/" + $("#ddlYear").val()));
    }
    else {
        $("#FormF3_PkRefId").val("");
    }
}


function OnAssetChange(tis) {

    var ctrl = $(tis);
    if (ctrl.val() != null)
        $('#FormF3Dtl_AssetID').val(ctrl.val());
    if ($('#FormF3Dtl_AssetID').val() != "") {

        $("#FormF3Dtl_LocCh").val(ctrl.find("option:selected").attr("FromKm"));
        $("#FormF3Dtl_LocChDeci").val(ctrl.find("option:selected").attr("FromM"));
        $("#FormF3Dtl_Code").val(ctrl.find("option:selected").attr("Item1"));
        $("#FormF3Dtl_Bound").val(ctrl.find("option:selected").attr("Item2"));
        $("#FormF3Dtl_Width").val(ctrl.find("option:selected").attr("Item3"));
        $("#FormF3Dtl_Height").val(ctrl.find("option:selected").attr("CValue"));

    }
    else {
        $("#FormF3Dtl_LocCh").val('');
        $("#FormF3Dtl_LocChDeci").val('');
        $("#FormF3Dtl_Code").val('');
        $("#FormF3Dtl_Bound").val('');
        $("#FormF3Dtl_Width").val('');
        $("#FormF3Dtl_Height").val('');
    }
}




function Save(GroupName, SubmitType) {


    if (SubmitType == "Submitted") {
        $("#FormF3_SubmitSts").val(true);
        $("#ddlCrew").addClass("validate");
    }

    if (ValidatePage('#headerDiv')) {

        if ($("#FormF3_Status").val() == "")
            $("#FormF3_Status").val("Initialize");
        else if ($("#FormF3_Status").val() == "Initialize")
            $("#FormF3_Status").val("Saved");

        InitAjaxLoading();
        EnableDisableElements(false);
        $.get('/FormF3/SaveFormF3', $("form").serialize(), function (data) {
            EnableDisableElements(true)
            HideAjaxLoading();
            if (data == -1) {
                app.ShowErrorMessage(data.errorMessage);
            }
            else {

                if (SubmitType == "") {
                    if (data.formExist) {
                        location.href = "/FormF3/Add?Id=" + data.pkRefNo + "&view=0";
                        return;
                    }
                    $('#ddlSource').prop('disabled', false).trigger("chosen:updated");
                    UpdateFormAfterSave(data);

                }
                else if (SubmitType == "Saved") {
                    app.ShowSuccessMessage('Saved Successfully', false);
                    location.href = "/FormF3/Index";
                }
                else if (SubmitType == "Submitted") {
                    app.ShowSuccessMessage('Submitted Successfully', false);
                    location.href = "/FormF3/Index";
                }
                else if (SubmitType == "Verified") {
                    process.ShowApprove(GroupName, SubmitType);
                }
            }
        });
    }

}

function SaveFormF3Dtl() {

    if (ValidatePage('#myModal')) {
        InitAjaxLoading();
        $.post('/FormF3/SaveFormF3Dtl', $("form").serialize(), function (data) {
            HideAjaxLoading();
            if (data == -1) {
                app.ShowErrorMessage(data.errorMessage);
            }
            else {
                ClearFormF3Dtl()

                InitializeGrid();
                app.ShowSuccessMessage('Saved Successfully', false);
            }
        });

    }
}

function EditFormF3Dtl(obj, view) {


    EditModeDtl = true;
    var currentRow = $(obj).closest("tr");
    var data = $('#FormF3DtlGridView').DataTable().row(currentRow).data();

    $("#FormF3Dtl_PkRefNo").val(data.pkRefNo);
    $("#ddlAsset").val(data.assetId);
    $("#ddlAsset").trigger('chosen:updated');
    $("#ddlAsset").trigger('change');
    $("#FormF3Dtl_LocCh").val(data.frmCh);
    $("#FormF3Dtl_LocChDeci").val(data.frmChDec);
    $("#FormF3Dtl_Code").val(data.structureCode);
    $('#FormF3Dtl_Bound').val(data.bound);
    $('#FormF3Dtl_Width').val(data.width);
    $("#FormF3Dtl_Height").val(data.height);
    $("#FormF3Dtl_Description").val(data.description);
    $("#ddlCondition").val(data.conditionRating);
    $("#ddlCondition").trigger('chosen:updated');
    $("#ddlCondition").trigger('change');

    if ($("#hdnView").val() == 1 || view == 1) {
        $('#ddlAsset').prop('disabled', true).trigger("chosen:updated");
        $('#ddlCondition').prop('disabled', true).trigger("chosen:updated");
        $("#FormF3Dtl_Description").attr("readonly", true);
        $("#btnSaveFormF3Dtl").hide();
    }
    else {
        if ($("#ddlSource").val() == "New") {
            $('#ddlAsset').prop('disabled', false).trigger("chosen:updated");
            $('#ddlCondition').prop('disabled', false).trigger("chosen:updated");
        }
        else {
            $('#ddlAsset').prop('disabled', true).trigger("chosen:updated");
            $('#ddlCondition').prop('disabled', true).trigger("chosen:updated");
        }
        $("#FormF3Dtl_Description").attr("readonly", false);
        $("#btnSaveFormF3Dtl").show();
    }

}

function ClearFormF3Dtl() {
    $('#ddlAsset').val("");
    $('#ddlAsset').trigger('chosen:updated');
    $('#ddlCondition').val("");
    $('#ddlCondition').trigger('chosen:updated');
    $("#FormF3Dtl_LocCh").val('');
    $("#FormF3Dtl_LocChDeci").val('');
    $("#FormF3Dtl_Code").val('');
    $("#FormF3Dtl_Bound").val('');
    $("#FormF3Dtl_Width").val('');
    $("#FormF3Dtl_Height").val('');
    $("#FormF3Dtl_Description").val('');
    $("#myModal").modal("hide");
}


function UpdateFormAfterSave(data) {

    $("#FormF3_PkRefNo").val(data.pkRefNo);
    $("#FormF3_PkRefId").val(data.refId);
    $("#FormF3_Status").val(data.status)

    $("#hdnPkRefNo").val(data.pkRefNo);
    $("#saveFormF3Btn").show();
    $("#SubmitFormF3Btn").show();

    if ($("#ddlSource").val() == "New")
        $("#btnDtlModal").show()
    BindAsset(data);
    DisableHeader();
    InitializeGrid();
}



function BindAsset(data) {
    var _asset = $("#ddlAsset");
    _asset.empty();
    _asset.append($("<option></option>").val("").html("Select Asset"));
    $.each(data.assetDS, function (index, v) {
        _asset.append($("<option></option>").val(v.value).html(v.text).attr("Item1", v.item1).attr("Item2", v.item2).attr("Item3", v.item3).attr("FromKm", v.fromKm).attr("FromM", v.fromM).attr("CValue", v.cValue));
    });

    _asset.trigger("chosen:updated");
    _asset.trigger("change");
}

function DisableHeader() {

    if ($("#FormF3_PkRefNo").val() != "0") {
        $("#headerDiv * > select").attr('disabled', true).trigger("chosen:updated");
        if ($("#hdnView").val() != 1 && $("#FormF3_SubmitSts").val() != true)
            $("#ddlCrew").attr('disabled', false).trigger("chosen:updated");
        $("#FormF3_Dist").attr("readonly", "true");
        $("#btnFindDetails").hide();
    }

}

function EnableDisableElements(state) {

    $('#headerDiv * > select').prop('disabled', state).trigger("chosen:updated");
    $('#ddlSource').prop('disabled', false).trigger("chosen:updated");

}

function DeleteFormF3Dtl(id) {

    InitAjaxLoading();
    $.post('/FormF3/DeleteFormF3Dtl?id=' + id, function (data) {
        HideAjaxLoading();
        if (data == -1) {
            app.ShowErrorMessage(data.errorMessage);
        }
        else {
            InitializeGrid();
            app.ShowSuccessMessage('Deleted Successfully', false);
        }
    });
}


function GoBack() {
    if ($("#hdnView").val() == "0") {
        if (app.Confirm("Unsaved changes will be lost. Are you sure you want to cancel?", function (e) {
            if (e) {
                location.href = "/FormF3";

            }
        }));
    }
    else
        location.href = "/FormF3";
}