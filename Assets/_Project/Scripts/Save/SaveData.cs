using System;

namespace Assets.Scripts.Tetris
{
    [Serializable]
    public struct SaveData
    {
        public bool IsFPSCounterView;
        public bool IsEffentMute;
        public bool IsMusicMute;

        public int TopScore;
        public int Lines;
        public int Level;
    }
}