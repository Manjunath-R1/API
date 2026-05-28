using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using ThoughtFocus.Business.Interfaces.SMS;
using ThoughtFocus.Domain.Common;

namespace ThoughtFocus.Common.Utilities
{
    public class SendSMS : ISendSMS
    {
        #region Fields

        /// <summary>
        /// ILog instance for logging.
        /// </summary>
        private static ILogger<SendSMS> Logger;

        #endregion Fields

        public SendSMS(ILogger<SendSMS> logger)
        {
            Logger = logger;
        }

        #region Methods

        public bool SendSMSByGetAPI(string to,string message, SMSSettings smsSettings)
        {
            Logger.LogDebug("Entered Send method");
            bool isSMSSent = false;
            try
            {
                string user = smsSettings.User;
                string url = smsSettings.GetApiURL;
                string from = smsSettings.From;
                string mo = smsSettings.Mo;
                string passWord = smsSettings.Password;
                string api_id = smsSettings.API_ID;


                string baseurl = url + "?api_id=" + api_id + "&user=" + user + "&from=" + from + "&mo=" + mo + "&password=" + passWord + "&to=" + to + "&text=" + message;
                using (var client = new HttpClient())
                {
                    //Passing service base url
                    client.DefaultRequestHeaders.Clear();
                    //Define request data format
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //Sending request to find web api REST service resource GetAllEmployees using HttpClient
                    HttpResponseMessage Res = client.GetAsync(baseurl).Result;
                    //Checking the response is successful or not which is sent using HttpClient
                    if (Res.IsSuccessStatusCode)
                    {
                        isSMSSent = true;
                    }


                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message.ToString());
                isSMSSent = false;
            }
            return isSMSSent;
        }


        public bool SendSMSNotificationsByPostAPI(string to, string message, SMSSettings smsSettings)
        {
            Logger.LogDebug("Entered Send method");
            bool isSMSSent = false;
            try
            {
                string url = smsSettings.PostApiURL;
                string mo = smsSettings.Mo;
                string token = smsSettings.Token;
                string xVersion = smsSettings.XVersion;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    client.DefaultRequestHeaders.Add("Authorization", "bearer " + token);
                    client.DefaultRequestHeaders.Add("X-Version", xVersion);
                    client.DefaultRequestHeaders.Add("Accept", "application/json");

                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);
                    request.Content = new StringContent("{\"text\":\"" + message + "\",\"to\":[" + to + "]}",
                                                        Encoding.UTF8,
                                                        "application/json");//CONTENT-TYPE header

                    client.SendAsync(request)
                          .ContinueWith(responseTask =>
                          {
                             
                              if (responseTask.Result.StatusCode == System.Net.HttpStatusCode.Accepted)
                              {
                                  isSMSSent = false;
                              }
                              else if (responseTask.Result.StatusCode == System.Net.HttpStatusCode.OK)
                              {
                                  isSMSSent = true;
                                 
                              }
                          });
                   

                }
            
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.Message.ToString());
                isSMSSent = false;
            }
            return isSMSSent;
        }
        #endregion Methods
    }
}
