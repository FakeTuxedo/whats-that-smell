using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;

namespace Network.Packet.Analyzer.Core.Domain.PacketData
{
    public class PacketTcp
    {
         /// <summary>
        ///  TCP header structure
        /// 
        /// IETF RFC793 defines the Transmission Control Protocol (TCP). 
        /// TCP provides a reliable stream delivery and virtual connection service 
        /// to applications through the use of sequenced acknowledgment with 
        /// retransmission of packets when necessary.
        /// 
        /// </summary>
        /// 
        private ushort _usSourcePort;         //16 bits for source port         
        private ushort _usDestinationPort;    //16 bits for destination port   
        private uint   _uiSequenceNumber;     //32 bits for sequence number   
        private uint   _uiAckNumber;          //32 bits for acknowledgement number
        private ushort _usDataOffsetAndFlags; //16 bits for data offset and flags  
        private ushort _usWindow;             //16 bits for window size   
        private short  _sChecksum;            //16 bits for checksum
                                                    
        private ushort _usUrgentPointer;      //16 bits for urgent pointer    

        private byte   _bHeaderLength;              //8 bits for TCP header lenght   
        private ushort _usMessageLength;            // data lenght carried by TCP packet    
        private byte[] _bTCPData = new byte[4096];  // buffer for data carried by TCP packet
       
        public PacketTcp(byte [] bBuffer, int iReceived)
        {
                // Preparing to read data from buffer (creating stream objects)
                MemoryStream memoryStream = null;
                BinaryReader binaryReader = null;

                try
                {
                    memoryStream = new MemoryStream(bBuffer, 0, iReceived);
                    binaryReader = new BinaryReader(memoryStream);

                    // first 16 bits for source port 
                    // NetworkToHostOrder() this method converts a value from host byte order to network byte order
                    _usSourcePort = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());

                    // 16 bits for destination port
                    _usDestinationPort = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());

                    // next 32 bits represent sequence number
                    _uiSequenceNumber = (uint)IPAddress.NetworkToHostOrder(binaryReader.ReadInt32());

                    // 32 bits for acknowledgement number
                    _uiAckNumber = (uint)IPAddress.NetworkToHostOrder(binaryReader.ReadInt32());

                    // 16 bits for offset and flags (flags 8 bits and 8 bits for data offset
                    _usDataOffsetAndFlags = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());

                    // 16 bits for window size
                    _usWindow = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());

                    // 16 bits for checksum
                    _sChecksum = (short)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());

                    //16 bits for urgentpoint
                    _usUrgentPointer = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());

                    // counting lenght of TCP header
                    _bHeaderLength = (byte)(_usDataOffsetAndFlags >> 12);
                    _bHeaderLength *= 4;

                    // counting lenght of data carried by TCP packet
                    _usMessageLength = (ushort)(iReceived - _bHeaderLength);

                    //copyong data carried by TCP packet in to buffer
                    Array.Copy(bBuffer, _bHeaderLength, _bTCPData, 0, iReceived - _bHeaderLength);
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
            get { return _usSourcePort.ToString(); }
        }

        public string DestinationPort
        {
            get { return _usDestinationPort.ToString(); }
        }

        public string SequenceNumber
        {
            get { return _uiSequenceNumber.ToString(); }
        }

        public string AcknowledgementNumber
        {
            get
            {
                if ((_usDataOffsetAndFlags & 0x10) != 0)
                    return _uiAckNumber.ToString();
                else
                    return "";
            }
        }

        public string HeaderLength
        {
            get { return _bHeaderLength.ToString(); }
        }

        public string WindowSize
        {
            get { return _usWindow.ToString(); }
        }

        public string UrgentPointer
        {
            get
            {
                if ((_usDataOffsetAndFlags & 0x20) != 0)
                    return _usUrgentPointer.ToString();
                else
                    return "";
            }
        }

        public string Flags
        {
            get
            {
                int iFlags = _usDataOffsetAndFlags & 0x3F;
 
                string strFlags = string.Format ("0x{0:x2} ", iFlags);

                if ((iFlags & 0x01) != 0)
                    strFlags += "FIN  ";

                if ((iFlags & 0x02) != 0)
                    strFlags += "SYN  ";

                if ((iFlags & 0x04) != 0)
                    strFlags += "RST  ";

                if ((iFlags & 0x08) != 0)
                    strFlags += "PSH  ";

                if ((iFlags & 0x10) != 0)
                    strFlags += "ACK  ";

                if ((iFlags & 0x20) != 0)
                    strFlags += "URG ";

                if (strFlags.Contains("()"))
                    strFlags = strFlags.Remove(strFlags.Length - 3);

                else if (strFlags.Contains(", )"))
                    strFlags = strFlags.Remove(strFlags.Length - 3, 2);

                return strFlags;
            }
        }

        public string Checksum
        {
            get { return "0x" + _sChecksum.ToString("x"); }
        }

        public byte[] Data
        {
            get { return _bTCPData; }
        }

        public string MessageLength
        {
            get { return _usMessageLength.ToString(); }
        }
    }
}
