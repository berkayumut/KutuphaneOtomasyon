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
    public partial class VatandasEkle : Form
    {
        public VatandasEkle()
        {
            InitializeComponent();
        }
        SqlCommand cmd;
        SqlConnection con;
        SqlDataAdapter da;
        DataSet ds;

        private void VatandasEkle_Load(object sender, EventArgs e)
        {
            con = new SqlConnection("Data Source=Localhost\\SQLEXPRESS;Initial Catalog=KutuphaneDB;Integrated Security=True");
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                string kayit = "insert into TblVatandaslar(VatandasAdi,VatandasAdres,VatandasTelefon) values (@VatandasAdi,@VatandasAdres,@VatandasTelefon)";
                SqlCommand komut = new SqlCommand(kayit, con);
                komut.Parameters.AddWithValue("@VatandasAdi", txtAdi.Text);
                komut.Parameters.AddWithValue("@VatandasAdres", txtAdres.Text);
                komut.Parameters.AddWithValue("@VatandasTelefon", txtTel.Text);
                komut.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Vatandaş Kayıt İşlemi Gerçekleşti.");
                txtTel.Text = "";
                txtAdi.Text = "";
                txtAdres.Text = "";
            }
            catch (Exception hata)
            {
                MessageBox.Show("İşlem Sırasında Hata Oluştu." + hata.Message);
            }
        }
    }
}
