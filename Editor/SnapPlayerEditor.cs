using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace BP.SoundSnap.Editor
{
    [CustomEditor(typeof(SnapPlayer))]
    public class SnapPlayerEditor : UnityEditor.Editor
    {
        [SerializeField] private VisualTreeAsset treeAsset;
        public override VisualElement CreateInspectorGUI()
        {
            var root = new VisualElement();
            treeAsset.CloneTree(root);
            return root;
        }
    }
}
