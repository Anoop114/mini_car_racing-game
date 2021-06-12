using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class camera_control : MonoBehaviourPun
{
    // public GameObject player;
    private Transform target;
    private Vector3 offset;
    void Start()
    {
        // offset = transform.position - player.transform.position;
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            if (PhotonView.Get(player).IsMine)
            {
                this.target = player.transform;
                offset = transform.position - player.transform.position;
                break;
            }
        }
    }
    void LateUpdate()
    {
        // transform.position = player.transform.position + offset;
        transform.position = target.position + offset;
    }
}
