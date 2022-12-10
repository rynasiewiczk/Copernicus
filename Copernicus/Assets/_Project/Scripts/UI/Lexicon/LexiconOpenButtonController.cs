namespace _Project.Scripts.UI
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class LexiconOpenButtonController : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private LexiconController _lexicon;

        private void OnEnable()
        {
            _button.onClick.AddListener(OpenLexicon);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OpenLexicon);
        }

        private void OpenLexicon() => _lexicon.Open();
    }
}