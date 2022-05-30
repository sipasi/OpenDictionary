using System;

using Android.Media;

using OpenDictionary.Droid.Services.Audio;
using OpenDictionary.Services.Audio;

using Xamarin.Forms;

[assembly: Dependency(typeof(AudioPlayer))]
namespace OpenDictionary.Droid.Services.Audio
{
    internal class AudioPlayer : IAudioPlayerServise
    {
        private readonly MediaPlayer player = new MediaPlayer();

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
        public void Replay()
        {
            throw new NotImplementedException();
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
}