namespace _Project.Scripts.UI
{
    using System.Collections.Generic;
    using Sirenix.OdinInspector;
    using UnityEngine;

    public class PickableGroupsController : MonoBehaviour
    {
        [SerializeField] private GroupsCatalog _groupsCatalog;
        [SerializeField] private List<PickableGroupSlot> _slots;

        private void Start()
        {
            FillAllSlots();
        }

        [Button]
        private void FillAllSlots(bool replaceExisting = false)
        {
            if (!replaceExisting)
            {
                foreach (var slot in _slots)
                {
                    if (!slot.HasGroup)
                    {
                        var group = _groupsCatalog.GetRandomGroup();
                        var instance = Instantiate(group);
                        slot.SetGroup(instance);
                    }
                }
            }
            else
            {
                ClearSlots();

                FillAllSlots();
            }
        }

        [Button]
        private void ClearSlots()
        {
            foreach (var slot in _slots)
            {
                if (slot.HasGroup)
                {
                    slot.Clear();
                }
            }
        }
        
        //debug
        [Button]
        private void DestroyGroups()
        {
            _slots.ForEach(x => x.DestroyGroup());
        }
    }
}