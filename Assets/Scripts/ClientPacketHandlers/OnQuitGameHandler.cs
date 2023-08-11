using NetworkShared;
using System;
using System.Collections;
using System.Collections.Generic;
using TTT.Server.NetworkShared;
using TTT.Server.NetworkShared.Attributes;
using UnityEngine;
using UnityEngine.SceneManagement;

[HandlerRegister(PacketType.OnQuitGame)]
public class OnQuitGameHandler : IPacketHandler
{
    public static event Action<NetOnQuitGame> OnQuitGame;
    public void Handle(INetPacket packet, int connectionID)
    {
        var msg =  (NetOnQuitGame)packet;
        Debug.Log("Quit Request!!!");
        if (NetworkClient.Instance.username == msg.Player)
        {
            SceneManager.LoadScene("01_Lobby");
            return;
        }

        OnQuitGame?.Invoke(msg);
    }
}
