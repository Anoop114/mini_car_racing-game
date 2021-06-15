using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
public class Stop_game : MonoBehaviourPunCallbacks
{
    // public GameObject endpoint;
    [SerializeField]private GameObject win;
    [SerializeField]private GameObject lose;
    public static float car_speed;
    void Start()
    {
        car_speed = car_movment.speed;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {   
                car_speed = 0f;
                photonView.RPC("endgame_win",RpcTarget.All,null);
                // endgame_win();
            
        }
        else if(car_speed == 0f && !other.gameObject.CompareTag("Player")){
            // endgame_lose();
                photonView.RPC("endgame_lose",RpcTarget.All,null);
            
        }
    }[PunRPC]
    private void endgame_win(){
        win.SetActive(true);
    }
    private void endgame_lose(){
        lose.SetActive(true);
    }
}
