namespace LazySloth.Editor
{
    using UnityEditor;
    using UnityEngine;

    public static class ScriptableObjectHelpers
    {
        public static T[] GetAllInstances<T>() where T : ScriptableObject
        {
            string[] guids = AssetDatabase.FindAssets("t:" + typeof(T).Name); //FindAssets uses tags check documentation for more info
            T[] allInstances = new T[guids.Length];
            for (int i = 0; i < guids.Length; i++) //probably could get optimized 
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[i]);
                allInstances[i] = AssetDatabase.LoadAssetAtPath<T>(path);
            }

            return allInstances;
        }
    }
}