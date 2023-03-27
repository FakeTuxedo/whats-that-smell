using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Net;
using Network.Packet.Analyzer.Core.Domain.Api;
using Network.Packet.Analyzer.Core.Domain.Api.Funct;

namespace Network.Packet.Analyzer.Core.Domain.Utils
{
    public class ProcessInformation
    {
        public ProcessInformation()
        {
        }

        public static List<Process> GetListOfProcesses()
        {
            return Process.GetProcesses().ToList<Process>();
        }

        public static Process FindProcessByPid(int pid)
        {
            return Process.GetProcesses().Where(p => p.Id == pid).SingleOrDefault();
        }

        public static string FindProcessNameByTcpConnection(IPAddress sourceAddress, IPAddress destinationAddress, ushort sourcePort, ushort destinationPort,IPAddress localIP)
        {
            List<TcpRecordPid> tcpRecords = null;
            ushort port;
            IPAddress address;

            if (localIP == sourceAddress)
            {
                port = sourcePort;
                address = sourceAddress;
            }
            else
            {
                port = destinationPort;
                address = destinationAddress;
            }
            if ((tcpRecords = NetworkStatisticData.GetAllTcpConnections()) != null && tcpRecords.Count > 0)
            {
                TcpRecordPid record = tcpRecords.Where(r=>r.LocalPort == port).SingleOrDefault();
                if (record != null)
                {
                    return record.PID.ToString();

                    //if (record.PID == 0)
                    //    return "System";
                    //else
                    //{
                    //    Process proc = null;
                    //    if ((proc = FindProcessByPid(record.PID)) != null)
                    //        return proc.ProcessName;
                    //    else
                    //        return "N/A";
                    //}
                }
            }

            return String.Empty;
        }


        public static string FindProcessNameByUDpConnection(IPAddress sourceAddress, IPAddress destinationAddress, ushort sourcePort, ushort destinationPort, IPAddress localIP)
        {
            List<UdpRecordPid> udpRecords = null;
            ushort port;
            IPAddress address;

            if (localIP == sourceAddress)
            {
                port = sourcePort;
                address = sourceAddress;
            }
            else
            {
                port = destinationPort;
                address = destinationAddress;
            }

            if ((udpRecords = NetworkStatisticData.GetAllUdpConnections()) != null)
            {
                UdpRecordPid record = udpRecords.Where(r => r.LocalPort == port).SingleOrDefault();
                if(record != null)
                    return record.PID.ToString();
            }

            return String.Empty;
        }
    }
}
