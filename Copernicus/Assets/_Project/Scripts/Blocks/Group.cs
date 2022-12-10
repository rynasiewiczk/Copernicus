namespace _Project.Scripts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CarterGames.Assets.AudioManager;
    using DG.Tweening;
    using LazySloth.Observable;
    using Sirenix.OdinInspector;
    using UnityEngine;

    public class Group : MonoBehaviour, IDraggable
    {
        [SerializeField] private List<Block> _blocks;

        [SerializeField] private Color _defaultColor;
        [SerializeField] private Color _pickedUpColor;

        [SerializeField] private List<Sprite> _blockSpritesToUse;

        private float _rotationDuration = .15f;
        private float _dropOnMapDuration = .15f;
        private float _bumpDuration = .2f;

        public Transform Root => gameObject.transform;
        public ObservableProperty<bool> IsDragged { get; } = new();
        public IReadOnlyList<Block> Blocks => _blocks;
        public bool IsOnMap => _blocks.Any(x => x.IsOnMap);

        private void OnEnable()
        {
            IsDragged.Subscribe(SetColorForDrag);
        }

        private void OnDisable()
        {
            IsDragged.Unsubscribe(SetColorForDrag);
        }

        private void SetColorForDrag(bool isDragged)
        {
            foreach (var block in _blocks)
            {
                block.SetColor(isDragged ? _pickedUpColor : _defaultColor);
            }
        }

        public void Init(int numberOfStars)
        {
            var spriteToUse = _blockSpritesToUse.OrderBy(x => Guid.NewGuid()).First();
            var blocksInRandom = _blocks.OrderBy(x => Guid.NewGuid()).ToList();
            for (var i = 0; i < blocksInRandom.Count; i++)
            {
                blocksInRandom[i].Init(spriteToUse, i < numberOfStars);
            }
        }

        public void DropOnMap(bool silent = false)
        {
            if(!silent) { AudioManager.instance.Play("group_fit"); }
            var reason = new InteractionIgnoreReason("Placing block");
            GameController.Instance.InteractionIgnoreReasons.Add(reason);
            DOVirtual.DelayedCall(_dropOnMapDuration, () => GameController.Instance.InteractionIgnoreReasons.Remove(reason));

            foreach (var block in _blocks)
            {
                block.SetColor(_defaultColor);
                block.DropOnMap(_dropOnMapDuration);
            }
        }

        public void ChangeLayer(int newLayer)
        {
            var root = gameObject.transform;
            var children = root.GetComponentsInChildren<Transform>(includeInactive: true);
            foreach (var child in children)
            {
                child.gameObject.layer = newLayer;
            }
        }

        public void RotateLeft()
        {
            AudioManager.instance.Play("group_rotation");

            var interactionIgnoreReason = new InteractionIgnoreReason("Rotating group");
            GameController.Instance.InteractionIgnoreReasons.Add(interactionIgnoreReason);
            transform.DORotate(new Vector3(0, 0, -90), _rotationDuration, RotateMode.WorldAxisAdd).OnComplete(() =>
            {
                GameController.Instance.InteractionIgnoreReasons.Remove(interactionIgnoreReason);
            });
        }

        public void RotateRight()
        {
             AudioManager.instance.Play("group_rotation");

            var interactionIgnoreReason = new InteractionIgnoreReason("Rotating group");
            GameController.Instance.InteractionIgnoreReasons.Add(interactionIgnoreReason);
            transform.DORotate(new Vector3(0, 0, 90), _rotationDuration, RotateMode.WorldAxisAdd).OnComplete(() =>
            {
                GameController.Instance.InteractionIgnoreReasons.Remove(interactionIgnoreReason);
            });
        }

        public void ResetRotation() => transform.eulerAngles = Vector3.zero;

        public void SetParentAndScale(Transform parent, Vector3 scale)
        {
            transform.SetParent(parent);
            transform.localScale = scale;
        }

        public void SetParent(Transform parent)
        {
            transform.SetParent(parent, false);
        }

        public void SetWorldPosition(Vector3 position)
        {
            transform.position = position;
        }
        
        public void SetLocalPosition(Vector3 position)
        {
            transform.localPosition = position;
        }

        public void SetActive(bool active) => gameObject.SetActive(active);

        public void PlayBump()
        {
            var reason = new InteractionIgnoreReason("Group bump");
            GameController.Instance.InteractionIgnoreReasons.Add(reason);
            transform.DOPunchScale(Vector3.one * 0.1f, _bumpDuration).OnComplete(() => GameController.Instance.InteractionIgnoreReasons.Remove(reason));
        }
        
        ///////////

        [Button]
        private void Validate()
        {
            OnValidate();
        }

        private void OnValidate()
        {
            _blocks.Clear();
            _blocks = GetComponentsInChildren<Block>().ToList();

            foreach (var block in _blocks)
            {
                block.Validate();
            }
        }
    }
}