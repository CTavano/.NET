using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UDPTapChat
{
    public partial class FilteredMessages : Form
    {
        public FilteredMessages()
        {
            InitializeComponent();
            UI_CloseForm.Click += UI_CloseForm_Click;
        }

        private void UI_CloseForm_Click(object sender, EventArgs e){
            FilteredMessages.ActiveForm.Close();
        }

        public List<string> FilterMessages
        {
            //set prop
            set
            {
                //iterates through messages 
                foreach (var item in value)
                {
                    //displays all messages in the listbox
                    lbxFiltered.Items.Add(item);                
                }

            }

            //why do i need this?????
            get
            {
                return new List<string>();
            }
        }

        public IEnumerable<List<string>> FilterName
        {
            set
            {
                //iterates through messages 
                foreach (var item in value)
                {
                    //displays all messages in the listbox
                    item.ForEach(x => lbxFiltered.Items.Add(x));
                }
            }

            //why do i need this?????
            get
            {
                return null;
            }
        }
    }
}
