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
    // Start is called before the first frame update
    void Awake()
    {
        CameraFollowScript = Camera.GetComponent<CameraFollowCar>();
        ButtonEventScript = ButtonManager.GetComponent<ButtonEvent>();
        if(PhotonNetwork.IsConnected)
        {
            if(NetworkedPlayer.LocalPlayer == null)
            {
                StartPos = SpawnPoints[PhotonNetwork.CurrentRoom.PlayerCount-1].position;
                StartRot = SpawnPoints[PhotonNetwork.CurrentRoom.PlayerCount-1].rotation;
                GameObject Car = PhotonNetwork.Instantiate(CarPrefab[PhotonNetwork.CurrentRoom.PlayerCount-1].name,StartPos,StartRot,0);
                CameraFollowScript.CarBody = Car; //setting car prefabs to other gameObjects so they easily find the car body.
                ButtonEventScript.Car = Car;
                
            }
        }
    }
}
