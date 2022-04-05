using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Globalization;

namespace PodrumPica
{
    public partial class Pica : Form
    {
        DataTable tabela;
        int pocetak = 0;
        public Pica()
        {
            InitializeComponent();
        }

        private void Load_Data()
        {
            SqlConnection veza = Konekcija.Connect();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT pice.*, magacin.kolicina, magacin.adresa FROM pice join magacin on magacin.id_pica = pice.id", veza);
            tabela = new DataTable();
            adapter.Fill(tabela);
        }

        private void Ucitavanje()
        {
            if (tabela.Rows.Count != 0)
            {
                dataGridView1.Rows.Clear();
                dataGridView1.Refresh();
                for (int i = 0; i < tabela.Rows.Count; i++)
                {
                    dataGridView1.Rows.Add(tabela.Rows[i]["naziv"].ToString(), tabela.Rows[i]["cena"].ToString(), tabela.Rows[i]["proizvodjac"].ToString(), tabela.Rows[i]["procenat_alkohola"].ToString(), tabela.Rows[i]["id"].ToString(), tabela.Rows[i]["kolicina"], tabela.Rows[i]["adresa"].ToString());
                }
            }
        }

        private void Unos()
        {

            /*
            if (tabela.Rows.Count != 0)
            {
                for (int i = 0; i < tabela.Rows.Count; i++)
                {
                    if(tabela.Rows[i]["naziv"].ToString().Contains(vrednost))dataGridView1.Rows.Add(tabela.Rows[i]["naziv"].ToString(), tabela.Rows[i]["cena"].ToString(), tabela.Rows[i]["proizvodjac"].ToString(), tabela.Rows[i]["procenat_alkohola"].ToString());
                    
                }
            }
            */
        }


        private void Form1_Load(object sender, EventArgs e)
        {

            Load_Data();
            Ucitavanje();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            StringBuilder Naredba = new StringBuilder("INSERT INTO pice (naziv, cena, proizvodjac, procenat_alkohola) VALUES('");
            Naredba.Append(textBox1.Text + "', '");
            Naredba.Append(Convert.ToInt32(textBox2.Text) + "', '");
            Naredba.Append(textBox3.Text + "', '");
            Naredba.Append(float.Parse(textBox4.Text, CultureInfo.InvariantCulture.NumberFormat) + "')");
            SqlConnection veza = Konekcija.Connect();
            SqlCommand Komanda = new SqlCommand(Naredba.ToString(), veza);
            try
            {
                veza.Open();
                Komanda.ExecuteNonQuery();
                veza.Close();
            }
            catch (Exception Greska)
            {
                MessageBox.Show(Greska.Message);
            }
            Load_Data();
            //broj_sloga = tabela.Rows.Count - 1;
            Ucitavanje();
            /*
            string ime = textBox1.Text;
            int kolicina = Convert.ToInt32(textBox2.Text);
            string ubacivanje = "insert into Narudzbine values(" + ime + ", " + kolicina + ")";
            SqlConnection veza = Konekcija.Connect();
            veza.Open();
            SqlCommand komanda = new SqlCommand(ubacivanje);
            komanda.ExecuteNonQuery();
            veza.Close();
            */
        }

        public void button2_Click(object sender, EventArgs e)
        {
            if (pocetak == 0)
            {
                Form2 cart = new Form2();
                cart.Show();
                pocetak++;
            }
            else
            {
                Form2 obj = (Form2)Application.OpenForms["Form2"];
                obj.Close();
                Form2 cart = new Form2();
                cart.Show();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //string tekst = textBox1.Text;
            //Ucitavanje1(tekst);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int rowindex = dataGridView1.CurrentCell.RowIndex;
            //int columnindex = dataGridView1.CurrentCell.ColumnIndex;
            string Naredba = "DELETE FROM pice WHERE id = " + dataGridView1.Rows[rowindex].Cells["ID"].Value.ToString();
            SqlConnection veza = Konekcija.Connect();
            SqlCommand Komanda = new SqlCommand(Naredba, veza);
            Boolean brisano = false;
            try
            {
                veza.Open();
                Komanda.ExecuteNonQuery();
                veza.Close();
                brisano = true;
            }
            catch (Exception Greska)
            {
                MessageBox.Show(Greska.Message);
            }
            if (brisano)
            {
                Load_Data();
                Ucitavanje();
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            int rowindex = dataGridView1.CurrentCell.RowIndex;
            //int columnindex = dataGridView1.CurrentCell.ColumnIndex;

            textBox1.Text = dataGridView1.Rows[rowindex].Cells["Naziv"].Value.ToString();
            textBox2.Text = dataGridView1.Rows[rowindex].Cells["Cena"].Value.ToString();
            textBox3.Text = dataGridView1.Rows[rowindex].Cells["Proizvodjac"].Value.ToString();
            textBox4.Text = dataGridView1.Rows[rowindex].Cells["Procenat"].Value.ToString();
            textBox5.Text = dataGridView1.Rows[rowindex].Cells["Kolicina"].Value.ToString();
            textBox6.Text = dataGridView1.Rows[rowindex].Cells["Adresa"].Value.ToString();
        }
    }
}
