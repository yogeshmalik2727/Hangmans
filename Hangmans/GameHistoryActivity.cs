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
using Hangmans.DAL;

namespace Hangmans
{
    [Activity(Label = "GameHistoryActivity")]
    public class GameHistoryActivity : Activity
    {
        ListView list;
        Button btnBack;
        string type;
        DataConnection connection;
        GameHistoryAdapter adapter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.game_history_layout);
            
            connection = new DataConnection(this);
            type = Intent.GetStringExtra("type");
            
            list = FindViewById<ListView>(Resource.Id.list);
            btnBack = FindViewById<Button>(Resource.Id.back);

            btnBack.Click += BtnBack_Click;

            if (type.Equals("winner"))
            {
                adapter = new GameHistoryAdapter(this, connection.GetWinnerProfile());
                list.Adapter = adapter;
            }
            else
            {
                adapter = new GameHistoryAdapter(this, connection.GetLoserProfile());
                list.Adapter = adapter;
            }
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            Finish();
        }
    }
}