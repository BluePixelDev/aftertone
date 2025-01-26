using UnityEngine;
using UnityEngine.Audio;

namespace BP.WavePool
{
    [CreateAssetMenu(fileName = "WaveAsset", menuName = "Wave Pool/Wave Asset")]
    public class WaveAsset : ScriptableObject
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
    }
}
