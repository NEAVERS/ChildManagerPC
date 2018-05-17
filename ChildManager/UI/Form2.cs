using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ChildManager.UI
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            long ret = 0;
            string fid = fidtxt.Text;
            string cmsgid = "";
            string getcmsg = "";
            MQDLL.MQFuntion MQManagment = new MQDLL.MQFuntion();
            ret = MQManagment.connectMQ();
            ret = MQManagment.putMsg(fid, putT.Text, ref cmsgid);
            ret = MQManagment.getMsgById(fid, cmsgid, 60000, ref getcmsg);
            putTId.Text = getcmsg;
            MQManagment.disconnectMQ();
        }
    }
}
