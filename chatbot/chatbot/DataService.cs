using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace chatbot
{
    public class DataService
    {
        public static async Task<dynamic> GetDataFromService(string sText)
        {
            
            HttpClient client = new HttpClient();
            var payload = new Properties
            {
                Message = sText
            };
            var stringPayload = await Task.Run(() => JsonConvert.SerializeObject(payload));
            var url = "http://10.201.113.49:5005/webhooks/rest/webhook";
            var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

            dynamic data = null;
            using (client)
            {
                var httpResponse = await client.PostAsync(url, content);
                if (httpResponse.Content != null)
                {
                    var responseContent = await httpResponse.Content.ReadAsStringAsync();
                    data = JsonConvert.DeserializeObject(responseContent);
                }
            }
            return data;
        }
    }
}