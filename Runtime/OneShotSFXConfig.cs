using UnityEngine;

namespace BP.OneShotSFX
{
    public class OneShotSFXConfig : ScriptableObject
    {
        [SerializeField] private bool initializeOnLoad = true;
        [SerializeField, Min(0)] private int initialWaveCapacity = 10;
        [SerializeField, Min(1)] private int waveCapacityIncrement = 10;
        [SerializeField, Min(10)] private int maxWaveCapacity = 100;

        public bool InitializeOnLoad => initializeOnLoad;

        public int InitialWaveCapacity
        {
            get
            {
                if (initialWaveCapacity < 0)
                    initialWaveCapacity = 0;

                return initialWaveCapacity;
            }
        }
        public int WaveCapacityIncrement
        {
            get
            {
                if (waveCapacityIncrement < 1)
                    waveCapacityIncrement = 1;

                return waveCapacityIncrement;
            }
        }
        public int MaxWaveCapacity
        {
            get
            {
                if (maxWaveCapacity < 10)
                    maxWaveCapacity = 10;

                return maxWaveCapacity;
            }
        }
    }
}
