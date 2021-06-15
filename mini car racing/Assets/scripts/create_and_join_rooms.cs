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
        // if (PhotonNetwork.playerList.Length == 2) {
        //     PhotonNetwork.LoadLevel("Game");

        // }
        // else if (PhotonNetwork.playerList.Length == 1) {
        //   Debug.Log ("Not Enough PLayers");
        // //   popup.SetActive (true);
        // }
        PhotonNetwork.LoadLevel("Game");
    }
}
