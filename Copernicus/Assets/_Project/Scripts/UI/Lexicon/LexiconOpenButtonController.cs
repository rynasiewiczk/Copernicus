namespace _Project.Scripts.UI
{
    using System;
    using Constellations;
    using UnityEngine;

    public class LexiconOpenButtonController : MonoBehaviour
    {
        [SerializeField] private GameObject _newConstellationNotification;

        private void OnEnable()
        {
            GameController.Instance.OnNewConstellationPlaced += ShowNotification;
            SetNotificationActive(false);
        }

        private void OnDisable()
        {
            GameController.Instance.OnNewConstellationPlaced -= ShowNotification;
        }

        private void ShowNotification(Constellation _) => SetNotificationActive(true);

        private void SetNotificationActive(bool active) => _newConstellationNotification.SetActive(active);

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

            SetNotificationActive(false);
            var lexicon = FindObjectOfType<LexiconController>(true);
            lexicon.Open();
        }
    }
}