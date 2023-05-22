using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace OpenDictionary.CodeAnalysis.EnumToInterface;

public readonly ref struct SymbolAndAttributeExtractor(Compilation compilation, EnumDeclarationSyntax declaration)
{
    public bool TryExtract(out INamedTypeSymbol symbol, out AttributeSyntax attribute)
    {
        symbol = compilation
            .GetSemanticModel(declaration.SyntaxTree)
            .GetDeclaredSymbol(declaration)!;

        if (symbol is null)
        {
            attribute = default!;

            return false;
        }

        attribute = declaration.AttributeLists
            .SelectMany(static list => list.Attributes)
            .FirstOrDefault(static syntax => syntax.Name.ToString() == Attributes.EnumToInterface.ClassName);

        if (attribute is null)
        {
            return false;
        }

        return true;
    }
}