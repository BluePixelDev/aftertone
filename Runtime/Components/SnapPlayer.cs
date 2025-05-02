using UnityEngine;

namespace BP.SoundSnap
{
    [AddComponentMenu("SoundSnap/Snap Player")]
    public class SnapPlayer : MonoBehaviour
    {
        [Tooltip("The SnapAsset to be played by this player.")]
        [SerializeField] private SnapAsset snapAsset;

        [Tooltip("If enabled, the SnapAsset will play automatically on Start.")]
        [SerializeField] private bool playOnStart;

        /// <summary>
        /// Gets or sets the SnapAsset assigned to this player.
        /// </summary>
        public SnapAsset SnapAsset
        {
            get => snapAsset;
            set => snapAsset = value;
        }

        private void Start()
        {
            if (playOnStart)
            {
                Play();
            }
        }

        /// <summary>
        /// Plays the assigned SnapAsset at the GameObject's current world position.
        /// </summary>
        public void Play()
        {
            if (snapAsset == null)
            {
                Debug.LogWarning($"{nameof(SnapPlayer)}: No SnapAsset assigned.", gameObject);
                return;
            }

            SoundSnap.Play(snapAsset, transform.position);
        }
    }
}
