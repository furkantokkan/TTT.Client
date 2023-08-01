using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using TTT.Server.NetworkShared.Models;
using TTT.Server.NetworkShared.Packets.ServerClient;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] private GameObject turnObject;
    [SerializeField] private TextMeshProUGUI player1Username;
    [SerializeField] private TextMeshProUGUI player1Score;
    [SerializeField] private TextMeshProUGUI player2Username;
    [SerializeField] private TextMeshProUGUI player2Score;

    private void Start()
    {
        Initialize();

        OnMarkCellHandler.OnMarkCell += HandleMarkCell;
    }
    private void OnDestroy()
    {
        OnMarkCellHandler.OnMarkCell -= HandleMarkCell;
    }
    public void Initialize()
    {
        var game = GameManager.instance.ActiveGame;
        player1Username.text = "[X] " + game.players[0].Player;
        player2Username.text = "[O] " + game.players[1].Player;

    }
    private void HandleMarkCell(NetOnMarkCell msg)
    {
        if (msg.Outcome != MarkOutcome.None)
        {
            bool isDraw = msg.Outcome == MarkOutcome.Draw;

            return;
        }

        StartCoroutine(ShowTurnText());
    }

    private IEnumerator ShowTurnText()
    {
        turnObject.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.25f);
        turnObject.gameObject.SetActive(true);
    }
}
