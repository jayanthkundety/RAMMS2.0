using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Plugin.Connectivity;

namespace RAMMS.MobileApps
{
    public static class  DropDownModel
    {
    //  static  IRestApi _restApi;
        static ObservableCollection<DDListItems> DDRodeCodeListItems { get; set; }

        static ObservableCollection<DDListItems> DDRMUListItems { get; set; }

        static ObservableCollection<DDListItems> DDAssetTypeListItems { get; set; }


        public static void GetDropDownValue(string ddtype, IRestApi restApi)
        {
            var dfdf = GetddListDetails(ddtype, restApi);
        }
        public static async Task<ObservableCollection<DDListItems>> GetddListDetails(string ddtype, IRestApi _restApi)
        {
            try
            {
                ObservableCollection<DDListItems> MonthListItems;
                //  _userDialogs.ShowLoading("Loading");
                if (CrossConnectivity.Current.IsConnected)
                {

                    var ddlist = new DDLookUpDTO()
                    {
                        Type = ddtype,
                    };

                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(ddlist);
                  
                    var response = await _restApi.GetDDList(ddlist);

                    if (response.success)
                    {
                        if (ddtype == "RD_Code")
                        {
                            DDRodeCodeListItems = new ObservableCollection<DDListItems>(response.data);

                            return DDRodeCodeListItems;

                        }
                        else if (ddtype == "RMU")
                        {
                            DDRMUListItems = new ObservableCollection<DDListItems>(response.data);

                            return DDRMUListItems;
                        }
                        else if (ddtype == "Asset Type")
                        {
                            DDAssetTypeListItems = new ObservableCollection<DDListItems>(response.data);

                            return DDAssetTypeListItems;
                        }else if(ddtype.Equals("Month"))
                        {
                           MonthListItems = new ObservableCollection<DDListItems>(response.data);
                            return MonthListItems;
                        }
                        else if (ddtype.Equals("Year"))
                        {
                            var year = new ObservableCollection<DDListItems>(response.data);
                            return year;
                        }
                    }
                    else
                        UserDialogs.Instance.Alert(response.errorMessage);

                }
                else
                    UserDialogs.Instance.Alert("Please check your Internet Connection !");
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.Alert(ex.Message);
            }
            finally
            {
                UserDialogs.Instance.HideLoading();
            }
            return new ObservableCollection<DDListItems>();
        }
    }
}
