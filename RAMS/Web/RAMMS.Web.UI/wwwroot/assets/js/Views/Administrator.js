var jsAdmin = new function () {
    this.PageName = "";
    this.HeaderGrid = new function () {
        this.ActionRender = function (data, type, row, meta) {
            var actionSection = "<div class='btn-group dropright' rowidx='" + meta.row + "'><button type='button' class='btn btn-sm btn-themebtn dropdown-toggle' data-toggle='dropdown'> Click Me </button>";
            actionSection += "<div class='dropdown-menu'>";//dorpdown menu start

            actionSection += "<button type='button' class='dropdown-item editdel-btns' onclick='jsAdmin.HeaderGrid.ActionClick(this);'>";
            actionSection += "<span class='edit-icon'></span> Edit </button>";

            if (data.Active) {
                actionSection += "<button type='button' class='dropdown-item editdel-btns' onclick='jsAdmin.HeaderGrid.ActionClick(this);'>";
                actionSection += "<span class='del-icon'></span> Delete </button>";
            }
            else {
                actionSection += "<button type='button' class='dropdown-item editdel-btns' onclick='jsAdmin.HeaderGrid.ActionClick(this);'>";
                actionSection += "<span class='view-icon'></span> Activate </button>";
            }
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
                        switch (jsAdmin.PageName) {
                            case "section":
                                $("#selDivision").val(data.Div).trigger("change").trigger("chosen:updated");
                                $("#selRMU").val(data.RMUCode).trigger("change").trigger("chosen:updated");
                                $("#txtSectionCode").val(data.Code);
                                $("#txtSectionName").val(data.Desc);
                                $("#txtId").val(data.Id);
                                $(".addmodal").modal("show");
                                break;
                            case "rmu":
                                $("#selDivision").val(data.Div).trigger("change").trigger("chosen:updated");
                                $("#txtRMUCode").val(data.Code);
                                $("#txtRMUName").val(data.Desc);
                                $("#txtId").val(data.Id);
                                $(".addmodal").modal("show");
                                break;
                            case "assettype":
                                $("#selAssestGroupName").val(data.GrpName).trigger("change").trigger("chosen:updated");
                                $("#txtAssestTypeDesc").val(data.Desc);
                                $("#txtAssestTypeCode").val(data.Code);
                                $("#txtAssestTypeContractCode").val(data.ContractCode);
                                $("#txtId").val(data.Id);
                                $(".addmodal").modal("show");
                                break;
                            case "defect":
                                $("#selAssestGroupName").val(data.GrpName).trigger("change").trigger("chosen:updated");
                                $("#txtAssestTypeDesc").val(data.Desc);
                                $("#txtAssestTypeCode").val(data.Code);
                                $("#txtAssestTypeContractCode").val(data.ContractCode);
                                $("#selFormNo").val(data.FormNo).trigger("change").trigger("chosen:updated");
                                $("#txtId").val(data.Id);
                                $(".addmodal").modal("show");
                                break;
                        }
                        break;
                    case "delete":
                        app.Confirm("Are you sure you want to delete this record? <br/>(Ref: " + data.Desc + ")", (status) => {
                            if (status) {
                                GetResponseValue("Delete", "Administration", { PageName: jsAdmin.PageName, Id: data.Id }, function (sdata) {
                                    tblGrid.Refresh();
                                    app.ShowSuccessMessage("Deleted Sucessfully! <br/>(Ref: " + data.Desc + ")");
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
    this.Update = function (sel) {
        if (ValidatePage(sel, "", "")) {
            GetResponseValue("Update", "Administration", FormValueCollection(sel, { PageName: jsAdmin.PageName }), function (data) {
                if (data.IsSuccess) {
                    app.ShowSuccessMessage(data.Message);
                    tblGrid.Refresh();
                    jsAdmin.Cancel();
                }
                else {
                    app.ShowErrorMessage(data.Message);
                }
            }, "Saving");
        }
    }
    this.Cancel = function () {
        var modal = $(".addmodal");
        modal.modal('hide');
        modal.find("input").each(function () {
            $(this).val("");
        });
        modal.find("select").each(function () {
            $(this).val("").trigger("chosen:updated");
        });
    }
    this.DivChange = function (tis) {
        if (tis.value != "") {
            $("#txtDivisionCode").val(tis.value);
        }
        else {
            $("#txtDivisionCode").val("");
        }

        GetResponseValue("GetRMUListByDiv", "Administration", {
            Code: tis.value
        }, function (data) {
            var _hd = $("#selRMU");
            _hd.empty();
            _hd.append($("<option></option>").val("").html("Select RMU"));
            $.each(data, function (index, v) {
                _hd.append($("<option></option>").val(v.value).html(v.text));
            });

            _hd.trigger("chosen:updated");

        }, "Loading");


    }
    this.AssestGroupChange = function (tis) {
        if (tis.value != "") {
            $("#txtAssestGroupCode").val($(tis).find("option:selected").attr("cvalue"));
        }
        else {
            $("#txtAssestGroupCode").val("");
        }
    }
    this.RMUChange = function (tis) {
        if (tis.value != "") {
            $("#txtRMUCode").val(tis.value);
            //$("#txtRMUName").val($(tis).find("option:selected").attr("cvalue"));
        }
        else {
            $("#txtRMUCode").val("");
            //$("#txtRMUName").val("");
        }
    }
}