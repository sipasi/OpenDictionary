using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace OpenDictionary.CodeAnalysis.EnumToInterface;

public readonly ref struct InterfaceNameExtractor(AttributeSyntax attribute, string defaultName)
{
    public string Extract()
    {
        AttributeArgumentSyntax? argument = attribute.ArgumentList?.Arguments
            .FirstOrDefault(static name => name.NameEquals?.Name.ToString() == Attributes.EnumToInterface.InterfaceName);

        string? name = argument?.Expression
            .GetText()
            .ToString()
            .Replace("\"", "");

        return name ?? defaultName;
    }
}