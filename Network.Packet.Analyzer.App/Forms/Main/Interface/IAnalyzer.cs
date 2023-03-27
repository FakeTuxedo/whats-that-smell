using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Network.Packet.Analyzer.App.Forms.Startup;

namespace Network.Packet.Analyzer.App.Forms.Main.Interface
{
    public interface IAnalyzer
    {
        ListView ListReceivedPackets { get; }
        ListView ListOpenPorts { get; }
        ProgressBar ProgressBufferusage { get; }
        TreeView TreePackedDetails{get;} 

        void SetTotalPacketReceivedText(string strNumber);
        void SetBufferUsage(string strNumber);
        void SetReadyText(string text);
        bool ButtonStartEnabled { get; set; }
        bool ButtonStopEnabled { get; set; }
        bool TopMostChecked { get; set; }
        bool FormShowAsTopMost { get; set; }
        void ApplicationClose();
        void ShowErrorMessage(string message);
        void ShowWarningMessage(string message);
        void ShowDefaultErrorMessage();
        void ShowDefaultErrorMessage(Exception ex);
        void ShowErrorMessage(string message, Exception ex);
        StartupInfo StartupInformation { get; set; }

        void Invoke(Action act);
    }
}
