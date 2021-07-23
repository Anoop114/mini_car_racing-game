using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Photon.Realtime;
using Photon.Pun;

public class RaceMonitor : MonoBehaviour
{
    [SerializeField] private GameObject Camera;
    CameraFollowCar CameraFollowScript;
    [SerializeField] private GameObject ButtonManager;
    ButtonEvent ButtonEventScript;
    public GameObject[] CarPrefab;
    public Transform[] SpawnPoints;
    private Vector3 StartPos;
    private Quaternion StartRot;
    GameObject Car;
    // Start is called before the first frame update
    void Awake()
    {
        CameraFollowScript = Camera.GetComponent<CameraFollowCar>();
        ButtonEventScript = ButtonManager.GetComponent<ButtonEvent>();
        if (PhotonNetwork.IsConnected)
        {
            if (NetworkedPlayer.LocalPlayer == null)
            {
                
                if (PhotonNetwork.IsMasterClient)
                {
                    StartPos = SpawnPoints[0].position;
                    StartRot = SpawnPoints[0].rotation;
                    Car = PhotonNetwork.Instantiate(CarPrefab[0].name, StartPos, StartRot, 0);
                }
                else
                {
                    StartPos = SpawnPoints[PhotonNetwork.CurrentRoom.PlayerCount - 1].position;
                    StartRot = SpawnPoints[PhotonNetwork.CurrentRoom.PlayerCount - 1].rotation;
                    Car = PhotonNetwork.Instantiate(CarPrefab[PhotonNetwork.CurrentRoom.PlayerCount - 1].name, StartPos, StartRot, 0);
                }
                CameraFollowScript.CarBody = Car; //setting car prefabs to other gameObjects so they easily find the car body.
                ButtonEventScript.Car = Car;
            }
        }
    }
}
