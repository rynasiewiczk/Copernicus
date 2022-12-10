namespace _Project.Scripts.UI
{
    using System;
    using System.Linq;
    using Constellations;
    using UnityEngine;
    using UnityEngine.UI;

    public class LexiconConstellationListItem : MonoBehaviour
    {
        private event Action<Constellation> OnConstellationClick;
        private void FireConstellationClick(Constellation constellation) => OnConstellationClick?.Invoke(constellation);

        [SerializeField] private Image _image;
        [SerializeField] private Button _button;

        public Constellation Constellation { get; private set; }

        private void OnEnable()
        {
            _button.onClick.AddListener(HandleConstellationClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(HandleConstellationClick);
        }

        private void HandleConstellationClick() => FireConstellationClick(Constellation);

        public void Setup(Constellation constellation, Action<Constellation> onClick)
        {
            Constellation = constellation;
            _image.sprite = constellation.Icon;

            OnConstellationClick = onClick;
        }
    }
}