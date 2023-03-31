using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

class ThirdPersonCamera : MonoBehaviour, ICameraOperation
{
    private GameObject _car;
    private float _camRotationY;
    private float _camRotationZ;
    private StarterAssetsInputs _input;

    public void Initialize(GameObject car)
    {
        _car = car;
        _camRotationY = Camera.main.transform.parent.transform.rotation.eulerAngles.y;
        _camRotationZ = _car.transform.rotation.eulerAngles.x;
        _input = GetComponent<StarterAssetsInputs>();
        //car.AddComponent<Camera.main>();
        Camera.main.transform.position = new Vector3(Camera.main.transform.parent.position.x + 6, Camera.main.transform.parent.position.y + 3, Camera.main.transform.parent.position.z);
        Camera.main.transform.rotation = Quaternion.Euler(0, 0, 0);
        Camera.main.transform.parent.position = new Vector3(_car.transform.position.x, _car.transform.position.y, _car.transform.position.z);
        //Camera.main.transform.parent.rotation = Quaternion.Euler(0, 0, 0);

        

    }

    void FixedUpdate()
    {
        if (_car != null)
            CameraWork();
    }

    public void CameraWork()
    {
        // if there is an input and camera position is not fixed
        //Debug.Log(_input.look.sqrMagnitude);
        float threshold = 0.01f;
        Camera.main.transform.parent.position = new Vector3 (_car.transform.position.x+1, _car.transform.position.y, _car.transform.position.z);
        if (_input.look.sqrMagnitude >= threshold)
        {
            //Don't multiply mouse input by Time.deltaTime;
            //float deltaTimeMultiplier = IsCurrentDeviceMouse ? 1.0f : Time.deltaTime;
            float deltaTimeMultiplier = 5f;
            _camRotationY += _input.look.x * deltaTimeMultiplier;
            _camRotationZ += -_input.look.y * deltaTimeMultiplier;
        }

        float TopClamp = 155.0f; //How far in degrees can you move the camera up
        float BottomClamp = 70.0f; //How far in degrees can you move the camera down
        // clamp our rotations so our values are limited 360 degrees
        _camRotationZ = ClampAngle(_camRotationZ, BottomClamp, TopClamp);
        _camRotationY = ClampAngle(_camRotationY, float.MinValue, float.MaxValue);
        // Cinemachine will follow this target
        Camera.main.transform.parent.rotation = Quaternion.Euler(0, _camRotationY, _camRotationZ);

        Camera.main.transform.LookAt(Camera.main.transform.parent.transform);
    }

    private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }
}
