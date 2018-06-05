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
    public partial class parc : Form

    {
        MySqlConnection connection = new MySqlConnection("server=bj881856-001.dbaas.ovh.net;user id=bdl; database=Bdl; port=35312; password=Bdl69100");
        MySqlCommand command;
        MySqlDataAdapter adapter;
        DataTable table;
        int id_parc = 0;


        public parc()
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
                using (MySqlCommand cmd = new MySqlCommand("SELECT id_parc,N_client,Categorie,Nserie,Marque,Model,Version,Os,Solution,Accessoire FROM Parc", con))

                {
                    con.Open();

                    MySqlDataReader reader = cmd.ExecuteReader();

                    dtbdd.Load(reader);

                }
            }

            return dtbdd;
        }

        private void DisplayData()
        {
            //connection.Open();
            DataTable dt = new DataTable();
            adapter = new MySqlDataAdapter("Select * From Parc", connection);
            adapter.Fill(dt);
            BdlDataGridView2.DataSource = dt;
            connection.Close();

        }

        private void parc_Load(object sender, EventArgs e)
        {
            BdlDataGridView2.DataSource = Getbdd();
            try
            {
                MySqlConnection connection = new MySqlConnection("server=bj881856-001.dbaas.ovh.net;user id=bdl; database=Bdl; port=35312; password=Bdl69100");
                string selectQuery = "SELECT * from clients";
                connection.Open();
                MySqlCommand command = new MySqlCommand(selectQuery, connection);

                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    comboBox3.Items.Add(reader.GetString("Nom"));
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            label1.Text = client.Nom_client;
        }

        private void ClearData()
        {
            txt_Acs.Text = "";
            txt_Marque.Text = "";
            txt_Model.Text = "";
            txt_Os.Text = "";
            txt_Solution.Text = "";
            txt_Nserie.Text = "";
            comboBox4.Text = "";
            txt_Version.Text = "";
            id_parc = 0;
        }

        private void BTN_Rech_Click(object sender, EventArgs e)
        {
            string valueToSearch = textBoxRech.Text.ToString();
            searchData(valueToSearch);
        }

        private void BdlDataGridView2_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            this.button3.Visible = true;
            id_parc = Convert.ToInt32(BdlDataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString());
            this.textBox1.Visible = true;
            textBox1.Text = BdlDataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBox2.Text = BdlDataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString();
            comboBox4.Text = BdlDataGridView2.Rows[e.RowIndex].Cells[2].Value.ToString();
            txt_Nserie.Text = BdlDataGridView2.Rows[e.RowIndex].Cells[3].Value.ToString();
            txt_Marque.Text = BdlDataGridView2.Rows[e.RowIndex].Cells[4].Value.ToString();
            txt_Model.Text = BdlDataGridView2.Rows[e.RowIndex].Cells[5].Value.ToString();
            txt_Version.Text = BdlDataGridView2.Rows[e.RowIndex].Cells[6].Value.ToString();
            txt_Os.Text = BdlDataGridView2.Rows[e.RowIndex].Cells[7].Value.ToString();
            txt_Solution.Text = BdlDataGridView2.Rows[e.RowIndex].Cells[8].Value.ToString();
            txt_Acs.Text = BdlDataGridView2.Rows[e.RowIndex].Cells[9].Value.ToString();
            connection.Close();
        }




        public void searchData(string valueToSearch)
        {
            string query = "SELECT N_client,Categorie,Nserie,Marque,Model,Version,Os,Solution,Accessoire from Parc WHERE CONCAT(" + comboBox1.SelectedItem + ") like '%" + valueToSearch + "%'";
            command = new MySqlCommand(query, connection);
            adapter = new MySqlDataAdapter(command);
            table = new DataTable();
            adapter.Fill(table);
            BdlDataGridView2.DataSource = table;
        }

        private void btn_Update_Click(object sender, EventArgs e)
        {
            if (id_parc != 0)
            {
                command = new MySqlCommand("update Parc set N_Client = @ncli, Categorie = @type, Nserie = @serie, Marque = @marque, Model = @model, Version = @version, Os = @os, Solution = @solution, Accessoire = @acs    where id_parc = @id", connection);
                connection.Open();
                command.Parameters.AddWithValue("@id", id_parc);
                command.Parameters.AddWithValue("@ncli", textBox1.Text);
                command.Parameters.AddWithValue("@type", comboBox4.SelectedItem);
                command.Parameters.AddWithValue("@serie", txt_Nserie.Text);
                command.Parameters.AddWithValue("@marque", txt_Marque.Text);
                command.Parameters.AddWithValue("@model", txt_Model.Text);
                command.Parameters.AddWithValue("@version", txt_Version.Text);
                command.Parameters.AddWithValue("@os", txt_Os.Text);
                command.Parameters.AddWithValue("@solution", txt_Solution.Text);
                command.Parameters.AddWithValue("@acs", txt_Acs.Text);
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

        private void button3_Click(object sender, EventArgs e)
        {
            if (id_parc != 0)
            {
                this.button3.Visible = false;
                this.pictureBox9.Visible = true;
                this.btn_Delete.Visible = true;
                this.pictureBox5.Visible = true;
                this.label6.Visible = true;
                this.textBox2.Visible = true;
                this.button2.Visible = true;
                this.textBox2.Visible = true;
            }
            else
            {
                MessageBox.Show("Selectionner une ligne !");
            }
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            if (id_parc != 0)
            {
                connection.Open();
                command = new MySqlCommand("DELETE FROM Parc WHERE id_parc = '" + id_parc + "'", connection);
                command.ExecuteReader();
                connection.Close();
                DisplayData();
                this.btn_Delete.Visible = false;
                this.pictureBox5.Visible = false;
                this.label6.Visible = false;
                this.textBox2.Visible = false;
                this.button2.Visible = false;
                this.btn_Delete.Visible = false;
                this.pictureBox9.Visible = false;
                ClearData();


            }
            else
            {
                MessageBox.Show("Selectionner une ligne a supprimer");
            }
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

        private void btn_Insert_Click(object sender, EventArgs e)
        {
            txt_Serie.Visible = true;
            this.pictureBox6.Visible = true;
            this.label9.Visible = true;
            this.btn_New.Visible = true;
            this.button4.Visible = true;
            this.label20.Visible = true;
            this.label21.Visible = true;
            this.label25.Visible = true;
            this.label15.Visible = true;
            this.label11.Visible = true;
            this.label16.Visible = true;
            this.label17.Visible = true;
            this.comboBox3.Visible = true;
            this.label12.Visible = true;
            this.comboBox2.Visible = true;
            this.pictureBox10.Visible = true; 
            txt_Model2.Visible = true;
            txt_Os2.Visible = true;
            txt_Acs2.Visible = true;
            txt_Marque2.Visible = true;
            txt_Version2.Visible = true;
            txt_Solution2.Visible = true;
            label26.Visible = true;
            ClearData2();
        }

        private void ClearData2()
        {
            txt_Acs2.Text = "";
            txt_Marque2.Text = "";
            txt_Model2.Text = "";
            txt_Os2.Text = "";
            txt_Solution2.Text = "";
            comboBox2.SelectedItem = "";
            txt_Version2.Text = "";
        }

        private void btn_New_Click(object sender, EventArgs e)
        {
            if (comboBox2.Text != "")
            {
                command = new MySqlCommand("insert into Parc(N_Client,Categorie,NSerie,Marque,Model,Version,Os,Solution,Accessoire) values (@ncli,@type,@serie, @marque, @model, @version, @os, @solution, @acs)", connection);
                connection.Open();
                command.Parameters.AddWithValue("@ncli", comboBox3.SelectedItem);
                command.Parameters.AddWithValue("@type", comboBox2.SelectedItem);
                command.Parameters.AddWithValue("@serie", txt_Serie.Text);
                command.Parameters.AddWithValue("@marque", txt_Marque2.Text);
                command.Parameters.AddWithValue("@model", txt_Model2.Text);
                command.Parameters.AddWithValue("@version", txt_Version2.Text);
                command.Parameters.AddWithValue("@os", txt_Os2.Text);
                command.Parameters.AddWithValue("@solution", txt_Solution2.Text);
                command.Parameters.AddWithValue("@acs", txt_Acs2.Text);

                command.ExecuteNonQuery();
                connection.Close();
                DisplayData();

                this.pictureBox6.Visible = false;
                this.label9.Visible = false;
                this.btn_New.Visible = false;
                this.button4.Visible = false;
                this.label20.Visible = false;
                this.label21.Visible = false;
                this.label25.Visible = false;
                this.label15.Visible = false;
                this.label16.Visible = false;
                this.label17.Visible = false;
                this.label12.Visible = false;
                this.label11.Visible = false;
                txt_Model2.Visible = false;
                this.comboBox3.Visible = false;
                txt_Os2.Visible = false;
                txt_Acs2.Visible = false;
                txt_Marque2.Visible = false;
                txt_Version2.Visible = false;
                txt_Solution2.Visible = false;
                txt_Serie.Visible = false;
                this.comboBox2.Visible = false;
                label26.Visible = false;
                this.pictureBox10.Visible = false;
                ClearData2();

            }
            else
            {
                MessageBox.Show("Replir le champ 'Type de parc'");
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.pictureBox6.Visible = false;
            this.pictureBox10.Visible = false;
            this.comboBox3.Visible = false;
            this.label9.Visible = false;
            this.btn_New.Visible = false;
            this.label12.Visible = false;
            this.button4.Visible = false;
            this.label11.Visible = false;
            this.label20.Visible = false;
            this.label21.Visible = false;
            this.label25.Visible = false;
            this.label15.Visible = false;
            this.label16.Visible = false;
            this.label17.Visible = false;
            this.comboBox2.Visible = false;
            this.label11.Visible = false;
            txt_Model2.Visible = false;
            txt_Os2.Visible = false;
            txt_Serie.Visible = false;
            txt_Acs2.Visible = false;
            txt_Marque2.Visible = false;
            txt_Version2.Visible = false;
            txt_Solution2.Visible = false;
            label26.Visible = false;
            ClearData2();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void txt_Solution2_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_Model2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
    

