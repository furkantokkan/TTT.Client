using System;
using System.Collections;
using System.Collections.Generic;
using TTT.Server.NetworkShared.Packets.ClientServer;
using UnityEngine;
using UnityEngine.UI;
public class CellUI : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private GameObject X;
    [SerializeField] private GameObject O;

    private byte index;
    private byte row;
    private byte col;
    internal void Initialize(byte index)
    {
        this.index = index;
        row = (byte)(index / 3);
        col = (byte)(index % 3);
        button.onClick.AddListener(CellClicked);
    }
    private void CellClicked()
    {
        if (!GameManager.instance.IsClientTurn || !GameManager.instance.inputEnabled)
        {
            return;
        }

        button.interactable = false;
        GameManager.instance.inputEnabled = false;

        var msg = new NetMarkCellRequest()
        {
            Index = this.index,
        };

        NetworkClient.Instance.SendServer(msg);

        Debug.Log("Sending mark request to server");
    }
}
