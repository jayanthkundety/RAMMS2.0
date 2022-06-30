
var _hd = {
    FormF1_PkRefId: $("#FormF1_PkRefId"),
    ddlDivision: $("#ddlDivision"),
    txtDist: $("#txtDist"),
    txtRmu: $("#txtRmu"),
    ddlYear: $("#ddlYear"),
    ddlRoadCode: $("#ddlRoadCode"),
    txtRoadName: $("#FormF1_RdName"),
    FormF1_RoadLength: $("#FormF1_RoadLength"),
    btnFindDetails: $("#btnFindDetails"),
    ddlCrewleader: $("#ddlCrewleader"),
    FormF1_CrewName: $("#FormF1_CrewName"),
    ddlInspectedby: $("#ddlInspectedby"),
    FormF1_CrewName: $("#FormF1_CrewName"),
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
    FormF1_SecName: $("#FormF1_SecName"),
    hdnRoadCode: $("#hdnRoadCode")
}



$(document).ready(function () {

    DisableHeader();

    if ($("#FormF1_PkRefNo").val() == 0)
        $("#btnDtlModal").hide();


    if ($("#hdnView").val() == 1) {
        $("#saveFormF1Btn").hide();
        $("#SubmitFormF1Btn").hide();
        $("#btnDtlModal").hide();
    }


    $("#ddlInspectedby").on("change", function () {
        var value = this.value;

        if (value == "") {
            $("#FormF1_InspectedName").val('');
            $("#FormF1_InspectedName").attr("readonly", "true");
        }
        else if (value == "99999999") {
            $("#FormF1_InspectedName").val('');
            $("#FormF1_InspectedName").removeAttr("readonly");
        }
        else {
            getUserDetail(value, function (data) {
                $("#FormF1_InspectedName").val(data.userName);
                $("#FormF1_InspectedDesg").val(data.position);
                $("#FormF1_InspectedDesg").attr("readonly", "true");
                
            });
        }
    });
    $("#ddlInspectedby").trigger('change');

    $("#ddlCrew").on("change", function () {

        var value = this.value;

        if (value == "") {
            $("#FormF1_CrewName").val('');
            $("#FormF1_CrewName").attr("readonly", "true");
        }
        else if (value == "99999999") {
            $("#FormF1_CrewName").val('');
            $("#FormF1_CrewName").removeAttr("readonly");
        }
        else {
            getUserDetail(value, function (data) {
                $("#FormF1_CrewName").val(data.userName);
                $("#FormF1_CrewName").attr("readonly", "true");
            });
        }
    });



    $("#ddlRMU").on("change", function () {
        // 

        $("#FormF1_RoadLength").val("");

        if (this.value == "") {
            $("#FormF1_PkRefId").val("");
            $("#FormF1_RoadLength").val("");
            _hd.ddlSection.val("");
            _hd.ddlRMU.trigger("chosen:updated");
            _hd.ddlSection.trigger("chosen:updated");
            _hd.ddlRoadCode.val("").trigger("chosen:updated");
            $("#FormF1_RdName").val("");
            $("#FormF1_SecName").val("");
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

        $("#FormF1_RoadLength").val("");
        bindRoadCode();
        if (this.value == "") {
            $("#FormF1_SecName").val("");
            $("#FormF1_PkRefId").val("");
            $("#FormF1_RoadLength").val("");
        }
        else {
            $("#FormF1_SecName").val($("#ddlSection").find("option:selected").text().split("-")[1]);

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

                $("#FormF1_RdName").val(data.roadName);
            });
            generateHeaderReference();


        }
        else {
            $("#FormF1_RdName").val("");
            $("#FormF1_PkRefId").val("");
            $("#FormF1_RoadLength").val("");

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
    $("#FormF1_SecName").val("");
    $("#FormF1_RdName").val("");

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
    $("#FormF1_RdName").val("");
    $("#FormF1_SecName").val("");

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
    $("#FormF1_RdName").val("");

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
            $("#FormF1_RoadLength").val(data);
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
            _hd.FormF1_SecName.val(data.FormF1_SecName);

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
        $("#FormF1_PkRefId").val(("CI/Form F1/" + roadcode + "/" + $("#ddlYear").val()));
    }
    else {
        $("#FormF1_PkRefId").val("");
    }
}


function OnAssetChange(tis) {

    var ctrl = $(tis);
    if (ctrl.val() != null)
        $('#FormF1Dtl_AssetID').val(ctrl.val());
    if ($('#FormF1Dtl_AssetID').val() != "") {

        $("#FormF1Dtl_LocCh").val(ctrl.find("option:selected").attr("FromKm"));
        $("#FormF1Dtl_LocChDeci").val(ctrl.find("option:selected").attr("FromM"));
        $("#FormF1Dtl_Code").val(ctrl.find("option:selected").attr("Item1"));
        $("#FormF1Dtl_Bound").val(ctrl.find("option:selected").attr("Item2"));
        $("#FormF1Dtl_Width").val(ctrl.find("option:selected").attr("Item3"));
        $("#FormF1Dtl_Height").val(ctrl.find("option:selected").attr("CValue"));

    }
    else {
        $("#FormF1Dtl_LocCh").val('');
        $("#FormF1Dtl_LocChDeci").val('');
        $("#FormF1Dtl_Code").val('');
        $("#FormF1Dtl_Bound").val('');
        $("#FormF1Dtl_Width").val('');
        $("#FormF1Dtl_Height").val('');
    }
}




function Save(GroupName, SubmitType) {


    if (SubmitType == "Submitted") {
        $("#FormF1_SubmitSts").val(true);
        $("#ddlCrew").addClass("validate");
    }

    if (ValidatePage('#headerDiv')) {

        if ($("#FormF1_Status").val() == "")
            $("#FormF1_Status").val("Initialize");
        else if ($("#FormF1_Status").val() == "Initialize")
            $("#FormF1_Status").val("Saved");

        InitAjaxLoading();
        EnableDisableElements(false);
        $.get('/FormF1/SaveFormF1', $("form").serialize(), function (data) {
            EnableDisableElements(true)
            HideAjaxLoading();
            if (data == -1) {
                app.ShowErrorMessage(data.errorMessage);
            }
            else {

                if (SubmitType == "") {
                    if (data.formExist) {
                        location.href = "/FormF1/Add?Id=" + data.pkRefNo + "&view=0";
                        return;
                    }
                    else if (data.pkRefNo == 0) {
                        EnableDisableElements(false)
                        app.ShowSuccessMessage('No matching records found in R1', false);
                        return;
                    }
                    else {
                        UpdateFormAfterSave(data);
                    }

                }
                else if (SubmitType == "Saved") {
                    app.ShowSuccessMessage('Saved Successfully', false);
                    location.href = "/FormF1/Index";
                }
                else if (SubmitType == "Submitted") {
                    app.ShowSuccessMessage('Submitted Successfully', false);
                    location.href = "/FormF1/Index";
                }
                else if (SubmitType == "Verified") {
                    process.ShowApprove(GroupName, SubmitType);
                }
            }
        });
    }

}

function SaveFormF1Dtl() {

    if (ValidatePage('#myModal')) {
        InitAjaxLoading();
        $.post('/FormF1/SaveFormF1Dtl', $("form").serialize(), function (data) {
            HideAjaxLoading();
            if (data == -1) {
                app.ShowErrorMessage(data.errorMessage);
            }
            else {
                ClearFormF1Dtl()

                InitializeGrid();
                app.ShowSuccessMessage('Saved Successfully', false);
            }
        });

    }
}

function EditFormF1Dtl(obj, view) {


    EditModeDtl = true;
    var currentRow = $(obj).closest("tr");
    var data = $('#FormF1DtlGridView').DataTable().row(currentRow).data();

    $("#FormF1Dtl_PkRefNo").val(data.pkRefNo);
    $("#FormF1Dtl_LocCh").val(data.frmCh);
    $("#FormF1Dtl_LocChDeci").val(data.frmChDec);
    $("#FormF1Dtl_Code").val(data.structureCode);
    $('#FormF1Dtl_Tier').val(data.tier);
    $('#FormF1Dtl_Width').val(data.width);
    $('#FormF1Dtl_BottomWidth').val(data.bottomWidth);
    $("#FormF1Dtl_Height").val(data.height);
    $("#FormF1Dtl_TotalLength").val(data.length);
    $("#FormF1Dtl_Description").val(data.description);
    $("#ddlCondition").val(data.conditionRating);
    $("#FormF1Dtl_AssetId").val(data.assetId);
  

    if ($("#hdnView").val() == 1 || view == 1) {
      
        $("#FormF1Dtl_Description").attr("readonly", true);
        $("#btnSaveFormF1Dtl").hide();
    }
    else {

        $("#FormF1Dtl_Description").attr("readonly", false);
        $("#btnSaveFormF1Dtl").show();
    }

}

function ClearFormF1Dtl() {
    
    $("#FormF1Dtl_Description").val('');
    $("#myModal").modal("hide");
}


function UpdateFormAfterSave(data) {

    $("#FormF1_PkRefNo").val(data.pkRefNo);
    $("#FormF1_PkRefId").val(data.refId);
    $("#FormF1_Status").val(data.status)

    $("#hdnPkRefNo").val(data.pkRefNo);
    $("#saveFormF1Btn").show();
    $("#SubmitFormF1Btn").show();


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

    if ($("#FormF1_PkRefNo").val() != "0") {
        $("#headerDiv * > select").attr('disabled', true).trigger("chosen:updated");

        $("#FormF1_Dist").attr("readonly", "true");
        $("#btnFindDetails").hide();
    }

}

function EnableDisableElements(state) {

    $('#headerDiv * > select').prop('disabled', state).trigger("chosen:updated");

}

function DeleteFormF1Dtl(id) {

    InitAjaxLoading();
    $.post('/FormF1/DeleteFormF1Dtl?id=' + id, function (data) {
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
                location.href = "/FormF1";

            }
        }));
    }
    else
        location.href = "/FormF1";
}