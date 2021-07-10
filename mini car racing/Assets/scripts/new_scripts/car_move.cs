using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Car_move : MonoBehaviour
{
    // [SerializeField] UnityEvent OnCompleteEvent;
    public WheelCollider[] Wcs;
    public GameObject[] Wheels;
    public float Torque = 200f;
    public float BrakeTorque = 500f;
    public float MaxSteerAngle = 30f;

    void OnEnable()
    {
        GameEvents.instance.OnMoveCar += Go;    
    }
    public void Go(float acc,float steer,float brake)
    {
        acc = Mathf.Clamp(acc,-1,1)*Torque;
        steer = Mathf.Clamp(steer,-1,1)*MaxSteerAngle;
        brake = Mathf.Clamp(brake,0,1)*BrakeTorque;

        for(int i=0; i < 4; i++)
        {
            Wcs[i].motorTorque = acc;
            if(i<2)
            {
                Wcs[i].steerAngle = steer;
            }else
            {
                Wcs[i].brakeTorque = brake;
            }
            Quaternion quat;
            Vector3 pos;
            Wcs[i].GetWorldPose(out pos,out quat);
            Wheels[i].transform.position = pos;
            Wheels[i].transform.rotation = quat;
        }
    }
}
