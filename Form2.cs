using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace PodrumPica
{
    public partial class Form2 : Form
    {
        DataTable tabela;
        public Form2()
        {
            InitializeComponent();
        }

        public void Load_Data()
        {
            SqlConnection veza = Konekcija.Connect();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Narudzbina", veza);
            tabela = new DataTable();
            adapter.Fill(tabela);
        }

        public void Form2_Load(object sender, EventArgs e)
        {
            Load_Data();

            dataGridView1.Rows.Add("Coca-Cola", "2", "120");
            dataGridView1.Rows.Add("Banjalucko pivo", "3", "150");
            dataGridView1.Rows.Add("Hoptopod IPA", "1", "350");
            if (tabela.Rows.Count != 0)
            {
                for (int i = 0; i < tabela.Rows.Count; i++)
                {
                    dataGridView1.Rows.Add(tabela.Rows[i]["naziv"].ToString(), tabela.Rows[i]["cena"].ToString(), tabela.Rows[i]["proizvodjac"].ToString(), tabela.Rows[i]["procenat_alkohola"].ToString());
                }
            }
            
            SqlConnection veza = Konekcija.Connect();
            SqlDataAdapter adapter1 = new SqlDataAdapter("select sum(cena) from Narudzbina", veza);
            DataTable broj = new DataTable();
            adapter1.Fill(broj);
            label3.Text = broj.ToString();
            
        }

        private void button1_Click(object sender, EventArgs e)
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
    }
}
