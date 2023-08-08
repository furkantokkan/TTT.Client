using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using TTT.Server.NetworkShared.Models;
using TTT.Server.NetworkShared.Packets.ServerClient;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] private GameObject endRoundPanel;
    [SerializeField] private GameObject turnObject;
    [SerializeField] private TextMeshProUGUI player1Username;
    [SerializeField] private TextMeshProUGUI player1Score;
    [SerializeField] private TextMeshProUGUI player2Username;
    [SerializeField] private TextMeshProUGUI player2Score;

    private int xScore = 0;
    private int oScore = 0;

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
            StartCoroutine(EndRoundRoutine(msg.Actor, isDraw));
            return;
        }

        StartCoroutine(ShowTurnText());
    }

    private IEnumerator EndRoundRoutine(string actor, bool isDraw)
    {
        float waitTime = isDraw ? 1.5f : 2;
        yield return new WaitForSeconds(waitTime);
        DisplayEndRoundUI(actor, isDraw);
    }

    private void DisplayEndRoundUI(string actor, bool isDraw)
    {
        endRoundPanel.SetActive(true);
        endRoundPanel.GetComponent<EndRoundPanel>().Initialize(actor, isDraw);
        if (isDraw)
        {
            return;
        }

        var playerType = GameManager.instance.ActiveGame.GetPlayerType(actor);

        if (playerType == MarkType.X)
        {
            xScore++;
            player1Score.text = xScore.ToString();
        }
        else
        {
            oScore++;
            player2Score.text = oScore.ToString();
        }
    }

    private IEnumerator ShowTurnText()
    {
        turnObject.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.25f);
        turnObject.gameObject.SetActive(true);
    }
}
