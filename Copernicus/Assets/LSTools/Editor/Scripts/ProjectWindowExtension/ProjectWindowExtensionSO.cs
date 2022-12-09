namespace LazySloth.Utilities.Editor
{
    using System.Collections.Generic;
    using UnityEngine;

    [CreateAssetMenu(fileName = "ProjectWindowExtensionSO", menuName = "LazySlothTools/ProjectWindowExtensionSO")]
    public class ProjectWindowExtensionSO : ScriptableObject
    {
        [SerializeField] private List<ProjectWindowElementData> _elements;

        public IReadOnlyList<ProjectWindowElementData> Elements => _elements;
    }
}