using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Assets.Scripts.Tetris
{
    public class BinarySaveSystem : ISaveSystem
    {
        private readonly string _path = Application.persistentDataPath + "/SettingsData.dat";

        public SaveData Load()
        {
            SaveData data;

            if(File.Exists(_path))
            {
                using FileStream file = File.Open(_path, FileMode.Open);
                {
                    object loadedData = new BinaryFormatter().Deserialize(file);
                    data = (SaveData)loadedData;
                }

                return data;
            }

            data = new SaveData();
            return data;
        }

        public void Save(SaveData data)
        {
            using FileStream file = File.Create(_path);
            new BinaryFormatter().Serialize(file, data);
        }
    }
}
