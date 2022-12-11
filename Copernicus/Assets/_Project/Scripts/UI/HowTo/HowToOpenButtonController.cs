namespace _Project.Scripts.UI.HowTo
{
    using Constellations;
    using UnityEngine;

    public class HowToOpenButtonController : MonoBehaviour
    {
        private void OnMouseUpAsButton()
        {
            OpenLexicon();
        }

        private void OpenLexicon()
        {
            if (PlayerController.Instance.HasDraggable)
            {
                return;
            }

            if (GameController.Instance.HasInteractionIgnoreReason)
            {
                return;
            }

            var lexicon = FindObjectOfType<HowToController>(true);
            lexicon.Open();
        }
    }
}