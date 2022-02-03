

function changeDdlReference() {
    //debugger;
    var value = $("#ReferenceNo").val();
    var refText = $("#ReferenceNo option:selected").text().split('/');
    var formHrefno = $("#formHReferenceNo").val();
    var test = formHrefno + "/" + refText[5] + "/" + refText[6];
    $("#formHReferenceNo").val(test);
    generateReferenceNo();
    if (value == "") {
        $("#btnClient").prop("disabled", true);
    }
    else {
        $("#btnClient").prop("disabled", false);
    }

    //$.ajax({
    //    url: '/NOD/HCheckbyReference',
    //    type: 'POST',
    //    data: { id: $("#ReferenceNo option:selected").text() },
    //    success: function (data) {
    //        console.log(data);
    //        if (data.data != 0) {
    //            $("#div-data-container").html(data);
    //        }
    //        else {
    //            clearDetail();
    //        }
    //    }
    //});

}

function closeClientRecordModel() {
    var isView = $("#IsViewDetail").val();
    if (isView != "1") {

        if (app.Confirm("Unsaved changes will be lost. Are you sure you want to cancel?", function (e) {
            if (e) {
                $("#ClientsRecordModal").modal("hide");
                FormHGridRefresh();
            }
        }));
    }
    else {
        $("#ClientsRecordModal").modal("hide");
        FormHGridRefresh();
    }
}
function closeRecordModel() {
    var isView = $("#IsViewDetail").val();
    if (isView != "1") {
        if (app.Confirm("Unsaved changes will be lost. Are you sure you want to cancel?", function (e) {
            if (e) {
                $("#FormHAddModal").modal("hide");
                FormHGridRefresh();
            }
        }));
    }
    else 
    {
        $("#FormHAddModal").modal("hide");
        FormHGridRefresh();
    }
}

function saveClientDetail() {
    saveDetail();
}
function bindReferene() {
    //debugger;
    var req = {};
    req.FormType = $("#ddlFormType option:selected").val();
    req.RoadCode = $("#FormADetRoadCode option:selected").val();
    req.AssetGroup = $("#FormADetAssetGrpCode").val();
    req.LocationFrom = $("#chainageFrom").val();
    req.LocationFromDec = $("#chainageFromDec").val();
    req.LocationTo = $("#chainageTo").val();
    req.LocationToDec = $("#chainageToDec").val();
    req.DateOfInspection = $("#Dateofinspection").val();


    if (req.FormType == "0") {
        $("#ReferenceNo").empty();
        $("#ReferenceNo").append($("<option></option>").val("").html("Select Reference"));
        $("#ReferenceNo").prop("disabled", true);
        $("#ReferenceNo").trigger("chosen:updated");
    }
    else if (req.AssetGroup != "" && req.RoadCode != "" && req.FormType != ""
        && req.LocationFrom != "" && req.LocationTo != "" &&
        req.DateOfInspection != "") {

        $.ajax({
            url: '/NOD/HGetReferenceList',
            dataType: 'JSON',
            data: req,
            type: 'Post',
            success: function (data) {
                $("#ReferenceNo").empty();
                $("#ReferenceNo").append($("<option></option>").val("").html("Select Reference"));
                $.each(data, function (index, v) {
                    $("#ReferenceNo").append($("<option></option>").val(v.value).html(v.text));
                });
                $("#ReferenceNo").trigger("chosen:updated");
            },
            error: function (data) {

                console.error(data);
            }
        });

    }

    generateReferenceNo();
}

function GetImageList(id) {

    var group = $("#FormADetAssetGrpCode option:selected").val();
    var FormType = $("#ddlFormType option:selected").val();
    if (id && id > 0) {
        $("#formAFadPKRefNO").val(id);
    }
    else {
        id = $("#formAFadPKRefNO").val();
    }
    if (FormType == "1") {
        id = $("#ReferenceNo option:selected").val();
        $.ajax({
            url: '/NOD/GetImageList',
            data: { assetPk: id, assetgroup: group },
            type: 'POST',
            success: function (data) {
                $("#ViewPhoto").html(data);
                $("#formAFadPKRefNO").val(id);
            },
            error: function (data) {
                alert(data.responseText);
            }

        });
    }
    else if (FormType == "2") {
        id = $("#ReferenceNo option:selected").val();
        $.ajax({
            url: '/NOD/JGetImageList',
            data: { assetPk: id, assetgroup: group },
            type: 'POST',
            success: function (data) {
                $("#ViewPhoto").html(data);
                $("#formAFadPKRefNO").val(id);
            },
            error: function (data) {
                alert(data.responseText);
            }

        });
    }
    else if (FormType == "3") {
        id = $("#formHFhhPkRefNo").val();
        $.ajax({
            url: '/NOD/HGetImageList',
            data: { assetPk: id, assetgroup: group },
            type: 'POST',
            success: function (data) {
                $("#ViewPhoto").html(data);
                $("#formAFadPKRefNO").val(id);
            },
            error: function (data) {
                alert(data.responseText);
            }

        });
    }
}

function openFormAModel(pid, isview) {
  
    if (isview != undefined && pid != 0) {
        $("#FormHAddModalid").text("View Form H Detail | Ref-" + pid);
    }
    else if (pid != 0) {
        $("#FormHAddModalid").text("Edit Form H Detail | Ref-" + pid);
    }
    else {
        $("#FormHAddModalid").text("Add Form H Detail");
    }
    $.ajax({
        url: '/NOD/HEdit',
        type: 'POST',
        data: { id: pid },
        success: function (data) {
            Validation.OnKeyPressInit();
            $("#FormHAddModal").modal({ backdrop: 'static', keyboard: false });
            $("#div-data-container").html(data);
            InitDecimalValidation();
            var e = $('#divDetails');
            var d = $("#addFADBtn");
            var s = $("#saveFormABtn");
            var su = $('#SubmitFormABtn');
            if (pid != 0 && e != undefined) {
                $('#searchHeaderBtn').hide();
                $(".editdisable").prop("disabled", true);
                e.show();
                d.show();
                s.show();
                su.show();
                //InitializeGrid(pid);
            }
            else if (e != undefined) {
                e.hide();
                d.hide();
                s.hide();
                su.hide();
                //InitializeGrid(0);
            }
            else {
                //InitializeGrid(0);
            }
        }
    });
}

function openClientRecord() {

    if ($("#receivedJKRS").is(":checked") || $("#sentReceivedJKRS").is(":checked")) {

        $("#ClientsRecordModal").modal("show");
    }
    else {
        alert("Please check Received JKRS to access Clients Record.")
    }
}

function ViewDetailHeader(pid) {
    //debugger;
    var filterData = new Object();
    filterData.HeaderNo = pid;
    $("#FormHAddModalid").text("View Form H Detail | Ref-" + pid);

    $.ajax({
        url: '/NOD/HEdit',
        type: 'POST',
        data: { id: pid },
        success: function (data) {
            $("#FormHAddModal").modal("show");
            $("#div-data-container").html(data);
            var d = $("#btnSave");
            var s = $("#saveclient");
            $("#IsViewDetail").val("1");
            $(".editdisable").prop("disabled", true);
            $(".disableinput").prop("disabled", true);
            $("#btnSubmit").hide();
            d.hide();
            s.hide();
        }
    });
    //$.ajax({
    //    url: '/NOD/HeaderListEdit',
    //    //url: '/NOD/DetailedListEdit',
    //    data: { filterData },
    //    type: 'POST',
    //    success: function (data) {
    //        //$("#FormAAddModal").modal("show");
    //        $("#div-data-container").html(data);
    //        $(".disableinput").prop("disabled", true);

    //        var e = $('#divDetails');
    //        if (pid != 0 && e != undefined) {
    //            $("#IsViewDetail").val("1");
    //            $("#addFADBtn").hide();
    //            $("#saveFormABtn").hide();
    //            $("#SubmitFormABtn").hide();
    //            $('#searchHeaderBtn').hide();
    //            e.show();
    //            //InitializeGrid(pid);
    //        }
    //        else if (e != undefined) {
    //            e.hide();
    //        }
    //        //if (view == 1) {
    //        //    $("#div-data-container *").attr("disabled", "disabled").off('click');
    //        //    $("#div-data-container").addClass("disableAssetImageAddBtn");
    //        //}
    //        //else {
    //        //    $("#div-data-container").removeClass("disableAssetImageAddBtn");
    //        //}
    //        $("body").removeClass("loading");
    //    }
    //});
}

function detailSearch() {
    $("#formAAdvSearch").slideToggle(500);
}

function generateReferenceNo() {
    //debugger;
    var req = {};
    req.formType = $("#ddlFormType option:selected").val();
    req.roadCode = $("#FormADetRoadCode option:selected").val();
    req.inspectiondate = $("#Dateofinspection").val();
    req.assetGroup = $("#FormADetAssetGrpCode option:selected").val();
    req.locationfrom = $("#chainageFrom").val();
    req.locationto = $("#chainageTo").val();
    req.sourcerefno = $("#ReferenceNo").val();
    req.sourcerefnoText = $("#ReferenceNo option:selected").text();


    if (req.formType != "" &&
        req.roadCode != "" &&
        req.inspectiondate != "" &&
        req.assetGroup != "" &&
        req.locationfrom != "" &&
        req.locationto != "") {
        $.ajax({
            url: '/NOD/HGetReferenceNOData',
            data: req,
            type: 'POST',
            success: function (data) {
                if (data != "" && data.isAlreadyexists) {
                    $("#btnSubmit").attr("disabled", true);
                    $("#btnSave").attr("disabled", true);
                    app.ShowErrorMessage("Record already exist for this Ref No. " + data.refNo)
                    $("#formHReferenceNo").val(data.refNo);
                    $("#formHFhhPkRefNo").val(data.id);
                }
                else if (data != "") {
                    $("#formHReferenceNo").val(data.refNo);
                    $("#btnSubmit").attr("disabled", false);
                    $("#btnSave").attr("disabled", false);
                    $("#formHFhhPkRefNo").val("");
                }
                else {
                    $("#formHFhhPkRefNo").val("");
                     }
           
                //if (data != "" && data.isAlreadyexists) {
                //    $("#formHReferenceNo").val(data.refNo);
                //    $("#formHFhhPkRefNo").val(data.id);
                //}
                //else if (data != "") {
                //    $("#formHReferenceNo").val(data.refNo);
                //    $("#formHFhhPkRefNo").val("");
                //}
                //else {
                //    $("#formHFhhPkRefNo").val("");
                //}
            }
        });
    }
    else {

    }
}

function clearDetail() {
    $("#fromDamagedetail").val('');
    $("#formDamagedBy").val('');
    $("#fromCLTCmnts").val('');
    $("#ddlUerId").val('').trigger('chosen:udpated');
    $("#InspectedName").val('');
    $("#InspectedDesignName").val('');
    $("#InspectedDate").val('');
    $("#ReceivedDate").val('');
    $("#sentJKRS").prop("checked", false);
    $("#receivedJKRS").prop("checked", false);
    $("#submittedJKRSDate").val('');
    $("#ReceivedJKRSDate").val('');
    $("#cientRemarks").val('');
    $("#authRemarks").val('');
    $("#authRecommendation").val('');
    $("#ddlReceivedby").val('').trigger('chosen:udpated');
    $("#ReceivedName").val('');
    $("#ReceivedDesignation").val('');
    $("#ReceivedDate").val('');
    $("#ddlVettedby").val('').trigger('chosen:udpated');
    $("#Vettedname").val('');
    $("#VettedDesignation").val('');
    $("#VettedDate").val('');
    $("#formHFhhPkRefNo").val('');
    $("#ddlreported").val('').trigger('chosen:updated');
    $("#reportedName").val('');
    $("#reportedDesignName").val('');
    $("#reportedDate").val('');
}
var _d = function () {
    return {
        authRemarks: $("#authRemarks"),
        cientRemarks: $("#cientRemarks"),
        Recommendation: $("#authRecommendation"),
        Receivedby: $("#ddlReceivedby"),
        ReceivedName: $("#ReceivedName"),
        ReceivedDesignation: $("#ReceivedDesignation"),
        ReceivedDate: $("#ReceivedDate"),
        Vettedby: $("#ddlVettedby"),
        Vettedname: $("#Vettedname"),
        VettedDesignation: $("#VettedDesignation"),
        VettedDate: $("#VettedDate")
    }
    
}
function saveDetail(isSubmit) {
    var d = _d();
    if (isSubmit && isSubmit != undefined && isSubmit != null) {
     
        $("#ddlreported").addClass("validate {visreq, Reported by User Id }");
        $("#reportedName").addClass("validate {visreq, Reported by Name }");
        $("#reportedDesignName").addClass("validate {visreq, Reported by Designation }");
        $("#reportedDate").addClass("validate {visreq, Reported by Date }");
        $("#ddlUerId").addClass("validate {visreq, Verified by User Id }");
        $("#InspectedName").addClass("validate {visreq, Verified by Name }");
        $("#InspectedDesignName").addClass("validate {visreq, Verified by Designation }");
        $("#InspectedDate").addClass("validate {visreq, Verified by Date }");
        d.authRemarks.addClass("validate {required, Report Number }");
        d.cientRemarks.addClass("validate {required, Remarks }");
        d.Recommendation.addClass("validate {required, Recommendation }");
        d.Receivedby.addClass("validate {visreq,Received by User Id  }");
        d.ReceivedName.addClass("validate {visreq,Received by User Name  }");
        d.ReceivedDesignation.addClass("validate {visreq,Received by User Designation  }");
        d.ReceivedDate.addClass("validate {visreq, Received by Date }");
        d.Vettedby.addClass("validate {visreq, Vetted by User Id }");
        d.Vettedname.addClass("validate {visreq, Vetted by User Name }");
        d.VettedDesignation.addClass("validate {visreq,Vetted by Designation  }");
        d.VettedDate.addClass("validate {visreq, Vetted by Date }");
    }
    else {
        $("#ddlreported").removeClass("border-error validate {required, Reported by User Id }");
        $("#ddlreported").next('div.chosen-container').removeClass('border-error');         
        $("#reportedName").removeClass("border-error validate {required, Reported by Name }");
        $("#reportedDesignName").removeClass("border-error validate {required, Reported by Designation }");
        $("#reportedDate").removeClass("border-error validate {required, Reported by Date }");
        $("#ddlUerId").removeClass("border-error validate {required, Verified by User Id }");
        $("#ddlUerId").next('div.chosen-container').removeClass('border-error');     
        $("#InspectedName").removeClass("border-error validate {required, Verified by Name }");
        $("#InspectedDesignName").removeClass("border-error validate {required, Verified by Designation }");
        $("#InspectedDate").removeClass("border-error validate {required, Verified by Date }");
        d.authRemarks.removeClass("border-error validate {required, Report Number }");
        d.cientRemarks.removeClass("border-error validate {required, Remarks }");
        d.Recommendation.removeClass("border-error validate {required, Recommendation }");
        d.Receivedby.removeClass("border-error validate {required,Received by User Id  }");
        d.Receivedby.next('div.chosen-container').removeClass('border-error');         
        d.ReceivedName.removeClass("border-error validate {required,Received by User Name  }");
        d.ReceivedDesignation.removeClass("border-error validate {required,Received by User Designation  }");
        d.ReceivedDate.removeClass("border-error validate {required, Received by Date }");
        d.Vettedby.removeClass("border-error validate {required, Vetted by User Id }");
        d.Vettedby.next('div.chosen-container').removeClass('border-error');         
        d.Vettedname.removeClass("border-error validate {required, Vetted by User Name }");
        d.VettedDesignation.removeClass("border-error validate {required,Vetted by Designation  }");
        d.VettedDate.removeClass("border-error validate {required, Vetted by Date }");
    }
    if (ValidatePage("#formDiv")) {
        var req = {};
        req.No = $("#formHFhhPkRefNo").val();
        req.SubmitSts = isSubmit;
        req.RoadCode = $("#FormADetRoadCode option:selected").val();
        req.RdName = $("#FormADetRoadName").val();
        req.Rmu = $("#FormADetRmu").val();
        req.Div = $("#FormADiv").val();
        req.Section = $("#FormADetsection").val();
        req.AssetGroupCode = $("#FormADetAssetGrpCode option:selected").val();
        req.ddlFormType = $("#ddlFormType option:selected").val();
        if (req.ddlFormType == "1") {
            req.FormAId = $("#ReferenceNo option:selected").val();
        }
        else if (req.ddlFormType == "2") {
            req.FormJId = $("#ReferenceNo option:selected").val();
        }
        req.ReferenceNo = $("#formHReferenceNo").val();
        req.InspDt = $("#Dateofinspection").val();
        req.ChinageFrom = $("#chainageFrom").val();
        req.ChinageFromDeci = $("#chainageFromDec").val();
        req.ChinageTo = $("#chainageTo").val();
        req.ChinageToDeci = $("#chainageToDec").val();
        req.DamDtl = $("#fromDamagedetail").val();
        req.DamCausedby = $("#formDamagedBy").val();
        req.CltRemarks = $("#fromCLTCmnts").val();
        req.UseridVer = $("#ddlUerId option:selected").val();
        req.UsernameVer = $("#InspectedName").val();
        req.DesignationVer = $("#InspectedDesignName").val();
        req.DtVer = $("#InspectedDate").val();
        req.DtRcvdAuth = $("#ReceivedDate").val();
        req.SubAuthSts = $("#sentJKRS").is(":checked");
        req.RcvdAuthSts = $("#receivedJKRS").is(":checked");
        req.SentJKRSDate = $("#submittedJKRSDate").val();
        req.RecievedFromJKRSDate = $("#ReceivedJKRSDate").val();
        req.Remarks = $("#cientRemarks").val();
        req.AuthRemarks = $("#authRemarks").val();
        req.AuthRecmd = $("#authRecommendation").val();
        req.UseridRcvdAuth = $("#ddlReceivedby option:selected").val();
        req.UsernameRcvdAuth = $("#ReceivedName").val();
        req.DesignationRcvdAuth = $("#ReceivedDesignation").val();
        req.DtRcvdAuth = $("#ReceivedDate").val();
        req.Rmu = $("#FormADetRmu").val();
        req.UseridVetAuth = $("#ddlVettedby option:selected").val();
        req.UsernameVetAuth = $("#Vettedname").val();
        req.DesignationVetAuth = $("#VettedDesignation").val();
        req.DtVet = $("#VettedDate").val();
        req.UseridPrp = $("#ddlreported option:selected").val();
        req.UsernamePrp = $("#reportedName").val();
        req.DesignationPrp = $("#reportedDesignName").val();
        req.DtPrp = $("#reportedDate").val();

        if (TryParseInt($("#chainageFrom").val()) > TryParseInt($("#chainageTo").val())) {
            app.ShowWarningMessage("Chainge From should not be greater than Chainage To");
        }
        else {

            //HSave
            $.ajax({
                url: '/NOD/HSave',
                data: req,
                type: 'POST',
                success: function (data) {
                    //$("#headerListGrid").html(data); 
                    if (data != "" && data != undefined && data != null) {
                        $("#formHFhhPkRefNo").val(data.no);
                        if (data.rcvdAuthSts) {
                            $("#btnClient").prop("disabled", false);
                        }
                    }
                    ClientsRecordModalOpen = $('#ClientsRecordModal').is(':visible');
                    if (ClientsRecordModalOpen) {
                        app.ShowSuccessMessage("Saved successfully");
                        $('#ClientsRecordModal').modal('hide');
                    }
                    else {

                        if (isSubmit) {
                            $("#formHSearchBtn").click();
                            app.ShowSuccessMessage("Submitted successfully");
                            $("#FormHAddModal").modal("hide");
                        }
                        else {
                            app.ShowSuccessMessage("Saved successfully");
                        }

                    }

                }
            });
        }
    }

}

function searchHeaderList() {
    var filterData = new Object();
    filterData.SmartInputValue = $("#FormASmartSearch").val();
    filterData.Rmu = $("#formADetSrchRMU").val();
    filterData.section = $("#formADetSrchSec").val();
    filterData.RoadCode = $("#formADetSrchRoadCode").val();
    filterData.AssetGroupCode = $("#formADetSrchAsstGrp").val();
    filterData.Month = $("#formADetSrchMonth").val();
    $("body").addClass("loading");
    $.ajax({
        url: '/NOD/HSearchHeaderList',
        data: filterData,
        type: 'POST',
        success: function (data) {

            $("#headerListGrid").html(data);
            $("body").removeClass("loading");
        }
    });
}

function DeleteHeaderRecord(id) {
    var headerId = id;
    app.Confirm("Are you sure you want to delete the record?, If Yes click OK.", function (e) {
        if (e) {
            //$("body").addClass("loading");
            InitAjaxLoading();
            $.ajax({
                url: '/NOD/FormHHeaderListDelete',
                data: { headerId },
                type: 'POST',
                success: function (data) {
                    if (data > 0) {
                        //$("body").removeClass("loading");
                        FormHGridRefresh();
                        HideAjaxLoading();

                        app.ShowSuccessMessage("Successfully Deleted");
                        //searchHeaderList();


                    }
                    else {
                        app.ShowErrorMessage("Error in Deleted. Kindly retry later.");
                        $("body").removeClass("loading");
                    }
                }
            });
        }
    });
}

function EditHeaderRecord(id) {
    var headerId = id;
    localStorage.setItem("headeridvalue", headerId);
}
