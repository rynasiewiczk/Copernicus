namespace _Project.Scripts.Constellations
{
    using DG.Tweening;
    using UnityEngine;

    public class ConstellationPart : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _validView;

        private Tweener _validTween;
        
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

        private bool _isValid = true;
        public void SetValid(bool valid)
        {
            if (valid && !_isValid)
            {
                _isValid = true;
                _validTween?.Kill();
                _validTween = _validView.DOFade(1f, 0.3f);
            }
            else if(!valid && _isValid)
            {
                _isValid = false;
                _validTween?.Kill();
                _validTween = _validView.DOFade(0f, 0.3f);
            }
        }
    }
}