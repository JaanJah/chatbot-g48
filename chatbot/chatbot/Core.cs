using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace chatbot
{
    public class Core
    {
        public static async Task<Properties> GetData()
        {
            string queryString = "Request data from this address";

            dynamic results = await DataService.GetDataFromService(queryString).ConfigureAwait(false);
            //ask properties from results

            if (results["property"] != null)
            {
                Properties property = new Properties();
                //property asks properties from Propertis class
                return property;
            }
            else
            {
                return null;
            }
        }
    }
}