///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///
/// UDP Chat Client
/// Christofer Tavano
/// Eric Kumbata
/// 
/// Created: Oct 2020
/// 
/// About : Udp Chat client that sends and recieves messages to/from all users in the lobby.
///         Custom skin made to look like the client was made in the 90's
///         
/// Updates: 
///         -Chris   : Built user interface and design work
///         -Chris   : Created event handlers
///         -Chris   : Receivingloop and Listening Method
///         -Eric    : Wrote Sending Method
///         -Eric    : Wrote filter Methods
///         -Eric    : Wrote filter Forms code to display based on filter criteria
///         
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Data.SqlTypes;
using System.Globalization;
using System.Linq.Expressions;

namespace UDPTapChat
{
    public partial class Form1 : Form
    {
        //class level udp objects
        //creates a udp endpoint for sending info to a destination
        private readonly UdpClient clientSocket = new UdpClient();

        //recieves information from port 1666
        private readonly UdpClient listeningSocket = new UdpClient(1666);

        //global list to keep track of all IP's
        readonly List<string> lstIP = new List<string>();

        //Collection that contains all the information we need from every ip
        readonly Dictionary<IPAddress, IpInfo> _UserInfo = new Dictionary<IPAddress, IpInfo>();

        public Form1(){
            InitializeComponent();
            UI_btnSend.Click += UI_btnSend_Click;
            UI_CloseProgram.Click += UI_CloseProgram_Click;
            UI_btnFilterIP.Click += UI_btnFilterIP_Click;
            UI_btnFilterName.Click += UI_btnFilterName_Click;
            KeyDown += Form1_KeyDown;
            lstIP.Add("127.0.0.1");
            UI_LbxIP.Items.Add("List of IP's");
            Receivingloop();
        }

        /// <summary>
        /// Event handler for the close button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UI_CloseProgram_Click(object sender, EventArgs e){
            Application.Exit();
        }

        /// <summary>
        /// Event Handler for pressing the enter key on a keyboard to send message
        /// and the Escape key to exit the program
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_KeyDown(object sender, KeyEventArgs e){
            if (!e.Handled) {
                switch (e.KeyCode) {
                    case Keys.Escape:
                        Application.Exit();
                        break;

                    case Keys.Enter:
                        UI_btnSend_Click(this, e);
                        _txtMessage.Clear();
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        break;

                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Method to send a message to the list of ip's saved in our dictionary
        /// without it blocking the functionality of the rest of the program.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async private void UI_btnSend_Click(object sender, EventArgs e){
            //get the message as a string, making sure we send an actual message
            //if message is empty, return
            string message = _txtMessage.Text;

            if (string.IsNullOrEmpty(message))
                return;

            //get the nickaname as a string
            string nickname = _txtNickname.Text;

            //add separators to the nickname
            nickname = "<" + nickname + ">";
            
            //concat the strings together and send to a byte array
            byte[] sendData = Encoding.Unicode.GetBytes(nickname.Concat(message).ToArray());

            //attempt to send what is in the text box
            try{
                //add ip address to list if it doesnt already contain it
                //make sure there is no empty ip's being added to the list
                if (!lstIP.Contains(_txtIP.Text) && _txtIP.Text != "" && _txtIP.Text.Length > 0)
                    lstIP.Add(_txtIP.Text);

                //create ipendpoint obj to read from any source
                //IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 1666);

                //send message with value in text box to all saved IP address
                foreach (string item in lstIP){
                    //get the number of bytest sent from the client socket. also send the data to all saved IP's with port 1666
                    var numBytesSent = await clientSocket.SendAsync(sendData, sendData.Length, item, 1666);
                    Console.WriteLine(numBytesSent);
                }

                //clear the message box
                _txtMessage.Clear();
            }
            catch (Exception ex){
                Console.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// Method to start Listening in a different thread without it blocking
        /// the functionality of the rest of the program.
        /// </summary>
        async private void Receivingloop() {
            UdpReceiveResult UdpRR;// = new UdpReceiveResult();
            //Extract the message from the returned information.
            string message;
            
            while (true) {
                try {
                    //Listen for an incoming message
                    UdpRR = await listeningSocket.ReceiveAsync();
                    //Extract the message from the returned information.
                    message = Listening(UdpRR);
                    //Send the information to my Listening method to extract the information we received
                    lbxDisplay.Items.Add(message);
                }
                catch (Exception e){
                    Console.WriteLine(e.ToString());
                }

                //Refresh the list box for displaying Ip Addresses
                UI_LbxIP.Items.Clear();
                UI_LbxIP.Items.Add("List of IP's");

                foreach (var item in _UserInfo)
                    UI_LbxIP.Items.Add(item.Value.IPAddress);
            }
        }

        /// <summary>
        /// Method for extracting information coming into the chat client
        /// </summary>
        /// <returns>The text received</returns>
        private string Listening(UdpReceiveResult UdpRR){
            //Creates an IPEndPoint to record the IP Address and port number of the sender.
            //The IPEndPoint will allow you to read datagrams sent from any source.
            //IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
            Regex reg = new Regex("(?'nickname'<.+>)?(?'message'.+)");
            string returnData = "";

            try {
                //The listening socket will stop the task here until a message is received on this socket.
                byte[] receiveBytes = UdpRR.Buffer;  //listeningSocket.Receive(ref RemoteIpEndPoint);

                //Get the current time stamp
                DateTime dt = DateTime.Now;

                //Turn the list of bytes back into a string that we can return
                returnData = Encoding.Unicode.GetString(receiveBytes);

                //If the IpAddress doesn't exist in the global list, then add it for outgoing messages
                if (!lstIP.Contains(UdpRR.RemoteEndPoint.Address.ToString()))
                        lstIP.Add(UdpRR.RemoteEndPoint.Address.ToString());

                if (reg.IsMatch(returnData)) {
                    MatchCollection matches = reg.Matches(returnData);

                    foreach (Match match in matches) {
                        //If the IpAddress doesn't exist in the database, create a spot for it
                        if (!_UserInfo.ContainsKey(UdpRR.RemoteEndPoint.Address)) {
                            _UserInfo.Add(UdpRR.RemoteEndPoint.Address, new IpInfo(UdpRR.RemoteEndPoint.Address));
                        }
                        //If the regex extraction for a nickname was successful and the associated IpAddress hasn't been named yet
                        //then set it
                        if (match.Groups["nickname"].Success && !_UserInfo[UdpRR.RemoteEndPoint.Address].NicknameSet) {
                            _UserInfo[UdpRR.RemoteEndPoint.Address].Nickname = match.Groups["nickname"].ToString().Split('<', '>')[1];
                            _UserInfo[UdpRR.RemoteEndPoint.Address].NicknameSet = true;
                        }
                        //If the regex extraction for a message was successful then add it to our chat history from that IpAddress
                        if (match.Groups["message"].Success) {
                            if (match.Groups["message"].Value.StartsWith("<")) {
                                _UserInfo[UdpRR.RemoteEndPoint.Address].Messages.Add(dt.ToString("d", DateTimeFormatInfo.InvariantInfo) + ": " + match.Groups["message"].ToString().Remove(0,2));
                            }
                            else
                                _UserInfo[UdpRR.RemoteEndPoint.Address].Messages.Add(dt.ToString("d", DateTimeFormatInfo.InvariantInfo) + ": " + match.Groups["message"].ToString());
                        }
                    }
                }

                //If there is a nickname associated with the IpAddress, then print the Nickname instead of the Ip otherwise just use the ip
                if (_UserInfo[UdpRR.RemoteEndPoint.Address].NicknameSet)
                    returnData = $"[{_UserInfo[UdpRR.RemoteEndPoint.Address].Nickname}] {_UserInfo[UdpRR.RemoteEndPoint.Address].Messages.Last()}";
                else
                    returnData = $"[{_UserInfo[UdpRR.RemoteEndPoint.Address].IPAddress}] {_UserInfo[UdpRR.RemoteEndPoint.Address].Messages.Last()}";
            }

            catch (Exception e) {
                Console.WriteLine(e.ToString());
            }

            return returnData;
        }

        private void UI_btnFilterIP_Click(object sender, EventArgs e){
            //try to parse the string from the text box to an ip
            if(IPAddress.TryParse(_txtIP.Text, out IPAddress ip)){
                //check to see if our dictionary contains that ip
                if (_UserInfo.ContainsKey(ip)){
                    //open up a new dialog
                    FilteredMessages fm = new FilteredMessages();
                    //pass in the list of messages from that key 
                    fm.FilterMessages = _UserInfo[ip].Messages;
                    //show the dialog
                    fm.Show();
                }
                else
                    MessageBox.Show("IP address not found!", "No IP Found", MessageBoxButtons.OK);
            }
            else
                MessageBox.Show("Please enter a valid IP!", "Bad Input", MessageBoxButtons.OK);
          
        }

        private void UI_btnFilterName_Click(object sender, EventArgs e){
            //get the name we want to filter
            string name = _txtNickname.Text;

            //get the user messages for that specific nick name
            var usermessages = from IpInfo in _UserInfo
                               where IpInfo.Value.Nickname == name
                               select IpInfo into x
                               select x.Value.Messages;

            //if the query found something to display
            //check if the usermessages variable got populated
            if (usermessages.Count() >= 1){
                //open up a new dialog
                FilteredMessages fm = new FilteredMessages();
                //pass in the list of messages from that key 
                fm.FilterName = usermessages;
                //show the dialog
                fm.Show();
            }
            else
                MessageBox.Show("No messages found for that nickname", "No Nickname", MessageBoxButtons.OK);

        }
    }
}
