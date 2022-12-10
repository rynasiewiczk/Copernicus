namespace _Project.Scripts.UI
{
    using UnityEngine;

    public class UiController : SingletonBehaviour<UiController>
    {
        private bool IsWindowOpen { get; set; }

        private InteractionIgnoreReason _openWindowReason = new("Window open");
        
        public void SetWindowOpen(bool open)
        {
            if (IsWindowOpen == open)
            {
                return;
            }
            
            IsWindowOpen = open;

            if (open)
            {
                GameController.Instance.InteractionIgnoreReasons.Add(_openWindowReason);
            }
            else
            {
                if (GameController.Instance.InteractionIgnoreReasons.Contains(_openWindowReason))
                {
                    GameController.Instance.InteractionIgnoreReasons.Remove(_openWindowReason);
                }
                else
                {
                    Debug.LogError("Reasons do not contain window reason");
                }
            }
        }
    }
}