namespace _Project.Scripts.UI.BlocksStack
{
    using Effects;
    using Sirenix.OdinInspector;
    using TMPro;
    using UnityEngine;

    public class BlocksStackController : MonoBehaviour
    {
        [SerializeField] private TextMeshPro _numberText;

        [SerializeField] private FloatingTextEffect _textEffect;
        [SerializeField] private Transform _textEffectSource;

        private int _lastCount;

        private bool _skippedInitialBlocksSpawnWithFloatingText;

        private void OnEnable()
        {
            GameController.Instance.OnGroupsCreated += ShowTextEffect;
            _textEffect.gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            GameController.Instance.OnGroupsCreated -= ShowTextEffect;
        }

        private void Update()
        {
            var stackCount = GameController.Instance.GroupsQueueCount;

            _numberText.text = stackCount.ToString();
            _lastCount = stackCount;
        }

        [Button]
        private void ShowTextEffect(int count)
        {
            if (!_skippedInitialBlocksSpawnWithFloatingText)
            {
                _skippedInitialBlocksSpawnWithFloatingText = true;
                return;
            }
            
            _textEffect.Setup(_textEffectSource.position, $"+{count.ToString()}", DisableTextEffect);
        }

        private void DisableTextEffect() => _textEffect.gameObject.SetActive(false);
    }
}