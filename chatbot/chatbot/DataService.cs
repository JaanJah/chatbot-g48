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
        public static async Task<dynamic> GetDataFromService()
        {
            HttpClient client = new HttpClient();
            var payload = new Properties
            {
                Message = "hi"
            };
            var stringPayload = await Task.Run(() => JsonConvert.SerializeObject(payload));
            var url = "http://10.201.113.130:5007/webhooks/rest/webhook";
           
            //Microsoft.CSharp.RuntimeBinder.RuntimeBinderException: 'Newtonsoft.Json.JsonObjectAttribute' does not contain a definition for 'Data'
            var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");

            dynamic data = null;
            using (client)
            {
                var httpResponse = await client.PostAsync(url, content);
                if (httpResponse.Content != null)
                {
                    var responseContent = await httpResponse.Content.ReadAsStringAsync();
                    data = JsonConvert.DeserializeObject(responseContent);
                    // From here on you could deserialize the ResponseContent back again to a concrete C# type using Json.Net
                }
            }
            return data;

        }
    }
}