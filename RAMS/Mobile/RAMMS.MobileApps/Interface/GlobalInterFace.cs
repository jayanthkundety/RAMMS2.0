using System;
namespace RAMMS.MobileApps.Interface
{
    public interface IFromJDropDown
    {
        void GetDropDownDetailResponse(FormJDropDownResponseData responseBase);
        void APIHitFailed(string error);
    }
    public interface IFromJAssetDrop
    {
        void GetDropDownJDetailResponse(JAssetDropDown responseBase);
        void GetDropDownJMonthResponse(JAssetDropDown responseBase);
        void GetDropDownJYearResponse(JAssetDropDown responseBase);

        void APIHitFailed(string error);
    }
    public interface IFormJAdd
    {
        void GetFormJAddRoadCodeResponse(JAssetDropDown responseBase);
        void GetFormJHeaderCodeResponse(FormJByHeader responseBase);
        void GetFormJUserList(JAssetDropDown responseBase);
        void GetFormJUserDetail(UserDataResponse responseBase);
        void GetSerialNo(GetSerialNo getSerialNo);
        void APIHitFailed(string error);
    }
    public interface IFromJLandingGrid
    {
        void GetFromJLandingGridResponse(FormJLadingGridList responseBase);
        void APIHitFailed(string error);
    }
    public interface IFromJSelectedRoadCode
    {
        void GetFromJLandingGridResponse(SelectedRoadCodeResponse responseBase);
        void GetRefrenceNumber(FormJAddRefrenceNumber formJAddRefrenceNumber);
        void GetFindDetails(FormJFindDetailResponse formJFindDetailResponse);
        void GetFormjADDGridDEtail(FormJGridListResponse formJGridListResponse);
        void APIHitFailed(string error);
    }
    public interface IFormJOne
    {
        void GetFormSerialNo(GetSerialNo getFormJDtlById);
        void GetFormAssetCodeNo(GetSerialNo getFormJDtlById);
        void GetFormAssetDefectCode(JAssetDropDown getFormJDtlById);
        void GetDropDownLocationDetailResponse(JAssetDropDown responseBase);
        void GetDropDownPriorityResponse(JAssetDropDown responseBase);
        void GetSaveDetil(FormJAddRefrenceNumber formJAddRefrenceNumber);
        // void DeleteDetail(GetSerialNo getSerialNo);
        void APIHitFailed(string error);
    }
    public interface IFormDelete
    {
        void DeleteHeader(GetSerialNo getSerialNo);

        void APIHitFailed(string error);
    }

}
