using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginUI : MonoBehaviour
{
    private Button loginButton;
    private Button sendButton;

    private void Awake()
    {
        loginButton = transform.Find("Connect").GetComponent<Button>();
        loginButton.onClick.AddListener(Connect);

        sendButton = transform.Find("Send").GetComponent<Button>();
        sendButton.onClick.AddListener(Send);
    }
    private void Send()
    {
        NetworkClient.Instance.SendServer("Test Message!");
    }
    private void Connect()
    {
        NetworkClient.Instance.Connect();
        Debug.Log("Connecting...");
    }
}
