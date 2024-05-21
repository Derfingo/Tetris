using System;
using UnityEngine.UI;

namespace Assets.Scripts.Tetris
{
    public interface ISpawn
    {
        public event Action<Image> OnShowNextEvent;
    }
}