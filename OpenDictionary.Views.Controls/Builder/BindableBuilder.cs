using Microsoft.Maui.Controls;

using static Microsoft.Maui.Controls.BindableProperty;

namespace OpenDictionary.Views.Controls;

public static class BindableBuilder
{
    public static Context<TDeclare, TValue> Create<TDeclare, TValue>() => new();
    public static Context<TDeclare, TValue> Create<TDeclare, TValue>(string name) => Create<TDeclare, TValue>().WithName(name);

    public static Context<TDeclare, TValue> WithName<TDeclare, TValue>(this Context<TDeclare, TValue> context, string value)
    {
        context.name = value;

        return context;
    }

    public static Context<TDeclare, TValue> WithDefaultValue<TDeclare, TValue>(this Context<TDeclare, TValue> context, TValue? value = default)
    {
        context.defaultValue = value;

        return context;
    }
    public static Context<TDeclare, TValue> WithDefaultBinding<TDeclare, TValue>(this Context<TDeclare, TValue> context, BindingMode value)
    {
        context.bindingMode = value;

        return context;
    }
    public static Context<TDeclare, TValue> WithPropertyChanged<TDeclare, TValue>(this Context<TDeclare, TValue> context, BindingPropertyChangedDelegate value)
    {
        context.propertyChanged = value;

        return context;
    }
    public static Context<TDeclare, TValue> WithPropertyChanging<TDeclare, TValue>(this Context<TDeclare, TValue> context, BindingPropertyChangingDelegate value)
    {
        context.propertyChanging = value;

        return context;
    }
    public static Context<TDeclare, TValue> WithPropertyChanged<TDeclare, TValue>(this Context<TDeclare, TValue> context, Context<TDeclare, TValue>.PropertyChanged value)
        where TDeclare : BindableObject
    {
        context.propertyChanged = (bindable, oldValue, newValue) =>
        {
            var declare = bindable.Cast<TDeclare>()!;

            TValue? old = CastOrDefault<TValue>(oldValue);
            TValue? cullent = CastOrDefault<TValue>(newValue);

            value.Invoke(declare, old, cullent);
        };

        return context;
    }
    public static Context<TDeclare, TValue> WithPropertyChangingd<TDeclare, TValue>(this Context<TDeclare, TValue> context, Context<TDeclare, TValue>.PropertyChanged value)
        where TDeclare : BindableObject
    {
        context.propertyChanging = (bindable, oldValue, newValue) =>
        {
            var declare = bindable.Cast<TDeclare>()!;

            value.Invoke(declare, (TValue)oldValue, (TValue)newValue);
        };

        return context;
    }
    public static Context<TDeclare, TValue> WithValidator<TDeclare, TValue>(this Context<TDeclare, TValue> context, ValidateValueDelegate value)
    {
        context.validateValue = value;

        return context;
    }

    public static Context<TDeclare, TValue> WithValueCreator<TDeclare, TValue>(this Context<TDeclare, TValue> context, CreateDefaultValueDelegate value)
    {
        context.defaultValueCreator = value;

        return context;
    }

    public static Context<TDeclare, TValue> WithCoerceValue<TDeclare, TValue>(this Context<TDeclare, TValue> context, CoerceValueDelegate value)
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