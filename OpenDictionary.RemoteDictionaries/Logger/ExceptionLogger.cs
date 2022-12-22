#nullable enable

using System;
using System.Diagnostics;

namespace OpenDictionary.RemoteDictionaries.Logger;

internal static class ExceptionLogger
{
    public static void Log(Exception exception)
    {
        string message = GetMessage(exception);

        Debug.WriteLine(message);
    }

    private static string GetMessage(Exception exception)
    {
        string message = exception.Message;
        string? inner = exception.InnerException?.Message;

        return $"{message}. {inner ?? null}";
    }
}
