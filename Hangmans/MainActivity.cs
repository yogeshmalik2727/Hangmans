using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Content;

namespace Hangmans
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme")]
    public class MainActivity : Activity
    {
        Button btnStart,btnWinners,btnLosers,btnExit;
        TextView textview;
        string name;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            name = Intent.GetStringExtra("name");
            btnStart = FindViewById<Button>(Resource.Id.btnStart);
            btnWinners = FindViewById<Button>(Resource.Id.btnWinners);
            btnLosers = FindViewById<Button>(Resource.Id.btnLosers);
            btnExit = FindViewById<Button>(Resource.Id.btnExit);
            textview = FindViewById<TextView>(Resource.Id.textview);

            textview.Text = "Welcome " + name;
            btnStart.Click += BtnStart_Click;
            btnExit.Click += BtnExit_Click;
            btnWinners.Click += BtnWinners_Click;
            btnLosers.Click += BtnLosers_Click;
        }

        private void BtnLosers_Click(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(this, typeof(GameHistoryActivity));
            intent.PutExtra("type","loser");
            StartActivity(intent);
        }

        private void BtnWinners_Click(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(this, typeof(GameHistoryActivity));
            intent.PutExtra("type", "winner");
            StartActivity(intent);
        }


        private void BtnExit_Click(object sender, System.EventArgs e)
        {
            Finish();
        }

        private void BtnStart_Click(object sender, System.EventArgs e)
        {
            Intent intent = new Intent(this, typeof(HangmanGameActivity));
            intent.PutExtra("name", name);
            StartActivity(intent);
            
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}