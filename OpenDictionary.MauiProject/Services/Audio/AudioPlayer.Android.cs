using Android.Media;

namespace OpenDictionary.Services.Audio;

internal sealed partial class AudioPlayer : IAudioPlayerServise
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
