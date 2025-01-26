using UnityEngine;

namespace BP.WavePool
{
    public class WaveSource : MonoBehaviour, IWaveSource
    {
        [SerializeField] private AudioSource audioSource;

        public bool IsPlaying => audioSource.isPlaying;
        public AudioSource Source => audioSource;

        private void LateUpdate()
        {
            if (!audioSource.isPlaying)
            {
                gameObject.SetActive(false);
            }
        }

        public void Play(WaveSourceData data)
        {
            gameObject.SetActive(true);
            audioSource.SetSourceData(data);
            audioSource.Play();
        }
        public void Stop() => audioSource.Stop();
    }
}