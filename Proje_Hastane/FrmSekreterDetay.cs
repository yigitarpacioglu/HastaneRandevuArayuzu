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


namespace Proje_Hastane
{
    public partial class FrmSekreterDetay : Form
    {
        public FrmSekreterDetay()
        {
            InitializeComponent();
        }
        newsql nw = new newsql();

        public string sektcno;
        private void FrmSekreterDetay_Load(object sender, EventArgs e)
        {
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.MaximizeBox = false;
            LblTC.Text = sektcno;

            //Sekreter Ad-Soyad 

            SqlCommand cmd = new SqlCommand("Select SekreterAdSoyad from Tbl_Sekreter where @p1=SekreterTC", nw.ConnSql());
            cmd.Parameters.AddWithValue("@p1", LblTC.Text);
            SqlDataReader sdr = cmd.ExecuteReader();
            while(sdr.Read())
            {
                LblAdSoyad.Text = sdr[0].ToString();
            }
            nw.ConnSql().Close();

            // DataGridView Branş

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DataTable dt1 = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter("Select * from Tbl_Branslar", nw.ConnSql());
            sda.Fill(dt1);
            dataGridView1.DataSource = dt1;

            // DataGridView Doktor 
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DataTable dt2 = new DataTable();
            SqlDataAdapter sda2 = new SqlDataAdapter("Select (DoktorAd+' '+DoktorSoyad) as 'Doktorlar',DoktorBrans from Tbl_Doktorlar", nw.ConnSql());
            sda2.Fill(dt2);
            dataGridView2.DataSource = dt2;

            // Branşları Randevu Panelindeki ComboBox a aktarma
            SqlCommand cmd2 = new SqlCommand("Select BransAd from Tbl_Branslar", nw.ConnSql());
            SqlDataReader sdr2 = cmd2.ExecuteReader();
            while (sdr2.Read())
            {
                cmbBrans.Items.Add(sdr2[0]);
            }
            nw.ConnSql().Close();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("insert into Tbl_Randevular (RandevuTarih,RandevuSaat,RandevuBrans,RandevuDoktor) values (@r1,@r2,@r3,@r4)", nw.ConnSql());
            cmd.Parameters.AddWithValue("@r1", mskTarih.Text);
            cmd.Parameters.AddWithValue("@r2", mskSaat.Text);
            cmd.Parameters.AddWithValue("@r3", cmbBrans.Text);
            cmd.Parameters.AddWithValue("@r4", cmbDoktor.Text);
            cmd.ExecuteNonQuery();
            nw.ConnSql().Close();
            MessageBox.Show("Randevu kaydınız oluşturulmuştur.", "Randevu", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void cmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbDoktor.Items.Clear();

            SqlCommand cmd = new SqlCommand("Select DoktorAd,DoktorSoyad from Tbl_Doktorlar where DoktorBrans=@p1", nw.ConnSql());
            cmd.Parameters.AddWithValue("@p1", cmbBrans.Text);
            SqlDataReader sdr = cmd.ExecuteReader();
            while(sdr.Read())
            {
                cmbDoktor.Items.Add(sdr[0] + " " + sdr[1]);
            }
            nw.ConnSql().Close();
        }

        private void btnOlustur_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Insert into Tbl_Duyurular(duyuru) values(@d1)", nw.ConnSql());
            cmd.Parameters.AddWithValue("@d1",RchDuyuru.Text);
            cmd.ExecuteNonQuery();
            nw.ConnSql().Close();
            MessageBox.Show("Duyuru oluşturuldu"," ",MessageBoxButtons.OK,MessageBoxIcon.Information);

        }

        private void btnDoktorPanel_Click(object sender, EventArgs e)
        {
            FrmDoktorPaneli dr = new FrmDoktorPaneli();
            dr.Show();
            
        }

        private void btnBransPanel_Click(object sender, EventArgs e)
        {
            FrmBransPaneli fr = new FrmBransPaneli();
            fr.Show();
                    }

        private void btnRandevuListesi_Click(object sender, EventArgs e)
        {
            FrmRandevuListesi fr = new FrmRandevuListesi();
            fr.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmDuyurular fr = new FrmDuyurular();
            fr.Show();
        }

        private void BtnDuyurular_Click(object sender, EventArgs e)
        {
            FrmDuyurular fr = new FrmDuyurular();
            fr.Show();
        }
    }
}
