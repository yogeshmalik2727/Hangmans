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
using Hangmans.Models;

namespace Hangmans
{
    [Activity(Label = "AdminActivity")]
    public class AdminActivity : Activity
    {
        Button btnSave, btnExit;
        EditText etWord;
        DataConnection connection;
        ListView list;
        List<string> words;
        ArrayAdapter<string> adapter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.admin_layout);
            connection = new DataConnection(this);
            words = connection.GetAllWordsString();

            btnExit = FindViewById<Button>(Resource.Id.btnExit);
            btnSave = FindViewById<Button>(Resource.Id.btnSave);
            etWord = FindViewById<EditText>(Resource.Id.etWord);
            list = FindViewById<ListView>(Resource.Id.list);

            btnExit.Click += BtnExit_Click;
            btnSave.Click += BtnSave_Click;

            adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, words);
            list.Adapter = adapter;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            string word = etWord.Text.Trim().ToUpper();
            string message = "";
            if(word.Length == 0 )
            {
                message = "Please Fill All Boxes";
            }
            else
            {
                Word obj = new Word();
                obj.WordString = word;
                if(connection.SaveWord(obj))
                {
                    message = "Word is Saved";
                    words.Add(word);
                    adapter.NotifyDataSetChanged();
                }
                else
                {
                    message = "This Word is Already in List";
                }
            }
            Toast.MakeText(this, message, ToastLength.Long).Show();
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Finish();
        }
    }
}