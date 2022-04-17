#nullable enable

using System.IO;
using System.Threading.Tasks;

namespace OpenDictionary.Words.Tests
{
    static class InsistWordJson
    {
        private readonly static string[] names = { "insist-json-with-array-scopes.json", "insist-json-without-array-scopes.json" };

        public static string Word => "insist";

        public static async ValueTask<string> LoadWithScopes() => await LoadByIndex(0);

        public static async ValueTask<string> LoadWithoutScopes() => await LoadByIndex(1);

        private static Task<string> LoadByIndex(int index)
        {
            string name = names[index];

            return File.ReadAllTextAsync(name);
        }
    }
}