namespace _Project.Scripts.UI
{
    using System.Collections.Generic;
    using UnityEngine;

    public class PickableGroupsController : MonoBehaviour
    {
        [SerializeField] private List<PickableGroupSlot> _slots;

        private void Awake()
        {
            GameController.Instance.OnGroupShowing += FillSlot;
        }

        private void OnDestroy()
        {
            if (GameController.Instance != null)
            {
                GameController.Instance.OnGroupShowing -= FillSlot;
            }
        }

        private void FillSlot(Group group)
        {
            foreach (var slot in _slots)
            {
                if (slot.HasNotDroppedGroup)
                {
                    continue;
                }
                
                slot.SetGroup(group);
                return;
            }
        }
    }
}