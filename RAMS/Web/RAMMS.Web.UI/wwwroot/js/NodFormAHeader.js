$('.close', "#FormAAdddetailsModal").click(function () {
    $('#FormAAdddetailsModal').modal('hide');
});

function GridReload() {
    oTable = $('#FormADetailGridView').DataTable();
    //oTable.columns(0).search($("#assetTypeCode").val().trim());
    //oTable.columns(1).search($("#AssetssSmartSearch").val().trim());
    //oTable.columns(2).search($("#FromCh").val().trim());
    //oTable.columns(3).search($("#ToCh").val().trim());
    //oTable.columns(4).search($("#SectionName").val().trim());
    //        oTable.columns(5).search($("#RoadName").val().trim());
    //        oTable.columns(6).search($("#AssetGroup").val().trim());
    //        oTable.columns(7).search($("#AssetType").val().trim());
    //        oTable.columns(8).search($("#RoadCode").val().trim());
    //        oTable.columns(9).search($("#RMU").val().trim());
    //    oTable.columns(10).search($("#Bound").val().trim());
    //   filterData.FromCh = 100;
    oTable.data = filterData;
    oTable.draw();
}

function NODHdrSaveClick() {

    var saveObj = new Object;
    saveObj.RoadCode = $("#FormADetRoadCode").val();
    saveObj.RoadName = $("#FormADetRoadName").val();
    saveObj.Rmu = $("#FormADetRmu").val();
    saveObj.Month = $("#FormADetMonth").val();
    saveObj.Year = $("#FormADetYear").val();
    saveObj.AssetGroupCode = $("#FormADetAssetGrpCode").val();
    saveObj.Id = $("#FormADetReferenceNO").val();
    //console.log(saveObj);
    if (saveObj.Id == "") {
        alert("Please fill all fields");
    }
    else {
        $.ajax({
            url: '/NOD/HeaderSave',
            type: 'POST',
            data: saveObj,
            success: function (data) {
                $("#FormADetPKId").val(data.no);
                $("#FormADetAssetGrpCode").val(data.assetGroupCode);
                $("#InspectedName").val(data.usernamePrp);
                $("#InspectedDesignName").val(data.designationPrp);
                var assignFormat = "YYYY-MM-DD";
                var date = new Date();
                if (data.dtPrp != null) {
                    date = new Date(data.dtPrp);
                    $('#InspectedDate').val(date.ToString(assignFormat));
                }
                else {
                    $('#InspectedDate').val(data.dtPrp)
                }
                $("#VerifiedName").val(data.usernameVer);
                $("#VerifiedDesignation").val(data.designationVer);
                if (data.verifiedDt != null) {
                    date = new Date(data.verifiedDt);
                    $("#VerifiedDate").val(date.ToString(assignFormat));
                }
                else {
                    $("#VerifiedDate").val(data.verifiedDt);
                }
                
                $("#ddlInspectedby").val(data.useridPrp).trigger("chosen:updated");
                $("#ddlVerifiedby").val(data.useridVer).trigger("chosen:updated");
                $("#addFADBtn").show();
                if (data.submitSts) {
                    $("#saveFormABtn").hide();
                    $('#SubmitFormABtn').hide();
                  
                }
                else {
                    $("#saveFormABtn").show();
                    $('#SubmitFormABtn').show();
                }

                if (data.no != null) {
                    $("#searchHeaderBtn").hide();
                    $("#FormADetAssetGrpCode").prop("disabled", true).trigger("chosen:updated");
                    $("#FormADetRoadCode").prop("disabled", true).trigger("chosen:updated");
                    $("#FormADetAssetGrpCode").prop("disabled", true).trigger("chosen:updated");
                    $("#FormADetMonth").prop("disabled", true).trigger("chosen:updated");
                    $("#FormADetYear").prop("disabled", true).trigger("chosen:updated");
                    $("#divDetails").show();
                }
                InitializeGrid(data.no);

            },
            error: function (data) {
                $("body").removeClass("loading");
            }
        });
    }
}

$(document).ready(function () {


    $("#searchHeaderBtn").click(function () {
        console.log('triggered Finddetail');
        NODHdrSaveClick();
    });

    localStorage.setItem("headeridvalue", "");
    // detailedlistgrid();
    //  AddNODClick();
    $("#ddlInspectedby").chosen();
    $("#ddlVerifiedby").chosen();
    $("#FormADetRoadCode").chosen();
    $("#FormADetAssetGrpCode").chosen();
    $("#FormADetMonth").chosen();
    $("#FormADetYear").chosen();
    $("#FormADetRoadCode").on("change", function () {
        $("#FormADetRoadName").val("");
        $("#FormADetRmu").val("");
        $("#FormADetsection").val("");
        $.ajax({
            url: '/NOD/GetAssetDataByRoadCode',
            dataType: 'JSON',
            data: { roadCode: $("#FormADetRoadCode").val() },
            type: 'Post',
            success: function (data) {
                //console.log(data);
                if (data != null) {
                    $("#FormADetRoadName").val(data.roadName);
                    $("#FormADetRmu").val(data.rmuCode);
                    $("#FormADetsection").val(data.secName);
                }
            },
            error: function (data) {

                console.error(data);
            }
        });
    })

    $("#FormADetAssetGrpCode").on("change", function () {
        var value = this.value;
        if (value != "") {
            $.ajax({
                url: '/NOD/GetReferenceNOData',
                dataType: 'JSON',
                data: { roadCode: $("#FormADetRoadCode").val(), year: $("#FormADetYear").val(), month: $("#FormADetMonth").val(), assetGroup: value },
                type: 'Post',
                success: function (data) {

                    //console.log(data);
                    if (data != null) {
                        $("#FormADetReferenceNO").val(data);
                    }
                },
                error: function (data) {

                    console.error(data);
                }
            });
        }
        else {
            console.log("Asset group code not passed on change");
        }
    })

    $("#FormADetRoadCode").on("change", function () {

        $.ajax({
            url: '/NOD/GetReferenceNOData',
            dataType: 'JSON',
            data: { roadCode: $("#FormADetRoadCode").val(), year: $("#FormADetYear").val(), month: $("#FormADetMonth").val(), assetGroup: $("#FormADetAssetGrpCode").val() },
            type: 'Post',
            success: function (data) {

                //console.log(data);
                if (data != null) {
                    $("#FormADetReferenceNO").val(data);
                    // $("#FormADetRmu").val(data.rmuCode);
                    // $("#FormADetsection").val(data.secName);
                }
            },
            error: function (data) {

                console.error(data);
            }
        });
    })

    $("#FormADetMonth").on("change", function () {

        $.ajax({
            url: '/NOD/GetReferenceNOData',
            dataType: 'JSON',
            data: { roadCode: $("#FormADetRoadCode").val(), year: $("#FormADetYear").val(), month: $("#FormADetMonth").val(), assetGroup: $("#FormADetAssetGrpCode").val() },
            type: 'Post',
            success: function (data) {

                //console.log(data);
                if (data != null) {
                    $("#FormADetReferenceNO").val(data);
                    // $("#FormADetRmu").val(data.rmuCode);
                    // $("#FormADetsection").val(data.secName);
                }
            },
            error: function (data) {

                console.error(data);
            }
        });
    })

    $("#FormADetYear").on("change", function () {

        $.ajax({
            url: '/NOD/GetReferenceNOData',
            dataType: 'JSON',
            data: { roadCode: $("#FormADetRoadCode").val(), year: $("#FormADetYear").val(), month: $("#FormADetMonth").val(), assetGroup: $("#FormADetAssetGrpCode").val() },
            type: 'Post',
            success: function (data) {

                //console.log(data);
                if (data != null) {
                    $("#FormADetReferenceNO").val(data);
                    // $("#FormADetRmu").val(data.rmuCode);
                    // $("#FormADetsection").val(data.secName);
                }
            },
            error: function (data) {

                console.error(data);
            }
        });
    });


    $("#ddlVerifiedby").on("change", function () {
        var id = $("#ddlVerifiedby option:selected").val();
        if (id != "99999999" && id != "") {
            $.ajax({
                url: '/NOD/GetUserById',
                dataType: 'JSON',
                data: { id },
                type: 'Post',
                success: function (data) {
                    $("#VerifiedName").val(data.userName);
                    $("#VerifiedDesignation").val(data.position);
                    $("#VerifiedName").prop("disabled", true);
                    $("#VerifiedDesignation").prop("disabled", true);
                    if (data.signIn != null && data.signIn != "") {
                        $("#verifiedSign").attr("src", data.signIn);
                    }
                    else {
                        $("#verifiedSign").attr("src", "");
                    }
                },
                error: function (data) {

                    console.error(data);
                }
            });
        }
        else if (id == "99999999") {
            $("#VerifiedName").prop("disabled", false);
            $("#VerifiedDesignation").prop("disabled", false);
            $("#VerifiedName").val('');
            $("#VerifiedDesignation").val('');
            $("#verifiedSign").attr("src", "");
        }
        else {
            $("#VerifiedName").val("");
            $("#VerifiedDesignation").val("");
            $("#VerifiedName").prop("disabled", true);
            $("#VerifiedDesignation").prop("disabled", true);
            $("#verifiedSign").attr("src", "");
        }
        return false;
    });

    $("#ddlInspectedby").on("change", function () {
        var id = $("#ddlInspectedby option:selected").val();
        if (id != "99999999" && id != "") {
            $.ajax({
                url: '/NOD/GetUserById',
                dataType: 'JSON',
                data: { id },
                type: 'Post',
                success: function (data) {
                    $("#InspectedDesignName").val(data.position);
                    $("#InspectedName").val(data.userName);
                    $("#InspectedDesignName").prop("disabled", true);
                    $("#InspectedName").prop("disabled", true);
                    if (data.signIn != null && data.signIn != "") {
                        $("#inspectedSign").attr("src", data.signIn);
                    }
                    else {
                        $("#inspectedSign").attr("src", "");
                    }
                },
                error: function (data) {

                    console.error(data);
                }
            });
        }
        else if (id == "99999999") {
            $("#InspectedName").prop("disabled", false);
            $("#InspectedDesignName").prop("disabled", false);
            $("#InspectedName").val('');
            $("#InspectedDesignName").val('');
            $("#inspectedSign").attr("src", "");
        }
        else {
            $("#InspectedDesignName").prop("disabled", true);
            $("#InspectedName").prop("disabled", true);
            $("#InspectedDesignName").val('');
            $("#InspectedName").val('');
            $("#inspectedSign").attr("src", "");
        }

        return false;
    });

    $.ajax({
        url: '/NOD/GetReferenceNOData',
        dataType: 'JSON',
        data: { roadCode: $("#FormADetRoadCode").val(), year: $("#FormADetYear").val(), month: $("#FormADetMonth").val(), assetGroup: $("#FormADetAssetGrpCode").val() },
        type: 'Post',
        success: function (data) {

            //console.log(data);
            if (data != null) {
                $("#FormADetReferenceNO").val(data);
                // $("#FormADetRmu").val(data.rmuCode);
                // $("#FormADetsection").val(data.secName);
            }
        },
        error: function (data) {

            console.error(data);
        }
    });

});


function AddNODClick() {
    $("body").addClass("loading");

    $.ajax({
        url: '/NOD/DetailedListEdit',
        type: 'POST',
        data: 0,
        //{
        //    assetName: $("#FormADetAssetGrpCode").val(), hdrId: $("#FormADetPKId").val(), hdrrefno: $("#FormADetReferenceNO").val()
        //},
        success: function (data) {
            //console.log(data);
            $("#DetailListGrid").html(data);
            $("body").removeClass("loading");
        }
    })
}
function AddNewForm(detId, title) {
    var Id = + $("#FormADetPKId").val();
    $("body").addClass("loading");
    $.ajax({
        url: '/NOD/AddNOD',
        data: { assetName: $("#FormADetAssetGrpCode").val(), hdrId: Id, hdrrefno: $("#FormADetReferenceNO").val(), detId: detId },
        type: 'POST',
        success: function (data) {
            
            $("#div-data-container1").html(data);
            $("#IsViewDetail").val('0');
            InitnumericValidation();
            var d = $("#multiSiteHidden").val();
            if (d != null && d != undefined && d != "") {
                var _multiSite = d.split(',');
                if (_multiSite != "") {
                    $("#formASiteRefmultiSelect").val(_multiSite).trigger("chosen:updated");

                }
            }
            var id = $("#formAFadPKRefNO").val();
            if (id != "0" && title != undefined && title != null) {
                //$("#saveContFormADetailsBtn").hide();
                $("#clearFormASubDetailBtn").hide();
                $("#formDetailText").text("Edit Form A Details | Ref-" + title);
            }

            $("body").removeClass("loading");
            Validation.OnKeyPressInit();
        },
        error: function (e) {
            console.log(e);
        }
    });
}

function ViewDetail(detId, title) {
    var Id = + $("#FormADetPKId").val();
    $("body").addClass("loading");
    $.ajax({
        url: '/NOD/AddNOD',
        data: { assetName: $("#FormADetAssetGrpCode").val(), hdrId: Id, hdrrefno: $("#FormADetReferenceNO").val(), detId: detId },
        type: 'POST',
        success: function (data) {

            $("#div-data-container1").html(data);
            $("#IsViewDetail").val('1');
            var id = $("#formAFadPKRefNO").val();
            var _multiSite = $("#multiSiteHidden").val().split(',');
            if (_multiSite != "") {
                $("#formASiteRefmultiSelect").val(_multiSite).trigger("chosen:updated");

            }
            $(".disableFADinput").prop("disabled", true);
            $("#saveContFormADetailsBtn").hide();
            $("#saveExitFormADetailsBtn").hide();

            $("#clearFormASubDetailBtn").hide();
            if (id != "0" && title != null && title != "") {
                $("#formDetailText").text("View Form A Details | Ref-" + title);
            }

            $("body").removeClass("loading");
        },
        error: function (e) {
            console.log(e);
        }
    });
}

function DeleteDetailRecord(detailId) {
    app.Confirm("Are you sure you want to delete the record?, If Yes click OK.", function (e) {
        if (e) {
            $("body").addClass("loading");
            $.ajax({
                url: '/NOD/DetailListDelete',
                data: { detailId },
                type: 'POST',
                success: function (data) {
                    if (data > 0) {
                        $("body").removeClass("loading");
                        app.ShowSuccessMessage("Successfully Deleted");
                        InitializeGrid();
                    }
                    else {
                        alert("Error in Deleted. Kindly retry later.");
                        $("body").removeClass("loading");
                    }
                }
            });
        }
    });
}
