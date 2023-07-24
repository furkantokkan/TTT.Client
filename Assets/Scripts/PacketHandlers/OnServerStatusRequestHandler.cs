using NetworkShared;
using System;
using TTT.Server.NetworkShared.Attributes;
using TTT.Server.NetworkShared.Packets.ServerClient;
using TTT.Server.NetworkShared;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace TTT.PacketHandlers
{
    [HandlerRegister(PacketType.OnServerStatus)]
    public class OnServerStatusRequestHandler : IPacketHandler
    {
        public static event Action<NetOnServerStatus> OnServerStatus;

        public void Handle(INetPacket packet, int connectionId)
        {

            if (SceneManager.GetActiveScene().name != "01_Lobby")
            {
                return;
            }

            var msg = (NetOnServerStatus)packet;
            OnServerStatus?.Invoke(msg);
        }
    }
}