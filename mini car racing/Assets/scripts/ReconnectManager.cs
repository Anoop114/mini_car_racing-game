using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ReconnectManager : MonoBehaviourPunCallbacks
{
    // public override void OnDisconnected(DisconnectCause cause)
    // {
    //     StartCoroutine(MainReconnect());
    // }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        StartCoroutine(MainReconnect());
        Debug.Log(PhotonNetwork.NickName+" Player has left the room");   
    }
    private IEnumerator MainReconnect()
    {
        while (PhotonNetwork.NetworkingClient.LoadBalancingPeer.PeerState != ExitGames.Client.Photon.PeerStateValue.Disconnected)
        {
            Debug.Log("Waiting for client to be fully disconnected..", this);
 
            yield return new WaitForSeconds(30f);
        }
 
        Debug.Log("Client is disconnected!", this);
 
        if (!PhotonNetwork.ReconnectAndRejoin())
        {
            if (PhotonNetwork.Reconnect())
            {
                Debug.Log("Successful reconnected!", this);
            }
        }
        else
        {
            Debug.Log("Successful reconnected and joined!", this);
        }
    }
 
    
}
