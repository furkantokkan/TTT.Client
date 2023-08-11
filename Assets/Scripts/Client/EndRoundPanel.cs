using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using TTT.Server.NetworkShared.Packets.ClientServer;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndRoundPanel : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] Color winColor;
    [SerializeField] Color looseColor;
    [SerializeField] Color drawColor;
    [SerializeField] string winTextValue = "YOU WIN!";
    [SerializeField] string looseTextValue = "YOU LOST!";
    [SerializeField] string drawTextValue = "DRAW";
    [Header("Referances")]
    [SerializeField] private Transform root;
    [SerializeField] private Image header;
    [SerializeField] private Button playAgainButton;
    [SerializeField] private Button acceptButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private TextMeshProUGUI opponentLeftText;
    [SerializeField] private TextMeshProUGUI waitingForOpponentText;
    [SerializeField] private TextMeshProUGUI playAgainText;
    [SerializeField] private TextMeshProUGUI winLooseText;


    private float button1MiddlePosY = 7f;
    private float button2MiddlePosY = -151f;

    private float button1BottomPosY = -77f;
    private float button2BottomPosY = -237f;
    private bool opponentLeft;

    private void OnEnable()
    {
        LeanTween.cancel(root.gameObject);
        root.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
        LeanTween.scale(root.gameObject, new Vector3(1.0f, 1.0f, 1.0f), 1f).setEase(LeanTweenType.easeOutBounce);

        playAgainButton.onClick.AddListener(RequestPlayAgain);
        acceptButton.onClick.AddListener(AcceptPlayAgain);
        quitButton.onClick.AddListener(QuitFromGame);

        OnPlayAgainHandler.OnPlayAgain += HandlePlayAgainRequest;
        OnNewRoundHandler.OnNewRound += ResetUI;
        OnQuitGameHandler.OnQuitGame += SetOpponentLeft;

        RectTransform rtAcceptButton = acceptButton.GetComponent<RectTransform>();
        rtAcceptButton.anchoredPosition = new Vector2(rtAcceptButton.anchoredPosition.x, button1MiddlePosY);

        RectTransform rtQuitButton = quitButton.GetComponent<RectTransform>();
        rtQuitButton.anchoredPosition = new Vector2(rtQuitButton.anchoredPosition.x, button2MiddlePosY);
    }

    private void OnDisable()
    {
        playAgainButton.onClick.RemoveAllListeners();
        quitButton.onClick.RemoveAllListeners();
        OnPlayAgainHandler.OnPlayAgain -= HandlePlayAgainRequest;
        OnNewRoundHandler.OnNewRound -= ResetUI;
        OnQuitGameHandler.OnQuitGame -= SetOpponentLeft;
    }

    public void Initialize(string winnerID, bool isDraw)
    {
        if (isDraw)
        {
            header.color = drawColor;
            winLooseText.text = drawTextValue;
            return;
        }

        bool isWin = NetworkClient.Instance.username == winnerID;

        if (isWin)
        {
            header.color = winColor;
            winLooseText.text = winTextValue;
        }
        else
        {
            header.color = looseColor;
            winLooseText.text = looseTextValue;
        }
    }
    public void SetOpponentLeft(NetOnQuitGame game)
    {
        opponentLeftText.gameObject.SetActive(true);
        playAgainButton.gameObject.SetActive(false);
        waitingForOpponentText.gameObject.SetActive(false);
        playAgainButton.gameObject.SetActive(false);
        acceptButton.gameObject.SetActive(false);
        playAgainText.gameObject.SetActive(false);

        RectTransform rtQuitButton = quitButton.GetComponent<RectTransform>();
        rtQuitButton.anchoredPosition = new Vector2(rtQuitButton.anchoredPosition.x, button1BottomPosY);

        opponentLeft = true;
    }
    private void QuitFromGame()
    {
        if (opponentLeft)
        {
            SceneManager.LoadScene("01_Lobby");
            return;
        }
        quitButton.interactable = false;
        var msg = new NetQuitGameRequest();
        NetworkClient.Instance.SendServer(msg);
    }
    private void ResetUI()
    {
        playAgainButton.gameObject.SetActive(true);
        waitingForOpponentText.gameObject.SetActive(false);
        acceptButton.interactable = true;
        acceptButton.gameObject.SetActive(false);
        playAgainText.gameObject.SetActive(false);

        RectTransform rtPlayAgain = playAgainButton.GetComponent<RectTransform>();
        rtPlayAgain.anchoredPosition = new Vector2(rtPlayAgain.anchoredPosition.x, button1MiddlePosY);

        RectTransform rtAcceptButton = acceptButton.GetComponent<RectTransform>();
        rtAcceptButton.anchoredPosition = new Vector2(rtAcceptButton.anchoredPosition.x, button1MiddlePosY);

        RectTransform rtQuitButton = quitButton.GetComponent<RectTransform>();
        rtQuitButton.anchoredPosition = new Vector2(rtQuitButton.anchoredPosition.x, button2MiddlePosY);

        gameObject.SetActive(false);
    }

    private void AcceptPlayAgain()
    {
        acceptButton.interactable = false;
        var msg = new NetAceptPlayAgainRequest();
        NetworkClient.Instance.SendServer(msg);
    }
    private void HandlePlayAgainRequest()
    {
        playAgainButton.gameObject.SetActive(false);
        acceptButton.gameObject.SetActive(true);
        playAgainText.gameObject.SetActive(true);

        RectTransform rtAcceptButton = acceptButton.GetComponent<RectTransform>();
        rtAcceptButton.anchoredPosition = new Vector2(rtAcceptButton.anchoredPosition.x, button1BottomPosY);

        RectTransform rtQuitButton = quitButton.GetComponent<RectTransform>();
        rtQuitButton.anchoredPosition = new Vector2(rtQuitButton.anchoredPosition.x, button2BottomPosY);
    }
    private void RequestPlayAgain()
    {
        playAgainButton.gameObject.SetActive(false);
        waitingForOpponentText.gameObject.SetActive(true);

        RectTransform rtQuitButton = quitButton.GetComponent<RectTransform>();
        rtQuitButton.anchoredPosition = new Vector2(rtQuitButton.anchoredPosition.x, button2MiddlePosY);


        var msg = new NetPlayAgainRequest();

        NetworkClient.Instance.SendServer(msg);
    }
}
