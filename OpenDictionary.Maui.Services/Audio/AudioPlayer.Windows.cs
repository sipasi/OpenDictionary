using System;

using Windows.Media.Playback;

namespace OpenDictionary.Services.Audio;

internal sealed partial class AudioPlayer : IAudioPlayerServise
{
    private readonly MediaPlayer player = new();

    public void Play(string path)
    {
        Uri uri = new Uri(path);

        player.SetUriSource(uri);

        player.Play();
    }
    public void Pause()
    {
        player.Pause();
    }

    public void Stop()
    {
        player.Pause();
        player.Position = TimeSpan.Zero;
    }

    public void Dispose()
    {
        player.Dispose();
    }
}
