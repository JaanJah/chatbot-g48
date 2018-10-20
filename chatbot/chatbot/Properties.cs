using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;

namespace chatbot
{
    public class Properties
    {
        //Properties of JSON here
        [JsonProperty("message")]
        public string Message { get; set; }
        public static List<string> ReceivedMessages = new List<string>();
        public static List<string> SentMessages = new List<string>();
    }
}