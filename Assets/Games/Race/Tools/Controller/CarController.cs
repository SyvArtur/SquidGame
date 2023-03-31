using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
    public static CarInput carInput;
    // Start is called before the first frame update
    void Awake()
    {
        carInput = new CarInput();
    }

    private void OnEnable()
    {
        carInput.Enable();
    }

    private void OnDisable()
    {
        carInput.Disable();
    }
    // Update is called once per frame
    void Update()
    {
        //Debug.Log(carInput.Car.WASD.ReadValue<Vector2>().x + carInput.Car.WASD.ReadValue<Vector2>().y);
    }
}
