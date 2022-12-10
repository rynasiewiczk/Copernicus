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
        [SerializeField] private TextMeshProUGUI _descriptionText;

        public void Setup(Constellation constellation)
        {
            _image.sprite = constellation.Icon;
            _nameText.text = constellation.Name;
            _descriptionText.text = constellation.Description;
        }
    }
}