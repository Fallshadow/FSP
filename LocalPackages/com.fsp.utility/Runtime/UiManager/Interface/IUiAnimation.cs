using System;

namespace fsp.ui
{
    public interface IUiAnimation
    {
        void Initialize(Action<object> completeCb);
        bool HasClip(UiAnimationClip clip, string suffix);
        void Play(UiAnimationClip clip, string suffix);
        void Stop();
    }
}