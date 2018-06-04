using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace BDL_System_Pro
{
	public partial class fiche_inter : Form
	{
		public fiche_inter()
		{
			InitializeComponent();
		}

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			if ( comboBox1.SelectedItem.ToString() != " Ajouter")
			{
				this.txt_Nserie.Visible = false;
				this.txt_Type.Visible = false;
				this.Nserie.Visible = false;
				this.label5.Visible = false;
			}
			else
			{
				this.txt_Nserie.Visible = true;
				this.txt_Type.Visible = true;
				this.Nserie.Visible = true;
				this.label5.Visible = true;
			}
		}

		private void fiche_inter_Load(object sender, EventArgs e)
		{
			try
			{
				MySqlConnection connection = new MySqlConnection("server=bj881856-001.dbaas.ovh.net;user id=bdl; database=Bdl; port=35312; password=Bdl69100");
				string selectQuery = "SELECT Nserie,Categorie from Parc where N_client = '"+ client.Nom_client + "'";

			}
			catch(Exception ex)
			{

			}
		}
	}


}
