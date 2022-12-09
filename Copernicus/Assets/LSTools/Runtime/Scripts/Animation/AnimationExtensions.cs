namespace LazySloth
{
    using System.Threading;
    using System.Threading.Tasks;
    using UnityEngine;

    public static class AnimationExtensions
    {
        public static void Play(this Animation animation, string animationName, bool instant)
        {
            if (instant)
            {
                var state = animation[animationName];
                state.clip.SampleAnimation(animation.gameObject, state.length);
            }
            else
            {
                animation.Play(animationName);
            }
        }
        
        public static async Task PlayAsync(this Animation animation, string animationName, CancellationToken ct, float speed = 1f)
        {
            var clip = animation[animationName];
            clip.speed = speed;
            clip.time = speed > 0f ? 0f : clip.length;
            
            animation.Play(animationName);
            while (animation != null && animation.isPlaying && !ct.IsCancellationRequested)
            {
                await Task.Yield();
            }

            if (ct.IsCancellationRequested)
            {
                animation.Stop(animationName);
            }
        }
        
        public static async Task PlayAsync(this Animation animation, CancellationToken ct) 
        {
            animation.Play();
            while (animation != null && animation.isPlaying && !ct.IsCancellationRequested)
            {
                await Task.Yield();
            }

            if (ct.IsCancellationRequested)
            {
                animation.Stop();
            }
        }
    }
}