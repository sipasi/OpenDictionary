using Microsoft.Maui.Controls;

namespace OpenDictionary.Views.Controls;

public static class BindableCaster
{
    public static T? Cast<T>(BindableObject bindable) where T : BindableObject => bindable as T;
    public static bool TryCast<T>(BindableObject bindable, out T result) where T : BindableObject
        => (result = Cast<T>(bindable)!) is not null;
}