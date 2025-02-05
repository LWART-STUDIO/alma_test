using System;
using System.Collections.Generic;
using CodeBase.PinsFactory;
using CodeBase.UI;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CodeBase.Camera
{
    public class CameraSystem2D : MonoBehaviour
    {
        //How fast the camera moves
        [SerializeField] private float _moveSpeed = 6f;

        //How fast the camera zooms
        [SerializeField] private float _zoomSpeed = 6f;

        //Constraints on how far or close the camera can zoom
        [SerializeField] private float _maxCamSize;
        [SerializeField] private float _minCamSize;
        [SerializeField] private LayerMask _pinLayerMask;
        [SerializeField] private LayerMask _uiLayerMask;
        
    
    
        [SerializeField]private bool _dragPanMoveActive;
        private Vector3 _lastMousePosition;
        private Vector3 _lastCamPosition;
        private UnityEngine.Camera _camera;
        private bool _blockInput = false;
        


        private void Awake()
        {
            _camera = UnityEngine.Camera.main;
        }
        private void Update()
        {
            
        }

        public void BlockInput()
        {
            _blockInput = true;
        }

        public void UnblockInput()
        {
            _blockInput = false;
        }

        private void HandlePinActions()
        { 
      
            Vector2 position = _camera.ScreenToWorldPoint(Input.mousePosition);
            Collider2D detectedCollider = 
                Physics2D.OverlapPoint(position, _pinLayerMask);
     
            if(Input.GetMouseButtonUp(0)&&detectedCollider ==null&& _lastCamPosition == transform.position)
            {
                SpawnPin();
            }
        }


        public void FocusCameraOnPoint(Vector2 position)
        {
            transform.DOMove(position+Vector2.up*1.8f, _moveSpeed/2);
            _camera.DOOrthoSize(5f, _moveSpeed/2);
        }
        private void SpawnPin()
        {
            
            // Add depth so it can actually be used to cast a ray.
            Vector3 worldPoint = Input.mousePosition;
            worldPoint.z = Mathf.Abs(_camera.transform.position.z);
            //worldPoint.z = 11f;
            Vector3 mouseWorldPosition = _camera.ScreenToWorldPoint(worldPoint);
            mouseWorldPosition.z = 0f;
            SpawnFactory.Instance.SpawnPin(mouseWorldPosition);
            FocusCameraOnPoint(mouseWorldPosition);
        }
        

        private void Drag()
        {
            if (!_dragPanMoveActive)
            {
                if (Input.GetMouseButton(0))
                {
                    _lastCamPosition = transform.position;
                    _lastMousePosition = _camera.ScreenToViewportPoint(Input.mousePosition);
                    _dragPanMoveActive = true;
                }
            }
            else
            {
                if (Input.GetMouseButton(0))
                {
                    UIControl.Instance.HideAllPanels();
                    var mouse = _camera.ScreenToViewportPoint(Input.mousePosition);
                    var delta = mouse - _lastMousePosition;
                    delta.x *= _camera.aspect * _camera.orthographicSize / 0.5f;
                    delta.y *= _camera.orthographicSize / 0.5f;
                    transform.position = _lastCamPosition - delta;
                }
                else
                {
                    _dragPanMoveActive = false;
                }
            }
        }
        private bool IsPointerOverUIElement()
        {
            return IsPointerOverUIElement(GetEventSystemRaycastResults());
        }
        private bool IsPointerOverUIElement(List<RaycastResult> eventSystemRaysastResults)
        {
            for (int index = 0; index < eventSystemRaysastResults.Count; index++)
            {
                RaycastResult curRaysastResult = eventSystemRaysastResults[index];
                if (curRaysastResult.gameObject.layer == LayerMask.NameToLayer("UI"))
                    return true;


            }
            return false;
        }
        private List<RaycastResult> GetEventSystemRaycastResults()
        {
            PointerEventData eventData = new PointerEventData(EventSystem.current);
            eventData.position = Input.mousePosition;
            List<RaycastResult> raysastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, raysastResults);
            return raysastResults;
        }
        private void LateUpdate()
        {
            if(_blockInput)
                return;
            if(IsPointerOverUIElement())
                return;
            Drag();
            if(_dragPanMoveActive)
                return;
            HandlePinActions();
            //Manages the zoom system
            if ((Input.GetAxis("Mouse ScrollWheel") != 0f) && (_camera.orthographicSize >= _minCamSize && _camera.orthographicSize <= _maxCamSize))
            {
                _camera.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * _zoomSpeed;
                _camera.orthographicSize=Mathf.Clamp(_camera.orthographicSize, _minCamSize, _maxCamSize);
            }
        }
    }
}
