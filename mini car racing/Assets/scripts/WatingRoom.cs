using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class WatingRoom : MonoBehaviourPunCallbacks
{
    private PhotonView MyPhotonView; 
    private int PlayerCount;
    private int RoomSize;
    [SerializeField] private int MinPlayerToStart;
    [SerializeField] private TMP_Text PlayerCountDisplay;
    [SerializeField] private TMP_Text TimerToStartDisplay;
    //bool values
    private bool ReadyToCountDown;
    private bool StartingGame;
    //countdown time
    private float TimerToStartGame;
    private float NotFullGameTimer;
    //CountDown Timer Reset
    [SerializeField] private float MaxWaitTime;

    private void Start() 
    {
        //initiallize variables
        MyPhotonView = GetComponent<PhotonView>();
        NotFullGameTimer = MaxWaitTime;
        TimerToStartGame = MaxWaitTime;

        PlayerCountUpdate();
    }
    void PlayerCountUpdate()
    {
        PlayerCount = PhotonNetwork.PlayerList.Length;
        RoomSize = PhotonNetwork.CurrentRoom.MaxPlayers;
        PlayerCountDisplay.text = "Player "+PlayerCount+":"+RoomSize +" Total";
        if(PlayerCount >= MinPlayerToStart)
        {
            ReadyToCountDown = true;
        }
        else
        {
            ReadyToCountDown = false;
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        PlayerCountUpdate();
        if(PhotonNetwork.IsMasterClient)
        {
            MyPhotonView.RPC("RPCSendTimer", RpcTarget.Others, TimerToStartGame);
        }
    }
    [PunRPC]
    private void RPCSendTimer(float TimeIn)
    {
        TimerToStartGame = TimeIn;
        if(TimeIn < NotFullGameTimer)
        {
            NotFullGameTimer = TimeIn;
        }
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        PlayerCountUpdate();
    }

    private void Update()
    {
        WatingForMorePlayers();    
    }

    void WatingForMorePlayers()
    {
        if(PlayerCount <= 1)
        {
            ResetTimer();
        }
        if(ReadyToCountDown)
        {
            NotFullGameTimer -= Time.deltaTime;
            if(NotFullGameTimer < 0f)
            {
                NotFullGameTimer = 0f;
            }
            TimerToStartGame = NotFullGameTimer;
        }

        string TempTimer = string.Format("{0:00}",TimerToStartGame);
        TimerToStartDisplay.text = "Wating "+TempTimer+"s";

        if(TimerToStartGame <= 0f)
        {
            if(StartingGame)
            {
                return;
            }
            StartGame();
        }
    }
    void ResetTimer()
    {
        TimerToStartGame = MaxWaitTime;
        NotFullGameTimer = MaxWaitTime;
    }

    public void StartGame()
    {
        StartingGame = true;
        if(!PhotonNetwork.IsMasterClient)
        {
            return;
        }
        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.LoadLevel("Track1");
    }
    public void CancelSearch()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("Lobby");
    }
}
