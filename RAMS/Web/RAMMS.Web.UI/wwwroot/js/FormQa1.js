var currentDate = new Date();
$(document).ready(function () {
    currentDate = formatDate(currentDate);
    var val = $("#formQa1PkRefNo").val();

    if (val != 0 && val != undefined && val != "") {
        gridAddBtnDis()
        $("#btnFindDetails").hide();
    }
    else {
        $("#saveFormQa1Btn").hide();
        $("#SubmitFormQa1Btn").hide();
        document.getElementById("btnEquipAdd").disabled = true;
        document.getElementById("btnGenAdd").disabled = true;
        document.getElementById("btnMaterialAdd").disabled = true; 
        disableAll();
     
    }

    if ($("#hdnView").val() == "1") {
        $("#saveFormQa1Btn").hide();
        $("#SubmitFormQa1Btn").hide();

        $("#div-addformd *:not(.enblmode)").attr("disabled", "disabled").off('click');
        $("#formQa1rmu").chosen('destroy');
        $("#btnFrmV2Back").attr("disabled", false);
        $("#formQa1CrewCode").chosen('destroy');
        $("#formQa1MaterialEdit").attr("disabled", "disabled").off('click');
        $("#formQa1EquipmentEdit").attr("disabled", "disabled").off('click');

        userIdDisable();
        disableAll();
        document.getElementById("btnEquipAdd").disabled = true;
        document.getElementById("btnGenAdd").disabled = true;
        document.getElementById("btnMaterialAdd").disabled = true;
    }

    $("#formQa1rmu").on("change", function () {
        var val = $(this).find(":selected").text();
        val = val.split("-").length > 0 ? val.split("-")[1] : val;
        $("#formQa1rmuDesc").val(val);
        var req = {};
        req.Section = '';
        req.RoadCode = '';
        req.RMU = $("#formQa1rmu option:selected").text().split("-")[1];
        $.ajax({
            url: '/MAM/RMUSecRoad',
            dataType: 'JSON',
            data: req,
            type: 'Post',
            success: function (data) {
                if (data != null) {
                    $("#formQa1SectionCode").empty();
                    $("#formQa1SectionCode").append($("<option></option>").val("").html("Select Section Code"));
                    $.each(data.section, function (index, v) {
                        $("#formQa1SectionCode").append($("<option></option>").val(v.value).html(v.text));
                    });
                    $("#formQa1SectionCode").trigger("change");
                    $('#formQa1SectionCode').trigger("chosen:updated");

                } else {

                }
            },
            error: function (data) {

                console.error(data);
            }
        });
    });

    $("#formQa1Crew").on("change", function () {
        var val = $("#formQa1Crew option:selected").text();
        var Name = val.split("-")[1];
        var ctrl = $(this);
        if (val != "99999999" && val != "") {
            $("#formQa1CrewName").val(Name);
        }
        else if (val == "99999999") {
            $("#txt CrewName").prop("disabled", false);
            $("#formQa1CrewName").val('');
        }
        else {
            $("#formQa1CrewName").prop("disabled", true);
            $("#formQa1CrewName").val('');
        }
        return false;
    });

    $("#formQa1SectionCode").on("change", function () {
        var ddldata = $(this).val();
        var secCode = $("#formQa1SectionCode option:selected").text().split("-")[0];
        if (ddldata != "") {
            $("#formQa1SectionName").val(ddldata);
            //GetAllRoadCodeDataBySectionCode
            $.ajax({
                url: '/FormQa1/GetRoadCodeBySection',
                dataType: 'JSON',
                data: { secCode },
                type: 'Post',
                success: function (data) {
                    if (data != null) {
                        $("#formQa1RoadCode").empty();
                        $("#formQa1RoadCode").append($("<option></option>").val("").html("Select Road Code"));
                        $.each(data, function (index, v) {
                            var text = v.roadCode + " - " + v.roadName;
                            $("#formQa1RoadCode").append($("<option></option>").val(v.roadCode).html(text).attr("Item1", v.roadName));
                        });
                        $("#formQa1RoadCode").trigger("change");
                        $('#formQa1RoadCode').trigger("chosen:updated");

                    } else {

                    }
                },
                error: function (data) {

                    console.error(data);
                }
            });
        }
        else {
            $("#formQa1SectionName").val("");
        }
        return false;
    });

    $("#formQa1ActivityCode").on("change", function () {
        var val = $(this).find(":selected").text();
        val = val.split("-").length > 0 ? val.split("-")[1] : val;
        $("#formQa1ActivityName").val(val);
    });

    $("#formQa1RoadCode").on("change", function () {
        var val = $(this).find(":selected").attr("Item1");
        $("#formQa12RoadName").val(val);
    });

    $("#formQa1WeekNo").on("change", function () {
        var text = GetDate($("#formQa1Year").val(), $(this).val(), $("#formQa1Day").val());
        getDateRangeOfWeek($("#formQa1WeekNo").val(), $("#formQa1Year").val());
    });

    $("#formQa1Day").on("change", function () {
        var text = GetDate($("#formQa1Year").val(), $("#formQa1WeekNo").val(), $(this).val());
        getDateRangeOfWeek($("#formQa1WeekNo").val(), $("#formQa1Year").val());
    });

    $("#formQa1Year").on("change", function () {
        getDateRangeOfWeek($("#formQa1WeekNo").val(), $("#formQa1Year").val());
        SetReferenceId();
    })

    $('#formQa1Dt').on("change", function () {
        SetWeekDayYear($('#formQa1Dt').val());
    });

});

function disableAll() {
    //Labour
    $("#AccordPage1 *").attr("disabled", "disabled").off('click');
    $(".lddl").chosen('destroy');
    $(".lddl").attr("disabled", "disabled");

    //Work Execution
    $("#AccordPage2 *").attr("disabled", "disabled").off('click');
    $(".wddl").chosen('destroy');
    $(".wddl").attr("disabled", "disabled").off('click');

    //Specific Condition
    $("#AccordPage6 *").attr("disabled", "disabled").off('click');
    $(".ddl").chosen('destroy');
    $(".ddl").attr("disabled", "disabled").off('click');

    //Work Completion Quality
    $("#AccordPage7 *").attr("disabled", "disabled").off('click');

    //Testing
    $("#AccordPage12 *").attr("disabled", "disabled").off('click');
    $(".tddl").chosen('destroy');
    $(".tddl").attr("disabled", "disabled").off('click');

    //General Comments
    $("#AccordPage16 *").attr("disabled", "disabled").off('click');

    //Remark
    $("#AccordPage8 *").attr("disabled", "disabled").off('click');
}

function GetDate(year, weekno, weekday) {
    var Value = "";
    $.ajax({
        url: '/ERT/GetDateString',
        dataType: 'html',
        data: { year: year, weekno: weekno, weekday: weekday },
        type: 'Post',
        success: function (data) {
            $("#hdnWeekNo").val(data.split('~')[0]);
            $("#hdnMonthNo").val(data.split('~')[1]);
            //$("#hdnFormDMonthNo").val(data.split('~')[1]);
            SetReferenceId()
        },
        error: function (data) {
            console.log(data);
        }
    });

    return Value;
}

$(document).on("click", "#btnFindDetails", function () {

    if (ValidatePage("#FormQa1Headers", "", "")) {
        InitAjaxLoading();
        GetResponseValue("FindDetails", "FormQa1", FormValueCollection("#FormQa1Headers"), function (data) {
            HideAjaxLoading();
            if (data != undefined && data != null) {

                $.ajax({
                    url: '/FormQA1/CreateFormQa1',
                    data: FormValueCollection("#FormQa1Headers"),
                    type: 'POST',
                    success: function (data) {
                        $("#btnFindDetails").hide();
                        $("#saveFormQa1Btn").show();
                        $("#SubmitFormQa1Btn").show();
                    }
                })

            }
        }, "Finding");
    }
});

Date.prototype.getWeek = function () {
    var target = new Date(this.valueOf());
    var dayNr = (this.getDay() + 6) % 7;
    target.setDate(target.getDate() - dayNr + 3);
    var firstThursday = target.valueOf();
    target.setMonth(0, 1);
    if (target.getDay() != 4) {
        target.setMonth(0, 1 + ((4 - target.getDay()) + 7) % 7);
    }
    return 1 + Math.ceil((firstThursday - target) / 604800000);
}

function GetWeekNumber(date) {
    var target = new Date(date);
    var dayNr = (target.getDay() + 6) % 7;
    target.setDate(target.getDate() - dayNr + 3);
    var firstThursday = target.valueOf();
    target.setMonth(0, 1);
    if (target.getDay() != 4) {
        target.setMonth(0, 1 + ((4 - target.getDay()) + 7) % 7);
    }
    return 1 + Math.ceil((firstThursday - target) / 604800000);
}

function getDateRangeOfWeek(weekNo, y) {
    var d1, numOfdaysPastSinceLastMonday, rangeIsFrom, rangeIsTo, d2;
    d1 = new Date('' + y + '');
    numOfdaysPastSinceLastMonday = d1.getDay() - 1;
    d2 = new Date('' + y + '');
    d2.setDate(d2.getDate() - numOfdaysPastSinceLastMonday);

    if (d2.getFullYear() < y) {
        d2.setDate(d2.getDate() + 7);
        d1 = new Date(d2);
    }
    else {
        d1.setDate(d1.getDate() - numOfdaysPastSinceLastMonday);
    }
    d1.setDate(d1.getDate() + (7 * (weekNo - d1.getWeek())));
    rangeIsFrom = new Date((d1.getMonth() + 1) + "/" + d1.getDate() + "/" + y);
    d1.setDate(d1.getDate() + 6);
    rangeIsTo = new Date((d1.getMonth() + 1) + "/" + d1.getDate() + "/" + y);

    var weekday = new Array(7);
    weekday[0] = "Sunday";
    weekday[1] = "Monday";
    weekday[2] = "Tuesday";
    weekday[3] = "Wednesday";
    weekday[4] = "Thursday";
    weekday[5] = "Friday";
    weekday[6] = "Saturday";

    while (rangeIsFrom <= rangeIsTo) {
        var d3 = new Date(rangeIsFrom);
        if (weekday[d3.getDay()] == $("#formQa1Day").val()) {
            var d4 = formatDate(d3);
            $('#formQa1Dt').val(d4);
        }
        d3.setDate(d3.getDate() + 1);
        rangeIsFrom = new Date(d3,);
    }

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

function SetWeekDayYear(date) {

    //Set Week
    var todaydate = new Date(date);
    $("#formQa1WeekNo").val(GetWeekNumber(date)).trigger("chosen:updated");

    //Set Day
    var weekday = new Array(7);
    weekday[0] = "Sunday";
    weekday[1] = "Monday";
    weekday[2] = "Tuesday";
    weekday[3] = "Wednesday";
    weekday[4] = "Thursday";
    weekday[5] = "Friday";
    weekday[6] = "Saturday";

    var n = weekday[todaydate.getDay()];
    $("#formQa1Day").val(n).trigger("chosen:updated");;

    //Set Year
    $("#formQa1Year").val(todaydate.getFullYear()).trigger("chosen:updated");;
}

function SetReferenceId() {
    /*var maxcount = $("#hdnMaxNo").val();
    if ($("#formQaPkRefNo").val() != 0) {
        //debugger;
        $("#btnFindDetails").hide();
        if ($("#hdnView").val() != "1") {
            $("#saveFormQa1Btn").show();
            $("#SubmitFormQa1Btn").show();
        }
    }
    else {
        var RefId = "MAM/FormQA1/" + $("#formQa1WeekNo").val() + "-" + $("#hdnMonthNo").val() + "-" + $("#formQa1Year").val() + "/" + $("#formQa1CrewUnit").val() + "/????"
        $("#formQa1ReferenceNo").val(RefId);

    }*/
}

function userIdDisable() {
    $('#formQa1AssignedBy').prop('disabled', true).trigger("chosen:updated");
    $('#formQa1ExecutedBy').prop('disabled', true).trigger("chosen:updated");
    $('#formQa1CheckedBy').prop('disabled', true).trigger("chosen:updated");
    $('#formQa1WitnessedBy').prop('disabled', true).trigger("chosen:updated");
    $('#formQa1AuditedBy').prop('disabled', true).trigger("chosen:updated");
}

function gridAddBtnDis() {
    $("#btnEquipAdd,#btnGenAdd,#btnMaterialAdd").prop("disabled", false);
}

function UserDtDisable() {
    $('#formQa1AssignedDate').prop('disabled', true);
    $('#formQa1ExecutedDate').prop('disabled', true);
    $('#formQa1CheckedDate').prop('disabled', true);
    $('#formQa1WitnessedDate').prop('disabled', true);
    $('#formQa1AuditedDate').prop('disabled', true);
}

function FormGenGridRefresh() {
    var filterData = new Object();
    oTable = $('#FormQa1GenGridView').DataTable();
    oTable.data = filterData;
    oTable.draw();
}

function FormMatGridRefresh() {
    var filterData = new Object();
    oTable = $('#FormQa1MaterialGridView').DataTable();
    oTable.data = filterData;
    oTable.draw();
}

function FormEquipGridRefresh() {
    var filterData = new Object();
    oTable = $('#FormQa1EquipGridView').DataTable();
    oTable.data = filterData;
    oTable.draw();
}

function EditFormQa1Gen(id, view) {

    InitAjaxLoading();
    $.ajax({
        url: '/FormQA1/EditFormQa1Gen',
        data: { id: id },
        type: 'POST',
        success: function (data) {
            $("#div-gen-container").html(data);
            if (view == 1 || $("#hdnView").val() == "1") {
                $("#hdnGenView").val(1);
                $("#FormQa1GenModalid").html("View Attention")
                $("#div-gen-container *").attr("disabled", "disabled").off('click');
                $("#saveFormQa1GenBtn").css("display", "none");
                $("#cancelAddModelBtn").attr("disabled", false);
                $("#saveContinueFormQa1GenBtn").css("display", "none");
            } else if ($("#hdnLabid").val() != "") {
                $("#FormQa1GenModalid").html("Edit Attention")
            }
            HideAjaxLoading();
        }
    })
}

function EditFormQa1Equip(id, view) {
    InitAjaxLoading();
    $.ajax({
        url: '/FormQA1/EditFormEquipment',
        data: { id: id },
        type: 'POST',
        success: function (data) {
            $("#div-equip-container").html(data);
            if (view == 1 || $("#hdnView").val() == "1") {
                $("#hdnEquView").val(1);
                $("#FormQa1EquipModalid").html("View Equipment & Vehicles");
                $("#div-equip-container *").attr("disabled", "disabled").off('click');
                $("#saveFormQa1EqBtn").css("display", "none");
                $("#cancelAddModelBtn").attr("disabled", false);
                $("#saveContinueFormQa1EqBtn").css("display", "none");
            }
            else if ($("#hdnEquipid").val() != "")
                $("#FormQa1EquipModalid").html("Edit Equipment & Vehicles");
            HideAjaxLoading();
        }
    })
}

function EditFormQa1Material(id, view) {
    InitAjaxLoading();
    $.ajax({
        url: '/FormQA1/EditFormMaterial',
        data: { id: id },
        type: 'POST',
        success: function (data) {
            $("#div-mat-container").html(data);
            if (view == 1 || $("#hdnView").val() == "1") {
                $("#hdnMatView").val(1);
                $("#FormQa1MaterialModalid").html("View Material")
                $("#div-mat-container *").attr("disabled", "disabled").off('click');
                $("#saveFormQa1MtBtn").css("display", "none");
                $("#cancelAddModelBtn").attr("disabled", false);
                $("#saveContinueFormQa1MtBtn").css("display", "none");
            } else if ($("#hdnMaterialNo").val() != "")
                $("#FormQa1MaterialModalid").html("Edit Material")
            HideAjaxLoading();
        }
    })
}

//ADD EQUIP

$(document).on("click", "#saveFormDEqBtn", function () {
    saveEquipment(false, false);
});

$(document).on("click", "#saveContinueFormDEqBtn", function () {
    saveEquipment(false, true);
});

function saveEquipment(isSubmit, cont) {
    if (ValidatePage('#FormAddEquipDetails')) {
        InitAjaxLoading();
        var d = new Date();
        var month = d.getMonth() + 1;
        var day = d.getDate();
        var output = (('' + month).length < 2 ? '0' : '') + month + '/' +
            (('' + day).length < 2 ? '0' : '') + day + '/' + d.getFullYear();

        var saveObj = new Object;
        //saveObj.SubmitStatus = isSubmit;
        saveObj.PkRefNo = $("#hdnEquipid").val();
        saveObj.Fqa1hPkRefNo = $("#formQa1PkRefNo").val();
        saveObj.Type = $("#formQa1EquipCode").find(":selected").val();
        saveObj.Desc = $("#formQa1EquipDesc").val();
        saveObj.PVNo = $("#formQa1EquipPVNo").val();
        saveObj.Capacity = $("#formQa1EquipCapacity").val();
        saveObj.Condition = $("#formQa1EquipCondition").val();
        saveObj.Unit = $("#formDEquipUnit").find(":selected").val();
        saveObj.Remark = $("#formQa1EqpRemark").val();
        saveObj.ModDt = output
        saveObj.CrDt = output
        saveObj.ActiveYn = true;
        $.ajax({
            url: '/FormQA1/SaveEquipment',
            data: saveObj,
            type: 'POST',
            success: function (data) {
                app.ShowSuccessMessage('Successfully Saved', false);
                $("#val-summary-displayer").css("display", "none");
                $("#val-summary-displayer-equip").css("display", "none");
                if (!cont)
                    $("#FormDEquipModal").modal("hide");
                else {
                    $("#hdnEquipid").val('');
                    $("#formQa1EquipCode").val('').trigger('chosen:updated');
                    $("#formQa1EquipDesc").val("");
                    $("#formQa1EquipPVNo").val("");
                    $("#formQa1EquipCapacity").val("");
                    $("#formQa1EquipCondition").val("");
                    $("#formQa1EqpRemark").val("");
                    $("#formDEquipUnit").val("").trigger('chosen:updated');
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

function saveGeneral(isSubmit, cont) {
    if (ValidatePage('#FormDLabourModal')) {
        InitAjaxLoading();
        var d = new Date();

        var month = d.getMonth() + 1;
        var day = d.getDate();

        var output = (('' + month).length < 2 ? '0' : '') + month + '/' +
            (('' + day).length < 2 ? '0' : '') + day + '/' + d.getFullYear();

        var saveObj = new Object;
        saveObj.SubmitStatus = isSubmit;
        var tdlen = $("#FormDLabourGridView").find("tbody").find("tr td").length;
        var rowno = $("#FormDLabourGridView").find("tbody").find("tr").length;
        //  saveObj.SerialNo = tdlen == 1 ? 1 : rowno + 1;
        saveObj.FdmdFdhPkRefNo = $("#FDHRef_No").val();
        saveObj.No = $("#hdnLabid").val();
        saveObj.LabourCode = $("#formDLabCode").find(":selected").val();
        saveObj.CodeDesc = $("#formDlabDesc").val();
        saveObj.LabourDesc = $("#formDLabDescription").val();
        saveObj.Quantity = $("#formDLabQty").val();
        saveObj.Unit = $("#formDLabUnit").find(":selected").val();
        saveObj.SerialNo = $("#LabSerialNo").val();
        //saveObj.ContNo = $("#formDContNo").val();

        //saveObj.ReportedByUsername = $("#FormDAttnUsername").val();

        //saveObj.UseridVer = $("#FormDVeriBy").find(":selected").text();
        //saveObj.UsernameVer = $("#FormDVerUsername").val();
        //saveObj.DtVer = $("#formDverDate").val();

        //saveObj.UseridVet = $("#FormDSVetby").find(":selected").text();
        //saveObj.UsernameVet = $("#FormDSVetUsername").val();

        saveObj.DateReported = output
        // saveObj.ModifeidBy = $("#FormDSVerby").find(":selected").text();
        saveObj.ModifiedDate = output
        // saveObj.CreatedBy = $("#FormDSVerby").find(":selected").text();
        saveObj.CreatedDate = output
        saveObj.ActiveYn = true;

        $.ajax({
            url: '/ERT/FormDSaveLabour',
            data: saveObj,
            type: 'POST',
            success: function (data) {
                app.ShowSuccessMessage('Successfully Saved', false);
                $("#val-summary-displayer").css("display", "none");
                $("#val-summary-displayer-labour").css("display", "none");
                FormLabGridRefresh();
                if (!cont)
                    $("#FormDLabourModal").modal("hide");
                else {
                    $("#hdnLabid").val('');
                    $("#formDLabCode").val('').trigger('chosen:updated');
                    $("#formDlabDesc").val("");
                    $("#formDLabUnit").val("").trigger('chosen:updated');
                    $("#formDLabQty").val("");
                    $("#formDLabDescription").val("")
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
    if (ValidatePage('#FormDMaterialModal')) {
        InitAjaxLoading();
        var d = new Date();

        var month = d.getMonth() + 1;
        var day = d.getDate();

        var output = (('' + month).length < 2 ? '0' : '') + month + '/' +
            (('' + day).length < 2 ? '0' : '') + day + '/' + d.getFullYear();

        var saveObj = new Object;
        saveObj.SubmitStatus = isSubmit;
        var tdlen = $("#FormDMaterialGridView").find("tbody").find("tr td").length;
        var rowno = $("#FormDMaterialGridView").find("tbody").find("tr").length;
        // saveObj.SerialNo = tdlen == 1 ? 1 : rowno + 1;
        saveObj.FdmdFdhPkRefNo = $("#FDHRef_No").val();
        saveObj.No = $("#hdnMaterialNo").val();
        saveObj.MaterialCode = $("#formDMatCode").find(":selected").val();
        saveObj.CodeDesc = $("#formDMatDesc").val();
        saveObj.MaterialDesc = $("#FormDMatDescription").val();
        saveObj.Quantity = $("#formDMatQuantity").val();
        saveObj.Unit = $("#formDMatUnit").find(":selected").val();
        saveObj.SerialNo = $("#MaterialSerialNo").val();
        //saveObj.ContNo = $("#formDContNo").val();

        //saveObj.ReportedByUsername = $("#FormDAttnUsername").val();

        //saveObj.UseridVer = $("#FormDVeriBy").find(":selected").text();
        //saveObj.UsernameVer = $("#FormDVerUsername").val();
        //saveObj.DtVer = $("#formDverDate").val();

        //saveObj.UseridVet = $("#FormDSVetby").find(":selected").text();
        //saveObj.UsernameVet = $("#FormDSVetUsername").val();

        saveObj.DateReported = output
        // saveObj.ModifeidBy = $("#FormDSVerby").find(":selected").text();
        saveObj.ModifiedDate = output
        // saveObj.CreatedBy = $("#FormDSVerby").find(":selected").text();
        saveObj.CreatedDate = output
        saveObj.ActiveYn = true;
        $.ajax({
            url: '/ERT/FormDSaveMaterial',
            data: saveObj,
            type: 'POST',
            success: function (data) {
                app.ShowSuccessMessage('Successfully Saved', false);
                $("#val-summary-displayer").css("display", "none");
                $("#val-summary-displayer-material").css("display", "none");
                if (!cont)
                    $("#FormDMaterialModal").modal("hide");
                else {
                    $("#hdnMaterialNo").val('');
                    $("#FormDMaterialNo").val("");
                    $("#formDMatCode").val('').trigger('chosen:updated');
                    $("#formDMatDesc").val("");
                    $("#FormDMatDescription").val("");
                    $("#formDMatQuantity").val("");
                    $("#formDMatUnit").val("").trigger('chosen:updated');
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