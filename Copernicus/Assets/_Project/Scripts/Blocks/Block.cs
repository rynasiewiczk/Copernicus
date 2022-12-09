namespace _Project.Scripts
{
    using System;
    using UnityEngine;

    public class Block : MonoBehaviour
    {
        [SerializeField] private GameObject _starContainer;
        [SerializeField] private bool _hasStar;

        public bool IsOnMap { get; private set; }
        
        public bool HasStar => _hasStar;

        private void OnEnable()
        {
            _starContainer.SetActive(_hasStar);
        }

        public Vector2Int GetGridPosition()
        {
            var x = Mathf.RoundToInt(transform.position.x);
            var y = Mathf.RoundToInt(transform.position.y);
            return new Vector2Int(x, y);
        }

        public void DropOnMap()
        {
            //spawn block on a map with current grid position (new one or this one?)
        }
    }
}