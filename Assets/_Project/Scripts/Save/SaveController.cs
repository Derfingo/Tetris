using System;

namespace Assets.Scripts.Tetris
{
    public class SaveController : ISaveController
    {
        public event Action<bool> OnShowFPSCounterEvent;
        public event Action<bool> OnChangeEffectEvent;
        public event Action<bool> OnChangeMusicEvent;

        private readonly ISaveSystem _saveSystem;

        private SaveData _saveData;

        public SaveController(ISaveSystem saveSystem)
        {
            _saveSystem = saveSystem;

            _saveData = _saveSystem.Load();
        }

        public void SetMute()
        {
            OnChangeEffectEvent?.Invoke(_saveData.IsEffentMute);
            OnChangeMusicEvent?.Invoke(_saveData.IsMusicMute);
            OnShowFPSCounterEvent?.Invoke(_saveData.IsFPSCounterView);
        }

        public void SetEffectMute(bool isMute)
        {
            _saveData.IsEffentMute = isMute;
            _saveSystem.Save(_saveData);
            OnChangeEffectEvent?.Invoke(isMute);
        }

        public void SetMusicMute(bool isMute)
        {
            _saveData.IsMusicMute = isMute;
            _saveSystem.Save(_saveData);
            OnChangeMusicEvent.Invoke(isMute);
        }

        public void SetViewFPSCounter(bool isView)
        {
            _saveData.IsFPSCounterView = isView;
            _saveSystem.Save(_saveData);
            OnShowFPSCounterEvent?.Invoke(isView);
        }

        public void SaveScore(int topScore)
        {
            _saveData.TopScore = topScore;
            _saveSystem.Save(_saveData);
        }

        public SaveData GetData()
        {
            return _saveData;
        }
    }
}
