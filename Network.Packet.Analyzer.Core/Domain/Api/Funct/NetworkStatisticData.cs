using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Network.Packet.Analyzer.Core.Domain.Api.Structures;
using System.Runtime.InteropServices;
using System.Net;

namespace Network.Packet.Analyzer.Core.Domain.Api.Funct
{
    public class NetworkStatisticData
    {
//        DWORD GetExtendedUdpTable(
//  __out    PVOID pUdpTable,
//  __inout  PDWORD pdwSize,
//  __in     BOOL bOrder,
//  __in     ULONG ulAf,
//  __in     UDP_TABLE_CLASS TableClass,
//  __in     ULONG Reserved
//);

//        DWORD GetExtendedTcpTable(
//  __out    PVOID pTcpTable,
//  __inout  PDWORD pdwSize,
//  __in     BOOL bOrder,
//  __in     ULONG ulAf,
//  __in     TCP_TABLE_CLASS TableClass,
//  __in     ULONG Reserved
//);


        [DllImport("iphlpapi.dll", SetLastError = true)]
        static extern uint GetExtendedTcpTable(IntPtr pTcpTable, ref int dwOutBufLen, bool sort, int ipVersion, TCP_TABLE_CLASS tblClass, int reserved);

        [DllImport("iphlpapi.dll", SetLastError = true)]
        static extern uint GetExtendedUdpTable(IntPtr pTcpTable, ref int dwOutBufLen, bool sort, int ipVersion, UDP_TABLE_CLASS tblClass, int reserved);



        public static List<TcpRecordPid> GetAllTcpConnections()
        {
            int AF_INET = 2;    // IP_v4 
            int buffSize = 0;

            // getting the memory size needed
            uint val = GetExtendedTcpTable(IntPtr.Zero, ref buffSize, true, AF_INET,TCP_TABLE_CLASS.TCP_TABLE_OWNER_PID_ALL, 0);

            if (val != 0 && val != 122) 
                throw new Exception("invalid size " + val);

            IntPtr buffTable = Marshal.AllocHGlobal(buffSize);
            List<TcpRecordPid> lstRecords = new List<TcpRecordPid>();
            try
            {
                val = GetExtendedTcpTable(buffTable,ref buffSize, true, AF_INET,TCP_TABLE_CLASS.TCP_TABLE_OWNER_PID_ALL,0);
                
                if (val != 0)
                    throw new Exception("ivalid data " + val);

                // get the number of entries in the table 
                MIB_TCPTABLE_OWNER_PID tcpTable = (MIB_TCPTABLE_OWNER_PID)Marshal.PtrToStructure( buffTable, typeof(MIB_TCPTABLE_OWNER_PID));
                IntPtr rowPtr = (IntPtr)((long)buffTable + Marshal.SizeOf(tcpTable.dwNumEntries));
                
                for (int i = 0; i < tcpTable.dwNumEntries; i++)
                {
                    MIB_TCPROW_OWNER_PID tcpRow = (MIB_TCPROW_OWNER_PID)Marshal.PtrToStructure(rowPtr, typeof(MIB_TCPROW_OWNER_PID));
                    lstRecords.Add(new TcpRecordPid(
                        new IPAddress(tcpRow.localAddr),
                        new IPAddress(tcpRow.remoteAddr),
                        BitConverter.ToUInt16(new byte[2] { tcpRow.localPort2, tcpRow.localPort1 }, 0),   // reverse order
                        BitConverter.ToUInt16(new byte[2] { tcpRow.remotePort2, tcpRow.remotePort1 }, 0), // reverse order
                        tcpRow.owningPid,
                        tcpRow.state));
                    // next record
                    rowPtr = (IntPtr)((long)rowPtr + Marshal.SizeOf(tcpRow));
                }
            }
            finally
            {
                Marshal.FreeHGlobal(buffTable);
            }
            return lstRecords.Distinct().ToList<TcpRecordPid>();
        }

        public static List<UdpRecordPid> GetAllUdpConnections()
        {
            int AF_INET = 2;    // IP_v4 
            int buffSize = 0;

            // getting the memory size needed
            uint val = GetExtendedUdpTable(IntPtr.Zero, ref buffSize, true, AF_INET, UDP_TABLE_CLASS.UDP_TABLE_OWNER_PID, 0);

            if (val != 0 && val != 122)
                throw new Exception("invalid size " + val);

            IntPtr buffTable = Marshal.AllocHGlobal(buffSize);
            List<UdpRecordPid> lstRecords = new List<UdpRecordPid>();
            try
            {
                val = GetExtendedUdpTable(buffTable, ref buffSize, true, AF_INET, UDP_TABLE_CLASS.UDP_TABLE_OWNER_PID, 0);

                if (val != 0)
                    throw new Exception("ivalid data " + val);

                // get the number of entries in the table 
                MIB_UDPTABLE_OWNER_PID udpTable = (MIB_UDPTABLE_OWNER_PID)Marshal.PtrToStructure(buffTable, typeof(MIB_UDPTABLE_OWNER_PID));
                IntPtr rowPtr = (IntPtr)((long)buffTable + Marshal.SizeOf(udpTable.dwNumEntries));

                for (int i = 0; i < udpTable.dwNumEntries; i++)
                {
                    MIB_UDPROW_OWNER_PID udpRow = (MIB_UDPROW_OWNER_PID)Marshal.PtrToStructure(rowPtr, typeof(MIB_UDPROW_OWNER_PID));
                    lstRecords.Add(new UdpRecordPid(
                        new IPAddress(udpRow.localAddr),
                        BitConverter.ToUInt16(new byte[2] { udpRow.localPort2, udpRow.localPort1 }, 0),   // reverse order
                        udpRow.owningPid));
                    // next record
                    rowPtr = (IntPtr)((long)rowPtr + Marshal.SizeOf(udpRow));
                }
            }
            finally
            {
                Marshal.FreeHGlobal(buffTable);
            }
            return lstRecords.Distinct().ToList<UdpRecordPid>();
        }
    }
}
