using System;
using CodeBase.Camera;
using UnityEngine;

namespace CodeBase.UI
{
    public class UIControl : MonoBehaviour
    {
        [SerializeField] private CameraSystem2D _cameraSystem;
        [SerializeField] private PinInfoPanel _pinInfoPanel;
        [SerializeField] private Panel _smallPinInfo;
        [SerializeField] private Panel _bigPinInfo;
        public static bool HasInstance => instance != null;
        public static UIControl Current;
        protected static UIControl instance;
    

        public static UIControl Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindFirstObjectByType<UIControl>();
                    if (instance == null)
                    {
                        GameObject obj = new GameObject();
                        obj.name = "UIControl";
                        instance = obj.AddComponent<UIControl>();
                    }
                }

                return instance;
            }

        }

        protected virtual void Awake() => InitializeSingleton();

        protected virtual void InitializeSingleton()
        {
            if (!Application.isPlaying)
            {
                return;
            }
        }

        private void Start()
        {
            HideAllPanels();
        }

        public void FocusCameraOnPoint(Vector2 position)
        {
            _cameraSystem.FocusCameraOnPoint(position);
            HideAllPanels();
            
        }

        public void HideAllPanels()
        {
            _smallPinInfo.Hide();
            _bigPinInfo.Hide();
        }

        public void ShowSmallPinInfo(Pin.Pin pin)
        {
            HideAllPanels();
            _pinInfoPanel.SetUp(pin);
            _smallPinInfo.Show();
            
        }
        public void ShowBigPinInfo(Pin.Pin pin)
        {
            HideAllPanels();
            _pinInfoPanel.SetUp(pin);
            _bigPinInfo.Show();
            
        }
        public void ShowBigPinInfo()
        {
            HideAllPanels();
            _bigPinInfo.Show();
            
        }

        public void BlockInput()
        {
            _cameraSystem.BlockInput();
        }

        public void UnblockInput()
        {
            _cameraSystem.UnblockInput();
        }
    }
}
