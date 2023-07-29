using NetworkShared;
using System.Collections;
using System.Collections.Generic;
using TTT.Server.NetworkShared;
using TTT.Server.NetworkShared.Attributes;
using TTT.Server.NetworkShared.Packets.ServerClient;
using UnityEngine;
using UnityEngine.SceneManagement;
[HandlerRegister(PacketType.OnStartGame)]
public class OnStartGameHandler : IPacketHandler
{
    public void Handle(INetPacket packet, int connectionID)
    {
        var msg = (NetOnStartGame)packet;

        SceneManager.LoadScene("02_Game");
    }
}
