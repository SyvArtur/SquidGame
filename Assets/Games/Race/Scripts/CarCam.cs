using System;
using System.Xml;
using UnityEngine;

public class CarCam : MonoBehaviour
{
    /*Transform rootNode;
    Transform carCam;
    Transform car;
    Rigidbody carPhysics;

    [Tooltip("If _car speed is below this value, then the camera will default to looking forwards.")]
    public float rotationThreshold = 1f;

    [Tooltip("How closely the camera follows the _car's position. The lower the value, the more the camera will lag behind.")]
    public float cameraStickiness = 0.1f;

    [Tooltip("How closely the camera matches the _car's velocity vector. The lower the value, the smoother the camera rotations, but too much results in not being able to see where you're going.")]
    public float cameraRotationSpeed = 5.0f;
    private Vector3 startPositionCamera;
    void Awake()
    {
        //carCam = Camera.main.GetComponent<Transform>();

        rootNode = GetComponent<Transform>();
        car = rootNode.parent.GetComponent<Transform>();
        carPhysics = car.GetComponent<Rigidbody>();
        startPositionCamera = rootNode.position;
    }

    void Start()
    {
        rootNode.parent = null;
    }

    void FixedUpdate()
    {
        //Quaternion look;

        // Moves the camera to match the _car's position.
        Vector3 shiftPoint = new Vector3(0, 0, 0);

        *//*if (_car.eulerAngles.y < 180)
        {
            shiftPoint = new Vector3(7, 1.5f, 0);
        }*//*
        if (car.eulerAngles.y > 45 && car.eulerAngles.y < 135)
        {
            shiftPoint = new Vector3(7, 1.5f, 0);
        }

        if (car.eulerAngles.y > 135 && car.eulerAngles.y < 225)
        {
            shiftPoint = new Vector3(0, 1.5f, -7f);
        }

        if (car.eulerAngles.y > 225 && car.eulerAngles.y < 315)
        {
            shiftPoint = new Vector3(-7, 1.5f, 0);
        }

        if (car.eulerAngles.y > 315 || car.eulerAngles.y < 45)
        {
            shiftPoint = new Vector3(0, 1.5f, 7f);
        }


        rootNode.position = Vector3.Lerp(rootNode.position, (car.position + shiftPoint), cameraStickiness * Time.deltaTime);

        *//*if (SpeedCalculator.Speed < 25)
        {*//*
        rootNode.LookAt(car);
        *//*  } else
          {
              // If the _car isn't moving, default to looking forwards. Prevents camera from freaking out with a zero velocity getting put into a Quaternion.LookRotation
              if (carPhysics.velocity.magnitude < rotationThreshold)
                  look = Quaternion.LookRotation(_car.forward);
              else
                  look = Quaternion.LookRotation(carPhysics.velocity.normalized);

              // HeadRotate the camera towards the velocity vector.
              look = Quaternion.Slerp(rootNode.rotation, look, cameraRotationSpeed * Time.fixedDeltaTime);
              rootNode.rotation = look;
          }*//*

    }*/
}