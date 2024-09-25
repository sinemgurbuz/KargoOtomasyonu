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
    public partial class uye_ol : Form
    {
        SqlConnection con;
        SqlDataAdapter da;
        SqlDataReader dr;
        SqlCommand cmd;
        
        public static string SqlCon = @"Data Source =LAPTOP-EAS3BIR0\SQLEXPRESS;Initial Catalog=kargootomasyonu;Integrated Security = True";

        public uye_ol()
        {
            InitializeComponent();
        }

        private void yenikayit()
        {
            // sql deki musteri tablosuna yeni bir kullanıcı kaydı olusturmak için yazdığımız kodlar.
            con = new SqlConnection(SqlCon);
            string sql = "insert into musteri ([isim],[soyisim],[mail],[sifre]) values (@isim,@soyisim,@mail,@sifre)";
            cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@isim", textBox1.Text);
            cmd.Parameters.AddWithValue("@soyisim", textBox2.Text);
            cmd.Parameters.AddWithValue("@mail", textBox3.Text);
            cmd.Parameters.AddWithValue("@sifre", textBox4.Text);
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
            con.Close();
        }



        private void button1_Click(object sender, EventArgs e)
        {

            if (textBox4.Text == textBox5.Text)
            {
                yenikayit();
               

                MessageBox.Show("Uyeliginiz basari ile olusturuldu....");

                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();
                
            }
            else
            {
                MessageBox.Show("sifre ile sifre tekrari ayni degil!! kontrol ediniz...");
                // sifre ile sifre tekrarı kısmı eger aynı degilse kayıt olusmasın istedik.

            }
        }
        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form anaSayfa = Application.OpenForms["Form1"];
            if (anaSayfa == null)
                anaSayfa = new Form1();
            this.Hide();
            anaSayfa.Show();
        }

        private void uye_ol_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
