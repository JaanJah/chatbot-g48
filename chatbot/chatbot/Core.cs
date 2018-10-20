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
        public static async Task<Properties> GetData()
        {
            dynamic results = await DataService.GetDataFromService();
            //ask properties from results
            if (results[0] != null)
            {
                //System.ArgumentException: Accessed JArray values with invalid key value: "property".Int32 array index expected.
                Properties property = new Properties();
                property.Message = (string)results[0]["text"];
                return property;
            }
            else
            {
                return null;
            }
        }
    }
}