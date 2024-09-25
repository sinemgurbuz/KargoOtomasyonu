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
    public partial class kullanici_ekrani : Form
    {
        SqlConnection con;
        SqlDataAdapter da;
        SqlDataReader dr;
        SqlCommand cmd;
        DataSet ds;
        public static string SqlCon = @"Data Source =LAPTOP-EAS3BIR0\SQLEXPRESS;Initial Catalog=kargootomasyonu;Integrated Security = True";
        public string Sqlsorgu;

        public kullanici_ekrani()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form anaSayfa = Application.OpenForms["Form1"];
            if (anaSayfa == null)
                anaSayfa = new Form1();
            this.Hide();
            anaSayfa.Show();
            // ana sayfaya donmeyi sagladık.
        }
        
        // dataGrid e Table_1 in doldurulmasını sagladık.
        void GridDoldur(string sqlsorgu)
        {
            con = new SqlConnection(SqlCon);
            da = new SqlDataAdapter("select * from Table_1", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "Table_1");
            dataGridView1.DataSource = ds.Tables["Table_1"];
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GridDoldur(Sqlsorgu);
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
           
            Sqlsorgu = "select * from Table_1 where KargoID like '%" + textBox6.Text + "%'"; 
            // Table_1 de ki kargo ID degeri ile texbox a girilen deger aynı ise datagrid e o tablodaki degerleri donsun.

        }

        
        private void Siparis()
        {
            // kullanıcının yeni siparis eklemesi için kargo ekleme kısmı yaptık burdaki girdigimiz verilerle sql deki tabloyu doldurucaz
            // yeni kargo kaydı eklemıs olacagız 

            con = new SqlConnection(SqlCon);
            string sql = "insert into Table_2 ([AliciAdi],[AliciAdresi],[GondericiAdi],[GondericiAdresi],[UrunAded]) values (@alici,@aliciadres,@gönderen,@gönderenadres,@aded)";
            cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@alici", textBox1.Text);
            cmd.Parameters.AddWithValue("@aliciadres", textBox2.Text);
            cmd.Parameters.AddWithValue("@gönderen", textBox3.Text);
            cmd.Parameters.AddWithValue("@gönderenadres", textBox4.Text);
            cmd.Parameters.AddWithValue("@aded", textBox5.Text);
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = sql;

            cmd.ExecuteNonQuery();

            con.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Siparis();
            MessageBox.Show("SİPARİSİNİZ ALINDI...");
            
        }

        
    }
}
