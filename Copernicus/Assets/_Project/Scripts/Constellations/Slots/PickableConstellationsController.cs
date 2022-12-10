namespace _Project.Scripts.Constellations
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using Utils;

    public class PickableConstellationsController : MonoBehaviour
    {
        [SerializeField] private List<PickableConstellationSlot> _slots;

        private void Awake()
        {
            GameController.Instance.OnConstellationShowing += FillSlot;
        }
        
        private void OnDestroy()
        {
            if (GameController.Instance != null)
            {
                GameController.Instance.OnConstellationShowing -= FillSlot;
            }
        }

        private void FillSlot(Constellation constellation)
        {
            constellation.Root.ChangeLayerToDesk();
            foreach (var slot in _slots)
            {
                if (slot.HasNotDroppedConstellation)
                {
                    continue;
                }
                
                slot.SetConstellation(constellation);
                return;
            }
        }
    }
}