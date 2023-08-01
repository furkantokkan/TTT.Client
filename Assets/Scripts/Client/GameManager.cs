using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TTT.Server.NetworkShared.Models;
using UnityEngine;
using UnityEngine.Playables;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    private GameData activeGame;

    public GameData ActiveGame { get { return activeGame; } }

    public string XPlayerUsername { get; set; }
    public string OPlayerUsername { get; set; }

    public bool inputEnabled { get; set; }

    public MarkType GetClientType
    {
        get
        {
            if (XPlayerUsername == NetworkClient.Instance.username)
            {
                return MarkType.X;
            }
            else
            {
                return MarkType.O;
            }
        }
    }

    public bool IsClientTurn
    {
        get
        {
            if (activeGame == null)
            {
                return false;
            }

            if (activeGame.CurrentUser != NetworkClient.Instance.username)
            {
                return false;
            }

            return true;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegisterGame(Guid gameID, PlayerData[] players)
    {
        XPlayerUsername = players[0].Player;
        OPlayerUsername = players[1].Player;

        activeGame = new GameData()
        {
            ID = gameID,
            players = players,
            StartTime = DateTime.UtcNow,
            CurrentUser = XPlayerUsername
        };

        inputEnabled = true;
    }

    public class GameData
    {
        public Guid ID { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public PlayerData[] players { get; set; }

        public string CurrentUser { get; set; }

        public void SwitchCurrentPlayer()
        {
            CurrentUser = GetOpponent(CurrentUser);
        }

        public MarkType GetPlayerType(string userID)
        {
            if (userID == players[0].Player)
            {
                return MarkType.X;
            }
            else
            {
                return MarkType.O;
            }
        }

        private string GetOpponent(string currentUser)
        {
            if (currentUser == players[0].Player)
            {
                return players[1].Player;
            }
            else
            {
                return players[0].Player;
            }
        }
    }
}
