using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Assets.Scripts.Tetris
{
    public class BinarySaveSystem : ISaveSystem
    {
        private readonly string _savePath;

        public BinarySaveSystem()
        {
            _savePath = Application.persistentDataPath + "/Save.dat";
            Debug.Log(_savePath);
        }

        public SaveData Load()
        {
            var data = new SaveData();

            if(File.Exists(_savePath))
            {
                using FileStream file = File.Open(_savePath, FileMode.Open);
                {
                    object loadedData = new BinaryFormatter().Deserialize(file);
                    data = (SaveData)loadedData;
                }

                return data;
            }

            data.Score = 0;

            return data;
        }

        public void Save(SaveData data)
        {
            using FileStream file = File.Create(_savePath);
            new BinaryFormatter().Serialize(file, data);
        }
    }
}
