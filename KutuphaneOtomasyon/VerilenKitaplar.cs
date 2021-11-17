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
    public partial class VerilenKitaplar : Form
    {
        public VerilenKitaplar()
        {
            InitializeComponent();
        }
        SqlCommand cmd;
        SqlConnection con;
        SqlDataAdapter da;
        DataSet ds;
        void griddoldur()
        {

            da = new SqlDataAdapter("Select *From TblKitapIslemleri", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "TblKitapIslemleri");
            dataGridView1.DataSource = ds.Tables["TblKitapIslemleri"];
            con.Close();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void VerilenKitaplar_Load(object sender, EventArgs e)
        {
            con = new SqlConnection("Data Source=Localhost\\SQLEXPRESS;Initial Catalog=KutuphaneDB;Integrated Security=True");
            txtId.Enabled = false;
            txtVatandas.Enabled = false;
            txtTarih.Enabled = false;
            txtAdi.Enabled = false;
            griddoldur();
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int secilensatir = Convert.ToInt16(ds.Tables["TblKitapIslemleri"].Rows[e.RowIndex]["Id"]);

            txtId.Text = secilensatir.ToString();
            txtAdi.Text = ds.Tables["TblKitapIslemleri"].Rows[e.RowIndex]["Kitap"].ToString();
            txtVatandas.Text = ds.Tables["TblKitapIslemleri"].Rows[e.RowIndex]["Vatandas"].ToString();
            txtTarih.Text = ds.Tables["TblKitapIslemleri"].Rows[e.RowIndex]["VerilisTarihi"].ToString();
        }

        private void btngerial_Click(object sender, EventArgs e)
        {
            con.Open();
            string kayit = "update TblKitapIslemleri set Kitap=@Kitap,Vatandas=@Vatandas,VerilisTarihi=@VerilisTarihi,GeriTeslimTarihi=@GeriTeslimTarihi,Durum=@Durum where Id=@Id";
            SqlCommand komut = new SqlCommand(kayit, con);
            komut.Parameters.AddWithValue("@Kitap",int.Parse(txtAdi.Text) );
            komut.Parameters.AddWithValue("@Vatandas",int.Parse(txtVatandas.Text));
            komut.Parameters.AddWithValue("@VerilisTarihi", DateTime.Parse(txtTarih.Text));
            komut.Parameters.AddWithValue("@GeriTeslimTarihi", DateTime.Now);
            komut.Parameters.AddWithValue("@Durum", "Geri Teslim Edildi");
            komut.Parameters.AddWithValue("@Id", int.Parse(txtId.Text));
            komut.ExecuteNonQuery();
            con.Close();
            griddoldur();
            MessageBox.Show("Kitap İşlemi Güncellendi");
        }
    }
}
