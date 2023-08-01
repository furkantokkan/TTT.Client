using System;
using System.Collections;
using System.Collections.Generic;
using TTT.Server.NetworkShared.Models;
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

    public void UpdateUI(string actor)
    {
        var actorType = GameManager.instance.ActiveGame.GetPlayerType(actor);


        if (actorType == MarkType.X)
        {
            X.gameObject.SetActive(true);
            O.gameObject.SetActive(false);

            LeanTween.cancel(X.gameObject);

            LeanTween.scale(X.gameObject, new Vector3(1.0f, 1.0f, 1.0f), 0.5f).setEase(LeanTweenType.easeOutBounce);
        }
        else
        {
            LeanTween.cancel(O.gameObject);

            X.gameObject.SetActive(false);
            O.gameObject.SetActive(true);

            LeanTween.scale(O.gameObject, new Vector3(1.0f, 1.0f, 1.0f), 0.5f).setEase(LeanTweenType.easeOutBounce);
        }

        button.interactable = false;
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
