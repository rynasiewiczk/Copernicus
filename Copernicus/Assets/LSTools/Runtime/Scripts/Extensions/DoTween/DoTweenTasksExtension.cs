namespace LazySloth.Utilities.DoTween
{
    using System.Threading;
    using System.Threading.Tasks;
    using DG.Tweening;

    public static class DoTweenTasksExtension
    {
        public static async Task GetTask(this Tween tween, CancellationToken ct)
        {
            while (tween.IsActive() && !ct.IsCancellationRequested)
            {
                await Task.Yield();
            }

            if (ct.IsCancellationRequested)
            {
                tween.Kill();
            }
        }
    }
}