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

namespace Hangmans
{
    class ButtonAdapter : BaseAdapter<string>
    {
        private string[] letters;
        private Context context;
        public ButtonAdapter(Context c)
        {
            letters = new string[26];
            for (int index = 0; index < letters.Length; index++)
            {
                letters[index] = "" + (char)(index + 'A');
            }
            this.context = c;
        }

        public override int Count
        {
            get { return letters.Length; }

        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override string this[int position]
        {
            get { return letters[position]; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            Button btn;

            if (convertView == null)
            {
                btn = (Button)LayoutInflater.From(context).Inflate(Resource.Layout.button, null, false);
            }
            else
            {
                btn = (Button)convertView;
            }
            btn.Text = letters[position];
            return btn;
        }

    }
}