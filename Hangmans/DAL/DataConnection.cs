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
using Hangmans.Models;
using SQLite;

namespace Hangmans.DAL
{
    public class DataConnection
    {
        private SQLiteConnection conn;

        private string error;

        public string GetError()
        {
            return error;
        }
        public DataConnection(Context context)
        {
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            conn = new SQLiteConnection(Path.Combine(path, "hangmans.db"));
            if (!CheckTable())
            {
                conn.CreateTable<Profile>();
                conn.CreateTable<Word>();
                try
                {
                    StreamReader br = new StreamReader(context.Assets.Open("words.txt"));
                    string line;
                    while ((line = br.ReadLine()) != null)
                    {
                        if( line.Length >= 4 && line.Length <= 20)
                        {
                            Word word = new Word();
                            word.WordString = line.Trim().ToUpper();
                            SaveWord(word);
                        }                        
                    }
                }
                catch(IOException ex)
                {

                }
            }

        }

        public List<string> GetAllWordsString()
        {
            List<string> wordStrings = new List<string>();
            List<Word> words = GetAllWords();
            foreach(Word word in words)
            {
                wordStrings.Add(word.WordString);
            }
            return wordStrings;
        }

        public bool SaveWord(Word word)
        {
            try
            {
                conn.Insert(word);
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        public bool CheckProfile(string name,string password)
        {
            List<Profile> profiles = conn.Query<Profile>("Select * from Profile");
            foreach(Profile profile in profiles)
            {
                if(profile.ProfileName.Equals(name) && profile.Password.Equals(password))
                {
                    return true;
                }
            }
            return false;
        }

        public bool UpdateProfile(string name,bool winning)
        {
            try
            {
                var profiles = conn.Table<Profile>();
                var profile = (from pro in profiles
                               where pro.ProfileName == name
                               select pro).Single();
                if (winning)
                {
                    profile.TotalWon += 1;
                }
                else
                {
                    profile.TotalLost += 1;
                }
                conn.Update(profile);
                return true;
            }
            catch(Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        public bool SaveProfile(Profile profile)
        {
            try
            {
                conn.Insert(profile);
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        public List<Word> GetAllWords()
        {
            List<Word> words = conn.Query<Word>("Select * from Word");
            return words;
        }

        public List<Profile> GetLoserProfile()
        {
            List<Profile> profiles = conn.Query<Profile>("Select * from Profile Order by TotalLost  Desc");
            return profiles;
        }

        public List<Profile> GetWinnerProfile()
        {
            List<Profile> profiles = conn.Query<Profile>("Select * from Profile Order by TotalWon Desc");
            return profiles;
        }

        private bool CheckTable()
        {
            try
            {
                conn.Get<Word>(1);
                conn.Get<Profile>(1);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}