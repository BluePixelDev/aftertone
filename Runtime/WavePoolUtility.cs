using UnityEngine;

namespace BP.WavePool
{
    public static class WavePoolUtility
    {
        public static WaveSourceData GetSourceData(this AudioSource audioSource)
        {
            return new(audioSource.resource)
            {
                audioMixerGroup = audioSource.outputAudioMixerGroup,
                volume = audioSource.volume,
                pitch = audioSource.pitch,
                panStereo = audioSource.panStereo,
                spatialBlend = audioSource.spatialBlend,
                dopplerLevel = audioSource.dopplerLevel,
                minDistance = audioSource.minDistance,
                maxDistance = audioSource.maxDistance,
                rolloffMode = audioSource.rolloffMode
            };
        }
        public static WaveSourceData ConvertToSourceData(this WaveAsset soundAsset)
        {
            WaveSourceData waveSourceData = new(soundAsset.Resource)
            {
                audioMixerGroup = soundAsset.MixerGroup,
                volume = soundAsset.Volume,
                pitch = soundAsset.Pitch,
                panStereo = soundAsset.PanStereo,
                spatialBlend = soundAsset.SpatialBlend,
                dopplerLevel = soundAsset.DopplerLevel,
                minDistance = soundAsset.MinDistance,
                maxDistance = soundAsset.MaxDistance,
                rolloffMode = soundAsset.RolloffMode
            };
            return waveSourceData;
        }
        public static void SetSourceData(this AudioSource audioSource, WaveSourceData sourceData)
        {
            audioSource.resource = sourceData.audioResource;
            audioSource.volume = sourceData.volume;
            audioSource.outputAudioMixerGroup = sourceData.audioMixerGroup;
            audioSource.pitch = sourceData.pitch;
            audioSource.panStereo = sourceData.panStereo;
            audioSource.spatialBlend = sourceData.spatialBlend;
            audioSource.dopplerLevel = sourceData.dopplerLevel;
            audioSource.minDistance = sourceData.minDistance;
            audioSource.maxDistance = sourceData.maxDistance;
            audioSource.rolloffMode = sourceData.rolloffMode;
        }
        public static void SetSourceData(this AudioSource audioSource, WaveAsset soundAsset)
        {
            SetSourceData(audioSource, soundAsset.ConvertToSourceData());
        }
    }
}
