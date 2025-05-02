using UnityEngine;
using UnityEngine.Audio;

namespace BP.SoundSnap
{
    [CreateAssetMenu(fileName = "OneShotAsset", menuName = "OneshotSFX")]
    public class SnapAsset : ScriptableObject
    {
        [SerializeField] private AudioResource resource;
        [SerializeField] private AudioMixerGroup mixerGroup;

        [Header("Audio Settings")]
        [SerializeField, Range(0, 256)] private int priority = 128;
        [SerializeField, Range(0f, 1f)] private float volume = 1f;
        [SerializeField, Range(0.5f, 3f)] private float pitch = 1f;
        [SerializeField, Range(-1f, 1f)] private float panStereo = 0f;
        [SerializeField, Range(0f, 1f)] private float spatialBlend = 1f;
        [SerializeField, Range(0f, 5f)] private float dopplerLevel = 1f;
        [SerializeField] private float minDistance = 1f;
        [SerializeField] private float maxDistance = 100f;
        [SerializeField] private AudioRolloffMode rolloffMode = AudioRolloffMode.Logarithmic;

        public AudioResource Resource => resource;
        public AudioMixerGroup MixerGroup => mixerGroup;
        public int Priority => priority;
        public float Volume => volume;
        public float Pitch => pitch;
        public float PanStereo => panStereo;
        public float SpatialBlend => spatialBlend;
        public float DopplerLevel => dopplerLevel;
        public float MinDistance => minDistance;
        public float MaxDistance => maxDistance;
        public AudioRolloffMode RolloffMode => rolloffMode;

        public static SnapAsset CreateDefaultConfig()
        {
            var defaultConfig = CreateInstance<SnapAsset>();
            defaultConfig.priority = 128;
            defaultConfig.volume = 1f;
            defaultConfig.pitch = 1f;
            defaultConfig.panStereo = 0f;
            defaultConfig.spatialBlend = 1f;
            defaultConfig.dopplerLevel = 1f;
            defaultConfig.minDistance = 1f;
            defaultConfig.maxDistance = 100f;
            defaultConfig.rolloffMode = AudioRolloffMode.Logarithmic;
            return defaultConfig;
        }

        public void CopyFromSource(AudioSource source)
        {
            if (source == null) return;

            resource = source.resource;
            mixerGroup = source.outputAudioMixerGroup;
            priority = source.priority;
            volume = source.volume;
            pitch = source.pitch;
            panStereo = source.panStereo;
            spatialBlend = source.spatialBlend;
            dopplerLevel = source.dopplerLevel;
            minDistance = source.minDistance;
            maxDistance = source.maxDistance;
            rolloffMode = source.rolloffMode;
        }

        public void CopyToSource(AudioSource source)
        {
            if (source == null) return;

            source.resource = resource;
            source.outputAudioMixerGroup = mixerGroup;
            source.priority = priority;
            source.volume = volume;
            source.pitch = pitch;
            source.panStereo = panStereo;
            source.spatialBlend = spatialBlend;
            source.dopplerLevel = dopplerLevel;
            source.minDistance = minDistance;
            source.maxDistance = maxDistance;
            source.rolloffMode = rolloffMode;
        }

        public void ResetSource(AudioSource source)
        {
            if (source == null) return;

            source.resource = null;
            source.outputAudioMixerGroup = null;
            source.priority = 128;
            source.volume = 1;
            source.pitch = 1;
            source.panStereo = 0;
            source.spatialBlend = 1;
            source.dopplerLevel = 1;
            source.minDistance = 1;
            source.maxDistance = 100;
            source.rolloffMode = AudioRolloffMode.Logarithmic;
        }
    }
}
