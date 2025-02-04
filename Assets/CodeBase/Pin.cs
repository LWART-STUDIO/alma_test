using System;
using CodeBase.SaveSystem.Extension;
using CodeBase.SaveSystem.Interfaces;
using UnityEngine;

namespace CodeBase
{
    public class Pin : MonoBehaviour, IBind<PinData>
    {
        [field: SerializeField] public SerializableGuid Id { get; set; } = SerializableGuid.NewGuid();
        [SerializeField] private string Name;
        [SerializeField] private string Description;
        [SerializeField] private Sprite Image;
        [SerializeField] private PinData _data;

        public void SetUpNew(PinData data = null)
        {
            if (data != null)
            {
                _data = data;
                Id = data.Id;
                transform.position = data.Position;
                Name = data.Name;
                Description = data.Description;
                Image = data.Image;
            }
            else
            {
                Id = SerializableGuid.NewGuid();
                _data.Id = Id;
                _data.Position = transform.position;
                _data.Name = Name;
                _data.Description = Description;
                _data.Image = Image;
            }
                
            
        }
        public void Bind(PinData data)
        {
            _data = data;
            _data.Id = Id;
            _data.Position = transform.position;
            _data.Name = Name;
            _data.Description = Description;
            _data.Image = Image;
        }

        private void Update()
        {
            _data.Position = transform.position;
        }
    }

    [Serializable]
    public class PinData : ISaveable
    {
        [field: SerializeField] public SerializableGuid Id { get; set; }
        public string Name;
        public Vector2 Position;
        public string Description;
        public Sprite Image;
    }
}