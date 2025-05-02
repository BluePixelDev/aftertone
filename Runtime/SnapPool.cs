using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BP.SoundSnap
{
    /// <summary>
    /// A pooling system for reusable audio sources.
    /// Initializes itself on Awake, and uses <see cref="SnapConfig"/> for capacity management.
    /// </summary>
    public class SnapPool : MonoBehaviour
    {
        // Pool
        private readonly List<SnapSource> pool = new();

        // <summary>
        /// The current number of SnapSources in the pool.
        /// </summary>
        public int PoolCount => pool.Count;

        private void Awake()
        {
            InitializePool();
            DontDestroyOnLoad(this);
        }

        /// <summary>
        /// Initializes the pool based on configuration.
        /// </summary>
        private void InitializePool()
        {
            var config = SoundSnap.Config;
            if (config == null)
            {
                Debug.LogError($"{nameof(SnapPool)}: Missing SnapConfig. Initialization aborted.");
                return;
            }

            for (int i = 0; i < config.InitialCapacity; i++)
            {
                CreateSnapSource();
            }
        }


        /// <summary>
        /// Retrieves an available (inactive) SnapSource from the pool, or expands the pool if needed.
        /// </summary>
        /// <returns>An available <see cref="SnapSource"/>, or null if max capacity reached.</returns>
        private SnapSource GetAvailableSource()
        {
            foreach (var source in pool)
            {
                if (!source.IsPlaying)
                {
                    return source;
                }
            }

            var config = SoundSnap.Config;
            if (pool.Count >= config.MaxCapacity)
            {
                Debug.LogWarning($"{nameof(SnapPool)}: Max capacity reached. Cannot create more sources.");
                return null;
            }

            return CreateBatch(config.CapacityIncrement).FirstOrDefault();
        }

        /// <summary>
        /// Creates and adds a batch of SnapSources to the pool.
        /// </summary>
        /// <param name="count">Number of SnapSources to create.</param>
        private SnapSource[] CreateBatch(int count)
        {
            var batch = new SnapSource[count];
            for (int i = 0; i < count; i++)
            {
                batch[i] = CreateSnapSource();
            }
            return batch;
        }


        /// <summary>
        /// Creates and adds a new SnapSource to the pool.
        /// </summary>
        private SnapSource CreateSnapSource()
        {
            var sourceObject = new GameObject($"SnapSource #{pool.Count}");
            sourceObject.transform.SetParent(transform);
            sourceObject.SetActive(false);

            var audioSource = sourceObject.AddComponent<AudioSource>();
            var snapSource = sourceObject.AddComponent<SnapSource>();
            snapSource.AudioSource = audioSource;

            pool.Add(snapSource);
            return snapSource;
        }

        /// <summary>
        /// Stops all playing SnapSources in the pool.
        /// </summary>
        public void StopAll()
        {
            foreach (var source in pool)
            {
                if (source != null)
                {
                    source.Stop();
                }
            }
        }

        /// <summary>
        /// Plays a SnapAsset at the given world position using a pooled SnapSource.
        /// </summary>
        /// <param name="asset">The audio asset to play.</param>
        /// <param name="position">World position to play the sound at.</param>
        /// <returns>The SnapSource used for playback, or null if playback failed.</returns>
        public SnapSource Play(SnapAsset asset, Vector3 position)
        {
            if (asset == null)
            {
                Debug.LogWarning($"{nameof(SnapPool)}: Provided asset is null.");
                return null;
            }

            var source = GetAvailableSource();
            if (source == null)
            {
                Debug.LogWarning($"{nameof(SnapPool)}: No available sources in pool.");
                return null;
            }

            source.transform.position = position;
            source.Play(asset);
            return source;
        }
    }
}
