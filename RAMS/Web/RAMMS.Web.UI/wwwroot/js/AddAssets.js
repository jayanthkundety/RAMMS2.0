$(document).ready(function () {

    $("#AssetFeatureId").chosen();
    $("#AssetStrucSuper").chosen();
    $("#AssetParapetType").chosen();
    $("#AssetGrpType").chosen();
    $("#AssetBound").chosen();
    //$("#AssetStrucCode").chosen();
    //$("#AssetGrpCode").chosen();
    $("#AssetBearingType").chosen();
    $("#AssetExpanType").chosen();
    $("#AssetDeckType").chosen();
    $("#AssetAbutType").chosen();
    $("#AssetPierType").chosen();

    $("#AssetAbutFound").chosen();
    $("#AssetPiersPrimComp").chosen();
    $("#AssetBearingSeatDiaphg").chosen();
    $("#AssetBeamsGridTrusArch").chosen();
    $("#AssetDeckPavement").chosen();
    $("#AssetUtilities").chosen();
    $("#AssetWaterway").chosen();
    $("#AssetWaterDownpipe").chosen();
    $("#AssetParapetRailing").chosen();
    $("#AssetSidewalksAppSlab").chosen();
    $("#AssetExpanJoint").chosen();
    $("#AssetSlopeRetainWall").chosen();
    $("#AssetExpanJointCount").chosen();
    $("#AssetMaterial").chosen();
    $("#AssetPreCast").chosen();
    $("#AssetIntelStruc").chosen();
    $("#AssetOutletStruc").chosen();
    $("#AssetLaneNumber").chosen();

    $("#AbutFoundOther").css("display", "none");
    $("#PiersCompOthers").css("display", "none")
    $("#BearingSeatDiaphgOthers").css("display", "none")
    $("#BeamsGridTrusArchOthers").css("display", "none")
    $("#DeckPavementOthers").css("display", "none")
    $("#SignboardUtilitiesOthers").css("display", "none")
    $("#WaterwayOthers").css("display", "none")
    $("#WaterDownPipeOthers").css("display", "none")
    $("#ParapetRailingOthers").css("display", "none")
    $("#SidewalkAppSlabOthers").css("display", "none")
    $("#ExpanJointOthers").css("display", "none")
    $("#SlopeRetainWallOthers").css("display", "none")
    $("#CulvertMaterialOthers").css("display", "none")
    $("#StrucCodeOthers").css("display", "none")

    GetImageList();

    $("#AssetAbutFound").on("change", function () {
        var temp = $("#AssetAbutFoundOther").val();
        var AssetAbutArray = [];
        $("#AbutFoundOther").css("display", "none")
        AssetAbutArray = $("#AssetAbutFound").val();
        if (AssetAbutArray.length == 0) {
            $("#AssetAbutFoundOther").val(null);
        }
        AssetAbutArray.forEach(function (data) {
            if (data == "Others") {
                $("#AbutFoundOther").css("display", "block")
                $("#AssetAbutFoundOther").val(temp);
            }
            else {
                $("#AssetAbutFoundOther").val(null);
            }
        })
    });

    $("#AssetAbutFound").trigger("change");

    $("#AssetPiersPrimComp").on("change", function () {
        var temp = $("#PiersPrimCompOthers").val();
        var PiersPrimArray = [];

        $("#PiersCompOthers").css("display", "none")
        PiersPrimArray = $("#AssetPiersPrimComp").val();
        if (PiersPrimArray.length == 0) {
            $("#PiersPrimCompOthers").val(null);
        }
        PiersPrimArray.forEach(function (data) {
            if (data == "Others") {
                $("#PiersCompOthers").css("display", "block")
                $("#PiersPrimCompOthers").val(temp);
            }
            else {
                $("#PiersPrimCompOthers").val(null);
            }
        })
    })

    $("#AssetPiersPrimComp").trigger("change");

    $("#AssetDeckPavement").on("change", function () {
        var temp = $("#AssetDeckPavementOthers").val();
        var AssetAbutArray = [];
        $("#DeckPavementOthers").css("display", "none")
        AssetAbutArray = $("#AssetDeckPavement").val();
        if (AssetAbutArray.length == 0) {
            $("#AssetDeckPavementOthers").val(null);
        }
        AssetAbutArray.forEach(function (data) {
            if (data == "Others") {
                $("#DeckPavementOthers").css("display", "block")
                $("#AssetDeckPavementOthers").val(temp);
            }
            else {
                $("#AssetDeckPavementOthers").val(null);
            }
        })
    })

    $("#AssetDeckPavement").trigger("change");

    $("#AssetBearingSeatDiaphg").on("change", function () {
        var temp = $("#AssetBearingSeatDiaphgOthers").val();
        var AssetAbutArray = [];
        $("#BearingSeatDiaphgOthers").css("display", "none")
        AssetAbutArray = $("#AssetBearingSeatDiaphg").val();
        if (AssetAbutArray.length == 0) {
            $("#AssetBearingSeatDiaphgOthers").val(null);
        }
        AssetAbutArray.forEach(function (data) {
            if (data == "Others") {
                $("#BearingSeatDiaphgOthers").css("display", "block")
                $("#AssetBearingSeatDiaphgOthers").val(temp);
            }
            else {
                $("#AssetBearingSeatDiaphgOthers").val(null);
            }
        })
    })

    $("#AssetBearingSeatDiaphg").trigger("change");

    $("#AssetBeamsGridTrusArch").on("change", function () {
        var temp = $("#AssetBeamsGridTrusArchOthers").val();
        var AssetAbutArray = [];
        $("#BeamsGridTrusArchOthers").css("display", "none")
        AssetAbutArray = $("#AssetBeamsGridTrusArch").val();
        if (AssetAbutArray.length == 0) {
            $("#AssetBeamsGridTrusArchOthers").val(null);
        }
        AssetAbutArray.forEach(function (data) {
            if (data == "Others") {
                $("#BeamsGridTrusArchOthers").css("display", "block")
                $("#AssetBeamsGridTrusArchOthers").val(temp);
            }
            else {
                $("#AssetBeamsGridTrusArchOthers").val(null);
            }
        })
    })

    $("#AssetBeamsGridTrusArch").trigger("change");

    $("#AssetUtilities").on("change", function () {
        var temp = $("#AssetSignboardUtilitiesOthers").val();
        var AssetAbutArray = [];
        $("#SignboardUtilitiesOthers").css("display", "none")
        AssetAbutArray = $("#AssetUtilities").val();
        if (AssetAbutArray.length == 0) {
            $("#AssetSignboardUtilitiesOthers").val(null);
        }
        AssetAbutArray.forEach(function (data) {
            if (data == "Others") {
                $("#SignboardUtilitiesOthers").css("display", "block")
                $("#AssetSignboardUtilitiesOthers").val(temp);
            }
            else {
                $("#AssetSignboardUtilitiesOthers").val(null);
            }
        })
    })

    $("#AssetUtilities").trigger("change");

    $("#AssetWaterway").on("change", function () {
        var temp = $("#AssetWaterwayOthers").val();
        var AssetAbutArray = [];
        $("#WaterwayOthers").css("display", "none")
        AssetAbutArray = $("#AssetWaterway").val();
        if (AssetAbutArray.length == 0) {
            $("#AssetWaterwayOthers").val(null);
        }
        AssetAbutArray.forEach(function (data) {
            if (data == "Others") {
                $("#WaterwayOthers").css("display", "block")
                $("#AssetWaterwayOthers").val(temp);
            }
            else {
                $("#AssetWaterwayOthers").val(null);
            }
        })
    })

    $("#AssetWaterway").trigger("change");

    $("#AssetWaterDownpipe").on("change", function () {
        var temp = $("#AssetWaterDownPipeOthers").val();
        var AssetAbutArray = [];
        $("#WaterDownPipeOthers").css("display", "none")
        AssetAbutArray = $("#AssetWaterDownpipe").val();
        if (AssetAbutArray.length == 0) {
            $("#AssetWaterDownPipeOthers").val(null);
        }
        AssetAbutArray.forEach(function (data) {
            if (data == "Others") {
                $("#WaterDownPipeOthers").css("display", "block")
                $("#AssetWaterDownPipeOthers").val(temp);
            }
            else {
                $("#AssetWaterDownPipeOthers").val(null);
            }
        })
    })

    $("#AssetWaterDownpipe").trigger("change");

    $("#AssetParapetRailing").on("change", function () {
        var temp = $("#AssetParapetRailingOthers").val();
        var AssetAbutArray = [];
        $("#ParapetRailingOthers").css("display", "none")
        AssetAbutArray = $("#AssetParapetRailing").val();
        if (AssetAbutArray.length == 0) {
            $("#AssetParapetRailingOthers").val(null);
        }
        AssetAbutArray.forEach(function (data) {
            if (data == "Others") {
                $("#ParapetRailingOthers").css("display", "block")
                $("#AssetParapetRailingOthers").val(temp);
            }
            else {
                $("#AssetParapetRailingOthers").val(null);
            }
        })
    })

    $("#AssetParapetRailing").trigger("change");

    $("#AssetSidewalksAppSlab").on("change", function () {
        var temp = $("#AssetSidewalkAppSlabOthers").val();
        var AssetAbutArray = [];
        $("#SidewalkAppSlabOthers").css("display", "none")
        AssetAbutArray = $("#AssetSidewalksAppSlab").val();
        if (AssetAbutArray.length == 0) {
            $("#AssetSidewalkAppSlabOthers").val(null);
        }
        AssetAbutArray.forEach(function (data) {
            if (data == "Others") {
                $("#SidewalkAppSlabOthers").css("display", "block")
                $("#AssetSidewalkAppSlabOthers").val(temp);
            }
            else {
                $("#AssetSidewalkAppSlabOthers").val(null);
            }
        })
    })

    $("#AssetSidewalksAppSlab").trigger("change");

    $("#AssetExpanJoint").on("change", function () {
        var temp = $("#AssetExpanJointOthers").val();
        var AssetAbutArray = [];
        $("#ExpanJointOthers").css("display", "none")
        AssetAbutArray = $("#AssetExpanJoint").val();
        if (AssetAbutArray.length == 0) {
            $("#AssetExpanJointOthers").val(null);
        }
        AssetAbutArray.forEach(function (data) {
            if (data == "Others") {
                $("#ExpanJointOthers").css("display", "block")
                $("#AssetExpanJointOthers").val(temp);
            }
            else {
                $("#AssetExpanJointOthers").val(null);
            }
        })
    })

    $("#AssetExpanJoint").trigger("change");

    $("#AssetSlopeRetainWall").on("change", function () {
        var temp = $("#AssetSlopeRetainWallOthers").val();
        var AssetAbutArray = [];
        $("#SlopeRetainWallOthers").css("display", "none")
        AssetAbutArray = $("#AssetSlopeRetainWall").val();
        if (AssetAbutArray.length == 0) {
            $("#AssetSlopeRetainWallOthers").val(null);
        }
        AssetAbutArray.forEach(function (data) {
            if (data == "Others") {
                $("#SlopeRetainWallOthers").css("display", "block")
                $("#AssetSlopeRetainWallOthers").val(temp);
            }
            else {
                $("#AssetSlopeRetainWallOthers").val(null);
            }
        })
    })

    $("#AssetSlopeRetainWall").trigger("change");


    $("#AssetMaterial").on("change", function () {
        var temp = $("#AssetCulvertMaterialOthers").val();
        var AssetAbutArray;
        $("#CulvertMaterialOthers").css("display", "none")
        AssetAbutArray = $("#AssetMaterial").val();
        //if (AssetAbutArray.length == 0) {
        //    $("#AssetCulvertMaterialOthers").val(null);
        //}
        if (AssetAbutArray == "Others") {
            $("#CulvertMaterialOthers").css("display", "block")
            $("#AssetCulvertMaterialOthers").val(temp);
        }
        else {
            $("#AssetCulvertMaterialOthers").val(null);
        }

    })
    $("#AssetMaterial").trigger("change");

    $("#CulvertType").on("change", function () {
        var temp = $("#AssetCulvertTypeOthers").val();
        var AssetAbutArray;
        $("#CulvertTypeOthers").css("display", "none")
        AssetAbutArray = $("#CulvertType").val();       
        if (AssetAbutArray == "Others") {
            $("#CulvertTypeOthers").css("display", "block")
            $("#AssetCulvertTypeOthers").val(temp);
        }
        else {
            $("#AssetCulvertTypeOthers").val(null);
        }
    })
    $("#CulvertType").trigger("change");

   
    $("#AssetStrucCode").on("change", function () {
        var val = $("#AssetStrucCode").val();
        if (val == 'O' || val == 'Others') {
            $("#StrucCodeOthers").css("display", "block")
        }
        else {
            $("#AssetStrucCodeOthers").val(null);
            $("#StrucCodeOthers").css("display", "none")
        }
    })

    $("#AssetStrucCode").trigger("change");



    //Asset Id Generation Change Events
    $("#AssetFeatureId").change(function () {
        if ($("#AssetFeatureId").val() != "") {
            $('#AssetGrpType').prop('disabled', false);
            $('#AssetGrpType').val('').trigger('chosen:updated');
            $('#AssetBound').prop('disabled', false);
            $('#AssetBound').val('').trigger('chosen:updated');
            $('#AssetLocChKm').prop('disabled', false);
            $('#AssetLocChM').prop('disabled', false);
            $('#AssetRefNo').prop('disabled', false);
            $('#AssetDistrict').prop('disabled', false);
            $('#AssetNumber').prop('disabled', false);
            $('#AssetLaneNumber').prop('disabled', false);
            $('#AssetLaneNumber').val('').trigger('chosen:updated');
            AssetIdGeneration()
        }
        else {
            $('#AssetGrpType').prop('disabled', true);
            $('#AssetGrpType').val('').trigger('chosen:updated');
            $('#AssetBound').prop('disabled', true);
            $('#AssetBound').val('').trigger('chosen:updated');
            $('#AssetLaneNumber').prop('disabled', true);
            $('#AssetLaneNumber').val('').trigger('chosen:updated');
            $("#AssetDivCode").val("");
            $("#AssetRmuCode").val("");
            $("#AssetRmuName").val(null);
            $("#AssetSecCode").val(null);
            $("#AssetSecName").val(null);
            $("#AssetRoadCode").val(null);
            $("#AssetRoadName").val(null);
            $('#AssetId').val("");
        }
        return false;
    })
    $("#AssetBound").change(function () {
        AssetIdGeneration()
        return false;
    })
    $("#AssetGrpType").change(function () {
        AssetIdGeneration();
        return false;
    })
    $("#AssetGrpType").trigger("change");


    $("#AssetLocChKm").keyup(function () {
        AssetIdGeneration();
    })
    $("#AssetLocChKm").trigger("keyup");

    $("#AssetLocChM").keyup(function () {
        AssetIdGeneration();
    })
    $("#AssetLocChM").trigger("keyup");

    $("#AssetNumber").keyup(function () {
        AssetIdGeneration();
    })
    $("#AssetNumber").trigger("keyup");

    $("#AssetLaneNumber").change(function () {
        AssetIdGeneration();
    })

    // $("#AssetLaneNumber").trigger("change");

    function AssetIdGeneration() {
        var formData = new Object();
        if ($("#AssetFeatureId").val() != "") {
            formData.FeatureId = $("#AssetFeatureId").val();
            formData.LocationChKm = $("#AssetLocChKm").val();
            formData.LocationChM = $("#AssetLocChM").val();
            formData.GroupType = $("#AssetGrpType").val();
            formData.Bound = $("#AssetBound").val();
            formData.GroupCode = $("#AssetGrpCode").val();
            formData.AssetNumber = $("#AssetNumber").val();
            $('#AssetLaneNumber').prop('disabled', false);
            formData.LaneNo = $("#AssetLaneNumber").val();
            InitAjaxLoading();
            $.ajax({
                url: '/Assets/GetAssetDataId',
                data: formData,
                type: 'Post',
                success: function (data) {
                    if (data != null) {
                        if (data._RMAllData != null && data._RMAllData != "" && data._RMAllData != undefined) {
                            $("#AssetDivCode").val(data._RMAllData.divisionCode);
                            //$("#AssetRmuAbbrev").val(data.rmuAbbrev);
                            $("#AssetRmuCode").val(data._RMAllData.rmuCode);
                            $("#AssetRmuName").val(data._RMAllData.rmuName);
                            $("#AssetSecCode").val(data._RMAllData.secCode);
                            $("#AssetSecName").val(data._RMAllData.secName);
                            $("#AssetRoadCode").val(data._RMAllData.roadCode);
                            $("#AssetRoadName").val(data._RMAllData.roadName);
                            $('#AssetID').val("");
                            $("#AssetId").val(data._RMAllData.assetId);
                            if (data._RMAllData.assetId != null && data._RMAllData.assetId != "" && data._RMAllData.assetId != undefined) {
                                var AssetIdSplit = data._RMAllData.assetId.split('/');
                                $("#AssetStrucCode").val(AssetIdSplit[1]);
                                $("#AssetStrucCode").trigger("change");
                            }
                        }
                        //Duplicate record restricted
                        if (data.pkNo != 0) {
                            app.ShowErrorMessage("Asset Id " + $("#AssetId").val() + " already exist.");
                            $("#AddAssetsBtn").prop("disabled", true);
                        }
                        else {
                            $("#AddAssetsBtn").prop("disabled", false);
                        }
                        HideAjaxLoading();
                    }

                },
                error: function (ex) {
                    //debugger;
                    app.ShowDialog(ex.responseJSON.message);
                    //alert('Failed.' + ex.responseJSON.message);
                    HideAjaxLoading();
                }
            });
        }
        else {
            $("#AssetDivCode").val("");
            $("#AssetRmuAbbrev").val("");
            $("#AssetRmuCode").val("");
            $("#AssetRmuName").val("");
            $("#AssetSecCode").val("");
            $("#AssetSecName").val("");
            $("#AssetRoadCode").val("");
            $("#AssetRoadName").val("");
            $("#AssetId").val("");
            $("#AssetStrucCode").val("");
            $("#AssetLocChKm").val("");
            $("#AssetLocChM").val("");
            $('#AssetBound').val("");
            $('#AssetNumber').val("");
        }
        return false;
    }


    //Assets Add Modal Save
    $("#AddAssetsBtn").click(function () {
        saveAssets();
        //var valdata = $("#AddAssetModalForm").serialize();

        //$.ajax({
        //    url: '/assets/Add',
        //    data: valdata,
        //    type: 'POST',
        //    success: function (data) {
        //        $("#AssetsAddModal").modal('hide');
        //        LoadAssetList();
        //        alert("Successfully Save");
        //    },
        //    error: function (data) {
        //        alert(data.responseText);
        //    }
        //});
    });

    function saveAssets(saveImg) {
        $('#custom-accordion-one *').attr("disabled", false);
        $('#AssetLaneNumber').prop('disabled', false);
        var featureId = $('#AssetFeatureId').val();
        //  $('#custom-accordion-one *').attr("disabled", true);
        if (featureId != null && featureId != "" && featureId != undefined) {
            var valdata = $("#AddAssetModalForm").serialize();
            var AssetLocChKm = $('#AssetLocChKm').val();
            if (AssetLocChKm != "")
                $('#AssetLocChKm').prop('disabled', true);
            var AssetLocChM = $('#AssetLocChM').val();
            if (AssetLocChM != "")
                $('#AssetLocChM').prop('disabled', true);
            //if (ValidatePage('#AddAssetModalForm')) {
            //    SubmitForm("AddAssetModalForm", "json", function (data) {
            //        LoadAssetList();
            //        alert("Successfully Saved New");

            //        GetImageList();
            //        if (saveImg) {
            //            saveImageUpload('FormFile');
            //        }

            //    });
            //}
            if (ValidatePage('#AddAssetModalForm')) {
                InitAjaxLoading();
                $.ajax({
                    url: '/assets/Add',
                    data: valdata,
                    type: 'POST',
                    success: function (data) {
                        if (data.refNO != null && data.refNO != "" && data.refNO != undefined && data.refNO != 0) {
                            $("#asset_PK").val(data.refNO);
                            $("#assetPK").val(data.refNO);
                            $("#AssetPkId").val(data.refNO);
                            $("#AssetOtherPk").val(data.otherPkId);
                        }
                        $('.nav-tabs a[href="#AssetTabPage2"]').tab('show');
                        //$('#custom-accordion-one *').attr("disabled", true);
                        $('#AssetGrpType').prop('disabled', true).trigger("chosen:updated");
                        $('#AssetBound').prop('disabled', true).trigger("chosen:updated");
                        $('#AssetFeatureId').prop('disabled', true).trigger("chosen:updated");
                        $('#AssetLaneNumber').prop('disabled', true).trigger("chosen:updated");

                        HideAjaxLoading();
                        app.ShowSuccessMessage($("#AssetId").val() + ' Successfully Saved', false);
                        $("#val-summary-displayer").css("display", "none");
                        GetImageList();
                        if (saveImg) {
                            saveImageUpload('FormFile');
                        }
                    },
                    error: function (data) {
                        HideAjaxLoading();
                        app.ShowDialog(ex.responseJSON.message);
                        //Code commented by John for future reference
                        //if (HandleValidationErrorSummaryIfAny(data, 'AddAssetModalForm')) { alert(data.responseText); }
                    }
                });
            }
            //else {
            //    $('#custom-accordion-one *').attr("disabled", true);
            //}
        }
        else {
            app.ShowErrorMessage("Please Select Road Feature ID, It Cannot Be Empty");
        }
    }

});
function GetImageList() {
    var assetPk = $("#asset_PK").val();
    var assetId = $("#AssetId").val();
    InitAjaxLoading();
    $.ajax({
        url: '/assets/GetImageList',
        data: { assetPk: assetPk, assetId: assetId, location: window.location.origin },
        type: 'POST',
        success: function (data) {
            $("#photoView").html(data);
            document.getElementById("AssetBrowseBtn").disabled = true;
            document.getElementById("AssetUploadBtn").disabled = true;
            HideAjaxLoading();
        },
        error: function (data) {
            HideAjaxLoading();
            app.ShowDialog(ex.responseJSON.message);
        }
    });


}

function ValidateNumber(e) {
    var code = (e.which) ? e.which : e.keyCode;
    if (code > 31 && (code < 48 || code > 57)) {
        e.preventDefault();
    }
};
