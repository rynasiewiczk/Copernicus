namespace _Project.Scripts.Constellations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CarterGames.Assets.AudioManager;
    using DG.Tweening;
    using Effects;
    using LazySloth.Observable;
    using LazySloth.Utilities;
    using Sirenix.OdinInspector;
    using Unity.VisualScripting;
    using UnityEngine;

    public class Constellation : MonoBehaviour, IDraggable
    {
        [SerializeField] private Transform _partsRoot;
        [SerializeField] private List<ConstellationPart> _parts;
        [SerializeField] private List<ConstellationConnection> _connections;
        [SerializeField] private List<LineRenderer> _lines;
        [SerializeField] private LineRenderer _linePrefab;
        [SerializeField] private SpriteRenderer _glow;

        [SerializeField] private Sprite _icon;
        [SerializeField] private string _name;
        [SerializeField] private Sprite _nameTag;
        
        [SerializeField, TextArea] private string _description;

        [SerializeField] private float _fadeOutDuration = .3f;
        [SerializeField] private LineRendererConnection _droppedConnectionLinePrefab;

        [SerializeField] private int _requiredConstellationsToEnable;

        [SerializeField] private AnimationCurve _glowCurve;
        [SerializeField] private float _glowSpeed = 2;
        [SerializeField] private float _glowDuration = 1;

        private int _prevValidParts;
        private bool _isGlowing;
        private float _glowStartTime;

        public Sprite Icon => _icon;
        public string Name => _name;
        public Sprite NameTag => _nameTag;
        public string Description => _description;
        public bool IsPossibleToPutOnBoard { get; private set; }

        public Transform Root => gameObject.transform;
        public ObservableProperty<bool> IsDragged { get; } = new();
        public IReadOnlyList<ConstellationPart> Parts => _parts;
        public IReadOnlyList<ConstellationConnection> Connections => _connections;

        public bool IsDroppedOnBoard { get; private set; }
        public int RequiredConstellationsToEnable => _requiredConstellationsToEnable;

        private void OnEnable()
        {
            ResetPartsState();
        }

        private void Update()
        {
            if (_parts.All(x => x.IsValid) && IsDragged.Value)
            {
                if (!_isGlowing)
                {
                    _isGlowing = true;
                    _glowStartTime = Time.time;
                }

                var value = ((Time.time - _glowStartTime) * _glowSpeed) % _glowDuration;
                var lerped = _glowCurve.Evaluate(value);
                _glow.color = _glow.color.SetAlpha(lerped);
            }
            else
            {
                if (_isGlowing)
                {
                    _isGlowing = false;

                    _glow.DOFade(0, .1f);
                }
            }
        }

        public void SetWorldPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void ResetRotation()
        {
            transform.eulerAngles = Vector3.zero;
        }

        public void MarkPossibleToPutOnBoard(bool isPossible)
        {
            IsPossibleToPutOnBoard = isPossible;
        }

        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }

        public void SetParentAndScale(Transform parent, Vector3 scale)
        {
            transform.SetParent(parent);
            transform.localScale = scale;
        }

        public void SetParent(Transform container)
        {
            transform.SetParent(container, false);
            transform.localPosition = Vector3.zero;
        }

        public void SetAsDroppedOnBoard()
        {
            IsDroppedOnBoard = true;

            var playingSource = AudioManager.instance.PlayAndGetSource("constellation_1");
            AudioManager.instance.PlayWithDelay("constellation_2", playingSource.clip.length / 3);
        }

        public void RefreshPartsState(List<ConstellationPart> validParts)
        {
            if (_prevValidParts != validParts.Count)
            {
                AudioManager.instance.Play("constellation_hover_over_star");
            }

            _prevValidParts = validParts.Count;

            foreach (var part in _parts)
            {
                part.SetValid(validParts.Contains(part));
            }
        }

        public void ResetPartsState()
        {
            foreach (var part in _parts)
            {
                part.SetValid(false);
            }
        }

        public void HandlePutOnMap()
        {
            var renderers = GetRenderers();
            foreach (var r in renderers)
            {
                if (r is SpriteRenderer sr)
                {
                    sr.DOFade(0, _fadeOutDuration);
                }
                else if (r is LineRenderer lr)
                {
                    lr.gameObject.SetActive(false);
                }
            }

            DOVirtual.DelayedCall(_fadeOutDuration / 2, () =>
            {
                foreach (var connection in _connections)
                {
                    var instance = Instantiate(_droppedConnectionLinePrefab);
                    instance.Show(connection.First.GetGridPosition(), connection.Second.GetGridPosition());
                }
            });

            DOVirtual.DelayedCall(_fadeOutDuration, () => gameObject.SetActive(false));
        }

        private Renderer[] GetRenderers()
        {
            return GetComponentsInChildren<Renderer>();
        }

#if UNITY_EDITOR
        [Button]
        public void Setup()
        {
            _parts.Clear();
            var parts = GetComponentsInChildren<ConstellationPart>();
            foreach (var part in parts)
            {
                _parts.Add(part);
            }

            _lines.ForEach(x => DestroyImmediate(x.gameObject));
            _lines.Clear();
            foreach (var connection in _connections)
            {
                var newLine = UnityEditor.PrefabUtility.InstantiatePrefab(_linePrefab, _partsRoot).GetComponent<LineRenderer>();
                newLine.SetPosition(0, connection.First.transform.localPosition);
                newLine.SetPosition(1, connection.Second.transform.localPosition);
                _lines.Add(newLine);
            }
        }
#endif

        private void OnDrawGizmos()
        {
            if (_connections == null) { return; }

            foreach (var connection in _connections)
            {
                if (connection.First == null || connection.Second == null)
                {
                    continue;
                }

                Gizmos.color = Color.green;
                Gizmos.DrawLine(connection.First.transform.position, connection.Second.transform.position);
            }
        }
    }

    [Serializable]
    public class ConstellationConnection
    {
        [SerializeField] private ConstellationPart _first;
        [SerializeField] private ConstellationPart _second;

        public ConstellationPart First => _first;
        public ConstellationPart Second => _second;
    }
}