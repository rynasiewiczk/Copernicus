namespace LazySloth.Utilities.Editor
{
    using System;
    using UnityEngine;

    [Serializable]
    public class ProjectWindowElementData
    {
        [SerializeField] private string _name;
        [SerializeField] private Color _color;

        public string Name => _name;
        public Color Color => _color;
    }
}