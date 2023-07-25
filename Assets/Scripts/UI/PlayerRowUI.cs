using System.Collections;
using System.Collections.Generic;
using TMPro;
using TTT.Server.NetworkShared.Packets.ServerClient;
using UnityEngine;

public class PlayerRowUI : MonoBehaviour
{
    [SerializeField] GameObject onlineImage, offlineImage;
    [SerializeField] TextMeshProUGUI username, score;
    public void Initialize(PlayerNetDto player)
    {
        if (player.IsOnlie)
        {
            onlineImage.gameObject.SetActive(true);
            offlineImage.gameObject.SetActive(false);
        }
        else
        {
            onlineImage.gameObject.SetActive(false);
            offlineImage.gameObject.SetActive(true);
        }

        username.text = player.Username;
        score.text = player.Score.ToString();
    }
}
