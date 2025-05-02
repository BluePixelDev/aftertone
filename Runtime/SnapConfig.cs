using UnityEngine;

namespace BP.SoundSnap
{
    /// <summary>
    /// Configuration asset for Snap sound pooling system.
    /// Controls initialization behavior and wave pool sizing.
    /// </summary>
    public class SnapConfig : ScriptableObject
    {
        [Header("Wave Pool Capacity")]
        [Tooltip("Initial number of pooled wave objects.")]
        [SerializeField, Min(0)] private int initialCapacity = 10;

        [Tooltip("Number of wave objects added when capacity is exceeded.")]
        [SerializeField, Min(1)] private int capacityIncrement = 10;

        [Tooltip("Maximum allowed number of pooled wave objects.")]
        [SerializeField, Min(10)] private int maxCapacity = 100;

        /// <summary>
        /// Gets the initial wave pool capacity.
        /// </summary>
        public int InitialCapacity => Mathf.Max(0, initialCapacity);

        /// <summary>
        /// Gets the number of wave objects to add when the pool exceeds its capacity.
        /// </summary>
        public int CapacityIncrement => Mathf.Max(1, capacityIncrement);

        /// <summary>
        /// Gets the maximum number of wave objects allowed in the pool.
        /// </summary>
        public int MaxCapacity => Mathf.Max(10, maxCapacity);
    }
}
