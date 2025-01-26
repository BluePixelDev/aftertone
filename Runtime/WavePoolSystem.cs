using UnityEditor;
using UnityEngine;

namespace BP.WavePool
{
#if UNITY_EDITOR
    [InitializeOnLoad] // Ensures initialization in the Editor
#endif
    public static class WavePoolSystem
    {
        public static WavePoolConfig Config { get; private set; }

        private const string ConfigName = "WavePoolConfig";
        private const string ConfigPath = "Assets/Resources/";

        static WavePoolSystem()
        {
            Initialize();
        }

        /// <summary>
        /// Initializes the WavePoolSystem at runtime or in the editor.
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Initialize()
        {
            LoadOrCreateConfig();

            if (Application.isPlaying && Config.InitializeOnLoad)
            {
                CreateWavePool();
            }
        }

        /// <summary>
        /// Loads the WavePoolConfig from the Resources folder, or creates it if it doesn't exist.
        /// </summary>
        private static void LoadOrCreateConfig()
        {
            // Load the existing configuration from Resources
            Config = Resources.Load<WavePoolConfig>(ConfigName);

            if (Config == null)
            {
                Debug.LogWarning("WavePoolSystem: Configuration not found. Creating a new configuration asset.");
                CreateConfigAsset();
            }
        }

        /// <summary>
        /// Creates the WavePool GameObject in the scene.
        /// </summary>
        /// <returns>The created WavePool GameObject.</returns>
        public static GameObject CreateWavePool()
        {
            if (Object.FindFirstObjectByType<WavePool>() != null)
            {
                Debug.LogWarning("WavePoolSystem: A WavePool instance already exists in the scene.");
                return null;
            }

            return new GameObject("WavePool", typeof(WavePool));
        }

        /// <summary>
        /// Creates a new WavePoolConfig asset in the Resources folder.
        /// </summary>
        private static void CreateConfigAsset()
        {
#if UNITY_EDITOR
            // Create a new instance of the WavePoolConfig ScriptableObject
            Config = ScriptableObject.CreateInstance<WavePoolConfig>();

            // Ensure the Resources directory exists
            if (!AssetDatabase.IsValidFolder(ConfigPath.TrimEnd('/')))
            {
                AssetDatabase.CreateFolder("Assets", "Resources");
            }

            // Save the new configuration as an asset
            string fullPath = ConfigPath + ConfigName + ".asset";
            AssetDatabase.CreateAsset(Config, fullPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
#endif
        }
    }
}
