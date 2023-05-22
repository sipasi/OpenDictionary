using System.Collections.Immutable;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace OpenDictionary.CodeAnalysis.EnumToInterface.Generators;

[Generator]
public class InterfaceGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        IncrementalValuesProvider<EnumDeclarationSyntax> provider = context.SyntaxProvider.CreateSyntaxProvider(
            predicate: static (node, _) => node is EnumDeclarationSyntax,
            transform: static (syntax, _) => ((EnumDeclarationSyntax)syntax.Node)
        ).Where(static node => node is not null);

        var compilation = context.CompilationProvider.Combine(provider.Collect());

        context.RegisterSourceOutput(compilation, static (context, source) => Execute(context, source.Left, source.Right));
    }

    private static void Execute(
        SourceProductionContext context,
        Compilation compilation,
        ImmutableArray<EnumDeclarationSyntax> declarations)
    {
        foreach (var declaration in declarations)
        {
            SymbolAndAttributeExtractor symbolAndAttribute = new(compilation, declaration);

            if (symbolAndAttribute.TryExtract(out INamedTypeSymbol symbol, out AttributeSyntax attribute) is false)
            {
                continue;
            }

            InterfaceParser.Result result = new InterfaceParser(symbol, attribute).Parce();

            context.AddSource($"{result.Name}.g.cs", result.Implementation);
        }
    }
}
