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

namespace PodrumPica
{
    public partial class Form1 : Form
    {
        DataTable tabela;
        public Form1()
        {
            InitializeComponent();
        }

        private void Load_Data()
        {
            SqlConnection veza = Konekcija.Connect();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM pice", veza);
            tabela = new DataTable();
            adapter.Fill(tabela);
        }

        private void Ucitavanje()
        {
            if (tabela.Rows.Count != 0)
            {
                for (int i = 0; i < tabela.Rows.Count; i++)
                {

                    //listBox1.Items.Insert(i, );

                    //listBox1.Items.AddRange(new object[] { "sdfwer, sdfas", "sdff" });
                    //listBox1.Items.Add(tabela.Rows[i]["naziv"].ToString());
                    //listBox1.Items.AddRange(string.Format(tabela.Rows[i]["naziv"].ToString(), tabela.Rows[i]["cena"].ToString()) );
                    //listBox1.Items.Add(i, 1) = tabela.Rows[i]["naziv"].ToString();
                    dataGridView1.Rows.Add(tabela.Rows[i]["naziv"].ToString(), tabela.Rows[i]["cena"].ToString(), tabela.Rows[i]["proizvodjac"].ToString(), tabela.Rows[i]["procenat_alkohola"].ToString());
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            Load_Data();
            Ucitavanje();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
