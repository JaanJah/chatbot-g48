using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System.Linq;
using System.Collections.Generic;

namespace chatbot
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : Activity
    {
        private List<string> convert = new List<string>();
        private List<string> convert2 = new List<string>();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            GetDataAndAssignToText();
            var sendButton = FindViewById<Button>(Resource.Id.button1);
            sendButton.Click += SendButton_Click;
        }

        private void SendButton_Click(object sender, System.EventArgs e)
        {
            var list = FindViewById<ListView>(Resource.Id.list);
            var message = FindViewById<EditText>(Resource.Id.textInputEditText1);
            convert.Add(message.Text);
            Properties.SentMessages = convert.ToArray();
            convert2.Add("");
            Properties.ReceivedMessages = convert2.ToArray();
            list.Adapter = new ChatAdapter(this, Properties.SentMessages, Properties.ReceivedMessages);
            //list.SetSelection()
        }

        private async void GetDataAndAssignToText()
        {
            Properties propertyData = await Core.GetData();
            //Doesn't call any of these statements below, means something wrong with Core.GetData() function.
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
                FindViewById<TextView>(Resource.Id.botMessage).Text = "Couldn't get property data.";
            }
        }
    }
}