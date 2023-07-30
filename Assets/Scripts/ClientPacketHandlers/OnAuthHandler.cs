using LiteNetLib.Utils;
using NetworkShared;
using System.Collections;
using System.Collections.Generic;
using LiteNetLib;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using TTT.Server.NetworkShared;
using TTT.Server.NetworkShared.Registries;
using UnityEngine;
using TTT.Server.NetworkShared.Attributes;
using UnityEngine.SceneManagement;
using TTT.Server.NetworkShared.Packets.ServerClient;

[HandlerRegister(PacketType.OnAuth)]
public class OnAuthHandler : IPacketHandler
{
    public void Handle(INetPacket packet, int connectionID)
    {
        SceneManager.LoadScene(1);
        var msg = (NetOnAuth)packet;
        NetworkClient.Instance.username = msg.username;
        NetworkClient.Instance.connectionID = connectionID;
        Debug.Log("Load Game Scene");
    }
}
