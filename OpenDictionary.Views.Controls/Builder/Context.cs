
using System;

using Microsoft.Maui.Controls;

using static Microsoft.Maui.Controls.BindableProperty;

namespace OpenDictionary.Views.Controls;

public class Context
{
    public delegate void PropertyChanged<TDeclare, TReturn>(TDeclare owner, TReturn old, TReturn current);

    internal string? name;

    internal Type? returnType;
    internal Type? declaringType;

    internal object? defaultValue;

    internal BindingMode bindingMode;

    internal BindingPropertyChangedDelegate? propertyChanged;
    internal BindingPropertyChangingDelegate? propertyChanging;


    internal ValidateValueDelegate? validateValue;

    internal CreateDefaultValueDelegate? defaultValueCreator;

    internal CoerceValueDelegate? coerceValue;

    public static implicit operator BindableProperty(Context context)
    {
        return Create(
            propertyName: context.name,
            returnType: context.returnType,
            declaringType: context.declaringType,
            defaultValue: context.defaultValue,
            defaultBindingMode: context.bindingMode,
            propertyChanged: context.propertyChanged,
            propertyChanging: context.propertyChanging,
            validateValue: context.validateValue,
            defaultValueCreator: context.defaultValueCreator,
            coerceValue: context.coerceValue);
    }
}