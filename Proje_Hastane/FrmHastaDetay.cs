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
using System.Data.SqlClient;

namespace Proje_Hastane
{
    public partial class FrmHastaDetay : Form
    {
        public FrmHastaDetay()
        {
            InitializeComponent();
        }

        public string tc;

        newsql nw = new newsql();

        private void FrmHastaDetay_Load(object sender, EventArgs e)
        {
            LblID.Enabled = false;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.MaximizeBox = false;

            //Ad-Soyad Taşıma

            LblTC.Text = tc;
            SqlCommand cmd = new SqlCommand("Select HastaAd, HastaSoyad from Tbl_Hastalar where HastaTC = @p1", nw.ConnSql());
            cmd.Parameters.AddWithValue("@p1", tc);  // LblTC.Text de yazılabilirdi.
            SqlDataReader sdr = cmd.ExecuteReader();

            while (sdr.Read())
            {
                LblAdSoyad.Text = sdr[0] + " " + sdr[1];
            }
            nw.ConnSql().Close();

            //Randevu Geçmişi
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * from Tbl_Randevular where HastaTC="+tc,nw.ConnSql());
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            //Branşları Çekme
            SqlCommand cmd2 = new SqlCommand("Select BransAd from Tbl_Branslar", nw.ConnSql());
            SqlDataReader sdr2 = cmd2.ExecuteReader();
            while(sdr2.Read())
            {
                cmbBrans.Items.Add(sdr2[0]); 
            }
            nw.ConnSql().Close();
        }

        private void cmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbDoktor.Items.Clear(); // normalde her doktor değiştirildiğinde bir önceki branstan doktorlar kalmaktaydı. Her seçim öncesi içerisini temizleyip bunu engelledik.
            SqlCommand cmd = new SqlCommand("Select DoktorAd, DoktorSoyad from Tbl_Doktorlar where DoktorBrans=@p1", nw.ConnSql());
            cmd.Parameters.AddWithValue("@p1", cmbBrans.Text);
            SqlDataReader sdr = cmd.ExecuteReader();
            while(sdr.Read())
            {
                cmbDoktor.Items.Add(sdr[0] + " " + sdr[1]);
            }
            nw.ConnSql().Close();
        }

        private void cmbDoktor_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter("Select * from Tbl_Randevular where RandevuBrans='" + cmbBrans.Text + "'" + "and RandevuDoktor='"+cmbDoktor.Text+"'and RandevuDurum=0", nw.ConnSql());
            sda.Fill(dt);
            dataGridView2.DataSource = dt;
        }

        private void lnkBilgiDuzenle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmHastaBilgiGuncelle fr = new FrmHastaBilgiGuncelle();
            fr.TCno = LblTC.Text;
            fr.Show();
            
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView2.SelectedCells[0].RowIndex;
            LblID.Text = dataGridView2.Rows[secilen].Cells[0].Value.ToString();
        }

        private void btnRandevuAl_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Update Tbl_Randevular set RandevuDurum=1, HastaTC=@p1, HastaSikayet=@p2 where RandevuID=@p3", nw.ConnSql());
            cmd.Parameters.AddWithValue("@p1", LblTC.Text);
            cmd.Parameters.AddWithValue("@p2", rchSikayet.Text);
            cmd.Parameters.AddWithValue("@p3", LblID.Text);
            cmd.ExecuteNonQuery();
            nw.ConnSql().Close();
            MessageBox.Show("Randevunuz oluşturulmuştur", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
