using UnityEngine;

namespace CodeBase.UI
{
    public class PinInfoPanel:MonoBehaviour
    {
        [SerializeField] private SmallPinInfoPanel _smallPinInfoPanel;
        [SerializeField] private BigPinInfoPanel _bigPinInfoPanel;
        public void SetUp(Pin.Pin pin)
        {
            _bigPinInfoPanel.SetUp(pin);
            _smallPinInfoPanel.SetUp(pin);
        }
    }
}