using System;
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

namespace chatbot
{
    [Activity(Label = "AISee", Theme = "@style/AppTheme", MainLauncher = true, Icon = "@drawable/logo_icon3")]
    public class MainActivity : AppCompatActivity
    {
        public int firstMessage = 0;
        EditText inputText;
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
                firstMessage++;
                hiddenFirstMessage();
            }
            sendInputBtn.Click += SendInputBtn_Click;
        }

        private async void hiddenFirstMessage()
        {
            firstMessage++;
            inputText.Text = "hi";
            var sText = inputText.Text;
            Properties propertyData = await Core.GetData(sText);
            if (propertyData != null)
            {
                FindViewById<TextView>(Resource.Id.botMessage).Text = propertyData.Message;
            }
            else
            {
                FindViewById<TextView>(Resource.Id.botMessage).Text = "Couldn't get property data.";
            }
            inputText.Text = "";
        }
        private async void SendInputBtn_Click(object sender, System.EventArgs e)
        {
            inputText.Text = "";
            var sText = inputText.Text;
            //Console.WriteLine(sText);
            Properties propertyData = await Core.GetData(sText);
            if (propertyData != null)
            {
                FindViewById<TextView>(Resource.Id.botMessage).Text = propertyData.Message;
            }
            else
            {
                FindViewById<TextView>(Resource.Id.botMessage).Text = "Couldn't get property data.";
            }
        }
    }
}