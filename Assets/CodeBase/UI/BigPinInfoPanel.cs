using System;
using CodeBase.SaveSystem;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace CodeBase.UI
{
    public class BigPinInfoPanel:Panel
    {
        [SerializeField] private TMP_InputField _title;
        [SerializeField] private TMP_InputField _description;
        
         private Pin.Pin _savedPin;

         private void OnEnable()
         {
             _title.onEndEdit.AddListener(SaveTitle);
             _description.onEndEdit.AddListener(SaveDescription);
         }

         private void OnDisable()
         {
             _title.onEndEdit.RemoveListener(SaveTitle);
             _description.onEndEdit.RemoveListener(SaveDescription);
         }

         public void SetUp(Pin.Pin pin)
        {
            _savedPin = pin;
            _description.text = pin.GetDescription();
            _title.text = pin.GetTitle();
        }

        private void SaveDescription(string text)
        {
            _savedPin.SetDescription(text);
            
        }
        private void SaveTitle(string text)
        {
            _savedPin.SetTitle(text);
        }
    }
}