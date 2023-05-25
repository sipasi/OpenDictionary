using Microsoft.Maui.Controls;

using static Microsoft.Maui.Controls.BindableProperty;

namespace OpenDictionary.Controls;

public class Context<TDeclare, TValue>
{
    public delegate void PropertyChanged(TDeclare owner, TValue? old, TValue? current);

    internal string? name;

    internal TValue? defaultValue;

    internal BindingMode bindingMode;

    internal BindingPropertyChangedDelegate? propertyChanged;
    internal BindingPropertyChangingDelegate? propertyChanging;

    internal ValidateValueDelegate? validateValue;

    internal CreateDefaultValueDelegate? defaultValueCreator;

    internal CoerceValueDelegate? coerceValue;

    public static implicit operator BindableProperty(Context<TDeclare, TValue> context)
    {
        return Create(
            propertyName: context.name,
            returnType: typeof(TValue),
            declaringType: typeof(TDeclare),
            defaultValue: context.defaultValue,
            defaultBindingMode: context.bindingMode,
            propertyChanged: context.propertyChanged,
            propertyChanging: context.propertyChanging,
            validateValue: context.validateValue,
            defaultValueCreator: context.defaultValueCreator,
            coerceValue: context.coerceValue);
    }
}