namespace _Project.Scripts
{
    using UnityEngine;

    public class BlockStar : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Color _usedColor;
        
        public bool IsAlreadyUsed { get; private set; }
        
        public void MarkUsed()
        {
            IsAlreadyUsed = true;
            _spriteRenderer.color = _usedColor;
        }
    }
}