namespace _Project.Scripts.Constellations
{
    using System.Collections.Generic;
    using LazySloth.Utilities;
    using UnityEngine;

    public class ConstellationsCatalog : ScriptableObject
    {
        [SerializeField] private List<Constellation> _constellations;
        
        public IReadOnlyList<Constellation> Constellations => _constellations;

        public Constellation GetRandomConstellation() => _constellations.GetRandom();
    }
}