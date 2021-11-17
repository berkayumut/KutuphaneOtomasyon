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
    public partial class KitapEkle : Form
    {
        public KitapEkle()
        {
            InitializeComponent();
        }
        SqlCommand cmd;
        SqlConnection con;
        SqlDataAdapter da;
        DataSet ds;
        private void KitapEkle_Load(object sender, EventArgs e)
        {
            con = new SqlConnection("Data Source=Localhost\\SQLEXPRESS;Initial Catalog=KutuphaneDB;Integrated Security=True");
        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                string kayit = "insert into TblKitaplar(KitapAdi,KitapTuru,Adet) values (@KitapAdi,@KitapTuru,@Adet)";
                SqlCommand komut = new SqlCommand(kayit, con);
                komut.Parameters.AddWithValue("@KitapAdi", txtAdi.Text);
                komut.Parameters.AddWithValue("@KitapTuru", (txtTuru.Text));
                komut.Parameters.AddWithValue("@Adet", int.Parse(txtAdedi.Text));
                komut.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Kitap Kayıt İşlemi Gerçekleşti.");
                txtTuru.Text = "";
                txtAdi.Text = "";
                txtAdedi.Text = "";
            }
            catch (Exception hata)
            {
                MessageBox.Show("İşlem Sırasında Hata Oluştu." + hata.Message);
            }
        }
    }
}
