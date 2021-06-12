using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class car_movment : MonoBehaviour
{
    // private Camera cam;
    [SerializeField] PhotonView my_photon_view;
    public Rigidbody rb;
    public Transform car;
    public float speed;

    Vector3 forward = new Vector3(0, 0, 1);
    Vector3 backward = new Vector3(0, 0, -1);

    Vector3 rotationRight = new Vector3(0, 30, 0);
    Vector3 rotationLeft = new Vector3(0, -30, 0);
    private void Start() {
        // cam = Camera.main;
        my_photon_view = GetComponent<PhotonView>();
        // if(!my_photon_view.IsMine){
        //     // Destroy(cam);
        //     cam.enabled = false;
        // }

    }
    private void Update() {
        // rb.MovePosition(car.position + forward * speed * Time.deltaTime);
        transform.Translate(forward * speed * Time.deltaTime);
    }
    void FixedUpdate()
    {
        if(my_photon_view.IsMine){
            // if (Input.GetKey("w"))                                         
            // {
            //     rb.MovePosition(car.position + forward * speed * Time.deltaTime);
            // }
            // if (Input.GetKey("s"))
            // {
            //     rb.MovePosition(car.position  + backward * speed * Time.deltaTime);
            // }

            if (Input.GetKey("d")|| Input.GetKey(KeyCode.LeftArrow))
            {
                Quaternion deltaRotationRight = Quaternion.Euler(rotationRight * Time.deltaTime);
                rb.MoveRotation(rb.rotation * deltaRotationRight);
            }

            if (Input.GetKey("a") || Input.GetKey(KeyCode.RightArrow))
            {
                Quaternion deltaRotationLeft = Quaternion.Euler(rotationLeft * Time.deltaTime);
                rb.MoveRotation(rb.rotation * deltaRotationLeft);
            }
        }
        

    }
}
