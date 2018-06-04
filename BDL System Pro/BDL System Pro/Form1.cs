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
			Getbdd();
		}
		

		private void Form1_Load(object sender, EventArgs e)
		{
			BdlDataGridView1.DataSource = Getbdd();
			searchData("");
			Datasize();

		}

		private DataTable Getbdd()
		{
			DataTable dtbdd = new DataTable();

			string connString = ConfigurationManager.ConnectionStrings["dbx"].ConnectionString;

			using (MySqlConnection con = new MySqlConnection(connString))
			{
				using (MySqlCommand cmd = new MySqlCommand("SELECT Nom,Maintenance,DateMaintenance,Adresse,Cp,Ville,Gsm,Fixe,Responsable,Mail,Siret,Web,code_client FROM clients", con))

				{
					con.Open();

					MySqlDataReader reader = cmd.ExecuteReader();

					dtbdd.Load(reader);
					

				}
			}

			return dtbdd;
		}

		private void Datasize()
		{
			BdlDataGridView1.Columns[0].Width = 180;
			BdlDataGridView1.Columns[1].Width = 120;
			BdlDataGridView1.Columns[2].Width = 170;
			BdlDataGridView1.Columns[3].Width = 200;
			BdlDataGridView1.Columns[4].Width = 55;
			BdlDataGridView1.Columns[8].Width = 130;
			BdlDataGridView1.Columns[9].Width = 200;
			BdlDataGridView1.Columns[10].Width = 130;
			BdlDataGridView1.Columns[11].Width = 200;
            BdlDataGridView1.Columns[12].Width = 200;
        }


		private void label2_Click(object sender, EventArgs e)
		{

		}

		private void textBox1_TextChanged(object sender, EventArgs e)
		{

		}

		public void searchData(string valueToSearch)
		{
			string query = "SELECT Nom,Maintenance,DateMaintenance,Adresse,Cp,Ville,Gsm,Fixe,Responsable,Mail,Siret,Web,code_client FROM clients WHERE CONCAT(" + comboBox1.SelectedItem + ") like '%" + valueToSearch + "%'";
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
			this.pictureBox6.Visible = true;
			this.label9.Visible = true;
			this.btn_New.Visible = true;
			this.button4.Visible = true;
			this.label20.Visible = true;
			this.label21.Visible = true;
			this.label22.Visible = true;
			this.label23.Visible = true;
			this.label24.Visible = true;
			this.label25.Visible = true;
			this.label15.Visible = true;
			this.label16.Visible = true;
			this.label17.Visible = true;
			this.label18.Visible = true;
			this.label19.Visible = true;
			this.txt_Nom2.Visible = true;
			this.txt_Maint2.Visible = true;
			this.txt_Dat2.Visible = true;
			this.txt_Adr2.Visible = true;
			this.txt_Cp2.Visible = true;
			this.txt_Ville2.Visible = true;
			this.txt_Gsm2.Visible = true;
			this.txt_Fix2.Visible = true;
			this.txt_Resp2.Visible = true;
			this.txt_Mail2.Visible = true;
			this.txt_Siret2.Visible = true;
			this.txt_Web2.Visible = true;
			this.label26.Visible = true;
			this.pictureBox10.Visible = true;
		}

		private void btn_Insert_Click_1(object sender, EventArgs e)
		{

		}

		private void DisplayData()
		{
			//connection.Open();
			DataTable dt = new DataTable();
			adapter = new MySqlDataAdapter("Select Nom,Maintenance,DateMaintenance,Adresse,Cp,Ville,Gsm,Fixe,Responsable,Mail,Siret,Web,code_client From clients", connection);
			adapter.Fill(dt);
			BdlDataGridView1.DataSource = dt;
			connection.Close();

		}

		private void ClearData()
		{
			textBox1.Text = "";
			txt_Maint.Text = "";
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
			client.code_client = 0;

		}

		private void ClearData2()
		{
			txt_Nom2.Text = "";
			txt_Maint2.Text = "";
			txt_Adr2.Text = "";
			txt_Dat2.Text = "";
			txt_Cp2.Text = "";
			txt_Ville2.Text = "";
			txt_Siret2.Text = "";
			txt_Web2.Text = "";
			txt_Resp2.Text = "";
			txt_Gsm2.Text = "";
			txt_Fix2.Text = "";
			txt_Mail2.Text = "";
			client.code_client = 0;
		}


		private void btn_Update_Click_1(object sender, EventArgs e)
		{
			if (client.code_client != 0)
			{
				command = new MySqlCommand("update clients set Nom = @nom , Maintenance = @maint, DateMaintenance = @date, Adresse = @adr,Cp = @cp,Ville = @ville, Gsm = @gsm, Fixe = @fixe,Responsable = @resp , Mail = @mail, Siret = @siret, Web = @web where code_client  = @id", connection);
				connection.Open();
				command.Parameters.AddWithValue("@id", client.code_client);
				command.Parameters.AddWithValue("@nom", textBox1.Text);
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
			connection.Open();
			command = new MySqlCommand("DELETE FROM clients WHERE code_client = '" + client.code_client + "'", connection);
			command.ExecuteReader();
			connection.Close();
			DisplayData();
			this.button3.Visible = false;
			this.btn_Delete.Visible = false;
			this.pictureBox5.Visible = false;
			this.label6.Visible = false;
			this.textBox2.Visible = false;
			this.button2.Visible = false;
			this.btn_Delete.Visible = false;
			this.pictureBox9.Visible = false;
			ClearData();
			ClearData2();
		}


		private void button3_Click_1(object sender, EventArgs e)
		{
			if (client.code_client != 0)
			{
				this.btn_Delete.Visible = true;
				this.pictureBox5.Visible = true;
				this.label6.Visible = true;
				this.textBox2.Visible = true;
				this.button2.Visible = true;
				this.pictureBox9.Visible = true;
			}
			else
			{
				MessageBox.Show("Selectionner un client !");
			}
		}

		private void BdlDataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			this.button3.Visible = true;
			client.code_client = Convert.ToInt64(BdlDataGridView1.Rows[e.RowIndex].Cells[12].Value.ToString());
			textBox1.Text = BdlDataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
			client.Nom_client = BdlDataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
			textBox2.Text = BdlDataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
			this.textBox1.Visible = true;
			txt_Maint.Text = BdlDataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
			txt_Date.Text = BdlDataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
			txt_Adr.Text = BdlDataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
			txt_CP.Text = BdlDataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
			txt_Ville.Text = BdlDataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
			txt_Gsm.Text = BdlDataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
			txt_Fix.Text = BdlDataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
			txt_Resp.Text = BdlDataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
			txt_Mail.Text = BdlDataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
			txt_Siret.Text = BdlDataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString();
			txt_Web.Text = BdlDataGridView1.Rows[e.RowIndex].Cells[11].Value.ToString();
            code_client = Convert.ToInt32(BdlDataGridView1.Rows[e.RowIndex].Cells[12].Value.ToString());

            connection.Close();
		}

		private void pictureBox1_Click(object sender, EventArgs e)
		{

		}

		private void label3_Click(object sender, EventArgs e)
		{

		}

		private void txt_Name_TextChanged(object sender, EventArgs e)
		{

		}

		private void button2_Click(object sender, EventArgs e)
		{
			this.btn_Delete.Visible = false;
			this.pictureBox5.Visible = false;
			this.label6.Visible = false;
			this.textBox2.Visible = false;
			this.button2.Visible = false;
			this.btn_Delete.Visible = false;
			this.pictureBox9.Visible = false;
		}

		private void button4_Click(object sender, EventArgs e)
		{
			this.pictureBox6.Visible = false;
			this.label9.Visible = false;
			this.btn_New.Visible = false;
			this.button4.Visible = false;
			this.label20.Visible = false;
			this.label21.Visible = false;
			this.label22.Visible = false;
			this.label23.Visible = false;
			this.label24.Visible = false;
			this.label25.Visible = false;
			pictureBox10.Visible = false;
			this.label15.Visible = false;
			this.label16.Visible = false;
			this.label17.Visible = false;
			this.label18.Visible = false;
			this.label19.Visible = false;
			this.txt_Nom2.Visible = false;
			txt_Maint2.Visible = false;
			txt_Dat2.Visible = false;
			txt_Adr2.Visible = false;
			txt_Cp2.Visible = false;
			txt_Ville2.Visible = false;
			txt_Gsm2.Visible = false;
			txt_Fix2.Visible = false;
			txt_Resp2.Visible = false;
			txt_Mail2.Visible = false;
			txt_Siret2.Visible = false;
			txt_Web2.Visible = false;
			label26.Visible = false;
			ClearData2();
		}

		private void btn_New_Click(object sender, EventArgs e)
		{
			if (txt_Nom2.Text != "")
			{
				command = new MySqlCommand("insert into clients(Nom,Maintenance,DateMaintenance,Adresse,Cp,Ville,Gsm,Fixe,Responsable,Mail,Siret,Web) values (@nom,@maint,@date,@adr,@cp,@ville,@gsm,@fixe,@resp,@mail,@siret,@web)", connection);
				connection.Open();
				command.Parameters.AddWithValue("@nom", txt_Nom2.Text);
				command.Parameters.AddWithValue("@maint", txt_Maint2.Text);
				command.Parameters.AddWithValue("@date", txt_Dat2.Text);
				command.Parameters.AddWithValue("@adr", txt_Adr2.Text);
				command.Parameters.AddWithValue("@cp", txt_Cp2.Text);
				command.Parameters.AddWithValue("@ville", txt_Ville2.Text);
				command.Parameters.AddWithValue("@gsm", txt_Gsm2.Text);
				command.Parameters.AddWithValue("@fixe", txt_Fix2.Text);
				command.Parameters.AddWithValue("@resp", txt_Resp2.Text);
				command.Parameters.AddWithValue("@mail", txt_Mail2.Text);
				command.Parameters.AddWithValue("@siret", txt_Siret2.Text);
				command.Parameters.AddWithValue("@web", txt_Web2.Text);



				command.ExecuteNonQuery();
				connection.Close();
				DisplayData();
				this.pictureBox6.Visible = false;
				this.label9.Visible = false;
				this.btn_New.Visible = false;
				this.button4.Visible = false;
				this.label20.Visible = false;
				this.label21.Visible = false;
				pictureBox10.Visible = false;
				this.label22.Visible = false;
				this.label23.Visible = false;
				this.label24.Visible = false;
				this.label25.Visible = false;
				this.label15.Visible = false;
				this.label16.Visible = false;
				this.label17.Visible = false;
				this.label18.Visible = false;
				this.label19.Visible = false;
				this.txt_Nom2.Visible = false;
				txt_Maint2.Visible = false;
				txt_Dat2.Visible = false;
				txt_Adr2.Visible = false;
				txt_Cp2.Visible = false;
				txt_Ville2.Visible = false;
				txt_Gsm2.Visible = false;
				txt_Fix2.Visible = false;
				txt_Resp2.Visible = false;
				txt_Mail2.Visible = false;
				txt_Siret2.Visible = false;
				txt_Web2.Visible = false;
				label26.Visible = false;
				ClearData2();

			}
			else
			{
				MessageBox.Show("Replir le champ 'Nom de l'entreprise'");
			}

		}

		private void txt_Date2_Click(object sender, EventArgs e)
		{
			if(client.code_client != 0)
			{
				fiche_inter inter = new fiche_inter();
				inter.ShowDialog();

			}
			else
			{
				MessageBox.Show("Selectionner un client");
			}
		}

		private void label21_Click(object sender, EventArgs e)
		{

		}

		private void txt_Fix2_TextChanged(object sender, EventArgs e)
		{

		}

		private void txt_Resp2_TextChanged(object sender, EventArgs e)
		{

		}

		private void label26_Click(object sender, EventArgs e)
		{

		}

		private void label25_Click(object sender, EventArgs e)
		{

		}

		private void txt_Gsm2_TextChanged(object sender, EventArgs e)
		{

		}

		private void label20_Click(object sender, EventArgs e)
		{

		}

		private void txt_Nom2_TextChanged(object sender, EventArgs e)
		{

		}

		private void label24_Click(object sender, EventArgs e)
		{

		}

		private void txt_Mail2_TextChanged(object sender, EventArgs e)
		{

		}

		private void label17_Click(object sender, EventArgs e)
		{

		}

		private void txt_Dat2_TextChanged(object sender, EventArgs e)
		{

		}

		private void label16_Click(object sender, EventArgs e)
		{

		}

		private void txt_Adr2_TextChanged(object sender, EventArgs e)
		{

		}

		private void txt_Cp2_TextChanged(object sender, EventArgs e)
		{

		}

		private void txt_Ville2_TextChanged(object sender, EventArgs e)
		{

		}

		private void txt_Web2_TextChanged(object sender, EventArgs e)
		{

		}

		private void txt_Siret2_TextChanged(object sender, EventArgs e)
		{

		}

		private void BdlDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{

		}

		private void pictureBox6_Click(object sender, EventArgs e)
		{

		}

		private void pictureBox7_Click(object sender, EventArgs e)
		{

		}

		private void label18_Click(object sender, EventArgs e)
		{

		}

		private void label19_Click(object sender, EventArgs e)
		{

		}

		private void label22_Click(object sender, EventArgs e)
		{

		}

		private void label23_Click(object sender, EventArgs e)
		{

		}

		private void pictureBox3_Click(object sender, EventArgs e)
		{

		}

		private void pictureBox10_Click(object sender, EventArgs e)
		{

		}

		private void BdlDataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
		{

		}
	}
	public class client
	{
		public static long code_client = 0;
		public static string Nom_client = "";
	}

}
