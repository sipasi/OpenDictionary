using Microsoft.Maui.Controls;

using static Microsoft.Maui.Controls.BindableProperty;

namespace OpenDictionary.Views.Controls;

public static class BindableBuilder
{
    public static Context Create() => new();
    public static Context Create<TDeclare, TReturn>() => Create().WithDeclaring<TDeclare>().WithReturn<TReturn>();
    public static Context Create<TDeclare, TReturn>(string name) => Create<TDeclare, TReturn>().WithName(name);

    public static Context WithName(this Context context, string value)
    {
        context.name = value;

        return context;
    }
    public static Context WithReturn<T>(this Context context)
    {
        context.returnType = typeof(T);

        return context;
    }
    public static Context WithDeclaring<T>(this Context context)
    {
        context.declaringType = typeof(T);

        return context;
    }
    public static Context WithDefaultValue(this Context context, object value)
    {
        context.defaultValue = value;

        return context;
    }
    public static Context WithDefaultBinding(this Context context, BindingMode value)
    {
        context.bindingMode = value;

        return context;
    }
    public static Context WithPropertyChanged(this Context context, BindingPropertyChangedDelegate value)
    {
        context.propertyChanged = value;

        return context;
    }
    public static Context WithPropertyChanging(this Context context, BindingPropertyChangingDelegate value)
    {
        context.propertyChanging = value;

        return context;
    }
    public static Context WithPropertyChanged<TDeclare, TValue>(this Context context, Context.PropertyChanged<TDeclare, TValue> value)
        where TDeclare : BindableObject
    {
        context.propertyChanged = (bindable, oldValue, newValue) =>
        {
            var declare = BindableCaster.Cast<TDeclare>(bindable)!;

            TValue? old = CastOrDefault<TValue>(oldValue);
            TValue? cullent = CastOrDefault<TValue>(newValue);

            value.Invoke(declare, old, cullent);
        };

        return context;
    }
    public static Context WithPropertyChangingd<TDeclare, TValue>(this Context context, Context.PropertyChanged<TDeclare, TValue> value)
        where TDeclare : BindableObject
    {
        context.propertyChanging = (bindable, oldValue, newValue) =>
        {
            var declare = BindableCaster.Cast<TDeclare>(bindable)!;

            value.Invoke(declare, (TValue)oldValue, (TValue)newValue);
        };

        return context;
    }
    public static Context WithValidator(this Context context, ValidateValueDelegate value)
    {
        context.validateValue = value;

        return context;
    }

    public static Context WithValueCreator(this Context context, CreateDefaultValueDelegate value)
    {
        context.defaultValueCreator = value;

        return context;
    }

    public static Context WithCoerceValue(this Context context, CoerceValueDelegate value)
    {
        context.coerceValue = value;

        return context;
    }

    private static T? CastOrDefault<T>(object obj)
    {
        if (obj is T value)
        {
            return value;
        }

        return default;
    }
}