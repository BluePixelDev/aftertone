using UnityEngine;

namespace BP.OneShotSFX
{
    public class OneShotPlayer : MonoBehaviour
    {
        [SerializeField] private OneShotAsset waveAsset;
        [SerializeField] private bool playOnStart;

        public OneShotAsset WaveAsset
        {
            get => waveAsset;
            set => waveAsset = value;
        }

        private void Start()
        {
            if (playOnStart)
            {
                Play();
            }
        }

        public void Play()
        {
            OneShotPool.PlayWave(waveAsset, transform.position);
        }
    }
}
