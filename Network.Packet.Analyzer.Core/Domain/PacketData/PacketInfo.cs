using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Network.Packet.Analyzer.Core.Domain.PacketData
{
    /// <summary>
    /// This class is designed to carried data 
    /// and place this data in to a buffer 
    /// </summary>
    public class PacketInfo
    {
        PacketIP   _ip;     // IP packet information
        PacketTcp  _tcp;    // TCP header information
        PacketUdp  _udp;    // UDP header information
        PacketIcmp _icmp;   // ICMP header information
        PacketIgmp _igmp;   // IGMP header informatiom

        /// <summary>
        /// 
        /// Some overloaded constructors 
        /// they may teke any of packets combination 
        /// ECSAMPLE: if IP packet contains TCP protocol and data using ->PacketInfo(IPData ip,TCPData tcp)
        /// 
        /// </summary>
        /// 
        public PacketInfo()
        {
        }
        public PacketInfo(PacketIP ip)
        {
            _ip = ip;
        }
        public PacketInfo(PacketIP ip, PacketTcp tcp)
        {
            _ip = ip;
            _tcp = tcp;
        }
        public PacketInfo(PacketIP ip, PacketUdp udp)
        {
            _ip = ip;
            _udp = udp;
        }
        public PacketInfo(PacketIP ip, PacketIcmp icmp)
        {
            _ip = ip;
            _icmp = icmp;
        }
        public PacketInfo(PacketIP ip, PacketIgmp igmp)
        {
            _ip = ip;
            _igmp = igmp;
        }

        public PacketIP IP
        {
            get { return _ip; }
        }
        public PacketTcp TCP
        {
            get { return _tcp; }
        }
        public PacketUdp UDP
        {
            get { return _udp; }
        }
        public PacketIcmp ICMP
        {
            get { return _icmp; }
        }
        public PacketIgmp IGMP
        {
            get { return _igmp; }
        }

    }
}
