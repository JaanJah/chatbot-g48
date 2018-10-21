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
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static async Task<dynamic> GetDataFromService(string sText)
        {
            var payload = new Properties
            {
                Sender = RandomString(5),
                Message = sText
            };
            HttpClient client = new HttpClient();
            if(Core.lastMessage == true)
            {
                payload = new Properties
                {
                    Sender = RandomString(5),
                    Message = "/LASTREQUEST"
                };
            }

            var stringPayload = await Task.Run(() => JsonConvert.SerializeObject(payload));
            var url = "http://10.201.113.130:5007/webhooks/rest/webhook";
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