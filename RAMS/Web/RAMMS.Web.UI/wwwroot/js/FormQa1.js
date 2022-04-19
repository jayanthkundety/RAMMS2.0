var currentDate = new Date();
$(document).ready(function () {
    currentDate = formatDate(currentDate);
    var val = $("#formQaPkRefNo").val();

    if (val != 0 && val != undefined && val != "") {
        gridAddBtnDis()

    }
    else {
        $("#saveFormQa1Btn").hide();
        $("#SubmitFormQa1Btn").hide();
        document.getElementById("btnEquipAdd").disabled = true;
        document.getElementById("btnGenAdd").disabled = true;
        document.getElementById("btnMaterialAdd").disabled = true;
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

    $('#formQa1Dt').change(function () {
        SetWeekDayYear($('#formQa1Dt').val());
    });

});

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
                    data: FormValueCollection("#FormV2Headers"),
                    type: 'POST',
                    success: function (data) {
                        createV2(data);
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
    $("#formQa1WeekNo").val(GetWeekNumber(date));

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
    $("#formQa1Day").val(n);

    //Set Year
    $("#formQa1Year").val(todaydate.getFullYear());
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

