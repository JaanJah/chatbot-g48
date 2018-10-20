using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System.Linq;

namespace chatbot
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : ListActivity
    {
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
            var message = FindViewById<EditText>(Resource.Id.textInputEditText1);
            Properties.ReceivedMessages.Add("test");
            Properties.SentMessages.Add(message.Text);
            var UpdateChat = new ChatAdapter(this, Properties.SentMessages, Properties.ReceivedMessages);
            SetContentView(Resource.Layout.test);
        }

        private async void GetDataAndAssignToText()
        {
            Properties propertyData = await Core.GetData();
            //Doesn't call any of these statements below, means something wrong with Core.GetData() function.
            if (propertyData != null)
            {
                //FindViewById<TextView>(Resource.Id.botMessage).Text = propertyData.Message;
                Properties.ReceivedMessages.Add(propertyData.Message);
            }
            else
            {
                FindViewById<TextView>(Resource.Id.botMessage).Text = "Couldn't get property data.";
            }
        }
    }
}