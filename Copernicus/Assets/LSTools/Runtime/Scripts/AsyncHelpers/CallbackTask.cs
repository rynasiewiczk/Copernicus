namespace LazySloth.Utilities
{
    using System.Threading;
    using System.Threading.Tasks;

    public class CallbackTask
    {
        private bool _waitForCallback = true;
        private CancellationToken _ct;

        public CallbackTask(CancellationToken ct)
        {
            _ct = ct;
        }

        public void Callback()
        {
            _waitForCallback = false;
        }

        public async Task WaitForCallback()
        {
            while (_waitForCallback && !_ct.IsCancellationRequested)
            {
                await Task.Yield();
            }
        }
    }
}