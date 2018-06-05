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
		MySqlConnection connection = new MySqlConnection("server=bj881856-001.dbaas.ovh.net;user id=bdl; database=Bdl; port=35312; password=Bdl69100");
		MySqlCommand command;
		MySqlDataAdapter adapter;
		DataTable table;
		int id_inter = 0;
		string categorie = "";
		string model = "";

		public fiche_inter()
		{
			InitializeComponent();
			Getbdd();
		}

		private DataTable Getbdd()
		{
			DataTable dtbdd = new DataTable();

			string connString = ConfigurationManager.ConnectionStrings["dbx"].ConnectionString;

			using (MySqlConnection con = new MySqlConnection(connString))
			{
				using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM interventions where N_client = '" + client.Nom_client + "'", con))

				{
					con.Open();

					MySqlDataReader reader = cmd.ExecuteReader();

					dtbdd.Load(reader);

				}
			}

			return dtbdd;
		}

		
		private void fiche_inter_Load(object sender, EventArgs e)
		{
			BdlDataGridView1.DataSource = Getbdd();
			try
			{
				MySqlConnection connection = new MySqlConnection("server=bj881856-001.dbaas.ovh.net;user id=bdl; database=Bdl; port=35312; password=Bdl69100");
				string selectQuery = "SELECT * from Parc where N_client = '"+ client.Nom_client + "'";
				connection.Open();
				MySqlCommand command = new MySqlCommand(selectQuery, connection);

				MySqlDataReader reader = command.ExecuteReader();

				while (reader.Read())
				{
					comboBox1.Items.Add(reader.GetString("Model"));
				}

			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			label1.Text = client.Nom_client;
		}

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{

			if (comboBox1.SelectedItem.ToString() != "")
			{
				this.txt_Type.Visible = false;
				model = comboBox1.SelectedItem.ToString();
				this.label5.Visible = false;
				txt_Type.Text = "";
				try
				{
					MySqlConnection connection = new MySqlConnection("server=bj881856-001.dbaas.ovh.net;user id=bdl; database=Bdl; port=35312; password=Bdl69100");
					string selectQuery = "SELECT Nserie from Parc where Model = '" + model + "' and N_client = '" + client.Nom_client + "'";
					connection.Open();
					MySqlCommand command = new MySqlCommand(selectQuery, connection);

					MySqlDataReader reader = command.ExecuteReader();

					while (reader.Read())
					{
						txt_Nserie.Text = (reader.GetString("Nserie"));
					}

				}
				catch
				{

				}
			}
			else
			{
				this.txt_Type.Visible = true;
				this.label5.Visible = true;
				txt_Nserie.Text = "";
			}
		}

		private void DisplayData()
		{
			//connection.Open();
			DataTable dt = new DataTable();
			adapter = new MySqlDataAdapter("SELECT * FROM interventions where N_client = '" + client.Nom_client + "'", connection);
			adapter.Fill(dt);
			BdlDataGridView1.DataSource = dt;
			connection.Close();

		}

		private void BdlDataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			textBox2.Text = BdlDataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
			id_inter = Convert.ToInt32(BdlDataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
			this.button1.Visible = true;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (client.code_client != 0)
			{
				this.btn_Delete.Visible = true;
				this.pictureBox5.Visible = true;
				this.label6.Visible = true;
				this.textBox2.Visible = true;
				this.button2.Visible = true;
				this.pictureBox9.Visible = true;
				this.label11.Visible = true;
			}
			else
			{
				MessageBox.Show("Selectionner un client !");
			}
		}

		private void btn_Delete_Click(object sender, EventArgs e)
		{
			connection.Open();
			command = new MySqlCommand("DELETE FROM interventions WHERE code_inter = '" + id_inter + "'", connection);
			command.ExecuteReader();
			connection.Close();
			DisplayData();
			this.button1.Visible = false;
			this.btn_Delete.Visible = false;
			this.pictureBox5.Visible = false;
			this.label6.Visible = false;
			this.textBox2.Visible = false;
			this.button2.Visible = false;
			this.btn_Delete.Visible = false;
			this.pictureBox9.Visible = false;
			this.label11.Visible = false;
		}

		private void ClearData()
		{
			txt_Date.Text = "";
			txt_Temps.Text = "";
			comboBox1.SelectedItem = "";
			txt_Nserie.Text = "";
			txt_Type.Text = "";
			txt_Panne.Text = "";
			txt_Com.Text = "";
			txt_Sign.Text = "";
		}

		private void btn_Update_Click(object sender, EventArgs e)
		{
			if(txt_Type.Visible == true)
			{
				categorie = txt_Type.Text;
			}
			else
			{
				categorie = Convert.ToString(comboBox1.SelectedItem);
			}


			if (txt_Date.Text != "")
			{
				command = new MySqlCommand("insert into interventions (Date,Temps,Nserie,Categorie,Panne,Commentaire,Signature,N_client) values (@date,@temps,@serie,@categorie,@panne,@com,@sign,@client)", connection);
				connection.Open();
				command.Parameters.AddWithValue("@date", txt_Date.Text);
				command.Parameters.AddWithValue("@temps", txt_Temps.Text);
				command.Parameters.AddWithValue("@categorie", categorie);
				command.Parameters.AddWithValue("@serie", txt_Nserie.Text);
				command.Parameters.AddWithValue("@panne", txt_Panne.Text);
				command.Parameters.AddWithValue("@com", txt_Com.Text);
				command.Parameters.AddWithValue("@sign", txt_Sign.Text);
				command.Parameters.AddWithValue("@client", client.Nom_client);


				command.ExecuteNonQuery();
				connection.Close();
				DisplayData();

			}
			else
			{
				MessageBox.Show("Replir le champ 'Date au format (aaaa-mm-jj)'");
			}

		}

		private void button2_Click(object sender, EventArgs e)
		{
			this.button1.Visible = false;
			this.btn_Delete.Visible = false;
			this.pictureBox5.Visible = false;
			this.label6.Visible = false;
			this.textBox2.Visible = false;
			this.button2.Visible = false;
			this.btn_Delete.Visible = false;
			this.pictureBox9.Visible = false;
			this.label11.Visible = false;
		}
	}


}
