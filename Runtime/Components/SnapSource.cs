using UnityEngine;

namespace BP.SoundSnap
{
    /// <summary>
    /// Represents a pooled audio source that can play a <see cref="SnapAsset"/> and automatically deactivates when done.
    /// </summary>
    public class SnapSource : MonoBehaviour
    {
        [Tooltip("The AudioSource used to play this snap sound.")]
        [SerializeField]
        private AudioSource audioSource;

        /// <summary>
        /// Whether this source is currently playing audio.
        /// </summary>
        public bool IsPlaying => audioSource != null && audioSource.isPlaying;

        /// <summary>
        /// Gets or sets the underlying AudioSource used by this SnapSource.
        /// </summary>
        public AudioSource AudioSource
        {
            get => audioSource;
            set => audioSource = value;
        }

        private void LateUpdate()
        {
            // Automatically deactivate the object once playback has finished
            if (audioSource != null && !audioSource.isPlaying)
            {
                gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// Plays the given <see cref="SnapAsset"/> using this audio source.
        /// </summary>
        /// <param name="asset">The audio asset to play.</param>
        public void Play(SnapAsset asset)
        {
            if (asset == null || audioSource == null)
            {
                Debug.LogWarning($"{nameof(SnapSource)}: Cannot play. Missing asset or AudioSource.");
                return;
            }

            gameObject.SetActive(true);
            asset.CopyToSource(audioSource);
            audioSource.Play();
        }

        /// <summary>
        /// Stops playback immediately.
        /// </summary>
        public void Stop()
        {
            if (audioSource != null)
            {
                audioSource.Stop();
            }
        }
    }
}
