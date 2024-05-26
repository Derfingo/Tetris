using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Tetris
{
    public class FigureView : ViewBase, IShowView
    {
        [SerializeField] private Image _figureImage;

        public void Show(Image image)
        {
            _figureImage.sprite = image.sprite;
            _figureImage.color = image.color;
            _figureImage.SetNativeSize();
        }
    }
}
