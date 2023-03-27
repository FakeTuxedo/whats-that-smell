using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using Network.Packet.Analyzer.App.Forms.Main;

namespace Network.Packet.Analyzer.App.Forms.Startup
{
    public class StartupInfoPresenter
    {
        private IStartupInfo _view;

        public StartupInfoPresenter(IStartupInfo view)
        {
            _view = view;
            InitBufferSizeChooserCtrl();
            InitIPChooserCtrl();
        }

        private void InitIPChooserCtrl()
        {
            string hostName = Dns.GetHostName();
            IPAddress[] IPs = Dns.GetHostAddresses(hostName);

            foreach (IPAddress ip in IPs)
            {
                _view.AddIPItem(ip.ToString());
            }
        }

        private void InitBufferSizeChooserCtrl()
        {
            for (int i = 100; i < 1000; i += 100)
                _view.AddBufferSizeItem(i.ToString());

            for (int j = 1000; j < 100000; j += 1000)
                _view.AddBufferSizeItem(j.ToString());

            _view.SelectedBufferSize = 1000;
        }

        public void Start()
        {
            StartupInfo startupInfo = null;
            try
            {
                if (_view.SelectedBufferSize != -1 && _view.SelectedIPAddress != null)
                {
                    startupInfo = new StartupInfo(_view.SelectedIPAddress,_view.SelectedBufferSize);
                    //FrmAnalyzer.GetStartupInformation(startupInfo);
                    _view.SetStartupInformation(startupInfo);
                    _view.CloseDialog();
                }
                else
                    _view.ShowMessage("Make sure that Ip address and the buffer size are selected");
            }
            catch (Exception)
            {
            }
        }
    }
}
