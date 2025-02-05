using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Pin;
using CodeBase.PinsFactory;
using CodeBase.SaveSystem.Interfaces;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.SaveSystem
{
    public class SaveLoadSystem:MonoBehaviour
    {
        
        public Data MapData;

        private IDataService _dataService;

        private void OnEnable() => SceneManager.sceneLoaded += OnSceneLoaded;
        private void OnDisable() => SceneManager.sceneLoaded -= OnSceneLoaded;
        
        public static bool HasInstance => instance != null;
        public static SaveLoadSystem Current;
        protected static SaveLoadSystem instance;
        
        public static SaveLoadSystem Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindFirstObjectByType<SaveLoadSystem>();
                    if (instance == null)
                    {
                        GameObject obj = new GameObject();
                        obj.name = "SaveLoadSystem";
                        instance = obj.AddComponent<SaveLoadSystem>();
                    }
                }
                return instance;
            }

        }

        protected virtual void Awake()
        {
            InitializeSingleton();
            _dataService = new FileDataService(new JsonSerializer());
        } 
        protected virtual void InitializeSingleton() {
            if (!Application.isPlaying) {
                return;
            }
            

            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(transform.gameObject);
                enabled = true;
            } else {
                if (this != instance) {
                    Destroy(this.gameObject);
                }
            }
        }
        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            Load(MapData.Name);
        }

        private void Bind<T, TData>(TData data) where T : MonoBehaviour, IBind<TData> where TData : ISaveable, new()
        {
            var entity = FindFirstObjectByType<T>();
            if (entity != null)
            {
                if(data == null)
                    data = new TData{Id = entity.Id};
                entity.Bind(data);
            }
        }
        
        private void Bind<T, TData>(List<TData> datas) where T : MonoBehaviour, IBind<TData> where TData : ISaveable, new()
        {
            var entities = FindObjectsByType<T>(FindObjectsSortMode.None);
            foreach (var entity in entities)
            {
                var data = datas.FirstOrDefault(x => x.Id == entity.Id);
                if (data == null)
                {
                    data = new TData{Id = entity.Id};
                    datas.Add(data);
                }
                entity.Bind(data);
            
            }
        }

        public void NewLaunch()
        {
            MapData = new Data
            {
                Name = "Test",
                PinDatas = new List<PinData>()
                
            };
        }

        public void Save()
        {   
            Bind<Pin.Pin, PinData>(MapData.PinDatas);
            _dataService.Save(MapData);
        }

        public void Load(string saveName)
        {
            SpawnFactory.Instance.DestroyAllPins();
            MapData = _dataService.Load(saveName);

            foreach (var pinData in MapData.PinDatas)
            {
                SpawnFactory.Instance.SpawnPin(pinData);
            }
            
        }

        public void Delete(string saveName)
        {
            _dataService.Delete(saveName);
            SpawnFactory.Instance.DestroyAllPins();
            NewLaunch();
            
            
        }
    }
}