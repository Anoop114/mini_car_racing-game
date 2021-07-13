using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Realtime;
using Photon.Pun;
public class CarMove : MonoBehaviour
{
    public WheelCollider[] WheelColliders;
    public GameObject[] Wheels;
    private float Accelerate,Steering,Brakes = 0f;
    [SerializeField] private PhotonView MyPhotonView;
    GameObject Camera;
    GameObject ButtonManager;
    CarDetails VehicalDetails;
    private void Start()
    {
        VehicalDetails = GetComponent<CarDetails>();
        MyPhotonView = GetComponent<PhotonView>();
        Camera = VehicalDetails.Camera; 
        ButtonManager = VehicalDetails.ButtonManager;
        VehicalDetails.Torque = 200f;
        VehicalDetails.BrakeForce = 500f;
        VehicalDetails.SteeringAngle = 30f;
        if(!MyPhotonView.IsMine)
        {
            Destroy(Camera); //if camera is not mine then destroy it.
            Destroy(ButtonManager);//if button is not mine then destroy them.
        }
    }
    public void CarStartMoving()
    {

        Accelerate = VehicalDetails.Accelerate*VehicalDetails.Torque;
        Steering = VehicalDetails.Steering*VehicalDetails.SteeringAngle;
        Brakes = VehicalDetails.Brakes*VehicalDetails.BrakeForce;

        for(int Cartire=0; Cartire < WheelColliders.Length; Cartire++)
        {
            WheelColliders[Cartire].motorTorque = Accelerate;
            if(Cartire<2)
            {
                WheelColliders[Cartire].steerAngle = Steering;
            }else
            {
                WheelColliders[Cartire].brakeTorque = Brakes;
            }
            Quaternion quat;
            Vector3 pos;
            WheelColliders[Cartire].GetWorldPose(out pos,out quat);
            Wheels[Cartire].transform.position = pos;
            Wheels[Cartire].transform.rotation = quat;
        }
    }
}
