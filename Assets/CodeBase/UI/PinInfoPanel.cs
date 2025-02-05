using System.IO;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Extensions.Unity.ImageLoader;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
    public class PinInfoPanel:MonoBehaviour
    {
        [SerializeField] private SmallPinInfoPanel _smallPinInfoPanel;
        [SerializeField] private BigPinInfoPanel _bigPinInfoPanel;
        [SerializeField] private Image[] _images;
        [SerializeField] private Sprite _defaultImage;

        private Pin.Pin _savedPin;
        public void SetUp(Pin.Pin pin)
        {
            _savedPin = pin;
            _bigPinInfoPanel.SetUp(pin);
            _smallPinInfoPanel.SetUp(pin);
            if(!string.IsNullOrEmpty(pin.GetImage()))
                SetImages();
            else
            {
                foreach (var image in _images)
                {
                    image.sprite = _defaultImage;
                    image.color = new Color(1, 1, 1, 0.8f);
                }
            }
        }

        public async void SetImages()
        {
            var sprite = await UpdateImage();
            if (sprite == null)
            {
                Debug.LogWarning($"Could not load image ");
                return;
            }
            foreach (var image in _images)
            {
                image.sprite = sprite;
                image.color = Color.white;
            }
        }

        public async void SetNewImage()
        {
            ImportImage();
            var sprite = await UpdateImage();
            if (sprite == null)
            {
                Debug.LogWarning($"Could not load image ");
                return;
            }
            foreach (var image in _images)
            {
                image.sprite = sprite;
                image.color = Color.white;
            }
        }
        private async Task<Sprite> UpdateImage()
        {
            string path = _savedPin.GetImage();
            if(string.IsNullOrEmpty(path))
                path = ImportImage();
            if (string.IsNullOrEmpty(path))
            {
                Debug.Log("Failed Load Image");
                return null;
            }

            return await ImageLoader.LoadSprite(path);

        }

        public string ImportImage()
        {
            string newPath = null;
            Debug.Log("Import image");
#if UNITY_ANDROID
            // Use MIMEs on Android
            string[] fileTypes = new string[] { "image/*"};
#elif UNITY_IOS
            // Use UTIs on iOS
            string[] fileTypes = new string[] { "public.image" };
#else
            // Use MIMEs on Editor
            string[] fileTypes = new string[] { "image/*"};
#endif

            // Pick image(s) and/or video(s)
            NativeFilePicker.Permission permission = NativeFilePicker.PickFile((path) =>
            {
                if (path == null)
                    Debug.Log("Operation cancelled");
                else
                {
                    
                    Debug.Log("Picked file: " + path);
                    FileInfo f = new FileInfo(path);
                    DirectoryInfo d = f.Directory;
                    _savedPin.SetImage(path);
                    newPath = path;
                }
            }, fileTypes);

            Debug.Log("Permission result: " + permission);
            return newPath;
        }
    }
}