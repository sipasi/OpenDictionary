#nullable enable

namespace OpenDictionary.Words.Tests;

static class InsistWordJson
{
    private readonly static string[] names =
    {
        "insist-json-empty-object.json",
        "insist-json-empty-scopes.json",
        "insist-json-with-array-scopes.json",
        "insist-json-with-null-properties.json",
        "insist-json-without-array-scopes.json",
    };

    public static string Word => "insist";

    public static async ValueTask<string> LoadEmptyObject() => await LoadByIndex(0);
    public static async ValueTask<string> LoadEmptyObjectWithScopes() => await LoadByIndex(1);
    public static async ValueTask<string> LoadWithScopes() => await LoadByIndex(2);
    public static async ValueTask<string> LoadWithNullProperties() => await LoadByIndex(3);
    public static async ValueTask<string> LoadWithoutScopes() => await LoadByIndex(4);

    private static Task<string> LoadByIndex(int index)
    {
        string name = names[index];

        return File.ReadAllTextAsync(name);
    }
}