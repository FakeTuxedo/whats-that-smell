using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace Network.Packet.Analyzer.App.Forms.Startup
{
    /// <summary>
    /// 
    /// This class provides startup information such as local IPAddress and 
    /// buffersize for captured packets 
    /// 
    /// </summary>
    /// 
    public class StartupInfo
    {
        private IPAddress _ipAddress;
        private int _iPacketsToCapture;

        public StartupInfo() { _ipAddress = null; _iPacketsToCapture = 0; }
        public StartupInfo(IPAddress ip, int size) { _ipAddress = ip; _iPacketsToCapture = size; }

        public IPAddress IP { get { return _ipAddress; } }
        public int PacketsToCapture { get { return _iPacketsToCapture; } }
    }
}
