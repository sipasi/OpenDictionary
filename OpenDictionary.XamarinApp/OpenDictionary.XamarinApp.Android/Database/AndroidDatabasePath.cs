
using System;

using OpenDictionary.AppDatabase;
using OpenDictionary.Droid.Database;

using Xamarin.Forms;

[assembly: Dependency(typeof(AndroidDatabasePath))]
namespace OpenDictionary.Droid.Database
{
    internal class AndroidDatabasePath : IDatabasePath
    {
        public string Path { get; } = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "database.db");
    }
}