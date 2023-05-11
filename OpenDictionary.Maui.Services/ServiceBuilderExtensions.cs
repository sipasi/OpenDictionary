#nullable enable

using Microsoft.Extensions.DependencyInjection;

using OpenDictionary.Maui.Services.Messages.Alerts;
using OpenDictionary.Maui.Services.Messages.Toasts;
using OpenDictionary.MauiProject.Services.Messages.Dialogs;
using OpenDictionary.Services.Audio;
using OpenDictionary.Services.Messages.Alerts;
using OpenDictionary.Services.Messages.Dialogs;
using OpenDictionary.Services.Messages.Loadings;
using OpenDictionary.Services.Messages.Toasts;
using OpenDictionary.Services.Navigations;

namespace OpenDictionary.Maui.Services;

public static class ServiceBuilderExtensions
{
    public static IServiceCollection ConfigureMauiServices(this IServiceCollection services)
    {
        return services
            .ConfigureAudio()
            .ConfigureNavigation()
            .ConfigureMessagesDialogs();
    }

    private static IServiceCollection ConfigureAudio(this IServiceCollection services)
    {
        services
            .AddSingleton<IPhoneticFilesService, PhoneticFilesService>()
            .AddSingleton<IAudioPlayerServise, AudioPlayer>();

        return services;
    }

    private static IServiceCollection ConfigureNavigation(this IServiceCollection services)
    {
        services.AddSingleton<INavigationService, ShellNavigationService>();

        return services;
    }
    private static IServiceCollection ConfigureMessagesDialogs(this IServiceCollection services)
    {
        services
            .AddSingleton<IAlertMessageService, AlertMessageService>()
            .AddSingleton<IToastMessageService, ToastMessageService>()
            .AddSingleton<IDialogMessageService, PopupDialogMessageService>()
            .AddSingleton<ILoadingMessageService, LoadingMessageService>();

        return services;
    }
}