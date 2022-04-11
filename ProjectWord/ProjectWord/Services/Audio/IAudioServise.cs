using System;

namespace ProjectWord.Services.Audio
{
    public interface IAudioServise : IDisposable
    {
        void Play(string path);
        void Stop();
        void Pause();
        void Replay();
    }
}
