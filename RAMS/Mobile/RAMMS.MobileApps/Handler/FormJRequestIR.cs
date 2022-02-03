using System;
using RAMMS.MobileApps.Interface;
using RestSharp;
using RAMMS.MobileApps;

namespace RAMMS.MobileApps
{
    public class FormJRequestIR
    {
        IFromJDropDown G_formJRequest;
        public FormJRequestIR(IFromJDropDown formJRequest)
        {
            G_formJRequest = formJRequest;
        }
        public async void LandingDropDownSucess(string restRequest)
        {
            await new FormJRequest().GetFormJLandingDropDown(restRequest, (response) =>
            {
                if (response.success)
                {
                    G_formJRequest?.GetDropDownDetailResponse(response);
                }
                else
                {
                    G_formJRequest?.APIHitFailed(response.errorMessage ?? "");
                }

            }, (error) =>
            {
                G_formJRequest?.APIHitFailed(error.errorMessage);

            });
        }

    }
    public class FormAssetIR
    {
        IFromJAssetDrop G_fromJAssetDrop;
        public FormAssetIR(IFromJAssetDrop fromJAssetDrop)
        {
            G_fromJAssetDrop = fromJAssetDrop;
        }
        public async void LandingAssetDropDownSucess(string restRequest)
        {
            await new FormJRequest().GetFormJLandingAssetDropDown(restRequest, (response) =>
            {
                if (response.success)
                {
                    G_fromJAssetDrop?.GetDropDownJDetailResponse(response);
                }
                else
                {
                    G_fromJAssetDrop?.APIHitFailed(response.errorMessage ?? "");
                }

            }, (error) =>
            {
                G_fromJAssetDrop?.APIHitFailed(error.errorMessage);

            });
        }
        public async void LandingMonthDropDownSucess(string restRequest)
        {
            await new FormJRequest().GetFormJLandingMonthDropDown(restRequest, (response) =>
            {
                if (response.success)
                {
                    G_fromJAssetDrop?.GetDropDownJMonthResponse(response);
                }
                else
                {
                    G_fromJAssetDrop?.APIHitFailed(response.errorMessage ?? "");
                }

            }, (error) =>
            {
                G_fromJAssetDrop?.APIHitFailed(error.errorMessage);

            });
        }
        public async void LandingYearDropDownSucess(string restRequest)
        {
            await new FormJRequest().GetFormJLandingYearDropDown(restRequest, (response) =>
            {
                if (response.success)
                {
                    G_fromJAssetDrop?.GetDropDownJYearResponse(response);
                }
                else
                {
                    G_fromJAssetDrop?.APIHitFailed(response.errorMessage ?? "");
                }

            }, (error) =>
            {
                G_fromJAssetDrop?.APIHitFailed(error.errorMessage);

            });
        }

    }
    public class FormJAdd
    {
        IFormJAdd G_FormJAdd;
        public FormJAdd(IFormJAdd formJAdd)
        {
            G_FormJAdd = formJAdd;
        }
        public async void FormJAddRoadCodeDropDownSucess(string restRequest)
        {
            await new FormJRequest().GetFormJAddRoadCodeDropDown(restRequest, (response) =>
            {
                if (response.success)
                {
                    G_FormJAdd?.GetFormJAddRoadCodeResponse(response);
                }
                else
                {
                    G_FormJAdd?.APIHitFailed(response.errorMessage ?? "");
                }

            }, (error) =>
            {
                G_FormJAdd?.APIHitFailed(error.errorMessage);

            });
        }
        public async void FormJAddHeaderid(RestRequest restRequest)
        {
            await new FormJRequest().GetFormJByHeaderid(restRequest, (response) =>
            {
                if (response.success)
                {
                    G_FormJAdd?.GetFormJHeaderCodeResponse(response);
                }
                else
                {
                    G_FormJAdd?.APIHitFailed(response.errorMessage ?? "");
                }

            }, (error) =>
            {
                G_FormJAdd?.APIHitFailed(error.errorMessage);

            });
        }
        public async void FormJUsertListResponse()
        {
            await new FormJRequest().GetFormJByUserList((response) =>
            {
                if (response.success)
                {
                    G_FormJAdd?.GetFormJUserList(response);
                }
                else
                {
                    G_FormJAdd?.APIHitFailed(response.errorMessage ?? "");
                }

            }, (error) =>
            {
                G_FormJAdd?.APIHitFailed(error.errorMessage);

            });
        }
        public async void FormJUserDetailResponse(string param)
        {
            await new FormJRequest().GetFormJUserDetail(param, (response) =>
            {
                if (response.success)
                {
                    G_FormJAdd?.GetFormJUserDetail(response);
                }
                else
                {
                    G_FormJAdd?.APIHitFailed(response.errorMessage ?? "");
                }

            }, (error) =>
            {
                G_FormJAdd?.APIHitFailed(error.errorMessage);

            });
        }
        public async void SaveSignature(string param)
        {
            await new FormJRequest().SaveSignature(param, (response) =>
            {
                if (response.success)
                {
                    G_FormJAdd?.GetSerialNo(response);
                }
                else
                {
                    G_FormJAdd?.APIHitFailed(response.errorMessage ?? "");
                }

            }, (error) =>
            {
                G_FormJAdd?.APIHitFailed(error.errorMessage);

            });
        }

    }

    public class FormLandingGridIR
    {
        IFromJLandingGrid G_fromJAssetDrop;
        public FormLandingGridIR(IFromJLandingGrid fromJAssetDrop)
        {
            G_fromJAssetDrop = fromJAssetDrop;
        }
        public async void LandingGridDropDownSucess(string restRequest)
        {
            await new FormJRequest().GetFormJLandingGrid(restRequest, (response) =>
            {
                if (response.success)
                {
                    G_fromJAssetDrop?.GetFromJLandingGridResponse(response);
                }
                else
                {
                    G_fromJAssetDrop?.APIHitFailed(response.errorMessage ?? "");
                }

            }, (error) =>
            {
                G_fromJAssetDrop?.APIHitFailed(error.errorMessage);

            });
        }
    }
    public class FormJSelectedRoadCode
    {
        IFromJSelectedRoadCode G_fromJAssetDrop;
        public FormJSelectedRoadCode(IFromJSelectedRoadCode fromJAssetDrop)
        {
            G_fromJAssetDrop = fromJAssetDrop;
        }
        public async void LandingFormJSelectedRoadCode(string restRequest)
        {
            await new FormJRequest().GetFormJAddSelectedRoadCodeDropDown(restRequest, (response) =>
            {
                if (response.success)
                {
                    G_fromJAssetDrop?.GetFromJLandingGridResponse(response);
                }
                else
                {
                    G_fromJAssetDrop?.APIHitFailed(response.errorMessage ?? "");
                }

            }, (error) =>
            {
                G_fromJAssetDrop?.APIHitFailed(error.errorMessage);

            });
        }
        public async void FormJGetRefrenceNumber(RestRequest restRequest)
        {
            await new FormJRequest().GetFormJAddRefrencenumber(restRequest, (response) =>
            {
                if (response.success)
                {
                    G_fromJAssetDrop?.GetRefrenceNumber(response);
                }
                else
                {
                    G_fromJAssetDrop?.APIHitFailed(response.errorMessage ?? "");
                }

            }, (error) =>
            {
                G_fromJAssetDrop?.APIHitFailed(error.errorMessage);

            });
        }
        public async void FormJFindDetailREsponse(string param)
        {
            await new FormJRequest().GetFormJFindDetail(param, (response) =>
            {
                if (response.success)
                {
                    G_fromJAssetDrop?.GetFindDetails(response);
                }
                else
                {
                    G_fromJAssetDrop?.APIHitFailed(response.errorMessage ?? "");
                }

            }, (error) =>
            {
                G_fromJAssetDrop?.APIHitFailed(error.errorMessage);

            });
        }
        public async void FormJGridDetailREsponse(string param)
        {
            await new FormJRequest().GetFormJADDGridDetail(param, (response) =>
            {
                if (response.success)
                {
                    G_fromJAssetDrop?.GetFormjADDGridDEtail(response);
                }
                else
                {
                    G_fromJAssetDrop?.APIHitFailed(response.errorMessage ?? "");
                }

            }, (error) =>
            {
                G_fromJAssetDrop?.APIHitFailed(error.errorMessage);

            });
        }

    }
    public class FormJOne
    {
        IFormJOne _formJOne;
        public FormJOne(IFormJOne formJOne)
        {
            _formJOne = formJOne;
        }

        public async void GetSerialNo(RestRequest param)
        {
            await new FormJRequest().GetFormJOneJDtlById(param, (response) =>
            {
                if (response.success)
                {
                    _formJOne?.GetFormSerialNo(response);
                }
                else
                {
                    _formJOne?.APIHitFailed(response.errorMessage ?? "");
                }

            }, (error) =>
            {
                _formJOne?.APIHitFailed(error.errorMessage);

            });
        }
        public async void GetLocationNo(string param)
        {
            await new FormJRequest().GetFormJLandingAssetDropDown(param, (response) =>
            {
                if (response.success)
                {
                    _formJOne?.GetDropDownLocationDetailResponse(response);
                }
                else
                {
                    _formJOne?.APIHitFailed(response.errorMessage ?? "");
                }

            }, (error) =>
            {
                _formJOne?.APIHitFailed(error.errorMessage);

            });
        }
        public async void GetPrioritylNo(string param)
        {
            await new FormJRequest().GetFormJLandingAssetDropDown(param, (response) =>
            {
                if (response.success)
                {
                    _formJOne?.GetDropDownPriorityResponse(response);
                }
                else
                {
                    _formJOne?.APIHitFailed(response.errorMessage ?? "");
                }

            }, (error) =>
            {
                _formJOne?.APIHitFailed(error.errorMessage);

            });
        }

        public async void GetAssetNameJ(RestRequest param)
        {
            await new FormJRequest().GetFormNameJ(param, (response) =>
            {
                if (response.success)
                {
                    _formJOne?.GetFormAssetCodeNo(response);
                }
                else
                {
                    _formJOne?.APIHitFailed(response.errorMessage ?? "");
                }

            }, (error) =>
            {
                _formJOne?.APIHitFailed(error.errorMessage);

            });
        }
        public async void GetDefectCodeNameJ(RestRequest param)
        {
            await new FormJRequest().GetFormNameJDefectCode(param, (response) =>
            {
                if (response.success)
                {
                    _formJOne?.GetFormAssetDefectCode(response);
                }
                else
                {
                    _formJOne?.APIHitFailed(response.errorMessage ?? "");
                }

            }, (error) =>
            {
                _formJOne?.APIHitFailed(error.errorMessage);

            });
        }
        public async void SaveResponse(string param)
        {
            await new FormJRequest().GetSaveFormJ(param, (response) =>
            {
                if (response.success)
                {
                    _formJOne?.GetSaveDetil(response);
                }
                else
                {
                    _formJOne?.APIHitFailed(response.errorMessage ?? "");
                }

            }, (error) =>
            {
                _formJOne?.APIHitFailed(error.errorMessage);

            });
        }
    }

    public class FormDelete
    {
        IFormDelete _formDelete;
        public FormDelete(IFormDelete formDelete)
        {
            _formDelete = formDelete;
        }
        public async void DeteleHeaderAndDetail(RestRequest restRequest)
        {
            await new FormJRequest().DeleteHeaderRequest(restRequest, (response) =>
            {
                _formDelete?.DeleteHeader(response)
;
            }, (error) =>
            {
                _formDelete?.APIHitFailed(error.errorMessage);
            });
        }
        public async void DeteleDetail(RestRequest restRequest)
        {
            await new FormJRequest().DeleteDetailRequest(restRequest, (response) =>
            {
                _formDelete?.DeleteHeader(response)
;
            }, (error) =>
            {
                _formDelete?.APIHitFailed(error.errorMessage);
            });
        }


    }

}
