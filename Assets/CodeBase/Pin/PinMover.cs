using CodeBase.UI;
using UnityEngine;

namespace CodeBase.Pin
{
    public class PinMover : MonoBehaviour
    {
        [SerializeField] private Pin _pin;
        [SerializeField]private bool _isDragging;
        private UnityEngine.Camera _camera;
        private float _maxDragTime=0.5f;
        [SerializeField]private float _dragTime;
        private float _dragThreshold=5f;

        private void Awake()
        {
            _camera = UnityEngine.Camera.main;
        }

        public void OnMouseDown()  
        {
        
            _isDragging = true;
            _dragTime = 0;
            _pin.PinAnimator.DoScaleUp();
        }
        public void OnMouseUp()  
        {
            if (_dragTime < 0.1f)
            {
                UIControl.Instance.FocusCameraOnPoint(transform.position);
                UIControl.Instance.ShowSmallPinInfo(_pin);
            }
            UIControl.Instance.UnblockInput();
            _isDragging = false;
            _dragTime = 0;
            _pin.PinAnimator.DoScaleDown();
        }
        public void OnMouseDrag()   
        {
            UIControl.Instance.HideAllPanels();
            _dragTime+=Time.deltaTime;
        
            if(_dragTime<_maxDragTime||!_isDragging)
                return;
            Vector2 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition) - (transform.position+Vector3.down*0.35f);
        
            if (mousePosition.sqrMagnitude > _dragThreshold)
            {
                _isDragging=false;
                return;
            }
            UIControl.Instance.BlockInput();
            _pin.PinAnimator.DoScaleUpAndDown();
            transform.Translate(mousePosition);  
            _pin.UpdatePosition();
        }
    
    }
}
