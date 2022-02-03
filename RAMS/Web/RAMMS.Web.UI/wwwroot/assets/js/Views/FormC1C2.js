var frmC1C2 = new function () {
    this.HeaderData = {};
    this.ImageList = [];
    this.Dis_Severity = {};
    this.ArDistress = {};
    this.DistressOthers = {};
    this.Severity = {};
    this.IsEdit = true;
    this.Pattern = "";
    this.FindDetails = function () {
        if (ValidatePage("#divFindDetailsFC1C2")) {
            //debugger;
            var tis = this;
            $("#AiAssetId").val($("#selAssetID option:selected").text());
            GetResponseValue("FindDetails", "FormC1C2", FormValueCollection("#divFindDetailsFC1C2"), function (data) {
                if (data) {
                    //debugger;
                    $("[finddetailhide]").hide();
                    $("#selAssetID,#formC1C2InsYear").prop("disabled", true).trigger("change").trigger("chosen:updated");
                    if (data.SubmitSts) {
                        window.location = _APPLocation + "FormC1C2/View/" + data.PkRefNo;
                    }
                    tis.HeaderData = data;
                    tis.PageInit();
                }
            }, "Finding");
        }
    }
    this.BindData = function () {
        //debugger;
        if (this.HeaderData && this.HeaderData.PkRefNo && this.HeaderData.PkRefNo > 0) {
            if (this.IsEdit) { this.IsEdit = this.HeaderData.SubmitSts ? false : true; }
            if (!this.IsEdit) {
                $("#ancAddImage").remove(); $("#ImageListTRTemplate td[deleteImg]").text("");
            }
            var tis = this;
            var assignFormat = jsMaster.AssignFormat;
            $("#divC1C2").find("input,select,textarea").filter("[name]").each(function () {
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
            tis.InspectionList();
            tis.RefreshImageList();
            $("#dtInspection").attr("min", this.HeaderData.YearOfInsp + "-01-01").attr("max", this.HeaderData.YearOfInsp + "-12-31");
        }
    }
    this.AssetIDChange = function (tis) { this.BindRefNumber(); }
    this.InsYearChange = function (tis) { this.BindRefNumber(); }
    this.BindRefNumber = function () {
        var tis = this;
        var yr = $("#formC1C2InsYear").val();
        var assid = $("#selAssetID");
        if (yr != "" && assid.val() != "") {
            $("#txtFormC1C2RefNum").val(tis.Pattern.replace("{AssetID}", assid.find(":selected").text()).replace("{Year}", yr));
        }
        else {
            $("#txtFormC1C2RefNum").val("");
        }
    }
    this.UserIdChange = function (tis) {
        var sel = $(tis);
        var opt = sel.find(":selected");
        var par = sel.closest("[userIdGroup]");
        var item1 = opt.attr("item1") ? opt.attr("item1") : "";
        if (item1 == "others") {
            par.find("[userName]").val("").addClass("validate").prop("disabled", false);
            par.find("[userDest]").val("").addClass("validate").prop("disabled", false);
        }
        else {
            var item2 = opt.attr("Item2") ? opt.attr("item2") : "";
            par.find("[userName]").val(item1).removeClass("validate").prop("disabled", true);
            par.find("[userDest]").val(item2).removeClass("validate").prop("disabled", true);
        }
    }
    this.Save = function (isSubmit) {
        //debugger;
        var tis = this;
        if (isSubmit) {
            $("#frmC1C2Data .svalidate").addClass("validate");
        }
        Validation.ResetErrStyles("#frmC1C2Data");
        $("#txtPhotoValidate").val(this.IsUploadAllImage(isSubmit) ? "valid" : "");
        if (ValidatePage("#frmC1C2Data", "", "")) {
            var refNo = $("#txtS1RefNumber");
            var action = isSubmit ? "Submit" : "Save";
            GetResponseValue(action, "FormC1C2", FormValueCollection("#AccordPage1,#AccordPage2,#FormC2TabPage2,#divApprovedInfo", tis.HeaderData), function (data) {
                app.ShowSuccessMessage('Successfully Saved', false);
                setTimeout(tis.NavToList, 2000);
            }, "Saving");
        }
        if (isSubmit) {
            $("#frmC1C2Data .svalidate").removeClass("validate");
        }
    }
    this.NavToList = function () {
        window.location = _APPLocation + "FormC1C2";
    }
    this.Cancel = function () {
        jsMaster.ConfirmCancel(() => { frmC1C2.NavToList(); });
    }
    this.ShowAddImage = function () {
        $("#myModal2").modal();
    }
    this.PhotoTypeChange = function (tis) {
        if (tis.value != "") {
            $("#files").prop("disabled", false);
        }
        else {
            $("#files").prop("disabled", true);
        }
    }
    this.RefreshImageList = function () {
        var tis = this;
        var post = {};
        post.headerId = frmC1C2.HeaderData.PkRefNo;
        GetResponseValue("ImageList", "FormC1C2", post, function (data) {
            if (data) {
                tis.BindImageList(data);
            }
        }, "Loading");
    }
    this.StanImageTypeCode = function (code) {
        return code.replace(/\(/g, "").replace(/\)/g, "").replace(/ /g, "");
    }
    this.IsUploadAllImage = function (isSubmit) {
        var isValid = true;
        if (isSubmit) {
            var imageList = $("#ImageViewBody");
            var error = "<div>One photo / image is required for below photo type.</div><ul>";
            $("#photoType option").each(function () {
                var val = $(this).val();
                if (val != "" && val != "Others") {
                    var cv = frmC1C2.StanImageTypeCode(val);
                    if (imageList.find("[" + cv + "]").length == 0 && imageList.find("[" + cv + "]").length <= 2) {
                        error += "<li><label class='error'>" + val + "</label></li>";
                        isValid = false;
                    }
                }
            });
            error += "</ul>";
            //if (!isValid) { Validation.ShowMessage(error); }
        }
        return isValid;
    }
    this.BindImageList = function (data) {
        var tbl = $("#tblImageList");
        var tbody = tbl.find("tbody");
        var trTemplate = $("#ImageListTRTemplate tbody tr");
        tbody.empty();
        var imageList = $("#ImageViewBody");
        imageList.empty();
        var imageListTemplate = $("#ImageViewBodyTemplate .container");
        var imageItemTemplate = $("#ImageViewBodyTemplate .item");
        this.ImageList = data;
        $.each(data, function (idx, obj) {
            var tr = trTemplate.clone();
            tr[0].Data = obj;
            tr.find("[imgsno]").text(idx + 1);
            tr.find("[imgtype]").text(obj.ImageTypeCode);
            tr.find("[imgfilename]").text(obj.ImageFilenameSys);
            tbody.append(tr);

            var cattr = frmC1C2.StanImageTypeCode(obj.ImageTypeCode); //.replace(/\(/g, "").replace(/\)/g, "").replace(/ /g, "");
            var ctrl = imageList.find("div.container[" + cattr + "]");
            if (ctrl.length == 0) {
                var template = imageListTemplate.clone();
                template.attr(cattr, "");
                template.find("[phototype]").text(obj.ImageTypeCode);
                imageList.append(template);
                ctrl = template;
            }
            var arrFile = obj.ImageFilenameUpload.split(".");
            var ext = arrFile[arrFile.length - 1].toLowerCase();
            var imgExt = ",jpg,jpeg,png,jfif,gif,";
            var item = imageItemTemplate.clone();
            if (imgExt.indexOf("," + ext + ",") > -1) {
                item.find("[gallery]").append("<img class='item-img' src='" + _APPLocation + "Uploads/" + obj.ImageUserFilePath.replace(/\\/, "/") + "/" + obj.ImageFilenameUpload.replace(/\\/, "/") + "' alt='Image_Unavailable' />");
                item.find("[gallery]").append("<span class='caption'>" + obj.ImageFilenameSys + "</span>");
            }
            else {
                item.find("[gallery]").append("<img class='item-img' src='" + _APPLocation + "Uploads/FormX/WAR/file-icon.png' alt='Image_Unavailable' />");
                item.find("[gallery]").append("<a href='" + _APPLocation + "Uploads/" + obj.ImageUserFilePath.replace(/\\/, "/") + "/" + obj.ImageFilenameUpload.replace(/\\/, "/") + "' class='captionDocs' target='_blank' title='Click here to View the Document'>" + obj.ImageFilenameSys + "</a>");

            }
            ctrl.append(item);
        });
    }
    this.DeleteImg = function (tis) {
        var tr = $(tis).closest("tr");
        var obj = tr[0].Data;
        var tis = this;
        app.Confirm("Are you sure you want to delete the selected image?<br/> (File Name : " + obj.ImageFilenameSys + ")", function (ok) {
            if (ok) {
                var post = {};
                post.imgId = obj.PkRefNo;
                post.headerId = obj.hPkRefNo;
                GetResponseValue("DeleteImage", "FormC1C2", post, function (data) {
                    if (data) {
                        app.ShowSuccessMessage("Deleted Sucessfully! <br/>(File Name : " + obj.ImageFilenameSys + ")");
                        tis.BindImageList(data);
                    }
                }, "Deleting");
            }
        }, "Yes", "No");
    }
    this.PageInit = function () {
        if (frmC1C2.HeaderData && frmC1C2.HeaderData.PkRefNo && frmC1C2.HeaderData.PkRefNo > 0) {
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

            if (data.Status != "Submitted" && tblFC1C2HGrid.Base.IsModify) {
                actionSection += "<button type='button' class='dropdown-item editdel-btns' onclick='frmC1C2.HeaderGrid.ActionClick(this);'>";
                actionSection += "<span class='edit-icon'></span> Edit </button>";
            }
            if (tblFC1C2HGrid.Base.IsView) {
                actionSection += "<button type='button' class='dropdown-item editdel-btns' onclick='frmC1C2.HeaderGrid.ActionClick(this);'>";
                actionSection += "<span class='view-icon'></span> View </button>";
            }
            if (tblFC1C2HGrid.Base.IsDelete) {
                actionSection += "<button type='button' class='dropdown-item editdel-btns' onclick='frmC1C2.HeaderGrid.ActionClick(this);'>";
                actionSection += "<span class='del-icon'></span> Delete </button>";
            }
            actionSection += "<button type='button' class='dropdown-item editdel-btns' onclick='frmC1C2.HeaderGrid.ActionClick(this);'>";
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
                var data = tblFC1C2HGrid.dataTable.row(rowidx).data();
                switch (type.toLowerCase()) {
                    case "edit":
                        window.location = _APPLocation + "FormC1C2/Edit/" + data.RefNo;
                        break;
                    case "view":
                        window.location = _APPLocation + "FormC1C2/View/" + data.RefNo;
                        break;
                    case "delete":
                        app.Confirm("Are you sure you want to delete this record? <br/>(Ref: " + data.RefID + ")", (status) => {
                            if (status) {
                                DeleteRequest("Delete/" + data.RefNo, "FormC1C2", {}, function (sdata) {
                                    tblFC1C2HGrid.Refresh();
                                    app.ShowSuccessMessage("Deleted Sucessfully! <br/>(Ref: " + data.RefID + ")");
                                });
                            }
                        }, "Yes", "No");
                        break;
                    case "print":
                        window.location = _APPLocation + "FormC1C2/download?id=" + data.RefNo;
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
        this.DateOfIns = (data, type, row, meta) => {
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
                req.SectionCode = ctrl.find("option:selected").attr("code");
                req.RdCode = '';
                req.GrpCode = "CV"
                frmC1C2.DropDownBind(req);
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

            //frmC1C2.FilterAssestID();
        }
        this.RoadCodeChange = function (tis, isAdd) {
            //debugger;
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
                frmC1C2.DropDownBind(req);
                var div = $("#selDivision");
                if (div.val() == "") { div.val("MIRI").trigger("chosen:updated"); }

            }
            else {
                $("#txtRoadName").val('');
            }
            //var sel = $(tis);
            //var obj = sel.find("option:selected");
            //$("#txtRoadName").val(sel.find("option:selected").attr("Item1"));
            //if (sel.val() != "") {
            //    var sec = $("#selSectionCode");
            //    if (sec.val() == "") { sec.val(sec.find("[code='" + obj.attr("item2") + "']").val()).trigger("chosen:updated"); $("#txtSectionName").val(sec.val()); }
            //    var rmu = $("#selRMU");
            //    if (rmu.val() == "") { rmu.val(obj.attr("cvalue")).trigger("chosen:updated"); }

            //}
            frmC1C2.FilterAssestID();
        }
        this.RmuChange = function (tis) {
            var ctrl = $(tis);
            $("#selAssetID").val("").trigger("change").trigger("chosen:updated");
            if (ctrl.val() != null) {
                var req = {};
                req.RMU = ctrl.find("option:selected").attr("code")
                req.SectionCode = '';
                req.RdCode = '';
                req.GrpCode = "CV"
                frmC1C2.DropDownBind(req);
                //var sec = $("#selSectionCode"); sec.find("option").hide().filter("[cvalue='" + $(tis).find("option:selected").attr("cvalue") + "']").show();
                //if (sec.find("option:selected:visible").length == 0) { sec.val("").trigger("change"); }
                //sec.trigger("chosen:updated");
                //var ctrl = $("#selRoadCode"); ctrl.find("option").hide().filter("[cvalue='" + tis.value + "']").show(); ctrl.val("").trigger("change").trigger("chosen:updated");

            }
            else { var ctrl = $("#selRoadCode,#selSectionCode"); ctrl.find("option:hidden").show(); ctrl.val("").trigger("change").trigger("chosen:updated"); }

            var div = $("#selDivision");
            if (div.val() == "") { div.val("MIRI").trigger("chosen:updated"); }
            frmC1C2.FilterAssestID();
        }
    }
    this.InspectionList = function () {
        if (this.HeaderData && this.HeaderData.PkRefNo && this.HeaderData.PkRefNo > 0 && frmC1C2.HeaderData.InsDtl && frmC1C2.HeaderData.InsDtl.length > 0) {
            var tis = this;
            var dtl = frmC1C2.HeaderData.InsDtl;
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

    this.InitDis_Severity = function () {
        var _name = "";
        var tis = this;
        tis.Severity = [];
        $.each(this.Dis_Severity, function (idx, obj) {
            //debugger;
            _name = obj.Name.replace("C1C2_", "");
            if (obj.Remarks == "DISTRESS") {
                if (!tis.ArDistress[_name]) { tis.ArDistress[_name] = []; }
                var _dis = tis.ArDistress[_name];
                _dis[_dis.length] = { Text: obj.Desc, Value: obj.Value };

            }
            else if (_name == "Severity") {
                tis.Severity[tis.Severity.length] = { Text: obj.Desc, Value: parseInt(obj.Value) };
            }
        });
        _name = "";
        this.Dis_Severity = null;
    }
    this.FilterAssestID = function () {
        var asset = $("#selAssetID");
        if (asset.length > 0) {
            var opt = asset.find("option").show();

            var rmu = $("#selRMU");
            if (rmu.val() != "") { opt.filter(":not([rmu='" + rmu.val() + "'])").hide(); }

            var sec = $("#selSectionCode");
            if (sec.val() != "") { opt.filter(":not([scode='" + sec.find("option:selected").attr("code") + "'])").hide(); }

            var rd = $("#selRoadCode");
            if (rd.val() != "") { opt.filter(":not([rdcode='" + rd.val() + "'])").hide(); }
            asset.val("").trigger("chosen:updated");
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
        frmC1C2.UserIdChange(this);
    });
    frmC1C2.InitDis_Severity();
    frmC1C2.PageInit();
    $("#smartSearch").focus();//Header Grid focus    
    if ($("#btnFindDetails:visible").length > 0) {
        setTimeout(function () { $('#selDivision').trigger('chosen:activate'); }, 200);
    }
    else {
        setTimeout(function () { $("#formC1C2InspectedBy").trigger('chosen:activate'); }, 200);
    }

    //Listener for Smart and Detail Search
    $("#FC1C2SrchSection").find("#smartSearch").focus();
    element = document.querySelector("#formC1C2AdvSearch");
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