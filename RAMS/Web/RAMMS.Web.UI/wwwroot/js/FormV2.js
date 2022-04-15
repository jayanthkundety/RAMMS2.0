var currentDate = new Date();
$(document).ready(function () {
    currentDate = formatDate(currentDate);
    var val = $("#FV2PkRefNo").val();

    $("#formV2CrewCode").on("change", function () {
        var id = $("#formV2CrewCode option:selected").val();
        var ctrl = $(this);
        $("#hdnCrew").val(id);
        if (id != "99999999" && id != "") {
            $("#formV2CrewName").val(ctrl.find("option:selected").attr("Item1"));
        }
        else if (id == "99999999") {
            $("#formV2CrewName").prop("disabled", false);
            $("#formV2CrewName").val('');
        }
        else {
            $("#formV2CrewName").prop("disabled", true);
            $("#formV2CrewName").val('');
        }
        return false;
    });

    $("#formV2SectionCode").on("change", function () {
        var ddldata = $(this).val();
        //$("#hdnSecCode").val(ddldata);
        if (ddldata != "") {
            $.ajax({
                url: '/MAM/GetAllRoadCodeDataBySectionCode',
                dataType: 'JSON',
                data: { secCode: $("#formV2SectionCode option:selected").text().split("-")[0] },
                type: 'Post',
                success: function (data) {
                    if (data != null) {
                        $("#hdnSecCode").val(data._RMAllData.secName);
                        if (data._RMAllData != undefined && data._RMAllData != null) {

                            $("#formV2SectionName").val(data._RMAllData.secName);
                            $("#formV2DivCode").val(data._RMAllData.divisionCode);
                            $("#formV2DivCodeName").val(data._RMAllData.DivisionName);
                        }
                        document.getElementById("formV2DivCode").disabled = true;
                    } else {
                        document.getElementById("formV2DivCode").disabled = false;
                    }
                },
                error: function (data) {

                    console.error(data);
                }
            });
        }
        else {
            $("#formV2SectionName").val("");
            $("#formV2DivCode").val("");
        }
        return false;
    });

    $("#formV2rmu").on("change", function () {
        var val = $(this).find(":selected").text();
        val = val.split("-").length > 0 ? val.split("-")[1] : val;
        $("#formV2rmuDesc").val(val);
        var req = {};
        req.Section = '';
        req.RoadCode = '';
        req.RMU = $("#formV2rmu option:selected").text().split("-")[1];
        //RM_div_RMU_Sec_Master
        $.ajax({
            url: '/MAM/RMUSecRoad',
            dataType: 'JSON',
            data: req,
            type: 'Post',
            success: function (data) {
                if (data != null) {
                    $("#formV2SectionCode").empty();
                    $("#formV2SectionCode").append($("<option></option>").val("").html("Select Section Code"));
                    $.each(data.section, function (index, v) {
                        $("#formV2SectionCode").append($("<option></option>").val(v.value).html(v.text));
                    });
                    $.each(data.section, function (index, v) {
                        if (v != null) {
                            var splittedVal = v.value;
                            var editVal = $('#hdnSecCode').val();
                            if (splittedVal == editVal) {
                                $("#formV2SectionCode").val(v.value);
                            }
                        }
                    });
                    $("#formV2SectionCode").trigger("change");
                    $('#formV2SectionCode').trigger("chosen:updated");
                    document.getElementById("formV2DivCode").disabled = true;
                } else {
                    document.getElementById("formV2DivCode").disabled = false;
                }
            },
            error: function (data) {

                console.error(data);
            }
        });
    });

    if (val != 0 && val != undefined && val != "") {
        gridAddBtnDis()
        $("#formV2rmu").trigger("change");
        $('#formV2rmu').trigger("chosen:updated");
        $("#formV2rmu").attr("disabled", "disabled").off('click');
        $("#formV2rmu").chosen('destroy');

        $("#formV2SectionCode").trigger("change");
        $("#formV2SectionCode").trigger("chosen:updated");
        $("#formV2SectionCode").attr("disabled", "disabled").off('click');
        $("#formV2SectionCode").chosen('destroy');

        $("#formV2CrewCode").trigger("change");
        $("#formV2CrewCode").trigger("chosen:updated");
        $("#formV2CrewCode").attr("disabled", "disabled").off('click');
        $("#formV2CrewCode").chosen('destroy');


        $("#formV2ActivityCode").trigger("change");
        $("#formV2ActivityCode").trigger("chosen:updated");
        $("#formV2ActivityCode").attr("disabled", "disabled").off('click');
        $("#formV2ActivityCode").chosen('destroy');

        $("#formV2Date").attr("disabled", "disabled");
        $("#formV2FindDetailsBtn").hide();

        $("#saveFormV2Btn").show();
        $("#SubmitFormV2Btn").show();
    }
    else {
        $("#saveFormV2Btn").hide();
        $("#SubmitFormV2Btn").hide();
        document.getElementById("btnEquipAdd").disabled = true;
        document.getElementById("btnLabourAdd").disabled = true;
        document.getElementById("btnMaterialAdd").disabled = true;
    }

    $("#formV2ActivityCode").on("change", function () {
        var val = $(this).find(":selected").text();
        val = val.split("-").length > 0 ? val.split("-")[1] : val;
        $("#formV2ActivityName").val(val);
    });

    $("#hdnisAdd").val($("#FV2PkRefNo").val());

    if ($("#hdnView").val() == "1") {
        $("#saveFormV2Btn").hide();
        $("#SubmitFormV2Btn").hide();
        $("#div-addformd *:not(.enblmode)").attr("disabled", "disabled").off('click');
        $("#formV2rmu").chosen('destroy');
        $("#btnFrmV2Back").attr("disabled", false);
        $("#formV2CrewCode").chosen('destroy');
        $("#formV2MaterialEdit").attr("disabled", "disabled").off('click');
        $("#formV2EquipmentEdit").attr("disabled", "disabled").off('click');
        $("#formV2DtlEdit").attr("disabled", "disabled").off('click');
        userIdDisable();
    }
    //else {
    //    $("#saveFormV2Btn").hide();
    //    $("#SubmitFormV2Btn").hide();
    //}


    $("#formV2RecordedBy").on("change", function () {
        var id = $("#formV2RecordedBy option:selected").val();
        var ctrl = $(this);
        if (id != "99999999" && id != "") {
            $("#formV2RecordedName").val(ctrl.find("option:selected").attr("Item1"));
            $("#formV2RecordedDesig").val(ctrl.find("option:selected").attr("Item2"));
            $("#formV2SignRecorded").prop("checked", true);
        }
        else if (id == "99999999") {
            $("#formV2RecordedName").prop("disabled", false);
            $("#formV2RecordedName").val('');
            $("#formV2RecordedDesig").prop("disabled", false);
            $("#formV2RecordedDesig").val('');
            $("#formV2SignRecorded").prop("checked", true);

        }
        else {
            $("#formV2RecordedName").prop("disabled", true);
            $("#formV2RecordedName").val('');
            $("#formV2RecordedDesig").prop("disabled", true);
            $("#formV2RecordedDesig").val('');
        }
        $("#formV2RecordedDate").val(currentDate);
        return false;
    });

    $("#formV2VettedBy").on("change", function () {
        var id = $("#formV2VettedBy option:selected").val();
        var ctrl = $(this);
        if (id != "99999999" && id != "") {
            $("#formV2VettedName").val(ctrl.find("option:selected").attr("Item1"));
            $("#formV2VettedDesig").val(ctrl.find("option:selected").attr("Item2"));
            $("#formV2SignVetted").prop("checked", true);
        }
        else if (id == "99999999") {
            $("#formV2VettedName").prop("disabled", false);
            $("#formV2VettedName").val('');
            $("#formV2VettedDesig").prop("disabled", false);
            $("#formV2VettedDesig").val('');
            $("#formV2SignVetted").prop("checked", true);

        }
        else {
            $("#formV2VettedName").prop("disabled", true);
            $("#formV2VettedName").val('');
            $("#formV2VettedDesig").prop("disabled", true);
            $("#formV2VettedDesig").val('');
        }
        $("#formV2VettedDate").val(currentDate);
        return false;
    });

    $("#formV2FacilitatedBy").on("change", function () {
        var id = $("#formV2FacilitatedBy option:selected").val();
        var ctrl = $(this);
        if (id != "99999999" && id != "") {
            $("#formV2FacilitatedName").val(ctrl.find("option:selected").attr("Item1"));
            $("#formV2FacilitatedDesig").val(ctrl.find("option:selected").attr("Item2"));
            $("#formV2SignFacilitated").prop("checked", true);
        }
        else if (id == "99999999") {
            $("#formV2FacilitatedName").prop("disabled", false);
            $("#formV2FacilitatedName").val('');
            $("#formV2FacilitatedDesig").prop("disabled", false);
            $("#formV2FacilitatedDesig").val('');
            $("#formV2SignFacilitated").prop("checked", true);

        }
        else {
            $("#formV2FacilitatedName").prop("disabled", true);
            $("#formV2FacilitatedName").val('');
            $("#formV2FacilitatedDesig").prop("disabled", true);
            $("#formV2FacilitatedDesig").val('');
        }
        $("#formV2FacilitatedDate").val(currentDate);
        return false;
    });
});

$(document).on("click", "#formV2FindDetailsBtn", function () {

    if (ValidatePage("#AccordPage0", "", "")) {
        InitAjaxLoading();
        GetResponseValue("FindDetails", "MAM", FormValueCollection("#FormV2Headers"), function (data) {
            HideAjaxLoading();
            if (data != undefined && data != null) {
                if (data.status == 'V1NotExisit') {
                    app.ShowErrorMessage("Form V1 details not found")
                    return;
                }

                if (data.status == 'V2Exisit') {
                    if (app.Confirm("Form V2 for this record has exist, Do you want to proceed with another Form V2", function (e) {
                        if (e) {
                            $("#hdnV1PkRefNo").val(data.v1id);
                            $.ajax({
                                url: '/MAM/CreateFormV2',
                                data: FormValueCollection("#FormV2Headers"),
                                type: 'POST',
                                success: function (data) {
                                    createV2(data);
                                }
                            })

                        }
                    }));
                    return;
                } else
                    createV2(data);
            }
        }, "Finding");
    }
    //saveHdr(false);
});

function createV2(data) {
    $("#formV2rmu").prop("disabled", true).trigger("chosen:updated");
    $("#formV2SectionCode").prop("disabled", true).trigger("chosen:updated");
    $("#formV2CrewCode").prop("disabled", true).trigger("chosen:updated");
    $("#formV2ActivityCode").prop("disabled", true).trigger("chosen:updated");
    $('#formV2Date').prop("disabled", true);
    $("#formV2ReferenceNo").val(data.RefId)
    $("#formV2FindDetailsBtn").hide();
    if (!data.SubmitSts) {
        $("#saveFormV2Btn").show();
        $("#SubmitFormV2Btn").show();
        gridAddBtnDis();
    }
    else {
        userIdDisable()
        UserDtDisable()
    }
    $("#FV2PkRefNo").val(data.PkRefNo);

    DtlGridLoad(data.PkRefNo);


    $("#formV2RecordedBy").val(data.UseridSch).trigger("chosen:updated");
    $("#formV2RecordedBy").trigger("change");
    $("#formV2VettedBy").val(data.UseridAgr).trigger("chosen:updated");
    $("#formV2VettedBy").trigger("change");
    $("#formV2FacilitatedBy").val(data.UseridAck).trigger("chosen:updated");
    $("#formV2FacilitatedBy").trigger("change");

    //$("#formV2RecordedName").val(data.UsernameSch);
    //$("#formV2RecordedDesig").val(data.DesignationSch);
    var Format = "YYYY-MM-DD";
    if (data.DtSch != null) {
        var date = new Date(data.DtSch);
        $("#formW2RecordedDate").val(date.ToString(Format));
    }

    //$("#formV2VettedName").val(data.UsernameAgr);
    //$("#formV2VettedDesig").val(data.DesignationAgr);
    if (data.DtAgr != null) {
        date = new Date(data.DtAgr);    
        $("#formW2VettedDate").val(date.ToString(Format));
    }
    //$("#formV2FacilitatedName").val(data.UsernameAck);
    //$("#formV2FacilitatedDesig").val(data.DesignationAck);
    if (data.DtAck != null) {
        date = new Date(data.DtAck);
        $("#formV2FacilitatedDate").val(date.ToString(Format));
    }
}

function GoBack() {
    if ($("#hdnView").val() == "0") {
        if (app.Confirm("Unsaved changes will be lost. Are you sure you want to cancel?", function (e) {
            if (e) {
                location.href = "/MAM/FormV2";

            }
        }));
    }
    else
        location.href = "/MAM/FormV2";
}

function getParameterByName(name, url = window.location.href) {
    name = name.replace(/[\[\]]/g, '\\$&');
    var regex = new RegExp('[?&]' + name + '(=([^&#]*)|&|#|$)'),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, ' '));
}

function DeleteFormV2LabourRecord(id) {
    var headerId = id;
    if ($("#hdnView").val() == "1") {
        app.ShowErrorMessage("Delete function not available in View Mode");
        return false;
    }
    if (app.Confirm("Are you sure you want to delete the record?", function (e) {
        if (e) {
            InitAjaxLoading();
            $.ajax({
                url: '/MAM/FormV2LabourDelete',
                data: { headerId },
                type: 'POST',
                success: function (data) {
                    if (data > 0) {
                        app.ShowSuccessMessage('Successfully Deleted', false);
                        FormLabGridRefresh();
                    }
                    else {
                        app.ShowErrorMessage("Error in Deleted. Kindly retry later.");
                    }
                    HideAjaxLoading();
                }
            });
        }
    }));
}

function DeleteFormV2MaterialRecord(id) {
    var headerId = id;
    if ($("#hdnView").val() == "1") {
        app.ShowErrorMessage("Delete function not available in View Mode");
        return false;
    }
    if (app.Confirm("Are you sure you want to delete the record?", function (e) {
        if (e) {

            InitAjaxLoading();
            $.ajax({
                url: '/MAM/FormV2MaterialDelete',
                data: { headerId },
                type: 'POST',
                success: function (data) {
                    if (data > 0) {
                        app.ShowSuccessMessage('Successfully Deleted', false);
                        FormMatGridRefresh();
                    }
                    else {
                        app.ShowErrorMessage("Error in Deleted. Kindly retry later.");
                    }
                    HideAjaxLoading();
                }
            });
        }
    }));
}

function DeleteFormV2EquipmentRecord(id) {
    var headerId = id;

    if ($("#hdnView").val() == "1") {
        app.ShowErrorMessage("Delete function not available in View Mode");
        return false;
    }
    if (app.Confirm("Are you sure you want to delete the record?", function (e) {
        if (e) {

            InitAjaxLoading();
            $.ajax({
                url: '/MAM/FormV2EquipmentDelete',
                data: { headerId },
                type: 'POST',
                success: function (data) {
                    if (data > 0) {
                        app.ShowSuccessMessage('Successfully Deleted', false);
                        FormEquipGridRefresh();
                    }
                    else {
                        app.ShowErrorMessage("Error in Deleted. Kindly retry later.");
                    }
                    HideAjaxLoading();
                }
            });
        }
    }));
}

function userIdDisable() {
    $('#formV2RecordedBy').prop('disabled', true).trigger("chosen:updated");
    $('#formV2VettedBy').prop('disabled', true).trigger("chosen:updated");
    $('#formV2FacilitatedBy').prop('disabled', true).trigger("chosen:updated");
}

function gridAddBtnDis() {
    $("#btnEquipAdd,#btnLabourAdd,#btnMaterialAdd").prop("disabled", false);
}

function UserDtDisable() {
    $('#formW2RecordedDate').prop('disabled', true);
    $('#formW2VettedDate').prop('disabled', true);
    $('#formV2FacilitatedDate').prop('disabled', true);
}

function EditFormV2Labour(id, view) {

    InitAjaxLoading();
    $.ajax({
        url: '/MAM/EditFormLabour',
        data: { id: id },
        type: 'POST',
        success: function (data) {
            $("#div-lab-container").html(data);
            if (view == 1 || $("#hdnView").val() == "1") {
                $("#hdnLabView").val(1);
                $("#FormV2LabourModalid").html("View Labour")
                $("#div-lab-container *").attr("disabled", "disabled").off('click');
                $("#saveFormV2LabBtn").css("display", "none");
                $("#cancelAddModelBtn").attr("disabled", false);
                $("#saveContinueFormV2LabBtn").css("display", "none");
            } else if ($("#hdnLabid").val() != "") {
                $("#FormV2LabourModalid").html("Edit Labour")
                //$("#hdnLabView").val(0);
            }
            HideAjaxLoading();
            //$("body").removeClass("loading");
        }
    })
}

function EditFormV2Equip(id, view) {
    InitAjaxLoading();
    $.ajax({
        url: '/MAM/EditFormV2EquipmentDetails',
        data: { id: id },
        type: 'POST',
        success: function (data) {
            $("#div-equip-container").html(data);
            if (view == 1 || $("#hdnView").val() == "1") {
                $("#hdnEquView").val(1);
                $("#FormV2EquipModalid").html("View Equipment");
                $("#div-equip-container *").attr("disabled", "disabled").off('click');
                $("#saveFormV2EqBtn").css("display", "none");
                $("#cancelAddModelBtn").attr("disabled", false);
                $("#saveContinueFormV2EqBtn").css("display", "none");
            }
            else if ($("#hdnEquipid").val() != "")
                $("#FormV2EquipModalid").html("Edit Equipment");
            HideAjaxLoading();
        }
    })
}

function EditFormV2Material(id, view) {
    InitAjaxLoading();
    $.ajax({
        url: '/MAM/EditFormMaterial',
        data: { id: id },
        type: 'POST',
        success: function (data) {
            $("#div-mat-container").html(data);
            if (view == 1 || $("#hdnView").val() == "1") {
                $("#hdnMatView").val(1);
                $("#FormV2MaterialModalid").html("View Material")
                $("#div-mat-container *").attr("disabled", "disabled").off('click');
                $("#saveFormV2MtBtn").css("display", "none");
                $("#cancelAddModelBtn").attr("disabled", false);
                $("#saveContinueFormV2MtBtn").css("display", "none");
            } else if ($("#hdnMaterialNo").val() != "")
                $("#FormV2MaterialModalid").html("Edit Material")
            HideAjaxLoading();
        }
    })
}

function FormLabGridRefresh() {
    var filterData = new Object();
    oTable = $('#FormV2LabourGridView').DataTable();
    oTable.data = filterData;
    oTable.draw();
}

function FormMatGridRefresh() {
    var filterData = new Object();
    oTable = $('#FormV2MaterialGridView').DataTable();
    oTable.data = filterData;
    oTable.draw();
}

//function FormV2DtlGridRefresh() {
//    var filterData = new Object();
//    oTable = $('#FormV2DetailsGridView').DataTable();
//    oTable.data = filterData;
//    oTable.draw();
//}

function FormEquipGridRefresh() {
    var filterData = new Object();
    oTable = $('#FormV2EquipGridView').DataTable();
    oTable.data = filterData;
    oTable.draw();
}

function DtlGridLoad(data) {

    var table = $('#FormV2EquipGridView').DataTable();
    table.ajax.url("/MAM/LoadFormV2EquipmentList?id=" + data).load();

    var ltable = $('#FormV2LabourGridView').DataTable();
    ltable.ajax.url("/MAM/LoadFormV2LabourList?id=" + data).load();

    var mtable = $('#FormV2MaterialGridView').DataTable();
    mtable.ajax.url("/MAM/LoadFormV2MaterialList?id=" + data).load();

    var dtable = $('#FormV2DetailsGridView').DataTable();
    dtable.ajax.url("/MAM/LoadFormV2DetailsList?id=" + data).load();
}

$(document).on("click", "#saveFormV2Btn", function () {
    saveHdr(false);
});

$(document).on("click", "#SubmitFormV2Btn", function () {
    saveHdr(true);
});

$(document).on("click", "#saveContinueFormV2LabBtn", function () {
    saveLabour(false, true);
});

$(document).on("click", "#saveFormV2LabBtn", function () {
    saveLabour(false, false);
});

$(document).on("click", "#saveFormV2EqBtn", function () {
    saveEquipment(false, false);
});

$(document).on("click", "#saveContinueFormV2EqBtn", function () {
    saveEquipment(false, true);
});

$(document).on("click", "#saveFormV2MtBtn", function () {
    saveMaterial(false, false);
});

$(document).on("click", "#saveContinueFormV2MtBtn", function () {
    saveMaterial(false, true);
});

$(document).on("click", "#saveFormV2UserBtn", function () {
    saveUserDetails(false);
});

function saveHdr(isSubmit) {

    if ((!isSubmit && ValidatePage('#AccordPage0')) || (isSubmit && ValidatePage('#AccordPage0,#div-addformV2'))) {
        InitAjaxLoading();

        var d = new Date();

        var month = d.getMonth() + 1;
        var day = d.getDate();

        var output = (('' + month).length < 2 ? '0' : '') + month + '/' +
            (('' + day).length < 2 ? '0' : '') + day + '/' + d.getFullYear();

        var saveObj = new Object;

        saveObj.PkRefNo = $("#FV2PkRefNo").val();
        saveObj.RefId = $("#formV2ReferenceNo").val();
        saveObj.Fv1hPkRefNo = $("#hdnV1PkRefNo").val();
        saveObj.ContNo = "";
        saveObj.Rmu = $("#formV2rmu").val();
        saveObj.SecCode = $("#formV2SectionCode").val();
        saveObj.SecName = $("#formV2SectionName").val();
        saveObj.Crew = $("#formV2CrewCode").val();
        saveObj.Crewname = $("#formV2CrewName").val();
        saveObj.DivCode = $("#formV2DivCode").val();
        saveObj.DivisionName = $("#formV2DivCodeName").val();
        saveObj.ActCode = $("#formV2ActivityCode").val();
        saveObj.ActName = $("#formV2ActivityName").val();
        saveObj.Dt = $("#formV2Date").val();
        saveObj.Remarks = $("#formV2Remarks").val();        //Recorded by
        if ($("#formV2RecordedBy").find(":selected").val() != "") saveObj.UseridSch = $("#formV2RecordedBy").find(":selected").val();

        if ($("#formV2RecordedName").val() != "") saveObj.UsernameSch = $("#formV2RecordedName").val();

        if ($("#formV2RecordedDesig").val() != "") saveObj.DesignationSch = $("#formV2RecordedDesig").val();

        if ($("#formV2RecordedDate").val() != "mm/dd/yyyy") saveObj.DtSch = $("#formV2RecordedDate").val();

        saveObj.SignSch = $("formV2SignRecorded").prop("checked");

        //Vetted By
        if ($("#formV2VettedBy").find(":selected").val() != "") saveObj.UseridAgr = $("#formV2VettedBy").find(":selected").val();

        if ($("#formV2VettedName").val() != "") saveObj.UsernameAgr = $("#formV2VettedName").val();

        if ($("#formV2VettedDesig").val() != "") saveObj.DesignationAgr = $("#formV2VettedDesig").val();

        if ($("#formV2VettedDate").val() != "mm/dd/yyyy") saveObj.DtAgr = $("#formV2VettedDate").val();

        saveObj.SignAgr = $("#formV2SignVetted").prop("checked");

        //Facilitator

        if ($("#formV2FacilitatedBy").find(":selected").text() != "") saveObj.UseridAck = $("#formV2FacilitatedBy").find(":selected").val();

        if ($("#formV2FacilitatedName").val() != "") saveObj.UsernameAck = $("#formV2FacilitatedName").val();

        if ($("#formV2FacilitatedDesig").val() != "") saveObj.DesignationAck = $("#formV2FacilitatedDesig").val();

        if ($("#formV2FacilitatedDate").val() != "mm/dd/yyyy") saveObj.DtAck = $("#formV2FacilitatedDate").val();

        saveObj.SignAck = $("formV2SignFacilitated").prop("checked");

        //Created by

        //if (saveObj.PkRefNo == 0) {
        //    if ($("#formV2RecordedBy").find(":selected").val() != "") saveObj.CrBy = $("#formV2RecordedBy").find(":selected").val();
        //    if ($("#formV2RecordedDate").val() != "mm/dd/yyyy") saveObj.CrDt = $("#formV2RecordedDate").val();
        //}

        //if ($("#formV2ReportedByUserId").find(":selected").val() != "") saveObj.ModBy = $("#formV2ReportedByUserId").find(":selected").val();
        //if ($("#FormV2ReportedByDate").val() != "mm/dd/yyyy") saveObj.ModDt = $("#FormV2ReportedByDate").val();


        saveObj.ActiveYn = true;
        saveObj.SubmitSts = isSubmit;
        console.log(saveObj);
        $.ajax({
            url: '/MAM/FormV2SaveHdr',
            data: saveObj,
            type: 'POST',
            success: function (data) {
                HideAjaxLoading();
                if (data == -1) {
                    app.ShowErrorMessage("Reference id already Exist");
                }
                else {
                    if ($("#FV2PkRefNo").val() == "" || $("#FV2PkRefNo").val() == "0") {
                        DtlGridLoad(data)
                    }
                    $("#FV2PkRefNo").val(data);
                    $("#formV2FindDetailsBtn").hide();
                    DataBind();
                    if (isSubmit) {
                        $("#saveFormV2Btn").hide();
                        $("#SubmitFormV2Btn").hide();
                        app.ShowSuccessMessage('Successfully Submitted', false);
                        location.href = "/MAM/FormV2";
                    }
                    else {
                        $("#saveFormV2Btn").show();
                        $("#SubmitFormV2Btn").show();
                        app.ShowSuccessMessage('Successfully Saved', false);
                        location.href = "/MAM/FormV2";
                    }
                }
            },
            error: function (data) {
                HideAjaxLoading();
                app.ShowErrorMessage(data.responseText);
            }

        });
    }
}

function saveEquipment(isSubmit, cont) {
    if (ValidatePage('#FormAddEquipDetails')) {
        InitAjaxLoading();
        var d = new Date();
        var month = d.getMonth() + 1;
        var day = d.getDate();
        var output = (('' + month).length < 2 ? '0' : '') + month + '/' +
            (('' + day).length < 2 ? '0' : '') + day + '/' + d.getFullYear();

        var saveObj = new Object;
        saveObj.SubmitSts = isSubmit;
        saveObj.PkRefNo = $("#hdnEquipid").val();
        saveObj.Fv2hPkRefNo = $("#FV2PkRefNo").val();
        saveObj.EqpRefCode = $("#formV2EquipCode").find(":selected").val();
        saveObj.Desc = $("#FormV2EquipDescription").val();
        saveObj.Capacity = $("#formV2EquipCapacity").val();
        saveObj.Cond = $("#formV2EquipCond").val();
        saveObj.Model = $("#formV2EquipModel").val();

        saveObj.CrDt = output
        saveObj.ModDt = output

        saveObj.ActiveYn = true;
        $.ajax({
            url: '/MAM/FormV2SaveEquipment',
            data: saveObj,
            type: 'POST',
            success: function (data) {
                app.ShowSuccessMessage('Successfully Saved', false);
                $("#val-summary-displayer").css("display", "none");
                $("#val-summary-displayer-equip").css("display", "none");
                if (!cont)
                    $("#FormV2EquipModal").modal("hide");
                else {
                    $("#hdnEquipid").val('');
                    $("#formV2EquipCode").val('').trigger('chosen:updated');
                    $("#formV2EquipCapacity").val("");
                    $("#formV2EquipCond").val("");
                    $("#formV2EquipModel").val("");
                    $("#FormV2EquipDescription").val("");
                }
                HideAjaxLoading();
                FormEquipGridRefresh();
            },
            error: function (data) {
                app.ShowErrorMessage(data.responseText);
                HideAjaxLoading();
            }

        });
    }
    else {
        $("#val-summary-displayer-equip").css("display", "block");
    }
}

function saveLabour(isSubmit, cont) {
    if (ValidatePage('#FormV2LabourModal')) {
        InitAjaxLoading();
        var d = new Date();

        var month = d.getMonth() + 1;
        var day = d.getDate();

        var output = (('' + month).length < 2 ? '0' : '') + month + '/' +
            (('' + day).length < 2 ? '0' : '') + day + '/' + d.getFullYear();

        var saveObj = new Object;
        saveObj.SubmitSts = isSubmit;
        saveObj.Fv2hPkRefNo = $("#FV2PkRefNo").val();
        saveObj.PkRefNo = $("#hdnLabid").val();
        saveObj.LabRefCode = $("#formV2LabCode").find(":selected").val();
        saveObj.Desc = $("#formV2LabDescription").val();
        saveObj.Remark = $("#formV2LabRemark").val();
        saveObj.ModDt = output
        saveObj.CrDt = output
        saveObj.ActiveYn = true;

        $.ajax({
            url: '/MAM/FormV2SaveLabour',
            data: saveObj,
            type: 'POST',
            success: function (data) {
                app.ShowSuccessMessage('Successfully Saved', false);
                $("#val-summary-displayer").css("display", "none");
                $("#val-summary-displayer-labour").css("display", "none");
                FormLabGridRefresh();
                if (!cont)
                    $("#FormV2LabourModal").modal("hide");
                else {
                    $("#hdnLabid").val('');
                    $("#formV2LabCode").val('').trigger('chosen:updated');
                    $("#formV2LabDescription").val("");
                    $("#formV2LabRemark").val("");
                }
                HideAjaxLoading();
            },
            error: function (data) {
                app.ShowErrorMessage(data.responseText, false);
                HideAjaxLoading();
            }

        });
    }
    else {
        $("#val-summary-displayer-labour").css("display", "block");
    }
}

function saveMaterial(isSubmit, cont) {
    if (ValidatePage('#FormV2MaterialModal')) {
        InitAjaxLoading();
        var d = new Date();

        var month = d.getMonth() + 1;
        var day = d.getDate();

        var output = (('' + month).length < 2 ? '0' : '') + month + '/' +
            (('' + day).length < 2 ? '0' : '') + day + '/' + d.getFullYear();

        var saveObj = new Object;
        saveObj.SubmitSts = isSubmit;
        saveObj.Fv2hPkRefNo = $("#FV2PkRefNo").val();
        saveObj.PkRefNo = $("#hdnMaterialNo").val();
        saveObj.MatRefCode = $("#formV2MatCode").find(":selected").val();
        //saveObj.Desc = $("#formV2MatDesc").val();
        saveObj.Qnty = $("#formV2MatQuantity").val();
        saveObj.Unit = $("#formV2MatUnit").find(":selected").val();
        saveObj.Remark = $("#FormV2MatDescription").val();
        saveObj.ModDt = output
        saveObj.CrDt = output
        saveObj.ActiveYn = true;
        $.ajax({
            url: '/MAM/FormV2SaveMaterial',
            data: saveObj,
            type: 'POST',
            success: function (data) {
                app.ShowSuccessMessage('Successfully Saved', false);
                $("#val-summary-displayer").css("display", "none");
                $("#val-summary-displayer-material").css("display", "none");
                if (!cont)
                    $("#FormV2MaterialModal").modal("hide");
                else {
                    $("#hdnMaterialNo").val('');
                    $("#formV2MatCode").val('').trigger('chosen:updated');
                    $("#formV2MatQuantity").val("");
                    $("#formV2MatUnit").val('').trigger('chosen:updated');
                    $("#FormV2MatDescription").val("");
                }
                FormMatGridRefresh();
                HideAjaxLoading();

            },
            error: function (data) {
                app.ShowErrorMessage(data.responseText, false);
                HideAjaxLoading();
            }

        });
    } else {
        $("#val-summary-displayer-material").css("display", "block");
    }
}

function DataBind() {

    $("#formDFindDetailsBtn").hide();

    $("#val-summary-displayer").css("display", "none");
    $("#formV2rmu").chosen('destroy');
    $("#formV2SectionCode").chosen('destroy');
    $("#formV2CrewCode").chosen('destroy');
    $("#formV2ActivityCode").chosen('destroy');

    $("#formV2rmu").attr("disabled", "disabled").off('click');
    $("#formV2SectionCode").attr("disabled", "disabled").off('click');
    $("#formV2CrewCode").attr("disabled", "disabled").off('click');
    $("#formV2ActivityCode").attr("disabled", "disabled").off('click');

    $("#btnEquipAdd,#btnLabourAdd,#btnMaterialAdd").prop("disabled", false);
}

function formatDate(date) {
    var d = new Date(date),
        month = '' + (d.getMonth() + 1),
        day = '' + d.getDate(),
        year = d.getFullYear();

    if (month.length < 2) month = '0' + month;
    if (day.length < 2) day = '0' + day;

    return [year, month, day].join('-');
}