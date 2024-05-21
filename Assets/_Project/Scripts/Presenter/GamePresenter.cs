using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Tetris
{
    public class GamePresenter : MonoBehaviour
    {
        private IPauseView _pauseView;
        private IShow _showView;

        private ISpawn _spawn;
        private IPause _pause;

        public void Initialize(IPauseView pauseView, IPause pause, ISpawn spawn, IShow show)
        {
            _pauseView = pauseView;
            _showView = show;

            _pause = pause;
            _spawn = spawn;

            _pauseView.OnPauseClickEvent += _pause.Pause;
            _spawn.OnShowNextEvent += _showView.Show;
        }
    }
}
