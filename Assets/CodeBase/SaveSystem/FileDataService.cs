using System;
using System.Collections.Generic;
using System.IO;
using CodeBase.SaveSystem.Interfaces;
using UnityEngine.Device;

namespace CodeBase.SaveSystem
{
    public class FileDataService:IDataService
    {
        private ISerializer _serializer;
        private string _dataPath;
        private string _fileExtension;

        public FileDataService(ISerializer serializer)
        {
            _dataPath = Application.persistentDataPath;
            _fileExtension = "json";
            _serializer = serializer;
        }
        
        public void Save(Data data, bool overwrite = true)
        {
            string filePath = GetPathToFile(data.Name);

            if (!overwrite && File.Exists(filePath))
            {
                throw new IOException($"File {data.Name}.{_fileExtension} already exists and cannot be overwritten.");
            }
            
            File.WriteAllText(filePath,_serializer.Serialize(data));
        }

        public Data Load(string name)
        {
            string filePath = GetPathToFile(name);
            
            if (!File.Exists(filePath))
            {
                throw new ArgumentException($"No save file found with name {name}.");
            }
            
            return _serializer.Deserialize<Data>(File.ReadAllText(filePath));
        }

        public void Delete(string name)
        {
            string filePath = GetPathToFile(name);
            
            if(File.Exists(filePath))
                File.Delete(filePath);
        }

        public void DeleteAll()
        {
            foreach(string filePath in Directory.GetFiles(_dataPath))
                File.Delete(filePath);
        }

        public IEnumerable<string> ListSaves()
        {
            foreach (string filePath in Directory.GetFiles(_dataPath))
            {
                if (Path.GetExtension(filePath) == _fileExtension)
                    yield return Path.GetFileNameWithoutExtension(filePath);
            }
        }

        private string GetPathToFile(string fileName)
        {
            return Path.Combine(_dataPath,string.Concat(fileName,".", _fileExtension));
        }
    }
}