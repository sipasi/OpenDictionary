using System;

namespace OpenDictionary.Services.Audio;

public interface IAudioPlayerServise : IDisposable
{
    void Play(string path);
    void Stop();
    void Pause();
}