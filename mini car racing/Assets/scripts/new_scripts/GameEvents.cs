using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents instance;
    public event Action<float,float,float> OnMoveCar;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(this);
        }
    }
    // public void MoveCar()
    // {
    //     float a = Input.GetAxis("Vertical");
    //     float s = Input.GetAxis("Horizontal");
    //     float b = Input.GetAxis("Jump");
    //     OnMoveCar?.Invoke(a,s,b);
    // }
    void Update()
    {
        float a = Input.GetAxis("Vertical");
        float s = Input.GetAxis("Horizontal");
        float b = Input.GetAxis("Jump");
        OnMoveCar?.Invoke(a,s,b);
    }
}
