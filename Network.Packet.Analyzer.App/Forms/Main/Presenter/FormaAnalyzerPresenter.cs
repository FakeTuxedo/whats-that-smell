using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Network.Packet.Analyzer.App.Forms.Main.Interface;
using System.Windows.Forms;
using Network.Packet.Analyzer.App.Forms.Startup;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.Drawing;
using Network.Packet.Analyzer.Core.Domain.PacketData;
using Network.Packet.Analyzer.Core.Domain.Utils;
using System.Timers;
using Network.Packet.Analyzer.Core.Domain.Api.Funct;
using Network.Packet.Analyzer.Core.Domain.Api;
using System.Diagnostics;
using System.Text.RegularExpressions;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Data.SqlTypes;


namespace Network.Packet.Analyzer.App.Forms.Main.Presenter
{
    public class FormaAnalyzerPresenter
    {
        private IPAddress _localIP;
        private IAnalyzer _view;
        string ip;
        public delegate void NewAddToListEvent(ListViewItem item);     // delegate used to create new AddToList event
        public delegate void NewRemoveFromListEvent();                 // delegate used to create RemoVeFromList event
        public delegate void NewAddToPortListEvent(ListViewItem item);
        public delegate void NewRemoveFromPortListEvent(ListViewItem item);
        public delegate void NewRemoveFromPortListByKeyEvent(string key);

        public event NewAddToListEvent AddToList;             // event used to add new (ListViewItem) in to ListView user control
        public event NewRemoveFromListEvent RemoveFromList;   // event used to remove data frpm List
        public event NewAddToPortListEvent AddtoPortList;
        public event NewRemoveFromPortListEvent RemoveFromPortlist;
        public event NewRemoveFromPortListByKeyEvent RemoveFromPortlistByKey;
        public string browserurl;

        // this should be filled before start capturing
        // this class contains local IPAddress and buffer size
        public StartupInfo StartupInformation { get; set; }

        private System.Windows.Forms.Timer TimerPorts { get; set; }

        // This collection class represents buffer to store PacketInfo class ecsemplars  
        Dictionary<string, PacketInfo> _pkgBuffer = null;

        // this should be filled before start capturing
        // this class contains local IPAddress and buffer size
     //   private static StartupInfo _startupInfo = null;

        Thread thrStartCapturing = null;

        Socket _socket;

        //this value shows total number of all received packages
        decimal _decPackagesReceived;

        //buffer for received data
        byte[] _bBuffer = new byte[8192];

        //contains true if Stop button pressed
        bool _stopCapturing = false;

        // Codes for low-level operating modes for the Socket
        byte[] _bIn = new byte[4] { 1, 0, 0, 0 };
        byte[] _bOut = new byte[4];

        public FormaAnalyzerPresenter(IAnalyzer view)
        {
            _view = view;
        }

        public void ApplicationStarted()
        {
            _view.ButtonStopEnabled = false;
            AddToList += OnAddToList;
            RemoveFromList += OnRemoveFromList;
            AddtoPortList += OnAddToPortList;
            RemoveFromPortlist += OnRemoveFromPortList;
            RemoveFromPortlistByKey += OnRemoveFromPortListByKey;
        }

        /// <summary>
        /// NewAddToListEvent method called when adding new item in to List
        /// </summary>
        /// <param name="item"></param>
        public void OnAddToList(ListViewItem item)
        {
            _view.ListReceivedPackets.Items.Add(item);
        }

        /// <summary>
        /// NewRemoveFromListEvent method callet to remove item from list
        /// </summary>
        public void OnRemoveFromList()
        {
            if (_view.ListReceivedPackets.Items[0] != null)
                _view.ListReceivedPackets.Items.Remove(_view.ListReceivedPackets.Items[0]);
        }

        public void OnAddToPortList(ListViewItem item)
        {
            _view.Invoke(()=>_view.ListOpenPorts.Items.Add(item));
        }

        public void OnRemoveFromPortList(ListViewItem item)
        {
            _view.Invoke(()=>_view.ListOpenPorts.Items.Remove(item));
        }

        public void OnRemoveFromPortListByKey(string key)
        {
            _view.Invoke(()=>_view.ListOpenPorts.Items.RemoveByKey(key));
        }

        /// <summary>
        /// This method called when start capturing packets
        /// 
        /// .assigning received packets buffer size
        /// .setting progress bar position from the beginning (0)
        /// .creting new socket object (raw socket)
        /// .binding socket to local IP address ,with no port No(this is raw socket)
        /// .setting socket option
        /// .seting low-level operating modes for the Socket
        /// .calling asynchronious operation (BeginReceive)
        /// 
        /// </summary>
        private void StartCapturing()
        {
            _view.ProgressBufferusage.Maximum = StartupInformation.PacketsToCapture;
            _view.ProgressBufferusage.Minimum = 0;


            try
            {
                _socket = new Socket(AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.IP);
                _socket.Bind(new IPEndPoint(StartupInformation.IP, 0));
                _socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.HeaderIncluded, true);
                _socket.IOControl(IOControlCode.ReceiveAll, _bIn, _bOut);

                thrStartCapturing = new Thread(StartReceiving);
                thrStartCapturing.Name = "Capture Thread";
                thrStartCapturing.Start();
            }
            catch (Exception ex)
            {
                _view.ShowDefaultErrorMessage(ex);
                _view.ButtonStartEnabled = true;
                _view.ButtonStopEnabled = false;
            }

        }


        private void StartReceiving()
        {
            while (!_stopCapturing)
            {
                int size = _socket.ReceiveBufferSize;
                int bytesReceived = _socket.Receive(_bBuffer, 0, _bBuffer.Length, SocketFlags.None);

                if (bytesReceived > 0)
                {
                    _decPackagesReceived++;
                    ConvertReceivedData(_bBuffer, bytesReceived);
                }
                Array.Clear(_bBuffer, 0, _bBuffer.Length);
            }
        }


        /// <summary>
        /// This method converts received data
        /// to packets information
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="iReceived"></param>
        public void ConvertReceivedData(byte[] buffer, int iReceived)
        {
            _view.Invoke(() => _view.SetTotalPacketReceivedText(_decPackagesReceived.ToString()));

            if (buffer.Length > 0 && iReceived != 0)
            {
                //getting IP header and data information
                PacketIP ipPacket = new PacketIP(buffer, iReceived);

                // this string used as a key in the buffer
                string strKey = _decPackagesReceived.ToString();   // Guid.NewGuid().ToString();

                //searching which uperlevel protocol contain IP packet
                switch (ipPacket.Protocol)
                {
                    case "TCP":
                        {
                           
                            //if IP contains TCP creating new TCPData object
                            //and assigning all TCP fields
                            PacketTcp tcpPacket = new PacketTcp(ipPacket.Data, ipPacket.MessageLength);

                            //creating new PacketInfo object to fill the buffer
                            PacketInfo pkgInfo = new PacketInfo(ipPacket, tcpPacket);

                            //_pkgBuffer.Add(strKey, pkgInfo)

                            //creating new list item to fill the list view control
                            ListViewItem item = new ListViewItem(_decPackagesReceived.ToString());
                            item.SubItems.Add(DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString());
                            item.SubItems.Add(ipPacket.SourceAddress.ToString());
                            item.SubItems.Add(tcpPacket.SourcePort);
                  
                            //loads netify html and inputs IP
                            string url = "https://www.netify.ai/resources/ips/" + ipPacket.DestinationAddress.ToString();
                           
                            WebClient client = new WebClient();
                            string htmlContent;
                            try
                            {
                                 htmlContent = client.DownloadString(url);
                            }
                           catch (Exception ex)
                            {
                                if (ex is WebException)
                                {
                                    item.SubItems.Add(ipPacket.DestinationAddress.ToString());
                                    goto beep;
                                }
                            }
                            //honestly, i have no fucking clue what i did here. it kind of works tho
                            string applicationinfo;
                            htmlContent = client.DownloadString(url);
                            int pFrom = htmlContent.IndexOf("<td>Application</td>") + "<td>Application</td>".Length;
                            int pTo = htmlContent.LastIndexOf("<td class=\"hidden-xs\">");
                            applicationinfo = htmlContent.Substring(pFrom, pTo - pFrom);                      
                            int pFrom3 = applicationinfo.IndexOf(@"applications/") + @"applications/".Length; 
                            int pTo3 = applicationinfo.LastIndexOf(@"""");
                            applicationinfo = applicationinfo.Substring(pFrom3, pTo3 - pFrom3);
                            applicationinfo =  applicationinfo.Substring(Math.Max(0, applicationinfo.Length - 10));
                            browser.browser1 = url;
                                //if the above code screws up, this sets the value to the IP
                                if (applicationinfo.Length > 10)
                                {
                                    item.SubItems.Add(ipPacket.DestinationAddress.ToString());
                                    if (ipPacket.DestinationAddress.ToString() != ip)

                                    {
                                        browser.browser1 = url;
                                    }

                                    ip = ipPacket.DestinationAddress.ToString();
                                    Thread.Sleep(500);
                                    goto beep;
                               
                                }

                            item.SubItems.Add(applicationinfo);                 
                            beep:
                            item.SubItems.Add(ipPacket.DestinationAddress.ToString());
                            item.SubItems.Add(tcpPacket.DestinationPort);
                            item.SubItems.Add(ipPacket.Protocol);
                            item.SubItems.Add(ipPacket.TotalLength);
                            item.SubItems.Add(strKey);

                            // checking if current buffer size is larger then maximum allowed buffer size
                            //then removing item from top of the list view control and also removing 
                            //the same item from buffer , then filling the list and buffer with new item
                            if (_pkgBuffer.Count == StartupInformation.PacketsToCapture && _view.ListReceivedPackets.Items.Count > 0)
                            {
                                _view.Invoke(() => _pkgBuffer.Remove(_view.ListReceivedPackets.Items[0].SubItems[8].Text));
                                _view.ListReceivedPackets.Invoke(RemoveFromList);

                                _pkgBuffer.Add(strKey, pkgInfo);
                                _view.ListReceivedPackets.Invoke(AddToList, new object[] { item });
                            }
                            else
                            {
                                _pkgBuffer.Add(strKey, pkgInfo);
                                _view.ListReceivedPackets.Invoke(AddToList, new object[] { item });
                            }

                        } break;
                        
                    // see TCP
                    case "UDP":
                        {
                            PacketUdp udpPacket = new PacketUdp(ipPacket.Data, ipPacket.MessageLength);
                            PacketInfo pkgInfo = new PacketInfo(ipPacket, udpPacket);

                            //_pkgBuffer.Add(strKey, pkgInfo);


                            ListViewItem item = new ListViewItem(_decPackagesReceived.ToString());
                            item.SubItems.Add(DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString());
                            item.SubItems.Add(ipPacket.SourceAddress.ToString());
                            item.SubItems.Add(udpPacket.SourcePort);
                            item.SubItems.Add(ipPacket.DestinationAddress.ToString());
                            item.SubItems.Add(udpPacket.DestinationPort);
                            item.SubItems.Add(ipPacket.Protocol);
                            item.SubItems.Add(ipPacket.TotalLength);
                            item.SubItems.Add(strKey);

                            if (_pkgBuffer.Count == StartupInformation.PacketsToCapture && _view.ListReceivedPackets.Items.Count > 0)
                            {
                                _view.Invoke(() => _pkgBuffer.Remove(_view.ListReceivedPackets.Items[0].SubItems[8].Text));
                                _view.ListReceivedPackets.Invoke(RemoveFromList);

                                _pkgBuffer.Add(strKey, pkgInfo);
                                _view.ListReceivedPackets.Invoke(AddToList, new object[] { item });
                            }
                            else
                            {
                                _pkgBuffer.Add(strKey, pkgInfo);
                                _view.ListReceivedPackets.Invoke(AddToList, new object[] { item });
                            }

                        } break;
                    //see TCP
                    case "ICMP":
                        {
                            PacketIcmp icmpPacket = new PacketIcmp(ipPacket.Data, ipPacket.MessageLength);
                            PacketInfo pkgInfo = new PacketInfo(ipPacket, icmpPacket);

                            //_pkgBuffer.Add(strKey, pkgInfo);

                            ListViewItem item = new ListViewItem(_decPackagesReceived.ToString());
                            item.SubItems.Add(DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString());
                            item.SubItems.Add(ipPacket.SourceAddress.ToString());
                            item.SubItems.Add("0");
                            item.SubItems.Add(ipPacket.DestinationAddress.ToString());
                            item.SubItems.Add("0");
                            item.SubItems.Add(ipPacket.Protocol);
                            item.SubItems.Add(ipPacket.TotalLength);
                            item.SubItems.Add("N/A");
                            item.SubItems.Add(strKey);

                            if (_pkgBuffer.Count == StartupInformation.PacketsToCapture && _view.ListReceivedPackets.Items.Count > 0)
                            {
                                _view.Invoke(() => _pkgBuffer.Remove(_view.ListReceivedPackets.Items[0].SubItems[8].Text));
                                _view.ListReceivedPackets.Invoke(RemoveFromList);

                                _view.ListReceivedPackets.Invoke(AddToList, new object[] { item });
                                _pkgBuffer.Add(strKey, pkgInfo);
                            }
                            else
                            {
                                _view.ListReceivedPackets.Invoke(AddToList, new object[] { item });
                                _pkgBuffer.Add(strKey, pkgInfo);
                            }

                        } break;
                    case "IGMP":
                        {
                            PacketIgmp igmpPacket = new PacketIgmp(ipPacket.Data, ipPacket.MessageLength);
                            PacketInfo pkgInfo = new PacketInfo(ipPacket, igmpPacket);

                            //_pkgBuffer.Add(strKey, pkgInfo);

                            ListViewItem item = new ListViewItem(_decPackagesReceived.ToString());
                            item.SubItems.Add(DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString());
                            item.SubItems.Add(ipPacket.SourceAddress.ToString());
                            item.SubItems.Add("0");
                            item.SubItems.Add(ipPacket.DestinationAddress.ToString());
                            item.SubItems.Add("0");
                            item.SubItems.Add(ipPacket.Protocol);
                            item.SubItems.Add(ipPacket.TotalLength);
                            item.SubItems.Add("N/A");
                            item.SubItems.Add(strKey);

                            if (_pkgBuffer.Count == StartupInformation.PacketsToCapture && _view.ListReceivedPackets.Items.Count > 0)
                            {
                                _view.Invoke(() => _pkgBuffer.Remove(_view.ListReceivedPackets.Items[0].SubItems[8].Text));
                                _view.ListReceivedPackets.Invoke(RemoveFromList);

                                _pkgBuffer.Add(strKey, pkgInfo);
                                _view.ListReceivedPackets.Invoke(AddToList, new object[] { item });
                            }
                            else
                            {
                                _pkgBuffer.Add(strKey, pkgInfo);
                                _view.ListReceivedPackets.Invoke(AddToList, new object[] { item });
                            }

                        } break;
                    case "Unknown":
                        {
                            PacketInfo pkgInfo = new PacketInfo(ipPacket);

                            _pkgBuffer.Add(strKey, pkgInfo);

                            ListViewItem item = new ListViewItem(_decPackagesReceived.ToString());
                            item.SubItems.Add(DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString());
                            item.SubItems.Add(ipPacket.SourceAddress.ToString());
                            item.SubItems.Add("0");
                            item.SubItems.Add(ipPacket.DestinationAddress.ToString());
                            item.SubItems.Add("0");
                            item.SubItems.Add(ipPacket.Protocol);
                            item.SubItems.Add(ipPacket.TotalLength);
                            item.SubItems.Add(strKey);

                            if (_pkgBuffer.Count > StartupInformation.PacketsToCapture && _view.ListReceivedPackets.Items.Count > 0)
                            {
                                _view.Invoke(() => _pkgBuffer.Remove(_view.ListReceivedPackets.Items[0].SubItems[8].Text));
                                _view.ListReceivedPackets.Invoke(RemoveFromList);

                                _view.ListReceivedPackets.Invoke(AddToList, new object[] { item });
                            }
                            else
                            {
                                _view.ListReceivedPackets.Invoke(AddToList, new object[] { item });
                            }

                        } break;
                }

                _view.Invoke(() => _view.ProgressBufferusage.Value = _pkgBuffer.Count);
                _view.Invoke(() => _view.SetBufferUsage(_pkgBuffer.Count.ToString()));
            }
        }


        /// <summary>
        /// This method is designed to crete detailed information tree 
        /// about selected packet
        /// </summary>
        public void CreateDetailedTree()
        {
            //getting the index of selected item
            System.Windows.Forms.ListView.SelectedIndexCollection indexCollection = _view.ListReceivedPackets.SelectedIndices;
            if (indexCollection.Count > 0)
            {
                int index = indexCollection[0];

                //getting the number of selected packet which is used as  an key in dictionary
                string strKey = _view.ListReceivedPackets.Items[index].SubItems[0].Text;

                PacketInfo pkgInfo = new PacketInfo();

                //trying to get data with specified key if this data exist 
                //creating a detailed tree
                if (_pkgBuffer.TryGetValue(strKey, out pkgInfo))
                {
                    switch (pkgInfo.IP.Protocol)
                    {
                        case "TCP":
                            {
                                _view.TreePackedDetails.Nodes.Clear();

                                TreeNode node = new TreeNode("IP");
                                node.ForeColor = Color.Green;

                                node.Nodes.Add("Protocol version: " + pkgInfo.IP.Version);
                                node.Nodes.Add("Header lenght: " + pkgInfo.IP.HeaderLength);
                                node.Nodes.Add("Type ofservice: " + pkgInfo.IP.TypeOfService);
                                node.Nodes.Add("Total lenght: " + pkgInfo.IP.TotalLength);
                                node.Nodes.Add("Identification No: " + pkgInfo.IP.Identification);
                                node.Nodes.Add("Flags: " + pkgInfo.IP.Flags);
                                node.Nodes.Add("Fragmentation offset: " + pkgInfo.IP.FragmentationOffset);
                                node.Nodes.Add("TTL: " + pkgInfo.IP.TTL);
                                node.Nodes.Add("Checksum: " + pkgInfo.IP.Checksum);
                                node.Nodes.Add(String.Format("Source address: {0}: {1}", pkgInfo.IP.SourceAddress, pkgInfo.TCP.SourcePort));
                                node.Nodes.Add(String.Format("Destination address: {0}: {1}", pkgInfo.IP.DestinationAddress, pkgInfo.TCP.DestinationPort));

                                TreeNode subNode = new TreeNode("TCP");
                                subNode.ForeColor = Color.Red;

                                subNode.Nodes.Add("Sequence No: " + pkgInfo.TCP.SequenceNumber);
                                subNode.Nodes.Add("Acknowledgement NO: " + pkgInfo.TCP.AcknowledgementNumber);
                                subNode.Nodes.Add("Header lenght: " + pkgInfo.TCP.HeaderLength);
                                subNode.Nodes.Add("Flags: " + pkgInfo.TCP.Flags);
                                subNode.Nodes.Add("Window size: " + pkgInfo.TCP.WindowSize);
                                subNode.Nodes.Add("Checksum: " + pkgInfo.TCP.Checksum);
                                subNode.Nodes.Add("Message lenght: " + pkgInfo.TCP.MessageLength);

                                node.Nodes.Add(subNode);
                                _view.TreePackedDetails.Nodes.Add(node);

                                _view.TreePackedDetails.ExpandAll();

                            } break;
                        case "UDP":
                            {
                                _view.TreePackedDetails.Nodes.Clear();

                                TreeNode node = new TreeNode("IP");
                                node.ForeColor = Color.Green;

                                node.Nodes.Add("Protocol version: " + pkgInfo.IP.Version);
                                node.Nodes.Add("Header lenght: " + pkgInfo.IP.HeaderLength);
                                node.Nodes.Add("Type ofservice: " + pkgInfo.IP.TypeOfService);
                                node.Nodes.Add("Total lenght: " + pkgInfo.IP.TotalLength);
                                node.Nodes.Add("Identification No: " + pkgInfo.IP.Identification);
                                node.Nodes.Add("Flags: " + pkgInfo.IP.Flags);
                                node.Nodes.Add("Fragmentation offset: " + pkgInfo.IP.FragmentationOffset);
                                node.Nodes.Add("TTL: " + pkgInfo.IP.TTL);
                                node.Nodes.Add("Checksum: " + pkgInfo.IP.Checksum);
                                node.Nodes.Add(String.Format("Source address: {0}: {1}", pkgInfo.IP.SourceAddress, pkgInfo.UDP.SourcePort));
                                node.Nodes.Add(String.Format("Destination address: {0}: {1}", pkgInfo.IP.DestinationAddress, pkgInfo.UDP.DestinationPort));

                                TreeNode subNode = new TreeNode("UDP");
                                subNode.ForeColor = Color.Blue;

                                subNode.Nodes.Add("Lenght: " + pkgInfo.UDP.Length);
                                subNode.Nodes.Add("Checksum: " + pkgInfo.UDP.Checksum);

                                node.Nodes.Add(subNode);
                                _view.TreePackedDetails.Nodes.Add(node);

                                _view.TreePackedDetails.ExpandAll();
                            } break;
                        case "ICMP":
                            {
                                _view.TreePackedDetails.Nodes.Clear();

                                TreeNode node = new TreeNode("IP");
                                node.ForeColor = Color.Green;

                                node.Nodes.Add("Protocol version: " + pkgInfo.IP.Version);
                                node.Nodes.Add("Header lenght: " + pkgInfo.IP.HeaderLength);
                                node.Nodes.Add("Type ofservice: " + pkgInfo.IP.TypeOfService);
                                node.Nodes.Add("Total lenght: " + pkgInfo.IP.TotalLength);
                                node.Nodes.Add("Identification No: " + pkgInfo.IP.Identification);
                                node.Nodes.Add("Flags: " + pkgInfo.IP.Flags);
                                node.Nodes.Add("Fragmentation offset: " + pkgInfo.IP.FragmentationOffset);
                                node.Nodes.Add("TTL: " + pkgInfo.IP.TTL);
                                node.Nodes.Add("Checksum: " + pkgInfo.IP.Checksum);
                                node.Nodes.Add("Source address: " + pkgInfo.IP.SourceAddress.ToString());
                                node.Nodes.Add("Destination address: " + pkgInfo.IP.DestinationAddress.ToString());

                                TreeNode subNode = new TreeNode("ICMP");
                                subNode.ForeColor = Color.Violet;

                                subNode.Nodes.Add("Type No: " + pkgInfo.ICMP.Type);
                                subNode.Nodes.Add("Code No: " + pkgInfo.ICMP.Code);
                                subNode.Nodes.Add("Checksum: " + pkgInfo.ICMP.Checksum);
                                subNode.Nodes.Add("Identifier: " + pkgInfo.ICMP.Identifier);
                                subNode.Nodes.Add("Sequence number: " + pkgInfo.ICMP.SequenceNUmber);
                                subNode.Nodes.Add("Address mask: " + pkgInfo.ICMP.AddressMask);

                                node.Nodes.Add(subNode);
                                _view.TreePackedDetails.Nodes.Add(node);

                                _view.TreePackedDetails.ExpandAll();
                            } break;
                        case "IGMP":
                            {
                                _view.TreePackedDetails.Nodes.Clear();

                                TreeNode node = new TreeNode("IP");
                                node.ForeColor = Color.Green;

                                node.Nodes.Add("Protocol version: " + pkgInfo.IP.Version);
                                node.Nodes.Add("Header lenght: " + pkgInfo.IP.HeaderLength);
                                node.Nodes.Add("Type ofservice: " + pkgInfo.IP.TypeOfService);
                                node.Nodes.Add("Total lenght: " + pkgInfo.IP.TotalLength);
                                node.Nodes.Add("Identification No: " + pkgInfo.IP.Identification);
                                node.Nodes.Add("Flags: " + pkgInfo.IP.Flags);
                                node.Nodes.Add("Fragmentation offset: " + pkgInfo.IP.FragmentationOffset);
                                node.Nodes.Add("TTL: " + pkgInfo.IP.TTL);
                                node.Nodes.Add("Checksum: " + pkgInfo.IP.Checksum);
                                node.Nodes.Add("Source address: " + pkgInfo.IP.SourceAddress.ToString());
                                node.Nodes.Add("Destination address: " + pkgInfo.IP.DestinationAddress.ToString());

                                TreeNode subnode = new TreeNode("IGMP");
                                subnode.ForeColor = Color.DarkSeaGreen;

                                subnode.Nodes.Add("Type: " + pkgInfo.IGMP.Type);
                                subnode.Nodes.Add("Max response time: " + pkgInfo.IGMP.MaxResponseTime);
                                subnode.Nodes.Add("Checksum: " + pkgInfo.IGMP.Checksum);
                                subnode.Nodes.Add("Group address: " + pkgInfo.IGMP.GropeAddress);

                                node.Nodes.Add(subnode);

                                _view.TreePackedDetails.Nodes.Add(node);
                                _view.TreePackedDetails.ExpandAll();
                            } break;
                    }
                }

            }
        }


        protected void OnTimerPortsElapsed(object source, EventArgs e)
        {
            ProcessOpenPorts();
        }

        private void InitTimer()
        {
            TimerPorts = new System.Windows.Forms.Timer();
            TimerPorts.Tick += new EventHandler(OnTimerPortsElapsed);
            TimerPorts.Interval = 1200;
            TimerPorts.Start();
        }

        public void StopTimer()
        {
            if (TimerPorts != null)
            {
                TimerPorts.Stop();
            }
        }


        private void ProcessOpenPorts()
        {
            ProcessTcpPorts();
            ProcessUdpPorts();
        }

        private void ProcessTcpPorts()
        {
            List<TcpRecordPid> tcpPortsCollection = null;

            if ((tcpPortsCollection = NetworkStatisticData.GetAllTcpConnections()) != null)
            {
                foreach (TcpRecordPid record in tcpPortsCollection)
                {
                    if(!(_view.ListOpenPorts.Items.ContainsKey(record.GetHashCode().ToString())))
                        OnAddToPortList(InitTcpItem(record));
                }

                foreach (ListViewItem item in _view.ListOpenPorts.Items)
                {
                    if (item.SubItems[1].Text == "UDP")
                        continue;

                    if (tcpPortsCollection.Where(r => r.GetHashCode() == int.Parse(item.Name) && r.Protocol.Equals("TCP")).SingleOrDefault() == null)
                        OnRemoveFromPortList(item);
                }
            }
        }

        private void ProcessUdpPorts()
        {
            List<UdpRecordPid> udpPortsCollection = null;

            if ((udpPortsCollection = NetworkStatisticData.GetAllUdpConnections()) != null)
            {
                foreach (UdpRecordPid record in udpPortsCollection)
                {
                    if (!(_view.ListOpenPorts.Items.ContainsKey(record.GetHashCode().ToString())))
                        OnAddToPortList(InitUdpItem(record));
                }

                foreach (ListViewItem item in _view.ListOpenPorts.Items)
                {
                    if (item.SubItems[1].Text == "TCP")
                        continue;

                    if (udpPortsCollection.Where(r => r.GetHashCode() == int.Parse(item.Name) && r.Protocol.Equals("UDP")).SingleOrDefault() == null)
                        OnRemoveFromPortList(item);
                }
            }
        }

        private ListViewItem InitTcpItem(TcpRecordPid tcprecord)
        {
            if (tcprecord == null)
                return null;

            ListViewItem item = new ListViewItem(tcprecord.LocalPort.ToString());
            item.SubItems.Add(tcprecord.Protocol);
            item.SubItems.Add(tcprecord.LocalAddress.ToString());
            item.SubItems.Add(tcprecord.RemoteAddress.ToString());
            item.SubItems.Add(tcprecord.RemotePort.ToString());
            item.SubItems.Add(tcprecord.State.ToString());
            item.SubItems.Add(tcprecord.PID.ToString());
            item.SubItems.Add(tcprecord.ProcessName);
            item.Name = tcprecord.GetHashCode().ToString();

            return item;
        }

        private ListViewItem InitUdpItem(UdpRecordPid udprecord)
        {
            if (udprecord == null)
                return null;

            ListViewItem item = new ListViewItem(udprecord.LocalPort.ToString());
            item.SubItems.Add(udprecord.Protocol);
            item.SubItems.Add(udprecord.LocalAddress.ToString());
            item.SubItems.Add("0.0.0.0");
            item.SubItems.Add("0.0.0.0");
            item.SubItems.Add("0");
            item.SubItems.Add(udprecord.PID.ToString());
            item.SubItems.Add(udprecord.ProcessName);
            item.Name = udprecord.GetHashCode().ToString();

            return item;
        }


        public void StartClicked()
        {
            StartupInformation = new StartupInfo();

            //creating Start dialog to set startup data
            using (FrmStartupInfo start = new FrmStartupInfo(_view))
            {
                start.ShowDialog();

                if (StartupInformation != null)
                {
                    if (StartupInformation.IP != null && StartupInformation.PacketsToCapture > 0)
                    {
                        _pkgBuffer = new Dictionary<string, PacketInfo>();

                        _localIP = StartupInformation.IP;
                        if (_stopCapturing)
                            _stopCapturing = false;

                        if (_pkgBuffer.Count > 0)
                            _pkgBuffer.Clear();

                        if (_view.ProgressBufferusage.Value > 0)
                            _view.ProgressBufferusage.Value = 0;

                        if (_view.ListReceivedPackets.Items.Count > 0)
                            _view.ListReceivedPackets.Items.Clear();

                        if (_view.TreePackedDetails.Nodes.Count > 0)
                            _view.TreePackedDetails.Nodes.Clear();

                        _view.SetTotalPacketReceivedText("0");
                        _decPackagesReceived = 0;

                        _view.ButtonStopEnabled = true;
                        _view.ButtonStartEnabled = false;
                        _view.SetReadyText("Running...");
                        _view.SetTotalPacketReceivedText("0");

                        InitTimer();
                        StartCapturing();
                    }
                }
            }
        }


        public void StopClicked()
        {
            _stopCapturing = true;

            _view.ButtonStopEnabled = false;
            _view.ButtonStartEnabled = true;
            _view.SetReadyText("Stopped");
            _decPackagesReceived = 0;
            StopTimer();

            if (thrStartCapturing.IsAlive)
                thrStartCapturing.Abort();

            _socket.Shutdown(SocketShutdown.Both);
            _socket.Close();
        }

        public void ClearAllClicked()
        {
            if (_view.ListReceivedPackets.Items.Count > 0)
                _view.ListReceivedPackets.Items.Clear();

            if (_view.TreePackedDetails.Nodes.Count > 0)
                _view.TreePackedDetails.Nodes.Clear();

            if (_view.ListOpenPorts.Items.Count > 0)
                _view.ListOpenPorts.Items.Clear();

            if (_pkgBuffer.Count > 0)
            {
                _pkgBuffer.Clear();
                _view.ProgressBufferusage.Value = 0;
                _view.Invoke(() => _view.SetBufferUsage("0"));
                _view.Invoke(() => _view.SetTotalPacketReceivedText("0"));
            }
        }

        public void TopMostClicked()
        {
            
            if (_view.TopMostChecked)
            {
                _view.TopMostChecked = false;
                _view.FormShowAsTopMost = false;
            }
            else if (!_view.TopMostChecked)
            {
                _view.TopMostChecked = true;
                _view.FormShowAsTopMost = true;
            }
        }
    }
}
