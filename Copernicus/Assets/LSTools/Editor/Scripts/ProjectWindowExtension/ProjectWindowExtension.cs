namespace LazySloth.Utilities.Editor
{
    using System.Linq;
    using UnityEditor;
    using UnityEngine;
    using Object = UnityEngine.Object;

    [InitializeOnLoad]
    public class ProjectWindowExtension
    {
        private static readonly ProjectWindowExtensionSO _projectWindowExtensionSO;

        static ProjectWindowExtension()
        {
            var guids = AssetDatabase.FindAssets($"t:{typeof(ProjectWindowExtensionSO)}");
            foreach (var guid in guids)
            {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                var asset = AssetDatabase.LoadAssetAtPath<ProjectWindowExtensionSO>(path);
                if (asset != null)
                {
                    _projectWindowExtensionSO = asset;
                    EditorApplication.projectWindowItemOnGUI += ColorizeKeyFolders;

                    return;
                }
            }

            Debug.LogWarning($"Didn't find {typeof(ProjectWindowExtensionSO)} asset in the project.");
        }

        private static void ColorizeKeyFolders(string guid, Rect rect)
        {
            var assetPath = AssetDatabase.GUIDToAssetPath(guid);
            if (!AssetDatabase.IsValidFolder(assetPath))
            {
                return;
            }

            var asset = AssetDatabase.LoadAssetAtPath<Object>(assetPath);
            if (asset == null)
            {
                // this entry could be Favourites or Packages. Ignore it.
                return;
            }

            if (!IsFolderToColorize(asset.name)) { return; }

            var color = GetColor(asset.name);

            GUI.DrawTexture(rect, Texture2D.whiteTexture, ScaleMode.ScaleAndCrop, true, .5f, color, 50, 5);
        }

        private static bool IsFolderToColorize(string assetName)
        {
            return _projectWindowExtensionSO != null && _projectWindowExtensionSO.Elements.Any(x => x.Name == assetName);
        }

        private static Color GetColor(string assetName)
        {
            return _projectWindowExtensionSO == null ? Color.white : _projectWindowExtensionSO.Elements.First(x => x.Name == assetName).Color;
        }
    }
}