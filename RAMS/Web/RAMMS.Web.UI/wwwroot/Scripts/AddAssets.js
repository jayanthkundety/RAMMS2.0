$(document).ready(function () { 

    $("#AssetFeatureId").chosen();
    $("#AssetStrucSuper").chosen();
    $("#AssetParapetType").chosen();
    $("#AssetGrpType").chosen();
    $("#AssetBound").chosen();
    $("#AssetStrucCode").chosen();
    $("#AssetGrpCode").chosen();
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


    $("#AssetFeatureId").change(function () {
        $.ajax({
            url: '/Assets/GetAssetDataByFeatureId',
            dataType: 'JSON',
            data: { featureId: $("#AssetFeatureId").val(), assetId: $("#AssetGroupType").val(), boundId: $("#AssetBound").val() },
            type: 'Post',            
            success: function (data) {
                if (data != null) {
                    $("#AssetDivCode").val(data.divisionCode);
                    $("#AssetRmuCode").val(data.rmuCode);
                    //$("#AssetRmuAbbrev").val(data.rmuAbbrev);
                    $("#AssetSecCode").val(data.secCode);
                    $("#AssetSecName").val(data.secName);
                    $("#AssetRoadCode").val(data.roadCode);
                    $("#AssetRoadName").val(data.roadName);
                    $('#AssetID').val("");
                    $("#AssetID").val(data.assetId);                    
                }
            },
            error: function (data) {
                
                console.error(data);
            }
        });
        
        return false;
    })
    $("#AssetBound").change(function () {
        
        $.ajax({
            url: '/Assets/GetAssetDataByFeatureId',
            dataType: 'JSON',
            data: { featureId: $("#AssetFeatureId").val(), assetId: $("#AssetGroupType").val(), boundId: $("#AssetBound").val() },
            type: 'Post',
            success: function (data) {                
                if (data != null) {
                    $("#AssetDivCode").val(data.divisionCode);
                    //$("#AssetRmuAbbrev").val(data.rmuAbbrev);
                    $("#AssetRmuCode").val(data.rmuCode);
                    $("#AssetSecCode").val(data.secCode);
                    $("#AssetSecName").val(data.secName);
                    $("#AssetRoadCode").val(data.roadCode);
                    $("#AssetRoadName").val(data.roadName);
                    $('#AssetID').val("");
                    $("#AssetId").val(data.assetId); 
                }

            },
            error: function (ex) {
                alert('Failed.' + ex);
            }
        });
        return false;
    })
    $("#AssetGrpType").change(function () {

        //alert($("#AiFeatureId").val());
        $.ajax({
            url: '/Assets/GetAssetDataByFeatureId',
            dataType: 'JSON',
            data: { featureId: $("#AssetFeatureId").val(), assetId: $("#AssetGrpType").val(), boundId: $("#AssetBound").val() },
            type: 'Post',
            success: function (data) {
                if (data != null) {
                    $("#AssetDivCode").val(data.divisionCode);
                    $("#AssetRmuCode").val(data.rmuCode);
                    //$("#AssetRmuAbbrev").val(data.rmuAbbrev);
                    $("#AssetSecCode").val(data.secCode);
                    $("#AssetSecName").val(data.secName);
                    $("#AssetRoadCode").val(data.roadCode);
                    $("#AssetRoadName").val(data.roadName);
                    $('#AssetID').val("");
                    $("#AssetId").val(data.assetId); 
                }

            },
            error: function (ex) {
                alert('Failed.' + ex);
            }
        });
        return false;
    })

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
        var valdata = $("#AddAssetModalForm").serialize();

        $.ajax({
            url: '/assets/Add',
            data: valdata,
            type: 'POST',
            success: function (data) {
                $("#AssetsAddModal").modal('hide');
                LoadAssetList();
                if (saveImg) {
                    saveImageUpload('FormFile')
                }
            },
            error: function (data) {
                alert(data.responseText);
            }
        });
    }
});