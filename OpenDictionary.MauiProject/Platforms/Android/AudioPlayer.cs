
using Android.Media;

using OpenDictionary.Services.Audio;

namespace OpenDictionary.Platforms.Android;

internal class AudioPlayer : IAudioPlayerServise
{
    private readonly MediaPlayer player = new();

    public void Play(string path)
    {
        player.Reset();

        player.SetDataSource(path);

        player.Prepare();

        player.Start();
    }
    public void Pause()
    {
        player.Pause();
    }

    public void Stop()
    {
        player.Stop();
    }

    public void Dispose()
    {
        player.Release();
    }
}