using UnityEngine;

namespace BP.WavePool
{
    public class WavePlayer : MonoBehaviour
    {
        [SerializeField] private WaveAsset waveAsset;
        [SerializeField] private bool playOnStart;

        public WaveAsset WaveAsset
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
            WavePool.PlayWave(waveAsset, transform.position);
        }
    }
}
