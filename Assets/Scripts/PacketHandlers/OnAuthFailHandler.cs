using NetworkShared;
using System;
using System.Collections;
using System.Collections.Generic;
using TTT.Server.NetworkShared;
using TTT.Server.NetworkShared.Attributes;
using TTT.Server.NetworkShared.Packets.ServerClient;
using UnityEngine;

[HandlerRegister(PacketType.OnAuthFail)]
public class OnAuthFailHandler : IPacketHandler
{
    public static event Action<NetOnAuthFail> OnAuthFail;

    public void Handle(INetPacket packet, int connectionID)
    {
        var msg = (NetOnAuthFail)packet;
        OnAuthFail?.Invoke(msg);
    }
}
