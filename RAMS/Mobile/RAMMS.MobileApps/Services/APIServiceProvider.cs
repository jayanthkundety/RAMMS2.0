using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using RAMMS.MobileApps;
using RAMMS.MobileApps.Services;

namespace RAMMS.MobileApps
{
    public static class APIServiceProvider
    {
        public static RestRequest GetRestRequest(Method method)
        {
            var request = new RestRequest(method);           

            return request;
        }
        public static async Task ExecuteWithOutBody<T>(RestRequest restRequest, Method method, string url, Action<T> successCallback, Action<ResponseBase> errorCallback) where T : new()
        {
            try
            {
                IRestResponse<T> response = null;
                url = AppConst.DevApiBaseAddress + url;
                var client = new RestClient(url);
                restRequest.Method = method;
                response = await client.ExecuteAsync<T>(restRequest);
                Console.WriteLine(response.Data);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Debug.WriteLine("Response:\nUrl:>> " + url + "\n Method:>>>" + restRequest.Method.ToString() + "\n" + JsonConvert.SerializeObject(response));
                    successCallback?.Invoke(response.Data);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.RequestTimeout)
                {
                    Debug.WriteLine("Response:\nUrl:>> " + url + "\n Method:>>>" + restRequest.Method.ToString() + "\n" + JsonConvert.SerializeObject(response));
                    errorCallback?.Invoke(new ResponseBase() { errorMessage = "Time Out", success = false });
                }
                else
                {
                    errorCallback?.Invoke(new ResponseBase() { errorMessage = "something went wrong", success = false });
                }

            }
            catch(Exception ex)
            {
                errorCallback?.Invoke(new ResponseBase() { errorMessage = "something went wrong", success = false });
            }
            
        }
         public static async Task ExecuteWithUrlEncode<T>(string queryString,Method method, string url, Action<T> successCallback, Action<ResponseBase> errorCallback) where T : new()
        {


            IRestResponse<T> response = null;
            try
            {
                RestRequest restRequest = new RestRequest();
                restRequest.Method = method;
                restRequest.AddHeader("Content-Type", "application/json");
                restRequest.AddParameter("application/json", queryString, ParameterType.RequestBody);

                url = AppConst.DevApiBaseAddress + url;
                var client = new RestClient(url);

                response = await client.ExecuteAsync<T>(restRequest);
                Console.WriteLine(response.Data);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Debug.WriteLine("Response:\nUrl:>> " + url + "\n Method:>>>" + restRequest.Method.ToString() + "\n" + JsonConvert.SerializeObject(response));
                    successCallback?.Invoke(response.Data);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.RequestTimeout)
                {
                    Debug.WriteLine("Response:\nUrl:>> " + url + "\n Method:>>>" + restRequest.Method.ToString() + "\n" + JsonConvert.SerializeObject(response));
                    errorCallback?.Invoke(new ResponseBase() { errorMessage = "Time Out", success = false });
                }
                else
                {
                    errorCallback?.Invoke(new ResponseBase() { errorMessage = "something went wrong", success = false });
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Execption:"+ex);
                errorCallback?.Invoke(new ResponseBase() { errorMessage = "something went wrong", success = false });

            }
        }

        public static async Task Execute<T>( string url, Method method, Action<T> successCallback, Action<ResponseBase> errorCallback) where T : new()
        {
            IRestResponse<T> response = null;
            try
            {
                   url = AppConst.DevApiBaseAddress + url;
                    var client = new RestClient(url);

                RestRequest restRequest = new RestRequest();
                restRequest.Method = method; ;
                response = await client.ExecuteAsync<T>(restRequest);
                Console.WriteLine(response.Data);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Debug.WriteLine("Response:\nUrl:>> " + url + "\n Method:>>>" + restRequest.Method.ToString() + "\n" + JsonConvert.SerializeObject(response));
                    successCallback?.Invoke(response.Data);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.RequestTimeout)
                {
                    Debug.WriteLine("Response:\nUrl:>> " + url + "\n Method:>>>" + restRequest.Method.ToString() + "\n" + JsonConvert.SerializeObject(response));
                    errorCallback?.Invoke(new ResponseBase () { errorMessage = "Time Out" , success = false });
                }
                else
                {
                    errorCallback?.Invoke(new ResponseBase() { errorMessage = "something went wrong", success = false });
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                errorCallback?.Invoke(new ResponseBase() { errorMessage = "something went wrong", success = false });

            }
        }

        public static bool IsJson(this string input)
        {
            if (String.IsNullOrEmpty(input) || String.IsNullOrWhiteSpace(input))
                return false;

            input = input.Trim();
            return input.StartsWith("{") && input.EndsWith("}")
                   || input.StartsWith("[") && input.EndsWith("]");
        }

        public static void ParseLocalJson<T>(string jsonFileName, Action<T> successCallback, Action<ResponseBase> errorCallback) where T : new()
        {
#if __ANDROID__

            var file = Android.App.Application.Context.Assets.Open(jsonFileName);

            try
            {
                using (var streamReader = new StreamReader(file, Encoding.UTF8))
                {
                    var text = streamReader.ReadToEnd();

                    streamReader.Close();

                    var obj = JsonConvert.DeserializeObject<T>(text);

                    successCallback?.Invoke(obj);
                }
            }
            catch (Exception ex)
            {
                errorCallback?.Invoke(new ResponseBase { Status = 0, Message = "Sorry! Something went wrong." });
            }

#endif
        }
    }
}