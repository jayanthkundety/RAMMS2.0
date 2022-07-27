


$(document).ready(function () {

    DisableHeader();


    if ($("#FormT_PkRefNo").val() == 0) {
        $("#btnDtlModal").hide();
        $("#saveFormTBtn").hide();
        $("#SubmitFormTBtn").hide();
        
    }

    if ($("#FormT_Status").val() == "Approved")
        $("#btnDtlModal").hide();


    if ($("#hdnView").val() == 1) {
        $("#saveFormTBtn").hide();
        $("#SubmitFormTBtn").hide();
        $("#btnDtlModal").hide();
    }


    $('.editable').on('click', function (e) {
        var $this = $(this);
        typein($this);
        e.preventDefault();
    });



    $("#ddlRecordedby").on("change", function () {
        var value = this.value;

        if (value == "") {
            $("#FormT_UsernameRcd").val('');
            $("#FormT_UsernameRcd").attr("readonly", "true");
        }
        else if (value == "99999999") {
            $("#FormT_UsernameRcd").val('');
            $("#FormT_UsernameRcd").removeAttr("readonly");
        }
        else {
            getUserDetail(value, function (data) {
                $("#FormT_UsernameRcd").val(data.userName);
                $("#FormT_DesignationRcd").val(data.position);
                $("#FormT_DesignationRcd").attr("readonly", "true");

            });
        }
    });
    $("#ddlRecordedby").trigger('change');

    $("#ddlHeadedby").on("change", function () {
        var value = this.value;

        if (value == "") {
            $("#FormT_UsernameHdd").val('');
            $("#FormT_UsernameHdd").attr("readonly", "true");
        }
        else if (value == "99999999") {
            $("#FormT_UsernameHdd").val('');
            $("#FormT_UsernameHdd").removeAttr("readonly");
        }
        else {
            getUserDetail(value, function (data) {
                $("#FormT_UsernameHdd").val(data.userName);
                $("#FormT_DesignationHdd").val(data.position);
                $("#FormT_DesignationHdd").attr("readonly", "true");

            });
        }
    });
    $("#ddlHeadedby").trigger('change');




    $("#ddlRMU").on("change", function () {
        // 

        $("#FormT_RoadLength").val("");

        if (this.value == "") {
            $("#FormT_PkRefId").val("");
            $("#FormT_RoadLength").val("");
            $('#ddlSection').val("");
            $('#ddlRMU').trigger("chosen:updated");
            $('#ddlSection').trigger("chosen:updated");
            $('#ddlRoadCode').val("").trigger("chosen:updated");
            $("#FormT_RdName").val("");
            $("#FormT_SecName").val("");
            // bindRMU();
            bindSection();
            bindRoadCode();
        }
        else {
            bindSection();
            bindRoadCode();
        }
    });

    $("#ddlSection").on("change", function () {

        $("#FormT_RoadLength").val("");
        bindRoadCode();
        if (this.value == "") {
            $("#FormT_SecName").val("");
            $("#FormT_PkRefId").val("");
            $("#FormT_RoadLength").val("");
        }
        else {
            $("#FormT_SecName").val($("#ddlSection").find("option:selected").text().split("-")[1]);

        }
    });



    $("#ddlYear").on("change", function () {
        generateHeaderReference();
    });

    $("#ddlRoadCode").on("change", function () {


        var value = this.value;
        if (value != "") {
            bindRoadLength(value);

            bindRoadDetail(value, function (data) {

                $("#FormT_RdName").val(data.roadName);
            });
            generateHeaderReference();


        }
        else {
            $("#FormT_RdName").val("");
            $("#FormT_PkRefId").val("");
            $("#FormT_RoadLength").val("");

        }
    });



});


function getUserDetail(id, callback) {
    var req = {};
    req.id = id;
    $.ajax({
        url: '/NOD/GetUserById',
        dataType: 'JSON',
        data: req,
        type: 'Post',
        success: function (data) {
            callback(data);
        },
        error: function (data) {
            console.error(data);
        }
    });
}

function bindRMU(callback) {
    //
    var req = {};
    req.RMU = ''
    req.Section = '';
    req.RdCode = '';
    req.GrpCode = "GR"
    $("#txtRmu").val("");
    $("#FormT_SecName").val("");
    $("#FormT_RdName").val("");

    $.ajax({
        url: '/FormF2/RMUSecRoad',
        dataType: 'JSON',
        data: req,
        type: 'Post',
        success: function (data) {
            // 
            $('#ddlRMU').empty();
            $('#ddlRMU').append($("<option></option>").val("").html("Select RMU"));
            $.each(data.rmu, function (index, v) {
                $('#ddlRMU').append($("<option></option>").val(v.value).html(v.text));
            });
            $('#ddlRMU').trigger("chosen:updated");

            if (callback)
                callback();
        },
        error: function (data) {

            console.error(data);
        }
    });
}

function bindSection(callback) {


    // 
    var req = {};
    var _rmu = $("#ddlRMU");
    var _sec = $("#ddlSection");
    var _road = $("#ddlRoadCode");
    req.RMU = $('#ddlRMU').val();
    req.SectionCode = '';
    req.RdCode = '';
    req.GrpCode = "GR"
    $("#FormT_RdName").val("");
    $("#FormT_SecName").val("");

    $.ajax({
        url: '/FormF2/RMUSecRoad',
        dataType: 'JSON',
        data: req,
        type: 'Post',
        success: function (data) {
            // 
            _sec.empty();
            _sec.append($("<option></option>").val("").html("Select Section Code"));
            $.each(data.section, function (index, v) {
                _sec.append($("<option></option>").val(v.code).html(v.text).attr("code", v.code).attr("text", v.value));
            });

            if ($("#hdnSecCode").val() != "" && $("#hdnSecCode").val() != undefined) {
                $("#ddlSection").val($("#hdnSecCode").val());
                $("#hdnSecCode").val("");
            }

            _sec.trigger("chosen:updated");
            _sec.trigger("change");
            if (callback)
                callback();
        },
        error: function (data) {

            console.error(data);
        }
    });
}


function bindRoadCode(callback) {

    var req = {};
    var _road = $("#ddlRoadCode");
    req.RMU = $('#ddlRMU').val();
    req.SectionCode = $('#ddlSection').val();
    req.RdCode = '';
    req.GrpCode = "GR"
    $("#FormT_RdName").val("");

    $.ajax({
        url: '/FormF2/RMUSecRoad',
        dataType: 'JSON',
        data: req,
        type: 'Post',
        success: function (data) {

            _road.empty();
            _road.append($("<option></option>").val("").html("Select Road Code"));
            $.each(data.rdCode, function (index, v) {
                _road.append($("<option></option>").val(v.value).html(v.text));
                // _road.append($("<option></option>").val(v.value).html(v.text).attr("Item1", v.item1).attr("Item3", v.item3).attr("PKId", v.pkId).attr("code", v.code));
            });

            if ($("#hdnRdCode").val() != "" && $("#hdnRdCode").val() != undefined) {
                $("#ddlRoadCode").val($("#hdnRdCode").val());
                $("#hdnRdCode").val("");
            }
            _road.trigger("chosen:updated");
            _road.trigger("change");
            if (callback)
                callback();
        },
        error: function (data) {

            console.error(data);
        }
    });
}

function bindRoadLength(code, callback) {

    var req = {};
    req.roadcode = code;
    $.ajax({
        url: '/FormF2/GetRoadLength',
        dataType: 'JSON',
        data: req,
        type: 'Post',
        success: function (data) {
            $("#FormT_RoadLength").val(data);
            if (callback)
                callback(data);
        },
        error: function (data) {

            console.error(data);
        }
    });
}

function bindRoadDetail(code, callback) {
    var req = {};

    req.code = code;
    $.ajax({
        url: '/FormF2/GetRoadDetailByCode',
        dataType: 'JSON',
        data: req,
        type: 'Post',
        success: function (data) {

            $('#ddlDivision').val(data.divisionCode);
            $('#ddlDivision').trigger("chosen:updated");
            $('#ddlRMU').val(data.rmuCode);
            $('#ddlRMU').trigger("chosen:updated");
            $('#ddlSection').val(data.secCode);
            $('#ddlSection').trigger("chosen:updated");
            $('#FormT_SecName').val(data.secName);

            if (callback)
                callback(data);
        },
        error: function (data) {

            console.error(data);
        }
    });
}



function generateHeaderReference() {
    if ($('#ddlRoadCode').val() != "" && $('#FormT_InspectionDate').val() != "") {
        //var roadcode = $('#ddlRoadCode').find(":selected").text().split('-')[0].trim();
        var v = $("#ddlRoadCode").find(":selected").text().split('-');
        if (v.length > 2) {
            var roadcode = v[0] + '-' + v[1];
        }
        else {
            var roadcode = v[0];
        }

        var d = new Date($('#FormT_InspectionDate').val());
        var day = d.getDate();
        var month = d.getMonth() + 1;
        var year = d.getFullYear();
        if (day < 10) {
            day = "0" + day;
        }
        if (month < 10) {
            month = "0" + month;
        }
        var date = year + month + day;

        $("#FormT_PkRefId").val(("CI/Form T/" + roadcode + "/" + date));
    }
    else {
        $("#FormT_PkRefId").val("");
    }
}

function CalToTime() {

    if ($('#FormTDtl_AuditTimeFrm').timeEntry('getTime') != null) {
        var time = $('#FormTDtl_AuditTimeFrm').timeEntry('getTime');
        $('#FormTDtl_AuditTimeTo').timeEntry('setTime', new Date(0, 0, 0, time.getHours() + 1, time.getMinutes(), 0));
    }
}



function Save(GroupName, SubmitType) {


    if (SubmitType == "Submitted") {
        $("#FormT_SubmitSts").val(true);
        $("#ddlCrew").addClass("validate");
    }

    if (ValidatePage('#headerFindDiv')) {

        if ($("#FormT_Status").val() == "")
            $("#FormT_Status").val("Initialize");
        else if ($("#FormT_Status").val() == "Initialize")
            $("#FormT_Status").val("Saved");

        InitAjaxLoading();
        EnableDisableElements(false);
        $.get('/FrmT/SaveFormT', $("form").serialize(), function (data) {
            EnableDisableElements(true)
            HideAjaxLoading();
            if (data == -1) {
                app.ShowErrorMessage(data.errorMessage);
            }
            else {

                if (SubmitType == "") {
                    if (data.formExist) {
                        location.href = "/FrmT/Add?Id=" + data.pkRefNo + "&view=0";
                        return;
                    }
                    else {
                        UpdateFormAfterSave(data);
                    }

                }
                else if (SubmitType == "Saved") {
                    app.ShowSuccessMessage('Saved Successfully', false);
                    location.href = "/FrmT/Index";
                }
                else if (SubmitType == "Submitted") {
                    app.ShowSuccessMessage('Submitted Successfully', false);
                    location.href = "/FrmT/Index";
                }
                else if (SubmitType == "Verified") {
                    process.ShowApprove(GroupName, SubmitType);
                }
            }
        });
    }

}

function SaveFormTDtl() {

    var failed = false;

    if ($("#FormTDtl_TotalDay").val() != "" && $("#FormTDtl_Day").val() != "") {

        if (parseInt($("#FormTDtl_TotalDay").val()) < 0) {
            app.ShowErrorMessage("Total day should not be negative");
            failed = true;
        }
        if ($("#FormTDtl_Day").val() < 0) {
            app.ShowErrorMessage("Day should not be negative");
            failed = true;
        }
        if ($("#FormTDtl_Day").val() > $("#FormTDtl_TotalDay").val()) {
            app.ShowErrorMessage("Day should not be greater than total day");
            failed = true;
        }


    }

    if (parseInt($("#FormTDtl_HourlyCountPerDay").val()) < 0) {
        app.ShowErrorMessage("Hourly Count Per Day should not be negative");
        failed = true;
    }
 
    if (parseInt($("#FormTDtl_HourlyCountPerDay").val()) > 9999) {
        app.ShowErrorMessage("Hourly Count Per Day should be less than or equal to 4 digits");
        failed = true;
    }

    if (failed)
        return;

    //  if (ValidatePage('#myModal')) {
    InitAjaxLoading();

    var FormTDtl = new Object();
    FormTDtl.PkRefNo = $("#FormTDtl_PkRefNo").val()
    FormTDtl.FmtPkRefNo = $("#FormT_PkRefNo").val()
    FormTDtl.InspectionDate = $("#FormTDtl_InspectionDate").val()
    FormTDtl.AuditTimeFrm = $("#FormTDtl_AuditTimeFrm").val();
    FormTDtl.AuditTimeTo = $("#FormTDtl_AuditTimeTo").val();
    FormTDtl.DirectionFrm = $("#FormTDtl_DirectionFrm").val();
    FormTDtl.DirectionTo = $("#FormTDtl_DirectionTo").val();
    FormTDtl.HourlyCountPerDay = $("#FormTDtl_HourlyCountPerDay").val();
    FormTDtl.Day = $("#FormTDtl_Day").val();
    FormTDtl.TotalDay = $("#FormTDtl_TotalDay").val();
    FormTDtl.Description = $("#FormTDtl_Description").val();
    FormTDtl.DescriptionPC = $("#FormTDtl_DescriptionPC").val();
    FormTDtl.DescriptionHV = $("#FormTDtl_DescriptionHV").val();
    FormTDtl.DescriptionMC = $("#FormTDtl_DescriptionMC").val();

    var Vechicles = []

    $('.tblPC > tbody  > tr').each(function (index, tr) {
        $(tr).find('td').each(function (index, td) {
            if (index != 0) {
                if ($(td).html().trim() != "") {
                    var Vechicle = new Object();
                    Vechicle.FmtdiPkRefNo = $("#FormTDtl_PkRefNo").val();
                    Vechicle.VechicleType = "PC";
                    Vechicle.Axle = "";
                    Vechicle.Loading = "";
                    Vechicle.Time = index * 5;
                    Vechicle.Count = $(td).html().trim();
                    Vechicles.push(Vechicle);
                }
            }
        });
    });

    var Axle;
    var Load;
    $('.tblHV > tbody  > tr').each(function (i, tr) {
        $(tr).find('td').each(function (index, td) {
            if (index == 0 && (i == 0 || (i % 3) == 0)) {
                return;
            }
            if (index == 1 && (i == 0 || (i % 3) == 0)) {
                Axle = $(td).html().trim();
                return;
            }
            if (index == 2 && (i == 0 || (i % 3) == 0)) {
                Load = $(td).html().trim();
                return;
            }
            if (index == 0) {
                Load = $(td).html().trim();
                return;
            }

            if ($(td).html().trim() != "") {
                var Time;
                if (i == 0 || (i % 3) == 0) {
                    Time = (index - 2) * 5;
                }
                else {
                    Time = index * 5;
                }
                var Vechicle = new Object();
                Vechicle.FmtdiPkRefNo = $("#FormTDtl_PkRefNo").val();
                Vechicle.VechicleType = "HV";
                Vechicle.Axle = Axle;
                Vechicle.Loading = Load;
                Vechicle.Time = Time;
                Vechicle.Count = $(td).html().trim();
                Vechicles.push(Vechicle);
            }


        });
    });

    $('.tblMC > tbody  > tr').each(function (i, tr) {
        $(tr).find('td').each(function (index, td) {
            if (index != 0) {
                if ($(td).html().trim() != "") {
                    var Vechicle = new Object();
                    Vechicle.FmtdiPkRefNo = $("#FormTDtl_PkRefNo").val();
                    Vechicle.VechicleType = "MC";
                    Vechicle.Axle = "";
                    Vechicle.Loading = "";
                    Vechicle.Time = index * 5;
                    Vechicle.Count = $(td).html().trim();
                    Vechicles.push(Vechicle);
                }
            }
        });
    });

    FormTDtl.Vechicles = Vechicles;

    $.ajax({
        url: '/FrmT/SaveFormTDtl',
        data: FormTDtl,
        type: 'POST',
        success: function (data) {
            HideAjaxLoading();
            if (data == -1) {
                app.ShowErrorMessage(data.errorMessage);
            }
            else {
                ClearFormTDtl()

                InitializeGrid();
                app.ShowSuccessMessage('Saved Successfully', false);
            }
        }
    });



    // }
}

function NewFormTDtl() {
     

    $.get('/FrmT/GetFormTDtlById?id=' + 0, function (data) {

        $('#divVechicleDetails').html(data).promise().done(function () {

            $('.editable').on('click', function (e) {
                var $this = $(this);
                typein($this);
                e.preventDefault();
            });

            if ($("#hdnView").val() == 1 || view == 1) {

                $("#FormTDtl_Description").attr("readonly", true);
                $("#btnSaveFormTDtl").hide();
            }
            else {

                $("#FormTDtl_Description").attr("readonly", false);
                $("#btnSaveFormTDtl").show();
            }
        });

    });




}


function EditFormTDtl(obj, view) {


    EditModeDtl = true;
    var currentRow = $(obj).closest("tr");
    var data = $('#FormTDtlGridView').DataTable().row(currentRow).data();

    $("#FormTDtl_PkRefNo").val(data.pkRefNo);

    $.get('/FrmT/GetFormTDtlById?id=' + data.pkRefNo, function (data) {

        $('#divVechicleDetails').html(data).promise().done(function () {

            $('.editable').on('click', function (e) {
                var $this = $(this);
                typein($this);
                e.preventDefault();
            });

            if ($("#hdnView").val() == 1 || view == 1) {

                $("#FormTDtl_Description").attr("readonly", true);
                $("#btnSaveFormTDtl").hide();
            }
            else {

                $("#FormTDtl_Description").attr("readonly", false);
                $("#btnSaveFormTDtl").show();
            }
        });

    });




}

function ClearFormTDtl() {

    $("#FormTDtl_Description").val('');
    $("#myModal").modal("hide");
}


function UpdateFormAfterSave(data) {

    $("#FormT_PkRefNo").val(data.pkRefNo);
    $("#FormT_PkRefId").val(data.refId);
    $("#FormT_Status").val(data.status)

    $("#hdnPkRefNo").val(data.pkRefNo);

    $("#btnDtlModal").show();
    $("#saveFormTBtn").show();
    $("#SubmitFormTBtn").show();


    DisableHeader();
    InitializeGrid();
}




function DisableHeader() {

    if ($("#FormT_PkRefNo").val() != "0") {
        $("#headerDiv * > select").attr('disabled', true).trigger("chosen:updated");

        $("#FormT_Dist").attr("readonly", "true");
        $("#FormT_InspectionDate").attr("readonly", "true");
        $("#FormT_ReferenceNo").attr("readonly", "true");
        $("#btnFindDetails").hide();

    }

}

function EnableDisableElements(state) {

    $('#headerDiv * > select').prop('disabled', state).trigger("chosen:updated");

}

function DeleteFormTDtl(id) {

    InitAjaxLoading();
    $.post('/FrmT/DeleteFormTDtl?id=' + id, function (data) {
        HideAjaxLoading();
        if (data == -1) {
            app.ShowErrorMessage(data.errorMessage);
        }
        else {
            InitializeGrid();
            app.ShowSuccessMessage('Deleted Successfully', false);
        }
    });
}


function GoBack() {
    if ($("#hdnView").val() == "0") {
        if (app.Confirm("Unsaved changes will be lost. Are you sure you want to cancel?", function (e) {
            if (e) {
                location.href = "/FrmT";

            }
        }));
    }
    else
        location.href = "/FrmT";
}


function typein($this) {

    if ($this[0].childElementCount > 0)
        return;

    var width = ($($this).css('max-width').substr(0, 3)) - 10;

    if (isNaN(width)) {
        width = ($($this).css('width').substr(0, 3)) - 10;
    }

    var $input = $('<input>', {
        value: $($this).html().trim(),
        type: 'text',
        width: width,
        blur: function () {

            if (isNaN($($input).val())) {
                $($input).parent().html("");
                app.ShowErrorMessage("Please enter numeric values");
            }
            else {

                $($input).parent().html($($input).val());
            }
            CalSubTotal();
        },
        keyup: function (e) {
            if (e.which === 13) {
                // saveEdit(event, $input);
                $input.blur();
            };

        }
    }).appendTo($this.empty()).focus();

    SetCaretAtEnd($input[0]);
}


function CalSubTotal() {
    var t5 = 0, t10 = 0, t15 = 0, t20 = 0, t25 = 0, t30 = 0, t35 = 0, t40 = 0, t45 = 0, t50 = 0, t55 = 0, t60 = 0;
    $('.tblHV > tbody  > tr').each(function (i, tr) {
        $(tr).find('td').each(function (index, td) {
            if (index == 0 && (i == 0 || (i % 3) == 0)) {
                return;
            }
            if (index == 1 && (i == 0 || (i % 3) == 0)) {
                Axle = $(td).html().trim();
                return;
            }
            if (index == 2 && (i == 0 || (i % 3) == 0)) {
                Load = $(td).html().trim();
                return;
            }
            if (index == 0) {
                Load = $(td).html().trim();
                return;
            }

            if ($(td).html().trim() != "") {
                var Time;
                if (i == 0 || (i % 3) == 0) {
                    Time = (index - 2) * 5;
                }
                else {
                    Time = index * 5;
                }

                if (Time == 5) {
                    t5 = t5 + parseInt($(td).html().trim());
                }
                else if (Time == 10) {
                    t10 = t10 + parseInt($(td).html().trim());
                }
                else if (Time == 15) {
                    t15 = t15 + parseInt($(td).html().trim());
                }
                else if (Time == 20) {
                    t20 = t20 + parseInt($(td).html().trim());
                }
                else if (Time == 25) {
                    t25 = t25 + parseInt($(td).html().trim());
                }
                else if (Time == 30) {
                    t30 = t30 + parseInt($(td).html().trim());
                }
                else if (Time == 35) {
                    t35 = t35 + parseInt($(td).html().trim());
                }
                else if (Time == 40) {
                    t40 = t40 + parseInt($(td).html().trim());
                }
                else if (Time == 45) {
                    t45 = t45 + parseInt($(td).html().trim());
                }
                else if (Time == 50) {
                    t50 = t50 + parseInt($(td).html().trim());
                }
                else if (Time == 55) {
                    t55 = t55 + parseInt($(td).html().trim());
                }
                else if (Time == 60) {
                    t60 = t60 + parseInt($(td).html().trim());
                }
            }


        });
    });


    $(".tblHV > tfoot  > tr > th:nth-child(4)").html(t5)
    $(".tblHV > tfoot  > tr > th:nth-child(5)").html(t10)
    $(".tblHV > tfoot  > tr > th:nth-child(6)").html(t15)
    $(".tblHV > tfoot  > tr > th:nth-child(7)").html(t20)
    $(".tblHV > tfoot  > tr > th:nth-child(8)").html(t25)
    $(".tblHV > tfoot  > tr > th:nth-child(9)").html(t30)
    $(".tblHV > tfoot  > tr > th:nth-child(10)").html(t35)
    $(".tblHV > tfoot  > tr > th:nth-child(11)").html(t40)
    $(".tblHV > tfoot  > tr > th:nth-child(12)").html(t45)
    $(".tblHV > tfoot  > tr > th:nth-child(13)").html(t50)
    $(".tblHV > tfoot  > tr > th:nth-child(14)").html(t55)
    $(".tblHV > tfoot  > tr > th:nth-child(15)").html(t60)

}

function SetCaretAtEnd(elem) {
    var elemLen = elem.value.length;
    // For IE Only
    if (document.selection) {
        // Set focus
        elem.focus();
        // Use IE Ranges
        var oSel = document.selection.createRange();
        // Reset position to 0 & then set at end
        oSel.moveStart('character', -elemLen);
        oSel.moveStart('character', elemLen);
        oSel.moveEnd('character', 0);
        oSel.select();
    }
    else if (elem.selectionStart || elem.selectionStart == '0') {
        // Firefox/Chrome
        elem.selectionStart = elemLen;
        elem.selectionEnd = elemLen;
        elem.focus();
    } // if
}

