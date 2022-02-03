var jsAdmin = new function () {
    this.HeaderGrid = new function () {
        this.ActionRender = function (data, type, row, meta) {
            var actionSection = "<div class='btn-group dropright' rowidx='" + meta.row + "'><button type='button' class='btn btn-sm btn-themebtn dropdown-toggle' data-toggle='dropdown'> Click Me </button>";
            actionSection += "<div class='dropdown-menu'>";//dorpdown menu start

            actionSection += "<button type='button' class='dropdown-item editdel-btns' onclick='jsAdmin.HeaderGrid.ActionClick(this);'>";
            actionSection += "<span class='edit-icon'></span> Edit </button>";

            //if (data.Active) {
                actionSection += "<button type='button' class='dropdown-item editdel-btns' onclick='jsAdmin.HeaderGrid.ActionClick(this);'>";
                actionSection += "<span class='del-icon'></span> Delete </button>";
            //}
            //else {
                //actionSection += "<button type='button' class='dropdown-item editdel-btns' onclick='jsAdmin.HeaderGrid.ActionClick(this);'>";
                //actionSection += "<span class='view-icon'></span> Activate </button>";
            //}
            actionSection += "</div>"; //dorpdown menu close
            actionSection += "</div>"; // action close

            return actionSection;
        }
        this.ActionClick = function (tis) {
            var obj = $(tis);
            var type = $.trim(obj.text());
            var rowidx = parseInt(obj.closest("[rowidx]").attr("rowidx"), 10);
            if (rowidx >= 0) {
                var data = tblGrid.dataTable.row(rowidx).data();
                switch (type.toLowerCase()) {
                    case "edit":
                        edit(data.pkRefNo);
                        break;
                    case "delete":
                        app.Confirm("Are you sure you want to delete this record? <br/>(Road: " + data.rdName + ")", (status) => {
                            if (status) {
                                GetResponseValue("DeleteRoad", "Administration", { PkRefNo: data.pkRefNo }, function (sdata) {
                                    tblGrid.Refresh();
                                    app.ShowSuccessMessage("Deleted Sucessfully! <br/>(Road: " + data.rdName + ")");
                                });
                            }
                        }, "Yes", "No");
                        break;
                }
            }
        }
        this.DateOfEntry = (data, type, row, meta) => {
            var result = "";
            if (data && data != null && data != "") {
                result = (new Date(data)).ToString(jsMaster.GridFormat);
            }
            return result;
        }
    }
}


var _hd = {
    ddlDivision: $("#ddlDivision"),
    txtDivisionCode: $("#txtDivisionCode"),
    ddlRmu: $("#ddlRmu"),
    txtRmuCode: $("#txtRmuCode"),
    ddlSection: $("#ddlSection"),
    txtSectionCode: $("#txtSectionCode"),
    txtRoadCategoryName: $("#txtRoadCategoryName"),
    txtRoadCategoryCode: $("#txtRoadCategoryCode"),
    txtRoadCode: $("#txtRoadCode"),
    txtRoadName: $("#txtRoadName"),
    txtLocationFrom: $("#txtLocationFrom"),
    txtLocationTo: $("#txtLocationTo"),
    txtChainageFromKm: $("#txtChainageFromKm"),
    txtChainageFromM: $("#txtChainageFromM"),
    txtChaingeToKm: $("#txtChaingeToKm"),
    txtChainageToM: $("#txtChainageToM"),
    txtTotalLength: $("#txtTotalLength"),
    txtPavedLength: $("#txtPavedLength"),
    txtUnpavedLength: $("#txtUnpavedLength"),
    ddlMaintainedBy: $("#ddlMaintainedBy"),
    ddlOwner: $("#ddlOwner"),
    txtRemarks: $("#txtRemarks"),
    txtFeatureId: $("#txtFeatureId"),
    hdnPkRefNo: $("#hdnPkRefNo"),
    btnCancel: $("#btnCancel"),
    btnSave: $("#btnSave")
}

$(document).ready(function () {
    _hd.ddlDivision.chosen();
    _hd.ddlRmu.chosen();
    _hd.ddlSection.chosen();
    _hd.ddlOwner.chosen();
    bindDivision();

    _hd.ddlDivision.on("change", function () {
        _hd.txtDivisionCode.val(this.value);
        bindRMU(this.value);
    });

    _hd.ddlRmu.on("change", function () {
        _hd.txtRmuCode.val(this.value);
        bindSection(_hd.ddlDivision.val(), this.value);
    });

    _hd.ddlSection.on("change", function () {
        _hd.txtSectionCode.val(this.value);
    });

    _hd.btnSave.on("click", function () {
        save();
    });

    _hd.btnCancel.on("click", function () {
        app.Confirm("Unsaved changes will be lost. Are you sure you want to cancel?", function (e) {
            if (e) {
                clearRoad();
            }
        });

    });

    $(".featureid").on("change", function () {
        generateFeatureId();
    });
});

function generateFeatureId() {
    if (_hd.ddlDivision.val() != "" && _hd.ddlRmu.val() != "" && _hd.txtRoadCode.val() && _hd.ddlSection.val() != "") {
        var result = `${_hd.ddlDivision.val()}/${_hd.ddlRmu.val()}/${_hd.ddlSection.val()}/${_hd.txtRoadCode.val()}`;
        _hd.txtFeatureId.val(result);
    }
    else {
        _hd.txtFeatureId.val('');
    }
}

function bindDivision(callback) {
    var req = {};
    $.ajax({
        url: "/Administration/GetDivList",
        dataType: "JSON", data: req, type: "Post",
        success: function (data) {
            _hd.ddlDivision.empty();
            _hd.ddlSection.empty();
            _hd.txtSectionCode.val('');
            _hd.txtRmuCode.val('');
            _hd.ddlDivision.append($("<option></option>").val("").html("Select Division"));
            $.each(data, function (index, v) {
                _hd.ddlDivision.append($("<option></option>").val(v.value).html(v.text));
            });

            _hd.ddlDivision.trigger("chosen:updated");
            if (callback)
                callback();
        }, error: function (data) { console.error(data); }
    });
}

function bindRMU(divcode, callback) {
    var req = {};
    req.code = divcode;
    $.ajax({
        url: "/Administration/GetRMUListByDiv",
        dataType: "JSON", data: req, type: "Post",
        success: function (data) {
            _hd.ddlRmu.empty();
            _hd.txtRmuCode.val('');
            _hd.ddlSection.empty();
            _hd.txtSectionCode.val('');
            _hd.ddlRmu.append($("<option></option>").val("").html("Select RMU"));
            $.each(data, function (index, v) {
                _hd.ddlRmu.append($("<option></option>").val(v.value).html(v.text));
            });

            _hd.ddlRmu.trigger("chosen:updated");
            if (callback)
                callback();
        }, error: function (data) { console.error(data); }
    });
}

function bindSection(div, rmu, callback) {
    var req = {};
    req.div = div;
    req.rmu = rmu;
    $.ajax({
        url: "/Administration/GetSectionListByDivAndRMU",
        dataType: "JSON", data: req, type: "Post",
        success: function (data) {
            _hd.ddlSection.empty();
            _hd.txtSectionCode.val('');
            _hd.ddlSection.append($("<option></option>").val("").html("Select Section Name"));
            $.each(data, function (index, v) {
                _hd.ddlSection.append($("<option></option>").val(v.value).html(v.text));
            });

            _hd.ddlSection.trigger("chosen:updated");
            if (callback)
                callback();
        }, error: function (data) { console.error(data); }
    });
}

function clearRoad() {
    _hd.ddlDivision.val('');
    _hd.ddlDivision.trigger('chosen:updated');
    _hd.txtDivisionCode.val('');
    _hd.ddlRmu.val('');
    _hd.ddlRmu.trigger('chosen:updated');
    _hd.txtRmuCode.val('');
    _hd.ddlSection.val('');
    _hd.ddlSection.trigger('chosen:updated');
    _hd.txtSectionCode.val('');
    _hd.txtRoadCategoryName.val('');
    _hd.txtRoadCategoryCode.val('');
    _hd.txtRoadCode.val('');
    _hd.txtRoadName.val('');
    _hd.txtLocationFrom.val('');
    _hd.txtLocationTo.val('');
    _hd.txtChainageFromKm.val('');
    _hd.txtChainageFromM.val('');
    _hd.txtChaingeToKm.val('');
    _hd.txtChainageToM.val('');
    _hd.txtTotalLength.val('');
    _hd.txtPavedLength.val('');
    _hd.txtUnpavedLength.val('');
    _hd.ddlMaintainedBy.val('');
    _hd.ddlMaintainedBy.trigger('chosen:updated');
    _hd.ddlOwner.val('');
    _hd.ddlOwner.trigger('chosen:updated');
    _hd.txtRemarks.val('');
    _hd.txtFeatureId.val('');
    _hd.hdnPkRefNo.val('');
    //_hd.txtFeatureId.prop("disabled", false);
    $(".border-error").removeClass("border-error");
    $("#AddRoad").modal("hide");
}

function save() {

    if (ValidatePage("#divSave", "", "validate")) {
        var req = {}
        req.PkRefNo = _hd.hdnPkRefNo.val();
        req.FeatureId = _hd.txtFeatureId.val();
        req.DivCode = _hd.ddlDivision.val();
        req.RmuCode = _hd.ddlRmu.val();
        req.SecName = _hd.ddlSection.find(":selected").text();
        req.RdCatgName = _hd.txtRoadCategoryName.val();
        req.RdCatgCode = _hd.txtRoadCategoryCode.val();
        req.RdCode = _hd.txtRoadCode.val();
        req.RdName = _hd.txtRoadName.val();
        req.FrmLoc = _hd.txtLocationFrom.val();
        req.ToLoc = _hd.txtLocationTo.val();
        req.FrmCh = _hd.txtChainageFromKm.val();
        req.FrmChDeci = _hd.txtChainageFromM.val();
        req.ToCh = _hd.txtChaingeToKm.val();
        req.ToChDeci = _hd.txtChainageToM.val();
        req.LengthPaved = _hd.txtPavedLength.val();
        req.LengthUnpaved = _hd.txtUnpavedLength.val();
        req.Owner = 'JKR SARAWAK';// _hd.ddlOwner.val();
        req.ActiveYn = true;
        req.SecCode = _hd.ddlSection.val();
        var rmuname = [];
        rmuname = _hd.ddlRmu.find(":selected").text().split('-');
        req.RmuName = rmuname[1];
        $.ajax({
            url: "/Administration/SaveRoad",
            dataType: "JSON", data: req, type: "Post",
            success: function (data) {
                if (data > 0) {
                    app.ShowSuccessMessage("Saved Successfully");
                    clearRoad();
                }
                if (data == -1) {
                    app.ShowErrorMessage("Road code already exists");
                }
                $("#btnSearch").click();
                $("#AddRoad").modal("hide");
            }
        });
    }
}

function edit(id) {
    var req = {};
    req.id = id;
    $.ajax({
        url: "/Administration/GetRoadById",
        dataType: "JSON", data: req, type: "Post",
        success: function (data) {
            if (data != null) {
                _hd.hdnPkRefNo.val(data.pkRefNo);
                _hd.txtFeatureId.val(data.featureId);
                _hd.txtDivisionCode.val(data.divCode);
                _hd.ddlDivision.val(data.divCode);
                _hd.ddlDivision.trigger('chosen:updated');
                _hd.txtRmuCode.val(data.rmuCode);
                bindRMU(_hd.ddlDivision.val(), function () {
                    _hd.ddlRmu.val(data.rmuCode);
                    _hd.ddlRmu.trigger("chosen:updated");
                    _hd.txtRmuCode.val(data.rmuCode);
                    bindSection(data.divCode, data.rmuCode, function () {
                        _hd.ddlSection.val(data.secCode);
                        _hd.ddlSection.trigger("chosen:updated");
                        _hd.txtSectionCode.val(data.secCode);
                    });
                });

                _hd.txtRoadCategoryName.val(data.rdCatgName);
                _hd.txtRoadCategoryCode.val(data.rdCatgCode);
                _hd.txtRoadCode.val(data.rdCode);
                _hd.txtRoadName.val(data.rdName);
                _hd.txtLocationFrom.val(data.frmLoc);
                _hd.txtLocationTo.val(data.toLoc);
                _hd.txtChainageFromKm.val(data.frmCh);
                _hd.txtChainageFromM.val(data.frmChDeci);
                _hd.txtChaingeToKm.val(data.toCh);
                _hd.txtChainageToM.val(data.toChDeci);
                _hd.txtPavedLength.val(data.lengthPaved);
                _hd.txtUnpavedLength.val(data.lengthUnpaved);
                _hd.ddlOwner.val(data.owner);
                _hd.ddlOwner.trigger("chosen:updated");
                _hd.txtFeatureId.prop("disabled", true);
                $("#AddRoad").modal("show");
                $('#title').text('Edit Road Detail');
                $(".border-error").removeClass("border-error");
            }
            else {
                app.ShowErrorMessage("Something went wrong!!!");
            }
        }, error: function (data) { console.error(data); }
    });
}
