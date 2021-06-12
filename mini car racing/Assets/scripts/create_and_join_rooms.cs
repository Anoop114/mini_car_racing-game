using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class create_and_join_rooms : MonoBehaviourPunCallbacks
{
    public InputField create;
    public InputField join;
    public void create_room(){
        PhotonNetwork.CreateRoom(create.text);
    }
    public void join_room(){
        PhotonNetwork.JoinRoom(join.text);
    }
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Game");
    }
}
