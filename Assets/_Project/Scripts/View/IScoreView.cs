namespace Assets.Scripts.Tetris
{
    public interface IScoreView
    {
        void SetCurrent(int count);
        void SetLines(int count);
        void SetLevel(int count);
        void SetTop(int count);
    }
}