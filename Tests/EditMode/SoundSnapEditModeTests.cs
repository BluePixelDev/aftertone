using NUnit.Framework;

namespace BP.SoundSnap.Tests
{
    public class SoundSnapEditModeTests
    {
        [Test]
        public void Config_IsLoadedOrCreated()
        {
            var config = SoundSnap.Config;
            Assert.IsNotNull(config, "SnapConfig should not be null.");
        }

        [Test]
        public void ConfigValues_RespectMinimums()
        {
            var config = SoundSnap.Config;

            Assert.GreaterOrEqual(config.InitialCapacity, 0);
            Assert.GreaterOrEqual(config.CapacityIncrement, 1);
            Assert.GreaterOrEqual(config.MaxCapacity, 10);
        }
    }
}
