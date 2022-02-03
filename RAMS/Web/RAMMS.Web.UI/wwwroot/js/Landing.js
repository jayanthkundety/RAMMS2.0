$(document).ready(function () {
    $('#formADetSrchRMU').chosen();
    $('#formADetSrchSec').chosen();
    $('#formADetSrchRoadCode').chosen();
    $("#formADetSrchAsstGrp").chosen();
    $("#formADetSrchMonth").chosen();
    $("#formADetSrchYear").chosen();

    $("#formADetSrchSec").on("change", function () {
        
        var req = {};
        var _rmu = $("#formADetSrchRMU");
        var _sec = $("#formADetSrchSec");
        var _road = $("#formADetSrchRoadCode");
        var rmu = $("#formADetSrchRMU option:selected").text();
        if (rmu == "Select RMU") {
            req.RMU ="";
        }
        else {
            req.RMU = $("#formADetSrchRMU option:selected").text();
        }
        //req.RMU = $("#formADetSrchRMU option:selected").text();
        req.SectionCode = _sec.val();
        req.RdCode = "";
        if (req.Section != "") {
            var dsection = $("#formADetSrchSec option:selected").text();
            $("#formADetSrchSecName").val(dsection.split('-')[1]);
        }
        else {
            $("#formADetSrchSecName").val("");
        }

        
        $("#formADetSrchRoadName").val("");
        $.ajax({
            url: '/NOD/RMUSecRoad',
            dataType: 'JSON',
            data: req,
            type: 'Post',
            success: function (data) {
                
                if (req.RMU == "") {
                    $("#formADetSrchRMU").empty();
                    $("#formADetSrchRMU").append($("<option></option>").val("").html("Select RMU"));
                    $.each(data.rmu, function (index, v) {
                        $("#formADetSrchRMU").append($("<option></option>").val(v.value).html(v.text));
                    });
                }
                //if (req.Section == "") {
                //    $("#formADetSrchSec").empty();
                //    $("#formADetSrchSec").append($("<option></option>").val("").html("Select Section"));
                //    $.each(data.section, function (index, v) {
                //        $("#formADetSrchSec").append($("<option></option>").val(v.value).html(v.text));
                //    });
                //}
                //if (req.RoadCode == "") {
                //if(data.)
                $("#formADetSrchRoadCode").empty();
                $("#formADetSrchRoadCode").append($("<option></option>").val("").html("Select Road Code"));
                $.each(data.rdCode, function (index, v) {
                    $("#formADetSrchRoadCode").append($("<option></option>").val(v.value).html(v.text));
                });
                //}

                $('#formADetSrchRMU').trigger("chosen:updated");
                $('#formADetSrchSec').trigger("chosen:updated");
                $('#formADetSrchRoadCode').trigger("chosen:updated");
            },
            error: function (data) {

                console.error(data);
            }
        });
    });

    $("#formADetSrchRoadCode").on("change", function () {
        var data = $("#formADetSrchRoadCode option:selected").text();
        $("#formADetSrchRoadName").val(data.split('-')[1]);
        
        var req = {};
        var _rmu = $("#formADetSrchRMU");
        var _sec = $("#formADetSrchSec");
        var _road = $("#formADetSrchRoadCode");
        req.RMU = $("#formADetSrchRMU option:selected").text();
        if (req.RMU == "Select RMU") {
            req.RMU = '';
        }
        req.Section = $("#formADetSrchSec option:selected").text();
        if (req.Section == "Select Section") {
            req.Section = '';
        }
        req.RdCode = _road.val();// data;//$("#formADetSrchRoadCode option:selected").text();
      //  $("#formADetSrchRoadName").val("");
       // $("#formADetSrchSecName").val("");
        $.ajax({
            url: '/NOD/RMUSecRoad',
            dataType: 'JSON',
            data: req,
            type: 'Post',
            success: function (data) {
                console.log(data);
                
                if (req.RMU == "") {
                    $("#formADetSrchRMU").empty();
                    $("#formADetSrchRMU").append($("<option></option>").val("").html("Select RMU"));
                    $.each(data.rmu, function (index, v) {
                        $("#formADetSrchRMU").append($("<option></option>").val(v.value).html(v.text));
                    });
                }
                if (req.Section == "") {
                    $("#formADetSrchSec").empty();
                    $("#formADetSrchSec").append($("<option></option>").val("").html("Select Section"));
                    $.each(data.section, function (index, v) {
                        $("#formADetSrchSec").append($("<option></option>").val(v.value).html(v.text));
                    });
                }
                if (req.RdCode == "") {
                    $("#formADetSrchRoadCode").empty();
                    $("#formADetSrchRoadCode").append($("<option></option>").val("").html("Select Road Code"));
                    $.each(data.rdCode, function (index, v) {
                        $("#formADetSrchRoadCode").append($("<option></option>").val(v.value).html(v.text));
                    });
                }
                $('#formADetSrchRMU').trigger("chosen:updated");
                $('#formADetSrchSec').trigger("chosen:updated");
                $('#formADetSrchRoadCode').trigger("chosen:updated");
            },
            error: function (data) {

                console.error(data);
            }
        });


    });


    $("#formADetSrchRMU").on("change", function () {
     
        var req = {};
        var _rmu = $("#formADetSrchRMU");
        var _sec = $("#formADetSrchSec");
        var _road = $("#formADetSrchRoadCode");
        req.RMU = $("#formADetSrchRMU option:selected").text();
        if (req.RMU == "Select RMU") {
            req.RMU = '';
        }
        req.Section = '';
        req.RdCode = '';
        $("#formADetSrchRoadName").val("");
        $("#formADetSrchSecName").val("");
        $.ajax({
            url: '/NOD/RMUSecRoad',
            dataType: 'JSON',
            data: req,
            type: 'Post',
            success: function (data) {
                console.log(data);
                if (req.RMU == "") {
                    $("#formADetSrchRMU").empty();
                    $("#formADetSrchRMU").append($("<option></option>").val("").html("Select RMU"));
                    $.each(data.rmu, function (index, v) {
                        $("#formADetSrchRMU").append($("<option></option>").val(v.value).html(v.text));
                    });
                }
                //if (req.Section == "") {
                $("#formADetSrchSec").empty();
                $("#formADetSrchSec").append($("<option></option>").val("").html("Select Section"));
                $.each(data.section, function (index, v) {
                    $("#formADetSrchSec").append($("<option></option>").val(v.value).html(v.text));
                });
                //}
                //if (req.RoadCode == "") {
                $("#formADetSrchRoadCode").empty();
                $("#formADetSrchRoadCode").append($("<option></option>").val("").html("Select Road Code"));
                $.each(data.rdCode, function (index, v) {
                    $("#formADetSrchRoadCode").append($("<option></option>").val(v.value).html(v.text));
                });
                //}
                $('#formADetSrchSec').trigger("chosen:updated");
                $('#formADetSrchRoadCode').trigger("chosen:updated");
            },
            error: function (data) {

                console.error(data);
            }
        });
    });
});

function clearHeaderSearch() {
    
    $("#formADetSrchRMU").val("").trigger("chosen:updated");
    $('#formADetSrchRoadCode').val("").trigger("chosen:updated");
    $("#formADetSrchSec").val("").trigger("chosen:updated");
    $("#formADetSrchAsstGrp").val("").trigger("chosen:updated");
    $("#formADetSrchRoadName").val("");
    $("#formADetSrchSecName").val("");
    $("#FormASmartSearch").val("");
    $("#formADetSrchMonth").val("").trigger("chosen:updated");
    $("#formADetSrchYear").val("").trigger("chosen:updated");
    $("#formAFromKm").val("");
    $("#formAFromM").val("");
    $("#formAToKm").val("");
    $("#formAToM").val("");
    var req = {};
    var _rmu = $("#formADetSrchRMU");
    var _sec = $("#formADetSrchSec");
    var _road = $("#formADetSrchRoadCode");
    req.RMU = this.value;
    req.Section = '';
    req.RdCode = '';

    $.ajax({
        url: '/NOD/RMUSecRoad',
        dataType: 'JSON',
        data: req,
        type: 'Post',
        success: function (data) {
            
            //if (req.RMU == "") {
                $("#formADetSrchRMU").empty();
                $("#formADetSrchRMU").append($("<option></option>").val("").html("Select RMU"));
                $.each(data.rmu, function (index, v) {
                    $("#formADetSrchRMU").append($("<option></option>").val(v.value).html(v.text));
                });
           // }
            //if (req.Section == "") {
            $("#formADetSrchSec").empty();
            $("#formADetSrchSec").append($("<option></option>").val("").html("Select Section"));
            $.each(data.section, function (index, v) {
                $("#formADetSrchSec").append($("<option></option>").val(v.value).html(v.text));
            });
            //}
            //if (req.RoadCode == "") {
            $("#formADetSrchRoadCode").empty();
            $("#formADetSrchRoadCode").append($("<option></option>").val("").html("Select Road Code"));
            $.each(data.rdCode, function (index, v) {
                $("#formADetSrchRoadCode").append($("<option></option>").val(v.value).html(v.text));
            });
            //}
            $('#formADetSrchSec').trigger("chosen:updated");;
            $('#formADetSrchRoadCode').trigger("chosen:updated");
        },
        error: function (data) {

            console.error(data);
        }
    });
    $("#formASearchBtn").click();
}