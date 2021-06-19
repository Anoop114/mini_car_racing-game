using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class camera_control : MonoBehaviourPun
{

    private Transform target;
    private Vector3 offset;
    void Start()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Car");
        foreach (GameObject play in players)
        {
            if (PhotonView.Get(play).IsMine)
            {
                this.target = play.transform;
                offset = transform.position - target.position;
                break;
            }
        }
    }
    void LateUpdate()
    {
        transform.position = target.position + offset;
    }
}
