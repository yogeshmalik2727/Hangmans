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
using SQLite;

namespace Hangmans.Models
{
    public class Word
    {
        [PrimaryKey, AutoIncrement]
        public int WordID { get; set; }

        [Unique]
        public string WordString { get; set; }
    }
}