using CodeBase.PinsFactory;
using UnityEngine;
public class CameraSystem2D : MonoBehaviour
{
    //How fast the camera moves
    [SerializeField] private float _moveSpeed = 6f;

    //How fast the camera zooms
    [SerializeField] private float _zoomSpeed = 6f;

    //Constraints on how far or close the camera can zoom
    [SerializeField] private float _maxCamSize;
    [SerializeField] private float _minCamSize;
    

    private bool _dragPanMoveActive;
    private Vector2 _lastMousePosition;
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }
    private void Update()
    {
        HandleCameraMovementDragPan();
        HandlePinActions();
    }

    private void HandlePinActions()
    {
        if(Input.GetMouseButtonDown(0))
            SpawnPin();
    }

    private void SpawnPin()
    {
        // Store current mouse position in pixel coordinates.
        Vector3 mousePixelPos = Input.mousePosition;

        // Add depth so it can actually be used to cast a ray.
        Vector3 worldPoint = Input.mousePosition;
        worldPoint.z = Mathf.Abs(_camera.transform.position.z);
        //worldPoint.z = 11f;
        Vector3 mouseWorldPosition = _camera.ScreenToWorldPoint(worldPoint);
        mouseWorldPosition.z = 0f;
        SpawnFactory.Instance.SpawnPin(mouseWorldPosition);
        Debug.Log(mouseWorldPosition);
    }

    //Manages the camera drag input
    private void HandleCameraMovementDragPan()
    {
        Vector3 inputDir = new Vector3(0, 0, 0);

        //Drags from using right-click button
        if (Input.GetMouseButtonDown(1))
        {
            _dragPanMoveActive = true;
            _lastMousePosition = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(1))
        {
            _dragPanMoveActive = false;
        }

        if (_dragPanMoveActive)
        {
            Vector2 mouseMovementDelta = (Vector2)Input.mousePosition - _lastMousePosition;

            float dragPanSpeed = 1f;
            inputDir.x = mouseMovementDelta.x * dragPanSpeed;
            inputDir.y = mouseMovementDelta.y * dragPanSpeed;

            _lastMousePosition = Input.mousePosition;
        }

        Vector3 moveDir = -transform.up * inputDir.y + -transform.right * inputDir.x;

        transform.position += moveDir * _moveSpeed*_camera.orthographicSize * Time.deltaTime;
    }

    private void LateUpdate()
    {
        //Manages the zoom system
        if ((Input.GetAxis("Mouse ScrollWheel") != 0f) && (_camera.orthographicSize >= _minCamSize && _camera.orthographicSize <= _maxCamSize))
        {
            _camera.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * _zoomSpeed;
            _camera.orthographicSize=Mathf.Clamp(_camera.orthographicSize, _minCamSize, _maxCamSize);
        }
    }
}
