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
        private List<string> convert = new List<string>();
        private List<string> convert2 = new List<string>();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            var sendInputBtn = FindViewById<Button>(Resource.Id.sendInputBtn);
            inputText = FindViewById<EditText>(Resource.Id.inputMessage);
            //GetDataAndAssignToText();
            if (firstMessage == 0)
            {
                HiddenFirstMessage();
            }
            sendInputBtn.Click += SendInputBtn_Click;
        }
        private async void HiddenFirstMessage()
        {
            firstMessage++;
            //inputText.Text = "hi";
            var sText = "hi";
            Properties propertyData = await Core.GetData(sText);
            if (propertyData != null)
            {
                var list = FindViewById<ListView>(Resource.Id.list);
                convert2.Add(propertyData.Message);
                Properties.ReceivedMessages = convert2.ToArray();
                convert.Add("");
                Properties.SentMessages = convert.ToArray();
                list.Adapter = new ChatAdapter(this, Properties.SentMessages, Properties.ReceivedMessages);
            }
            else
            {
                var list = FindViewById<ListView>(Resource.Id.list);
                convert2.Add("Couldn't get property data");
                Properties.ReceivedMessages = convert2.ToArray();
                convert.Add("");
                Properties.SentMessages = convert.ToArray();
                list.Adapter = new ChatAdapter(this, Properties.SentMessages, Properties.ReceivedMessages);
            }
            inputText.Text = "";
        }
        private async void SendInputBtn_Click(object sender, System.EventArgs e)
        {
            var list = FindViewById<ListView>(Resource.Id.list);
            var message = FindViewById<EditText>(Resource.Id.inputMessage);
            var sText = inputText.Text;
            inputText.Text = "";
            convert.Add(sText);
            convert2.Add("");
            Properties.SentMessages = convert.ToArray();
            Properties.ReceivedMessages = convert2.ToArray();
            list.Adapter = new ChatAdapter(this, Properties.SentMessages, Properties.ReceivedMessages);
            Properties propertyData = await Core.GetData(sText);
            if (propertyData != null)
            {
                convert.Add("");
                convert2.Add(propertyData.Message);
                Properties.ReceivedMessages = convert2.ToArray();
                Properties.SentMessages = convert.ToArray();
                list.Adapter = new ChatAdapter(this, Properties.SentMessages, Properties.ReceivedMessages);
            }
            else
            {
                FindViewById<TextView>(Resource.Id.botMessage).Text = "Couldn't get property data.";
            }
        }
    }
}