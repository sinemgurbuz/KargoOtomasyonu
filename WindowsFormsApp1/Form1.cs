using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Data.SqlClient;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        SqlConnection con;
        SqlDataAdapter da;
        SqlDataReader dr;
        SqlCommand cmd;
        DataSet ds;
        public static string SqlCon = @"Data Source =LAPTOP-EAS3BIR0\SQLEXPRESS;Initial Catalog=kargootomasyonu;Integrated Security = True";
        public string Sqlsorgu;

        public int denemesayisi = 0;
        public static string kullanıcımsession = "";

        public Form1()
        {
            InitializeComponent();
        }
        // sql server musteri tablosundaki verilerle girilen veriler aynı ise kullanıcı formunun acılmasını sagladık.
        public void kullaniciGirisi()
        {
            string sorgu = "select * from musteri where mail=@user and sifre=@pass";

            con = new SqlConnection(SqlCon);
            cmd = new SqlCommand(sorgu, con);
            cmd.Parameters.AddWithValue("@user", textBox1.Text);
            cmd.Parameters.AddWithValue("@pass", textBox2.Text);

            con.Open();
            dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                kullanici_ekrani a = new kullanici_ekrani();
                this.Hide();
                kullanıcımsession = textBox1.Text;
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

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form yoneticiGirisi = Application.OpenForms["Form3"];
            if (yoneticiGirisi == null)
                yoneticiGirisi = new Form3();
            this.Hide();
            yoneticiGirisi.Show();
            // label a tıklayınca yonetici giriş ekranının cıkmasını sagladık.
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Berdan Aksoy\n202523004\n\nElif Şahan\n202523021\n\nSinem Gürbüz\n202523060");
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form uye_ol = Application.OpenForms["uye_ol"];
            if (uye_ol == null)
                uye_ol = new uye_ol();
            this.Hide();
            uye_ol.Show();
            // label a tıklayınca uye ol ekranının cıkmasını sagladık.
        }

        private void button4_Click(object sender, EventArgs e)
        {
            kullaniciGirisi();
            denemesayisi++;
            textBox1.Clear();    // Giriş yaptıktan sonra textboxların içi temizlendi.
            textBox2.Clear();
            if (denemesayisi == 3)
            {
                // eger 3 kere hatalı giris olursa yani musteri tablosundaki verilerle girilen veriler 3 kere uyusmassa programdan atsın.

                MessageBox.Show("3 defa hatali giris yaptınız");
                Application.Exit();
            }
        }
       
  
    }
}
