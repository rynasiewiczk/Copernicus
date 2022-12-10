namespace _Project.Scripts.Effects
{
    using DG.Tweening;
    using UnityEngine;

    public class LineRendererConnection : MonoBehaviour
    {
        [SerializeField] private LineRenderer _line;
        [SerializeField] private float _showDuration;
        [SerializeField] private float _finishWidth = 1;

        public void Show(Vector2 point1, Vector2 point2)
        {
            var midpoint = Vector2.Lerp(point1, point2, .5f);

            DOVirtual.Float(0, 1, _showDuration, value =>
            {
                var progress1 = Vector2.Lerp(midpoint, point1, value);
                var progress2 = Vector2.Lerp(midpoint, point2, value);

                _line.SetPosition(0, progress1);
                _line.SetPosition(1, progress2);
            });
        }
    }
}