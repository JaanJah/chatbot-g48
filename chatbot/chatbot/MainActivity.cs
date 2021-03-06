﻿using System;
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System.Drawing;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Linq;
using System.Collections.Generic;

namespace chatbot
{
    [Activity(Label = "AISee", Theme = "@style/AppTheme", MainLauncher = true, Icon = "@drawable/logo_icon3")]
    public class MainActivity : AppCompatActivity
    {
        public int firstMessage = 0;
        EditText inputText;
        ImageButton sendInputBtn;
        string rndSender;
        private List<string> convert = new List<string>();
        private List<string> convert2 = new List<string>();
        // true = human, false = bot response
        private List<bool> convertSender = new List<bool>();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            sendInputBtn = FindViewById<ImageButton>(Resource.Id.sendInputBtn);
            inputText = FindViewById<EditText>(Resource.Id.inputMessage);
            if (firstMessage == 0)
            {
                rndSender = RandomString(5);
                HiddenFirstMessage();
            }
            sendInputBtn.Click += SendInputBtn_Click;
        }
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        private async void HiddenFirstMessage()
        {
            firstMessage++;
            var sText = "hi";
            Properties propertyData = await Core.GetData(sText, rndSender);
            if (propertyData != null)
            {
                var list = FindViewById<ListView>(Resource.Id.list);
                convert.Add("");
                convert2.Add(propertyData.Message);
                convertSender.Add(false);
                Properties.ReceivedMessages = convert2.ToArray();
                Properties.SentMessages = convert.ToArray();
                Properties.SenderArray = convertSender.ToArray();
                list.Adapter = new ChatAdapter(this, Properties.SentMessages, Properties.ReceivedMessages, Properties.SenderArray);
            }
            else
            {
                var list = FindViewById<ListView>(Resource.Id.list);
                convert.Add("");
                convert2.Add("Couldn't get property data");
                convertSender.Add(false);
                Properties.ReceivedMessages = convert2.ToArray();
                Properties.SentMessages = convert.ToArray();
                Properties.SenderArray = convertSender.ToArray();
                list.Adapter = new ChatAdapter(this, Properties.SentMessages, Properties.ReceivedMessages, Properties.SenderArray);
            }
            inputText.Text = "";
        }
        private async void SendInputBtn_Click(object sender, System.EventArgs e)
        {
            sendInputBtn.SetImageResource(Resource.Drawable.nupp1_2);
            var list = FindViewById<ListView>(Resource.Id.list);
            var message = FindViewById<EditText>(Resource.Id.inputMessage);
            var sText = inputText.Text;
            inputText.Text = "";
            convert.Add(sText);
            convert2.Add("");
            convertSender.Add(true);
            Properties.SentMessages = convert.ToArray();
            Properties.ReceivedMessages = convert2.ToArray();
            Properties.SenderArray = convertSender.ToArray();
            list.Adapter = new ChatAdapter(this, Properties.SentMessages, Properties.ReceivedMessages, Properties.SenderArray);
            Properties propertyData = await Core.GetData(sText, rndSender);
            if (propertyData != null)
            {
                convert.Add("");
                convert2.Add(propertyData.Message);
                convertSender.Add(false);
                Properties.ReceivedMessages = convert2.ToArray();
                Properties.SentMessages = convert.ToArray();
                Properties.SenderArray = convertSender.ToArray();
                list.Adapter = new ChatAdapter(this, Properties.SentMessages, Properties.ReceivedMessages, Properties.SenderArray);
                sendInputBtn.SetImageResource(Resource.Drawable.nupp2_2);
            }
            else
            {
                convertSender.Add(false);
                convert2.Add("Couldn't get property data");
                convert.Add("");
                Properties.SentMessages = convert.ToArray();
                Properties.ReceivedMessages = convert2.ToArray();
                Properties.SenderArray = convertSender.ToArray();
                list.Adapter = new ChatAdapter(this, Properties.SentMessages, Properties.ReceivedMessages, Properties.SenderArray);
            }
        }
    }
}