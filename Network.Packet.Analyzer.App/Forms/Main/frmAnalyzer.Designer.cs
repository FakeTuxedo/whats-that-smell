namespace Network.Packet.Analyzer.App.Forms.Main
{
    partial class FrmAnalyzer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        //private void InitializeComponent()
        //{
        //    this.SuspendLayout();
        //    // 
        //    // FrmAnalyzer
        //    // 
        //    this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        //    this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        //    this.ClientSize = new System.Drawing.Size(284, 262);
        //    this.Name = "FrmAnalyzer";
        //    this.Text = "Network Sniffer";
        //    this.ResumeLayout(false);

        //}


                /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
           private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAnalyzer));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.topMostMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tbtnStar = new System.Windows.Forms.ToolStripButton();
            this.tbtnStop = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tbtnClearAll = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStripReady = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.chromiumWebBrowser1 = new CefSharp.WinForms.ChromiumWebBrowser();
            this.button2 = new System.Windows.Forms.Button();
            this.lstReceivedPackets = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.boxOpenPorts = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.lstOpenPorts = new System.Windows.Forms.ListView();
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader13 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader14 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader15 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader16 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel1 = new System.Windows.Forms.Panel();
            this.treePacketDetails = new System.Windows.Forms.TreeView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.progressBufferUsage = new System.Windows.Forms.ProgressBar();
            this.label4 = new System.Windows.Forms.Label();
            this.lblTotalPkgsReceived = new System.Windows.Forms.Label();
            this.lblBufferUsage = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.boxOpenPorts.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenuItem,
            this.viewMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1782, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // fileMenuItem
            // 
            this.fileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeMenuItem});
            this.fileMenuItem.Name = "fileMenuItem";
            this.fileMenuItem.Size = new System.Drawing.Size(46, 24);
            this.fileMenuItem.Text = "&File";
            // 
            // closeMenuItem
            // 
            this.closeMenuItem.Name = "closeMenuItem";
            this.closeMenuItem.Size = new System.Drawing.Size(128, 26);
            this.closeMenuItem.Text = "&Close";
            this.closeMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // viewMenuItem
            // 
            this.viewMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.topMostMenuItem});
            this.viewMenuItem.Name = "viewMenuItem";
            this.viewMenuItem.Size = new System.Drawing.Size(55, 24);
            this.viewMenuItem.Text = "&View";
            // 
            // topMostMenuItem
            // 
            this.topMostMenuItem.Name = "topMostMenuItem";
            this.topMostMenuItem.Size = new System.Drawing.Size(186, 26);
            this.topMostMenuItem.Text = "Always on top";
            this.topMostMenuItem.Click += new System.EventHandler(this.menuAlwaysOnTop_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbtnStar,
            this.tbtnStop,
            this.toolStripSeparator1,
            this.tbtnClearAll});
            this.toolStrip1.Location = new System.Drawing.Point(0, 28);
            this.toolStrip1.MinimumSize = new System.Drawing.Size(0, 37);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1782, 37);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tbtnStar
            // 
            this.tbtnStar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnStar.Image = ((System.Drawing.Image)(resources.GetObject("tbtnStar.Image")));
            this.tbtnStar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnStar.Name = "tbtnStar";
            this.tbtnStar.Size = new System.Drawing.Size(29, 34);
            this.tbtnStar.Text = "Start";
            this.tbtnStar.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // tbtnStop
            // 
            this.tbtnStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnStop.Image = ((System.Drawing.Image)(resources.GetObject("tbtnStop.Image")));
            this.tbtnStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnStop.Name = "tbtnStop";
            this.tbtnStop.Size = new System.Drawing.Size(29, 34);
            this.tbtnStop.Text = "Stop";
            this.tbtnStop.Click += new System.EventHandler(this.tbtnStop_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 37);
            // 
            // tbtnClearAll
            // 
            this.tbtnClearAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbtnClearAll.Image = ((System.Drawing.Image)(resources.GetObject("tbtnClearAll.Image")));
            this.tbtnClearAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbtnClearAll.Name = "tbtnClearAll";
            this.tbtnClearAll.Size = new System.Drawing.Size(29, 34);
            this.tbtnClearAll.Text = "Clear All";
            this.tbtnClearAll.Click += new System.EventHandler(this.tbtnClearAll_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStripReady});
            this.statusStrip1.Location = new System.Drawing.Point(0, 669);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1782, 25);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblStripReady
            // 
            this.lblStripReady.Margin = new System.Windows.Forms.Padding(0, 3, 1, 2);
            this.lblStripReady.Name = "lblStripReady";
            this.lblStripReady.Size = new System.Drawing.Size(50, 20);
            this.lblStripReady.Text = "Ready";
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(4, 19);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.chromiumWebBrowser1);
            this.splitContainer1.Panel1.Controls.Add(this.button2);
            this.splitContainer1.Panel1.Controls.Add(this.lstReceivedPackets);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.SystemColors.Window;
            this.splitContainer1.Panel2.Controls.Add(this.boxOpenPorts);
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Size = new System.Drawing.Size(1774, 575);
            this.splitContainer1.SplitterDistance = 375;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 0;
            this.splitContainer1.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer1_SplitterMoved);
            // 
            // chromiumWebBrowser1
            // 
            this.chromiumWebBrowser1.ActivateBrowserOnCreation = false;
            this.chromiumWebBrowser1.Location = new System.Drawing.Point(1294, -1);
            this.chromiumWebBrowser1.Name = "chromiumWebBrowser1";
            this.chromiumWebBrowser1.Size = new System.Drawing.Size(471, 281);
            this.chromiumWebBrowser1.TabIndex = 3;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1464, 286);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(141, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "View last IP in Netify";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // lstReceivedPackets
            // 
            this.lstReceivedPackets.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8});
            this.lstReceivedPackets.ForeColor = System.Drawing.SystemColors.WindowText;
            this.lstReceivedPackets.FullRowSelect = true;
            this.lstReceivedPackets.GridLines = true;
            this.lstReceivedPackets.HideSelection = false;
            this.lstReceivedPackets.Location = new System.Drawing.Point(0, 0);
            this.lstReceivedPackets.Margin = new System.Windows.Forms.Padding(4);
            this.lstReceivedPackets.Name = "lstReceivedPackets";
            this.lstReceivedPackets.Size = new System.Drawing.Size(1287, 309);
            this.lstReceivedPackets.TabIndex = 0;
            this.lstReceivedPackets.UseCompatibleStateImageBehavior = false;
            this.lstReceivedPackets.View = System.Windows.Forms.View.Details;
            this.lstReceivedPackets.SelectedIndexChanged += new System.EventHandler(this.lstReceivedPackets_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "No.";
            this.columnHeader1.Width = 70;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Time";
            this.columnHeader2.Width = 100;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Source";
            this.columnHeader3.Width = 100;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Source Port";
            this.columnHeader4.Width = 100;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Destination";
            this.columnHeader5.Width = 100;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Destination Port";
            this.columnHeader6.Width = 100;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Protocol";
            this.columnHeader7.Width = 100;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Package Size";
            this.columnHeader8.Width = 100;
            // 
            // boxOpenPorts
            // 
            this.boxOpenPorts.Controls.Add(this.button1);
            this.boxOpenPorts.Controls.Add(this.lstOpenPorts);
            this.boxOpenPorts.Location = new System.Drawing.Point(417, 0);
            this.boxOpenPorts.Margin = new System.Windows.Forms.Padding(4);
            this.boxOpenPorts.Name = "boxOpenPorts";
            this.boxOpenPorts.Padding = new System.Windows.Forms.Padding(4);
            this.boxOpenPorts.Size = new System.Drawing.Size(871, 305);
            this.boxOpenPorts.TabIndex = 1;
            this.boxOpenPorts.TabStop = false;
            this.boxOpenPorts.Text = "Open Ports";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1010, 231);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(178, 42);
            this.button1.TabIndex = 2;
            this.button1.Text = "update latest url";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lstOpenPorts
            // 
            this.lstOpenPorts.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader11,
            this.columnHeader12,
            this.columnHeader13,
            this.columnHeader14,
            this.columnHeader15,
            this.columnHeader16});
            this.lstOpenPorts.FullRowSelect = true;
            this.lstOpenPorts.GridLines = true;
            this.lstOpenPorts.HideSelection = false;
            this.lstOpenPorts.Location = new System.Drawing.Point(4, 22);
            this.lstOpenPorts.Margin = new System.Windows.Forms.Padding(4);
            this.lstOpenPorts.Name = "lstOpenPorts";
            this.lstOpenPorts.Size = new System.Drawing.Size(861, 278);
            this.lstOpenPorts.TabIndex = 1;
            this.lstOpenPorts.UseCompatibleStateImageBehavior = false;
            this.lstOpenPorts.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Local Port";
            this.columnHeader9.Width = 67;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "Protocol";
            this.columnHeader10.Width = 64;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "Local Address";
            this.columnHeader11.Width = 100;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "Remote Address";
            this.columnHeader12.Width = 100;
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "Remote Port";
            this.columnHeader13.Width = 120;
            // 
            // columnHeader14
            // 
            this.columnHeader14.Text = "State";
            this.columnHeader14.Width = 50;
            // 
            // columnHeader15
            // 
            this.columnHeader15.Text = "PID";
            // 
            // columnHeader16
            // 
            this.columnHeader16.Text = "Process Name";
            this.columnHeader16.Width = 100;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.treePacketDetails);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(417, 193);
            this.panel1.TabIndex = 0;
            // 
            // treePacketDetails
            // 
            this.treePacketDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treePacketDetails.Location = new System.Drawing.Point(0, 0);
            this.treePacketDetails.Margin = new System.Windows.Forms.Padding(4);
            this.treePacketDetails.Name = "treePacketDetails";
            this.treePacketDetails.Size = new System.Drawing.Size(417, 193);
            this.treePacketDetails.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.splitContainer1);
            this.groupBox1.Location = new System.Drawing.Point(0, 65);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(1782, 598);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Packet Analyzer:";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1392, 673);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Buffer usage:";
            // 
            // progressBufferUsage
            // 
            this.progressBufferUsage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBufferUsage.BackColor = System.Drawing.SystemColors.Control;
            this.progressBufferUsage.Location = new System.Drawing.Point(1487, 669);
            this.progressBufferUsage.Margin = new System.Windows.Forms.Padding(4);
            this.progressBufferUsage.Name = "progressBufferUsage";
            this.progressBufferUsage.Size = new System.Drawing.Size(169, 22);
            this.progressBufferUsage.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1155, 673);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(115, 16);
            this.label4.TabIndex = 6;
            this.label4.Text = "Packets received:";
            // 
            // lblTotalPkgsReceived
            // 
            this.lblTotalPkgsReceived.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotalPkgsReceived.AutoSize = true;
            this.lblTotalPkgsReceived.Location = new System.Drawing.Point(1280, 673);
            this.lblTotalPkgsReceived.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotalPkgsReceived.Name = "lblTotalPkgsReceived";
            this.lblTotalPkgsReceived.Size = new System.Drawing.Size(14, 16);
            this.lblTotalPkgsReceived.TabIndex = 7;
            this.lblTotalPkgsReceived.Text = "0";
            // 
            // lblBufferUsage
            // 
            this.lblBufferUsage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBufferUsage.AutoSize = true;
            this.lblBufferUsage.Location = new System.Drawing.Point(1660, 673);
            this.lblBufferUsage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBufferUsage.Name = "lblBufferUsage";
            this.lblBufferUsage.Size = new System.Drawing.Size(14, 16);
            this.lblBufferUsage.TabIndex = 8;
            this.lblBufferUsage.Text = "0";
            // 
            // FrmAnalyzer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1782, 694);
            this.Controls.Add(this.lblBufferUsage);
            this.Controls.Add(this.lblTotalPkgsReceived);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.progressBufferUsage);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmAnalyzer";
            this.Text = "What\'s That Smell? v0.01";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.boxOpenPorts.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripButton tbtnStar;
        private System.Windows.Forms.ToolStripButton tbtnStop;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView lstReceivedPackets;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.TreeView treePacketDetails;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolStripStatusLabel lblStripReady;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tbtnClearAll;
        private System.Windows.Forms.ToolStripMenuItem closeMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewMenuItem;
        private System.Windows.Forms.ToolStripMenuItem topMostMenuItem;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ProgressBar progressBufferUsage;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblTotalPkgsReceived;
        private System.Windows.Forms.Label lblBufferUsage;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListView lstOpenPorts;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.ColumnHeader columnHeader13;
        private System.Windows.Forms.ColumnHeader columnHeader14;
        private System.Windows.Forms.GroupBox boxOpenPorts;
        private System.Windows.Forms.ColumnHeader columnHeader15;
        private System.Windows.Forms.ColumnHeader columnHeader16;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private CefSharp.WinForms.ChromiumWebBrowser chromiumWebBrowser1;
    }
}