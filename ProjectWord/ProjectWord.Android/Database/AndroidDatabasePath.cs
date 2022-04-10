
using System;

using ProjectWord.AppDatabase;
using ProjectWord.Droid.Database;

using Xamarin.Forms;

[assembly: Dependency(typeof(AndroidDatabasePath))]
namespace ProjectWord.Droid.Database
{
    internal class AndroidDatabasePath : IDatabasePath
    {
        public string Path { get; } = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "database.db");
    }
}