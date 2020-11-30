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
    public class Profile
    {
        [PrimaryKey, AutoIncrement]
        public int ProfileID { get; set; }

        [Unique]
        public string ProfileName { get; set; }

        public string Password { get; set; }

        public int TotalWon { get; set; }

        public int TotalLost { get; set; }
    }
}