using NetworkShared;
using System;
using System.Collections;
using System.Collections.Generic;
using TTT.Server.NetworkShared;
using TTT.Server.NetworkShared.Attributes;
using UnityEngine;

[HandlerRegister(NetworkShared.PacketType.OnSurrender)]
public class OnSurrenderHandler : IPacketHandler
{
    public static Action<NetOnSurrender> OnSurrender;
    public void Handle(INetPacket packet, int connectionID)
    {
        OnSurrender?.Invoke((NetOnSurrender)packet);
    }
}
