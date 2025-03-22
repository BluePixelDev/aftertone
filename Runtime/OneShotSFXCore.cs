using UnityEditor;
using UnityEngine;

namespace BP.OneShotSFX
{
#if UNITY_EDITOR
    [InitializeOnLoad]
#endif
    public static class OneShotSFXCore
    {
        public static OneShotSFXConfig Config { get; private set; }

        private const string ConfigName = "OneShotSFXConfig";
        private const string ConfigPath = "Assets/Resources/";

        static OneShotSFXCore()
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
                CreatePool();
            }
        }

        /// <summary>
        /// Loads the WavePoolConfig from the Resources folder, or creates it if it doesn't exist.
        /// </summary>
        private static void LoadOrCreateConfig()
        {
            // Load the existing configuration from Resources
            Config = Resources.Load<OneShotSFXConfig>(ConfigName);

            if (Config == null)
            {
                Debug.LogWarning("OneShotSFX: Configuration not found. Creating a new configuration asset.");
                CreateConfigAsset();
            }
        }

        /// <summary>
        /// Creates the WavePool GameObject in the scene.
        /// </summary>
        /// <returns>The created WavePool GameObject.</returns>
        public static GameObject CreatePool()
        {
            if (Object.FindFirstObjectByType<OneShotPool>() != null)
            {
                Debug.LogWarning("OneShotSFX: A WavePool instance already exists in the scene.");
                return null;
            }

            return new GameObject("OneShotSFX", typeof(OneShotPool));
        }

        private static void CreateConfigAsset()
        {
#if UNITY_EDITOR
            Config = ScriptableObject.CreateInstance<OneShotSFXConfig>();
            if (!AssetDatabase.IsValidFolder(ConfigPath.TrimEnd('/')))
            {
                AssetDatabase.CreateFolder("Assets", "Resources");
            }

            string fullPath = ConfigPath + ConfigName + ".asset";
            AssetDatabase.CreateAsset(Config, fullPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
#endif
        }
    }
}
