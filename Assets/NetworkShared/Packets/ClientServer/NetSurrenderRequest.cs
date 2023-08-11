using LiteNetLib.Utils;
using NetworkShared;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetSurrenderRequest : INetPacket
{
    public PacketType Type => PacketType.SurrenderRequest;

    public void Deserialize(NetDataReader reader)
    {

    }

    public void Serialize(NetDataWriter writer)
    {
        writer.Put((byte)Type);
    }

}
