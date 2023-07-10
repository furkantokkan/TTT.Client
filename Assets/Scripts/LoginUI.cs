using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginUI : MonoBehaviour
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

    private string username = string.Empty;

    private string password = string.Empty;


    private void Start()
    {
        loginButton.onClick.AddListener(Login);

        usernameInputfield.onValueChanged.AddListener(UpdateUsername);

        passwordInputfield.onValueChanged.AddListener(UpdatePassword);
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
    }

    private void InteractLoginButton(bool interactable)
    {
        loginButton.interactable = interactable;
        Color color = loginButton.interactable ? Color.white : Color.grey;
        loginText.color = color;
    }

    private void Login()
    {
        Debug.Log("Logining in!");
    }
}
