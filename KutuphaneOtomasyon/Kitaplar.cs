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
    public partial class Kitaplar : Form
    {
        public Kitaplar()
        {
            InitializeComponent();
        }
        SqlCommand cmd;
        SqlConnection con;
        SqlDataAdapter da;
        DataSet ds;
        void griddoldur()
        {

            da = new SqlDataAdapter("Select *From TblKitaplar", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "TblKitaplar");
            dataGridView1.DataSource = ds.Tables["TblKitaplar"];
            con.Close();
        }

        private void Kitaplar_Load(object sender, EventArgs e)
        {
            con = new SqlConnection("Data Source=Localhost\\SQLEXPRESS;Initial Catalog=KutuphaneDB;Integrated Security=True");
            txtId.Enabled = false;
            griddoldur();
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int secilensatir = Convert.ToInt16(ds.Tables["TblKitaplar"].Rows[e.RowIndex]["Id"]);

            txtId.Text = secilensatir.ToString();
            txtTuru.Text = ds.Tables["TblKitaplar"].Rows[e.RowIndex]["KitapTuru"].ToString();
            txtAdedi.Text = ds.Tables["TblKitaplar"].Rows[e.RowIndex]["Adet"].ToString();
            txtAdi.Text = ds.Tables["TblKitaplar"].Rows[e.RowIndex]["KitapAdi"].ToString();
        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            con.Open();
            string kayit = "update TblKitaplar set KitapAdi=@KitapAdi,KitapTuru=@KitapTuru,Adet=@Adet where Id=@Id";
            // müşteriler tablomuzun ilgili alanlarını değiştirecek olan güncelleme sorgusu.
            SqlCommand komut = new SqlCommand(kayit, con);
            //Sorgumuzu ve baglantimizi parametre olarak alan bir SqlCommand nesnesi oluşturuyoruz.
            komut.Parameters.AddWithValue("@KitapAdi", txtAdi.Text);
            komut.Parameters.AddWithValue("@Kitapturu", txtTuru.Text);
            komut.Parameters.AddWithValue("@Adet", decimal.Parse(txtAdedi.Text));
            komut.Parameters.AddWithValue("@Id", int.Parse(txtId.Text));
            //Parametrelerimize Form üzerinde ki kontrollerden girilen verileri aktarıyoruz.
            komut.ExecuteNonQuery();
            //Veritabanında değişiklik yapacak komut işlemi bu satırda gerçekleşiyor.
            con.Close();
            griddoldur();
            MessageBox.Show("Kitap Bilgileri Güncellendi.");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            griddoldur();
        }

        private void btnKitapEkle_Click(object sender, EventArgs e)
        {
            KitapEkle fr = new KitapEkle();
            fr.ShowDialog();
        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtId.Text);
            string SilIslem = "DELETE FROM TblKitapIslemleri where Kitap=" + id;
            SqlCommand komutt = new SqlCommand(SilIslem, con);
            con.Open();
            komutt.ExecuteNonQuery();

            con.Close();
            string sil = "DELETE FROM TblKitaplar where Id=" + id;

            SqlCommand komut = new SqlCommand(sil, con);
            con.Open();
            komut.ExecuteNonQuery();

            con.Close();
            MessageBox.Show("Kitap ve İşlemleri Silindi");
            griddoldur();

        }

        private void btnVerilmisKitap_Click(object sender, EventArgs e)
        {
            VerilenKitaplar fr = new VerilenKitaplar();
            fr.ShowDialog();
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex > -1)//datagridview'ın herhangi birsatırın indisi olup olmadığını kontrol ediyoruz.

            {

                KitapVer fr = new KitapVer();

                fr.KitapId = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                fr.KitapAdi = dataGridView1.CurrentRow.Cells[1].Value.ToString();
               
                fr.Show();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            VatandasEkle fr = new VatandasEkle();
            fr.ShowDialog();
        }
    }
}
