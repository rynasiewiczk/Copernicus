namespace _Project.Scripts.Constellations
{
    using UnityEngine;

    public class ConstellationPart : MonoBehaviour
    {
        [SerializeField] private GameObject _validView;
        
        public Vector2Int GetGridPosition()
        {
            var x = Mathf.RoundToInt(transform.position.x);
            var y = Mathf.RoundToInt(transform.position.y);
            return new Vector2Int(x, y);
        }

        public Vector2Int GetLocalGridPosition()
        {
            var x = Mathf.RoundToInt(transform.localPosition.x);
            var y = Mathf.RoundToInt(transform.localPosition.y);
            return new Vector2Int(x, y);
        }

        public void SetValid(bool valid)
        {
            _validView.SetActive(valid);
            //transform.localScale = valid ? Vector3.one  * 2f : Vector3.one;
        }
    }
}