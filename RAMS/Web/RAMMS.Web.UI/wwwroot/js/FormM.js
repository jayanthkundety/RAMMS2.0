var frmM = new function () {
    this.HeaderData = {};
    this.ImageList = [];
    this.Dis_Severity = {};
    this.ArDistress = {};
    this.DistressOthers = {};
    this.Severity = {};
    this.IsEdit = true;
    this.Pattern = "";
    this.FindDetails = function () {
        if (ValidatePage("#divFindDetailsFM")) {
            //debugger;
            var tis = this;
            GetResponseValue("FindDetails", "FormM", FormValueCollection("#divFindDetailsFM"), function (data) {
                if (data) {
                    $("[finddetailhide]").hide();
                    if (data.SubmitSts) {
                        window.location = _APPLocation + "FormM/View/" + data.PkRefNo;
                    }
                    $('#txtFormMRefNum').val(data.RefId)
                    tis.HeaderData = data;
                    tis.PageInit();                    
                }
            }, "Finding");
        }
    }
    this.BindData = function () {
        if (this.HeaderData && this.HeaderData.PkRefNo && this.HeaderData.PkRefNo > 0) {
            if (this.IsEdit) { this.IsEdit = this.HeaderData.Status == "Approved" ? false : true; }
            if (!this.IsEdit) {
                $("[finddetailsdep]").hide();
            }
            var tis = this;
            var assignFormat = jsMaster.AssignFormat;

            $("#divSubHeader").find("input,select,textarea").filter("[name]").each(function () {
                debugger;
                var obj = $(this);
                var name = obj.attr("name");
                if (tis.HeaderData[name] != null) {
                    if (this.type == "select-one") { obj.val("" + tis.HeaderData[name]).trigger("change").trigger("chosen:updated"); }
                    else if (this.type == "date") { obj.val((new Date(tis.HeaderData[name])).ToString(assignFormat)); }
                    else {
                        obj.val(tis.HeaderData[name]);
                    }
                }
                else { obj.val(""); }
                if (!tis.IsEdit) {
                    obj.prop("disabled", true);
                    if (this.type == "select-one") { obj.trigger("chosen:updated"); };
                }
            });

            $("#divR1R2").find("input,select,textarea").filter("[name]").each(function () {
                var obj = $(this);
                var name = obj.attr("name");
                if (tis.HeaderData[name] != null) {
                    if (this.type == "select-one") { obj.val("" + tis.HeaderData[name]).trigger("change").trigger("chosen:updated"); }
                    else if (this.type == "date") { obj.val((new Date(tis.HeaderData[name])).ToString(assignFormat)); }
                    else {
                        obj.val(tis.HeaderData[name]);
                    }
                }
                else { obj.val(""); }
                if (!tis.IsEdit) {
                    obj.prop("disabled", true);
                    if (this.type == "select-one") { obj.trigger("chosen:updated"); };
                }
            });

            $("#pkRefNo").val(tis.HeaderData.PkRefNo);
            tis.LoadR2(tis.HeaderData.FormMAD);
            //tis.RefreshImageList();
        }
    }    
    this.LoadR2 = function (tis) { this.BindR2(tis); }
    this.BindR2 = (req) => {
        if (req == null) return;
        debugger;
        $('#A1TallyBox').val(req.A1tallyBox);
        $('#A1Total').val(req.A1total);
        $('#A2TallyBox').val(req.A2tallyBox);
        $('#A2Total').val(req.A2total);
        $('#A3TallyBox').val(req.A3tallyBox);
        $('#A3Total').val(req.A3total);
        $('#A4TallyBox').val(req.A4tallyBox);
        $('#A4Total').val(req.A4total);
        $('#A5TallyBox').val(req.A5tallyBox);
        $('#A5Total').val(req.A5total);
        $('#A6TallyBox').val(req.A6tallyBox);
        $('#A6Total').val(req.A6total);
        $('#A7TallyBox').val(req.A7tallyBox);
        $('#A7Total').val(req.A7total);
        $('#A8TallyBox').val(req.A8tallyBox);
        $('#A8Total').val(req.A8total);


        $('#B1TallyBox').val(req.B1tallyBox);
        $('#B1Total').val(req.B1total);
        $('#B2TallyBox').val(req.B2tallyBox);
        $('#B2Total').val(req.B2total);
        $('#B3TallyBox').val(req.B3tallyBox);
        $('#B3Total').val(req.B3total);
        $('#B4TallyBox').val(req.B4tallyBox);
        $('#B4Total').val(req.B4total);
        $('#B5TallyBox').val(req.B5tallyBox);
        $('#B5Total').val(req.B5total);
        $('#B7TallyBox').val(req.B7tallyBox);
        $('#B7Total').val(req.B7total);
        $('#B8TallyBox').val(req.B8tallyBox);
        $('#B8Total').val(req.B8total);
        $('#B9TallyBox').val(req.B9tallyBox);
        $('#B9Total').val(req.B9total);

        $('#C1TallyBox').val(req.C1tallyBox);
        $('#C1Total').val(req.C1total);
        $('#C2TallyBox').val(req.C2tallyBox);
        $('#C2Total').val(req.C2total);

        $('#D1TallyBox').val(req.D1tallyBox);
        $('#D1Total').val(req.D1total);
        $('#D2TallyBox').val(req.D2tallyBox);
        $('#D2Total').val(req.D2total);
        $('#D3TallyBox').val(req.D3tallyBox);
        $('#D3Total').val(req.D3total);
        $('#D4TallyBox').val(req.D4tallyBox);
        $('#D4Total').val(req.D4total);
        $('#D5TallyBox').val(req.D5tallyBox);
        $('#D5Total').val(req.D5total);
        $('#D6TallyBox').val(req.D6tallyBox);
        $('#D6Total').val(req.D6total);
        $('#D7TallyBox').val(req.D7tallyBox);
        $('#D7Total').val(req.D7total);
        $('#D8TallyBox').val(req.D8tallyBox);
        $('#D8Total').val(req.D8total);

        $('#E1TallyBox').val(req.E1tallyBox);
        $('#E1Total').val(req.E1total);
        $('#E2TallyBox').val(req.E2tallyBox);
        $('#E2Total').val(req.E2total);

        $('#F1TallyBox').val(req.F1tallyBox);
        $('#F1Total').val(req.F1total);
        $('#F2TallyBox').val(req.F2tallyBox);
        $('#F2Total').val(req.F2total);
        $('#F3TallyBox').val(req.F3tallyBox);
        $('#F3Total').val(req.F3total);
        $('#F4TallyBox').val(req.F4tallyBox);
        $('#F4Total').val(req.F4total);
        $('#F5TallyBox').val(req.F5tallyBox);
        $('#F5Total').val(req.F5total);
        $('#F6TallyBox').val(req.F6tallyBox);
        $('#F6Total').val(req.F6total);
        $('#F7TallyBox').val(req.F7tallyBox);
        $('#F7Total').val(req.F7total);

        $('#G1TallyBox').val(req.G1tallyBox);
        $('#G1Total').val(req.G1total);
        $('#G2TallyBox').val(req.G2tallyBox);
        $('#G2Total').val(req.G2total);
        $('#G3TallyBox').val(req.G3tallyBox);
        $('#G3Total').val(req.G3total);
        $('#G4TallyBox').val(req.G4tallyBox);
        $('#G4Total').val(req.G4total);
        $('#G5TallyBox').val(req.G5tallyBox);
        $('#G5Total').val(req.G5total);
        $('#G6TallyBox').val(req.G6tallyBox);
        $('#G6Total').val(req.G6total);
        $('#G7TallyBox').val(req.G7tallyBox);
        $('#G7Total').val(req.G7total);
        $('#G8TallyBox').val(req.G8tallyBox);
        $('#G8Total').val(req.G8total);
        $('#G9TallyBox').val(req.G9tallyBox);
        $('#G9Total').val(req.G9total);
        $('#G10TallyBox').val(req.G10tallyBox);
        $('#G10Total').val(req.G10total);       

    }
    this.InAuditDateChange = function (tis) { this.BindRefNumber(); }
    this.BindRefNumber = function () {
        debugger;
        //var tis = this;
        //var dateAudit = $("#formMAuditDate").val();
        //var rdCode = $('#selRoadCode').find("option:selected").attr("code");
        //if (rdCode != "" && dateAudit != "") {
        //    $("#txtFormMRefNum").val(tis.Pattern.replace("{RoadCode}", rdCode).replace("{YYYYMMDD}", dateAudit).replace("{ActivityCode}",""));
        //}
        //else {
        //    $("#txtFormMRefNum").val("");
        //}
    }
    this.UserIdChange = function (tis) {
        debugger;
        var sel = $(tis);
        var opt = sel.find(":selected");
        var par = sel.closest("[userIdGroup]");
        var item1 = opt.attr("item1") ? opt.attr("item1") : "";
        if (item1 == "others") {
            par.find("[userName]").val("").addClass("validate").prop("disabled", false);
            par.find("[userDest]").val("").addClass("validate").prop("disabled", false);
            //par.find("[userOffice]").val("").addClass("validate").prop("disabled", false);
        }
        else {
            var item2 = opt.attr("Item2") ? opt.attr("item2") : "";
            var item3 = opt.attr("Item3") ? opt.attr("item3") : "";
            par.find("[userName]").val(item1).removeClass("validate").prop("disabled", true);
            par.find("[userDest]").val(item2).removeClass("validate").prop("disabled", true);
            //par.find("[userOffice]").val(item3).addClass("validate").prop("disabled", true);
        }
    }   
    this.Save = function (isSubmit, isApproveSave) {
        var tis = this;
        if (isSubmit) {
            $("#frmMData .svalidate").addClass("validate");
        }
        Validation.ResetErrStyles("#frmMData");
        if (ValidatePage("#frmMData", "", "")) {
            //var refNo = $("#txtS1RefNumber");
            var action = isSubmit ? "Submit" : "Save";
            if (isApproveSave == 1) {
                GetResponseValue(action, "FormM", FormValueCollection("#divFindDetailsFM,#AccordPage0,#AccordPage1,#AccordPage2,#AccordPage6,#AccordPage7,#AccordPage8,#AccordPage9,#AccordPage10,#AccordPage11,#AccordPage5,#divApprovedInfo", tis.HeaderData), function (data) {
                }, "Saving");
            }
            else {
                GetResponseValue(action, "FormM", FormValueCollection("#divFindDetailsFM,#AccordPage0,#AccordPage1,#AccordPage2,#AccordPage6,#AccordPage7,#AccordPage8,#AccordPage9,#AccordPage10,#AccordPage11,#AccordPage5,#divApprovedInfo", tis.HeaderData), function (data) {
                    app.ShowSuccessMessage('Successfully Saved', false);
                    setTimeout(tis.NavToList, 2000);
                }, "Saving");
            }
        }
        if (isSubmit) {
            $("#frmMData .svalidate").removeClass("validate");
        }
    }
    this.NavToList = function () {
        window.location = _APPLocation + "FormM";
    }
    this.Cancel = function () {
        jsMaster.ConfirmCancel(() => { frmM.NavToList(); });
    }
    this.PageInit = function () {
        if (frmM.HeaderData && frmM.HeaderData.PkRefNo && frmM.HeaderData.PkRefNo > 0) {
            $("[finddetailsdep]").show();
            $("#btnFindDetails").hide();
        }
        else {
            $("[finddetailsdep]").hide();
            $("#btnFindDetails").show();
            $("#selRMU").trigger("change");
        }
        this.BindData();
    }
    this.HeaderGrid = new function () {
        this.ActionRender = function (data, type, row, meta) {
            var actionSection = "<div class='btn-group dropright' rowidx='" + meta.row + "'><button type='button' class='btn btn-sm btn-themebtn dropdown-toggle' data-toggle='dropdown'> Click Me </button>";
            actionSection += "<div class='dropdown-menu'>";//dorpdown menu start

            if (data.ProcessStatus != "Approved" && tblFMHGrid.Base.IsModify) {
                actionSection += "<button type='button' class='dropdown-item editdel-btns' onclick='frmM.HeaderGrid.ActionClick(this);'>";
                actionSection += "<span class='edit-icon'></span> Edit </button>";
            }
            if (tblFMHGrid.Base.IsView) {
                actionSection += "<button type='button' class='dropdown-item editdel-btns' onclick='frmM.HeaderGrid.ActionClick(this);'>";
                actionSection += "<span class='view-icon'></span> View </button>";
            }
            if (tblFMHGrid.Base.IsDelete) {
                actionSection += "<button type='button' class='dropdown-item editdel-btns' onclick='frmM.HeaderGrid.ActionClick(this);'>";
                actionSection += "<span class='del-icon'></span> Delete </button>";
            }
            actionSection += "<button type='button' class='dropdown-item editdel-btns' onclick='frmM.HeaderGrid.ActionClick(this);'>";
            actionSection += "<span class='print-icon'></span> Print </button>";

            actionSection += "</div>"; //dorpdown menu close
            actionSection += "</div>"; // action close

            return actionSection;
        }
        this.ActionClick = function (tis) {
            var obj = $(tis);
            var type = $.trim(obj.text());
            var rowidx = parseInt(obj.closest("[rowidx]").attr("rowidx"), 10);
            if (rowidx >= 0) {
                var data = tblFMHGrid.dataTable.row(rowidx).data();
                switch (type.toLowerCase()) {
                    case "edit":
                        window.location = _APPLocation + "FormM/Edit/" + data.RefNo;
                        break;
                    case "view":
                        window.location = _APPLocation + "FormM/View/" + data.RefNo;
                        break;
                    case "delete":
                        app.Confirm("Are you sure you want to delete this record? <br/>(Ref: " + data.RefNo + ")", (status) => {
                            if (status) {
                                DeleteRequest("Delete/" + data.RefNo, "FormM", {}, function (sdata) {
                                    if (sdata.id == "-1") {
                                        app.ShowErrorMessage("Form M cannot be deleted, first delete Form F1");
                                        return false;
                                    }
                                    tblFMHGrid.Refresh();
                                    app.ShowSuccessMessage("Deleted Sucessfully! <br/>(Ref: " + data.RefNo + ")");
                                });
                            }
                        }, "Yes", "No");
                        break;
                    case "print":
                        window.location = _APPLocation + "FormM/download?id=" + data.RefNo;
                        break;
                }
            }
        }
        this.DateOfEntry = (data, type, row, meta) => {
            var result = "";
            if (row.InsDate && row.InsDate != null && row.InsDate != "") {
                result = (new Date(row.InsDate)).ToString(jsMaster.DisplayDateFormat);
                result = " (" + result + ")";
            }
            result = data + result;
            return result;
        }
        this.AuditDate = (data, type, row, meta) => {
            var result = "";
            if (data && data != "") {
                result = (new Date(data)).ToString(jsMaster.GridFormat);
            }
            return result;
        }
    }
    this.Search = new function () {
        this.SecCodeChange = function (tis, isAdd) {
            //var sel = $(tis);
            //var obj = sel.find("option:selected");            
            //var rmu = $("#selRMU");
            //var val = obj.attr("cvalue");
            var ctrl = $(tis);
            if (ctrl.val() != null) {
                var req = {};
                if (isAdd) {
                    req.RMU = $("[rmuCode]").attr("code");
                }
                else {
                    req.RMU = ""
                }
                req.SecCode = ctrl.find("option:selected").attr("code");
                req.RdCode = '';
                req.GrpCode = "G"
                frmM.DropDownBind(req);
                $("#txtSectionName").val(ctrl.find("option:selected").attr("value"));
            }
            else {
                $("#txtSectionName").val('');
            }
            //if (val) { rmu.val(val == "Batu Niah" ? "BTN" : "MRI").trigger("chosen:updated"); }
            //if (tis.value != "") {
            //    var ctrl = $("#selRoadCode"); ctrl.find("option").hide().filter("[item2='" + obj.attr("code") + "']").show(); ctrl.val("").trigger("change").trigger("chosen:updated");
            //}
            //else {
            //    var ctrl = $("#selRoadCode"); ctrl.find("option:hidden").show(); ctrl.val("").trigger("change").trigger("chosen:updated");
            //}
        }
        this.RoadCodeChange = function (tis, isAdd) {
           debugger;
            var ctrl = $(tis);
            $("#txtRoadName").val(ctrl.find("option:selected").attr("Item1"));
            if (ctrl.val() != null && ctrl.val() != "") {
                var req = {};
                if (isAdd) {
                    req.RMU = $("[rmuCode]").attr("code");
                    req.SectionCode = $("[sectionCode]").attr("code");
                }
                else {
                    req.RMU = "";
                    req.SectionCode = "";
                }
                req.RdCode = ctrl.find("option:selected").attr("code");
                req.GrpCode = "CV"
                frmM.DropDownBind(req);
                var div = $("#selDivision");
                if (div.val() == "") { div.val("MIRI").trigger("chosen:updated"); }                
            }
            else {
                $("#txtRoadName").val('');
            }         
        }
        this.ActivityCodeChange = function (tis, isAdd) {
            debugger;
            var ctrl = $(tis);
            var val = ctrl.find(":selected").text();
            val = val.split("-").length > 0 ? val.split("-")[1] : val;
            $("#txtActivityName").val(val);
            if (ctrl.val() != null && ctrl.val() != "") {
                var req = {};
                if (isAdd) {
                    req.RMU = $("[rmuCode]").attr("code");
                    req.SectionCode = $("[sectionCode]").attr("code");
                }
                else {
                    req.RMU = "";
                    req.SectionCode = "";
                }
                req.ActCode = ctrl.find("option:selected").attr("code");
                frmM.DropDownBind(req);
                var div = $("#selDivision");
                if (div.val() == "") { div.val("MIRI").trigger("chosen:updated"); }
            }
            else {
                $("#txtActivityName").val('');
            }
        }
        this.RmuChange = function (tis) {
            debugger;
            var ctrl = $(tis);
            if (ctrl.val() != null) {
                var req = {};
                req.RMU = ctrl.find("option:selected").attr("code")
                req.SectionCode = '';
                req.RdCode = '';
                req.GrpCode = "CV"
                frmM.DropDownBind(req);
                $("#txtRMUName").val(ctrl.find("option:selected").text());
                //var sec = $("#selSectionCode"); sec.find("option").hide().filter("[cvalue='" + $(tis).find("option:selected").attr("cvalue") + "']").show();
                //if (sec.find("option:selected:visible").length == 0) { sec.val("").trigger("change"); }
                //sec.trigger("chosen:updated");
                //var ctrl = $("#selRoadCode"); ctrl.find("option").hide().filter("[cvalue='" + tis.value + "']").show(); ctrl.val("").trigger("change").trigger("chosen:updated");

            }
            else {
                var ctrl = $("#selRoadCode,#selSectionCode");
                ctrl.find("option:hidden").show();
                ctrl.val("").trigger("change").trigger("chosen:updated");
                $("#txtRMUName").val("");
            }

            var div = $("#selDivision");
            if (div.val() == "") { div.val("MIRI").trigger("chosen:updated"); }
            //frmM.FilterAssestID();
        }
    }
    this.InspectionList = function () {
        if (this.HeaderData && this.HeaderData.PkRefNo && this.HeaderData.PkRefNo > 0 && frmR1R2.HeaderData.InsDtl && frmR1R2.HeaderData.InsDtl.length > 0) {
            var tis = this;
            var dtl = frmR1R2.HeaderData.InsDtl;
            var template = $("#divInsDtlTemplate");
            var _lst = $("#divInspectionList");

            $.each(dtl, function (idx, obj) {
                var _name = obj.mPkRefNoNavigation.InspName;
                var valmsg = "";
                var hdr = "";
                var cklblOthers = "";
                var exists = _lst.find("[tmpInsDtlHd" + obj.mPkRefNo + "]");
                if (exists.length == 0) {
                    hdr = template.find("[tmpInsDtlHd]").clone();
                    hdr.find("[Heading]").text(_name);
                    hdr.attr("tmpInsDtlHd" + obj.mPkRefNo, "").removeAttr("tmpInsDtlHd");
                    //var lblOthers ="lbl"+_name.replace(/[^a-zA-Z0-9]/g, "");
                    //hdr.find("[lblDistOthers]").addClass("" + lblOthers + "");
                    //hdr.find("[lblDistOthers]").attr("id", lblOthers);

                    _lst.append(hdr);
                    exists = hdr;
                    //if (obj.DistressOthers != null) {
                    //    hdr.find("#" + lblOthers).css("display", "block");
                    //}
                    //else {
                    //    hdr.find("#" + lblOthers).css("display", "none");
                    //    //hdr.find("#lblCulvertMarker").css("display", "none");
                    //}
                }
                var sctrl = template.find("[tmpInsDtlSubCtrl]").clone();

                /*********Inspection Type*********/
                if (obj.InspCodeDesc != "") {
                    valmsg = obj.InspCodeDesc + " (" + obj.InspCode + ")";
                    sctrl.find("[InsType]").val(obj.InspCodeDesc + " (" + obj.InspCode + ")");
                }
                else {
                    valmsg = _name + " (" + obj.InspCode + ")";
                    sctrl.find("[InsType]").val(_name + " (" + obj.InspCode + ")");
                }

                /*********Bind Distress**********/
                var selDist = sctrl.find("[ArDistress]");
                selDist.addClass("svalidate {req," + valmsg + " Distress}");

                var selDist1 = sctrl.find("[testDistOthers]");
                var tdisOthers = "div" + valmsg.replace(/[^a-zA-Z0-9]/g, "");
                console.log(tdisOthers);
                selDist1.attr('Id', tdisOthers);
                selDist1.addClass(tdisOthers);

                var selDistOthers = sctrl.find("[DistressOthers]");
                //selDistOthers.addClass("validate {req," + valmsg + " Distress Others} " + valmsg.replace(/[^a-zA-Z0-9]/g, "") + "");
                //selDistOthers.addClass("validate {req," + valmsg + " Distress Others}");

                var distress = tis.ArDistress[_name];
                if (distress) {
                    $.each(distress, function (idx, objDist) {
                        var opt = $("<option/>");
                        opt.val(objDist.Value).text(objDist.Value + (objDist.Text ? " - " + objDist.Text : ""));
                        selDist.append(opt);
                    });

                    if (obj.Distress != null && obj.Distress != undefined) {
                        var ar = obj.Distress.split(',');
                        selDist.val(ar);
                        selDist.trigger("chosen:updated");
                    }
                }
                if (obj.DistressOthers != null) {
                    var lst = obj.DistressOthers.split(',');
                    sctrl.find("[DistressOthers]").val(lst);
                    sctrl.find("#" + tdisOthers).css("display", "block");
                    //sctrl.find("[divCulvertMarker1A]").css("display", "block");
                    //selDistOthers.addClass("validate {req," + valmsg + " Distress Others}");
                }
                else {
                    sctrl.find("#" + tdisOthers).css("display", "none");
                    //selDistOthers.removeClass("validate {req," + valmsg + " Distress Others}");

                }
                /*********Bind Severity**********/
                var selSev = sctrl.find("[Severity]");
                selSev.addClass("svalidate {req," + valmsg + " Severity}");
                $.each(tis.Severity, function (idx, objSel) {
                    var opt = $("<option/>");
                    opt.val(objSel.Value).text(objSel.Value + (objSel.Text ? " - " + objSel.Text : ""));
                    if (obj.Severity && obj.Severity == objSel.Value) { opt.attr("selected", "selected"); }
                    selSev.append(opt);
                });
                if (!tis.IsEdit) {
                    selDist.prop("disabled", true);
                    selSev.prop("disabled", true);
                }
                sctrl[0].Details = obj;
                exists.find("[tmpInsDtlSub]").append(sctrl);

            });
            _lst.find("select").on("change", function () {
                //debugger;
                var par = $(this).closest("[tmpInsDtlSubCtrl]");
                if (this.hasAttribute("ArDistress")) {
                    var value = $(this).val();

                    if (value.length > 0) {
                        par[0].Details.Distress = "";
                        $.each(value, function (i, v) {
                            if (par[0].Details.Distress != "") {
                                par[0].Details.Distress += ",";
                            }
                            par[0].Details.Distress += v;
                        });
                    }
                    par[0].Details.ArDistress = $(this).val();

                    var _name = par[0].Details.mPkRefNoNavigation.InspName;
                    if (par[0].Details.InspCodeDesc != "") {
                        valmsg = par[0].Details.InspCodeDesc + "" + par[0].Details.InspCode;
                    }
                    else {
                        valmsg = _name + "" + par[0].Details.InspCode;

                    }

                    //var valmsg = par[0].Details.InspCodeDesc + " (" + par[0].Details.InspCode + ")";
                    var sctrl = template.find("[tmpInsDtlSubCtrl]").clone();
                    var test = "div" + valmsg.replace(/[^a-zA-Z0-9]/g, "");

                    //hdr = template.find("[tmpInsDtlHd]").clone();
                    //var lblOthers = "lbl" + _name.replace(/[^a-zA-Z0-9]/g, "");
                    //hdr.find("[lblDistOthers]").addClass("" + lblOthers + "");
                    //hdr.find("[lblDistOthers]").attr("id", lblOthers);

                    if (par[0].Details.ArDistress != null) {
                        if (par[0].Details.ArDistress.indexOf("C12") > -1 || par[0].Details.ArDistress.indexOf("C20") > -1 || par[0].Details.ArDistress.indexOf("C32") > -1
                            || par[0].Details.ArDistress.indexOf("C35") > -1) {
                            par.find("[DistressOthers]").css("display", "block");
                            par.find("#" + test).css("display", "block");
                            //par.find("[DistressOthers]").addClass("validate {req," + valmsg + " Distress Others}");

                        }
                        else {
                            par.find("[DistressOthers]").css("display", "none");
                            par.find("#" + test).css("display", "none");
                            //par.find("[DistressOthers]").removeClass("validate {req," + valmsg + " Distress Others}");
                            par.find("[DistressOthers]").val("");

                        }

                    }
                }
                else if (this.hasAttribute("Severity")) {
                    par[0].Details.Severity = $(this).val();
                }
            }).chosen();
            _lst.find("select").trigger("change");
        }
        this.DistressOthers = function (tis) {
            //debugger;
            var par = $(tis).closest("[tmpInsDtlSubCtrl]");
            par[0].Details.DistressOthers = $(tis).val();
            // console.log("test");
        }
    }
    this.DropDownBind = (req) => {
        _rmu = $("[rmuCode]");
        _sec = $("[sectionCode]");
        _road = $("[roadCode]");
        $.ajax({
            url: '/FormF2/RMUSecRoad',
            dataType: 'JSON',
            data: req,
            type: 'Post',
            success: function (data) {
                if (req.RMU == "") {
                    _rmu.empty();
                    if (data.rmu.length != 1) {
                        _rmu.append($("<option></option>").val("").html("Select RMU"));
                    }
                    $.each(data.rmu, function (index, x) {
                        _rmu.append($("<option></option>").val(x.value).html(x.text));
                    });
                    _rmu.trigger("chosen:updated");
                    $("#txtRMUName").val(_rmu.find("option:selected").attr("value"));
                }
                if (req.SectionCode == "") {
                    _sec.empty();
                    if (data.section.length != 1) {
                        _sec.append($("<option></option>").val("").html("Select Section Code"));
                    }
                    $.each(data.section, function (index, v) {
                        _sec.append($("<option></option>").val(v.value).html(v.text).attr("code", v.code));
                    });
                    _sec.trigger("chosen:updated");
                    $("#txtSectionName").val(_sec.find("option:selected").attr("value"));
                }
                if (req.RdCode == "") {
                    _road.empty();
                    _road.append($("<option></option>").val("").html("Select Road Code"));
                    $.each(data.rdCode, function (index, v) {
                        _road.append($("<option></option>").val(v.value).html(v.text).attr("Item1", v.item1).attr("Item3", v.item3).attr("PKId", v.pkId).attr("code", v.code));
                    });
                    _road.trigger("chosen:updated");
                    $("#txtRoadName").val(_road.find("option:selected").attr("Item1"));
                    //$("#F4HdrRdLength").val(_road.find("option:selected").attr("item3"));
                    //$("#RoadId").val(_road.find("option:selected").attr("pkid"))
                }
            },
            error: function (data) {

                console.error(data);
            }
        });
    }
}

$(document).ready(function () {
    $("[useridChange]").on("change", function () {
        frmM.UserIdChange(this);
    });
    
    frmM.PageInit();
    $("#smartSearch").focus();//Header Grid focus    
    if ($("#btnFindDetails:visible").length > 0) {
        setTimeout(function () { $('#selDivision').trigger('chosen:activate'); }, 200);
    }
    else {
        //setTimeout(function () { $("#formMInspectedBy").trigger('chosen:activate'); }, 200);
    }

    //Listener for Smart and Detail Search
    $("#FMSrchSection").find("#smartSearch").focus();
    element = document.querySelector("#formMAdvSearch");
    if (element) {
        element.addEventListener("keyup", () => {
            if (event.keyCode === 13) {
                $('[searchsectionbtn]').trigger('onclick');
            }
        });
    }
    $("#smartSearch").keyup(function () {
        if (event.keyCode === 13) {
            $('[searchsectionbtn]').trigger('onclick');
        }
    })

});

function FormValueCollection1(selector, post) {
    if (!post) { post = {}; }
    var obj = $(selector);
    var _form = obj.find("[name]:not(':radio'):not(':checkbox')");
    _form.each(function () {
        if (this.name == "DistressObserved1") {
            if ($(this).val() != "" && $(this).val() != null) {
                post[this.name] = $(this).val().filter(function (v) { return v !== '' }).join(",")
            }
        }
        else {
            post[this.name] = $(this).val();
        }
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

function TotalValue(tis) {
    debugger;
    var sel = $(tis);
}