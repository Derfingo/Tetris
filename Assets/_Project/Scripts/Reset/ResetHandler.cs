using System.Collections.Generic;

namespace Assets.Scripts.Tetris
{
    public class ResetHandler : IReset
    {
        private readonly List<IReset> _handlers = new();

        public void Reset()
        {
            foreach (var handler in _handlers)
            {
                handler.Reset();
            }
        }

        public void Register(params IReset[] handlers)
        {
            foreach (var handler in handlers)
            {
                _handlers.Add(handler);
            }
        }

        public void Unregister(IReset handler)
        {
            _handlers.Remove(handler);
        }
    }
}
