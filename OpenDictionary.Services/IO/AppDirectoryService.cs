#nullable enable

namespace OpenDictionary.Services.IO
{
    public sealed class AppDirectoryService : IAppDirectoryService
    {
        public string AppData { get; }
        public string Cache { get; }

        public AppDirectoryService(string appData, string cache)
        {
            AppData = appData;
            Cache = cache;
        }
    }
}