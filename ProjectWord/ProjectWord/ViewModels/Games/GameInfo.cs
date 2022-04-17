using System;

namespace OpenDictionary.ViewModels.Games
{
    public struct GameInfo
    {
        public string Image { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Type Page { get; set; }
    }
}