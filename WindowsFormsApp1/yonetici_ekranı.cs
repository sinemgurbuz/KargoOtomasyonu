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

namespace WindowsFormsApp1
{
    public partial class yonetici_ekranı : Form
    {
        SqlConnection con;
        SqlDataAdapter da;
        SqlDataReader dr;
        SqlCommand cmd;
        DataSet ds;
        public static string SqlCon = @"Data Source =LAPTOP-EAS3BIR0\SQLEXPRESS;Initial Catalog=kargootomasyonu;Integrated Security = True";
        public string Sqlsorgu;

        public yonetici_ekranı()
        {
            InitializeComponent();
        }

        // kargo takip tablosunu dataGridViev' e doldurmak için kullandıgımız fonksiyon
        void GridDoldur()
        {
            con = new SqlConnection(SqlCon);
            da = new SqlDataAdapter("select * from KargoTakip", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "KargoTakip");
            dataGridView1.DataSource = ds.Tables["KargoTakip"];
            con.Close();
        }
        // musteri tablosunu dataGridViev' e doldurmak için kullandıgımız fonksiyon
        void GridDoldur2()
        {
            con = new SqlConnection(SqlCon);
            da = new SqlDataAdapter("select * from musteri", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "musteri");
            dataGridView2.DataSource = ds.Tables["musteri"];
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GridDoldur();
            // butona tıklayınca fonk cagırıldı.
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GridDoldur2();
            // butona tıklayınca fonk cagırıldı.
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //dataGrid deki degerleri texbox a atamak için kullandık.

            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
        }
        

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // kargo durumunu guncellemek için yazdığımız kodlar

            con = new SqlConnection(SqlCon);
            string Sql = "update KargoTakip set  [KargoDurumu] ='" + textBox2.Text + "' where [KargoTakipNo] ='" + textBox1.Text + "'";
            cmd = new SqlCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = Sql;
            cmd.ExecuteNonQuery();
            con.Close();

            GridDoldur();
        }

        private void button5_Click(object sender, EventArgs e)
        {

            // uye silme kısmı için yazdıgımız kodlar

            con = new SqlConnection(SqlCon);
            con.Open();
            string sql = "delete from musteri where[musteri_ıd]=@no";
            cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@no", Convert.ToInt32(textBox3.Text));
            cmd.Connection = con;
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();

            con.Close();
            GridDoldur2();

            textBox3.Clear();
        }
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //dataGrid deki degerleri texbox a atamak için kullandık.
            textBox3.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();

            

        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form anaSayfa = Application.OpenForms["Form1"];
            if (anaSayfa == null)
                anaSayfa = new Form1();
            this.Hide();
            anaSayfa.Show();
            // tekrar anasayfa formuna donmek için
        }

        
    }
}
