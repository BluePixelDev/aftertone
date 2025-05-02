using UnityEditor;
using UnityEngine;

namespace BP.SoundSnap
{
#if UNITY_EDITOR
    [InitializeOnLoad]
#endif
    /// <summary>
    /// Static manager for the SoundSnap system.
    /// Handles configuration and audio pool initialization and access.
    /// </summary>
    public static class SoundSnap
    {
        private const string ConfigName = "OneShotSFXConfig";
        private const string ConfigPath = "Assets/Resources/";
        private const string PoolName = "SnapPool";

        private static SnapConfig snapConfig;
        private static SnapPool snapPool;

        /// <summary>
        /// Gets the <see cref="SnapConfig"/> instance from Resources or creates it if missing.
        /// </summary>
        public static SnapConfig Config
        {
            get
            {
                if (!snapConfig)
                {
                    snapConfig = SnapUtil.LoadOrCreateConfig(ConfigPath, ConfigName);
                }

                return snapConfig;
            }
        }

        /// <summary>
        /// Gets the active <see cref="SnapPool"/> in the scene, or creates it if missing.
        /// </summary>
        private static SnapPool Pool
        {
            get
            {
                if (!snapPool)
                {
                    snapPool = SnapUtil.GetOrCreatePool(PoolName);
                }

                return snapPool;
            }
        }

        /// <summary>
        /// Static constructor to initialize configuration and pool in the Editor.
        /// </summary>
        static SoundSnap()
        {
            Initialize();
        }

        /// <summary>
        /// Initializes the Snap system at runtime before any scene loads.
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            snapConfig = SnapUtil.LoadOrCreateConfig(ConfigPath, ConfigName);
            if (Application.isPlaying)
            {
                snapPool = SnapUtil.GetOrCreatePool(PoolName);
            }
        }

        /// <summary>
        /// Plays a <see cref="SnapAsset"/> at the origin (Vector3.zero).
        /// </summary>
        /// <param name="asset">The SnapAsset to play.</param>
        /// <returns>The <see cref="SnapSource"/> used, or null if playback failed.</returns>
        public static SnapSource Play(SnapAsset asset) => Play(asset, Vector3.zero);

        /// <summary>
        /// Plays a <see cref="SnapAsset"/> at a specified world position.
        /// </summary>
        /// <param name="asset">The SnapAsset to play.</param>
        /// <param name="position">The world position to play the sound at.</param>
        /// <returns>The <see cref="SnapSource"/> used, or null if playback failed.</returns>
        public static SnapSource Play(SnapAsset asset, Vector3 position)
        {
            if (asset == null)
            {
                Debug.LogError("SoundSnap: Cannot play null SnapAsset.");
                return null;
            }

            return Pool.Play(asset, position);
        }
    }
}
