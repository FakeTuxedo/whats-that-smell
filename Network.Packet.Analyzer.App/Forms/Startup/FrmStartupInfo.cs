using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using Network.Packet.Analyzer.App.Forms.Main.Interface;

namespace Network.Packet.Analyzer.App.Forms.Startup
{
    /// <summary>
    /// This dialog  called when start button is pressed 
    /// it sets all startup information (IPAddress and captured packets buffer maximum size)
    /// </summary>
    /// 
    public partial class FrmStartupInfo : Form, IStartupInfo
    {
        StartupInfoPresenter _presenter;
        IAnalyzer _mainView;

        public FrmStartupInfo(IAnalyzer mainView)
        {
            InitializeComponent();
            _mainView = mainView;
            _presenter = new StartupInfoPresenter(this);
        }

        #region IStartupInfo Members

        public void SetStartupInformation(StartupInfo info)
        {
            _mainView.StartupInformation = info;
        }

        public IPAddress SelectedIPAddress
        {
            get
            {
                if (comboIp.SelectedIndex != -1)
                    return IPAddress.Parse(comboIp.SelectedItem.ToString());

                return null;
            }
        }

        public int SelectedBufferSize
        {
            get
            {

                if (comboBuffer.SelectedIndex != -1)
                    return int.Parse(comboBuffer.SelectedItem.ToString());
                return -1;
            }
            set
            {
                if (comboBuffer.Items.Count > 0)
                    comboBuffer.SelectedItem = value.ToString();
            }
        }

        public void AddIPItem(string item)
        {
            comboIp.Items.Add(item);
        }

        public void AddBufferSizeItem(string item)
        {
            comboBuffer.Items.Add(item);
        }

        public void ShowMessage(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void CloseDialog()
        {
            this.Close();
        }
        #endregion

        private void btnStart_Click(object sender, EventArgs e)
        {
            _presenter.Start();
        }
    }
}
