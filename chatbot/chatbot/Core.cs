using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json.Linq;

namespace chatbot
{
    public class Core
    {
        public static bool lastMessage = false;
        public static async Task<Properties> GetData(string sText, string rndSender)
        {    
            dynamic results = await DataService.GetDataFromService(sText, rndSender);
            //ask properties from results
            if (results[0] != null)
            {
                Properties property = new Properties();
                property.Message = (string)results[0]["text"];
                if (property.Message == "Goodbye")
                {
                    lastMessage = true;
                }
                return property;
            }
            else
            {
                return null;
            }
        }
    }
}