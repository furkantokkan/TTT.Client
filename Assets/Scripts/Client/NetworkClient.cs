using LiteNetLib;
using LiteNetLib.Utils;
using NetworkShared;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using TTT.Server.NetworkShared;
using TTT.Server.NetworkShared.Registries;
using UnityEngine;

public class NetworkClient : MonoBehaviour, INetEventListener
{
    private NetManager netManager;

    private NetPeer server;
    private NetDataWriter writer;

    private PacketRegistry packetRegistry;
    private HandlerRegistry handlerRegistry;

    private static NetworkClient intance;

    public event Action onServerConnected;
    public event Action onServerDisconnected;

    public static NetworkClient Instance
    {
        get
        {
            return intance;
        }
    }

    private void Awake()
    {
        if (intance == null)
        {
            intance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        Initialzie();
    }

    private void Initialzie()
    {
        packetRegistry = new PacketRegistry();
        handlerRegistry = new HandlerRegistry();
        writer = new NetDataWriter();
        netManager = new NetManager(this)
        {
            DisconnectTimeout = 300000,
        };

        netManager.Start();
    }

    private void Update()
    {
        netManager.PollEvents();
    }
    private void OnApplicationQuit()
    {
        DisconnectFromServer();
    }
    private void OnDestroy()
    {
        if (server != null)
        {
            netManager.Stop();
        }    
    }

    public void DisconnectFromServer()
    {
        netManager.DisconnectAll();
    }

    public void Connect()
    {
        netManager.Connect("localhost", 9050, "");
    }

    public void SendServer<T>(T packet, DeliveryMethod deliveryMethod = DeliveryMethod.ReliableSequenced) where T : INetSerializable
    {
        if (server == null)
        {
            return;
        }

        writer.Reset();
        packet.Serialize(writer);
        server.Send(writer, deliveryMethod);
    }

    public void OnConnectionRequest(ConnectionRequest request)
    {
        Debug.Log("<color=green>" + "Connection requested" + "</color>");
    }

    public void OnNetworkError(IPEndPoint endPoint, SocketError socketError)
    {
        Debug.Log("<color=red>" + "Network error, casuse is: " + socketError.ToString() + "</color>");
    }

    public void OnNetworkLatencyUpdate(NetPeer peer, int latency)
    {

    }

    public void OnNetworkReceive(NetPeer peer, NetPacketReader reader, byte channelNumber, DeliveryMethod deliveryMethod)
    {
        //var data = Encoding.UTF8.GetString(reader.RawData).Replace("\0", "");
        var packetType = (PacketType)reader.GetByte();
        var packet = ResolvePacket(packetType, reader);
        var handler = ResolveHandler(packetType);
        handler.Handle(packet, peer.Id);
        Debug.Log("<color=green>" + "Data recived from sever: " + packet.Type.ToString() + "</color>");
        reader.Recycle();

    }

    public void OnNetworkReceiveUnconnected(IPEndPoint remoteEndPoint, NetPacketReader reader, UnconnectedMessageType messageType)
    {

    }

    public void OnPeerConnected(NetPeer peer)
    {
        Debug.Log("<color=green>" + "On Peer Connected at " + peer.EndPoint + "</color>");
        server = peer;
        onServerConnected?.Invoke();
    }

    public void OnPeerDisconnected(NetPeer peer, DisconnectInfo disconnectInfo)
    {
        Debug.Log("<color=red>" + "Disconnected from server, casuse is: " + disconnectInfo.Reason.ToString() + "</color>");
        onServerDisconnected?.Invoke();
    }
    private INetPacket ResolvePacket(PacketType packetType, NetPacketReader reader)
    {
        Type type = packetRegistry.PacketTypes[packetType];
        var packet = (INetPacket)Activator.CreateInstance(type);
        packet.Deserialize(reader);
        return packet;

    }
    private IPacketHandler ResolveHandler(PacketType packetType)
    {
        Type handlerType = handlerRegistry.Handlers[packetType];
        return (IPacketHandler)Activator.CreateInstance(handlerType);
    }
}
