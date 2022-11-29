using System;
using System.Diagnostics;

using Microsoft.Extensions.DependencyInjection;

namespace OpenDictionary.Databases;

internal static class DatabaseChecker
{
    public static void Check(IServiceProvider provider)
    {
        IDatabasePath path = provider.GetService<IDatabasePath>()!;

        DatabaseContext context = new DatabaseContext(path);

        try
        {
            context.Database.EnsureCreated();

            context.SaveChanges();
        }
        catch (Exception e)
        {
            Debug.WriteLine(e.Message);

            return;
        }
    }
}