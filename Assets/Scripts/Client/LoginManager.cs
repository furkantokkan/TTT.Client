using Networkshared.Packets.ClientServer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using TTT.Server.NetworkShared.Packets.ServerClient;
using UnityEngine;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int maxUsernameLength = 10;
    [SerializeField] private int maxPasswordLength = 10;
    [Space]
    [Header("Referances")]
    [SerializeField] private Button loginButton;
    [SerializeField] private TextMeshProUGUI loginText;
    [SerializeField] private TMP_InputField usernameInputfield;
    [SerializeField] private TMP_InputField passwordInputfield;
    [SerializeField] private GameObject loginButtonError;
    [SerializeField] private GameObject usernameError;
    [SerializeField] private GameObject passwordError;
    [SerializeField] private GameObject loadingAnimation;

    private string username = string.Empty;

    private string password = string.Empty;

    private bool isConnected = false;

    private void Start()
    {
        loginButton.onClick.AddListener(Login);

        usernameInputfield.onValueChanged.AddListener(UpdateUsername);

        passwordInputfield.onValueChanged.AddListener(UpdatePassword);

        NetworkClient.Instance.onServerConnected += SetConnected;
        NetworkClient.Instance.onServerDisconnected += SetDisconnected;

        NetworkClient.Instance.onServerDisconnected += ResetButton;

        OnAuthFailHandler.OnAuthFail += ShowLoginError;
    }
    private void OnDisable()
    {
        NetworkClient.Instance.onServerConnected -= SetConnected;
        NetworkClient.Instance.onServerDisconnected -= SetDisconnected;
        NetworkClient.Instance.onServerDisconnected -= ResetButton;
        OnAuthFailHandler.OnAuthFail -= ShowLoginError;
    }
    public void QuitButton()
    {
        Application.Quit();
    }
    private void SetConnected()
    {
        isConnected = true;
    }

    private void SetDisconnected()
    {
        isConnected = false;
    }

    private void UpdateUsername(string value)
    {
        username = value;
        ValidateUserInput();
    }

    private void UpdatePassword(string value)
    {
        password = value;
        ValidateUserInput();
    }

    private void ValidateUserInput()
    {
        var usernameRegex = Regex.Match(username, "^[a-zA-Z0-9]+$");

        bool interactable = !string.IsNullOrWhiteSpace(username) &&
            !string.IsNullOrWhiteSpace(password) &&
            username.Length <= maxUsernameLength &&
            password.Length <= maxPasswordLength &&
            usernameRegex.Success;

        InteractLoginButton(interactable);

        if (password != null)
        {
            bool passwordtooLong = password.Length > maxPasswordLength;
            passwordError.gameObject.SetActive(passwordtooLong);
        }

        if (username != null)
        {
            bool userNameNotValid = username.Length > maxUsernameLength || !usernameRegex.Success;
            usernameError.gameObject.SetActive(userNameNotValid);
        }
    }

    private void InteractLoginButton(bool interactable)
    {
        loginButton.interactable = interactable;
        Color color = loginButton.interactable ? Color.white : Color.grey;
        loginText.color = color;
    }
    private void ResetButton()
    {
        StopCoroutine(LoginRoutine());
        InteractLoginButton(true);
        loadingAnimation.gameObject.SetActive(false);
    }
    private void Login()
    {
        StopCoroutine(LoginRoutine());
        StartCoroutine(LoginRoutine());
        Debug.Log("Logining in!");
    }
    private void ShowLoginError(NetOnAuthFail fail)
    {
        ResetButton();
        loginButtonError.gameObject.SetActive(true);
    }

    private IEnumerator LoginRoutine()
    {
        InteractLoginButton(false);
        loginButtonError.gameObject.SetActive(false);
        loadingAnimation.gameObject.SetActive(true);

        NetworkClient.Instance.Connect();

        while (!isConnected)
        {
            yield return null;
        }

        var authRequest = new NetAuthRequest()
        {
            Username = username,
            Password = password
        };

        NetworkClient.Instance.SendServer(authRequest);
    }
}
