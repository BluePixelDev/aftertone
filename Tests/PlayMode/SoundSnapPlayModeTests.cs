using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

namespace BP.SoundSnap.Tests
{
    public class SoundSnapPlayModeTests : MonoBehaviour
    {
        private SnapAsset testAsset;

        [UnitySetUp]
        public IEnumerator SetUp()
        {
            // Set up a dummy SnapAsset
            GameObject dummy = new("TestAudioSource");
            var audioSource = dummy.AddComponent<AudioSource>();
            testAsset = ScriptableObject.CreateInstance<SnapAsset>();
            testAsset.CopyFromSource(audioSource);

            yield return null;
        }

        [UnityTest]
        public IEnumerator CanPlaySnapAsset()
        {
            var source = SoundSnap.Play(testAsset, Vector3.zero);
            Assert.IsNotNull(source);
            Assert.IsTrue(source.gameObject.activeSelf);

            yield return new WaitForSeconds(0.1f);
            source.Stop();

            Assert.IsFalse(source.IsPlaying);
        }

        [UnityTest]
        public IEnumerator Pool_ReusesInactiveSource()
        {
            var first = SoundSnap.Play(testAsset, Vector3.zero);
            first.Stop();

            yield return new WaitForSeconds(0.1f);

            var second = SoundSnap.Play(testAsset, Vector3.zero);
            Assert.AreSame(first, second, "Should reuse the same SnapSource from pool.");
        }
    }
}
