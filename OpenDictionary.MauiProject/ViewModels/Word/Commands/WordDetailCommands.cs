#nullable enable

using Microsoft.EntityFrameworkCore;

namespace OpenDictionary.ViewModels.Words.Commands;

public class WordDetailCommands
{
    public required WordStorageCommands WordStorage { get; init; }
    public required WordAudioPlayerCommands AudioPlayer { get; init; }
}