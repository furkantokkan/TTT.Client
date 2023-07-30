using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI player1Username;
    [SerializeField] private TextMeshProUGUI player1Score;
    [SerializeField] private TextMeshProUGUI player2Username;
    [SerializeField] private TextMeshProUGUI player2Score;

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        var game = GameManager.instance.activeGame;
        player1Username.text = "[X] " + game.players[0].Player;
        player2Username.text = "[O] " + game.players[1].Player;
    }
}
