﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;

public class LaunchManager : MonoBehaviourPunCallbacks
{
    private byte MaxPlayerInRoom = 2;
    private bool IsConnecting;
    private string GameVersion = "1";
    public TMP_Text FeedBack;
    public TMP_InputField PlayerName;

    void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        if (PlayerPrefs.HasKey("PlayerName"))
        {
            PlayerName.text = PlayerPrefs.GetString("PlayerName");
        }
    }
    public void SetName(string name)
    {
        PlayerPrefs.SetString("PlayerName", name);
    }

    public void ConnectNetwork()
    {
        FeedBack.text = "";
        IsConnecting = true;
        PhotonNetwork.NickName = PlayerName.text;
        if (PhotonNetwork.IsConnected)
        {
            FeedBack.text += "\n Joining Room...";
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            FeedBack.text += "\n Connecting....";
            PhotonNetwork.GameVersion = GameVersion;
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    //Network CallBacks
    public override void OnConnectedToMaster()
    {
        if (IsConnecting)
        {
            FeedBack.text = "\n OnConnected To Master....";
            PhotonNetwork.JoinRandomRoom();
        }
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        FeedBack.text += "\n Failde to Join room";
        CreateRoom();
    }
    void CreateRoom()
    {
        FeedBack.text += "\n Creating New Room";
        int RandomRoomNumber = Random.Range(0,1000);
        RoomOptions RoomOption = new RoomOptions() { IsVisible = true, IsOpen = true , MaxPlayers = this.MaxPlayerInRoom};
        PhotonNetwork.CreateRoom("Room"+RandomRoomNumber,RoomOption);
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        FeedBack.text += "\n Failed to create room... Trying again";
        CreateRoom();
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        FeedBack.text += "\n Disconnected Because" + cause;
        IsConnecting = false;
    }
    public override void OnJoinedRoom()
    {
        FeedBack.text += "\n Join room with" + PhotonNetwork.CurrentRoom.PlayerCount + "players";
        // PhotonNetwork.LoadLevel("Track1");
        PhotonNetwork.LoadLevel("WatingRoom");
    }
}
