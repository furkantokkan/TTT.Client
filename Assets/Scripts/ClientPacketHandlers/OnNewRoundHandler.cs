using NetworkShared;
using System;
using System.Collections;
using System.Collections.Generic;
using TTT.Server.NetworkShared;
using TTT.Server.NetworkShared.Attributes;
using UnityEngine;


[HandlerRegister(PacketType.OnNewRound)]
public class OnNewRoundHandler : IPacketHandler
{
    public static event Action OnNewRound;
    public void Handle(INetPacket packet, int connectionID)
    {
        OnNewRound?.Invoke();
        GameManager.instance.ActiveGame.Reset();
        GameManager.instance.inputEnabled = true;
    }
}
