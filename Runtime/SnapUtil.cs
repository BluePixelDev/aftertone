using System.IO;
using UnityEditor;
using UnityEngine;

namespace BP.SoundSnap
{
    /// <summary>
    /// Utility methods for managing Snap audio components and configuration.
    /// </summary>
    internal static class SnapUtil
    {
        /// <summary>
        /// Loads a <see cref="SnapConfig"/> from the Resources folder, or creates it if not found.
        /// </summary>
        /// <param name="configPath">The relative path (inside Assets) to save the config asset.</param>
        /// <param name="configName">The name of the config asset (without extension).</param>
        /// <returns>The loaded or newly created <see cref="SnapConfig"/> asset.</returns>
        public static SnapConfig LoadOrCreateConfig(string configPath, string configName)
        {
            var config = Resources.Load<SnapConfig>(configName);
#if UNITY_EDITOR
            if (config == null)
            {
                config = CreateConfigAsset(configPath, configName);
            }
#endif
            return config;
        }

#if UNITY_EDITOR
        /// <summary>
        /// Creates a new <see cref="SnapConfig"/> asset at the given path.
        /// </summary>
        /// <param name="configPath">The relative path (inside Assets) to save the config asset.</param>
        /// <param name="configName">The name of the config asset (without extension).</param>
        /// <returns>The created <see cref="SnapConfig"/> asset.</returns>
        private static SnapConfig CreateConfigAsset(string configPath, string configName)
        {
            var config = ScriptableObject.CreateInstance<SnapConfig>();
            string fullFolderPath = Path.Combine("Assets", configPath).TrimEnd('/');
            if (!AssetDatabase.IsValidFolder(fullFolderPath))
            {
                Directory.CreateDirectory(fullFolderPath);
                AssetDatabase.Refresh();
            }

            string assetPath = Path.Combine(fullFolderPath, configName + ".asset");
            AssetDatabase.CreateAsset(config, assetPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            Debug.Log($"SnapUtil: Created new config asset at {assetPath}");
            return config;
        }
#endif

        /// <summary>
        /// Creates a new GameObject in the scene with a <see cref="SnapPool"/> component, if one doesn't already exist.
        /// </summary>
        /// <param name="poolName">The name to assign to the new GameObject.</param>
        /// <returns>The created <see cref="SnapPool"/> component, or null if one already exists.</returns>
        public static SnapPool GetOrCreatePool(string poolName)
        {
            var snapPool = Object.FindFirstObjectByType<SnapPool>();
            if (snapPool != null)
                return snapPool;

            var gameObject = new GameObject(poolName);
            return gameObject.AddComponent<SnapPool>();
        }
    }
}
