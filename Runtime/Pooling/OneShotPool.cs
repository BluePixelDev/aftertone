using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BP.OneShotSFX
{
    /// <summary>
    /// A pool for audio sources that can be played in the scene.
    /// Loads prefab from Resources folder.
    /// Automatically initializes itself upon request.
    /// </summary>
    public class OneShotPool : MonoBehaviour
    {
        // Singleton
        public static OneShotPool Instance
        {
            get
            {
                if (instance == null) OneShotSFXCore.CreatePool();
                return instance;
            }
        }
        private static OneShotPool instance;

        // Pool
        private readonly List<IOneShotSource> pool = new();
        public int PoolCount => pool.Count;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
                InitializePool();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// Initializes the wave pool and preloads wave sources.
        /// </summary>
        private void InitializePool()
        {
            var config = OneShotSFXCore.Config;
            for (int i = 0; i < config.InitialWaveCapacity; i++)
            {
                AddWaveItemToPool();
            }
        }

        /// <summary>
        /// Retrieves an available wave source from the pool or creates more if necessary.
        /// </summary>
        /// <returns>An available wave source.</returns>
        private IOneShotSource GetAvailableWaveItem()
        {
            foreach (var item in pool)
            {
                if (!item.IsPlaying)
                {
                    return item;
                }
            }

            if (pool.Count >= OneShotSFXCore.Config.MaxWaveCapacity)
            {
                Debug.LogWarning("WavePool: Reached max capacity. Can't create more waves.");
                return null;
            }

            return CreateBatch(OneShotSFXCore.Config.WaveCapacityIncrement).FirstOrDefault();
        }

        /// <summary>
        /// Adds a new wave source to the pool.
        /// </summary>
        /// <returns>The newly created wave source.</returns>
        private IOneShotSource AddWaveItemToPool()
        {
            GameObject obj = new($"Source #{pool.Count}");
            var audioSource = obj.AddComponent<AudioSource>();
            var oneShotSource = obj.AddComponent<OneShotSource>();
            oneShotSource.Source = audioSource;

            obj.transform.SetParent(transform);
            obj.SetActive(false);
            pool.Add(oneShotSource);
            return oneShotSource;
        }

        /// <summary>
        /// Creates a batch of new wave sources and adds them to the pool.
        /// </summary>
        /// <param name="count">Number of wave sources to create.</param>
        /// <returns>The created wave sources.</returns>
        private IOneShotSource[] CreateBatch(int count)
        {
            IOneShotSource[] batch = new IOneShotSource[count];
            for (int i = 0; i < count; i++)
            {
                batch[i] = AddWaveItemToPool();
            }

            return batch;
        }

        /// <summary>
        /// Plays a wave using the given WaveSourceData.
        /// </summary>
        private void PlayWaveInternal(OneShotAsset asset, Vector3 position)
        {
            if (asset == null)
            {
                Debug.LogWarning("OneShotPool: Asset is null. Can't play sound.");
                return;
            }

            var waveItem = GetAvailableWaveItem();

            if (waveItem == null)
            {
                Debug.LogWarning("OneShotPool: No available sources. Can't play sound.");
                return;
            }

            waveItem.Source.transform.position = position;
            waveItem.Play(asset);
        }

        #region PUBLIC API
        /// <summary>
        /// Plays a wave using a WaveAsset and a position.
        /// </summary>
        public static void PlayWave(OneShotAsset waveAsset, Vector3 position)
        {
            Instance.PlayWaveInternal(waveAsset, position);
        }
        /// <summary>
        /// Stops all currently playing waves.
        /// </summary>
        public static void StopAllWaves()
        {
            foreach (var item in Instance.pool)
            {
                item.Stop();
            }
        }
        #endregion
    }
}
