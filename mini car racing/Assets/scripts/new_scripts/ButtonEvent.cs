using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEvent : MonoBehaviour
{
    private bool MoveLeft,MoveRight,MoveForward,Brake;
    private float accelerate,steering,brakes = 0f;
    CarMove StartCar;
    [SerializeField] GameObject Car;
    
    private void Start() {
        StartCar = Car.GetComponent<CarMove>();
        MoveForward = false;
        MoveLeft = false;
        MoveRight = false;
        Brake = false;
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
                accelerate = 1f;
            }
            if(MoveLeft){
                steering = -1f;
            }
            if(MoveRight){
                steering = 1f;
            }
            if(Brake){
                brakes = 1f;
            }
        }else
        {
            accelerate = 0f;
            steering =0f;
            brakes = 0f;
        }
        StartCar.CarStartMoving(accelerate,steering,brakes);
    }
}
