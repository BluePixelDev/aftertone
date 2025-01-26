using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

namespace BP.WavePool
{
    /// <summary>
    /// A pool for audio sources that can be played in the scene.
    /// Loads prefab from Resources folder.
    /// Automatically initializes itself upon request.
    /// </summary>
    public class WavePool : MonoBehaviour
    {
        // Singleton
        public static WavePool Instance
        {
            get
            {
                if (instance == null) WavePoolSystem.CreateWavePool();
                return instance;
            }
        }
        private static WavePool instance;

        // Pool
        private readonly List<IWaveSource> pool = new();
        public int PoolCount => pool.Count;

        // Prefab
        private GameObject waveSourcePrefab;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                waveSourcePrefab = Resources.Load<GameObject>("WaveSource");
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
            var config = WavePoolSystem.Config;
            for (int i = 0; i < config.InitialWaveCapacity; i++)
            {
                AddWaveItemToPool();
            }
        }

        /// <summary>
        /// Retrieves an available wave source from the pool or creates more if necessary.
        /// </summary>
        /// <returns>An available wave source.</returns>
        private IWaveSource GetAvailableWaveItem()
        {
            foreach (var item in pool)
            {
                if (!item.IsPlaying)
                {
                    return item;
                }
            }

            if (pool.Count >= WavePoolSystem.Config.MaxWaveCapacity)
            {
                Debug.LogWarning("WavePool: Reached max capacity. Can't create more waves.");
                return null;
            }

            return CreateBatch(WavePoolSystem.Config.WaveCapacityIncrement).FirstOrDefault();
        }

        /// <summary>
        /// Adds a new wave source to the pool.
        /// </summary>
        /// <returns>The newly created wave source.</returns>
        private IWaveSource AddWaveItemToPool()
        {
            GameObject obj = Instantiate(waveSourcePrefab, transform);
            obj.name = $"Source #{pool.Count}";
            var waveItem = obj.GetComponent<IWaveSource>();
            obj.SetActive(false);
            pool.Add(waveItem);
            return waveItem;
        }

        /// <summary>
        /// Creates a batch of new wave sources and adds them to the pool.
        /// </summary>
        /// <param name="count">Number of wave sources to create.</param>
        /// <returns>The created wave sources.</returns>
        private IWaveSource[] CreateBatch(int count)
        {
            IWaveSource[] batch = new IWaveSource[count];
            for (int i = 0; i < count; i++)
            {
                batch[i] = AddWaveItemToPool();
            }

            return batch;
        }

        /// <summary>
        /// Plays a wave using the given WaveSourceData.
        /// </summary>
        private void PlayWaveInternal(WaveSourceData sourceData, Vector3 position)
        {
            if (sourceData.audioResource == null)
            {
                Debug.LogWarning("WavePool: AudioResource is null. Can't play wave.");
                return;
            }

            var waveItem = GetAvailableWaveItem();
            waveItem.Source.transform.position = position;
            waveItem.Play(sourceData);
        }

        #region PUBLIC API
        /// <summary>
        /// Plays a wave using WaveSourceData.
        /// </summary>
        public static void PlayWave(WaveSourceData sourceData, Vector3 position) => Instance.PlayWaveInternal(sourceData, position);
        /// <summary>
        /// Plays a wave using a WaveAsset and a position.
        /// </summary>
        public static void PlayWave(WaveAsset waveAsset, Vector3 position)
        {
            var sourceData = waveAsset.ConvertToSourceData();
            Instance.PlayWaveInternal(sourceData, position);
        }
        /// <summary>
        /// Plays a wave using an AudioResource and a position.
        /// </summary>
        public static void PlayWave(AudioResource resource, Vector3 position)
        {
            var sourceData = new WaveSourceData(resource);
            Instance.PlayWaveInternal(sourceData, position);
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
