using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    #region Fields
    [SerializeField]
    Transform _followTarget;

    [SerializeField]
    float _rotationSpeed = 2f;

    [SerializeField]
    float _distance = 10;

    [SerializeField]
    float _minVerticalAngle = 0;
    [SerializeField]
    float _maxVerticalAngle = 60;

    [SerializeField]
    Vector2 _framingOffset;

    [SerializeField]
    bool _invertX;
    [SerializeField]
    bool _invertY;

    float _rotationX;
    float _rotationY;

    float _invertXVal;
    float _invertYVal;

    const string MOUSE_X = "Mouse X";
    const string MOUSE_Y = "Mouse Y";
    const string MOUSE_SCROLL_WHEEL = "Mouse ScrollWheel";

    [SerializeField]
    float _zoomSpeed = 2f;

    [SerializeField]
    float _minZoomDistance = 3f;

    [SerializeField]
    float _maxZoomDistance = 20f;

    [SerializeField]
    float zoomSmoothness = 5f; 
    #endregion Fields

    #region Methods
    public void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void Update()
    {
        CameraRotationControls();  
        CameraZoomControls();
    }

    private void CameraRotationControls() 
    {
        _invertXVal = (_invertX) ? -1 : 1;
        _invertYVal = (_invertY) ? -1 : 1;

        _rotationX += Input.GetAxis(MOUSE_Y) * _invertYVal * _rotationSpeed;
        _rotationX = Mathf.Clamp(_rotationX, _minVerticalAngle, _maxVerticalAngle);

        _rotationY += Input.GetAxis(MOUSE_X) * _invertXVal * _rotationSpeed;

        var targetRotation = Quaternion.Euler(_rotationX, _rotationY, 0);

        var focusPostion = _followTarget.position + new Vector3(_framingOffset.x, _framingOffset.y);

        transform.SetPositionAndRotation(focusPostion - targetRotation * new Vector3(0, 0, 5), targetRotation);
    }
    private void CameraZoomControls()
    {
        float zoomInput = Input.GetAxis("Mouse ScrollWheel");
        float zoomAmount = zoomInput * _zoomSpeed; // Calculate the zoom amount

        // Adjust the distance based on zoom amount
        _distance -= zoomAmount;

        // Clamp the distance within the defined limits
        _distance = Mathf.Clamp(_distance, _minZoomDistance, _maxZoomDistance);

        // Update the camera position based on the new distance from the target
        Vector3 cameraPosition = _followTarget.position - transform.forward * _distance;
        transform.position = cameraPosition;
    }
    #endregion Methods
}
