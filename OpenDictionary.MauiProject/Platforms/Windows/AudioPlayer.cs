using System;

using Microsoft.Maui.Controls;

using OpenDictionary.Platforms.Windows;

using OpenDictionary.Services.Audio;

using Windows.Media.Playback;

[assembly: Dependency(typeof(AudioPlayer))]
namespace OpenDictionary.Platforms.Windows;

internal class AudioPlayer : IAudioPlayerServise
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