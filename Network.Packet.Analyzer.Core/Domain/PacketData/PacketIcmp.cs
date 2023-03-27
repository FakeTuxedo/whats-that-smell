using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;

namespace Network.Packet.Analyzer.Core.Domain.PacketData
{
    /// <summary>
    /// ICMP header structure
    /// 
    /// IETF RFC792 defines the Internet Control Message Protocol (ICMP). 
    /// ICMP messages generally contain information about 
    /// routing difficulties with IP datagrams or simple exchanges such as time-stamp or echo transactions.
    /// 
    /// </summary>
    /// 
    public class PacketIcmp
    {
          /// <summary>
        ///  Type and Code contains numbers which may provide some information about 
        /// host,port,destination ... etc
        /// </summary>
        /// 
        private byte _bType;                  //8 bytes for Type number
        private byte _bCode;                  //8 bits for Code
        private short _sChecksum;             //16bits for checksum
        private ushort _sIdentifier;          //16 bits for identiffier
        private ushort _sSequenceNUmber;      //16 bits for sequence number
        private int _iAddressMask;            //32 bits for address mask

        public PacketIcmp(byte[] buffer, int iReceived)
        {
            MemoryStream ms = null;
            BinaryReader br = null;

            try
            {

                ms = new MemoryStream(buffer, 0, iReceived);
                br = new BinaryReader(ms);
                //first byte contains TYpe number
                _bType = br.ReadByte();

                //next byte contains Code number
                _bCode = br.ReadByte();

                //next 16 bits contains checksum
                _sChecksum = IPAddress.NetworkToHostOrder(br.ReadInt16());

                //next 16 bits contains identiffier
                _sIdentifier = (ushort)IPAddress.NetworkToHostOrder(br.ReadInt16());

                //next 16 bits contains sequence number
                _sSequenceNUmber = (ushort)IPAddress.NetworkToHostOrder(br.ReadInt16());

                //32 bits contains Address mask
                _iAddressMask = IPAddress.NetworkToHostOrder(br.ReadInt32());
            }
            finally
            {
                br.Close();
                ms.Close();
            }
        }

        public string Type
        {
            get{return _bType.ToString();}
        }

        public string Code
        {
            get { return _bCode.ToString(); }
        }
        public string Checksum
        {
            get { return "0x" + _sChecksum.ToString("X"); }
        }
        public string Identifier
        {
            get { return _sIdentifier.ToString(); }
        }
        public string SequenceNUmber
        {
            get { return _sSequenceNUmber.ToString(); }
        }
        public string AddressMask
        {
            get { return "0x" + _iAddressMask.ToString("X"); }
        }
    }
}
