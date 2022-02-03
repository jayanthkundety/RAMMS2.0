var formS1D = new function () {
    this.F1DetailInfo = {};
    this.FormS1 = {};
    this.FromDate = null;
    this.PlannedCode = 0;
    this.Refresh = null;
    this.SCButton = null;
    this.SEButton = null;
    this.Init = function () {
        // //debugger;
        $("#FormS1AdddetailsModalid").text((this.F1DetailInfo && this.F1DetailInfo.PkRefNo > 0) ? "Edit / Update Details" : "Add Details");
        this.InitDayScheduleDesign($("#tblPlanDaySchedule"));
        this.PlannedEventReg();

        this.InitDayScheduleDesign($("#tblActualDaySchedule"));
        this.ActualEventReg();
        this.PlannedCode = $("#divStatusListTemplate .wday-schl-icon").attr("code");
        this.BindFormData();
    }
    this.BindFormData = function () {

        var data = this.F1DetailInfo;
        var hasValue = data.PkRefNo ? true : false;
        var ctrl = $("#FormS1AdddetailsModal");
        var ref = ctrl.find("#txtDRefNumber");
        if (data && data.RefId) {
            ref.val(data.RefId);
            ref[0].PKId = data.PkRefNo;
        }
        else {
            ref.val(ref[0].FormS1RefId + "/????");
            ref[0].PKId = 0;
        }
        ctrl.find("#secFromFormA").hide().find("input").each(function () { $(this).val(""); });
        ctrl.find("#drpFormType").val(hasValue ? data.FormType : '').trigger("change").trigger("chosen:updated");
        //$("#drpFormType").val(hasValue ? data.FormType : '').trigger("change").trigger("chosen:updated");
        ctrl.find("#formS1DActivityCode").val(hasValue ? data.ActId : '').trigger("change").trigger("chosen:updated");
        ctrl.find("#formS1DRoadCode").val(hasValue ? data.RoadId : '').trigger("change").trigger("chosen:updated");
        ctrl.find("#formAFromCh").val(hasValue ? data.FrmChKm : '');
        ctrl.find("#formAFromChDeci").val(hasValue ? data.FrmChM : '');
        ctrl.find("#formAToCh").val(hasValue ? data.ToChKm : '');
        ctrl.find("#formAToChDeci").val(hasValue ? data.ToChM : '');
        var ref = ctrl.find("#drpFormS1DRefNo").val(hasValue ? data.FormTypeRefNo : '').trigger("change").trigger("chosen:updated");
        ref[0].selval = hasValue ? data.FormTypeRefNo : '';
        ref[0].seltype = hasValue ? data.FormType : '';
        ctrl.find("#formS1DCrewUser").val(hasValue ? data.CrewSupervisor : '').trigger("change").trigger("chosen:updated");
        if (hasValue && data.CrewSupervisorName && data.CrewSupervisorName != "") { ctrl.find("#txtCrewSupName").val(data.CrewSupervisorName); }

        //ctrl.find("#txtFASiteRef").val(data.FormASiteRef);
        //ctrl.find("#txtFAPriority").val(data.FormAPriority);
        //ctrl.find("#txtFAWorkQTY").val(data.FormAWorkQty);
        //ctrl.find("#txtFACDR").val(data.FormACdr);

        ctrl.find("#txtFASiteRef").val(hasValue ? data.FormASiteRef : '');
        ctrl.find("#txtFAPriority").val(hasValue ? data.FormAPriority : '');
        ctrl.find("#txtFAWorkQTY").val(hasValue ? data.FormAWorkQty : '');
        ctrl.find("#txtFACDR").val(hasValue ? data.FormACdr : '');

        ctrl.find("#sentJKRS")[0].checked = hasValue && data.SentToJkrs ? true : false;
        ctrl.find("#receivedJKRS")[0].checked = hasValue && data.RcvFromJkrs ? true : false;
        ctrl.find("#submittedJKRSDate").val(hasValue && data.SentToJkrsDt ? data.SentToJkrsDt.ToFormatDate(jsMaster.AssignFormat) : '');
        ctrl.find("#ReceivedJKRSDate").val(hasValue && data.RcvFromJkrsDt ? data.RcvFromJkrsDt.ToFormatDate(jsMaster.AssignFormat) : '');
        //this.FormDWorkStatus();
        this.LoadWKDetails();
        this.BindFollowUp();
        ctrl.find("#FS1DRemarks").val(hasValue && data.Remarks ? data.Remarks : '');
        $("#drpFormType").val(hasValue ? data.FormType : '').trigger("change").trigger("chosen:updated");
    }
    this.InitDayScheduleDesign = function (sel) {
        var dt = new Date(this.FromDate.getTime());
        var tbl = sel;
        var th = tbl.find("th:eq(0)");
        th.text(th.text().replace(/{YYYY}/g, dt.ToString("YYYY")));
        var ul = sel.find("[ulScheduled]");
        ul.empty();
        var tem = $("#divScheduleTemplate").children();
        for (var i = 0; i < 7; i++) {
            if (i > 0) { dt.setDate(dt.getDate() + 1); };
            var obj = tem.clone();
            obj.find(".day-name").text(dt.ToString("ddd"));
            obj.find(".dt").text(dt.ToString("DD"));
            obj.find(".mnth").text(dt.ToString("MMM"));
            var btn = obj.find("button");
            btn[0].SchldDate = new Date(dt.getTime());//.ToString("YYYY-MM-DDTHH:mm:ss:00.000") + "Z";
            btn[0].SchldDay = dt.getDay();
            ul.append(obj);
        }
    }
    this.PlannedEventReg = function () {
        $("#tblPlanDaySchedule [ulScheduled] li button").on("click", function () {

            var btn = $(this);
            if (btn.find("span").length == 0) { var tem = $("#divIconTemplate").children().clone(); tem.find("[icon]").addClass("wday-schl-icon")[0].code = formS1D.PlannedCode; tem.attr("title", "Workday scheduled"); btn.append(tem); }
            else { btn.empty(); }
        });
    }
    this.FormDWorkStatus = function () {
        ////debugger;
        var fromCHKM = $("#formAFromCh").val();
        var fromCHM = $("#formAFromChDeci").val();
        var toCHKM = $("#formAToCh").val();
        var toCHM = $("#formAToChDeci").val();
        if (fromCHKM != "" && fromCHM != "" && toCHKM != "" && toCHM != "") {
            var post = {};
            post.actCode = $("#formS1DActivityCode option:selected").attr("code");
            post.roadCode = $("#formS1DRoadCode option:selected").attr("code");
            post.frmCh = fromCHKM;
            post.frmchDeci = fromCHM;
            post.toCh = toCHKM;
            post.tochDeci = toCHM;
            post.CrewSupervisor = $("#formS1DCrewUser").val();
            post.weekNo = $("#drpWeekNo").val();
            GetResponseValue("GetFormDDetails", "FormS1", post, function (data) {
                var fdata = formS1D.F1DetailInfo;
                if (data) {
                    for (var j = 0; j < fdata.WkDtl.length; j++) {
                        for (var i = 0; i < data.Result.length; i++) {
                            if (data.Result[i].FormDFdhDay == "Monday" && fdata.WkDtl[j].SchldDayOfWeek == 1) {
                                fdata.WkDtl[j].FormDFddWorkSts = data.Result[i].FormDFddWorkSts;
                                fdata.WkDtl[j].FormDFdhDay = data.Result[i].FormDFdhDay;
                            }
                            if (data.Result[i].FormDFdhDay == "Tuesday" && fdata.WkDtl[j].SchldDayOfWeek == 2) {
                                fdata.WkDtl[j].FormDFddWorkSts = data.Result[i].FormDFddWorkSts;
                                fdata.WkDtl[j].FormDFdhDay = data.Result[i].FormDFdhDay;
                            }
                            if (data.Result[i].FormDFdhDay == "Wednesday" && fdata.WkDtl[j].SchldDayOfWeek == 3) {
                                fdata.WkDtl[j].FormDFddWorkSts = data.Result[i].FormDFddWorkSts;
                                fdata.WkDtl[j].FormDFdhDay = data.Result[i].FormDFdhDay;
                            }
                            if (data.Result[i].FormDFdhDay == "Thursday" && fdata.WkDtl[j].SchldDayOfWeek == 4) {
                                fdata.WkDtl[j].FormDFddWorkSts = data.Result[i].FormDFddWorkSts;
                                fdata.WkDtl[j].FormDFdhDay = data.Result[i].FormDFdhDay;
                            }
                            if (data.Result[i].FormDFdhDay == "Friday" && fdata.WkDtl[j].SchldDayOfWeek == 5) {
                                fdata.WkDtl[j].FormDFddWorkSts = data.Result[i].FormDFddWorkSts;
                                fdata.WkDtl[j].FormDFdhDay = data.Result[i].FormDFdhDay;
                            }
                            if (data.Result[i].FormDFdhDay == "Saturday" && fdata.WkDtl[j].SchldDayOfWeek == 6) {
                                fdata.WkDtl[j].FormDFddWorkSts = data.Result[i].FormDFddWorkSts;
                                fdata.WkDtl[j].FormDFdhDay = data.Result[i].FormDFdhDay;
                            }
                            if (data.Result[i].FormDFdhDay == "Sunday" && fdata.WkDtl[j].SchldDayOfWeek == 0) {
                                fdata.WkDtl[j].FormDFddWorkSts = data.Result[i].FormDFddWorkSts;
                                fdata.WkDtl[j].FormDFdhDay = data.Result[i].FormDFdhDay;
                            }
                        }
                    }
                    formS1D.F1DetailInfo = fdata;
                    // formS1D.LoadWKDetails();
                }
            }, "Loading");
        }
    }
    this.ActualEventReg = function () {

        $("#tblActualDaySchedule [ulScheduled] li button").on("click", function () {

            var btn = $(this);
            btn.closest("ul").find("[statuslegend]:visible").hide();
            var li = btn.closest("li");
            var leg = li.find("[statuslegend]");
            if (leg.length == 0) {
                var isLast = li.next().length > 0 ? false : true;
                var tem = $("#divStatusListTemplate").children().clone(); btn.after(tem); if (isLast) { tem.css("right", "0").css("left", "auto"); } tem.slideDown();
                tem.find("a").on("click", function (event) {

                    var anc = $(this);
                    var btn = anc.closest("li").find("button");
                    var lclassCtrl = anc.find("[legclass]");
                    var lclass = lclassCtrl.attr("legclass");
                    if (lclass != "") {
                        var tem = $("#divIconTemplate").children().clone(); tem.find("[icon]").addClass(lclass)[0].code = lclassCtrl.attr("code"); tem.attr("title", $.trim(anc.text())); btn.empty().append(tem);
                    }
                    else {
                        btn.empty();
                    }
                    anc.closest("[statuslegend]").slideUp();
                    event.preventDefault();
                    event.stopPropagation();
                });
            }
            else { leg.slideDown(); }
        });

        $("#tblFollowupGroup [ulFollowup] li button").on("click", function () {
            //debugger;
            var btn = $(this);
            btn.closest("ul").find("[statuslegend]:visible").hide();
            var li = btn.closest("li");
            var leg = li.find("[statuslegend]");
            if (leg.length == 0) {
                var isLast = li.next().length > 0 ? false : true;
                var tem = $("#divStatusListTemplate").children().clone(); btn.after(tem); if (isLast) { tem.css("top", "auto").css("right", "0").css("left", "auto"); } tem.slideDown();
                tem.find("a").on("click", function (event) {

                    var anc = $(this);
                    var btn = anc.closest("li").find("button");
                    var lclassCtrl = anc.find("[legclass]");
                    var lclass = lclassCtrl.attr("legclass");
                    if (lclass != "") {
                        var tem = $("#divIconTemplate").children().clone(); tem.find("[icon]").addClass(lclass)[0].code = lclassCtrl.attr("code"); tem.attr("title", $.trim(anc.text())); btn.empty().append(tem);
                    }
                    else {
                        btn.empty();
                    }
                    anc.closest("[statuslegend]").slideUp();
                    event.preventDefault();
                    event.stopPropagation();
                });
            }
            else { leg.slideDown(); }
        });
    }
    this.RodeCodeChange = function (tis) {
        //debugger;
        var ctrl = $(tis);
        if (ctrl.val() != "") {
            $("#txtFS1DRodeName").val(ctrl.find("option:selected").attr("Item1"));
            $("#formAFromCh").val(ctrl.find("option:selected").attr("fromkm"));
            $("#formAFromChDeci").val(ctrl.find("option:selected").attr("fromm"));
            $("#formAToCh").val(ctrl.find("option:selected").attr("tokm"));
            $("#formAToChDeci").val(ctrl.find("option:selected").attr("tom"));

        }
        else {
            $("#txtFS1DRodeName").val("");
        }
    }
    this.WkDetailsDateIdx = function (obj, dt) {
        var dtTime = dt.getTime();
        var idx = -1;
        for (var i = 0; i < obj.length; i++) {
            if (obj[i].SchldTime == dtTime) {
                idx = i;
                break;
            }
        }
        return idx;
    }
    this.LoadWKDetails = function () {
        // //debugger;
        var data = formS1D.F1DetailInfo;
        $("#tblPlanDaySchedule [ulScheduled] li button").each(function () {
            var btn = $(this);
            btn.empty();
            if (data.WkDtl && data.WkDtl.length > 0) {
                for (var i = 0; i < data.WkDtl.length; i++) {
                    if (btn[0].SchldDay == data.WkDtl[i].SchldDayOfWeek && formS1D.PlannedCode == data.WkDtl[i].Planned) {
                        var tem = $("#divIconTemplate").children().clone(); tem.find("[icon]").addClass("wday-schl-icon")[0].code = formS1D.PlannedCode;
                        tem.attr("title", "Workday scheduled");
                        btn.append(tem);
                    }
                }
            }
        });
        $("#tblActualDaySchedule [ulScheduled] li button").each(function () {
            var btn = $(this);
            btn.empty();
            if (data.WkDtl && data.WkDtl.length > 0) {
                for (var i = 0; i < data.WkDtl.length; i++) {
                    if (btn[0].SchldDay == data.WkDtl[i].SchldDayOfWeek && parseInt(data.WkDtl[i].Actual) > 0) {
                        var lclassCtrl = $("#divStatusListTemplate [code='" + $.trim(data.WkDtl[i].Actual) + "']")
                        var lclass = lclassCtrl.attr("legclass");
                        var anc = lclassCtrl.closest("a");
                        var tem = $("#divIconTemplate").children().clone();
                        tem.find("[icon]").addClass(lclass)[0].code = lclassCtrl.attr("code");
                        tem.attr("title", $.trim(anc.text())); btn.append(tem);
                    }
                }
            }
        });
    }
    this.BindWKDetails = function (post) {
        // //debugger;
        if (!post.WkDtl) { post.WkDtl = []; }
        var actIdx = post.WkDtl.length - 1;
        var actualBtns = $("#tblActualDaySchedule [ulScheduled] li button");
        var icount = -1;
        $("#tblPlanDaySchedule [ulScheduled] li button").each(function (liIdx) {
            icount++;
            var wkdtl = post.WkDtl[icount];
            if (!wkdtl) { post.WkDtl[icount] = {}; wkdtl = post.WkDtl[icount]; }
            else {
                wkdtl.Planned = null;
                wkdtl.Actual = null;
            }
            var objUpdate = false;
            var btn = $(this);
            var icon = btn.find("[icon]");
            var idx = -1;
            if (icon.length > 0) {
                wkdtl.SchldDayOfWeek = btn[0].SchldDay;
                wkdtl.SchldDate = btn[0].SchldDate.ToString("YYYY-MM-DD");
                wkdtl.Planned = icon[0].code;
                objUpdate = true;
            }
            var actbtn = actualBtns.filter(":eq(" + liIdx + ")");
            var acticon = actbtn.find("[icon]");
            if (acticon.length > 0) {
                if (!objUpdate) {
                    wkdtl.SchldDayOfWeek = actbtn[0].SchldDay;
                    wkdtl.SchldDate = actbtn[0].SchldDate.ToString("YYYY-MM-DD");
                }
                wkdtl.Actual = acticon[0].code;
                objUpdate = true;
            }
            if (!objUpdate) { icount--; }
        });
        if (actIdx != icount) {
            if (icount == -1) { post.WkDtl = []; }
            else {
                post.WkDtl = post.WkDtl.slice(0, icount + 1);
            }
        }
    }
    this.BindFollowUp = function (post) {
        var objBtns = $("#divfollowupgroup ul li button");
        //debugger;
        if (post && post != null) {
            $("#tblFollowupGroup [ulFollowup] li button").each(function (liIndx) {
                var btn = $(this);
                var icon = $(this).find('[icon]');
                var typename = btn[0].attributes[0].name;
                var code = null;
                if (icon.length > 0) {
                    code = icon[0].code;

                }
                switch (typename) {
                    case "ra":
                        post.FapRa = code;
                        break;
                    case "mt":
                        post.FapMt = code; break;
                    case "qa1":
                        post.FapQa1 = code; break;
                    case "qa2":
                        post.FapQa2 = code; break;
                    case "sa":
                        post.FapSa = code; break;
                    case "n1":
                        post.FapN1 = code; break;
                    case "n2":
                        post.FapN2 = code; break;
                }
            });
        }
        else {
            var data = this.F1DetailInfo;
            $("#tblFollowupGroup [ulFollowup] li button").each(function (liIndx) {
                var btn = $(this);
                var typename = btn[0].attributes[0].name;
                var code = '';
                switch (typename) {
                    case "ra":
                        code = data.FapRa;
                        break;
                    case "mt":
                        code = data.FapMt; break;
                    case "qa1":
                        code = data.FapQa1; break;
                    case "qa2":
                        code = data.FapQa2; break;
                    case "sa":
                        code = data.FapSa; break;
                    case "n1":
                        code = data.FapN1; break;
                    case "n2":
                        code = data.FapN2; break;
                }
                btn.empty();
                var lclassCtrl = $("#divStatusListTemplate [code='" + $.trim(code) + "']")
                var lclass = lclassCtrl.attr("legclass");
                var anc = lclassCtrl.closest("a");
                var tem = $("#divIconTemplate").children().clone();
                tem.find("[icon]").addClass(lclass)[0].code = lclassCtrl.attr("code");
                tem.attr("title", $.trim(anc.text())); btn.append(tem);
                //}
            });
            //data.FapMt ? objBtns.filter("[MT]").addClass("active") : objBtns.filter("[MT]").removeClass("active");
            //data.FapQa1 ? objBtns.filter("[QA1]").addClass("active") : objBtns.filter("[QA1]").removeClass("active");
            //data.FapQa2 ? objBtns.filter("[QA2]").addClass("active") : objBtns.filter("[QA2]").removeClass("active");
            //data.FapSa ? objBtns.filter("[SA]").addClass("active") : objBtns.filter("[SA]").removeClass("active");
            //data.FapN1 ? objBtns.filter("[N1]").addClass("active") : objBtns.filter("[N1]").removeClass("active");
            //data.FapN2 ? objBtns.filter("[N2]").addClass("active") : objBtns.filter("[N2]").removeClass("active");
        }
    }
    this.Save = function (IsExit) {
        //debugger;
        $("#hdnPlnWeekDay").val($("#tblPlanDaySchedule [ulScheduled] li button [icon]").length > 0 ? "1" : "");
        if (ValidatePage("#FormS1AdddetailsModal", "", "")) {
            var refNo = $("#txtDRefNumber");
            var _act = $("#formS1DActivityCode option:selected");
            var _road = $("#formS1DRoadCode option:selected");
            var post = formS1D.F1DetailInfo;
            post.PkRefNo = refNo[0].PKId ? refNo[0].PKId : 0;
            post.hPkRefNo = refNo[0].FormS1Id;
            post.ActCode = _act.attr("code");
            post.ActName = _act.attr("cvalue");
            post.RoadCode = _road.attr("code");
            post.RoadName = _road.attr("item1");
            post.HdrWeekNo = $("#drpWeekNo").val();
            this.BindWKDetails(post);
            this.BindFollowUp(post);
            GetResponseValue("SaveDetails", "FormS1", FormValueCollection("#FormS1AdddetailsModal", post), function (data) {
                //debugger;
                if (data != null) {
                    if (data.Details.IsExist) {
                        if (post.PkRefNo > 0) {
                            refNo[0].PKId = data.Details.PkRefNo;
                            refNo.val(data.Details.RefId);
                            formS1D.F1DetailInfo = data.Details;
                            if (!IsExit) {
                                formS1D.Close();
                            }
                        }
                        else if (!IsExit) {
                            formS1D.F1DetailInfo = {};
                            formS1D.Init();
                        }
                        if (IsExit) {
                            formS1D.Close();
                        }

                        app.ShowSuccessMessage('Successfully Saved', false);
                    }
                    else {
                        // formS1D.Close();
                        app.ShowInfoMessage('Record already exits');
                    }
                }

            }, "Saving");
        }
    }
    this.Cancel = function () {
        jsMaster.ConfirmCancel(() => { formS1D.Close(); });
    }
    this.Close = function () {
        $('#FormS1AdddetailsModal').modal('hide');
        if (formS1D.Refresh && formS1D.Refresh != null) { formS1D.Refresh(); }
    }
    this.FormTypeChange = function (tis) {
        //        //debugger;
        var sel = $(tis);
        var ref = $("#drpFormS1DRefNo");
        ref.find("option:not(:eq(0))").remove(); ref.find("option:not(:eq(0))").remove();
        var actCode = $("#formS1DActivityCode");
        var rdCode = $("#formS1DRoadCode").val();
        if (sel.val() != "") {
            switch (sel.val()) {
                case "New":
                    ref.trigger("chosen:updated");
                    ref.closest(".form-group").hide();
                    $("#secFromFormA").hide();
                    ref.removeClass("validate");
                    break;
                case "FormS2":
                    if (actCode.val() != "" && rdCode != "") {
                        ref.addClass("validate");
                        ref.closest(".form-group").show();
                        GetResponseValue("GetDetailsActiveRefIDs", "FormS2", { activityCode: actCode.find(":selected").attr("pid"), roadCode: rdCode }, function (data) {
                            if (data) {
                                $.each(data, function (idx, obj) {
                                    var opt = $("<option/>")
                                    opt.text(obj.FsiidRefId).val(obj.FsiidPkRefNo);
                                    ref.append(opt);
                                });
                                if (ref[0].selval && ref[0].seltype == "FormS2" && ref[0].selval != "") {
                                    ref.val(ref[0].selval);
                                }
                                ref.trigger("chosen:updated");
                            }
                        }, "Loading");
                    }
                    else { sel.val("").trigger("chosen:updated"); }
                    $("#secFromFormA").hide();
                    break;
                case "FormA":
                    var fromCHKM = $("#formAFromCh").val();
                    var fromCHM = $("#formAFromChDeci").val();
                    var toCHKM = $("#formAToCh").val();
                    var toCHM = $("#formAToChDeci").val();
                    if (actCode.val() != "" && rdCode != "" && fromCHKM != "" && fromCHM != "" && toCHKM != "" && toCHM != "") {
                        ref.addClass("validate");
                        ref.closest(".form-group").show();
                        var post = {};
                        post.activityCode = actCode.find(":selected").attr("code");
                        post.roadCode = $("#formS1DRoadCode option:selected").attr("code");
                        post.fromCHKM = fromCHKM;
                        post.fromCHM = fromCHM;
                        post.toCHKM = toCHKM;
                        post.toCHM = toCHM;
                        GetResponseValue("GetActiveRefIDs", "NOD", post, function (data) {
                            if (data) {
                                $.each(data, function (idx, obj) {
                                    var opt = $("<option/>")
                                    opt.text(obj.FadRefId).val(obj.FadPkRefNo);
                                    opt[0].Data = obj;
                                    ref.append(opt);
                                });
                                if (ref[0].selval && ref[0].seltype == "FormA" && ref[0].selval != "") {
                                    ref.val(ref[0].selval);
                                }
                                ref.trigger("chosen:updated");
                                $("#secFromFormA").show();
                            }
                        }, "Loading");
                    }
                    else { sel.val("").trigger("chosen:updated"); }
                    break;
            }
        }
        else {
            $("#secFromFormA").hide();
            ref.trigger("chosen:updated");
        }
    }
    this.FormTypeRefChange = function (tis) {
        //debugger;
        if ($("#drpFormType").val() == "FormA") {
            var sel = $(tis);
            var opt = sel.find("option:selected");
            if (opt.length != 1) {
                var data = opt;
            }
            else {
                var data = opt[0].Data;
            }
            console.log(opt);
            //console.log(opt[0].Data);
            //console.log(opt[0].data);
            //var data = opt[0].Data;
            //var data = opt;
            var sec = $("#secFromFormA");
            sec.find("#txtFASiteRef").val(data ? data.FadSiteRef : "");
            sec.find("#txtFAPriority").val(data ? data.FadPriority : "");
            sec.find("#txtFAWorkQTY").val(data ? data.FadAdp : "");
            sec.find("#txtFACDR").val(data ? data.FadCdr : "");
        }
    }
    this.CrewSupChange = function (tis) {

        // this.FormDWorkStatus();
        if (tis.value != "") {
            if ($("#txtCrewSupName[viewmode]").length == 0) {
                var sel = $(tis).find(":selected");
                var _name = sel.attr("item1");
                if (_name != "others") {
                    $("#txtCrewSupName").val(_name).removeClass("validate").prop("disabled", true);
                }
                else {
                    $("#txtCrewSupName").val("").addClass("validate").prop("disabled", false);
                }
            }
        }
        else {
            $("#txtCrewSupName").val("");
        }
    }
}
$(document).ready(function () {
    //$('.followup-group li button').on('click', function () {
    //    //$(this).toggleClass('active')
    //});
    $("#cancelAddModelBtn,#cancelAddModelHdrBtn").on("click", function () {
        formS1D.Cancel();
    });
    $("input:text[formtypeAdep]").keypress(function () {
        var data = formS1D.F1DetailInfo;
        var sel = $("#drpFormType");
        if (sel.val() == "FormA" && (data.PkRefNo == 0 || data.PkRefNo == undefined)) {
            sel.val("").trigger("change").trigger("chosen:updated");
            app.ShowInfoMessage("Based on the dependency form type reset");
        }
    });
    $("select[formtypedep]").change(function () {
        var data = formS1D.F1DetailInfo;
        var sel = $("#drpFormType");
        if ((sel.val() == "FormA" || sel.val() == "FormS2") && (data.PkRefNo == 0 || data.PkRefNo == undefined)) {
            sel.val("").trigger("change").trigger("chosen:updated");
            app.ShowInfoMessage("Based on the dependency form type reset");
        }
    });
    formS1D.SCButton = $("[scview]").removeAttr("scview").clone();
    formS1D.SEButton = $("[seview]").removeAttr("seview").clone();
});
