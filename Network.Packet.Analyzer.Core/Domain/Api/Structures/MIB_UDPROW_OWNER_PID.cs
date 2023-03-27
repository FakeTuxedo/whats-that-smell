using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Network.Packet.Analyzer.Core.Domain.Api.Structures
{
     [StructLayout(LayoutKind.Sequential)]
    public struct MIB_UDPROW_OWNER_PID
    {
         public uint localAddr;
         public byte localPort1;
         public byte localPort2;
         public byte localPort3;
         public byte localPort4;
         public int owningPid;
    }
}
