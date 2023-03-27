using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace Network.Packet.Analyzer.Core.Api
{
    public class TcpRecordPid
    {
        public IPAddress LocalAddress { get; set; }
        public IPAddress RemoteAddress { get; set; }
        public ushort LocalPort { get; set; }
        public ushort RemotePort { get; set; }
        public int PID { get; set; }
        public uint State { get; set; }

        public TcpRecordPid()
        {
        }

        public TcpRecordPid(IPAddress localIP, IPAddress remoteIP, ushort localPort, ushort remotePort, int pid, uint state)
        {
            LocalAddress = localIP;
            RemoteAddress = remoteIP;
            LocalPort = localPort;
            RemotePort = remotePort;
            PID = pid;
            State = state;
        }

    }
}
