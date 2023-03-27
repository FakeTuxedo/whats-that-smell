using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;

namespace Network.Packet.Analyzer.Core
{
    /// <summary>
    /// IGMP header structure
    /// The Internet Group Management Protocol (IGMP) is used by IP hosts 
    /// to report their host group memberships to any immediately 
    /// neighboring multicast routers. IGMP is a integral part of IP
    /// </summary>
    /// 
    public class PacketIgmp
    {
        private byte  _bType;                        // 8 bits for message type
        private short _sMaxResponseTime;             // 16 bits for Max response type (used only in membership query message)
        private short _sChecksum;                    // 16 bits for checksum
        private int   _iGroupAddress;                // 32 bits for grup Address ( Group address is set to 0 when sending a general query)(

        public PacketIgmp(byte[] buffer, int iReceived)
        {
            // preparing streams to read IGMP data from buffer
            MemoryStream _ms = null;
            BinaryReader _br = null;

            try
            {
                if (buffer.Length > 0)
                {
                    _ms = new MemoryStream(buffer, 0, iReceived);
                    _br = new BinaryReader(_ms);

                    // first byte contains Type number
                    _bType = _br.ReadByte();

                    // next 16 bits contains Maximum response time
                    _sMaxResponseTime = IPAddress.NetworkToHostOrder(_br.ReadInt16());

                    // next 16 bits contains Checksum
                    _sChecksum = IPAddress.NetworkToHostOrder(_br.ReadInt16());

                    //next 32 bits contains Group address
                    _iGroupAddress = IPAddress.NetworkToHostOrder(_br.ReadInt32());
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                _br.Close();
                _ms.Close();
            }
        }

        public string Type
        {
            get { return _bType.ToString(); }
        }
        public string MaxResponseTime
        {
            get { return _sMaxResponseTime.ToString(); }
        }
        public string Checksum
        {
            get { return "0x" + _sChecksum.ToString("X"); }
        }
        public string GropeAddress
        {
            get { return _iGroupAddress.ToString(); }
        }
    }
}
