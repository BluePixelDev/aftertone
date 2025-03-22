using UnityEngine;

namespace BP.OneShotSFX
{
    public class OneShotSource : MonoBehaviour, IOneShotSource
    {
        [SerializeField] private AudioSource audioSource;

        public bool IsPlaying => audioSource.isPlaying;
        public AudioSource Source
        {
            get => audioSource;
            set => audioSource = value;
        }

        private void LateUpdate()
        {
            if (!audioSource.isPlaying)
            {
                gameObject.SetActive(false);
            }
        }

        public void Play(OneShotAsset asset)
        {
            gameObject.SetActive(true);
            asset.CopyToSource(Source);
            audioSource.Play();
        }
        public void Stop() => audioSource.Stop();
    }
}