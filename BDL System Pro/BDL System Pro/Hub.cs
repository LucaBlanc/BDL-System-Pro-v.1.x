using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BDL_System_Pro
{
	public partial class Hub : Form
	{
		public Hub()
		{
			InitializeComponent();
		}

        private void Button_Logiciel_Click(object sender, EventArgs e)
        {

        }

        private void Hub_Load(object sender, EventArgs e)
        {

        }

       

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

		private void Button_Client_Click(object sender, EventArgs e)
		{
			clients client = new clients();
			client.ShowDialog();
		}
	}
}
