﻿using NetworkShared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTT.Server.NetworkShared
{
    public interface IPacketHandler
    {
        void Handle(INetPacket packet, int connectionID);
    }
}
