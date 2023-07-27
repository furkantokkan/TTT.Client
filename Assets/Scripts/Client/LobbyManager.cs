using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using TTT.PacketHandlers;
using TTT.Server.NetworkShared.Packets.ClientServer;
using TTT.Server.NetworkShared.Packets.ServerClient;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{
    [SerializeField] private Transform topPlayersContainer;
    [SerializeField] private Transform loadingContainer;
    [SerializeField] private TextMeshProUGUI playersOnlineLabel;
    [SerializeField] private Button logoutButton;
    [SerializeField] private Button cancelButton;
    [SerializeField] private Button findOpponentButton;
    [SerializeField] GameObject playerRowPrefab;
    void Start()
    {
        logoutButton.onClick.AddListener(Logout);
        findOpponentButton.onClick.AddListener(FindOpponent);
        cancelButton.onClick.AddListener(CancelFindOpponent);

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
    private void FindOpponent()
    {
        findOpponentButton.gameObject.SetActive(false);
        loadingContainer.gameObject.SetActive(true);

        NetFindOpponentRequest request = new NetFindOpponentRequest();
        NetworkClient.Instance.SendServer(request);
    }
    private void CancelFindOpponent()
    {
        findOpponentButton.gameObject.SetActive(true);
        loadingContainer.gameObject.SetActive(false);

        NetCancelFindOpponentRequest request = new NetCancelFindOpponentRequest();
        NetworkClient.Instance.SendServer(request);
    }
    private void Logout()
    {
        NetworkClient.Instance.DisconnectFromServer();
        SceneManager.LoadScene("00_Login");
    }
    private void RequestServerStatus()
    {
        var msg = new NetServerStatusRequest();
        NetworkClient.Instance.SendServer(msg);
    }
}
