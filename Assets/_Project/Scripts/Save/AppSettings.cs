using UnityEngine;

namespace Assets.Scripts.Tetris
{
    public class AppSettings
    {
        private readonly int _frameRate = 120;

        public AppSettings()
        {
            Application.targetFrameRate = _frameRate;
        }
    }
}
