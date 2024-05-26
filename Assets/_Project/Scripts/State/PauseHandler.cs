using System.Collections.Generic;

namespace Assets.Scripts.Tetris
{
    public class PauseHandler : IPause
    {
        private readonly List<IPause> _handlers = new();

        public void Pause(bool isPaused)
        {
            foreach (var handler in _handlers)
            {
                handler.Pause(isPaused);
            }
        }

        public void Register(params IPause[] handlers)
        {
            foreach (var handler in handlers)
            {
                _handlers.Add(handler);
            }
        }

        public void Unregister(IPause handler)
        {
            _handlers.Remove(handler);
        }
    }
}
