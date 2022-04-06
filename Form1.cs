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
                    dataGridView1.Rows.Add(tabela.Rows[i]["naziv"].ToString(), tabela.Rows[i]["cena"].ToString(), tabela.Rows[i]["proizvodjac"].ToString(), tabela.Rows[i]["procenat_alkohola"].ToString(), tabela.Rows[i]["kolicina"], tabela.Rows[i]["adresa"].ToString(), tabela.Rows[i]["id"].ToString());
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Load_Data();
            Ucitavanje();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection veza = Konekcija.Connect();

            StringBuilder Provera1 = new StringBuilder("select id from pice where naziv = '");
            Provera1.Append(textBox1.Text + "' and cena = ");
            Provera1.Append(Convert.ToInt32(textBox2.Text) + " and proizvodjac = '");
            Provera1.Append(textBox3.Text + "' and procenat_alkohola = ");
            Provera1.Append(float.Parse(textBox4.Text, CultureInfo.InvariantCulture.NumberFormat));

            SqlDataAdapter adapter1 = new SqlDataAdapter(Provera1.ToString(), veza);
            DataTable pice = new DataTable();
            adapter1.Fill(pice);

            if ((pice != null) && (pice.Rows.Count > 0))
            {
                StringBuilder Provera2 = new StringBuilder("select magacin.id from magacin join pice on magacin.id_pica = pice.id where magacin.adresa = '");
                Provera2.Append(textBox6.Text + "' and pice.id = ");
                Provera2.Append(pice.Rows[0][0].ToString());

                SqlDataAdapter adapter2 = new SqlDataAdapter(Provera2.ToString(), veza);
                DataTable adresa = new DataTable();
                adapter2.Fill(adresa);

                if ((adresa != null) && (adresa.Rows.Count > 0))
                {
                    string apdejt = "UPDATE magacin SET kolicina = (kolicina + " + Convert.ToInt32(textBox5.Text) + ") WHERE id = " + adresa.Rows[0][0];
                    SqlCommand Komanda = new SqlCommand(apdejt.ToString(), veza);
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
                }
                else
                {
                    string noviMag = "insert into magacin(id_pica, kolicina, adresa) values (" + Convert.ToInt32(pice.Rows[0][0].ToString()) + ", " + Convert.ToInt32(textBox5.Text) + ", '" + textBox6.Text + "')";
                    SqlCommand Komanda = new SqlCommand(noviMag.ToString(), veza);
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
                }
            }
            else
            {
                StringBuilder insert1 = new StringBuilder("insert into pice values ('");
                insert1.Append(textBox1.Text + "', ");
                insert1.Append(Convert.ToInt32(textBox2.Text) + ", '");
                insert1.Append(textBox3.Text + "', ");
                insert1.Append(float.Parse(textBox4.Text, CultureInfo.InvariantCulture.NumberFormat) + ")");
                SqlCommand InsertPice = new SqlCommand(insert1.ToString(), veza);

                try
                {
                    veza.Open();
                    InsertPice.ExecuteNonQuery();
                    veza.Close();
                }
                catch (Exception Greska)
                {
                    MessageBox.Show(Greska.Message);
                }

                SqlDataAdapter getID = new SqlDataAdapter(Provera1.ToString(), veza);
                DataTable ID = new DataTable();
                getID.Fill(ID);

                string insert2 = "insert into magacin(id_pica, kolicina, adresa) values (" + Convert.ToInt32(ID.Rows[0][0].ToString()) + ", " + Convert.ToInt32(textBox5.Text) + ", '" + textBox6.Text + "')";
                SqlCommand InsertMag = new SqlCommand(insert2.ToString(), veza);

                try
                {
                    veza.Open();
                    InsertMag.ExecuteNonQuery();
                    veza.Close();
                }
                catch (Exception Greska)
                {
                    MessageBox.Show(Greska.Message);
                }
            }
            
            Load_Data();
            Ucitavanje();
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

        private void textBox1_TextChanged(object sender, EventArgs e) { }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection veza = Konekcija.Connect();

            int rowindex = dataGridView1.CurrentCell.RowIndex;
            string ID = dataGridView1.Rows[rowindex].Cells["ID"].Value.ToString();

            string BrisiMag = "delete from magacin where id_pica = " + ID;
            SqlCommand KomandaMag = new SqlCommand(BrisiMag, veza);

            string BrisiPice = "DELETE FROM pice WHERE id = " + ID;      
            SqlCommand KomandaPice = new SqlCommand(BrisiPice, veza);

            Boolean brisano = false;
            try
            {
                veza.Open();
                KomandaMag.ExecuteNonQuery();
                KomandaPice.ExecuteNonQuery();
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
            textBox1.Text = dataGridView1.Rows[rowindex].Cells["Naziv"].Value.ToString();
            textBox2.Text = dataGridView1.Rows[rowindex].Cells["Cena"].Value.ToString();
            textBox3.Text = dataGridView1.Rows[rowindex].Cells["Proizvodjac"].Value.ToString();
            textBox4.Text = dataGridView1.Rows[rowindex].Cells["Procenat"].Value.ToString();
            textBox5.Text = dataGridView1.Rows[rowindex].Cells["Kolicina"].Value.ToString();
            textBox6.Text = dataGridView1.Rows[rowindex].Cells["Adresa"].Value.ToString();
        }
    }
}
