using System.Collections.Generic;

using OpenDictionary.CodeAnalysis.EnumToInterface.Extensions;

namespace OpenDictionary.CodeAnalysis.EnumToInterface;

public readonly ref struct NameToPropertyConverter(SetAccessor setAccessor)
{
    public string Parse(IEnumerable<string> names)
    {
        string accessor = setAccessor.ToStringWithSemi();

        PropertyBuilder.Appender appender = PropertyBuilder.Creator.Create(names, accessor);

        foreach (var name in names)
        {
            appender.Append(name);
        }

        appender.Assert();

        return appender.ToString();
    }
}