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
    

	public partial class clients : Form
	{
        MySqlConnection connection = new MySqlConnection("server=bj881856-001.dbaas.ovh.net;user id=bdl; database=Bdl; port=35312; password=Bdl69100");
        MySqlCommand command;
        MySqlDataAdapter adapter;
        DataTable table;

        public clients()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			BdlDataGridView1.DataSource = Getbdd();
            searchData("");
			
		}

		private DataTable Getbdd()
		{
			DataTable dtbdd = new DataTable();

			string connString = ConfigurationManager.ConnectionStrings["dbx"].ConnectionString;

			using (MySqlConnection con = new MySqlConnection(connString))
			{
				using (MySqlCommand cmd = new MySqlCommand("SELECT Nom,Maintenance,DateMaintenance,Adresse,Cp,Ville,Gsm,Fixe,Responsable,Mail,Siret FROM clients", con))

				{
					con.Open();

					MySqlDataReader reader = cmd.ExecuteReader();

					dtbdd.Load(reader);

				}
			}

			return dtbdd;
		}

		private void label2_Click(object sender, EventArgs e)
		{

		}

		private void textBox1_TextChanged(object sender, EventArgs e)
		{
		
		}

        public void searchData (string valueToSearch)
        {
            string query = "SELECT Nom,Maintenance,DateMaintenance,Adresse,Cp,Ville,Gsm,Fixe,Responsable,Mail,Siret FROM clients WHERE CONCAT(" + comboBox1.SelectedItem +") like '%" + valueToSearch +"%'";
            command = new MySqlCommand(query,connection);
            adapter = new MySqlDataAdapter(command);
            table = new DataTable();
            adapter.Fill(table);
            BdlDataGridView1.DataSource = table;
        }

        private void BTN_Rech_Click(object sender, EventArgs e)
        {
            string valueToSearch = textBoxRech.Text.ToString();
            searchData(valueToSearch);
        }

		private void textBoxRech_TextChanged(object sender, EventArgs e)
		{

		}

		private void button1_Click(object sender, EventArgs e)
		{
			textBox1.Text = Convert.ToString(comboBox1.Items);
		}

	}
}
