namespace _Project.Scripts.UI
{
    using UnityEngine;

    public class LexiconOpenButtonController : MonoBehaviour
    {
        private void OnMouseDown()
        {
            OpenLexicon();
        }

        private void OpenLexicon()
        {
            if (PlayerController.Instance.HasDraggable)
            {
                return;
            }

            if (UiController.Instance.IsWindowOpen)
            {
                return;
            }

            var lexicon = FindObjectOfType<LexiconController>(true);
            lexicon.Open();
        }
    }
}