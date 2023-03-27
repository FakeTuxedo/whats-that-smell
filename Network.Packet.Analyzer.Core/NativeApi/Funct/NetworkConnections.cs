using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Network.Packet.Analyzer.Core.NativeApi.Structures;
using System.Runtime.InteropServices;

namespace Network.Packet.Analyzer.Core.NativeApi.Funct
{
    public class NetworkConnections
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
        static extern uint GetExtendedUdpTable(IntPtr pTcpTable, ref int dwOutBufLen, bool sort, int ipVersion, TCP_TABLE_CLASS tblClass, int reserved);

        public MIB_TCPROW_OWNER_PID[] SelectAllTcpConnections()
        {
            MIB_TCPROW_OWNER_PID[] tTable;
            int AF_INET = 2;    // IP_v4
            int buffSize = 0;

            // calculate memory usage
            uint res = GetExtendedTcpTable(IntPtr.Zero, ref buffSize, true, AF_INET, TCP_TABLE_CLASS.TCP_TABLE_OWNER_PID_ALL, 0);
            IntPtr buffTable = Marshal.AllocHGlobal(buffSize);

            try
            {
                res = GetExtendedTcpTable(buffTable, ref buffSize, true, AF_INET, TCP_TABLE_CLASS.TCP_TABLE_OWNER_PID_ALL, 0);
                if (res != 0)
                {
                    return null;
                }

                // get the number of entries in the table
                MIB_TCPTABLE_OWNER_PID tab = (MIB_TCPTABLE_OWNER_PID)Marshal.PtrToStructure(buffTable, typeof(MIB_TCPTABLE_OWNER_PID));
                
                IntPtr rowPtr = (IntPtr)((long)buffTable + Marshal.SizeOf(tab.dwNumEntries));
                
                // buffer we will be returning
                tTable = new MIB_TCPROW_OWNER_PID[tab.dwNumEntries];
     
                for (int i = 0; i < tab.dwNumEntries; i++)
                {
                    MIB_TCPROW_OWNER_PID tcpRow = (MIB_TCPROW_OWNER_PID)Marshal.PtrToStructure(rowPtr, typeof(MIB_TCPROW_OWNER_PID));
                    tTable[i] = tcpRow;

                    //getting the next entry
                    //  C/C++  baseAddress+= sizeof( MIB_TCPROW_OWNER_PID);
                    rowPtr = (IntPtr)((long)rowPtr + Marshal.SizeOf(tcpRow));
                }

            }
            finally
            {
                // Free the Memory
                Marshal.FreeHGlobal(buffTable);
            }



            return tTable;
        }
    }
}
