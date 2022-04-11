using System;

using Android.Media;

using ProjectWord.Droid.Services.Audio;
using ProjectWord.Services.Audio;

using Xamarin.Forms;

[assembly: Dependency(typeof(AudioServise))]
namespace ProjectWord.Droid.Services.Audio
{
    internal class AudioServise : IAudioServise
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