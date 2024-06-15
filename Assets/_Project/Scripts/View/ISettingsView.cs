using System;

namespace Assets.Scripts.Tetris
{
    public interface ISettingsView
    {
        public event Action<bool> OnShowFPSCounterEvent;
        public event Action<bool> OnChangeEffectEvent;
        public event Action<bool> OnChangeMusicEvent;

        void SetViewFPSCounter(bool isView);
        void SetMuteEffect(bool isMute);
        void SetMuteMusic(bool isMute);
    }
}