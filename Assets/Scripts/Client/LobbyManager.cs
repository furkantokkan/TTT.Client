using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using TTT.PacketHandlers;
using TTT.Server.NetworkShared.Packets.ServerClient;
using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    [SerializeField] private Transform topPlayersContainer;
    [SerializeField] private TextMeshProUGUI playersOnlineLabel;
    [SerializeField] GameObject playerRowPrefab;
    void Start()
    {
        OnServerStatusRequestHandler.OnServerStatus += UpdateUI;
        RequestServerStatus();
    }
    private void OnDisable()
    {
        OnServerStatusRequestHandler.OnServerStatus -= UpdateUI;
    }
    private void UpdateUI(NetOnServerStatus msg)
    {
        while (topPlayersContainer.childCount > 0)
        {
            DestroyImmediate(topPlayersContainer.GetChild(0).gameObject);
        }

        playersOnlineLabel.text = $"{msg.PlayersCount} player online";

        for (int i = 0; i < msg.TopPlayers.Length; i++)
        {
            if (i < 10)
            {
                PlayerNetDto player = msg.TopPlayers[i];
                GameObject instance = Instantiate(playerRowPrefab, topPlayersContainer);
                instance.GetComponent<PlayerRowUI>().Initialize(player);
            }
        }
    }

    private void RequestServerStatus()
    {
        var msg = new NetServerStatusRequest();
        NetworkClient.Instance.SendServer(msg);
    }
}
