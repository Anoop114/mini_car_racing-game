using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class car_move : MonoBehaviour
{
    public WheelCollider[] Wcs;
    public GameObject[] Wheels;
    public float Torque = 200f;
    public float BrakeTorque = 500f;
    public float MaxSteerAngle = 30f;

    
    void Start()
    {
        GameEvents CarMove = GetComponent<GameEvents>();
        CarMove.onMoveCar += test;
    }
    private void test(float a,float b,float c)
    {
        Debug.Log(a+" "+b+" "+c);
    }
    private void Go(float acc, float steer, float brake)
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

    // Update is called once per frame
    void Update()
    {
        float a = Input.GetAxis("Vertical");
        float s = Input.GetAxis("Horizontal");
        float b = Input.GetAxis("Jump");
        Go(a,s,b);
    }
}
