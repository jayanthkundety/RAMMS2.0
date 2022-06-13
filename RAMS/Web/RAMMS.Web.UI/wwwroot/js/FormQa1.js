var currentDate = new Date();
$(document).ready(function () {
    currentDate = formatDate(currentDate);
    var val = $("#formQa1PkRefNo").val();

    $('.allow_numeric').keypress(function (event) {
        var $this = $(this);
        if ((event.which != 46 || $this.val().indexOf('.') != -1) &&
            ((event.which < 48 || event.which > 57) &&
                (event.which != 0 && event.which != 8))) {
            event.preventDefault();
        }

        var text = $(this).val();
        if ((event.which == 46) && (text.indexOf('.') == -1)) {
            setTimeout(function () {
                if ($this.val().substring($this.val().indexOf('.')).length > 3) {
                    $this.val($this.val().substring(0, $this.val().indexOf('.') + 3));
                }
            }, 1);
        }

        if ((text.indexOf('.') != -1) &&
            (text.substring(text.indexOf('.')).length > 3) &&
            (event.which != 0 && event.which != 8) &&
            ($(this)[0].selectionStart >= text.length - 3)) {
            event.preventDefault();
        }
    });

    $('.allow_numericWOD').keypress(function (event) {
        var $this = $(this);
        if ((event.which != 46 || $this.val().indexOf('.') == -1) &&
            ((event.which < 48 || event.which > 57) &&
                (event.which != 0 && event.which != 8))) {
            event.preventDefault();
        }

        var text = $(this).val();
        if ((event.which == 46) && (text.indexOf('.') != -1)) {
            setTimeout(function () {
                if ($this.val().substring($this.val().indexOf('.')).length > 3) {
                    $this.val($this.val().substring(0, $this.val().indexOf('.') + 3));
                }
            }, 1);
        }
    });

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
        disableAll(true);

    }

    if ($("#hdnView").val() == "1") {
        $("#saveFormQa1Btn").hide();
        $("#SubmitFormQa1Btn").hide();
        $("#formQa1AssignedDate").attr("disabled", "true");
        $("#formQa1ExecutedDate").attr("disabled", "true");
        $("#formQa1CheckedDate").attr("disabled", "true");

        $("#formQa1SignAssg").attr("disabled", "true");
        $("#formQa1SignExe").attr("disabled", "true");
        $("#formQa1SignChck").attr("disabled", "true");

        $("#div-addformd *:not(.enblmode)").attr("disabled", "disabled").off('click');
        $("#formQa1rmu").chosen('destroy');
        $("#btnFrmV2Back").attr("disabled", false);
        $("#formQa1CrewCode").chosen('destroy');
        $("#formQa1MaterialEdit").attr("disabled", "disabled").off('click');
        $("#formQa1EquipmentEdit").attr("disabled", "disabled").off('click');

        userIdDisable();
        disableAll(true);
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
        val = val.split("-")[0];
        var ctrl = $(this);
        if (val != "99999999" && val != "") {
            $("#formQa1CrewName").val(Name);
        }
        else if (val == "99999999") {
            $("#formQa1CrewName").prop("readonly", false);
            $("#formQa1CrewName").val('');
        }
        else {
            $("#formQa1CrewName").prop("readonly", true);
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

    $('#FormQa1Date').on("change", function () {
        SetWeekDayYear($('#FormQa1Date').val());
    });

    $("#formQa1AuditedBy").on("change", function () {
        var id = $("#formQa1AuditedBy option:selected").val();
        var ctrl = $(this);
        if (id != "99999999" && id != "") {
            $("#formQa1AuditedName").val(ctrl.find("option:selected").attr("Item1"));
            $("#formQa1AuditedDesig").val(ctrl.find("option:selected").attr("Item2"));
            $("#formQa1AuditOffice").val(ctrl.find("option:selected").attr("Item3"));
            $("#formQa1AuditSign").prop("checked", true);
        }
        else if (id == "99999999") {
            $("#formQa1AuditedName").prop("disabled", false);
            $("#formQa1AuditedName").val('');
            $("#formQa1AuditedDesig").prop("disabled", false);
            $("#formQa1AuditedDesig").val('');
            $("#formQa1AuditOffice").prop("disabled", false);
            $("#formQa1AuditOffice").val('');
            $("#formQa1AuditSign").prop("checked", true);
        }
        else {
            $("#formQa1AuditedName").prop("disabled", true);
            $("#formQa1AuditedName").val('');
            $("#formQa1AuditedDesig").prop("disabled", true);
            $("#formQa1AuditedDesig").val('');
            $("#formQa1AuditOffice").prop("disabled", true);
            $("#formQa1AuditOffice").val('');
            //$("#formQa1AuditSign").prop("checked", true);
        }
        $("#formQa1AuditDate").val(currentDate);
        return false;
    });

    $("#formQa1WitnessedBy").on("change", function () {
        var id = $("#formQa1WitnessedBy option:selected").val();
        var ctrl = $(this);
        if (id != "99999999" && id != "") {
            $("#formQa1WitnessedName").val(ctrl.find("option:selected").attr("Item1"));
            $("#formQa1WitnessedDesig").val(ctrl.find("option:selected").attr("Item2"));
            $("#formW2WitnessedOffice").val(ctrl.find("option:selected").attr("Item3"));
            $("#formQa1SignWit").prop("checked", true);
        }
        else if (id == "99999999") {
            $("#formQa1WitnessedName").prop("disabled", false);
            $("#formQa1WitnessedName").val('');
            $("#formQa1WitnessedDesig").prop("disabled", false);
            $("#formQa1WitnessedDesig").val('');
            $("#formW2WitnessedOffice").prop("disabled", false);
            $("#formW2WitnessedOffice").val('');
            $("#formQa1SignWit").prop("checked", true);
        }
        else {
            $("#formQa1WitnessedName").prop("disabled", true);
            $("#formQa1WitnessedName").val('');
            $("#formQa1WitnessedDesig").prop("disabled", true);
            $("#formQa1WitnessedDesig").val('');
            $("#formW2WitnessedOffice").prop("disabled", true);
            $("#formW2WitnessedOffice").val('');
            //$("#formQa1SignWit").prop("checked", true);
        }
        $("#formQa1WitnessedDate").val(currentDate);
        return false;
    });

    $("#formQa1AssignedBy").on("change", function () {
        var id = $("#formQa1AssignedBy option:selected").val();
        var ctrl = $(this);
        if (id != "99999999" && id != "") {
            $("#formQa1AssignedName").val(ctrl.find("option:selected").attr("Item1"));
            $("#formQa1SignAssg").prop("checked", true);
        }
        else if (id == "99999999") {
            $("#formQa1AssignedName").prop("disabled", false);
            $("#formQa1AssignedName").val('');
            $("#formQa1SignAssg").prop("disabled", false);
            $("#formQa1SignAssg").prop("checked", true);
        }
        else {
            $("#formQa1AssignedName").prop("disabled", true);
            $("#formQa1AssignedName").val('');
            $("#formQa1SignAssg").prop("disabled", false);
        }
        $("#formQa1AssignedDate").val(currentDate);
        return false;
    });

    $("#formQa1ExecutedBy").on("change", function () {
        var id = $("#formQa1ExecutedBy option:selected").val();
        var ctrl = $(this);
        if (id != "99999999" && id != "") {
            $("#formQa1ExecutedName").val(ctrl.find("option:selected").attr("Item1"));
            $("#formQa1SignExe").prop("checked", true);
        }
        else if (id == "99999999") {
            $("#formQa1ExecutedName").prop("disabled", false);
            $("#formQa1ExecutedName").val('');
            $("#formQa1SignExe").prop("disabled", false);
            $("#formQa1SignExe").prop("checked", true);
        }
        else {
            $("#formQa1ExecutedName").prop("disabled", true);
            $("#formQa1ExecutedName").val('');
            $("#formQa1SignExe").prop("disabled", true);
        }
        $("#formQa1ExecutedDate").val(currentDate);
        return false;
    });

    $("#formQa1CheckedBy").on("change", function () {
        var id = $("#formQa1CheckedBy option:selected").val();
        var ctrl = $(this);
        if (id != "99999999" && id != "") {
            $("#formQa1CheckedName").val(ctrl.find("option:selected").attr("Item1"));
            $("#formQa1SignChck").prop("checked", true);
        }
        else if (id == "99999999") {
            $("#formQa1CheckedName").prop("disabled", false);
            $("#formQa1CheckedName").val('');
            $("#formQa1SignChck").prop("disabled", false);
            $("#formQa1SignChck").prop("checked", true);
        }
        else {
            $("#formQa1CheckedName").prop("disabled", true);
            $("#formQa1CheckedName").val('');
            $("#formQa1SignChck").prop("disabled", true);
        }
        $("#formQa1CheckedDate").val(currentDate);
        return false;
    });
});

function getDt() {
    var d = new Date();
    var month = d.getMonth() + 1;
    var day = d.getDate();
    var output = (('' + month).length < 2 ? '0' : '') + month + '/' +
        (('' + day).length < 2 ? '0' : '') + day + '/' + d.getFullYear();
    return output;
}

function disableAll(action) {
    if (action) {
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
    else {
        //Labour
        $("#AccordPage1 *").removeAttr("disabled");
        $(".lddl").removeAttr("disabled");
        $(".lddl").prop('disabled', false).trigger("chosen:updated");

        //Work Execution
        $("#AccordPage2 *").removeAttr("disabled");
        $(".wddl").removeAttr("disabled");
        $(".wddl").prop('disabled', false).trigger("chosen:updated");


        //Specific Condition
        $("#AccordPage6 *").removeAttr("disabled");
        $(".ddl").removeAttr("disabled");
        $(".ddl").prop('disabled', false).trigger("chosen:updated");

        //Work Completion Quality
        $("#AccordPage7 *").removeAttr("disabled");

        //Testing
        $("#AccordPage12 *").removeAttr("disabled");
        $(".tddl").removeAttr("disabled");
        $(".tddl").prop('disabled', false).trigger("chosen:updated");

        //General Comments
        $("#AccordPage16 *").removeAttr("disabled");

        //Remark
        $("#AccordPage8 *").removeAttr("disabled");
    }
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

            if (data != undefined && data != null && data.PkRefNo > 0) {

                if (data.isExist) {
                    location.href = './EditFormQa1?id=' + data.PkRefNo;
                    return;
                }
                $("#btnFindDetails").hide();
                $("#saveFormQa1Btn").show();
                $("#SubmitFormQa1Btn").hide();
                gridAddBtnDis();
                disableAll(false);
                $("#formQa1AssignedBy").val($("#hdnUserId").val()).trigger("chosen:updated");
                $("#formQa1AssignedBy").trigger("change");
                $("#formQa1PkRefNo").val(data.PkRefNo);
                $("#formQa1ReferenceNo").val(data.RefId);
                $("#formQa1TesPkRefNo").val(data.Tes.PkRefNo);

                $('#formQa1AuditedBy').val(_CurrentUser).trigger("chosen:updated");
                $("#formQa1AuditedBy").trigger("change");

                $('#formQa1WitnessedBy').val(_CurrentUser).trigger("chosen:updated");
                $("#formQa1WitnessedBy").trigger("change");

                $('#FormQa1GenGridView').DataTable().settings()[0].ajax.url = "/FormQA1/LoadFormQa1GenList?id=" + data.PkRefNo;
                FormGenGridRefresh();

                $('#FormQa1MaterialGridView').DataTable().settings()[0].ajax.url = "/FormQA1/LoadFormQa1MaterialList?id=" + data.PkRefNo;
                FormMatGridRefresh();

                $('#FormQa1EquipGridView').DataTable().settings()[0].ajax.url = "/FormQA1/LoadFormQa1EquipmentList?id=" + data.PkRefNo;
                FormEquipGridRefresh();
                
            }
            else {
                app.ShowErrorMessage("Unable to fetch Form QA1");
            }
        }, "Finding");
    }
});

$(document).on("click", "#formQa1GcWhs", function () {
    if (!$(this).prop("checked")) return false;
    $("#formQa1GcWis").prop("checked", false);
    $("#formQa1GcWius").prop("checked", false);
    $("#formQa1GcWiusMat").prop("checked", false);
    $("#formQa1GcWiusEqp").prop("checked", false);
    $("#formQa1GcWiusWrk").prop("checked", false);

    //clearGcRemarks();
    
    //$("#divWhsRemark *").attr("disabled", false);
    //$("#divWisRemark *").attr("disabled", true);
    //$("#divWiusMatRemark *").attr("disabled", true);
    //$("#divWiusEqpRemark *").attr("disabled", true);
    //$("#divWiusWrkRemark *").attr("disabled", true);
    //$("#formQa1WhsRemark").focus();
    return true;
});

$(document).on("click", "#formQa1GcWis", function () {
    if (!$(this).prop("checked")) return false;
    $("#formQa1GcWhs").prop("checked", false);
    $("#formQa1GcWius").prop("checked", false);
    $("#formQa1GcWiusMat").prop("checked", false);
    $("#formQa1GcWiusEqp").prop("checked", false);
    $("#formQa1GcWiusWrk").prop("checked", false);
    //clearGcRemarks();   
    //$("#divWisRemark *").attr("disabled", false);
    //$("#divWhsRemark *").attr("disabled", true);
    //$("#divWiusMatRemark *").attr("disabled", true);
    //$("#divWiusEqpRemark *").attr("disabled", true);
    //$("#divWiusWrkRemark *").attr("disabled", true);
    //$("#formQa1WisRemark").focus();
    return true;
});

$(document).on("click", "#formQa1GcWius", function () {
    if (!$(this).prop("checked")) return false;
    $("#formQa1GcWhs").prop("checked", false);
    $("#formQa1GcWis").prop("checked", false);
    $("#formQa1GcWiusMat").prop("checked", true);
    $("#formQa1GcWiusEqp").prop("checked", false);
    $("#formQa1GcWiusWrk").prop("checked", false);
    //clearGcRemarks();
    //$("#divWisRemark *").attr("disabled", true);
    //$("#divWhsRemark *").attr("disabled", true);
    //$("#divWiusMatRemark *").attr("disabled", false);
    //$("#divWiusEqpRemark *").attr("disabled", true);
    //$("#divWiusWrkRemark *").attr("disabled", true);
    //$("#formQa1WiusMatRemark").focus();
    return true;
});

$(document).on("click", "#formQa1GcWiusMat", function () {
   // if (!$(this).prop("checked")) return false;
    //$("#formQa1GcWiusEqp").prop("checked", false);
    //$("#formQa1GcWiusWrk").prop("checked", false);

    if (!$("#formQa1GcWius").prop("checked"))
        $("#formQa1GcWius").prop("checked", true);

    $("#formQa1GcWhs").prop("checked", false);
    $("#formQa1GcWis").prop("checked", false);
    //clearGcRemarks();
    
    //$("#divWisRemark *").attr("disabled", true);
    //$("#divWhsRemark *").attr("disabled", true);
    //$("#divWiusMatRemark *").attr("disabled", false);
    //$("#divWiusEqpRemark *").attr("disabled", true);
    //$("#divWiusWrkRemark *").attr("disabled", true);
    //$("#formQa1WiusMatRemark").focus();
    return true;
});

$(document).on("click", "#formQa1GcWiusEqp", function () {
    //if (!$(this).prop("checked")) return false;
    //$("#formQa1GcWiusMat").prop("checked", false);
    //$("#formQa1GcWiusWrk").prop("checked", false);

    if (!$("#formQa1GcWius").prop("checked"))
        $("#formQa1GcWius").prop("checked", true);

    $("#formQa1GcWhs").prop("checked", false);
    $("#formQa1GcWis").prop("checked", false);
    //clearGcRemarks();
    
    //$("#divWisRemark *").attr("disabled", true);
    //$("#divWhsRemark *").attr("disabled", true);
    //$("#divWiusMatRemark *").attr("disabled", true);
    //$("#divWiusEqpRemark *").attr("disabled", false);
    //$("#divWiusWrkRemark *").attr("disabled", true);
    //$("#formQa1WiusEqpRemark").focus();
    return true;
});

$(document).on("click", "#formQa1GcWiusWrk", function () {
    //if (!$(this).prop("checked")) return false;
    //$("#formQa1GcWiusMat").prop("checked", false);
    //$("#formQa1GcWiusEqp").prop("checked", false);

    if (!$("#formQa1GcWius").prop("checked"))
        $("#formQa1GcWius").prop("checked", true);

    $("#formQa1GcWhs").prop("checked", false);
    $("#formQa1GcWis").prop("checked", false);
    //clearGcRemarks();
    
    //$("#divWisRemark *").attr("disabled", true);
    //$("#divWhsRemark *").attr("disabled", true);
    //$("#divWiusMatRemark *").attr("disabled", true);
    //$("#divWiusEqpRemark *").attr("disabled", true);
    //$("#divWiusWrkRemark *").attr("disabled", false);
    //$("#formQa1WiusWrkRemark").focus();
    return true;
});

//END GC

$(document).on("click", "#formQa1FlushType", function () {
    if ($(this).prop("checked"))
        $("#formQa1FlFlush").focus();
});

$(document).on("click", "#formQa1FlThType", function () {
    if ($(this).prop("checked"))
        $("#formQa1FlTh").focus();
});

$(document).on("click", "#formQa1FlTlType", function () {
    if ($(this).prop("checked"))
        $("#formQa1FlTl").focus();
});

$(document).on("click", "#formQa1JnType", function () {
    if (!$(this).prop("checked")) return false;
    $("#formQa1JiType").prop("checked", false);
    $("#formQa1JiRemark").attr("disabled", true);
    $("#formQa1JnRemark").attr("disabled", false);
    return true;
});

$(document).on("click", "#formQa1JiType", function () {
    if (!$(this).prop("checked")) return false;
    $("#formQa1JnType").prop("checked", false);
    $("#formQa1JnRemark").attr("disabled", true);
    $("#formQa1JiRemark").attr("disabled", false);
    return true;
});

$(document).on("click", "#formQa1SrevType", function () {
    if (!$(this).prop("checked")) return false;
    $("#formQa1SruevType").prop("checked", false);
    $("#formQa1SrprType").prop("checked", false);

    $("#formQa1SrEvenRemark").attr("disabled", false);
    $("#formQa1SrUnEvenRemark").attr("disabled", true);
    $("#formQa1SrPrRemark").attr("disabled", true);
    return true;
});

$(document).on("click", "#formQa1SruevType", function () {
    if (!$(this).prop("checked")) return false;
    $("#formQa1SrevType").prop("checked", false);
    $("#formQa1SrprType").prop("checked", false);

    $("#formQa1SrEvenRemark").attr("disabled", true);
    $("#formQa1SrUnEvenRemark").attr("disabled", false);
    $("#formQa1SrPrRemark").attr("disabled", true);
    return true;
});

$(document).on("click", "#formQa1SrprType", function () {
    if (!$(this).prop("checked")) return false;
    $("#formQa1SruevType").prop("checked", false);
    $("#formQa1SrevType").prop("checked", false);

    $("#formQa1SrEvenRemark").attr("disabled", true);
    $("#formQa1SrUnEvenRemark").attr("disabled", true);
    $("#formQa1SrPrRemark").attr("disabled", false);
    return true;
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
            $('#FormQa1Date').val(d4);
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
    var gTable = $('#FormQa1GenGridView').DataTable();
    gTable.data = filterData;
    gTable.draw();
    gTable.columns.adjust().draw();
}

function FormMatGridRefresh() {
    var filterData = new Object();
    var mTable = $('#FormQa1MaterialGridView').DataTable();
    mTable.data = filterData;
    mTable.draw();
    //$('#FormQa1MaterialGridView').DataTable().columns.adjust().draw();
}

function FormEquipGridRefresh() {
    var filterData = new Object();
    var eTable = $('#FormQa1EquipGridView').DataTable();
    eTable.data = filterData;
    eTable.draw();
    //$('#FormQa1EquipGridView').DataTable().columns.adjust().draw();
}

function EditFormQa1Gen(id, view) {
    debugger;
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
                $("#cancelFormQa1GenBtn").attr("disabled", false);
                $("#saveContinueFormQa1GenBtn").css("display", "none");
            }
            else if ($("#hdnGenId").val() == "0")
                $("#FormQa1EquipModalid").html("Add Attention");
            else if ($("#hdnGenId").val() != "") {
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
                $("#cancelFormQa1EqBtn").attr("disabled", false);
                $("#saveContinueFormQa1EqBtn").css("display", "none");
            }
            else if ($("#hdnEquipid").val() == "0")
                $("#FormQa1EquipModalid").html("Add Equipment & Vehicles");
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
                $("#cancelFormQa1MtBtn").attr("disabled", false);
                $("#saveContinueFormQa1MtBtn").css("display", "none");
            }
            else if ($("#hdnMaterialNo").val() == "0")
                $("#FormQa1MaterialModalid").html("Add Material")
            else if ($("#hdnMaterialNo").val() != "")
                $("#FormQa1MaterialModalid").html("Edit Material")
            HideAjaxLoading();
        }
    })
}

function DeleteFormQa1MaterialRecord(pKRefNo) {
    if ($("#hdnView").val() == "1") return;
    if (app.Confirm("Are you sure you want to delete the record?", function (e) {
        if (e) {
            InitAjaxLoading();
            $.ajax({
                url: '/FormQA1/DeleteMaterial',
                data: { pKRefNo },
                type: 'POST',
                success: function (data) {
                    if (data.ret.result > 0) {
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

function DeleteFormQa1GenRecord(pKRefNo) {
    if ($("#hdnView").val() == "1") return;
    if (app.Confirm("Are you sure you want to delete the record?", function (e) {
        if (e) {
            InitAjaxLoading();
            $.ajax({
                url: '/FormQA1/DeleteGeneral',
                data: { pKRefNo },
                type: 'POST',
                success: function (data) {
                    if (data.ret.result > 0) {
                        app.ShowSuccessMessage('Successfully Deleted', false);
                        FormGenGridRefresh();
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

function DeleteFormQa1EquipmentRecord(pKRefNo) {
    if ($("#hdnView").val() == "1") return;
    if (app.Confirm("Are you sure you want to delete the record?", function (e) {
        if (e) {
            InitAjaxLoading();
            $.ajax({
                url: '/FormQA1/DeleteEquipment',
                data: { pKRefNo },
                type: 'POST',
                success: function (data) {
                    if (data.ret.result > 0) {
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

//SAVE EQUIP
$(document).on("click", "#saveFormQa1EqBtn", function () {
    saveEquipment(false, false);
});

$(document).on("click", "#saveContinueFormQa1EqBtn", function () {
    saveEquipment(false, true);
});

//SAVE MATERIAL
$(document).on("click", "#saveContinueFormQa1MtBtn", function () {
    saveMaterial(false, true);
});

$(document).on("click", "#saveFormQa1MtBtn", function () {
    saveMaterial(false, false);
});

//SAVE GENERAL
$(document).on("click", "#saveContinueFormQa1GenBtn", function () {
    saveGeneral(false, true);
});

$(document).on("click", "#saveFormQa1GenBtn", function () {
    saveGeneral(false, false);
});

//SAVE FORMQA1
$(document).on("click", "#saveFormQa1Btn", function () {
    saveHeader(false);
});

$(document).on("click", "#SubmitFormQa1Btn", function () {
    saveHeader(true);
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
                    $("#FormQa1EquipModal").modal("hide");
                else {
                    $("#hdnEquipid").val('');
                    $("#formQa1EquipCode").val('').trigger('chosen:updated');
                    $("#formQa1EquipDesc").val("");
                    $("#formQa1EquipPVNo").val("");
                    $("#formQa1EquipCapacity").val("");
                    $("#formQa1EqpRemark").val("");
                    $("#formQa1EquipCondition").val("").trigger('chosen:updated');
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
    if (ValidatePage('#FormAddGeneral')) {
        InitAjaxLoading();
        var d = new Date();

        var month = d.getMonth() + 1;
        var day = d.getDate();

        var output = (('' + month).length < 2 ? '0' : '') + month + '/' +
            (('' + day).length < 2 ? '0' : '') + day + '/' + d.getFullYear();

        var saveObj = new Object;
        //saveObj.SubmitStatus = isSubmit;
        saveObj.PkRefNo = $("#hdnGenId").val();
        saveObj.Fqa1hPkRefNo = $("#formQa1PkRefNo").val();
        saveObj.Item = $("#formQa1GenItem").val();
        saveObj.AttTo = $("#formQa1GenAttTo").val();
        saveObj.AttRemarks = $("#formQa1GenAttRemark").val();
        saveObj.ModDt = output
        saveObj.CrDt = output
        saveObj.ActiveYn = true;

        $.ajax({
            url: '/FormQA1/SaveGeneral',
            data: saveObj,
            type: 'POST',
            success: function (data) {
                app.ShowSuccessMessage('Successfully Saved', false);
                $("#val-summary-displayer").css("display", "none");
                $("#val-summary-displayer-labour").css("display", "none");
                if (!cont)
                    $("#FormAddGeneral").modal("hide");
                else {
                    $("#hdnGenId").val('');
                    $("#formQa1GenItem").val("");
                    $("#formQa1GenAttTo").val("");
                    $("#formQa1GenAttRemark").val("")
                }
                HideAjaxLoading();
                FormGenGridRefresh();
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
    if (ValidatePage('#FormQa1MaterialModal')) {
        InitAjaxLoading();
        var d = new Date();

        var month = d.getMonth() + 1;
        var day = d.getDate();

        var output = (('' + month).length < 2 ? '0' : '') + month + '/' +
            (('' + day).length < 2 ? '0' : '') + day + '/' + d.getFullYear();

        var saveObj = new Object;
        //saveObj.SubmitStatus = isSubmit;
        saveObj.PkRefNo = $("#hdnMaterialNo").val();
        saveObj.Fqa1hPkRefNo = $("#formQa1PkRefNo").val();
        saveObj.Type = $("#formQa1MatType").val();
        saveObj.Qty = $("#formQa1MatQty").val();
        saveObj.Unit = $("#formQa1MatUnit").find(":selected").val();
        saveObj.Spec = $("#formQa1MatSpec").find(":selected").val();
        saveObj.Remark = $("#formQa1MatRemark").val();
        saveObj.ModDt = output
        saveObj.CrDt = output
        saveObj.ActiveYn = true;
        $.ajax({
            url: '/FormQA1/SaveMaterial',
            data: saveObj,
            type: 'POST',
            success: function (data) {
                app.ShowSuccessMessage('Successfully Saved', false);
                $("#val-summary-displayer").css("display", "none");
                $("#val-summary-displayer-material").css("display", "none");
                if (!cont)
                    $("#FormQa1MaterialModal").modal("hide");
                else {
                    $("#hdnMaterialNo").val('');
                    $("#formQa1MatType").val("");
                    $("#formQa1MatQty").val("");
                    $("#formQa1MatUnit").val('').trigger('chosen:updated');
                    $("#formQa1MatSpec").val("").trigger('chosen:updated');
                    $("#formQa1MatRemark").val("");
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

function saveHeader(submitsts) {

    if (submitsts) {
        $(".svalidate").addClass("validate");
        AddWEValidate();
        AddGCValidate();
        AddWCQValidate();
    }
    else {
        $(".svalidate").removeClass("validate");
        $(".awvalidate").removeClass("validate");
        $(".tcvalidate").removeClass("validate");
        $(".acvalidate").removeClass("validate");
        $(".pcvalidate").removeClass("validate");
        $(".rbvalidate").removeClass("validate");
        $(".sbvalidate").removeClass("validate");
        $(".sgvalidate").removeClass("validate");
    }


    if (ValidatePage('#divFormQA1')) {
        InitAjaxLoading();
        var d = new Date();
        var month = d.getMonth() + 1;
        var day = d.getDate();
        var output = (('' + month).length < 2 ? '0' : '') + month + '/' +
            (('' + day).length < 2 ? '0' : '') + day + '/' + d.getFullYear();

        var saveObj = new Object;
        //saveObj.SubmitStatus = isSubmit;
        saveObj.PkRefNo = $("#formQa1PkRefNo").val();

        saveObj.Rmu = $("#formQa1rmu").find(":selected").val();
        saveObj.RmuName = $("#formQa1rmuDesc").val();
        saveObj.SecCode = $("#formQa1SectionCode").find(":selected").val();
        saveObj.SecName = $("#formQa1SectionName").val();
        saveObj.RoadCode = $("#formQa1RoadCode").find(":selected").val();
        saveObj.RoadName = $("#formQa12RoadName").val();
        saveObj.WeekNo = $("#formQa1WeekNo").find(":selected").val();
        saveObj.Day = $("#formQa1Day").val();
        saveObj.Year = $("#formQa1Year").val();
        saveObj.RefId = $("#formQa1ReferenceNo").val();
        saveObj.Crew = $("#formQa1Crew").find(":selected").val();
        saveObj.Crewname = $("#formQa1CrewName").val();
        saveObj.ActCode = $("#formQa1ActivityCode").find(":selected").val();
        saveObj.ActName = $("#formQa1ActivityName").val();
        saveObj.Dt = $("#FormQa1Date").val();
        saveObj.Lab = getLabour();
        saveObj.Gc = getGC()
        saveObj.Ssc = getSSC();
        saveObj.Tes = getTES();
        saveObj.Wcq = getWCQ();
        saveObj.We = getWE();
        saveObj.UseridAssgn = $("#formQa1AssignedBy").find(":selected").val();
        saveObj.UsernameAssgn = $("#formQa1AssignedName").val();
        saveObj.InitialAssgn = $("#formQa1SignAssg").prop("checked");
        saveObj.DtAssgn = $("#formQa1AssignedDate").val();

        saveObj.UseridExec = $("#formQa1ExecutedBy").find(":selected").val();
        saveObj.UsernameExec = $("#formQa1ExecutedName").val();
        saveObj.InitialExec = $("#formQa1SignExe").prop("checked");
        saveObj.DtExec = $("#formQa1ExecutedDate").val();
        saveObj.UseridChked = $("#formQa1CheckedBy").find(":selected").val();
        saveObj.UsernameChked = $("#formQa1CheckedName").val();
        saveObj.InitialChked = $("#formQa1SignChck").prop("checked");
        saveObj.DtChked = $("#formQa1CheckedDate").val();
        saveObj.ModDt = output
        saveObj.CrDt = output
        saveObj.SignAudit = $("#formQa1AuditSign").val();
        saveObj.UseridAudit = $("#formQa1AuditedBy").find(":selected").val();
        saveObj.UsernameAudit = $("#formQa1AuditedName").val();
        saveObj.DesignationAudit = $("#formQa1AuditedDesig").val();
        saveObj.DtAudit = $("#formQa1AuditDate").val();
        saveObj.OfficeAudit = $("#formQa1AuditOffice").val();
        saveObj.SignWit = $("#formQa1SignWit").val();
        saveObj.UseridWit = $("#formQa1WitnessedBy").find(":selected").val();
        saveObj.UsernameWit = $("#formQa1WitnessedName").val();
        saveObj.DesignationWit = $("#formQa1WitnessedDesig").val();
        saveObj.DtWit = $("#formQa1WitnessedDate").val();
        saveObj.OfficeWit = $("#formW2WitnessedOffice").val();
        saveObj.Remarks = $("#formQa1Remarks").val();
        saveObj.SubmitSts = submitsts;
        saveObj.ActiveYn = true;

        $.ajax({
            url: '/FormQA1/SaveHeader',
            data: saveObj,
            type: 'POST',
            success: function (data) {
                app.ShowSuccessMessage('Successfully Saved', false);
                location.href = "/FormQA1/QA1"
                HideAjaxLoading();
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

function getLabour() {
    var lab = new Object();
    var dt = getDt();
    lab.PkRefNo = $("#formQa1LabPkRefNo").val();
    lab.Fqa1hPkRefNo = $("#formQa1PkRefNo").val();
    lab.LabCsOnSite = $("#formQa1LabCROnSite").val();
    lab.LabCsOnLeave = $("#formQa1LabCROnLeave").val();
    lab.LabCsPerfStd = $("#formQa1LabCRPerfStd").find(":selected").val();
    lab.LabCsRemark = $("#formQa1LabCRRemark").val();
    lab.LabOpOnSite = $("#formQa1LabOPOnSite").val();
    lab.LabOpOnLeave = $("#formQa1LabOPOnLeave").val();
    lab.LabOpPerfStd = $("#formQa1LabOPPerfStd").find(":selected").val();
    lab.LabOpRemark = $("#formQa1LabOPRemark").val();
    lab.LabDrOnSite = $("#formQa1LabDROnSite").val();
    lab.LabDrOnLeave = $("#formQa1LabDROnLeave").val();
    lab.LabDrPerfStd = $("#formQa1LabDRPerfStd").find(":selected").val();
    lab.LabDrRemark = $("#formQa1LabDRRemark").val();
    lab.LabWmOnSite = $("#formQa1LabWMOnSite").val();
    lab.LabWmOnLeave = $("#formQa1LabWMOnLeave").val();
    lab.LabWmPerFStd = $("#formQa1LabWMPerfStd").find(":selected").val();
    lab.LabWmRemark = $("#formQa1LabWMRemark").val();
    lab.LabOthOnSite = $("#formQa1LabOTHOnSite").val();
    lab.LabOthOnLeave = $("#formQa1LabOTHOnLeave").val();
    lab.LabOthPerfStd = $("#formQa1LabOTHPerfStd").find(":selected").val();
    lab.LabOthRemark = $("#formQa1LabOTHRemark").val();
    lab.ActiveYn = true;
    lab.CrDt = dt;
    lab.ModDt = dt;
    return lab;
}

function getGC() {
    var gc = new Object();
    var dt = getDt();
    gc.PkRefNo = $("#formGCPkRefNo").val();
    gc.Fqa1hPkRefNo = $("#formQa1PkRefNo").val();
    gc.Whs = $("#formQa1GcWhs").prop("checked");
    gc.WhsRemark = $("#formQa1WhsRemark").val();
    gc.WhsReason = $("#formQa1WhsReason").val();
    gc.Wis = $("#formQa1GcWis").prop("checked");
    gc.WisRemark = $("#formQa1WisRemark").val();
    gc.WisReason = $("#formQa1WisReason").val();
    gc.Wius = $("#formQa1GcWius").prop("checked");
    gc.WiusMat = $("#formQa1GcWiusMat").prop("checked");
    gc.WiusMatRemark = $("#formQa1WiusMatRemark").val();
    gc.WiusMatReason = $("#formQa1WiusMatReason").val();
    gc.WiusEqp = $("#formQa1GcWiusEqp").prop("checked");
    gc.WiusEqpReason = $("#formQa1WiusEqpRemark").val();
    gc.WiusEqpRemark = $("#formQa1WiusEqpReason").val();
    gc.WiusWrk = $("#formQa1GcWiusWrk").prop("checked");
    gc.WiusWrkRemark = $("#formQa1WiusWrkRemark").val();
    gc.WiusWrkReason = $("#formQa1WiusWrkReason").val();
    gc.ActiveYn = true;
    gc.ModDt = dt;
    gc.CrDt = dt;
    return gc;
}

function getSSC() {
    var ssc = new Object();
    var dt = getDt();
    ssc.PkRefNo = $("#formQa1SSCPkRefNo").val();
    ssc.Fqa1hPkRefNo = $("#formQa1PkRefNo").val();
    ssc.Sp = $("#formQa1Sp").find(":selected").val();
    ssc.SpRemark = $("#formQa1SpRemark").val();
    ssc.Ed = $("#formQa1Ed").find(":selected").val();
    ssc.EdRemark = $("#formQa1EdRemark").val();
    ssc.Wpe = $("#formQa1Wpe").find(":selected").val();
    ssc.WpeRemark = $("#formQa1WpeRemark").val();
    ssc.Ims = $("#formQa1Ims").find(":selected").val();
    ssc.ImsRemark = $("#formQa1ImsRemark").val();
    ssc.Asd = $("#formQa1Asd").find(":selected").val();
    ssc.AsdRemark = $("#formQa1AsdRemark").val();
    ssc.ActiveYn = true;
    ssc.ModDt = dt;
    ssc.CrDt = dt;
    return ssc;
}

function getTES() {
    var tes = new Object();
    var dt = getDt();
    tes.PkRefNo = $("#formQa1TesPkRefNo").val();
    tes.Fqa1hPkRefNo = $("#formSSCPkRefNo").val();
    tes.CtCs = $("#formQa1CtCs").find(":selected").val();
    tes.CtCsA = "";
    tes.CtCsRemark = $("#formQa1CtCsRemark").val();
    tes.DtCs = $("#formQa1DtCs").find(":selected").val();
    tes.DtCsA = "";
    tes.DtCsRemark = $("#formQa1DtCsRemark").val();
    tes.MgtCs = $("#formQa1MgtCs").find(":selected").val();
    tes.MgtCsA = "";
    tes.MgtCsRemark = $("#formQa1MgtCsRemark").val();
    tes.CbrCs = $("#formQa1CbrCs").find(":selected").val();
    tes.CbrCsA = "";
    tes.CbrCsRemark = $("#formQa1CbrCsRemark").val();
    tes.OtCs = $("#formQa1OtCs").find(":selected").val();
    tes.OtCsA = "";
    tes.OtCsRemark = $("#formQa1OtCsRemark").val();
    tes.ActiveYn = true;
    tes.ModDt = dt;
    tes.CrDt = dt;
    return tes;
}

function getWCQ() {
    var wcq = new Object();
    var dt = getDt();
    wcq.PkRefNo = $("#formQa1WCQPkRefNo").val();
    wcq.Fqa1hPkRefNo = $("#formQa1PkRefNo").val();
    wcq.FlFlushType = $("#formQa1FlushType").prop("checked");
    wcq.FlFlush = $("#formQa1FlFlush").val();
    wcq.FlFlushRemark = $("#formQa1FlFlushRemark").val();
    wcq.FlThType = $("#formQa1FlThType").prop("checked");
    wcq.FlTh = $("#formQa1FlTh").val();
    wcq.FlThRemark = $("#formQa1FlThRemark").val();
    wcq.FlTlType = $("#formQa1FlTlType").prop("checked");
    wcq.FlTl = $("#formQa1FlTl").val();
    wcq.FlTlRemark = $("#formQa1FlTlRemark").val();
    wcq.FlScType = $("#formQa1FlScType").prop("checked");
    wcq.FlScRemark = $("#formQa1FlScRemark").val();
    wcq.FlUcType = $("#formQa1FlUcType").prop("checked");
    wcq.FlUcRemark = $("#formQa1FlUcRemark").val();
    wcq.JnType = $("#formQa1JnType").prop("checked");
    wcq.JnRemark = $("#formQa1JnRemark").val();
    wcq.JiType = $("#formQa1JiType").prop("checked");
    wcq.JiRemark = $("#formQa1JiRemark").val();
    wcq.SrevType = $("#formQa1SrevType").prop("checked");
    wcq.SrevRemark = $("#formQa1SrEvenRemark").val();
    wcq.SruevType = $("#formQa1SruevType").prop("checked");
    wcq.SruevRemark = $("#formQa1SrUnEvenRemark").val();
    wcq.SrprType = $("#formQa1SrprType").prop("checked");
    wcq.SrprRemark = $("#formQa1SrPrRemark").val();
    wcq.ActiveYn = true;
    wcq.ModDt = dt;
    wcq.CrDt = dt;
    return wcq;

}

function getWE() {
    var we = new Object();
    var dt = getDt();
    we.PkRefNo = $("#formQa1WEPkRefNo").val();
    we.Fqa1hPkRefNo = $("#formQa1PkRefNo").val();
    we.AcwcThinkness = $("#formQa1AcwcThinkness").val();
    we.AcwcThinknessUnit = "";
    we.AcwcTemperature = $("#formQa1AcwcTemperature").val();
    we.AcwcTemperatureUnit = "";
    we.AcwcPasses = $("#formQa1AcwcPasses").val();
    we.AcwcRemark = $("#formQa1AcwcRemark").val();
    we.TcDRate = $("#formQa1TcDRate").val();
    we.TcDRateUnit = "";
    we.TcType = $("#formQa1TcType").val();
    we.TcEvenlySpread = $("#formQa1TcEvSpNo").find(":selected").val();
    we.TcRemark = $("#formQa1TcRemark").val();
    we.AcbcThinkness = $("#formQa1AcbcThinkness").val();
    we.AcbcThinknessUnit = "";
    we.AcbcTemperature = $("#formQa1AcbcTemperature").val();
    we.AcbcTemperatureUnit = "";
    we.AcbcPasses = $("#formQa1AcbcPasses").val();
    we.AcbcRemark = $("#formQa1AcbcRemark").val();
    we.PcDRate = $("#formQa1PcDRate").val();
    we.PcDRateUnit = "";
    we.PcType = $("#formQa1PcType").val();
    we.PcEvenlySpread = $("#formQa1PcEvSpNo").find(":selected").val();
    we.PcRemark = $("#formQa1PcRemark").val();
    we.RbThinkness = $("#formQa1RbThinkness").val();
    we.RbThinknessUnit = "";
    we.RbLayers = $("#formQa1RbLayers").val();
    we.RbPasses = $("#formQa1RbPasses").val();
    we.RbRemark = $("#formQa1RbRemark").val();
    we.SbThinkness = $("#formQa1SbThinkness").val();
    we.SbThinknessUnit = "";
    we.SbLayers = $("#formQa1SbLayers").val();
    we.SbPasses = $("#formQa1SbPasses").val();
    we.SbRemark = $("#formQa1SbRemark").val();
    we.SgThinkness = $("#formQa1SgThinkness").val();
    we.SgThinknessUnit = "";
    we.SgLayers = $("#formQa1SgLayers").val();
    we.SgPasses = $("#formQa1SgPasses").val();
    we.SgRemark = $("#formQa1SgRemark").val();
    we.SsdSb = $("#formQa1SsdSb").val();
    we.SsdSbUnit = "";
    we.SsdPp = $("#formQa1SsdPp").val();
    we.SsdPpUnit = "";
    we.SsdRemark = $("#formQa1SsdRemark").val();

    we.SsdCh = $("#formQa1SsdCh").val();
    we.SsdChDeci = $("#formQa1SsdChDeci").val();
    we.SsdRhsL = $("#formQa1SsdRhsL").val();
    we.SsdRhsW = $("#formQa1SsdRhsW").val();
    we.SsdLhsL = $("#formQa1SsdLhsL").val();
    we.SsdLhsW = $("#formQa1SsdLhsW").val();

    we.ActiveYn = true;
    we.ModDt = dt;
    we.CrDt = dt;

    return we;
}

function GoBack() {
    if ($("#hdnView").val() == 0) {
        if (app.Confirm("Unsaved changes will be lost. Are you sure you want to cancel?", function (e) {
            if (e) {
                location.href = "/FormQA1/QA1";
            }
        }));
    } else
        location.href = "/FormQA1/QA1";
}

//Attachment

function FormQA1Image(row) {
    $("#hdnImageRow").val(row);
    if ($("#hdnView").val() == "1") return;
    if ($("#formQa1PkRefNo").val() != "0") {
        $("#myModal").modal('show');
        GetAttachment();
    }
}

function UploadModalClose() {
    document.getElementById("files").disabled = true;
    document.getElementById("FormBrowseBtn").disabled = true;
    document.getElementById("btnImageUpload").disabled = true;
    $("#photolist").empty();
    $('#myModal').modal('hide');
}

function deleteImageupload(imageToDelete) {
    var imagedelete = imageToDelete.cellIndex - 1;
    app.Confirm("Are you sure you want to remove?, If Yes click OK.", function (e) {
        if (e) {
            var row = document.getElementById("ImageRow")
            row.deleteCell(imageToDelete.cellIndex);
            row.deleteCell(imagedelete);
        }
    });
}

function DeleteAttachment(pKRefNo, row) {
    if ($("#hdnView").val() == "1") return;
    if (app.Confirm("Are you sure you want to delete the record?", function (e) {
        if (e) {
            InitAjaxLoading();
            $.ajax({
                url: '/FormQA1/DeleteAttachment',
                data: { pKRefNo },
                type: 'POST',
                success: function (data) {
                    if (data.ret > 0) {
                        $("#divCs" + row).html(AddShowAttachmentButton(row));
                        HideAjaxLoading();
                        app.ShowSuccessMessage("Successfully Deleted")
                    }
                    else {
                        HideAjaxLoading();
                        alert("Error in Deleted. Kindly retry later.");

                    }

                }
            });
        }
    }));
}

function AddShowAttachmentButton(row) {
    return '<label>&nbsp;</label><div style="text-align:center"><a class="btn btn-sm btn-themebtn" data-toggle="modal" onclick="FormQA1Image(' + row + ')"><span class="add-icon"></span> Add Attachment</a></div>';
}

function SetAttachment(row) {
    var tesPKRefNo = $("#formQa1TesPkRefNo").val();
    $.ajax({
        url: '/FormQA1/GetImages',
        data: { tesPKRefNo, row },
        type: 'POST',
        success: function (data) {
            var ret = "";
            if (data.ret.length > 0) {
                for (var i = 0; i < data.ret.length; i++) {
                    ret += '<label>&nbsp;</label><div style="text-align:center"><span style=" white-space: nowrap; overflow: hidden; width:60%; float:left; text-overflow: ellipsis;">' +
                        '<a class="warCaption" href="~/' + data.ret[i].imageFilenameUpload + '" target="_blank" title="Click here to View the Document">' + data.ret[i].imageUserFilePath + '</a></span>' +
                        '<span style="width: 40%; float:left"><button type="button" style="border:none;align-content:center;background:transparent" onclick="DeleteAttachment(' + data.ret[i].pkRefNo + ',' + row + ')">' +
                        '<span class="remove-icon"></span></button></span></div>';
                }
            }
            $("#divCs" + row).html(ret);
        }
    });
}

function GetAttachment() {
    $.ajax({
        url: '/FormQA1/GetAttachment',
        type: 'POST',
        success: function (data) {
            $("#divAttachment").html(data);
        },
        error: function (data) {
            alert(data.responseText);
        }

    });

    return true;
}

//Validate General Comments

function AddGCValidate() {
    var isChecked = false;
    if ($("#formQa1GcWhs").prop("checked")) {
        isChecked = true;
        $("#formQa1GcWhs").addClass("validate")
        //$("#formQa1WhsRemark").addClass("validate")
        //$("#formQa1WhsReason").addClass("validate")
    } else {
        $("#formQa1GcWhs").removeClass("validate")
        //$("#formQa1WhsRemark").removeClass("validate")
        //$("#formQa1WhsReason").removeClass("validate")

        $("#formQa1GcWhs").removeClass("border-error")
        //$("#formQa1WhsRemark").removeClass("border-error")
        //$("#formQa1WhsReason").removeClass("border-error")
    }


    if ($("#formQa1GcWis").prop("checked")) {
        isChecked = true;
        $("#formQa1GcWis").addClass("validate");
        //$("#formQa1WisRemark").addClass("validate");
        //$("#formQa1WisReason").addClass("validate");
    }
    else {
        $("#formQa1GcWis").removeClass("validate");
        //$("#formQa1WisRemark").removeClass("validate");
        //$("#formQa1WisReason").removeClass("validate");

        $("#formQa1GcWis").removeClass("border-error");
        //$("#formQa1WisRemark").removeClass("border-error");
        //$("#formQa1WisReason").removeClass("border-error");
    }

    if ($("#formQa1GcWius").prop("checked")) {
        isChecked = true;

    }

    if ($("#formQa1GcWiusMat").prop("checked")) {
        isChecked = true;
        $("#formQa1GcWiusMat").addClass("validate");
        //$("#formQa1WiusMatRemark").addClass("validate");
        //$("#formQa1WiusMatReason").addClass("validate");
    }
    else {
        $("#formQa1GcWius").removeClass("validate");
        $("#formQa1GcWiusMat").removeClass("validate");
        //$("#formQa1WiusMatRemark").removeClass("validate");
        //$("#formQa1WiusMatReason").removeClass("validate");

        $("#formQa1GcWius").removeClass("border-error");
        $("#formQa1GcWiusMat").removeClass("border-error");
        //$("#formQa1WiusMatRemark").removeClass("border-error");
        //$("#formQa1WiusMatReason").removeClass("border-error");
    }

    if ($("#formQa1GcWiusEqp").prop("checked")) {
        isChecked = true;
        $("#formQa1GcWiusEqp").addClass("validate");
        //$("#formQa1WiusEqpRemark").addClass("validate");
        //$("#formQa1WiusEqpReason").addClass("validate");
    } else {
        $("#formQa1GcWius").removeClass("validate");
        $("#formQa1GcWiusEqp").removeClass("validate");
        //$("#formQa1WiusEqpRemark").removeClass("validate");
        //$("#formQa1WiusEqpReason").removeClass("validate");

        $("#formQa1GcWius").removeClass("border-error");
        $("#formQa1GcWiusEqp").removeClass("border-error");
        //$("#formQa1WiusEqpRemark").removeClass("border-error");
        //$("#formQa1WiusEqpReason").removeClass("border-error");

    }

    if ($("#formQa1GcWiusWrk").prop("checked")) {
        isChecked = true;
        $("#formQa1GcWiusWrk").addClass("validate");
        //$("#formQa1WiusWrkRemark").addClass("validate");
        //$("#formQa1WiusWrkReason").addClass("validate");
    } else {
        $("#formQa1GcWius").removeClass("validate");
        $("#formQa1GcWiusWrk").removeClass("validate");
        //$("#formQa1WiusWrkRemark").removeClass("validate");
        //$("#formQa1WiusWrkReason").removeClass("validate");

        $("#formQa1GcWius").removeClass("border-error");
        $("#formQa1GcWiusWrk").removeClass("border-error");
        //$("#formQa1WiusWrkRemark").removeClass("border-error");
        //$("#formQa1WiusWrkReason").removeClass("border-error");
    }

    if (!isChecked) {
        addGCValidateClass();
    }

}


function addGCValidateClass() {
    $("#formQa1GcWhs").addClass("validate")
    $("#formQa1WhsRemark").addClass("validate")
    $("#formQa1WhsReason").addClass("validate")

    $("#formQa1GcWis").addClass("validate");
    $("#formQa1WisRemark").addClass("validate");
    $("#formQa1WisReason").addClass("validate");

    $("#formQa1GcWius").addClass("validate");

    $("#formQa1GcWiusMat").addClass("validate");
    $("#formQa1WiusMatRemark").addClass("validate");
    $("#formQa1WiusMatReason").addClass("validate");

    $("#formQa1GcWiusEqp").addClass("validate");
    $("#formQa1WiusEqpRemark").addClass("validate");
    $("#formQa1WiusEqpReason").addClass("validate");

    $("#formQa1GcWiusWrk").addClass("validate");
    $("#formQa1WiusWrkRemark").addClass("validate");
    $("#formQa1WiusWrkReason").addClass("validate");
}

function AddWEValidate() {
    if ($("#formQa1AcwcThinkness").val() != "") {
        $(".awvalidate").addClass("validate");
    }
    if ($("#formQa1TcDRate").val() != "") {
        $(".tcvalidate").addClass("validate");
    }
    if ($("#formQa1AcbcThinkness").val() != "") {
        $(".acvalidate").addClass("validate");
    }
    if ($("#formQa1PcDRate").val() != "") {
        $(".pcvalidate").addClass("validate");
    }
    if ($("#formQa1RbThinkness").val() != "") {
        $(".rbvalidate").addClass("validate");
    }
    if ($("#formQa1SbThinkness").val() != "") {
        $(".sbvalidate").addClass("validate");
    }
    if ($("#formQa1SgThinkness").val() != "") {
        $(".sgvalidate").addClass("validate");
    }
}

function AddWCQValidate() {
    var isChecked = false;

    if ($("#formQa1FlushType").prop("checked")) {
        isChecked = true;
        $("#formQa1FlushType").addClass("validate");
        $("#formQa1FlFlush").addClass("validate");
        //$("#formQa1FlFlushRemark").addClass("validate");
    } else {
        $("#formQa1FlushType").removeClass("validate");
        $("#formQa1FlFlush").removeClass("validate");
        $("#formQa1FlFlushRemark").removeClass("validate");
        $("#formQa1FlFlush").removeClass("border-error");
        //$("#formQa1FlFlushRemark").removeClass("border-error");
    }

    if ($("#formQa1FlThType").prop("checked")) {
        isChecked = true;
        $("#formQa1FlThType").addClass("validate");
        $("#formQa1FlTh").addClass("validate");
        //$("#formQa1FlThRemark").addClass("validate");
    }
    else {
        $("#formQa1FlThType").removeClass("validate");
        $("#formQa1FlTh").removeClass("validate");
        //$("#formQa1FlThRemark").removeClass("validate");

        $("#formQa1FlThType").removeClass("border-error");
        $("#formQa1FlTh").removeClass("border-error");
       // $("#formQa1FlThRemark").removeClass("border-error");

    }

    if ($("#formQa1FlTlType").prop("checked")) {
        isChecked = true;
        $("#formQa1FlTlType").addClass("validate");
        $("#formQa1FlTl").addClass("validate");
        //$("#formQa1FlTlRemark").addClass("validate");
    } else {
        $("#formQa1FlTlType").removeClass("validate");
        $("#formQa1FlTl").removeClass("validate");
        //$("#formQa1FlTlRemark").removeClass("validate");

        $("#formQa1FlTlType").removeClass("border-error");
        $("#formQa1FlTl").removeClass("border-error");
        //$("#formQa1FlTlRemark").removeClass("border-error");
    }

    if ($("#formQa1FlScType").prop("checked")) {
        isChecked = true;
        $("#formQa1FlScType").addClass("validate");
        //$("#formQa1FlScRemark").addClass("validate");
    } else {
        $("#formQa1FlScType").removeClass("validate");
        //$("#formQa1FlScRemark").removeClass("validate");

        $("#formQa1FlScType").removeClass("border-error");
        //$("#formQa1FlScRemark").removeClass("border-error");
    }

    if ($("#formQa1FlUcType").prop("checked")) {
        isChecked = true;
        $("#formQa1FlUcType").addClass("validate");
        //$("#formQa1FlUcRemark").addClass("validate");
    } else {
        $("#formQa1FlUcType").removeClass("validate");
        //$("#formQa1FlUcRemark").removeClass("validate");

        $("#formQa1FlUcType").removeClass("border-error");
        //$("#formQa1FlUcRemark").removeClass("border-error");
    }

    var jnChecked = false;
    if ($("#formQa1JnType").prop("checked")) {
        jnChecked = true;
        $("#formQa1JnType").addClass("validate");
        //$("#formQa1JnRemark").addClass("validate");
    } else {
        $("#formQa1JnType").removeClass("validate");
        //$("#formQa1JnRemark").removeClass("validate");

        $("#formQa1JnType").removeClass("border-error");
        //$("#formQa1JnRemark").removeClass("border-error");
    }

    if ($("#formQa1JiType").prop("checked")) {
        jnChecked = true;
        $("#formQa1JiType").addClass("validate");
        //$("#formQa1JiRemark").addClass("validate");
    }
    else {
        $("#formQa1JiType").removeClass("validate");
        //$("#formQa1JiRemark").removeClass("validate");

        $("#formQa1JiType").removeClass("border-error");
        //$("#formQa1JiRemark").removeClass("border-error");
    }


    var srChecked = false;
    if ($("#formQa1SrevType").prop("checked")) {
        srChecked = true;
        $("#formQa1SrevType").addClass("validate");
        //$("#formQa1SrEvenRemark").addClass("validate");
    }
    else {
        $("#formQa1SrevType").removeClass("validate");
        //$("#formQa1SrEvenRemark").removeClass("validate");

        $("#formQa1SrevType").removeClass("border-error");
        //$("#formQa1SrEvenRemark").removeClass("border-error");
    }

    if ($("#formQa1SruevType").prop("checked")) {
        srChecked = true;
        $("#formQa1SruevType").addClass("validate");
        //$("#formQa1SrUnEvenRemark").addClass("validate");
    } else {
        $("#formQa1SruevType").removeClass("validate");
        //$("#formQa1SrUnEvenRemark").removeClass("validate");

        $("#formQa1SruevType").removeClass("border-error");
        //$("#formQa1SrUnEvenRemark").removeClass("border-error");
    }

    if ($("#formQa1SrprType").prop("checked")) {
        srChecked = true;
        $("#formQa1SrprType").addClass("validate");
        //$("#formQa1SrPrRemark").addClass("validate");
    } else {
        $("#formQa1SrprType").removeClass("validate");
        //$("#formQa1SrPrRemark").removeClass("validate");

        $("#formQa1SrprType").removeClass("border-error");
        //$("#formQa1SrPrRemark").removeClass("border-error");
    }

    if (!isChecked) {
        addWCQValidateClass();
    }

    if (!jnChecked) {
        addWCQJointingClass();
    }


    if (!srChecked) {
        addWCQSurfaceReg();
    }


}

function addWCQValidateClass() {
    $("#formQa1FlushType").addClass("validate");
    $("#formQa1FlFlush").addClass("validate");
    //$("#formQa1FlFlushRemark").addClass("validate");

    $("#formQa1FlThType").addClass("validate");
    $("#formQa1FlTh").addClass("validate");
    //$("#formQa1FlThRemark").addClass("validate");

    $("#formQa1FlTlType").addClass("validate");
    $("#formQa1FlTl").addClass("validate");
    //$("#formQa1FlTlRemark").addClass("validate");

    $("#formQa1FlScType").addClass("validate");
    //$("#formQa1FlScRemark").addClass("validate");

    $("#formQa1FlUcType").addClass("validate");
    //$("#formQa1FlUcRemark").addClass("validate");
}

function addWCQJointingClass() {
    $("#formQa1JnType").addClass("validate");
    //$("#formQa1JnRemark").addClass("validate");

    $("#formQa1JiType").addClass("validate");
    //$("#formQa1JiRemark").addClass("validate");
}

function addWCQSurfaceReg() {
    $("#formQa1SrevType").addClass("validate");
    //$("#formQa1SrEvenRemark").addClass("validate");

    $("#formQa1SruevType").addClass("validate");
    //$("#formQa1SrUnEvenRemark").addClass("validate");

    $("#formQa1SrprType").addClass("validate");
    //$("#formQa1SrPrRemark").addClass("validate");
}

function clearGcRemarks() {
    $("#formQa1WhsRemark").val("");
    $("#formQa1WhsReason").val("");
    $("#formQa1WisRemark").val("");
    $("#formQa1WisReason").val("");

    $("#formQa1WiusMatRemark").val("");
    $("#formQa1WiusMatReason").val("");

    $("#formQa1WiusEqpRemark").val("");
    $("#formQa1WiusEqpReason").val("");

    $("#formQa1WiusWrkRemark").val("");
    $("#formQa1WiusWrkReason").val("");

}