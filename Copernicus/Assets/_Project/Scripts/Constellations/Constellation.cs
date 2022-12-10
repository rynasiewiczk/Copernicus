namespace _Project.Scripts.Constellations
{
    using System;
    using System.Collections.Generic;
    using Sirenix.OdinInspector;
    using UnityEngine;

    public class Constellation : MonoBehaviour, IDraggable
    {
        [SerializeField] private List<ConstellationPart> _parts;
        [SerializeField] private List<ConstellationConnection> _connections;

        public Transform Root => gameObject.transform;
        public IReadOnlyList<ConstellationPart> Parts => _parts;
        public IReadOnlyList<ConstellationConnection> Connections => _connections;
        
        public bool IsDroppedOnBoard { get; private set; }

        public void SetWorldPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void ResetRotation()
        {
            transform.eulerAngles = Vector3.zero;
        }

        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }

        public void SetParentAndScale(Transform parent, Vector3 scale)
        {
            transform.SetParent(parent);
            transform.localScale = scale;
        }
        
        public void SetParent(Transform container)
        {
            transform.SetParent(container, false);
            transform.localPosition = Vector3.zero;
        }

        public void SetAsDroppedOnBoard() => IsDroppedOnBoard = true;
        
        [Button]
        public void Setup()
        {
            _parts.Clear();
            var parts = GetComponentsInChildren<ConstellationPart>();
            foreach (var part in parts)
            {
                _parts.Add(part);
            }
        }

        private void OnDrawGizmos()
        {
            if(_connections == null) { return; }
            foreach (var connection in _connections)
            {
                if (connection.First == null || connection.Second == null)
                {
                    continue;
                }
                
                Gizmos.color = Color.green;
                Gizmos.DrawLine(connection.First.transform.position, connection.Second.transform.position);
            }
        }
    }

    [Serializable]
    public class ConstellationConnection
    {
        [SerializeField] private ConstellationPart _first;
        [SerializeField] private ConstellationPart _second;

        public ConstellationPart First => _first;
        public ConstellationPart Second => _second;
    }
}