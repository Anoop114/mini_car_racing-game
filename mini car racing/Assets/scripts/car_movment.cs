using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class car_movment : MonoBehaviour
{
    [SerializeField] PhotonView my_photon_view;
    Camera cam;
    camera_control cam1;
    public Rigidbody rb;
    public Transform car;
    public static float speed = 5f;
    
    public static int ongame;

    Vector3 forward = new Vector3(0, 0, 1);
    Vector3 backward = new Vector3(0, 0, -1);

    Vector3 rotationRight = new Vector3(0, 30, 0);
    Vector3 rotationLeft = new Vector3(0, -30, 0);
    private void Start() {
        my_photon_view = GetComponent<PhotonView>();
        cam = Camera.main;
        cam1 = cam.GetComponent<camera_control>();
        cam1.enabled = true;
    }
    void FixedUpdate()
    {
        ongame = Start_game.ongame;
        if(Stop_game.car_speed == 0f){
            speed = Stop_game.car_speed;
        }
        if(ongame == 1){
            transform.Translate(forward * speed * Time.deltaTime);
        }
        if(my_photon_view.IsMine){
            if (Input.GetKey("w")|| Input.GetKey(KeyCode.UpArrow)){
                speed = 10f;
            }else{
                speed = 5f;
            }
            if (Input.GetKey("d")|| Input.GetKey(KeyCode.RightArrow))
            {
                Quaternion deltaRotationRight = Quaternion.Euler(rotationRight * Time.deltaTime);
                rb.MoveRotation(rb.rotation * deltaRotationRight);
            }

            if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow))
            {
                Quaternion deltaRotationLeft = Quaternion.Euler(rotationLeft * Time.deltaTime);
                rb.MoveRotation(rb.rotation * deltaRotationLeft);
            }
        }
    }
}
