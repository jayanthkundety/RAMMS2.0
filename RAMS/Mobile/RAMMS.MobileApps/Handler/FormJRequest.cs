using System;
using System.Threading.Tasks;
using RAMMS.MobileApps.Services;
using RestSharp;

namespace RAMMS.MobileApps
{
    public interface IFormJRequest
    {
        Task GetFormJLandingDropDown(string restRequest, Action<FormJDropDownResponseData> successCallback, Action<ResponseBase> errorCallback);
        Task GetFormJLandingAssetDropDown(string restRequest, Action<JAssetDropDown> successCallback, Action<ResponseBase> errorCallback);
        Task GetFormJLandingMonthDropDown(string restRequest, Action<JAssetDropDown> successCallback, Action<ResponseBase> errorCallback);
        Task GetFormJLandingYearDropDown(string restRequest, Action<JAssetDropDown> successCallback, Action<ResponseBase> errorCallback);
        Task GetFormJLandingGrid(string restRequest, Action<FormJLadingGridList> successCallback, Action<ResponseBase> errorCallback);
        Task GetFormJAddRoadCodeDropDown(string restRequest, Action<JAssetDropDown> successCallback, Action<ResponseBase> errorCallback);
        Task GetFormJAddSelectedRoadCodeDropDown(string restRequest, Action<SelectedRoadCodeResponse> successCallback, Action<ResponseBase> errorCallback);
        Task GetFormJAddRefrencenumber(RestRequest restRequest, Action<FormJAddRefrenceNumber> successCallback, Action<ResponseBase> errorCallback);
        Task GetFormJFindDetail(string param, Action<FormJFindDetailResponse> successCallback, Action<ResponseBase> errorCallback);
        Task GetFormJADDGridDetail(string param, Action<FormJGridListResponse> successCallback, Action<ResponseBase> errorCallback);
        Task GetFormJOneJDtlById(RestRequest param, Action<GetSerialNo> successCallback, Action<ResponseBase> errorCallback);
        Task GetFormNameJ(RestRequest param, Action<GetSerialNo> successCallback, Action<ResponseBase> errorCallback);
        Task GetFormNameJDefectCode(RestRequest param, Action<JAssetDropDown> successCallback, Action<ResponseBase> errorCallback);
        Task GetSaveFormJ(string param, Action<FormJAddRefrenceNumber> successCallback, Action<ResponseBase> errorCallback);
        Task GetFormJByHeaderid(RestRequest param, Action<FormJByHeader> successCallback, Action<ResponseBase> errorCallback);
        Task GetFormJByUserList(Action<JAssetDropDown> successCallback, Action<ResponseBase> errorCallback);
        Task GetFormJUserDetail(string param, Action<UserDataResponse> successCallback, Action<ResponseBase> errorCallback);

        Task SaveSignature(string param, Action<GetSerialNo> successCallback, Action<ResponseBase> errorCallback);
        Task DeleteHeaderRequest(RestRequest param, Action<GetSerialNo> successCallback, Action<ResponseBase> errorCallback);
        Task DeleteDetailRequest(RestRequest param, Action<GetSerialNo> successCallback, Action<ResponseBase> errorCallback);
    }
    public class FormJRequest : IFormJRequest
    {
        public async Task GetFormJAddRefrencenumber(RestRequest restRequest, Action<FormJAddRefrenceNumber> successCallback, Action<ResponseBase> errorCallback)
        {
            await APIServiceProvider.ExecuteWithOutBody(restRequest, Method.POST, AppConst.URL_GetRefrenceNumber, successCallback, errorCallback);
        }

        public async Task GetFormJAddRoadCodeDropDown(string restRequest, Action<JAssetDropDown> successCallback, Action<ResponseBase> errorCallback)
        {
            await APIServiceProvider.Execute(AppConst.URL_RoadCode, Method.GET, successCallback, errorCallback);
        }

        public async Task GetFormJAddSelectedRoadCodeDropDown(string restRequest, Action<SelectedRoadCodeResponse> successCallback, Action<ResponseBase> errorCallback)
        {
            await APIServiceProvider.ExecuteWithUrlEncode(restRequest, Method.POST, AppConst.URL_SelectedRoadCode, successCallback, errorCallback);
        }

        public async Task GetFormJFindDetail(string param, Action<FormJFindDetailResponse> successCallback, Action<ResponseBase> errorCallback)
        {
            await APIServiceProvider.ExecuteWithUrlEncode(param, Method.POST, AppConst.URL_SaveHdr, successCallback, errorCallback);
        }

        public async Task GetFormJADDGridDetail(string param, Action<FormJGridListResponse> successCallback, Action<ResponseBase> errorCallback)
        {
            await APIServiceProvider.ExecuteWithUrlEncode(param, Method.POST, AppConst.URL_DetailGrid, successCallback, errorCallback);
        }

        public async Task GetFormJLandingAssetDropDown(string restRequest, Action<JAssetDropDown> successCallback, Action<ResponseBase> errorCallback)
        {
            await APIServiceProvider.ExecuteWithUrlEncode(restRequest, Method.POST, AppConst.URL_DDLIST, successCallback, errorCallback);
        }

        public async Task GetFormJLandingDropDown(string restRequest, Action<FormJDropDownResponseData> successCallback, Action<ResponseBase> errorCallback)
        {
            await APIServiceProvider.ExecuteWithUrlEncode(restRequest, Method.POST, AppConst.URL_LandingDropDown, successCallback, errorCallback);
        }

        public async Task GetFormJLandingGrid(string restRequest, Action<FormJLadingGridList> successCallback, Action<ResponseBase> errorCallback)
        {
            await APIServiceProvider.ExecuteWithUrlEncode(restRequest, Method.POST, AppConst.URL_LandingGrid, successCallback, errorCallback);
        }

        public async Task GetFormJLandingMonthDropDown(string restRequest, Action<JAssetDropDown> successCallback, Action<ResponseBase> errorCallback)
        {
            await APIServiceProvider.ExecuteWithUrlEncode(restRequest, Method.POST, AppConst.URL_DDLIST, successCallback, errorCallback);
        }

        public async Task GetFormJLandingYearDropDown(string restRequest, Action<JAssetDropDown> successCallback, Action<ResponseBase> errorCallback)
        {
            await APIServiceProvider.ExecuteWithUrlEncode(restRequest, Method.POST, AppConst.URL_DDLIST, successCallback, errorCallback);
        }

        public async Task GetFormJOneJDtlById(RestRequest param, Action<GetSerialNo> successCallback, Action<ResponseBase> errorCallback)
        {
            await APIServiceProvider.ExecuteWithOutBody(param, Method.POST, AppConst.URL_GetFormJDlSerialNo, successCallback, errorCallback);
        }

        public async Task GetFormNameJ(RestRequest param, Action<GetSerialNo> successCallback, Action<ResponseBase> errorCallback)
        {
            await APIServiceProvider.ExecuteWithOutBody(param, Method.POST, AppConst.URL_GetAssetCode, successCallback, errorCallback);
        }

        public async Task GetFormNameJDefectCode(RestRequest param, Action<JAssetDropDown> successCallback, Action<ResponseBase> errorCallback)
        {
            await APIServiceProvider.ExecuteWithOutBody(param, Method.POST, AppConst.URL_GetDefecetCode, successCallback, errorCallback);
        }

        public async Task GetSaveFormJ(string param, Action<FormJAddRefrenceNumber> successCallback, Action<ResponseBase> errorCallback)
        {
            await APIServiceProvider.ExecuteWithUrlEncode(param, Method.POST, AppConst.URL_GetSaveformJ, successCallback, errorCallback);
        }

        public async Task GetFormJByHeaderid(RestRequest param, Action<FormJByHeader> successCallback, Action<ResponseBase> errorCallback)
        {
            await APIServiceProvider.ExecuteWithOutBody(param, Method.POST, AppConst.URL_HeaderBYID, successCallback, errorCallback);
        }

        public async Task GetFormJByUserList(Action<JAssetDropDown> successCallback, Action<ResponseBase> errorCallback)
        {
            await APIServiceProvider.ExecuteWithOutBody(new RestRequest(), Method.GET, AppConst.URL_Userlist, successCallback, errorCallback);
        }

        public async Task GetFormJUserDetail(string param, Action<UserDataResponse> successCallback, Action<ResponseBase> errorCallback)
        {
            await APIServiceProvider.ExecuteWithUrlEncode(param, Method.POST, AppConst.URL_UserDetail, successCallback, errorCallback);
        }

        public async Task SaveSignature(string param, Action<GetSerialNo> successCallback, Action<ResponseBase> errorCallback)
        {
            await APIServiceProvider.ExecuteWithUrlEncode(param, Method.POST, AppConst.URL_SaveSignature, successCallback, errorCallback);
        }

        public async Task DeleteHeaderRequest(RestRequest param, Action<GetSerialNo> successCallback, Action<ResponseBase> errorCallback)
        {
            await APIServiceProvider.ExecuteWithOutBody(param, Method.POST, AppConst.URL_DeleteHeader, successCallback, errorCallback);
        }

        public async Task DeleteDetailRequest(RestRequest param, Action<GetSerialNo> successCallback, Action<ResponseBase> errorCallback)
        {
            await APIServiceProvider.ExecuteWithOutBody(param, Method.POST, AppConst.URL_DeleteDetail, successCallback, errorCallback);
        }
    }
}
