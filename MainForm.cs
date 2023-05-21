using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diplom
{
    public partial class MainForm : Form
    {
        Test1Vopros1 test1Vopros1 = new Test1Vopros1();
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            test1Vopros1.Show();
            this.Visible = false;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
