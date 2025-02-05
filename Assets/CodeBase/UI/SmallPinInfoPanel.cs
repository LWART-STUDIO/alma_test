using TMPro;
using UnityEngine;

namespace CodeBase.UI
{
    public class SmallPinInfoPanel:Panel
    {
        
        [SerializeField] private TMP_InputField _title;
        
        private Pin.Pin _savedPin;
        private void OnEnable()
        {
            _title.onEndEdit.AddListener(SaveTitle);
        }

        private void OnDisable()
        {
            _title.onEndEdit.RemoveListener(SaveTitle);
        }
        public void SetUp(Pin.Pin pin)
        {
            _savedPin = pin;
            _title.text = pin.GetTitle();
        }
  
        private void SaveTitle(string text)
        {
            _savedPin.SetTitle(text);
        }
    
    }
}