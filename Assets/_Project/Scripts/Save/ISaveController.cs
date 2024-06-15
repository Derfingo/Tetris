using System;

namespace Assets.Scripts.Tetris
{
    public interface ISaveController
    {
        public event Action<bool> OnShowFPSCounterEvent;
        public event Action<bool> OnChangeEffectEvent;
        public event Action<bool> OnChangeMusicEvent;

        void SaveScore(int score);
        SaveData GetData();

        void SetViewFPSCounter(bool isView);
        void SetEffectMute(bool isMute);
        void SetMusicMute(bool isMute);
        void SetMute();
    }
}