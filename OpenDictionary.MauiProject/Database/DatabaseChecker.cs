using System;
using System.Diagnostics;

using Microsoft.Extensions.DependencyInjection;

namespace OpenDictionary.Databases;

internal static class DatabaseChecker
{
    public static void Check(IServiceProvider provider)
    {
        try
        {
            var connection = provider.GetService<IDatabaseConnection<DatabaseContext>>()!;

            connection.Open(static context =>
            {
                context.Database.EnsureCreated();

                context.SaveChanges();
            });
        }
        catch (Exception e)
        {
            Debug.WriteLine(e.Message);

            return;
        }
    }
}