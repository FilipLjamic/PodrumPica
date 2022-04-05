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
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Narudzbine", veza);
            tabela = new DataTable();
            adapter.Fill(tabela);
        }

        public void Form2_Load(object sender, EventArgs e)
        {
            if (tabela.Rows.Count != 0)
            {
                for (int i = 0; i < tabela.Rows.Count; i++)
                {
                    dataGridView1.Rows.Add(tabela.Rows[i]["naziv"].ToString(), tabela.Rows[i]["cena"].ToString(), tabela.Rows[i]["proizvodjac"].ToString(), tabela.Rows[i]["procenat_alkohola"].ToString());
                }
            }

            SqlConnection veza = Konekcija.Connect();
            SqlDataAdapter adapter1 = new SqlDataAdapter("select sum(cena) from Narudzbine", veza);
            DataTable broj = new DataTable();
            adapter1.Fill(broj);
            label3.Text = broj.ToString();
        }
    }
}
