using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ChildManager.UI.jibenluru
{
    public partial class Testtongji519 : Form
    {
        public Testtongji519()
        {
            InitializeComponent();
            WomenInfo wm = new WomenInfo();
            wm.cd_id = 26;//26 男   27女
            tongji_BMI519 tj = new tongji_BMI519(wm);
            this.panel1.Controls.Add(tj);
        }
    }
}
