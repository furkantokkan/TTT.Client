using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    void Start()
    {
        RequestServerStatus();
    }

    private void RequestServerStatus()
    {
        var msg = new NetServerStatusRequest();
        NetworkClient.Instance.SendServer(msg);
    }
}
