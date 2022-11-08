
using System;

namespace OpenDictionary.Databases;

public sealed class UniversalDatabasePath : IDatabasePath
{
    public string Path { get; } = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "database.db");
}