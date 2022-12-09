namespace _Project.Scripts
{
    using UnityEngine;
    using UnityEngine.UI;

    public class UiGroup : MonoBehaviour
    {
        [SerializeField] private Group _group;
        [SerializeField] private Button _button;
        
        private void OnEnable()
        {
            _button.onClick.AddListener(TryPickUpGroup);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(TryPickUpGroup);
        }

        private void TryPickUpGroup()
        {
            //blocksDragger.TryPickUp(_group);
        }
    }
}