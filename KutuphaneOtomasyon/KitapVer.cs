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
    public partial class KitapVer : Form
    {
        public KitapVer()
        {
            InitializeComponent();
        }
        SqlCommand cmd;
        SqlConnection con;
        SqlDataAdapter da;
        DataSet ds;
        public int KitapId;
        public string KitapAdi;

        private void KitapVer_Load(object sender, EventArgs e)
        {
            con = new SqlConnection("Data Source=Localhost\\SQLEXPRESS;Initial Catalog=KutuphaneDB;Integrated Security=True");

            SqlCommand cmd = new SqlCommand("select * from TblVatandaslar order by VatandasAdi", con);
            con.Open();
            DataTable tbl = new DataTable();
            tbl.Load(cmd.ExecuteReader());
            con.Close();

            cbVatandas.DataSource = tbl;
            cbVatandas.DisplayMember = "VatandasAdi";
            cbVatandas.ValueMember = "Id";
            txtId.Text = KitapId.ToString();
            txtAdi.Text = KitapAdi;
            txtId.Enabled = false;
        }

        private void btnteslimet_Click(object sender, EventArgs e)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                // Bağlantımızı kontrol ediyoruz, eğer kapalıysa açıyoruz.
                string kayit = "insert into TblKitapIslemleri(Kitap,Vatandas,VerilisTarihi,GeriTeslimTarihi,Durum) values (@Kitap,@Vatandas,@VerilisTarihi,@GeriTeslimTarihi,@Durum)";
                // müşteriler tablomuzun ilgili alanlarına kayıt ekleme işlemini gerçekleştirecek sorgumuz.
                SqlCommand komut = new SqlCommand(kayit, con);
                //Sorgumuzu ve baglantimizi parametre olarak alan bir SqlCommand nesnesi oluşturuyoruz.
                komut.Parameters.AddWithValue("@Kitap",int.Parse(txtId.Text) );
                komut.Parameters.AddWithValue("@Vatandas", int.Parse(cbVatandas.GetItemText(cbVatandas.SelectedValue)));
                komut.Parameters.AddWithValue("@VerilisTarihi",DateTime.Now );
                komut.Parameters.AddWithValue("@GeriTeslimTarihi", DateTime.Now);
                komut.Parameters.AddWithValue("@Durum", "Geri Verilmedi");
              

                //Parametrelerimize Form üzerinde ki kontrollerden girilen verileri aktarıyoruz.
                komut.ExecuteNonQuery();
                //Veritabanında değişiklik yapacak komut işlemi bu satırda gerçekleşiyor.
                con.Close();
                MessageBox.Show("Kitap Teslim  İşlemi Gerçekleşti.");
            }
            catch (Exception hata)
            {
                MessageBox.Show("İşlem Sırasında Hata Oluştu." + hata.Message);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            VatandasEkle fr = new VatandasEkle();
            fr.ShowDialog();
        }
    }
}
