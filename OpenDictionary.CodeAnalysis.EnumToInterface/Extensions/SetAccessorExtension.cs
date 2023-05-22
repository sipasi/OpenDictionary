namespace OpenDictionary.CodeAnalysis.EnumToInterface.Extensions;

internal static class SetAccessorExtension
{
    public static string ToStringWithSemi(this SetAccessor accessor) => accessor switch
    {
        SetAccessor.Set => "set;",
        SetAccessor.Init => "init;",
        _ => string.Empty,
    };
}