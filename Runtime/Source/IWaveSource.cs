using UnityEngine;

namespace BP.WavePool
{
    public interface IWaveSource
    {
        public bool IsPlaying { get; }
        public AudioSource Source { get; }
        public void Play(WaveSourceData data);
        public void Stop();
    }
}
