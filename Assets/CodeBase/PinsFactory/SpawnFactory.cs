using System;
using System.Collections.Generic;
using CodeBase.SaveSystem;
using Mono.Cecil;
using UnityEngine;

namespace CodeBase.PinsFactory
{
    public class SpawnFactory : MonoBehaviour
    {
        public static bool HasInstance => instance != null;
        public static SpawnFactory Current;
        protected static SpawnFactory instance;
        
        [SerializeField] private Pin _pinPrefab;
        [SerializeField] private List<Pin> _pins;

        public static SpawnFactory Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindFirstObjectByType<SpawnFactory>();
                    if (instance == null)
                    {
                        GameObject obj = new GameObject();
                        obj.name = "SpawnFactory";
                        instance = obj.AddComponent<SpawnFactory>();
                    }
                }
                return instance;
            }

        }
        protected virtual void Awake() => InitializeSingleton();
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
        public void DestroyAllPins()
        {
            foreach (Pin pin in _pins)
            {
                Destroy(pin.gameObject);
            }
            _pins.Clear();
        }
        public void DestroyPin(Pin pin)
        {
            //Нецелесообразно использовать пулл объектов в данном случае
            _pins.Remove(pin);
            Destroy(pin.gameObject);
        }
        public void SpawnPin(PinData data)
        {
            Pin pin = Instantiate(_pinPrefab, Vector3.zero, Quaternion.identity);
            pin.SetUpNew(data);
            _pins.Add(pin);
        }
        public void SpawnPin(Vector2 spawnPosition)
        {
            Pin pin = Instantiate(_pinPrefab, spawnPosition, Quaternion.identity);
            pin.SetUpNew();
            _pins.Add(pin);
            SaveLoadSystem.Instance.Save();
        }
    }
}
