namespace _Project.Scripts.UI.BlocksStack
{
    using TMPro;
    using UnityEngine;

    public class BlocksStackController : MonoBehaviour
    {
        [SerializeField] private TextMeshPro _numberText;
        
        private int _lastCount;
        
        private void Update()
        {
            var stackCount = GameController.Instance.GroupsQueueCount;

            _numberText.text = stackCount.ToString();
            _lastCount = stackCount;
        }
    }
}