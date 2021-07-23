using System.Collections;
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

    public GameObject JoinRoomPanel;
    public GameObject WatingRoomPanel;

    private readonly string connectionStatusMessage = "    Connection Status: ";

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
            FeedBack.text = connectionStatusMessage + PhotonNetwork.NetworkClientState;
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            FeedBack.text = connectionStatusMessage + PhotonNetwork.NetworkClientState;
            PhotonNetwork.GameVersion = GameVersion;
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    //Network CallBacks
    public override void OnConnectedToMaster()
    {
        if (IsConnecting)
        {
            FeedBack.text = connectionStatusMessage + PhotonNetwork.NetworkClientState;
            PhotonNetwork.JoinRandomRoom();
        }
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        FeedBack.text = connectionStatusMessage + PhotonNetwork.NetworkClientState;
        CreateRoom();
    }
    void CreateRoom()
    {
        FeedBack.text = connectionStatusMessage + PhotonNetwork.NetworkClientState;
        int RandomRoomNumber = Random.Range(0, 1000);
        RoomOptions RoomOption = new RoomOptions()
        {
            IsVisible = true,
            IsOpen = true,
            MaxPlayers = this.MaxPlayerInRoom,
            PlayerTtl = 60000// 1 minuts.
        };
        PhotonNetwork.CreateRoom("Room" + RandomRoomNumber, RoomOption ,TypedLobby.Default);
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        FeedBack.text = connectionStatusMessage + PhotonNetwork.NetworkClientState;
        CreateRoom();
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        FeedBack.text = connectionStatusMessage + PhotonNetwork.NetworkClientState;
        IsConnecting = false;
    }
    public override void OnJoinedRoom()
    {
        FeedBack.text += "\n Join room with" + PhotonNetwork.CurrentRoom.PlayerCount + "players";
        FeedBack.text = connectionStatusMessage + PhotonNetwork.NetworkClientState;
        JoinRoomPanel.SetActive(false);
        WatingRoomPanel.SetActive(true);
    }
}
