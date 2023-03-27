using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;

namespace Network.Packet.Analyzer.Core
{
    public class PacketIP
    {
        private byte      _bVersionAndHeader;     //8 bits for version and header (4 bits version + 4bits header)
        private byte      _bTypeOfService;        //8 bits for type of service (TOS)(indicates the quality of service)
        private ushort    _usTotalLenght;         //16 bits for total lenght of datagram    
        private ushort    _usIdentification;      //16 bits for identification ( this value assigned by the sender)    
        private ushort    _usFlagsAndOffset;      //16 bits for flags and Fragment offset   
        private byte      _bTTL;                  //8 bits for timto live(TTL) 
        private byte      _bProtocol;             //8 bits for next level protocol
        private short     _sChecksum;             //16 bits for check sum (this checksum is for header only)    
                                                   
        private uint      _uiSourceAddress;       //32 bits for source address
        private uint      _uiDestinationAddress;  //32 bits for destination address
        
        private byte      _bHeaderLength;         //8 bits for IP header lenght
           
        private byte[]    byIPData = new byte[4096];  // data carried by IP packet 


        public PacketIP(byte[] bBuffer, int iReceived)
        {
                // Preparing for reading data from IP packet
                MemoryStream memoryStream = null;
                BinaryReader binaryReader = null;

                try
                {
                    memoryStream = new MemoryStream(bBuffer, 0, iReceived);
                    binaryReader = new BinaryReader(memoryStream);

                    // first 8 bits contains version(4 bits) ,and (4 bits) internet header lenght (IHL this point to the beginning of the data)
                    _bVersionAndHeader = binaryReader.ReadByte();

                    //8 bits which indicates the quality of service
                    _bTypeOfService = binaryReader.ReadByte();

                    //16 bits for lenght of the datagram in bytes ( the lenght may be up to 65535 bytes)
                    _usTotalLenght = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());

                    //16 bits for identification 
                    _usIdentification = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());

                    //16 bits for flags(3 bits) ,and fragment offset(13 bits) 
                    _usFlagsAndOffset = (ushort)IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());

                    //8 bits for time to live (TTL)
                    _bTTL = binaryReader.ReadByte();

                    //8 bits for higher level protocol
                    _bProtocol = binaryReader.ReadByte();

                    //16 bits for checksum (this checksum is for header only)
                    _sChecksum = IPAddress.NetworkToHostOrder(binaryReader.ReadInt16());

                    // 32 bits for source address
                    _uiSourceAddress = (uint)(binaryReader.ReadInt32());

                    //32 bits for destination address
                    _uiDestinationAddress = (uint)(binaryReader.ReadInt32());



                    _bHeaderLength = _bVersionAndHeader;

                    _bHeaderLength <<= 4;
                    _bHeaderLength >>= 4;

                    _bHeaderLength *= 4;

                    //copying data carried by IP packet in to a buffer
                    Array.Copy(bBuffer, _bHeaderLength, byIPData, 0, _usTotalLenght - _bHeaderLength);
                }
                finally
                {
                    binaryReader.Close();
                    memoryStream.Close();
                }
        }

        public string Version
        {
            get
            {
                
                if ((_bVersionAndHeader >> 4) == 4)
                {
                    return "IP v4";
                }

                else if ((_bVersionAndHeader >> 4) == 6)
                {
                    return "IP v6";
                }

                else
                {
                    return "Unknown";
                }
            }
        }

        public string HeaderLength
        {
            get { return _bHeaderLength.ToString(); }
        }

        public ushort MessageLength
        {
            get{return (ushort)(_usTotalLenght - _bHeaderLength);}
        }

        public string TypeOfService
        {
            get {return string.Format ("0x{0:x2} ({1})", _bTypeOfService, _bTypeOfService); }
        }

        public string Flags
        {
            get
            {
                int iFlags = _usFlagsAndOffset >> 13;
                if (iFlags == 2)
                {
                    return "Not fragmented";
                }
                else if (iFlags == 1)
                {
                    return "Fragmented";
                }
                else
                {
                    return iFlags.ToString();
                }
            }
        }

        public string FragmentationOffset
        {
            get
            {
                int iOffset = _usFlagsAndOffset << 3;
                iOffset >>= 3;

                return iOffset.ToString();
            }
        }

        public string TTL
        {
            get{ return _bTTL.ToString();}
        }

        public string Protocol
        {
            get
            {
                if (_bProtocol == 6)       
                    return "TCP";
                else if (_bProtocol == 17)
                    return "UDP";
                else if (_bProtocol == 1)
                    return "ICMP";
                else if (_bProtocol == 2)
                    return "IGMP";
                
                else
                {
                    return "Unknown";
                }
            }
        }

        public string Checksum
        {
            get{return "0x" + _sChecksum.ToString("x");}
        }

        public IPAddress SourceAddress
        {
            get{  return new IPAddress(_uiSourceAddress); }
        }

        public IPAddress DestinationAddress
        {
            get {return new IPAddress(_uiDestinationAddress); }
        }

        public string TotalLength
        {
            get{return _usTotalLenght.ToString(); }
        }

        public string Identification
        {
            get{ return _usIdentification.ToString();}
        }

        public byte[] Data
        {
            get{return byIPData;}
        }
    }
}
