using Acr.UserDialogs;
using ModernHttpClient;
using Plugin.Connectivity;
using Refit;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace RAMMS.MobileApps
{
    public class AuthenticatedHttpClientHandler : NativeMessageHandler
    {
        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                // var auth = request.Headers.Authorization;

                // request.Headers.Add("Content-Type", "application/json");

                //if (auth != null)
                //{
                //   //// request.Headers.Add("Content-Type", "application/json");
                //}
                
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", App.AuthToken);


                var url = request.RequestUri;
                               
                var response = await base.SendAsync(request, cancellationToken);

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    throw new UnauthorizedAccessException("Authentication error");

                if (response.StatusCode == System.Net.HttpStatusCode.RequestTimeout)
                    await UserDialogs.Instance.AlertAsync("Service TimeOut.. Please try again..");

                return response;
            }
            catch (Exception ex)
            {
                if (ex is UnauthorizedAccessException)
                {
                    throw new UnauthorizedAccessException("Authentication error");
                }

                if (ex is ApiException)
                {
                    throw;
                }
                if (ex is OperationCanceledException)
                {
                    throw new OperationCanceledException("Service TimeOut.. Please check your internet connection and try again..");
                }
                if (!CrossConnectivity.Current.IsConnected)
                {
                    throw new WebException("No internet connection at the moment");
                }
                throw;
            }
        }
    }
}