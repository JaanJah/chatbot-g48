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

namespace chatbot
{
    class ChatAdapter : BaseAdapter<string>
    {
        string[] sent;
        string[] received;
        Activity context;
        public ChatAdapter(Activity context, List<string> sent, List<string> received) : base()
        {
            this.context = context;
            this.sent = sent.ToArray();
            this.received = received.ToArray();
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
                view = context.LayoutInflater.Inflate(Resource.Layout.activity_main, null);
            view.FindViewById<TextView>(Resource.Id.textView1).Text = sent[position];
            view.FindViewById<TextView>(Resource.Id.botMessage).Text = received[position];
            return view;
        }
    }
}