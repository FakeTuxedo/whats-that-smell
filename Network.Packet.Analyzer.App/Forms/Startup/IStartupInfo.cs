using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace Network.Packet.Analyzer.App.Forms.Startup
{
    public interface IStartupInfo
    {
        IPAddress SelectedIPAddress { get; }
        int SelectedBufferSize { get; set; }

        void AddIPItem(string item);
        void AddBufferSizeItem(string item);
        void ShowMessage(string message);
        void CloseDialog();
        void SetStartupInformation(StartupInfo info);
    }
}
