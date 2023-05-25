using Microsoft.Maui.Controls;

namespace OpenDictionary.Controls;

public static class BindableCaster
{
    public static T? Cast<T>(this BindableObject bindable) where T : BindableObject => bindable as T;
    public static bool TryCast<T>(this BindableObject bindable, out T result) where T : BindableObject
        => (result = bindable.Cast<T>()!) is not null;
}