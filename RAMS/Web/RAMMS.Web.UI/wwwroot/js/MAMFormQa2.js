var _ = {
    roadcode: $("#formRoadCode"),
    roadName: $("#formRoadName"),
    rmu: $("#formRmu"),
    referenceNo: $("#formReferenceNo"),
    CrewSup: $("#formQA2CrewUnit"),
    CrewSupName: $("#formQA2CrewName"),
    no: $("#RefNo"),
    UseridQaIni: $("#formQA2QaInByUserId"),
    UseridQaI: $("#formQA2UseridQaI"),
    UseridQaIi: $("#formQA2UseridQaII"),
    UseridQaIii: $("#formQA2UseridQaIII"),
    UseridQaIiv: $("#formQA2UseridQaIV"),
    UsernameQaIni: $("#FormQA2QaInByName"),
    UsernameQaI: $("#FormQA2QaIByName"),
    UsernameQaIi: $("#FormQA2QaIIByName"),
    UsernameQaIii: $("#FormQA2QaIIIByName"),
    UsernameQaIiv: $("#FormQA2QaIVByName"),
    DesignationQaIni: $("#FormQA2QaInByDesign"),
    DesignationQaI: $("#FormQA2QaIDesign"),
    DesignationQaIi: $("#FormQA2QaIIDesign"),
    DesignationQaIii: $("#FormQA2QaIIIDesign"),
    DesignationQaIv: $("#FormQA2QaIVDesign"),
    RemarksQAIn: $("#dtlRemarksQAIn"),
    RemarksQAI: $("#dtlRemarksQAI"),
    RemarksQAIi: $("#dtlRemarksQAIi"),
    RemarksQAIii: $("#dtlRemarksQAIii"),
    RemarksQAIv: $("#dtlRemarksQAIV"),
    BUTTONFindDetail: $("#btnFindDetails"),
    HDNHeaderNo: $("#hdnHeaderPkNo"),
    ADDForm: $("#AddDetail"),
    BUTTONAddForm: $("#addFormDetail"),
    BUTTONCancel: $("#btnHCancel"),
    BUTTNNSave: $("#btnHSave"),
    BUTTONSubmit: $("#btnHSubmit"),
    isView: $("#IsViewDetail"),
    QA2InSign: $("#FormQA2InSign"),
    QA2ISign: $("#FormQA2ISign"),
    QA2IiSign: $("#FormQA2IiSign"),
    QA2IiiSign: $("#FormQA2IiiSign"),
    QA2IvSign: $("#FormQA2IvSign"),
    HdrMonth: $("#FormQA2HdrMonth"),
    HdrYear: $("#FormQA2HdrYear"),
}

var _d = function () {
    return {
        reference: $("#dtlRef"),
        remakrs: $("#dtlRemarks"),
        WWS: $("#dtlWWS"),
        defectDesc: $("#dtlDefectdescription"),
        locaToDec: $("#dtlLocationchtoDec"),
        locaTo: $("#dtlLocationchto"),
        locaFromDec: $("#dtlLocationchfromDec"),
        locaFrom: $("#dtlLocationchfrom"),
        defect: $("#dtlDefect"),
        defDesc: $("#dtlDefectDesc"),
        siteref: $("#dtlSiteRef"),
        ActCode: $("#dtlActCode"),
        ActName: $("#dtlActName"),
        S1Refno: $("#dtlS1RefNo"),
        activitycode: $("#dtlActivitycode"),
        roadcode: $("#dtlRoadcode"),
        roadname: $("#dtlRoadname"),
        initRating: $("#qaIniRating"),
        qa1Rating: $("#qa1Rating"),
        qa2Rating: $("#qa2Rating"),
        qa3Rating: $("#qa3Rating"),
        qa4Rating: $("#qa4Rating"),
        initDate: $("#qaInitDate"),
        qa1Date: $("#qa1Date"),
        qa2Date: $("#qa2Date"),
        qa3Date: $("#qa3Date"),
        qa4Date: $("#qa4Date"),
        No: $("#hdnQa2DetailPkId"),
        sourceType: $("#dtlSourceType"),
        S1RefNoDiv: $("#dtlS1RefNoDiv"),
        IssueNCN: $("#dtlIssueNCN"),
        DimLength: $("#DimLength"),
        DimWidth: $("#DimWidth"),
        DimHeight: $("#DimHeight"),
        RefNo: $("#QA2DtlRefNo")
    };
}

$(document).ready(function () {
    _.rmu.chosen();
    _.roadcode.chosen();
    _.BUTTONAddForm.hide();
    _.BUTTNNSave.hide();
    _.BUTTONSubmit.hide();

    _.UsernameQaI.prop("disabled", true);
    _.UsernameQaIi.prop("disabled", true);
    _.UsernameQaIii.prop("disabled", true);
    _.UsernameQaIiv.prop("disabled", true);
    _.UsernameQaIni.prop("disabled", true);
    _.DesignationQaI.prop("disabled", true);
    _.DesignationQaIi.prop("disabled", true);
    _.DesignationQaIii.prop("disabled", true);
    _.DesignationQaIni.prop("disabled", true);
    _.DesignationQaIv.prop("disabled", true);
    _.CrewSupName.prop("disabled", true);

    var d = _d();
    if (_.HDNHeaderNo.val() != "0" && _.HDNHeaderNo.val() != "") {
        _.BUTTONAddForm.show();
        InitializeGrid(_.HDNHeaderNo.val());
        _.BUTTONFindDetail.hide();
        $(".disableHinput").prop("disabled", true);
        _.roadcode.trigger("chosen:updated");
        _.rmu.trigger("chosen:updated");
        _.HdrMonth.trigger("chosen:updated");
        _.HdrYear.trigger("chosen:updated");
        _.CrewSup.trigger("chosen:updated");
        if (_.isView.val() == 0) {
            _.BUTTNNSave.show();
            _.BUTTONSubmit.show();
        }
    }

    d.S1Refno.on("change", function () {
        bindS1Detil(this.value);
    });
    d.ActCode.on("change", function () {
        bindActDetail()
    })
    _.rmu.on("change", function () {
        bindRoadName();
        generateReferenceNo();
    });
    _.CrewSup.on("change", function () {
        generateReferenceNo();
    });
    _.roadcode.on("change", function () {
        generateReferenceNo();
        $.ajax({
            url: '/MAM/GetDivisionByRoadCode',
            dataType: 'JSON',
            data: { roadCode: $(this).val() },
            type: 'Post',
            success: function (data) {
                if (data != null) {
                    if (data._RMAllData != undefined && data._RMAllData != null) {
                        _.roadName.val(data._RMAllData.roadName);
                    }
                }
            },
            error: function (data) {
                console.error(data);
            }
        });
    });
    _.HdrMonth.on("change", function () {
        generateReferenceNo()
    })
    _.HdrYear.on("change", function () {
        generateReferenceNo()
    })
    _.UseridQaIni.on("change", function () {
        var id = $("#formQA2QaInByUserId option:selected").val();
        var obj = new Object();
        obj.designation = _.DesignationQaIni;
        obj.userName = _.UsernameQaIni;
        obj.sign = _.QA2InSign;
        userDetail(id, obj)
        return false;
    })
    _.UseridQaI.on("change", function () {
        var id = $("#formQA2UseridQaI option:selected").val();
        var obj = new Object();
        obj.designation = _.DesignationQaI;
        obj.userName = _.UsernameQaI;
        obj.sign = _.QA2ISign;
        userDetail(id, obj)
        return false;
    })
    _.UseridQaIi.on("change", function () {
        var id = $("#formQA2UseridQaII option:selected").val();
        var obj = new Object();
        obj.designation = _.DesignationQaIi;
        obj.userName = _.UsernameQaIi;
        obj.sign = _.QA2IiSign;
        userDetail(id, obj)
        return false;
    })
    _.UseridQaIii.on("change", function () {
        var id = $("#formQA2UseridQaIII option:selected").val();
        var obj = new Object();
        obj.designation = _.DesignationQaIii;
        obj.userName = _.UsernameQaIii;
        obj.sign = _.QA2IiiSign;
        userDetail(id, obj)
        return false;
    })
    _.UseridQaIiv.on("change", function () {
        var id = $("#formQA2UseridQaIV option:selected").val();
        var obj = new Object();
        obj.designation = _.DesignationQaIv;
        obj.userName = _.UsernameQaIiv;
        obj.sign = _.QA2IvSign;
        userDetail(id, obj)
        return false;
    })
    _.BUTTONFindDetail.on("click", function () {
        var rmu = _.rmu.val();
        var rdCode = _.roadcode.val();
        var refNo = _.referenceNo.val();
        if (rmu != "" && rdCode != "" && refNo != "") {
            _.BUTTONFindDetail.hide();
            FindDetails();
        }
        else {
            app.ShowErrorMessage("Please fill all fields")
        }
    });

    _.BUTTONAddForm.on("click", function () {
        AddDetail(0, '');
    });

    _.BUTTONCancel.on("click", function () {
        disableAttrExist = $("#AddFormQA2").attr("disabled")
        if (!disableAttrExist) {
            if (app.Confirm("Unsaved changes might be lost. Are you sure you want to cancel?", function (e) {
                if (e) {
                    window.location.href = "/MAM/QA2";
                }
            }));
        }
        else {
            window.location.href = "/MAM/QA2";
        }
    });

    _.BUTTNNSave.on("click", function () {
        SaveHeader(false);
    });

    _.BUTTONSubmit.on("click", function () {
        _.UseridQaIni.addClass("validate {required, Initial Condition}")
        SaveHeader(true);
    });
});

function userDetail(id, obj) {
    if (id != "99999999" && id != "") {
        $.ajax({
            url: '/NOD/GetUserById',
            dataType: 'JSON',
            data: { id },
            type: 'Post',
            success: function (data) {
                obj.designation.val(data.position);
                obj.userName.val(data.userName);
                obj.designation.prop("disabled", true);
                obj.userName.prop("disabled", true);
                if (data.siignIn != null && data.signIn != "") {
                    obj.sign.attr("src", data.signIn);
                }
                else {
                    obj.sign.attr("src", "");
                }
            },
            error: function (data) {

                console.error(data);
            }
        });
    }
    else if (id == "99999999") {
        obj.userName.prop("disabled", false);
        obj.designation.prop("disabled", false);
        obj.userName.val('');
        obj.designation.val('');
        obj.sign.attr("src", "");
    }
    else {
        obj.userName.prop("disabled", true);
        obj.designation.prop("disabled", true);
        obj.userName.val('');
        obj.designation.val('');
        obj.sign.attr("src", "");
    }
    return false;
}

function bindS1Detil() {
    var req = {};
    var d = _d();
    req.id = d.S1Refno.val();
    $.ajax({
        url: '/MAM/GetS1RefDetails',
        dataType: 'JSON',
        data: req,
        type: 'Post',
        success: function (data) {
            //debugger;
            var d = _d();
            //d.siteref.val(data.siteRef);
            d.siteref.val(data.siteRef).trigger("chosen:updated");
            d.ActCode.val(data.actCode);
            d.ActCode.prop("disabled", true).trigger("chosen:updated");

            d.defect.val(data.defect);
            d.defect.prop("disabled", true).trigger("chosen:updated");
          
            d.locaFrom.val(data.chainageFrom);
            d.locaFromDec.val(data.chainageFromDec);
            d.locaTo.val(data.chainageTo);
            d.locaToDec.val(data.chainageToDec);
            d.defect.val(data.defect).trigger("chosen:updated");
            d.locaFrom.prop("disabled", true);
            d.locaFromDec.prop("disabled", true);
            d.locaTo.prop("disabled", true);
            d.locaToDec.prop("disabled", true);
            d.siteref.prop("disabled", true).trigger("chosen:updated");
            bindActDetail();
        }
    });
}

function bindActDetail() {
    var d = _d();
    var actName = d.ActCode.find("option:selected").text().split("-")[1]
    d.ActName.val(actName);

    var defdesc = d.defect.find("option:selected").text().split("-")[1]
    d.defDesc.val(defdesc);
}

function save(isContinue) {
    if (ValidatePage("#AddDetailFormQA2")) {
        var d = _d();
        var req = {};
        req.No = d.No.val();
        req.FormQA2HeaderRefNo = _.HDNHeaderNo.val();
        req.S1DtlRefNo = d.S1Refno.val();
        req.SiteRef = "";
        for (var i = 0; i < d.siteref.val().length; i++) {
            req.SiteRef += (i > 0 ? ',' : '') + d.siteref.val()[i];
        }
        req.SourceType = d.sourceType.find("option:selected").text();
        req.WrkAct = d.ActCode.val();
        req.DefCode = d.defect.val();
        req.FrmCh = d.locaFrom.val();
        req.FrmChDeci = d.locaFromDec.val();
        req.ToCh = d.locaTo.val();
        req.ToChDeci = d.locaToDec.val();
        req.InitialCond = d.initRating.val();
        req.QaI = d.qa1Rating.val();
        req.QaIi = d.qa2Rating.val();
        req.QaIii = d.qa3Rating.val();
        req.QaIv = d.qa4Rating.val();
        req.DtQaI = d.qa1Date.val();
        req.DtQaIi = d.qa2Date.val();
        req.DtQaIii = d.qa3Date.val();
        req.DtQaIv = d.qa4Date.val();
        req.DtInitialCond = d.initDate.val();
        req.DefDesc = d.defectDesc.val();
        req.Wws13aFol = d.WWS.val();
        req.DimHeight = d.DimHeight.val();
        req.DimWidth = d.DimWidth.val();
        req.DimLength = d.DimLength.val();
        req.IssueNCN = d.IssueNCN.val();
        req.Remarks = d.remakrs.val();
        req.RefId = d.RefNo.val();
        InitAjaxLoading();
        $.ajax({
            url: '/MAM/SaveQa2Detail',
            dataType: 'JSON',
            data: req,
            type: 'Post',
            success: function (data) {
                HideAjaxLoading();
                if (data > 0) {
                    app.ShowSuccessMessage('Successfully Saved', false);
                    if (isContinue) {
                        AddDetail(0, '');
                    }
                    else {
                        $("#FormQA2AdddetailsModal").modal("hide");
                        DetailGridRefresh();
                    }
                }
                else {
                    app.ShowWarningMessage('Not Saved', false);
                }
            }
        });
    }

}
function close() {
    var d = _d();
    d.No.val("");
    $("#FormQA2AdddetailsModal").modal("hide");
}

function SaveHeader(issubmit) {
    if (ValidatePage("#AddFormQA2")) {
        var req = {};
        req.No = _.HDNHeaderNo.val();
        req.RefId = _.referenceNo.val();
        req.RoadCode = _.roadcode.val();
        req.RoadName = _.roadName.val();
        req.Rmu = _.rmu.val();
        req.Month = _.HdrMonth.val();
        req.Year = _.HdrYear.val();
        req.SubmitSts = issubmit;
        req.CrewSup = _.CrewSup.val();
        req.CrewSupName = _.CrewSupName.val();
        req.UseridQaIni = _.UseridQaIni.val();
        req.UseridQaI = _.UseridQaI.val();
        req.UseridQaIi = _.UseridQaIi.val();
        req.UseridQaIii = _.UseridQaIii.val();
        req.UseridQaIv = _.UseridQaIiv.val();
        req.UsernameQaI = _.UsernameQaI.val();
        req.UsernameQaIi = _.UsernameQaIi.val();
        req.UsernameQaIii = _.UsernameQaIii.val();
        req.UsernameQaIv = _.UsernameQaIiv.val();
        req.UsernameQaIni = _.UsernameQaIni.val();
        req.DesignationQaI = _.DesignationQaI.val();
        req.DesignationQaIi = _.DesignationQaIi.val();
        req.DesignationQaIii = _.DesignationQaIii.val();
        req.DesignationQaIv = _.DesignationQaIv.val();
        req.DesignationQaIni = _.DesignationQaIni.val();
        req.RemarksQaIni = _.RemarksQAIn.val();
        req.RemarksQaI = _.RemarksQAI.val();
        req.RemarksQaIi = _.RemarksQAIi.val();
        req.RemarksQaIii = _.RemarksQAIii.val();
        req.RemarksQaIv = _.RemarksQAIv.val();
        InitAjaxLoading();
        $.ajax({
            url: '/MAM/UpdateHeader',
            dataType: 'JSON',
            data: req,
            type: 'Post',
            success: function (data) {
                HideAjaxLoading();
                if (data.no > 0) {
                    if (issubmit) {
                        app.ShowSuccessMessage("Successfull Submitted")
                    }
                    else {
                        app.ShowSuccessMessage("Successfull Saved")
                    }
                    setTimeout(function () {
                        window.location.href = "/MAM/QA2";
                    }, 3000);
                }
            }
        });
    }
}

function FindDetails() {
    var req = {};
    req.No = _.no.val();
    req.RefId = _.referenceNo.val();
    req.RoadCode = _.roadcode.val();
    req.RoadName = _.roadName.val();
    req.Rmu = _.rmu.val();
    req.Month = _.HdrMonth.val();
    req.Year = _.HdrYear.val();
    req.CrewSup = _.CrewSup.val();
    req.CrewSupName = _.CrewSupName.val();
    InitAjaxLoading();
    $.ajax({
        url: '/MAM/SaveHeader',
        dataType: 'JSON',
        data: req,
        type: 'Post',
        success: function (data) {
            //debugger;
            console.log(data);
            HideAjaxLoading();
            if (data.no > 0) {
                _.roadcode.prop("disabled", true);
                _.rmu.prop("disabled", true);
                _.referenceNo.prop("disabled", true);
                _.roadcode.trigger("chosen:updated");
                _.rmu.trigger("chosen:updated");
                _.HdrMonth.prop("disabled", true);
                _.HdrMonth.trigger("chosen:updated");
                _.HdrYear.prop("disabled", true);
                _.HdrYear.trigger("chosen:updated");
                _.CrewSup.prop("disabled", true)
                _.CrewSup.trigger("chosen:updated");
                _.CrewSupName.prop("disabled", true);
                _.HDNHeaderNo.val(data.no);              

                _.UseridQaIni.val(data.useridQaIni).trigger("change").trigger("chosen:updated");
                _.UseridQaI.val(data.useridQaI).trigger("change").trigger("chosen:updated");
                _.UseridQaIii.val(data.useridQaIii).trigger("change").trigger("chosen:updated");
                _.UseridQaIiv.val(data.useridQaIv).trigger("change").trigger("chosen:updated");
                _.UseridQaIi.val(data.useridQaIi).trigger("change").trigger("chosen:updated");

                _.RemarksQAIn.val(data.remarksQaIni);
                _.RemarksQAI.val(data.remarksQaI);
                _.RemarksQAIi.val(data.remarksQaIi);
                _.RemarksQAIii.val(data.remarksQaIii);
                _.RemarksQAIv.val(data.remarksQaIv);              
    
               
                if (!data.submitSts) {
                    _.BUTTNNSave.show();
                    _.BUTTONSubmit.show();
                }
                else{
                    $("#div-addformd *").attr("disabled", "disabled").off("click");
                    $(".disableinput").prop("disabled", true).trigger("chosen:updated");
                }
                InitializeGrid(data.no);
                _.BUTTONAddForm.show();
            }
            else {
                _.BUTTONFindDetail.show();
                _.BUTTONAddForm.hide();
            }
        }
    });


}

function DeleteDetail(id) {
    var req = {};
    req.id = id;
    InitAjaxLoading();
    $.ajax({
        url: '/MAM/DeleteQa2Detail',
        data: req,
        type: 'Post',
        success: function (data) {
            HideAjaxLoading();
            if (data > 0) {
                app.ShowSuccessMessage("Successfully Deleted");
                DetailGridRefresh();
            }
        }
    });
}

function ViewDetail(id, title) {
    const _d_ = _d;
    var req = {};
    req.headerid = _.HDNHeaderNo.val();
    req.id = id;
    InitAjaxLoading();
    $.ajax({
        url: '/MAM/GetQa2Details',
        data: req,
        type: 'Post',
        success: function (data) {
            //debugger;
            HideAjaxLoading();
            _.ADDForm.html(data);
            var d = _d();
            $(".disableinput").prop("disabled", true);
            $(".disablebtn").hide();
            d.roadcode.val(_.roadcode.val());
            d.roadname.val(_.roadName.val());
            var data = $("#multiSiteHidden").val().split(",");
            d.siteref.val(data);
            d.siteref.chosen();
            d.siteref.trigger("chosen:updated");
            d.WWS.chosen();
            d.WWS.trigger("chosen:udpated");
            d.S1Refno.chosen();
            d.S1Refno.trigger("chosen:updated");
            d.sourceType.chosen();
            d.sourceType.trigger("chosen:updated");
            d.initRating.chosen();
            d.initRating.trigger("chosen:updated");
            d.qa1Rating.chosen();
            d.qa1Rating.trigger("chosen:updated");
            d.qa2Rating.chosen();
            d.qa2Rating.trigger("chosen:updated");
            d.qa3Rating.chosen();
            d.qa3Rating.trigger("chosen:updated");
            d.qa4Rating.chosen();
            d.qa4Rating.trigger("chosen:updated");
            d.ActCode.trigger("change");
            d.defect.chosen();
            d.defect.trigger("chosen:updated");
            d.defect.trigger("change");
            $("#FormQA2AdddetailsModal").modal("show");

        }
    });
}

function AddDetail(id, title) {
    const _d_ = _d;
    var req = {};
    req.headerid = _.HDNHeaderNo.val();
    req.id = id;
    req.hdrRefNo = $("#formReferenceNo").val();
    req.roadCode = _.roadcode.val()
    InitAjaxLoading();
    $.ajax({
        url: '/MAM/GetQa2Details',
        data: req,
        type: 'Post',
        success: function (data) {
            //debugger;
            HideAjaxLoading();
            _.ADDForm.html(data);
            Validation.OnKeyPressInit();
            var d = _d();
            d.roadcode.val(_.roadcode.val());
            d.roadname.val(_.roadName.val());
            var data = $("#multiSiteHidden").val().split(",");
            d.siteref.val(data);
            d.siteref.chosen();
            d.siteref.trigger("chosen:updated");
            d.WWS.chosen();
            d.WWS.trigger("chosen:udpated");
            d.S1Refno.chosen();
            d.S1Refno.trigger("chosen:updated");
            d.sourceType.chosen();
            d.sourceType.trigger("chosen:updated");
            if (id > 0) {
                $(".editdisable").prop("disabled", true).trigger("chosen:updated");
                $(".editdisable").prop("disabled", true);
                d.defect.prop("disabled", true);
                //d.ActCode.trigger("chosen:updated");

            }
            d.initRating.chosen();
            d.initRating.trigger("chosen:updated");
            d.qa1Rating.chosen();
            d.qa1Rating.trigger("chosen:updated");
            d.qa2Rating.chosen();
            d.qa2Rating.trigger("chosen:updated");
            d.qa3Rating.chosen();
            d.qa3Rating.trigger("chosen:updated");
            d.qa4Rating.chosen();
            d.qa4Rating.trigger("chosen:updated");
            d.ActCode.chosen();
            d.ActCode.prop("disabled", true);
            d.ActCode.trigger("chosen:updated");
            d.S1RefNoDiv.hide();
            d.ActCode.trigger("change");
            bindST();
            d.defect.chosen();
            d.defect.trigger("chosen:updated");
            d.defDesc.prop("disabled", true);
            bindDefCode()
            $("#FormQA2AdddetailsModal").modal("show");
            //$(".disableinput").prop("disabled", false);
        }
    });
}

function generateReferenceNo() {
    var req = {};
    req.roadcode = _.roadcode.val();
    req.rmu = _.rmu.val();
    req.month = _.HdrMonth.val();
    req.year = _.HdrYear.val();

    if (req.roadcode != "" && req.rmu != "" && req.month != "" && req.year != "" && _.CrewSup.val() != "") {
        InitAjaxLoading();
        $.ajax({
            url: '/MAM/GetQa2ReferenceNo',
            dataType: 'JSON',
            data: req,
            type: 'Post',
            success: function (data) {
                HideAjaxLoading();
                if (data.isExists) {
                    _.HDNHeaderNo.val(data.headerNo);
                }
                else {
                    _.HDNHeaderNo.val('');
                }
                _.referenceNo.val(data.reference);
            },
            error: function (e) {
                HideAjaxLoading();
                console.log(e);
            }
        });
    }
    else {
        _.referenceNo.val("");
    }
}

function bindRoadName() {
    var obj = new Object();
    var val = _.rmu.find(":selected").text().split('-');
    obj.RMU = val[1];
    $.ajax({
        url: '/Assets/detailSearchDdList',
        data: obj,
        type: 'Post',
        success: function (data) {
            $("#formRoadCode option").remove();
            $("#formRoadCode").append($('<option>').val(null).text("Select Road Code"));
            $.each(data.rdCode, function (index, value) {
                $("#formRoadCode").append($('<option>').val(value.value).html(value.text));
                $("#formRoadCode").trigger("chosen:updated");

            })
            _.referenceNo.val("");
            _.roadName.val(null);

        }
    })
}

function bindReference() {
    var roadcode = $("#formRoadCode option:selected").val();
    var rmu = $("#")
}

$("#formQA2CrewUnit").on("change", function () {
    var crewName = $("#formQA2CrewUnit option:selected").text().split('-')[1];
    var id = $("#formQA2CrewUnit option:selected").val();
    if (id != "" && id != "99999999") {
        $("#formQA2CrewName").prop("disabled", true);
        $("#formQA2CrewName").val(crewName);
    }
    else if (id == "99999999") {
        $("#formQA2CrewName").prop("disabled", false);
        $("#formQA2CrewName").val(null);
    }
    else {
        $("#formQA2CrewName").prop("disabled", true);
        $("#formQA2CrewName").val('');
    }

    return false;
});

function bindST() {
    //debugger;
    var d = _d();
    val = d.sourceType.val();
    if (val == "New") {
        d.S1RefNoDiv.hide();
        d.S1Refno.val(null);
       
        $("#dtlLocationchfromDec").val('');
        $("#dtlLocationchfrom").val('');
        $("#dtlLocationchto").val('');
        $("#dtlLocationchtoDec").val('');
        $("#dtlDefectdescription").val('');
        $("#dtlActName").val('');
        $("#dtlActCode").val('').trigger("chosen:updated");
        $("#dtlDefect").val('').trigger("chosen:updated");
        $("#dtlDefectDesc").val('');
        $("#dtlSiteRef").val('').trigger("chosen:updated");
        d.ActCode.prop("disabled", false);
        d.ActCode.trigger('chosen:updated');
        d.defect.prop("disabled", false).trigger("chosen:updated");
        d.locaFrom.prop("disabled", false);
        d.locaFromDec.prop("disabled", false);
        d.locaTo.prop("disabled", false);
        d.locaToDec.prop("disabled", false);
        d.siteref.prop("disabled", false).trigger("chosen:updated");
    }
    else if (val == "Form S1") {
        d.S1RefNoDiv.show();
        d.ActCode.prop("disabled", true);
        d.ActCode.trigger('chosen:updated');
        d.defect.prop("disabled", true).trigger("chosen:updated");
        d.siteref.prop("disabled", true).trigger("chosen:updated");
        $("#dtlS1RefNo").val('').trigger("chosen:updated");

        $("#dtlLocationchfromDec").val('');
        $("#dtlLocationchfrom").val('');
        $("#dtlLocationchto").val('');
        $("#dtlLocationchtoDec").val('');
        $("#dtlDefectdescription").val('');
        $("#dtlActName").val('');
        $("#dtlActCode").val('').trigger("chosen:updated");
        $("#dtlDefect").val('').trigger("chosen:updated");
        $("#dtlDefectDesc").val('');
        $("#dtlSiteRef").val('').trigger("chosen:updated");
        d.locaFrom.prop("disabled", false);
        d.locaFromDec.prop("disabled", false);
        d.locaTo.prop("disabled", false);
        d.locaToDec.prop("disabled", false);

    }
}

function bindDefCode() {
    var d = _d();
    val = d.defect.find("option:selected").text().split('-')[1];
    d.defDesc.val(val);
}


function FormQA2AdddetailsModalClose() {
    disableAttrExist = $(".disableinput").attr("disabled")
    if (!disableAttrExist) {
        if (app.Confirm("Unsaved changes might be lost. Are you sure you want to cancel?", function (e) {
            if (e) {
                $("#FormQA2AdddetailsModal").modal("hide");
            }
        }));
    }
    else {
        $("#FormQA2AdddetailsModal").modal("hide");
    }
    DetailGridRefresh();
}
function DetailGridRefresh() {
    oTable = $('#FormQa2GridDetail').DataTable();
    var filterData = new Object();
    filterData.HeaderNo = + $("#HDNHeaderNo").val();
    oTable.data = filterData;
    oTable.draw();
}