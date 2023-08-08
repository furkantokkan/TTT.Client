using NetworkShared;
using System;
using System.Collections;
using System.Collections.Generic;
using TTT.Server.NetworkShared.Attributes;
using UnityEngine;

namespace TTT.Server.NetworkShared.Packets.ClientServer
{
    [HandlerRegister(PacketType.OnPlayAgain)]
    public class OnPlayAgainHandler : IPacketHandler
    {

        public static event Action OnPlayAgain;

        public void Handle(INetPacket packet, int connectionID)
        {
            OnPlayAgain?.Invoke();
        }
    }
}