using UnityEngine;
using UnityEngine.Audio;

namespace BP.WavePool
{
    public class WaveSourceData
    {
        public AudioResource audioResource;
        public AudioMixerGroup audioMixerGroup;

        public float volume = 1f;
        public float pitch = 1f;
        public float panStereo = 0f;
        public float spatialBlend = 1f;
        public float dopplerLevel = 1f;
        public float minDistance = 1f;
        public float maxDistance = 100f;
        public AudioRolloffMode rolloffMode = AudioRolloffMode.Logarithmic;

        public WaveSourceData(AudioResource audioResource)
        {
            this.audioResource = audioResource;
        }
        public WaveSourceData(AudioResource audioResource, AudioMixerGroup audioMixerGroup)
        {
            this.audioResource = audioResource;
            this.audioMixerGroup = audioMixerGroup;
        }
        public WaveSourceData(AudioResource audioResource, AudioMixerGroup audioMixerGroup, float pitch, float panStereo, float spatialBlend, float dopplerLevel, float minDistance, float maxDistance, AudioRolloffMode rolloffMode)
        {
            this.audioResource = audioResource;
            this.audioMixerGroup = audioMixerGroup;
            this.pitch = pitch;
            this.panStereo = panStereo;
            this.spatialBlend = spatialBlend;
            this.dopplerLevel = dopplerLevel;
            this.minDistance = minDistance;
            this.maxDistance = maxDistance;
            this.rolloffMode = rolloffMode;
        }

        public void ApplyTo(AudioSource audioSource)
        {
            audioSource.resource = audioResource;
            audioSource.outputAudioMixerGroup = audioMixerGroup;
            audioSource.pitch = pitch;
            audioSource.panStereo = panStereo;
            audioSource.spatialBlend = spatialBlend;
            audioSource.dopplerLevel = dopplerLevel;
            audioSource.minDistance = minDistance;
            audioSource.maxDistance = maxDistance;
            audioSource.rolloffMode = rolloffMode;
        }
    }
}
