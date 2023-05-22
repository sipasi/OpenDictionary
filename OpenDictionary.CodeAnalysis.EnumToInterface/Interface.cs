using System.Collections.Generic;

namespace OpenDictionary.CodeAnalysis.EnumToInterface;

public readonly ref struct Interface(string name, IEnumerable<string> names, SetAccessor accessor)
{
    public string CreateImplementation()
    {
        string properies = new NameToPropertyConverter(accessor)
            .Parse(names);

        string implementation = $$""" 
            public interface {{name}}
            { 
                {{properies}}
            }
            """;

        return implementation;
    }
}