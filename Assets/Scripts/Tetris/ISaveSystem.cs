namespace Assets.Scripts.Tetris
{
    public interface ISaveSystem
    {
        void Save(SaveData data);
        SaveData Load();
    }
}
