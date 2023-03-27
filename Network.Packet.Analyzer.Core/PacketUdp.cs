using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;

namespace Network.Packet.Analyzer.Core
{
    /// <summary>
    /// This class contains UDP header structure
    /// 
    /// The User Datagram Protocol (UDP), defined by IETF RFC768, 
    /// provides a simple, but unreliable message service for transaction-oriented services. 
    /// Each UDP header carries both a source port identifier and destination port identifier, 
    /// allowing high-level protocols to target specific applications and services among hosts.
    /// 
    /// </summary>
    /// 

    public class PacketUdp
    {
          private ushort _usSourcePort;           // 16 bits for source port  
        private ushort _usDestinationPort;      // 16 bits for destination port
        private ushort _usLength;               // 16 bits for lenght(in octets)
        private short _sChecksum;               // 16 bits for checksum
                                                    

       // Buffer for data carried by UDP datagram
        private byte[] _bUDPData = new byte[4096];  

       /// <summary>
       /// Constructor wich takes 2 argument first argument is UDP packet data including header
       /// second argument bytes number 
       /// </summary>
       /// <param name="byBuffer"></param>
       /// <param name="nReceived"></param>
        public PacketUdp(byte [] byBuffer, int nReceived)
        {
                // Preparing for data reading from UDO packets
                MemoryStream memoryStream = null;
                BinaryReader binaryReader = null;

                try
                {
                    memoryStream = new MemoryStream(byBuffer, 0, nReceived);
                    binaryReader = new BinaryReader(memoryStream);

                    // reading first 16 bits wich contains source port
                    //IPAddress.NetworkToHostOrder(...) -> This method converts a value from host byte order to network byte order
                    _usSourcePort = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());

                    // 16 bits contains destination port
                    _usDestinationPort = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());

                    //next 16 bits contains lenght
                    _usLength = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());

                    // next 16 bits contains checksum
                    _sChecksum = IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());

                    // copy UDP packet data in to a buffer
                    Array.Copy(byBuffer, 8, _bUDPData, 0, nReceived - 8);
                }
                catch (Exception) { }

                finally
                {
                    binaryReader.Close();
                    memoryStream.Close();
                }
        }

        public string SourcePort
        {
            get {return _usSourcePort.ToString(); }
        }

        public string DestinationPort
        {
            get { return _usDestinationPort.ToString(); }
        }

        public string Length
        {
            get { return _usLength.ToString(); }
        }

        public string Checksum
        {
            get { return "0x" + _sChecksum.ToString("x"); }
        }

        public byte[] Data
        {
            get { return _bUDPData; }
        }
    }
}
