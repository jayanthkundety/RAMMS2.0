$(document).ready(function () {
    $("#formXmod").chosen();
    $("#formXRepBy").chosen();
    $("#formXRMU").chosen();
    $("#FormXAttnto").chosen();
    $("#FormXVeriBy").chosen();
    $("#formXmainCode").chosen();
    $("#FormXsubCode").chosen();
    $("#FormXSVetby").chosen();
    $("#FormXAssgnto").chosen();
    $("#FormXSVerby").chosen();
    $("#FormXSVetby").chosen();
    $("#FormXRoadCode").chosen();
    $("#formXSrchType").chosen();
    $("#formXSrchSec").chosen();
    $("#formXSrchMainTask").chosen();
    $("#formXSrchSubTask").chosen();
    $("#formXRepBy").trigger("change")
    val = $("#FormXFddNo").val();
    if (val != null && val != undefined && val != "" && val != "0") {
        document.getElementById("btnUCUOpenModal").disabled = false;
        document.getElementById("btnWarOpenModal").disabled = false;
    }
    else {
        document.getElementById("btnUCUOpenModal").disabled = true;
        document.getElementById("btnWarOpenModal").disabled = true;

    }

    var fxh_pk_id = $("#FormXID").val();
    //if (fxh_pk_id == "" || fxh_pk_id == "0") {
    //    $("#saveFormXBtn").attr("disabled", "disabled");
    //}
    // $("#SiteRefmultiSelect").chosen();

    $('#formADetSrchRMU').chosen();
    $('#formADetSrchSec').chosen();
    $('#formADetSrchRoadCode').chosen();
    $("#formADetSrchAsstGrp").chosen();


    //$.ajax({
    //    url: '/NOD/RMUSecRoad',
    //    dataType: 'JSON',
    //    data: req,
    //    type: 'Post',
    //    success: function (data) {
    //        if (req.RoadCode == "") {
    //            $("#FormXRoadCode").empty();
    //            $("#FormXRoadCode").append($("<option></option>").val("").html("Select RoadCode"));
    //            $.each(data.roadCode, function (index, v) {
    //                $("#FormXRoadCode").append($("<option></option>").val(v.value).html(v.text));
    //            });

    //            $("#formReference").val('');
    //            $("#saveFormXBtn").prop("disabled", true);
    //        }

    //        $('#formXRMU').trigger("chosen:updated");
    //        $('#FormXRoadCode').trigger("chosen:updated");;
    //    },
    //    error: function (data) {

    //        console.error(data);
    //    }
    //});




    $("#formADetSrchRMU").on("change", function () {
        var req = {};
        var _rmu = $("#formADetSrchRMU");
        var _sec = $("#formADetSrchSec");
        var _road = $("#formADetSrchRoadCode");
        req.RMU = this.value;
        req.Section = '';
        req.RoadCode = '';

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
                $("#formADetSrchSec").empty();
                $("#formADetSrchSec").append($("<option></option>").val("").html("Select Section"));
                $.each(data.section, function (index, v) {
                    $("#formADetSrchSec").append($("<option></option>").val(v.value).html(v.text));
                });
                //}
                //if (req.RoadCode == "") {
                $("#formADetSrchRoadCode").empty();
                $("#formADetSrchRoadCode").append($("<option></option>").val("").html("Select RoadCode"));
                $.each(data.roadCode, function (index, v) {
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
    });
});



var saveFormADetList = new Array();

var tempHTTP;
function getHttp() {
    return tempHTTP;
}
//$(document).on("click", "#saveFormXBtn", function () {
//    debugger;
//    save(false);
//});

$(document).on("click", "#SubmitFormXBtn", function () {
    save(true);
});

function saveFormXBtn() {
    save(false);
}

function save(isSubmit) {
    var d = new Date();

    var month = d.getMonth() + 1;
    var day = d.getDate();

    var output = (('' + month).length < 2 ? '0' : '') + month + '/' +
        (('' + day).length < 2 ? '0' : '') + day + '/' + d.getFullYear();

    if (ValidatePage('#accordion')) {

        var saveObj = new Object;
        saveObj.SubmitSts = isSubmit;
        saveObj.No = $("#FormXID").val();
        var val = $("#formXRMU option:selected").text().split("-");
        saveObj.Rmu = val[1];
        saveObj.RoadCode = $("#FormXRoadCode").val();
        saveObj.RoadName = $("#FormxRoadName").val();
        saveObj.Section = $("#FormXDetsection").val();
        saveObj.FddNo = 102;
        saveObj.ModComType = $("#formXmod").val();
        saveObj.ModComDesc = $("#formXcomDesc").val();
        saveObj.ContNo = $("#formXContNo").val();
        //saveObj.ModComUpload = $("#ModComUpload").val();
        saveObj.Location = $("#formXRepBy").val();
        var val = $("#formXRepBy").find("option:selected").text();
        if (val.toLowerCase() == "others") {
            saveObj.LocRepDesc = $("#formXRepName").val();
        }
        else {
            saveObj.LocRepDesc = null;
        }
        saveObj.ReportedByUsername = $("#formXName").val();

        saveObj.Name = $("#formXName").val();

        saveObj.ModComUpload = $("#formXcomUpload").val();



        saveObj.DateReported = $("#formXrepDate").val();// + " " + $("#formXrepTime").val();
        //saveObj.Time = $("#formXrepDate").val();
        var d = new Date($("#formXrepDate").val())
        //var crtDate = (('' + month).length < 2 ? '0' : '') + month + '/' +
        //    (('' + day).length < 2 ? '0' : '') + day + '/' + d.getFullYear();
        //console.log("Date:" + saveObj.Date);

        saveObj.Date = d.getFullYear() + "-" + (('' + month).length < 2 ? '0' : '') + month + "-" + (('' + day).length < 2 ? '0' : '') + day ;

        console.log(saveObj.Date);
        saveObj.Time = d.getHours() + ":" + d.getMinutes(); //+ ":" + d.getSeconds();
        //saveObj.Date = d.getMonth() + "/" + d.getDay() + "/" + d.getFullYear();
        //console.log("Date:" + saveObj.Date);

        saveObj.WorkPhone = $("#formXWorkPh1").val();
        saveObj.HandPhone = $("#formXhandPh1").val();
        saveObj.EmailId = $("#formXemail").val();
        saveObj.Description = $("#FormXDescription").val();

        if ($("#FormXAttnto").val() != "") {
            val = $("#FormXAttnto option:selected").text().split('-');
            saveObj.UseridAttnTo = val[0];
            saveObj.AttentionTo = $("#FormXAttnUsername").val();
        }
        val = $("#FormXVeriBy option:selected").text().split('-');
        if (val.length > 0) {
            saveObj.UseridVer = val[0];
        }
        saveObj.UsernameVer = $("#FormXVerUsername").val();
        saveObj.DtVer = $("#formXverDate").val();

        saveObj.AssignWork = $("#FormXAssgWorkRef").val();
        saveObj.Comments = $("#FormXcomment").val();
        saveObj.DtJkrSent = $("#JkrsSentDt").val();
        saveObj.DtJkrRcvdFrm = $("#DtJkrRcvdFrm").val();
        saveObj.JkrRemarks = $("#formXRemarks").val();
        saveObj.StsJkr = $("#formXSts").val();
        var val = $("#formXmainCode option:selected").text().split("-");
        saveObj.ActMainCode = val[0];
        saveObj.ActMainName = $("#FormXActMainName").val();
        val = $("#FormXsubCode").find(":selected").text().split("-");
        saveObj.ActSubCode = val[0];
        saveObj.ActSubName = $("#FormXActSubName").val();

        saveObj.EstDays = $("#FormXEstDays").val();
        saveObj.EstDate = $("#FormXEstDate").val();

        val = $("#FormXAssgnto").find(":selected").text().split('-');
        saveObj.UseridAssgn = val[0];
        saveObj.UsernameAssgn = $("#FormXAssgnUsername").val();
        saveObj.DtAssgn = $("#formXassgnToDate").val();

        saveObj.WorkSc = $("#formXWrkSchDate").val();
        saveObj.WorkCompleted = $("#formXWrkCmpDate").val();

        val = $("#FormXSVerby").find(":selected").text().split('-')
        saveObj.UseridSchdVer = val[0];
        saveObj.UsernameSchdVer = $("#FormXSVerUsername").val();
        saveObj.DtSchdVer = $("#formXSchverDate").val();

        saveObj.CaseClosedOn = $("#formXCaseCloseDate").val();

        val = $("#FormXSVetby").find(":selected").text().split('-');
        saveObj.UseridVet = val[0];
        saveObj.UsernameVet = $("#FormXSVetUsername").val();
        saveObj.Remarks = $("#FormXRemark").val();

        saveObj.DtJkrSent = $("#JkrsSentDt").val();
        saveObj.DtJkrRcvdFrm = $("#DtJkrRcvdFrm").val();
        saveObj.JkrRemarks = $("#formXRemarks").val();
        //saveObj.DateReported = output
        // saveObj.ModifeidBy = $("#FormXSVerby").find(":selected").text();
        saveObj.ModifiedDate = output
        // saveObj.CreatedBy = $("#FormXSVerby").find(":selected").text();
        saveObj.CreatedDate = output
        saveObj.ActiveYn = true;
        saveObj.ReferenceId = $("#formReference").val();

        $.ajax({
            url: '/ERT/Save',
            data: saveObj,
            type: 'POST',
            success: function (data) {
                RefNO = data;
                var input = document.getElementById('comUpload');
                if (input.files.length > 0) {
                    var formData = new FormData();
                    formData.append("FormFile", input.files[0])
                    formData.append("RefNO", data)
                    $("#FormXID").val(data);
                    saveObj.No = data;
                    $.ajax({
                        url: '/ERT/ChooseFile',
                        data: formData,
                        processData: false,
                        contentType: false,
                        type: 'POST',
                        success: function (data) {
                            var fileName = data.fileName;
                            if (data.filePath != "") {

                                saveObj.ModComUpload = data.filePath;

                            }
                            $.ajax({
                                url: '/ERT/Save',
                                data: saveObj,
                                dataType: 'JSON',
                                type: 'POST',
                                success: function (data) {
                                    formData.append("RefNO", data)


                                },
                                error: function (data) {
                                    app.ShowErrorMessage(data.responseText, false);
                                }

                            });
                        },
                        error: function (data) {
                            app.ShowErrorMessage(data.responseText, false);
                        }
                    });
                }
                $("#FormXFddNo").val(RefNO);
                $("#formXUCUId").val(RefNO);
                $("#formXWarNo").val(RefNO);
                document.getElementById("btnUCUOpenModal").disabled = false;
                document.getElementById("btnWarOpenModal").disabled = false;
                app.ShowSuccessMessage($("#formReference").val() + ' Successfully Saved');
            }
        });
    }
}

function openPhoto() {
    id = $("#FormXFddNo").val();
    $.ajax({
        url: '/ERT/GetImageList',
        data: { id: id, location: window.location.origin },
        type: 'POST',
        success: function (data) {
            $("#div-WarPhoto").html(data);
            $("#WarphotoType").chosen();
            document.getElementById("files").disabled = true;
            $("#files1").addClass("disabled");
            $("#formXWarNo").val(id);
            // document.getElementById("btnWarImageBrowe").disabled = true;
            document.getElementById("btnWarImageUpload").disabled = true;
        },
        error: function (data) {
            alert(data.responseText);
        }
    });
}

function openUSeeU() {
    id = $("#FormXFddNo").val();
    $.ajax({
        url: '/ERT/GetUSeeUPage',
        data: { id: id },
        type: 'POST',
        success: function (data) {
            $("#div-USeeUModal").html(data);
            $("#UCUphotoType").chosen();
            //document.getElementById("btnUCUDocBrowse").disabled = true;
            document.getElementById("files").disabled = true;
            $("#filesdiv").addClass("disabled");
            document.getElementById("btnUCUDocUpload").disabled = true;
            $("#formXUCUId").val(id);
        },
        error: function (data) {
            alert(data.responseText);
        }
    });
}
function CloseWarModal() {
    $("#WarPhotoModal").modal('hide');
    $("#photolist").empty();
}


function CloseUSeeUModal() {
    $("#USeeUModal").modal('hide');
    $("#photolist").empty();
}

function CloseDownload() {
    $("#DownloadPdfModal").modal('hide')
}

function DeleteImage(id, type) {
    if (app.Confirm("Are you sure you want to delete the record?", function (e) {
        if (e) {
            InitAjaxLoading();
            $.ajax({
                url: '/ERT/DeleteImage',
                data: { id: id, type: type },
                type: 'POST',
                success: function (data) {
                    HideAjaxLoading();
                    if (data > 0) {

                        app.ShowSuccessMessage(' Successfully Deleted ', false);
                        if (type == 'War') {
                            openPhoto();
                        }
                        else if (type == 'UCU') { openUSeeU() }
                    }
                    else {
                        alert("Error in Deleted. Kindly retry later.");
                        $("body").removeClass("loading");
                    }

                }
            });
        }
    }));
}

$("#formXRepBy").on("change", function () {
    $("#formXRepNamediv").hide();
    var val = $("#formXRepBy").find("option:selected").text();
    if (val.toLowerCase() == "others") {
        $("#formXRepNamediv").show();
    }
    else {
        $("#formXRepName").val(null);
    }
})