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

namespace Networkshared.Packets.ClientServer
{
    public class NetAuthRequest : INetPacket
    {
        public PacketType Type => PacketType.AuthRequest;

        public string Username { get; set; }

        public string Password { get; set; }

        public void Deserialize(NetDataReader reader)
        {
            Username = reader.GetString();
            Password = reader.GetString();
        }

        public void Serialize(NetDataWriter writer)
        {
            writer.Put((byte)Type);
            writer.Put(Username);
            writer.Put(Password);
        }
    }
}

