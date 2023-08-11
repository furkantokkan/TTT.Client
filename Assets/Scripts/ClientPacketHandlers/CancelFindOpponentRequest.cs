using NetworkShared;
using System;
using System.Collections;
using System.Collections.Generic;
using TTT.Server.NetworkShared.Attributes;
using TTT.Server.NetworkShared;
using UnityEngine;


[HandlerRegister(PacketType.CancelFindOpponentRequest)]
public class CancelFindOpponentRequestHandler : IPacketHandler
{
    public void Handle(INetPacket packet, int connectionId)
    {
        Console.WriteLine("Received cancel find opponent request!");
    }
}
