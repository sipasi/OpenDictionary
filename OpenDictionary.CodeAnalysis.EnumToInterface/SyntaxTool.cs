using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace OpenDictionary.CodeAnalysis.EnumToInterface;

internal static class SyntaxTool
{
    public static string Normalize(string text)
    {
        return CSharpSyntaxTree
            .ParseText(text)
            .GetRoot()
            .NormalizeWhitespace()
            .ToFullString();
    }
}