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
    public partial class Form3 : Form
    {
        SqlConnection con;
        SqlDataAdapter da;
        SqlDataReader dr;
        SqlCommand cmd;
        DataSet ds;
        public static string SqlCon = @"Data Source=LAPTOP-EAS3BIR0\SQLEXPRESS;Initial Catalog=kargootomasyonu;Integrated Security=True";
        public int denemesayisi = 0;
        public static string kullanıcımsession2 = "";


        public Form3()
        {
            InitializeComponent();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form anaSayfa = Application.OpenForms["Form1"];
            if (anaSayfa == null)
                anaSayfa = new Form1();
            this.Hide();
            anaSayfa.Show();
        }

        // sql deki yonetici sayfasında ki yonetici bilgileri ile programda girilen yonetici bilgileri uyusursa yonetici formu acılsın istedik.
        public void yoneticigirisi()
        {
            string sorgu = "select * from yonetici where e_posta=@user and sifre=@pass";

            con = new SqlConnection(SqlCon);
            cmd = new SqlCommand(sorgu, con);
            cmd.Parameters.AddWithValue("@user", textBox1.Text);
            cmd.Parameters.AddWithValue("@pass", textBox2.Text);

            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                yonetici_ekranı a = new yonetici_ekranı();
                kullanıcımsession2 = textBox1.Text;
                this.Hide();
                
                a.Show();
            }
            else
            {
                MessageBox.Show("kullanici adi veya sifre hatali");
                textBox1.Clear();
                textBox2.Clear();
                textBox1.Focus();
            }
            con.Close();

        }

        
        
        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            yoneticigirisi();
            denemesayisi++;
            textBox1.Clear(); // Yönetici giriş kısmı giriş yapıldıktan sonra temizlendi.
            textBox2.Clear();

            if (denemesayisi == 3)
            {
                MessageBox.Show("3 defa hatali giris yaptınız");
                Application.Exit();
                // 3 kere hatalı denemede sistemden cıkmasını sagladık.
            }
        }


    }
}
