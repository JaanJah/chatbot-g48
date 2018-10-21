using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using static Android.App.ActionBar;

namespace chatbot
{
    class ChatAdapter : BaseAdapter<string>
    {
        string[] sent;
        string[] received;
        bool[] sender;
        Activity context;
        public ChatAdapter(Activity context, string[] sent, string[] received, bool[] sender) : base()
        {
            this.context = context;
            this.sent = sent;
            this.received = received;
            this.sender = sender;
        }

        public override string this[int position]
        {
            get { return sent[position]; }
        }

        public override int Count { get { return sent.Length; } }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView; // re-use an existing view, if one is available
            if (view == null) // otherwise create a new one
                view = context.LayoutInflater.Inflate(Resource.Layout.CustomAdapter, null);
            view.FindViewById<TextView>(Resource.Id.textView1).Text = sent[position] + "  ";
            view.FindViewById<TextView>(Resource.Id.botMessage).Text = "  " + received[position];
            
            if (sender[position] == true)
            {
                view.FindViewById<TextView>(Resource.Id.textView1).SetBackgroundColor(Android.Graphics.Color.GhostWhite);
                //view.FindViewById<TextView>(Resource.Id.botMessage).SetBackgroundColor(Android.Graphics.Color.GhostWhite);
                //view.FindViewById<TextView>(Resource.Id.textView1).LayoutParameters = view.LayoutParameters.
            }
            if (sender[position] == false)
            {
                //view.FindViewById<TextView>(Resource.Id.textView1).SetBackgroundColor(Android.Graphics.Color.LightBlue);
                view.FindViewById<TextView>(Resource.Id.botMessage).SetBackgroundColor(Android.Graphics.Color.LightBlue);
            }
            //view.FindViewById<ImageButton>(Resource.Id.sendInputBtn).SetBackgroundColor(Android.Graphics.Color.Transparent);
            return view;
        }
    }
}