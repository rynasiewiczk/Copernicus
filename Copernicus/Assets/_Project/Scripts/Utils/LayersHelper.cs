namespace _Project.Scripts.Utils
{
    using UnityEngine;

    public static class LayersHelper
    {
        public static void ChangeLayerToDesk(this Transform root)
        {
            var layer = LayerMask.NameToLayer("Desk");
            root.ChangeLayer(layer);
        }

        public static void ChangeLayerToAboveDesk(this Transform root)
        {
            var layer = LayerMask.NameToLayer("AboveDesk");
            root.ChangeLayer(layer);
        }

        public static void ChangeLayerToDefault(this Transform root)
        {
            var layer = LayerMask.NameToLayer("Default");
            root.ChangeLayer(layer);
        }
        
        private static void ChangeLayer(this Transform root, int newLayer)
        {
            var children = root.GetComponentsInChildren<Transform>(includeInactive: true);
            foreach (var child in children)
            {
                child.gameObject.layer = newLayer;
            }
        }
    }
}