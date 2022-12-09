namespace _Project.Scripts
{
    using UnityEngine;

    public class BlockStar : MonoBehaviour
    {
        public bool IsAlreadyUsed { get; private set; }
        
        public void MarkUsed()
        {
            IsAlreadyUsed = true;
        }
    }
}