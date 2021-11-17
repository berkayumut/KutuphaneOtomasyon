using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KutuphaneOtomasyon
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlCommand cmd;
        SqlConnection con;
        SqlDataReader dr;
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btngiris_Click(object sender, EventArgs e)
        {
            string user = txtkullaniciAdi.Text;
            string pass = txtSifre.Text;
            con = new SqlConnection("Data Source=Localhost\\SQLEXPRESS;Initial Catalog=KutuphaneDB;Integrated Security=True");
            cmd = new SqlCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "SELECT * FROM TblKullanicilar where KullaniciAdi='" + txtkullaniciAdi.Text + "' AND Sifre='" + txtSifre.Text + "'";
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                Kitaplar fr = new Kitaplar();
                fr.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Kullanıcı adını ve şifrenizi kontrol ediniz.");
            }
            con.Close();
        }
      
    }
}
