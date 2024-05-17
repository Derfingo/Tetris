namespace Assets.Scripts.Tetris
{
    public interface IScore
    {
        public int Amount { get; }
        void Add(int count);
        void SetTop(int count);
    }
}