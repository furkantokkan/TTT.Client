using NetworkShared;
using System;
using System.Collections;
using System.Collections.Generic;
using TTT.Server.NetworkShared;
using TTT.Server.NetworkShared.Attributes;
using TTT.Server.NetworkShared.Packets.ServerClient;
using TTT.Server.NetworkShared.Models;
using UnityEngine;

[HandlerRegister(PacketType.OnMarkCell)]
public class OnMarkCellHandler : IPacketHandler
{
    public static event Action<NetOnMarkCell> OnMarkCell;
    public void Handle(INetPacket packet, int connectionID)
    {
        var msg = (NetOnMarkCell)packet;

        GameManager.instance.ActiveGame.SwitchCurrentPlayer();

        if (GameManager.instance.IsClientTurn && msg.Outcome == MarkOutcome.None)
        {
            GameManager.instance.inputEnabled = true;
        }

        if (msg.Outcome > MarkOutcome.None)
        {
            GameManager.instance.inputEnabled = false;
        }
        OnMarkCell?.Invoke(msg);
    }
}
