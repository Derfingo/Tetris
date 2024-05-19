using UnityEngine;

namespace Assets.Scripts.Tetris
{
    public class GamePresenter : MonoBehaviour
    {
        private IPauseView _pauseView;
        private IPause _pause;

        public void Initialize(IPauseView pauseView, IPause pause)
        {
            _pauseView = pauseView;
            _pause = pause;

            _pauseView.OnPauseClickEvent += _pause.Pause;
        }
    }
}
