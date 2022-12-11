namespace _Project.Scripts.UI
{
    using Constellations;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    public class LexiconConstellationsDetailsController : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private Image _nameTag;
        [SerializeField] private TextMeshProUGUI _descriptionText;

        [SerializeField] private Sprite _lockedSprite;
        
        public void Setup(Constellation constellation, bool isUnlocked)
        {
            if (isUnlocked)
            {
                _image.sprite = constellation.Icon;
                //_nameTag.gameObject.SetActive(true);
                _nameTag.sprite = constellation.NameTag;
                _nameText.text = constellation.Name;
                _descriptionText.text = constellation.Description;

                return;
            }

            _image.sprite = _lockedSprite;
            //_nameTag.gameObject.SetActive(false);
            _nameText.text = string.Empty;
            _descriptionText.text = "Build this constellation to see its description";
        }
    }
}