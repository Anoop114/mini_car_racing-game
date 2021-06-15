using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
public class Start_game : MonoBehaviourPunCallbacks
{
    public static int ongame;
    public GameObject start_button;
    public GameObject other_player_msg;
    public GameObject countdown_text;
    public Text countText;
    // public Text winText;
    public float currCountdownValue = 3f;
    // Start is called before the first frame update
    void Start()
    {
        start_button.SetActive(false);
        countdown_text.SetActive(false);
        other_player_msg.SetActive(false);
        countText.text = "";
        if(PhotonNetwork.IsConnected){
            if(PhotonNetwork.IsMasterClient){
                start_button.SetActive(true);
            }else{
                other_player_msg.SetActive(true);
            }
        }
    }
    public void BeginGame(){
        if(PhotonNetwork.IsMasterClient){
            photonView.RPC("start_gam",RpcTarget.All,null);
        }
    }
    [PunRPC]
    public void start_gam(){
        StartCoroutine(StartCountdown());
    }
    IEnumerator StartCountdown()
    {
        start_button.SetActive(false);
        other_player_msg.SetActive(false);
        countdown_text.SetActive(true);
        while (currCountdownValue >= 0)
        {
            countText.text = currCountdownValue.ToString();
            yield return new WaitForSeconds(1.0f);
            currCountdownValue--;
        }
        if(currCountdownValue == 0 || currCountdownValue < 0){
            countText.text = "Go..";
            yield return new WaitForSeconds(0.3f);
            countdown_text.SetActive(false);
            ongame = 1;
        }
    }
}
