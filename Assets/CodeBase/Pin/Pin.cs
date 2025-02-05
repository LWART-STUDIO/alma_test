using System;
using CodeBase.SaveSystem;
using CodeBase.SaveSystem.Extension;
using CodeBase.SaveSystem.Interfaces;
using UnityEngine;

namespace CodeBase.Pin
{
    public class Pin : MonoBehaviour, IBind<PinData>
    {
        public PinAnimator PinAnimator => _pinAnimator;
        [field: SerializeField] public SerializableGuid Id { get; set; } = SerializableGuid.NewGuid();
        [SerializeField] private string _name;
        [SerializeField] private string _description;
        [SerializeField] private string _imagePath;
        [SerializeField] private PinData _data;
        [SerializeField] private PinAnimator _pinAnimator;
        
        

        public void SetUpNew(PinData data = null)
        {
            if (data != null)
            {
                _data = data;
                Id = data.Id;
                transform.position = data.Position;
                _name = data.Name;
                _description = data.Description;
                _imagePath = data.ImagePath;
            }
            else
            {
                Id = SerializableGuid.NewGuid();
                _data.Id = Id;
                _data.Position = transform.position;
                _data.Name = _name;
                _data.Description = _description;
                _data.ImagePath = _imagePath;
            }
            
        }

        public string GetImage()
        {
            return _imagePath;
        }
        public void SetImage(string path)
        {
            _imagePath = path;
            _data.ImagePath = path;
            SaveLoadSystem.Instance.Save();
        }
        public void SetTitle(string text)
        {
            _name = text;
            _data.Name = _name;
            SaveLoadSystem.Instance.Save();
        }
        public void SetDescription(string text)
        {
            _description = text;
            _data.Description = _description;
            SaveLoadSystem.Instance.Save();
        }
        public string GetTitle()
        {
            return _name;
        }
        public string GetDescription()
        {
            return _description;
        }
        public void UpdatePosition()
        {
            _data.Position = transform.position;
        }
        public void Bind(PinData data)
        {
            _data = data;
            _data.Id = Id;
            _data.Position = transform.position;
            _data.Name = _name;
            _data.Description = _description;
            _data.ImagePath = _imagePath;
        }

        public SerializableGuid GetId()
        {
            return Id;
        }


    }
    
    

    [Serializable]
    public class PinData : ISaveable
    {
        [field: SerializeField] public SerializableGuid Id { get; set; }
        public string Name;
        public Vector2 Position;
        public string Description;
        public string ImagePath;
    }
}