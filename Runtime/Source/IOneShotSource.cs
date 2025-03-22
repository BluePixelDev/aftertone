using UnityEngine;

namespace BP.OneShotSFX
{
    public interface IOneShotSource
    {
        public bool IsPlaying { get; }
        public AudioSource Source { get; }
        public void Play(OneShotAsset data);
        public void Stop();
    }
}
