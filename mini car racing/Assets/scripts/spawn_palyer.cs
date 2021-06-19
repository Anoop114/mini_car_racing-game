using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class spawn_palyer : MonoBehaviour
{
    public GameObject Player;
    private void Start() {
        // Vector2 playerpos = new Vector2(Random.Range(-4f,3f),0.8f);
        Vector3 playerpos = new Vector3(284.13f,19.11f,Random.Range(343.74f,336.83f));
        PhotonNetwork.Instantiate(Player.name,playerpos,Quaternion.Euler(0,80,0));    
    }
}
