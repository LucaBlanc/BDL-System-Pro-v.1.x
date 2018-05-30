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
		int code_client = 0;


		public clients()
		{
			InitializeComponent();
			Getbdd();
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
				using (MySqlCommand cmd = new MySqlCommand("SELECT code_client,Nom,Maintenance,DateMaintenance,Adresse,Cp,Ville,Gsm,Fixe,Responsable,Mail,Siret,Web FROM clients", con))

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

		public void searchData(string valueToSearch)
		{
			string query = "SELECT code_client,Nom,Maintenance,DateMaintenance,Adresse,Cp,Ville,Gsm,Fixe,Responsable,Mail,Siret,Web FROM clients WHERE CONCAT(" + comboBox1.SelectedItem + ") like '%" + valueToSearch + "%'";
			command = new MySqlCommand(query, connection);
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

		private void btn_Insert_Click_1(object sender, EventArgs e)
		{
			if (txt_Name.Text != "" && txt_Ville.Text != "")
			{
				command = new MySqlCommand("insert into clients(Nom,Maintenance,DateMaintenance,Adresse,Cp,Ville,Gsm,Fixe,Responsable,Mail,Siret,Web) values (@nom,@maint,@date,@adr,@cp,@ville,@gsm,@fixe,@resp,@mail,@siret,@web)", connection);
				connection.Open();
				command.Parameters.AddWithValue("@nom", txt_Name.Text);
				command.Parameters.AddWithValue("@maint", txt_Maint.Text);
				command.Parameters.AddWithValue("@date", txt_Date.Text);
				command.Parameters.AddWithValue("@adr", txt_Adr.Text);
				command.Parameters.AddWithValue("@cp", txt_CP.Text);
				command.Parameters.AddWithValue("@ville", txt_Ville.Text);
				command.Parameters.AddWithValue("@gsm", txt_Gsm.Text);
				command.Parameters.AddWithValue("@fixe", txt_Fix.Text);
				command.Parameters.AddWithValue("@resp", txt_Resp.Text);
				command.Parameters.AddWithValue("@mail", txt_Mail.Text);
				command.Parameters.AddWithValue("@siret", txt_Siret.Text);
				command.Parameters.AddWithValue("@web", txt_Web.Text);



				command.ExecuteNonQuery();
				connection.Close();
				MessageBox.Show("Ajouter avec succes");
				DisplayData();
				ClearData();

			}
			else
			{
				MessageBox.Show("Remplir les champs");
			}

		}

		private void DisplayData()
		{
			//connection.Open();
			DataTable dt = new DataTable();
			adapter = new MySqlDataAdapter("Select * From clients", connection);
			adapter.Fill(dt);
			BdlDataGridView1.DataSource = dt;
			connection.Close();

		}

		private void ClearData()
		{
			txt_Name.Text = "";
			txt_Adr.Text = "";
			txt_Date.Text = "";
			txt_Maint.Text = "";
			txt_CP.Text = "";
			txt_Ville.Text = "";
			txt_Siret.Text = "";
			txt_Web.Text = "";
			txt_Resp.Text = "";
			txt_Gsm.Text = "";
			txt_Fix.Text = "";
			txt_Mail.Text = "";
			code_client = 0;

		}


		private void btn_Update_Click_1(object sender, EventArgs e)
		{
			if (txt_Ville.Text != "" && txt_Name.Text != "")
			{
				command = new MySqlCommand("update clients set Nom = @nom , Maintenance = @maint, DateMaintenance = @date, Adresse = @adr,Cp = @cp,Ville = @ville, Gsm = @gsm, Fixe = @fixe,Responsable = @resp , Mail = @mail, Siret = @siret, Web = @web where code_client  = @id", connection);
				connection.Open();
				command.Parameters.AddWithValue("@id", code_client);
				command.Parameters.AddWithValue("@nom", txt_Name.Text);
				command.Parameters.AddWithValue("@maint", txt_Maint.Text);
				command.Parameters.AddWithValue("@date", txt_Date.Text);
				command.Parameters.AddWithValue("@adr", txt_Adr.Text);
				command.Parameters.AddWithValue("@cp", txt_CP.Text);
				command.Parameters.AddWithValue("@ville", txt_Ville.Text);
				command.Parameters.AddWithValue("@gsm", txt_Gsm.Text);
				command.Parameters.AddWithValue("@fixe", txt_Fix.Text);
				command.Parameters.AddWithValue("@resp", txt_Resp.Text);
				command.Parameters.AddWithValue("@mail", txt_Mail.Text);
				command.Parameters.AddWithValue("@siret", txt_Siret.Text);
				command.Parameters.AddWithValue("@web", txt_Web.Text);
				command.ExecuteNonQuery();
				MessageBox.Show("Mise a jour réalisé avec succès");
				DisplayData();
				ClearData();
			}
			else
			{
				MessageBox.Show("Selectionner une ligne !");
			}

		}

		private void btn_Delete_Click_1(object sender, EventArgs e)
		{
			if (code_client != 0)
			{
				connection.Open();
				command = new MySqlCommand("DELETE FROM clients WHERE code_client = '" + code_client + "'", connection);
				command.ExecuteReader();
				//command.Parameters.AddWithValue("@id", code_client);
				connection.Close();
				MessageBox.Show("Ligne supprimé avec succès");
				DisplayData();
				ClearData();


			}
			else
			{
				MessageBox.Show("Selectionner une ligne a supprimer");
			}
		}


		private void button3_Click_1(object sender, EventArgs e)
		{
			this.btn_Delete.Visible = true;
		}

		private void BdlDataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			connection.Open();
			code_client = Convert.ToInt32(BdlDataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
			txt_Name.Text = BdlDataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
			textBox1.Text = BdlDataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
			this.textBox1.Visible = true;
			txt_Maint.Text = BdlDataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
			txt_Date.Text = BdlDataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
			txt_Adr.Text = BdlDataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
			txt_CP.Text = BdlDataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
			txt_Ville.Text = BdlDataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
			txt_Gsm.Text = BdlDataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
			txt_Fix.Text = BdlDataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
			txt_Resp.Text = BdlDataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
			txt_Mail.Text = BdlDataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString();
			txt_Siret.Text = BdlDataGridView1.Rows[e.RowIndex].Cells[11].Value.ToString();
			txt_Web.Text = BdlDataGridView1.Rows[e.RowIndex].Cells[12].Value.ToString();

			connection.Close();
		}

	}

}
