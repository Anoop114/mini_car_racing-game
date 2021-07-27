using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class FeedBack : MonoBehaviour
{
    private readonly string connectionStatusMessage = "    Connection Status: ";

    [Header("UI References")]
    public TMP_Text ConnectionStatusText;

    #region UI_DISPLAY

    public void FeedBackText()
    {
        ConnectionStatusText.text = connectionStatusMessage + PhotonNetwork.NetworkClientState;
    }

    #endregion
}
