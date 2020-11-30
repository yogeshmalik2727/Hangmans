using System;
using System.Collections.Generic;
using System.IO;
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
    public class Hangdiction
    {
        List<string> wordList;
        DataConnection connection;
        Random random;
        public Hangdiction(Context context)
        {
            try
            {
                connection = new DataConnection(context);
                random = new Random();
                wordList = new List<string>();
                List<Word> words = connection.GetAllWords();
                foreach(Word word in words)
                {
                    wordList.Add(word.WordString);
                }
            }
            catch (Exception ex) { }

        }

        public string PickGoodStarterWord()
        {
            int index = random.Next(wordList.Count());
            string t = wordList[index];
            if (t.Count() <= 10)
                return wordList[index];
            else 
                return PickGoodStarterWord();
        }
    }
}