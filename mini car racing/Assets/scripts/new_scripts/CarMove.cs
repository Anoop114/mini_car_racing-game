using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMove : MonoBehaviour
{
    public WheelCollider[] Wcs;
    public GameObject[] Wheels;
    public float Torque = 200f;
    public float BrakeTorque = 500f;
    public float MaxSteerAngle = 30f;

    public void CarStartMoving(float accelerate,float steering,float brakes)
    {
        accelerate = accelerate*Torque;
        steering = steering*MaxSteerAngle;
        brakes = brakes*BrakeTorque;

        for(int i=0; i < 4; i++)
        {
            Wcs[i].motorTorque = accelerate;
            if(i<2)
            {
                Wcs[i].steerAngle = steering;
            }else
            {
                Wcs[i].brakeTorque = brakes;
            }
            Quaternion quat;
            Vector3 pos;
            Wcs[i].GetWorldPose(out pos,out quat);
            Wheels[i].transform.position = pos;
            Wheels[i].transform.rotation = quat;
        }
    }
}
