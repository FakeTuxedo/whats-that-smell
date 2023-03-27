using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Network.Packet.Analyzer.App.Forms.Main.Interface;
using Network.Packet.Analyzer.App.Forms.Main.Presenter;
using Network.Packet.Analyzer.App.Forms.Startup;
using System.Security.Permissions;
using System.Data.SqlTypes;

namespace Network.Packet.Analyzer.App.Forms.Main
{
    public partial class FrmAnalyzer : Form, IAnalyzer
    {
        public FormaAnalyzerPresenter _presenter;
     
        public FrmAnalyzer()
        {
            InitializeComponent();
            _presenter = new FormaAnalyzerPresenter(this);
        }
    
        //called when ListView control selection being made
        private void lstReceivedPackets_SelectedIndexChanged(object sender, EventArgs e)
        {
         
            _presenter.CreateDetailedTree();
        }

        //start button click event method
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            _presenter.StartClicked();
        }

        //stop button click event method
        private void tbtnStop_Click(object sender, EventArgs e)
        {
            _presenter.StopClicked();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _presenter.ApplicationStarted();
            
        }

        // clear all button click event method
        // clearing buffer,listvie control,and treeview
        private void tbtnClearAll_Click(object sender, EventArgs e)
        {
            _presenter.ClearAllClicked();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ApplicationClose();
        }

        private void menuAlwaysOnTop_Click(object sender, EventArgs e)
        {
            _presenter.TopMostClicked();
        }

        #region IAnalyzer Members

        public ListView ListReceivedPackets

        {
         
            get { return lstReceivedPackets; }
        }

        public ListView ListOpenPorts
        {
            get
            {
                return lstOpenPorts;
            }
        }

        public ProgressBar ProgressBufferusage
        {
            get { return progressBufferUsage; }
        }

        public TreeView TreePackedDetails
        {
            get {return treePacketDetails; }
        }

        public void SetTotalPacketReceivedText(string strNumber)
        {
            if (strNumber != null)
                lblTotalPkgsReceived.Text = strNumber;
        }

        public void SetBufferUsage(string strNumber)
        {
            if (strNumber != null)
                lblBufferUsage.Text = strNumber;
        }

        public void SetReadyText(string text)
        {
            if (text != null)
                lblStripReady.Text = text;
        }

        public bool ButtonStartEnabled
        {
            get { return tbtnStar.Enabled; }
            set { tbtnStar.Enabled = value; }
        }
        public bool ButtonStopEnabled
        {
            get { return tbtnStop.Enabled; }
            set { tbtnStop.Enabled = value; }
        }

        public bool TopMostChecked
        {
            get
            {
                return topMostMenuItem.Checked;
            }
            set
            {
                topMostMenuItem.Checked = value;
            }
        }
        public bool FormShowAsTopMost
        {
            get
            {
                return this.TopMost;
            }
            set
            {
                this.TopMost = value;
            }
        }

        public void ApplicationClose()
        {
            this.Close();
        }

        public StartupInfo StartupInformation
        {
            get
            {
                return _presenter.StartupInformation;
            }
            set
            {
                _presenter.StartupInformation = value;
            }
        }

        public void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public void ShowWarningMessage(string message)
        {
            MessageBox.Show(message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        public void ShowDefaultErrorMessage()
        {
            MessageBox.Show("Unexpected error has acquired","Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public void ShowDefaultErrorMessage(Exception ex)
        {
            MessageBox.Show(String.Format("Unexpected error has acquired. Error message: {0}",ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public void ShowErrorMessage(string message, Exception ex)
        {
            MessageBox.Show(String.Format("{0}. Error message: {1}",message,ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void Invoke(Action act)
        {
            this.Invoke(new MethodInvoker(delegate { act(); }));
        }
        #endregion

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
         
        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
         chromiumWebBrowser1.LoadUrl(browser.browser1);
        }
    }
}
