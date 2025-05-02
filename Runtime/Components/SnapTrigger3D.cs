using UnityEngine;

namespace BP.SoundSnap
{
    /// <summary>
    /// Triggers a <see cref="SnapPlayer"/> to play its assigned sound when a 3D physics interaction occurs.
    /// This can be configured to respond to either trigger or collision events and optionally filter by tag.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class SnapTrigger3D : MonoBehaviour
    {
        [Tooltip("The SnapPlayer to trigger on collision/trigger.")]
        [SerializeField] private SnapPlayer snapPlayer;

        [Tooltip("Use OnTriggerEnter instead of OnCollisionEnter.")]
        [SerializeField] private bool useTrigger = true;

        [Tooltip("Tag filter (leave empty to allow all).")]
        [SerializeField] private string tagFilter;

        private void OnTriggerEnter(Collider other)
        {
            if (useTrigger)
            {
                if (IsTagAllowed(other.gameObject))
                {
                    TriggerPlayer();
                }
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!useTrigger)
            {
                if (IsTagAllowed(collision.gameObject))
                {
                    TriggerPlayer();
                }
            }
        }

        /// <summary>
        /// Triggers the associated <see cref="SnapPlayer"/> to play its sound.
        /// </summary>
        private void TriggerPlayer()
        {
            if (snapPlayer != null)
            {
                snapPlayer.Play();
            }
        }

        /// <summary>
        /// Determines whether the given GameObject is allowed based on the tag filter.
        /// </summary>
        /// <param name="gameObject">The GameObject to evaluate.</param>
        /// <returns>True if allowed; otherwise, false.</returns>
        private bool IsTagAllowed(GameObject gameObject)
        {
            return string.IsNullOrEmpty(tagFilter) || gameObject.CompareTag(tagFilter);
        }
    }
}
