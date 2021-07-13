using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEvent : MonoBehaviour
{
    private bool MoveLeft,MoveRight,MoveForward,Brake;
    private float Accelerate,Steering,Brakes = 0f;
    CarMove StartCar;
    CarDetails CarDetail;
    public GameObject Car;

    
    private void Start() {
        StartCoroutine("CarGameObject");
        StartCar = Car.GetComponent<CarMove>();
        CarDetail = Car.GetComponent<CarDetails>();
        CarDetail.ButtonManager = gameObject;
        MoveForward = false;
        MoveLeft = false;
        MoveRight = false;
        Brake = false;
    }
    IEnumerator CarGameObject()
    {
        yield return new WaitForSeconds(0.1f);
    }
    public void MoveForwardActive()
    {
        MoveForward = true;
        CarMoveActive();
    }
    public void MoveLeftActive()
    {
        MoveLeft = true;
        CarMoveActive();
    }
    public void MoveRightActive()
    {
        MoveRight = true;
        CarMoveActive();
    }
    public void BrakeActive()
    {
        Brake = true;
        CarMoveActive();
    }
    public void MoveForwardDeactive()
    {
        MoveForward = false;
        CarMoveActive();
    }
    public void MoveLeftDeactive()
    {
        MoveLeft = false;
        CarMoveActive();
    }
    public void MoveRightDeactive()
    {
        MoveRight = false;
        CarMoveActive();
    }
    public void BrakeDeactive()
    {
        Brake = false;
        CarMoveActive();
    }
    private void CarMoveActive(){
        if(MoveForward || MoveLeft || MoveRight || Brake){
            if(MoveForward){
                Accelerate = 1f;
            }
            if(MoveLeft){
                Steering = -1f;
            }
            if(MoveRight){
                Steering = 1f;
            }
            if(Brake){
                Brakes = 1f;
            }
        }else
        {
            Accelerate = 0f;
            Steering =0f;
            Brakes = 0f;
        }
        CarDetail.Accelerate = Accelerate;
        CarDetail.Steering = Steering;
        CarDetail.Brakes = Brakes;
        StartCar.CarStartMoving();
    }
}
