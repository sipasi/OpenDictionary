using System;

namespace OpenDictionary.CodeAnalysis.EnumToInterface;

public static class Attributes
{
    public static class EnumToInterface
    {
        public static string ClassName { get; } = nameof(EnumToInterface);
        public static string FullName { get; } = ClassName + nameof(Attribute);

        public static string InterfaceName { get; } = nameof(InterfaceName);
    }
}