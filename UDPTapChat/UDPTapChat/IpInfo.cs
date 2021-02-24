using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UDPTapChat
{
    class IpInfo {
        public IPAddress IPAddress { get; private set; }
        public string Nickname { get; set; }
        public bool NicknameSet { get; set; }
        public List<string> Messages { get; set; }

        /// <summary>
        /// Simple constructor that contains all the information
        /// pertaining to each unique user.
        /// </summary>
        /// <param name="iP">IPAddress of the user</param>
        public IpInfo(IPAddress iP) {
            IPAddress = iP;
            Nickname = "";
            NicknameSet = false;
            Messages = new List<string>();
        }
    }
}