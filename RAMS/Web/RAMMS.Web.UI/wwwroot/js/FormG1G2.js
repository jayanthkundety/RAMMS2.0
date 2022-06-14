var frmG1G2 = new function () {
    this.HeaderData = {};
    this.ImageList = [];
    this.Dis_Severity = {};
    this.ArDistress = {};
    this.DistressOthers = {};
    this.Severity = {};
    this.IsEdit = true;
    this.Pattern = "";
    this.FindDetails = function () {
        if (ValidatePage("#divFindDetailsFG1G2")) {
            //debugger;
            var tis = this;
            $("#AiAssetId").val($("#selAssetID option:selected").text());
            GetResponseValue("FindDetails", "FormG1G2", FormValueCollection("#divFindDetailsFG1G2"), function (data) {
                if (data) {
                    //debugger;
                    $("[finddetailhide]").hide();
                    $("#selAssetID,#formG1G2InsYear").prop("disabled", true).trigger("change").trigger("chosen:updated");
                    if (data.SubmitSts) {
                        window.location = _APPLocation + "FormG1G2/View/" + data.PkRefNo;
                    }
                    tis.HeaderData = data;
                    tis.PageInit();
                }
            }, "Finding");
        }
    }

    this.AssetIDChange = function (tis) { this.BindRefNumber(); }
    this.InsYearChange = function (tis) { this.BindRefNumber(); }
    this.BindRefNumber = function () {
        var tis = this;
        var yr = $("#formG1G2InsYear").val();
        var assid = $("#selAssetID");
        if (yr != "" && assid.val() != "") {
            $("#txtFormG1G2RefNum").val(tis.Pattern.replace("{AssetID}", assid.find(":selected").text()).replace("{Year}", yr));
        }
        else {
            $("#txtFormG1G2RefNum").val("");
        }
    }

    this.PageInit = function () {
        if (frmG1G2.HeaderData && frmG1G2.HeaderData.PkRefNo && frmG1G2.HeaderData.PkRefNo > 0) {
            $("[finddetailsdep]").show();
            $("#btnFindDetails").hide();
        }
        else {
            $("[finddetailsdep]").hide();
            $("#btnFindDetails").show();
            $("#selRMU").trigger("change");
        }
       //this.BindData();
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
                req.GrpCode = "G"
                frmG1G2.DropDownBind(req);
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

            frmG1G2.FilterAssestID();
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
                frmG1G2.DropDownBind(req);
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
            frmG1G2.FilterAssestID();
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
                frmG1G2.DropDownBind(req);
                //var sec = $("#selSectionCode"); sec.find("option").hide().filter("[cvalue='" + $(tis).find("option:selected").attr("cvalue") + "']").show();
                //if (sec.find("option:selected:visible").length == 0) { sec.val("").trigger("change"); }
                //sec.trigger("chosen:updated");
                //var ctrl = $("#selRoadCode"); ctrl.find("option").hide().filter("[cvalue='" + tis.value + "']").show(); ctrl.val("").trigger("change").trigger("chosen:updated");

            }
            else { var ctrl = $("#selRoadCode,#selSectionCode"); ctrl.find("option:hidden").show(); ctrl.val("").trigger("change").trigger("chosen:updated"); }

            var div = $("#selDivision");
            if (div.val() == "") { div.val("MIRI").trigger("chosen:updated"); }
            frmG1G2.FilterAssestID();
        }
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
        frmG1G2.UserIdChange(this);
    });
    //frmG1G2.InitDis_Severity();
    frmG1G2.PageInit();
    $("#smartSearch").focus();//Header Grid focus    
    if ($("#btnFindDetails:visible").length > 0) {
        setTimeout(function () { $('#selDivision').trigger('chosen:activate'); }, 200);
    }
    else {
        setTimeout(function () { $("#formG1G2InspectedBy").trigger('chosen:activate'); }, 200);
    }

    //Listener for Smart and Detail Search
    $("#FG1G2SrchSection").find("#smartSearch").focus();
    element = document.querySelector("#formG1G2AdvSearch");
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