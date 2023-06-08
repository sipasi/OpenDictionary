#nullable enable

using System;
using System.Threading.Tasks;

namespace OpenDictionary.Presentation.Shared;

public static class TaskUtilities
{
    public delegate void ExceptionHandler(string message, string? inner);

    public static async void SafeFireAndForget(this Task task, ExceptionHandler? exceptionHandler = null)
    {
        try
        {
            await task;
        }
        catch (Exception e)
        {
            HandleException(e, exceptionHandler);
        }
    }

    public static async void SafeFireAndForgetAsync(this ValueTask task, ExceptionHandler? exceptionHandler = null)
    {
        try
        {
            await task;
        }
        catch (Exception e)
        {
            HandleException(e, exceptionHandler);
        }
    }

    private static void HandleException(Exception exception, ExceptionHandler? exceptionHandler = null)
    {
        exceptionHandler?.Invoke(
            exception.Message,
            exception.InnerException?.Message
        );
    }
}